using Coinbook.Enumerations;
using Coinbook.Helper;
using Coinbook.Model;
using LiteDB.Database;
using SAN.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Coinbook.Sprache
{
    public partial class frmProgress : Form
    {
        private string language;
        private string table;
        ProgressParameter parameter = new ProgressParameter();
        List<Modul> transNation;


        public frmProgress(string language)
        {
            InitializeComponent();

            this.language = language;

            lblAnzeige.Text = "Texte werden auf die gewählte Sprache eingestellt";
            Text = "Sprachumstellung";

            table = "tblDetailTexte" + language;


            transNation = DatabaseHelper.LiteDatabase.ReadModulLanguage(language,"Nation", null);
            //transAera = DatabaseHelper.LiteDatabase.ReadModulLanguage(language,"Ära");
            //transRegion = DatabaseHelper.LiteDatabase.ReadModulLanguage(,language,"Gebiet");
        }

        public new void ShowDialog()
        {
            bgwLanguage.RunWorkerAsync();

            base.ShowDialog();
        }

        private void bgwLanguage_DoWork(object sender, DoWorkEventArgs e)
        {
            int max = DatabaseHelper.LiteDatabase.Count("tblSettings2") + 3;

            language = language.ToUpper();

            parameter.Text = "Initialisierung";
            parameter.Command = 4;
            parameter.Max = max;
            bgwLanguage.ReportProgress(0, parameter);

            parameter.Text = "Bearbeite Nation";
            parameter.Command = 3;
            bgwLanguage.ReportProgress(1, parameter);
            translateNations();

            parameter.Text = "Bearbeite Ära";
            parameter.Command = 3;
            bgwLanguage.ReportProgress(2, parameter);
            translateAeras();

            parameter.Text = "Bearbeite Gebiet";
            parameter.Command = 3;
            bgwLanguage.ReportProgress(3, parameter);
            translateRegions();

            translateMotiv();
        }

        private void bgwLanguage_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressParameter parameter = (ProgressParameter)e.UserState;

            pgbProgress.MaxValue = Convert.ToInt32(parameter.Max);
            pgbProgress.Value = e.ProgressPercentage;
            pgbProgress.Text = parameter.Text;

            switch (parameter.Command)
            {
                case 0:         //set maxValue
                    if (parameter.Max > 0)
                        pgbProgress.MaxValue = Convert.ToInt32(parameter.Max);
                    else
                        pgbProgress.MaxValue = e.ProgressPercentage;
                    break;

                case 1:         // set value
                    pgbProgress.Value = e.ProgressPercentage;
                    break;

                case 2:         //set maxValue
                    pgbProgress.Text = parameter.Text;
                    break;

                case 3:         // set value
                    pgbProgress.Value = e.ProgressPercentage;
                    pgbProgress.Text = string.Format("{0} {1} / {2}", parameter.Text, e.ProgressPercentage, parameter.Max);
                    pgbProgress.MaxValue = Convert.ToInt32(parameter.Max);
                    break;

                case 4:         // set value
                    pgbProgress.Value = e.ProgressPercentage;
                    pgbProgress.Text = parameter.Text;
                    pgbProgress.MaxValue = Convert.ToInt32(parameter.Max);
                    break;
            }
        }

        private void bgwLanguage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void translateNations()
        {
            transNation=DatabaseHelper.LiteDatabase.ReadModulLanguage(language, "Nation", null);
            var nationen = DatabaseHelper.LiteDatabase.ReadNationen();

            foreach (var item in nationen)
            {
                var temp = transNation.FirstOrDefault(x => x.ModulID == item.ID && x.Sprache == language);
                if (temp != null)
                    item.Bezeichnung = temp.Text;
            }

            DatabaseHelper.LiteDatabase.BulkUpsertNation(nationen);
        }

        private void translateAeras()
        {
            List<Nation> nationen = DatabaseHelper.LiteDatabase.ReadNationen();

            foreach (var nation in nationen)
            {
                List<Modul> transAera = DatabaseHelper.LiteDatabase.ReadModulLanguage(language, "Ära", CoinbookHelper.ModulKey);
                var aeras = DatabaseHelper.LiteDatabase.ReadAeras(CoinbookHelper.ModulKey, nation.ID);

                foreach (var item in aeras)
                {
                    var temp = transAera.FirstOrDefault(x => x.ModulID == item.ID && x.Sprache == language);
                    if (temp != null)
                        item.Bezeichnung = temp.Text;
                }

                DatabaseHelper.LiteDatabase.BulkUpsertAera(aeras, CoinbookHelper.ModulKey);
            }
        }

        private void translateRegions()
        {
            List<Nation> nationen = DatabaseHelper.LiteDatabase.ReadNationen();
            int max = 0;

            foreach (var nation in nationen)
                max = max + DatabaseHelper.LiteDatabase.Count(nation.Key);

                foreach (var nation in nationen)
            {
                List<Modul> transRegion = DatabaseHelper.LiteDatabase.ReadModulLanguage(language, "Gebiet", CoinbookHelper.ModulKey);
                var regions = DatabaseHelper.LiteDatabase.ReadRegions(CoinbookHelper.ModulKey, nation.ID);

                foreach (var item in regions)
                {
                    var temp = transRegion.FirstOrDefault(x => x.ModulID == item.ID && x.Sprache == language);
                    if (temp != null)
                        item.Bezeichnung = temp.Text;
                }

                DatabaseHelper.LiteDatabase.BulkUpsertRegion(regions, CoinbookHelper.ModulKey);
            }
        }

        private void translateMotiv()
        {
            int i = 0;

            List<Katalog2> katalog = new List<Katalog2>();
            List<Nation> nationen = DatabaseHelper.LiteDatabase.ReadNationen();
            foreach (var nation in nationen)
            {
                List<Aera> aeras = DatabaseHelper.LiteDatabase.ReadAeras(nation.Key);

                foreach (var aera in aeras)
                {
                    List<Texte> transMotiv = DatabaseHelper.LiteDatabase.ReadDetailTexte(language, nation.ID, CoinbookHelper.ModulKey);
                    katalog = DatabaseHelper.LiteDatabase.ReadKatalog(nation.Key, aera.ID);

                    foreach (var item in katalog)
                    {
                        i++;
                        var temp = transMotiv.FirstOrDefault(x => x.GUID == item.GUID);
                        if (temp != null)
                            item.Motiv = temp.Motiv;

                        parameter.Text = "Bearbeite Motive für " + nation.Bezeichnung;
                        parameter.Command = 3;
                        bgwLanguage.ReportProgress(i, parameter);
                    }
                    //var motive = LiteDatabase.ReadTexteLanguage(table, nation.id, enmTexte.Motiv, CoinbookHelper.ModulKey);
                    //var katalog = LiteDatabase.ReadKatalog(nation.id, CoinbookHelper.ModulKey);
                }
            }

            DatabaseHelper.LiteDatabase.BulkUpsertKatalog(katalog, CoinbookHelper.ModulKey);
        }
    }
}

