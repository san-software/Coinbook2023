using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using SAN.Converter;
using System.Data.OleDb;
using LiteDB.Database;
using Coinbook.Model;
using System.Linq;
using Coinbook.Lokalisierung;
using Syncfusion.Windows.Forms;
using System.Xml;
using Coinbook.Helper;

namespace Coinbook.Import
{
    public partial class frmImport2XML : Form
    {
        string ConnectionString;

        private string oldPath = String.Empty;
        private Lite database = new Lite();

        private List<Erhaltungsgrad> Erhaltungsgrade;

        public frmImport2XML(string sprache)
        {
            InitializeComponent();
            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook");
            LanguageHelper.CreateLocalization(resourcePath);

            LanguageHelper.Localization.UpdateModul(this);

            Erhaltungsgrade = database.ReadErhaltungsgrade(sprache);
        }

        /// <summary>
        /// Nach CB 6 Installation suchen.
        /// </summary>
        private void butSearch_Click(object sender, EventArgs e)
        {
            if (dlgFolder.ShowDialog() == DialogResult.OK)
            {
                if (dlgFolder.SelectedPath.Length > 0)
                    btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "tblSammlung.xml"));
            }
        }

        /// <summary>
        /// Import starten.
        /// </summary>
        private void butImport_Click(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
            btnImport.Enabled = false;
            btnSearch.Enabled = false;

            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgLoad");
            lblAnzeige.Visible = true;
            pgbProgress.Visible = true;

            bgwWorker.RunWorkerAsync();
        }

        private void bgwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(dlgFolder.SelectedPath, "tblSammlung.xml"));

            XmlNode root = document.SelectSingleNode("dataroot");
            XmlNodeList liste = root.SelectNodes("tblSammlung");

            //Sammlung
            List<Sammlung> sammlung = new List<Sammlung>();
            database.ClearCollection("tblSammlung");

            int i = 0;
            foreach (XmlNode node in liste)
            {
                i++;
                bgwWorker.ReportProgress(i * 100 / liste.Count, "Bearbeite Sammlung");

                Sammlung item = new Sammlung();
                item.ID = i;
                item.Guid = node.SelectSingleNode("ID_Katalog").InnerText.ToString();
                item.Ablage = node.SelectSingleNode("Ablage").InnerText.ToString();
                item.Kaufort = node.SelectSingleNode("Kaufort").InnerText.ToString();
                item.Verkaeufer = node.SelectSingleNode("Verkaeufer").InnerText.ToString();

                if (node.SelectSingleNode("Preis") != null)
                    item.Kaufpreis = ConvertEx.ToDecimal(node.SelectSingleNode("Preis").InnerText);
                else
                    item.Kaufpreis = 0;

                item.KatNrEigen = node.SelectSingleNode("KatNrEigen").InnerText.ToString();
                item.FehlerText = node.SelectSingleNode("Kommentar").InnerText.ToString();
                item.Fehlerhaft = item.FehlerText != "";
                item.Kaufdatum = node.SelectSingleNode("Kaufdatum").InnerText.ToString();
                item.Erhaltung = Convert.ToInt32(node.SelectSingleNode("ID_ErhaltungsGrad").InnerText);
                item.Erhaltungsgrad = Erhaltungsgrade[item.Erhaltung - 1].Bezeichnung;
                item.Doublette = ConvertEx.ToBoolean(node.SelectSingleNode("BOOL_Doublette").InnerText);
                item.EigenerPreis = 0;
                item.Kommentar = string.Empty;
                item.KatNr = database.GetCoinFromGuid(item.Guid, CoinbookHelper.ModulKey).KatNr;

                sammlung.Add(item);

                if (sammlung.Count > 10)
                {
                    database.BulkInsertSammlung(sammlung);
                    sammlung.Clear();
                }
            }

            database.BulkInsertSammlung(sammlung);


            ////Eigener Preis
            //cmd = "Select * from tblEigenerPreis";
            //dt = getDataTable(cmd);

            //database.ClearCollection("tblPreise");
            ////database.Execute("Delete * from tblEigenerPreis");

            //List<Preise> preise = new List<Preise>();

            //Dictionary<string, int> d = new Dictionary<string, int>();

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string guid = dt.Rows[i]["ID_Kat"].ToString();

            //    bgwWorker.ReportProgress(i * 100 / dt.Rows.Count, LanguageHelper.Localization.GetTranslation(Name, "msgPreise"));

            //    if (preise.Any(x => x.GUID == guid))
            //    {
            //        Preise preis = new Preise();
            //        preis.ID = i;
            //        preis.GUID = guid;
            //        preis.SPreis =0;
            //        preis.SPPreis =0;
            //        preis.SSPreis = 0;
            //        preis.VZPreis = 0;
            //        preis.VZPPreis = 0;
            //        preis.STNPreis = 0;
            //        preis.STHPreis = 0;
            //        preis.PPPreis = 0;
            //        preis.NationID = database.GetCoinFromGuid(preis.GUID).NationID;

            //        preise.Add(preis);
            //    }

            //    var item = preise.First(x => x.GUID == guid);

            //    switch (ConvertEx.ToInt32(dt.Rows[i]["ID_Erh"]))
            //    {
            //        case 1:
            //            item.SPreis = ConvertEx.ToDecimal(dt.Rows[i]["SPreis"]);
            //            break;

            //        case 2:
            //            item.SPPreis = ConvertEx.ToDecimal(dt.Rows[i]["SPPreis"]);
            //            break;

            //        case 3:
            //            item.SSPreis = ConvertEx.ToDecimal(dt.Rows[i]["SSPreis"]);
            //            break;

            //        case 4:
            //            item.SSPPreis = ConvertEx.ToDecimal(dt.Rows[i]["SSPreis"]);
            //            break;

            //        case 5:
            //            item.VZPreis = ConvertEx.ToDecimal(dt.Rows[i]["VZPreis"]);
            //            break;

            //        case 6:
            //            item.VZPPreis = ConvertEx.ToDecimal(dt.Rows[i]["VZPPreis"]);
            //            break;

            //        case 7:
            //            item.STNPreis = ConvertEx.ToDecimal(dt.Rows[i]["STNPreis"]);
            //            break;

            //        case 8:
            //            item.STHPreis = ConvertEx.ToDecimal(dt.Rows[i]["STHPreis"]);
            //            break;

            //        case 9:
            //            item.PPPreis = ConvertEx.ToDecimal(dt.Rows[i]["PPPreis"]);
            //            break;
            //    }
            //}

            //database.BulkInsertPreise(preise);

            ////Helper.RepairBestand(database);         TODO

            ////Auktion
            //cmd = "Select * from tblAuktionen";
            //dt = getDataTable(cmd);

            //List<Auktion> auktionen = new List<Auktion>();

            //database.ClearCollection("tblAuktionen");

            //cmd = "insert Into tblAuktionen (id, ID_Katalog, ID_Erhaltungsgrad, Datum, Preis, Auktionator, Auktionshaus) values(";

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    bgwWorker.ReportProgress(i * 100 / dt.Rows.Count, LanguageHelper.Localization.GetTranslation(Name, "msgAuktion"));

            //    Auktion auktion = new Auktion();

            //    auktion.Auktionator = dt.Rows[i]["Auktionator"].ToString();
            //    auktion.Auktionshaus = dt.Rows[i]["Auktionshaus"].ToString();
            //    auktion.Datum = dt.Rows[i]["Datum"].ToString();
            //    auktion.Erhaltungsgrad = Convert.ToInt32(dt.Rows[i]["ID_Erhaltungsgrad"]);
            //    auktion.Guid = dt.Rows[i]["ID_Katalog"].ToString();
            //    auktion.ID = i;
            //    auktion.Preis = Convert.ToDecimal(dt.Rows[i]["Preis"]);

            //    auktionen.Add(auktion);
            //}

            //database.BulkInsertAuktionen(auktionen);

            ////Eigene Bilder
            //cmd = "Select * from tblEigeneBilder";
            //dt = getDataTable(cmd);

            //List<EigeneBilder> bilder = new List<EigeneBilder>();

            //database.ClearCollection("tblEigeneBilder");
            //cmd = "insert Into tblEigeneBilder (id, Guid, DateiName) values(";

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    bgwWorker.ReportProgress(i * 100 / dt.Rows.Count, LanguageHelper.Localization.GetTranslation(Name, "msgImage"));

            //    EigeneBilder bild = new EigeneBilder();

            //    bild.ID = i;
            //    bild.Guid = dt.Rows[i]["ID_Kat"].ToString();
            //    bild.DateiName = dt.Rows[i]["DateiName"].ToString();

            //    bilder.Add(bild);
            //}

            //database.BulkInsertEigeneBilder(bilder);

            ////TODO Eigene Katalognummer
        }

        private void bgwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgbProgress.Value = e.ProgressPercentage;
            lblAnzeige.Text = e.UserState.ToString();
        }

        private void bgwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pgbProgress.Maximum = 0;
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgReady");

            //bgwBearbeiten.RunWorkerAsync();
            btnClose.Enabled = true;

            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
