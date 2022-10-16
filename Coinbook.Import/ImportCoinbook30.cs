using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAN.Converter;
using System.Data.OleDb;
using Coinbook.Model;
using Coinbook.Enumerations;

namespace Coinbook.Import
{
    public class ImportCoinbook30
    {
        public void Import()
        {
            string connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\ProgramData\\Coinbook\\coinbook.mdb;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";
            OleDbConnection accessConnection = new OleDbConnection(connectionstring);

            importNation(accessConnection);
            importAera(accessConnection);
            importRegion(accessConnection);
            //importKatalog(accessConnection);
            importAuktionen(accessConnection);
            importBestand(accessConnection);
            importEigeneBilder(accessConnection);
            importEigeneKatNr(accessConnection);
            importPreise(accessConnection);
            importSammlung(accessConnection);
            importSettings(accessConnection);
            importSettings2(accessConnection);
            importCulture(accessConnection);
            importDBVersion(accessConnection);
            importDownloads(accessConnection);
            importErhaltungsgrad(accessConnection);
            importModuleSQL(accessConnection);
            importPraegeanstalt(accessConnection);
            importParameter(accessConnection);
            importPreisliste(accessConnection);
            //importTexte(accessConnection, "DE");
            //importTexte(accessConnection, "EN");
        }

        /// <summary>
        /// Importiere Nationen von Access nach Firebird
        /// </summary>
        /// <param name="accessConnection"></param>
        private void importNation(OleDbConnection accessConnection)
        {
            List<Nation> list = new List<Nation>();
            string sql = "select * from tblNation";

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
                    nation.InUse = (bool)dr["bInUse"];
                    nation.Key = dr["Key"].ToString();
                    list.Add(nation);
                }
            }

            foreach (Nation item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        /// <summary>
        /// Importiere Äras von Access nach Firebird
        /// </summary>
        /// <param name="accessConnection"></param>
        private void importAera(OleDbConnection accessConnection)
        {
            //Aeras
            List<Aera> listAera = new List<Aera>();
            string sql = "select * from tblAera";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Aera aera = new Aera();
                    aera.ID = ConvertEx.ToInt32(dr["ID"]);
                    aera.Bezeichnung = dr["Bezeichnung"].ToString();
                    aera.NationID = ConvertEx.ToInt32(dr["Nat"]);
                    aera.Sortierung = ConvertEx.ToInt32(dr["Sortierung"]);
                    listAera.Add(aera);
                }
            }

