using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using SAN.Converter;
using System.Data.OleDb;
using Coinbook.Model;
using System.Windows.Forms;
using Coinbook.Enumerations;
using LiteDB.Database;
using Syncfusion.Windows.Forms;
using System.Globalization;
using Coinbook.Helper;

namespace Coinbook.Import
{
    public partial class frmImportCoinbook30 : Form
    {
        private OleDbConnection accessConnection;
        private Dictionary<string, int> xxx = new Dictionary<string, int>();
        Lite database = new Lite();
        private CultureInfo culture;
        private bool autostart = true;

        public frmImportCoinbook30()
        {
            InitializeComponent();

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook");
            LanguageHelper.CreateLocalization(resourcePath);

            autostart = true;
            database.Initialize();
        }

        public frmImportCoinbook30(bool noAutostart)
        {
            InitializeComponent();

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook");
            LanguageHelper.CreateLocalization(resourcePath);

            autostart = false;
            database.Initialize();
        }

        public new void ShowDialog(IWin32Window w = null)
        {
            btnCancel.Visible = true;
            btnImport.Visible = true;

            if (w == null)
                base.ShowDialog();
            else
                base.ShowDialog(w);
        }

        private void import()
        {
            string connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\ProgramData\\Coinbook\\coinbook.mdb;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";
            accessConnection = new OleDbConnection(connectionstring);

            var katalogPath = Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Katalog");

            string file = @"c:\Programdata\Coinbook\Coinbook.db";
            if (File.Exists(file))
                File.Delete(file);

            file = @"c:\Programdata\Coinbook\Coinbook-log.db";
            if (File.Exists(file))                File.Delete(file);

            file = @"c:\Programdata\Coinbook\Coinbook-Sammlung.db";
            if (File.Exists(file))
                File.Delete(file);

            Directory.CreateDirectory(katalogPath);

            var nationen = importNation(accessConnection);
            importSettings2(accessConnection, nationen);

            using (Class1 class1 = new Class1())
            {
                //class1.ModulUpdates();

                importSettings(accessConnection);
                importParameter(accessConnection);
                importBestand(accessConnection);
                importEigeneBilder(accessConnection);
                importEigeneKatNr(accessConnection);
                importPreise(accessConnection);
                importSammlung(accessConnection);
                importAuktionen(accessConnection);
                importErhaltungsgrad(accessConnection);
                importCulture(accessConnection);
                importModuleSQL(accessConnection);

                database = null;
            }
            this.TopMost = false;
            MessageBoxAdv.Show("Fertig");

            Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            btnImport.Enabled = false;
            import();
        }

        /// <summary>
        /// Importiere Nationen von Access nach Firebird
        /// </summary>
        /// <param name="accessConnection"></param>
        private List<Nation> importNation(OleDbConnection accessConnection)
        {
            string tabelle = "tblNation";
            List<Nation> liste = new List<Nation>();

            string sql = string.Format("select * from {0}", tabelle);

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Nation nation = new Nation();
                    nation.ID = ConvertEx.ToInt32(dr["ID"]);
                    nation.Bezeichnung = dr["Bezeichnung"].ToString();
                    nation.Bestellnummer = dr["Bestellnummer"].ToString();
                    nation.InUse = (bool)dr["InUse"];
                    nation.Key = dr["Key"].ToString();
                    nation.Mapping = "";

                    liste.Add(nation);
                }

                database.BulkInsertNation(liste);

            }

            accessConnection.Close();

