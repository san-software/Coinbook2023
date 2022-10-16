using Coinbook.Model;
using LiteDB.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Coinbook.Konvert
{
    public partial class frmKonvert : Form
    {
        Lite database = new Lite();
        List<Nation> nationen = new List<Nation>();
        Dictionary<string,int> Records = new Dictionary<string,int>();
        int gesamt = 0;
        public frmKonvert()
        {
            InitializeComponent();

            string text = "Die Datenbank von Coinbook muß jetzt auf ein neues Format konvertiert werden."
              + "Diese Konvertierung kann (je nach Anzahl Ihrer Sammlungsgebiete) relativ viel Zeit benötigen" + Environment.NewLine + Environment.NewLine
              + "Bitte haben Sie Geduld und schalten Sie während der Konvertierung den PC nicht aus!";

            txtAnzeige.Text = text;
            database.Initialize();
        }

        private void bgwVersion26_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwVersion26.ReportProgress(0, "0|Vorbereitung");

            if (!Directory.Exists(Path.Combine(database.DataPath, "Databases")))
                Directory.CreateDirectory(Path.Combine(database.DataPath, "Databases"));

            nationen = database.ReadNationen(true);
            var max = getRecordCount();

            //foreach (KeyValuePair<string, int> entity in Records)
            //    records = records + entity.Value;

            bgwVersion26.ReportProgress(0, $"1|{max}");

            //importErhaltungsgrade();
            //importCulture();
            //importSettings();
            //importNation();
            //importAera();
            //importRegion();
            //importKatalog();
            importAuktionen();
            //importPraegeanstalt();
            //importTexteDE();
            //importTexteEN();
            //importDetails();
            //importModule();
            importBestand();
            importEigeneBilder();
            importEigeneKatNr();
            importSammlung();
            //importParameter();

            MessageBox.Show("Konvertierung ist beendet");
        }

        ///// <summary>
        ///// Importiere Äras 
        ///// </summary>
        //private void importAera()
        //{
        //    int count = 0;
        //    string tabelle = "tblAera";
        //    List<Aera> liste = new List<Aera>();

        //    bgwVersion26.ReportProgress(0, $"0|{tabelle}");
        //    bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

        //    foreach (Nation modul in nationen)
        //    {
        //        liste = database.ReadAeras(modul.ID);
        //        database.ClearCollection("tblAera", modul.Key);
        //        database.BulkInsertAera(liste, modul.Key);

        //        count += liste.Count;
        //        gesamt += liste.Count;

        //        bgwVersion26.ReportProgress(count, "12|");
        //        bgwVersion26.ReportProgress(gesamt, "2|");
        //    }
        //}

        //private void importNation()
        //{
        //    string tabelle = "tblNation";
        //    List<Nation> liste = new List<Nation>();

        //    bgwVersion26.ReportProgress(0, $"0|{tabelle}");
        //    bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

        //    liste = database.ReadNationen();
        //    database.ClearCollection("tblNation", "Main");
        //    database.BulkInsertNation(liste,"Main");

        //    bgwVersion26.ReportProgress(Records[tabelle], "12|");
        //    gesamt += Records[tabelle];
        //    bgwVersion26.ReportProgress(gesamt, "2|");
        //}

        ///// <summary>
        ///// Importiere Region 
        ///// </summary>
        //private void importRegion()
        //{
        //    int count = 0;
        //    string tabelle = "tblGebiet";
        //    List<Gebiet> liste = new List<Gebiet>();

        //    bgwVersion26.ReportProgress(0, $"0|{tabelle}");
        //    bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

        //    foreach (Nation modul in nationen)
        //    {
        //        liste = database.ReadRegions(modul.ID);
        //        database.ClearCollection(tabelle, modul.Key);
        //        database.BulkInsertRegion(liste, modul.Key);

        //        count += liste.Count;
        //        gesamt += liste.Count;

        //        bgwVersion26.ReportProgress(count, "12|");
        //        bgwVersion26.ReportProgress(gesamt, "2|");
        //    }
        //}

        //private void importModule()
        //{
        //    string tabelle = "tblModule";
        //    List<Modul> liste = new List<Modul>();

        //    foreach (Nation modul in nationen)
        //    {
        //        bgwVersion26.ReportProgress(0, $"0|{tabelle}");
        //        bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

        //        liste = database.ReadModulLanguage(modul.ID);
        //        database.ClearCollection("tblModule", modul.Key);
        //        database.BulkInsertModule(liste, modul.Key);

        //        gesamt += liste.Count;

        //        bgwVersion26.ReportProgress(Records[tabelle], "12|");
        //        bgwVersion26.ReportProgress(gesamt, "2|");
        //    }
        //}

        ///// <summary>
        ///// Importiere Katalog 
        ///// </summary>
        //private void importKatalog()
        //{
        //    int count = 0;
        //    string tabelle = "tblkatalog";
        //    List<Katalog2> liste = new List<Katalog2>();

        //    bgwVersion26.ReportProgress(0, $"0|{tabelle}");
        //    bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

        //    foreach (Nation modul in nationen)
        //    {
        //        liste = database.ReadKatalog(modul.ID);

        //        bgwVersion26.ReportProgress(0, $"0|{tabelle}  /  {modul.Key}");

        //        database.ClearCollection(tabelle, modul.Key);
        //        database.BulkInsertKatalog(liste, modul.Key);

        //        count += liste.Count;
        //        gesamt += liste.Count; 

        //        bgwVersion26.ReportProgress(count, "12|");
        //        bgwVersion26.ReportProgress(gesamt, "2|");
        //    }
        //}

        //private void importTexteDE()
        //{
        //    int count = 0;
        //    string tabelle = "tblDetailTexteDE";
        //    List<Texte> liste = new List<Texte>();

        //    bgwVersion26.ReportProgress(0, $"0|{tabelle}");
        //    bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

        //    foreach (Nation modul in nationen)
        //    {
        //        bgwVersion26.ReportProgress(0, $"0|{tabelle}  /  {modul.Key}");

        //        liste = database.ReadDetailTextDE(modul.ID,"");

        //        count += liste.Count;
        //        gesamt += liste.Count;

        //        bgwVersion26.ReportProgress(count, "12|");
        //        bgwVersion26.ReportProgress(gesamt, "2|");

        //    database.ClearCollection(tabelle, modul.Key);
        //        database.BulkInsertTexteDE(liste, modul.Key);
        //    }
        //}

        //private void importTexteEN()
        //{
        //    int count = 0;
        //    string tabelle = "tblDetailTexteEN";
        //    List<Texte> liste = new List<Texte>();

        //    bgwVersion26.ReportProgress(0, $"0|{tabelle}");
        //    bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");


        //    foreach (Nation modul in nationen)
        //    {
        //        bgwVersion26.ReportProgress(0, $"0|{tabelle}  /  {modul.Key}");

        //            liste = database.ReadDetailTextEN(modul.ID, "");

        //        count += liste.Count;
        //        gesamt += liste.Count;

        //        bgwVersion26.ReportProgress(count, "12|");
        //        bgwVersion26.ReportProgress(gesamt, "2|");

        //    database.ClearCollection(tabelle, modul.Key);
        //        database.BulkInsertTexteEN(liste, modul.Key);
        //    }
        //}

        //private void importDetails()
        //{
        //    int count = 0;
        //    string tabelle = "tblDetails";

        //    List<MünzDetail> liste = new List<MünzDetail>();

        //    bgwVersion26.ReportProgress(0, $"0|{tabelle}");

        //    foreach (Nation modul in nationen)
        //    {
        //        bgwVersion26.ReportProgress(0, $"0|{tabelle}  /  {modul.Key}");
        //        bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

        //        liste = database.ReadMuenzDetails(modul.ID, "");

        //        count += liste.Count;
        //        gesamt += liste.Count;

        //        bgwVersion26.ReportProgress(count, "12|");
        //        bgwVersion26.ReportProgress(gesamt, "2|");

        //        database.ClearCollection(tabelle, modul.Key);
        //        database.BulkUpsertDetails(liste, modul.Key);
        //    }
        //}

        //private void importPraegeanstalt()
        //{
        //    int count = 0;
        //    string tabelle = "tblPraegeanstalt";
        //    List<Praegeanstalt> liste = new List<Praegeanstalt>();

        //    bgwVersion26.ReportProgress(0, $"0|{tabelle}");
        //    bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

        //    foreach (Nation modul in nationen)
        //    {
        //        liste = database.ReadPraegestellen(modul.ID);

        //        database.ClearCollection(tabelle, modul.Key);
        //        database.BulkInsertPraegeanstalt(liste, modul.Key);
        //        count++;

        //        bgwVersion26.ReportProgress(count, "12|");
        //        gesamt += count;
        //        bgwVersion26.ReportProgress(gesamt, "2|");
        //    }
        //}

        private void importAuktionen()
        {
            string tabelle = "tblAuktionen";
            List<Auktion> liste = new List<Auktion>();

            bgwVersion26.ReportProgress(0, $"0|{tabelle}");
            bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

            liste = database.ReadAuktionen();
            database.SaveAuktionen(liste);

            bgwVersion26.ReportProgress(Records[tabelle], "12|");
            gesamt += Records[tabelle];
            bgwVersion26.ReportProgress(gesamt, "2|");
        }

        private void importBestand()
        {
            string tabelle = "tblBestand";
            List<Bestand> liste = new List<Bestand>();

            bgwVersion26.ReportProgress(0, $"0|{tabelle}");
            bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

            liste = database.ReadBestand();

            database.SaveBestand(liste);

            bgwVersion26.ReportProgress(Records[tabelle], "12|");
            gesamt += liste.Count;
            bgwVersion26.ReportProgress(gesamt, "2|");
        }

        private void importEigeneKatNr()
        {
            string tabelle = "tblEigeneKatNr";
            List<EigeneKatNr> liste = new List<EigeneKatNr>();

            bgwVersion26.ReportProgress(0, $"0|{tabelle}");
            bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

            liste = database.ReadKatalogNummern();

            database.SaveEigeneKatNr(liste);

            bgwVersion26.ReportProgress(Records[tabelle], "12|");
            gesamt += liste.Count;
            bgwVersion26.ReportProgress(gesamt, "2|");
        }

        private void importSammlung()
        {
            string tabelle = "tblSammlung";
            List<Sammlung> liste = new List<Sammlung>();

            bgwVersion26.ReportProgress(0, $"0|{tabelle}");
            bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

            liste = database.ReadSammlung();

            database.SaveSammlung(liste);

            bgwVersion26.ReportProgress(Records[tabelle], "12|");
            gesamt += liste.Count;
            bgwVersion26.ReportProgress(gesamt, "2|");
        }

        private void importCulture()
        {
            string tabelle = "tblCulture";
            List<Culture> liste = new List<Culture>();

            bgwVersion26.ReportProgress(0, $"0|{tabelle}");
            bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

            liste = database.ReadCulture();
            database.BulkUpsertCulture(liste);

            bgwVersion26.ReportProgress(Records[tabelle], "12|");
            gesamt += Records[tabelle];
            bgwVersion26.ReportProgress(gesamt, "2|");
        }

        private void importErhaltungsgrade()
        {
            string tabelle = "tblErhaltungsgrad";
            List<Erhaltungsgrad> liste = new List<Erhaltungsgrad>();

            bgwVersion26.ReportProgress(0, $"0|{tabelle}");
            bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

            liste = database.ReadErhaltungsgrade("");
            database.BulkUpsertErhaltungsgrade(liste);

            bgwVersion26.ReportProgress(Records[tabelle], "12|");
            gesamt += Records[tabelle];
            bgwVersion26.ReportProgress(gesamt, "2|");
        }

        private void importSettings()
        {
            string tabelle = "tblSettings";
            Settings liste;

            bgwVersion26.ReportProgress(0, $"0|{tabelle}");
            bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

            liste = database.ReadSettings();
            database.SaveSettings(liste);

            bgwVersion26.ReportProgress(Records[tabelle], "12|");
            gesamt += Records[tabelle];
            bgwVersion26.ReportProgress(gesamt, "2|");
        }

        private void importEigeneBilder()
        {
            string tabelle = "tblEigeneBilder";
            List<EigeneBilder> liste;

            bgwVersion26.ReportProgress(0, $"0|{tabelle}");
            bgwVersion26.ReportProgress(0, $"11|{Records[tabelle]}");

            liste = database.ReadEigeneBilder();

            database.SaveEigeneBilder(liste);

            bgwVersion26.ReportProgress(Records[tabelle], "12|");
            gesamt += Records[tabelle];
            bgwVersion26.ReportProgress(gesamt, "2|");
        }

        private int getRecordCount()
        {
            int count = 0;
            List<string> collections = database.Collections("");
            foreach (var item in collections)
            {
                if (item.Substring(0, 1) != "$" && item != "tblTexte_DE" && item != "tblTexte_EN")
                {
                    Records.Add(item, database.Count(item, ""));
                    count += Records[item];
                }
            }

            return count;
        }

        private void bgwVersion26_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState.ToString() != "")
            {
                var text = e.UserState.ToString().Split('|');

                switch (text[0])
                {
                    case "0":               //Beschreibung
                        lblText.Text = text[1];
                        lblText.Refresh();
                        break;

                    case "1":               //Maximumm gesamt
                        pgbProgress.Maximum = Convert.ToInt32(text[1]);
                        break;

                    case "2":              //Progress gesamt
                        pgbProgress.Value = e.ProgressPercentage;
                        break;

                    case "11":               //Maximumm einzeln
                        pgbProgress2.Maximum = Convert.ToInt32(text[1]);
                        break;

                    case "12":              //Progress einzeln
                        pgbProgress2.Value = e.ProgressPercentage;
                        break;
                }
            }
        }

        private void bgwVersion26_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblEnde.Visible = true;
            Thread.Sleep(10000);

            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bgwVersion26.RunWorkerAsync();
        }
    }

}