            foreach (Aera item in listAera)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        /// <summary>
        /// Importiere Gebiete von Access nach Firebird
        /// </summary>
        /// <param name="accessConnection"></param>
        private void importRegion(OleDbConnection accessConnection)
        {
            List<Gebiet> listGebiet = new List<Gebiet>();
            string sql = "select * from tblGebiet";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Gebiet region = new Gebiet();
                    region.ID = ConvertEx.ToInt32(dr["ID"]);
                    region.Bezeichnung = dr["Bezeichnung"].ToString();
                    region.NationID = ConvertEx.ToInt32(dr["Nat"]);
                    region.Sortierung = ConvertEx.ToInt32(dr["Sortierung"]);
                    region.AeraID = ConvertEx.ToInt32(dr["Aera"]);
                    listGebiet.Add(region);
                }
            }

            foreach (Gebiet item in listGebiet)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importAuktionen(OleDbConnection accessConnection)
        {
            List<Auktionen> list = new List<Auktionen>();
            string sql = "select * from stblAuktionen";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Auktionen item = new Auktionen();
                    item.ID = ConvertEx.ToInt32(dr["ID"]);
                    item.Auktionator = dr["Auktionator"].ToString();
                    item.Auktionshaus = dr["Auktionshaus"].ToString();
                    item.Guid = dr["ID_Katalog"].ToString();
                    if (dr["Datum"] == DBNull.Value)
                        item.Datum = string.Empty;
                    else
                        item.Datum = dr["Datum"].ToString();
                    list.Add(item);
                }
            }

            foreach (Auktionen item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }


        private void importBestand(OleDbConnection accessConnection)
        {
            List<Bestand> list = new List<Bestand>();
            string sql = "select * from stblBestand";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Bestand item = new Bestand();
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
                    list.Add(item);
                }
            }

            foreach (Bestand item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importEigeneBilder(OleDbConnection accessConnection)
        {
            List<EigeneBilder> list = new List<EigeneBilder>();
            string sql = "select * from stblEigeneBilder";

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
                    list.Add(item);
                }
            }

            foreach (EigeneBilder item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importEigeneKatNr(OleDbConnection accessConnection)
        {
            List<EigeneKatNr> list = new List<EigeneKatNr>();
            string sql = "select * from stblEigeneKatNr";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    EigeneKatNr item = new EigeneKatNr();
                    item.Coinbook = dr["Coinbook"].ToString();
                    item.KatNr = dr["KatNr"].ToString();
                    list.Add(item);
                }
            }
            foreach (EigeneKatNr item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importPreise(OleDbConnection accessConnection)
        {
            List<Preise> list = new List<Preise>();
            string sql = "select * from stblPreise";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Preise item = new Preise();
                    item.ID = ConvertEx.ToInt32(dr["id"]);
                    item.GUID = dr["GUID"].ToString();
                    item.SPreis = ConvertEx.ToDecimal(dr["SPreis"]);
                    item.SPPreis = ConvertEx.ToDecimal(dr["SPPreis"]);
                    item.SSPreis = ConvertEx.ToDecimal(dr["SSPreis"]);
                    item.VZPreis = ConvertEx.ToDecimal(dr["VZPreis"]);
                    item.VZPPreis = ConvertEx.ToDecimal(dr["VZPPreis"]);
                    item.STNPreis = ConvertEx.ToDecimal(dr["STNPreis"]);
                    item.STHPreis = ConvertEx.ToDecimal(dr["STHPreis"]);
                    item.PPPreis = ConvertEx.ToDecimal(dr["PPPreis"]);
                    list.Add(item);
                }
            }

            foreach (Preise item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importSammlung(OleDbConnection accessConnection)
        {
            List<Sammlung> list = new List<Sammlung>();
            string sql = "select * from stblSammlung";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Sammlung item = new Sammlung();
                    item.ID = ConvertEx.ToInt32(dr["id"]);
                    item.Erhaltung = ConvertEx.ToInt32(dr["ID_ErhaltungsGrad"]);
                    item.Guid = dr["ID_Katalog"].ToString();
                    item.Ablage = dr["Ablage"].ToString();
                    item.Kaufort = dr["Kaufort"].ToString();
                    item.Verkaeufer = dr["Verkaeufer"].ToString();
                    item.Kommentar = dr["Kommentar"].ToString();
                    item.FehlerText = dr["FehlerText"].ToString();
                    item.KatNrEigen = dr["KatNrEigen"].ToString();
                    item.Picture = dr["Picture"].ToString();
                    item.Doublette = ConvertEx.ToBoolean(dr["BOOL_Doublette"]);
                    item.Fehlerhaft = ConvertEx.ToBoolean(dr["BOOL_Fehlerhaft"]);
                    item.ShowPicture = ConvertEx.ToBoolean(dr["ShowPicture"]);
                    item.Kaufpreis = ConvertEx.ToDecimal(dr["Preis"]);
                    item.Kaufdatum = (dr["Kaufdatum"] != DBNull.Value ? Convert.ToDateTime(dr["Kaufdatum"]).ToShortDateString() : "");
                    list.Add(item);
                }

                foreach (Sammlung item in list)
                {
                    Database.Database.Instance.Insert(item);
                }

                accessConnection.Close();
            }
        }

        private void importSettings(OleDbConnection accessConnection)
        {
            List<Settings> list = new List<Settings>();
            string sql = "select * from stblSettings";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Settings item = new Settings();
                    item.ID = dr["id"].ToString();
                    item.Wert = dr["Wert"].ToString();
                    list.Add(item);
                }
            }

            foreach (Settings item in list)
                Database.Database.Instance.Insert(item);

            accessConnection.Close();
        }

        private void importSettings2(OleDbConnection accessConnection)
        {
            List<Settings2> list = new List<Settings2>();
            string sql = "select * from stblSettings2";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Settings2 item = new Settings2();
                    item.id = ConvertEx.ToInt32(dr["id"]);
                    item.Lizenz = dr["Lizenz"].ToString();
                    item.Jahr = dr["Jahr"].ToString();
                    list.Add(item);
                }
            }

            foreach (Settings2 item in list)
            {
                Database.Database.Instance.Insert(item);
            }
            accessConnection.Close();
        }

        private void importCulture(OleDbConnection accessConnection)
        {
            List<Culture> list = new List<Culture>();
            string sql = "select * from tblCulture";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Culture item = new Culture();
                    item.id = ConvertEx.ToInt32(dr["id"]);
                    item.Bezeichnung = dr["Bezeichnung"].ToString();
                    item.EN_GB = dr["EN_GB"].ToString();
                    item.Waehrung = dr["Währung"].ToString();
                    item.Kultur = dr["Culture"].ToString();
                    item.Faktor = ConvertEx.ToDecimal(dr["Faktor"]);
                    list.Add(item);
                }
            }

            accessConnection.Close();
        }

        private void importDBVersion(OleDbConnection accessConnection)
        {
            List<DBVersion> list = new List<DBVersion>();
            string sql = "select * from tblDBVersion";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DBVersion item = new DBVersion();
                    item.colVersion = dr["colVersion"].ToString();
                    list.Add(item);
                }
            }

            foreach (DBVersion item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importDownloads(OleDbConnection accessConnection)
        {
            List<Downloads> list = new List<Downloads>();
            string sql = "select * from tblDownloads";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Downloads item = new Downloads();
                    item.id = ConvertEx.ToInt32(dr["id"]);
                    item.Key = dr["Key"].ToString();
                    item.Lizenz = dr["Lizenz"].ToString();
                    item.Datum = (dr["Datum"] != null ? Convert.ToDateTime(dr["Datum"]).ToShortDateString() : "");
                    list.Add(item);
                }
            }

            foreach (Downloads item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importErhaltungsgrad(OleDbConnection accessConnection)
        {
            List<Erhaltungsgrad> list = new List<Erhaltungsgrad>();
            string sql = "select * from tblErhaltungsgrad";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Erhaltungsgrad item = new Erhaltungsgrad();
                    item.ID = ConvertEx.ToInt32(dr["id"]);
                    item.Sprache = dr["Sprache"].ToString();
                    item.Erhaltung = dr["Erhaltungsgrad"].ToString();
                    item.Bezeichnung = dr["Bezeichnung"].ToString();
                    item.Land = dr["Land"].ToString();
                    list.Add(item);
                }
            }

            foreach (Erhaltungsgrad item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importModuleSQL(OleDbConnection accessConnection)
        {
            List<Modul> list = new List<Modul>();
            string sql = "select * from tblModule";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Modul item = new Modul();
                    item.ID = ConvertEx.ToInt32(dr["id"]);
                    item.Typ = dr["Typ"].ToString();
                    item.Sprache = dr["sprache"].ToString();
                    item.Text = dr["Text"].ToString();
                    item.NationID = ConvertEx.ToInt32(dr["Nation"]);
                    list.Add(item);
                }
            }

            foreach (Modul item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importPraegeanstalt(OleDbConnection accessConnection)
        {
            List<Praegeanstalt> list = new List<Praegeanstalt>();
            string sql = "select * from tblPrägeanstalt";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Praegeanstalt item = new Praegeanstalt();
                    item.ID = ConvertEx.ToInt32(dr["id"]);
                    item.Nation = ConvertEx.ToInt32(dr["Nation"]);
                    item.Muenzzeichen = dr["Münzzeichen"].ToString();
                    item.Ort = dr["Ort"].ToString();
                    item.Adresse = dr["Adresse"].ToString();
                    item.Email = dr["Email"].ToString();
                    item.Homepage = dr["Homepage"].ToString();
                    item.Bemerkung = dr["Bemerkung"].ToString();
                    item.Caption = dr["Caption"].ToString();
                    item.Zeit = dr["Zeit"].ToString();
                    list.Add(item);
                }
            }

            foreach (Praegeanstalt item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importParameter(OleDbConnection accessConnection)
        {
            List<Parameter> list = new List<Parameter>();
            string sql = "select * from tblParameter";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Parameter item = new Parameter();
                    item.id = dr["ID"].ToString();
                    item.Paramter = dr["Paramter"].ToString();
                    list.Add(item);
                }
            }

            foreach (Parameter item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        private void importPreisliste(OleDbConnection accessConnection)
        {
            List<Preisliste> list = new List<Preisliste>();
            string sql = "select * from tblPreisliste";

            accessConnection.Open();
            OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Preisliste item = new Preisliste();

                    item.Bestellnummer = dr["Bestellnummer"].ToString();
                    item.Modul = dr["Modul"].ToString();
                    item.Waehrung = dr["Waehrung"].ToString();
                    item.Jahr = dr["Jahr"].ToString();
                    item.Preis = ConvertEx.ToDecimal(dr["Preis"]);
                    list.Add(item);
                }
            }

            foreach (Preisliste item in list)
            {
                Database.Database.Instance.Insert(item);
            }

            accessConnection.Close();
        }

        //private void importTexte(OleDbConnection accessConnection, string sprache)
        //{
        //    List<Beschreibung> list = new List<Beschreibung>();
        //    string sql = "select * from tblTexte where sprache=@sprache";

        //    accessConnection.Open();
        //    OleDbCommand cmd = new OleDbCommand(sql, accessConnection);
        //    cmd.Parameters.AddWithValue("sprache", sprache);

        //    using (OleDbDataReader dr = cmd.ExecuteReader())
        //    {
        //        while (dr.Read())
        //        {
        //            Beschreibung item = new Beschreibung();
        //            enmTexte temp = (enmTexte)Enum.Parse(typeof(enmTexte), dr["Typ"].ToString(), true);
        //            item.Guid = dr["GUID"].ToString();
        //            item.Typ = ConvertEx.ToInt32(temp);
        //            item.Text = dr["Text"].ToString();
        //            item.NationID = ConvertEx.ToInt32(dr["Nation"]);
        //            list.Add(item);
        //        }
        //    }

        //    Database.Database.Instance.InsertTexte(list,sprache, null);
  
        //    accessConnection.Close();
        //}

    }
}