            return liste;
        }

        private void importSettings2(OleDbConnection accessConnection, List<Nation> nationen)
        {
            List<Settings2> liste = new List<Settings2>();

            string sql = string.Format("select * from {0}", "tblSettings2");

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Settings2 item = new Settings2();
                    item.id = ConvertEx.ToInt32(dr["id"]) + 1;
                    item.Lizenz = dr["Lizenz"].ToString();
                    item.Jahr = dr["Jahr"].ToString();
                    //item.Key = dr["Key"].ToString();    

                    liste.Add(item);
                }
            }
            database.BulkInsertSettings2(liste);

            accessConnection.Close();
        }

        private void importAuktionen(OleDbConnection accessConnection)
        {
            List<Auktion> liste = new List<Auktion>();

            string sql = "select * from tblAuktionen";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Auktion item = new Auktion();
                    item.ID = ConvertEx.ToInt32(dr["ID"]);
                    item.Auktionator = dr["Auktionator"].ToString();
                    item.Auktionshaus = dr["Auktionshaus"].ToString();
                    item.Guid = dr["Guid"].ToString();
                    item.Datum = dr["Datum"].ToString();
                    item.Erhaltungsgrad = ConvertEx.ToInt32(dr["Erhaltungsgrad"]);

                    liste.Add(item);
                }

                database.BulkInsertAuktionen(liste);
            }
            accessConnection.Close();
        }

        private void importBestand(OleDbConnection accessConnection)
        {
            int countTable = 0;
            List<Bestand> liste = new List<Bestand>();  

            string sql = "select tblBestand.*, tblKatalog.NationID, tblKatalog.AeraID, GebietID from tblBestand left join tblKatalog on tblBestand.guid = tblKatalog.Guid";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Bestand item = new Bestand();
                    item.id = countTable + 1;
                    item.Guid = dr["GUID"].ToString();
                    item.S = ConvertEx.ToInt32(dr["S"]);
                    item.SP = ConvertEx.ToInt32(dr["SP"]);
                    item.SS = ConvertEx.ToInt32(dr["SS"]);
                    item.SSP = ConvertEx.ToInt32(dr["SSP"]);
                    item.VZ = ConvertEx.ToInt32(dr["VZ"]);
                    item.VZP = ConvertEx.ToInt32(dr["VZP"]);
                    item.STN = ConvertEx.ToInt32(dr["STN"]);
                    item.STH = ConvertEx.ToInt32(dr["STH"]);
                    item.PP = ConvertEx.ToInt32(dr["PP"]);
                    item.DS = ConvertEx.ToInt32(dr["DS"]);
                    item.DSP = ConvertEx.ToInt32(dr["DSP"]);
                    item.DSS = ConvertEx.ToInt32(dr["DSS"]);
                    item.DSSP = ConvertEx.ToInt32(dr["DSSP"]);
                    item.DVZ = ConvertEx.ToInt32(dr["DVZ"]);
                    item.DVZP = ConvertEx.ToInt32(dr["DVZP"]);
                    item.DSTN = ConvertEx.ToInt32(dr["DSTN"]);
                    item.DSTH = ConvertEx.ToInt32(dr["DSTH"]);
                    item.DPP = ConvertEx.ToInt32(dr["DPP"]);
                    item.NationID = ConvertEx.ToInt32(dr["tblKatalog.NationID"]);
                    item.AeraID = ConvertEx.ToInt32(dr["tblKatalog.AeraID"]);
                    item.GebietID = ConvertEx.ToInt32(dr["GebietID"]);
                    liste.Add(item);

                    countTable++;
                }
            }
            database.BulkInsertBestand(liste);

            accessConnection.Close();
        }

        private void importEigeneBilder(OleDbConnection accessConnection)
        {
            List<EigeneBilder> liste = new List<EigeneBilder>();

            string sql = "select * from tblEigeneBilder";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    EigeneBilder item = new EigeneBilder();
                    item.ID = ConvertEx.ToInt32(dr["id"]);
                    item.Guid = dr["GUID"].ToString();
                    item.DateiName = dr["DateiName"].ToString();
                    item.ShowPicture = ConvertEx.ToBoolean(dr["ShowPicture"]);

                    liste.Add(item);
                }
            }
            database.BulkInsertEigeneBilder(liste);

            accessConnection.Close();
        }

        private void importEigeneKatNr(OleDbConnection accessConnection)
        {
            List<EigeneKatNr> liste = new List<EigeneKatNr>();
            Dictionary<string, int> nationen = new Dictionary<string, int>();

            accessConnection.Open();

            string sql = "Select Distinct KatNr, NationID from tblKatalog where KatNr <>'' order by KatNr";
            using (OleDbCommand cmd = new OleDbCommand(sql, accessConnection))
            {
                using (OleDbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        if (!nationen.ContainsKey(dr["KatNr"].ToString()))
                            nationen.Add(dr["KatNr"].ToString(), ConvertEx.ToInt32(dr["NationID"]));
                }
            }

            sql = "select * from tblEigeneKatNr";
            using (OleDbCommand cmd = new OleDbCommand(sql, accessConnection))
            {
                using (OleDbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EigeneKatNr item = new EigeneKatNr();
                        item.ID = dr["Coinbook"].ToString();
                        item.Coinbook = dr["Coinbook"].ToString();
                        item.KatNr = dr["KatNr"].ToString();

                        if (nationen.ContainsKey(item.Coinbook))
                            item.NationID = nationen[item.Coinbook];

                        liste.Add(item);
                    }
                }
            }
            database.BulkInsertEigeneKatNr(liste);

            accessConnection.Close();
        }

        private void importPreise(OleDbConnection accessConnection)
        {
            List<Preise> liste = new List<Preise>();

            string sql = "select *,tblKatalog.NationID, tblKatalog.AeraID from tblPreise left join tblKatalog on tblPreise.guid = tblKatalog.Guid";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);

            int id = 0;
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    id++;
                    Preise item = new Preise();
                    item.ID = id;
                    item.GUID = dr["tblPreise.GUID"].ToString();
                    item.SPreis = ConvertEx.ToDecimal(dr["tblPreise.SPreis"]);
                    item.SPPreis = ConvertEx.ToDecimal(dr["tblPreise.SPPreis"]);
                    item.SSPreis = ConvertEx.ToDecimal(dr["tblPreise.SSPreis"]);
                    item.SSPPreis = ConvertEx.ToDecimal(dr["tblPreise.SSPPreis"]);
                    item.VZPreis = ConvertEx.ToDecimal(dr["tblPreise.VZPreis"]);
                    item.VZPPreis = ConvertEx.ToDecimal(dr["tblPreise.VZPPreis"]);
                    item.STNPreis = ConvertEx.ToDecimal(dr["tblPreise.STNPreis"]);
                    item.STHPreis = ConvertEx.ToDecimal(dr["tblPreise.STHPreis"]);
                    item.PPPreis = ConvertEx.ToDecimal(dr["tblPreise.PPPreis"]);
                    item.NationID = ConvertEx.ToInt32(dr["tblKatalog.NationID"]);

                    liste.Add(item);
                }
            }
            database.BulkInsertPreise(liste);

            accessConnection.Close();
        }

        private void importSammlung(OleDbConnection accessConnection)
        {
            int countTable = 0;
            List<Sammlung> liste = new List<Sammlung>();
            List<Erhaltungsgrad> erhaltung = database.ReadErhaltungsgrade("de");

            string sql = "SELECT tblSammlung.*, tblKatalog.NationID FROM tblSammlung left JOIN tblKatalog ON tblSammlung.Guid = tblKatalog.Guid";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Sammlung item = new Sammlung();
                    item.ID = countTable + 1;
                    item.Erhaltung = ConvertEx.ToInt32(dr["Erhaltung"]);
                    item.Guid = dr["Guid"].ToString();
                    item.Ablage = dr["Ablage"].ToString();
                    item.Kaufort = dr["Kaufort"].ToString();
                    item.Verkaeufer = dr["Verkaeufer"].ToString();
                    item.Kommentar = dr["Kommentar"].ToString();
                    item.FehlerText = dr["FehlerText"].ToString();
                    item.KatNrEigen = dr["KatNrEigen"].ToString();
                    item.Picture = dr["Picture"].ToString();
                    item.Doublette = ConvertEx.ToBoolean(dr["Doublette"]);
                    item.Fehlerhaft = ConvertEx.ToBoolean(dr["Fehlerhaft"]);
                    item.ShowPicture = ConvertEx.ToBoolean(dr["ShowPicture"]);
                    item.Kaufpreis = ConvertEx.ToDecimal(dr["Kaufpreis"]);

                    DateTime d = DateTime.Now;
                    item.Kaufdatum = DateTime.TryParse(dr["Kaufdatum"].ToString(),out d) ? Convert.ToDateTime(dr["Kaufdatum"]).ToShortDateString() : string.Empty;

                    if (item.Erhaltung > 0)
                        item.Erhaltungsgrad = erhaltung[item.Erhaltung-1].Erhaltung;
                    item.NationID = ConvertEx.ToInt32(dr["NationID"]);

                    liste.Add(item);

                    countTable++;
                }
                database.BulkInsertSammlung(liste);

                accessConnection.Close();
            }
        }

        private void importErhaltungsgrad(OleDbConnection accessConnection)
        {
            string tabelle = "tblErhaltungsgrad";
            int countTable = 0;
            List<Erhaltungsgrad> liste = new List<Erhaltungsgrad>();

            string sql = string.Format("select * from {0}", tabelle);

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Erhaltungsgrad item = new Erhaltungsgrad();
                    item.ID = countTable + 1;
                    item.ErhaltungsgradID = ConvertEx.ToInt32(dr["id"]);
                    item.Sprache = dr["Sprache"].ToString();
                    item.Erhaltung = dr["Erhaltung"].ToString();
                    item.Bezeichnung = dr["Bezeichnung"].ToString();
                    item.Land = dr["Land"].ToString();

                    liste.Add(item);

                    countTable++;
                }
            }

            database.BulkUpsertErhaltungsgrade(liste);

            accessConnection.Close();
        }

        private void importCulture(OleDbConnection accessConnection)
        {
            string tabelle = "tblCulture";
            List<Culture> liste = new List<Culture>();

            string sql = string.Format("select * from {0}", tabelle);

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Culture item = new Culture();
                    item.ID = ConvertEx.ToInt32(dr["id"]) + 1;
                    item.Bezeichnung = dr["Bezeichnung"].ToString();
                    item.EN_GB = dr["EN_GB"].ToString();
                    item.Waehrung = dr["Waehrung"].ToString();
                    item.Kultur = dr["Kultur"].ToString();
                    item.Faktor = Convert.ToDecimal(dr["Faktor"], culture);

                    liste.Add(item);
                }
            }
            database.BulkUpsertCulture(liste);

            accessConnection.Close();
        }

        private void importSettings(OleDbConnection accessConnection)
        {
            List<Settings> liste = new List<Settings>();

            string sql = string.Format("select * from {0}", "tblSettings");

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);

            Settings item = new Settings();

            item.ID = 1;

            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    switch (dr["id"].ToString())
                    {
                        case "Activated":
                            item.Activated = dr["Wert"].ToString();
                            break;

                        case "Top":
                            item.Top = ConvertEx.ToInt32(dr["Wert"]);
                            break;

                        case "Left":
                            item.Left = ConvertEx.ToInt32(dr["Wert"]);
                            break;

                        case "Width":
                            item.Width = ConvertEx.ToInt32(dr["Wert"]);
                            break;

                        case "Height":
                            item.Height = ConvertEx.ToInt32(dr["Wert"]);
                            break;

                        case "Culture":
                            item.Culture = dr["Wert"].ToString();

                            break;
                        case "MünzTab":
                            item.MünzTab = dr["Wert"].ToString();

                            break;
                        case "Nachname":
                            item.Nachname = dr["Wert"].ToString();

                            break;
                        case "Vorname":
                            item.Vorname = dr["Wert"].ToString();
                            break;

                        case "Strasse":
                            item.Strasse = dr["Wert"].ToString();
                            break;

                        case "PLZ":
                            item.PLZ = dr["Wert"].ToString();
                            break;

                        case "Ort":
                            item.Ort = dr["Wert"].ToString();
                            break;

                        case "Land":
                            item.Land = dr["Wert"].ToString();
                            break;

                        case "Mail":
                            item.Mail = dr["Wert"].ToString();
                            break;

                        case "CurrentCurrency":
                            item.CurrentCurrency = ConvertEx.ToInt32(dr["Wert"]);
                            break;

                        case "CurrentWährung":
                            item.CurrentWährung = dr["Wert"].ToString();
                            break;

                        case "CurrentFaktor":
                            item.CurrentFaktor = ConvertEx.ToDecimal(dr["Wert"]);
                            break;

                        case "Exemplarsammler":
                            item.Exemplarsammler = ConvertEx.ToBoolean(dr["Wert"]);
                            break;

                        case "SelectedStyle":
                            {
                                string temp = dr["Wert"].ToString();
                                if (temp == "Sammlung") temp = "Icon";
                                item.SelectedStyle = (enmSelectedStyle)Enum.Parse(typeof(enmSelectedStyle), temp);
                            }
                            break;

                        case "Nation":
                            item.Nation = ConvertEx.ToInt32(dr["Wert"]);
                            break;

                        case "Ära":
                            item.Ära = ConvertEx.ToInt32(dr["Wert"]);
                            break;

                        case "Gebiet":
                            item.Gebiet = ConvertEx.ToInt32(dr["Wert"]);
                            break;

                        case "UpdatePath":
                            item.UpdatePath = dr["Wert"].ToString();
                            break;

                        case "MünzdetailIndex":
                            item.MünzdetailIndex = (enmMünzdetailIndex)Enum.Parse(typeof(enmMünzdetailIndex), dr["Wert"].ToString());
                            break;

                        case "LastUsed":
                            item.LastUsed = ConvertEx.ToBoolean(dr["Wert"]);
                            break;

                        case "Preisvorgabe":
                            item.Preisvorgabe = ConvertEx.ToBoolean(dr["Wert"]);
                            break;

                        case "International":
                            item.International = dr["Wert"].ToString();
                            break;

                        case "Ablage":
                            item.Ablage = dr["Wert"].ToString();
                            break;

                        case "PrintDestination":
                            item.PrintDestination = (enmPrintDestination)Enum.Parse(typeof(enmPrintDestination), dr["Wert"].ToString());
                            break;

                        case "ReportFolder":
                            item.ReportFolder = dr["Wert"].ToString();
                            break;

                        case "Lizenzkey":
                            item.Lizenzkey = dr["Wert"].ToString();
                            break;

                        case "Maximized":
                            item.Maximized = ConvertEx.ToBoolean(dr["Wert"]);
                            break;

                        case "NatFirst":
                            item.NatFirst = ConvertEx.ToBoolean(dr["Wert"]);
                            break;

                        case "ColumnWidth":
                            item.ColumnWidth = dr["Wert"].ToString();
                            break;

                        case "Telefon":
                            item.Telefon = dr["Wert"].ToString();
                            break;

                        case "Preise":
                            item.Preise = (enmPreise)Enum.Parse(typeof(enmPreise), dr["Wert"].ToString());
                            break;

                        case "ModulAutoUpdate":
                            item.ModulAutoUpdate = ConvertEx.ToBoolean(dr["Wert"]);
                            break;

                        case "BackupByQuit":
                            item.BackupByQuit = ConvertEx.ToBoolean(dr["Wert"]);
                            break;

                        case "Passwort":
                            item.Passwort = dr["Wert"].ToString();
                            break;

                        default:
                            break;
                    }
                }
            }

            item.DatabaseVersion = Application.ProductVersion;
            culture = CultureInfo.GetCultureInfo(item.Culture);

            database.InsertSettings(item);

            accessConnection.Close();
        }

        private void importModuleSQL(OleDbConnection accessConnection)
        {
            string tabelle = "tblModule";
            int countTable = 0;
            List<Modul> liste = new List<Modul>();

            string sql = string.Format("select * from {0}", tabelle);

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Modul item = new Modul();
                    item.ID = countTable + 1;
                    item.ModulID = ConvertEx.ToInt32(dr["id"]);
                    item.Typ = dr["Typ"].ToString();
                    item.Sprache = dr["sprache"].ToString();
                    item.Text = dr["Text"].ToString();
                    item.NationID = ConvertEx.ToInt32(dr["NationID"]);

                    liste.Add(item);

                    countTable++;
                }
            }

            database.BulkInsertModule(liste);

            accessConnection.Close();
        }

        private void importParameter(OleDbConnection accessConnection)
        {
            string tabelle = "tblParameter";
            List<Parameter> liste = new List<Parameter>();

            string sql = string.Format("select * from {0}", tabelle);

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Parameter item = new Parameter();
                    item.id = dr["ID"].ToString();
                    item.Paramter = dr["Paramter"].ToString();

                    liste.Add(item);
                }
            }
            database.BulkInsertParameter(liste);

            accessConnection.Close();
        }
        
        private void frmImportCoinbook30_Shown(object sender, EventArgs e)
        {
            if (autostart)
            {
                btnCancel.Visible = false;
                btnImport.Visible = false;

                import();
            }
        }
    }
}
