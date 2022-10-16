using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Coinbook.Enumerations;
using Coinbook.Model;
using Coinbook.Lokalisierung;

using System.Linq;
using System.IO;
//using Syncfusion.WinForms.DataGrid.Styles;
//using Syncfusion.Windows.Forms.PdfViewer;
using Syncfusion.Pdf;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System.Threading;
using Coinbook.Helper;

namespace Coinbook
{
    partial class frmReportingWert : Form
    {
        private List<Wertermittlung> wertermittlung;
        private List<KeyValuePair<int, string>> nationen = new List<KeyValuePair<int, string>>();
        private Settings settings = null;
        private List<Nation> nationsx = null;
        public frmReportingWert()
        {
            InitializeComponent();
            Localization.UpdateModul(this);

            progressBar.WaitingGradientEnabled = true;
            progressBar.ProgressStyle = ProgressBarStyles.WaitingGradient;
            progressBar.WaitingGradientInterval = 1;
            progressBar.WaitingGradientWidth = 300;
            progressBar.GradientEndColor = Color.IndianRed;
            progressBar.GradientStartColor = Color.MistyRose;
            progressBar.TextVisible = false;
        }

        public new void ShowDialog()
        {
            Cursor = Cursors.WaitCursor;

            switch (Liste)
            {
                case enmReportTyp.WerteSammlung:
                    Text = Localization.GetTranslation(Name, "Wert") + " " + Localization.GetTranslation(Name, "Sammlung");
                    break;
                case enmReportTyp.WerteDoubletten:
                    Text = Localization.GetTranslation(Name, "Wert") + " " + Localization.GetTranslation(Name, "Doubletten");
                    break;
                case enmReportTyp.KostenSammlung:
                    Text = Localization.GetTranslation(Name, "Kosten") + " " + Localization.GetTranslation(Name, "Sammlung");
                    break;
                case enmReportTyp.KostenDoubletten:
                    Text = Localization.GetTranslation(Name, "Kosten") + " " + Localization.GetTranslation(Name, "Doubletten");
                    break;
            }

            var nations = DatabaseHelper.LiteDatabase.ReadModulLizenzen();
            settings = DatabaseHelper.LiteDatabase.ReadSettings();

            //var nationsx = ReportHelper.GetNations(Localization.GetTranslation("Keys", "allNations"));
            nationsx = DatabaseHelper.LiteDatabase.ReadNationen(true);

            nationen = new List<KeyValuePair<int, string>>();
            foreach (var item in nations)
                nationen.Add(new KeyValuePair<int, string>(item.id, item.Lizenz));

            cboNationen.DisplayMember = "Value";
            cboNationen.ValueMember = "Key";
            cboNationen.DataSource = nationen;
            cboNationen.SelectedIndex = 0;

            Cursor = Cursors.WaitCursor;
            //btnWork.Enabled = false;

            progressBar.WaitingGradientEnabled = true;
            lblAnzeige.Text = string.Format(Localization.GetTranslation("Keys", "msgLoad"), cboNationen.Text);

            progressBar.Visible = true;
            lblAnzeige.Visible = true;
            cboNationen.Enabled = false;

            NationID = ((KeyValuePair<int, string>)cboNationen.SelectedItem).Key;

            bgwCalculate.RunWorkerAsync();

            base.ShowDialog();
        }

        public int NationID { get; set; }
        public enmReportTyp Liste { get; set; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            while (bgwCalculate.IsBusy)
            {
                string temp = Localization.GetTranslation("Keys", "msgWaitForBackgroundWorker");
                AutoClosingMessageBox.Show(temp, "Coinbook");
                Thread.Sleep(1000);
            }

            Close();
        }

        private void cboNation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bgwCalculate.IsBusy)
            {
                Cursor = Cursors.WaitCursor;

                progressBar.Visible = true;

                NationID = ((KeyValuePair<int, string>)cboNationen.SelectedItem).Key;

                lblAnzeige.Text = string.Format(Localization.GetTranslation("Keys", "msgLoad"), cboNationen.Text);

                progressBar.Visible = true;
                lblAnzeige.Visible = true;
                cboNationen.Enabled = false;

                grdAnzeige.DataSource = null;

                bgwCalculate.RunWorkerAsync();

                cboNationen.Enabled = false;
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgCreatePDF");
            lblAnzeige.Visible = true;
            progressBar.Visible = true;
            Application.DoEvents();

            ReportHelper.ReportToPDF(grdAnzeige.TableControl, this, Text, false);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
            progressBar.Visible = false;
            Application.DoEvents();

            this.Cursor = Cursors.Default;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgExcportExcel");
            lblAnzeige.Visible = true;
            progressBar.Visible = true;
            Application.DoEvents();

            ReportHelper.ReportToExcel(grdAnzeige, this, Text, enmReporting.Wertermittlung);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
            progressBar.Visible = false;
            Application.DoEvents();

            this.Cursor = Cursors.Default;
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgCreateCSV");
            lblAnzeige.Visible = true;
            progressBar.Visible = true;

            ReportHelper.ReportToCSV(wertermittlung);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
            progressBar.Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgPrint");
            lblAnzeige.Visible = true;
            progressBar.Visible = true;

            ReportHelper.ReportToPrinter(grdAnzeige.TableControl, this, false);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
            progressBar.Visible = false;
        }

        private void bgwCalculate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            List<Aera> aeras = null;

            if (NationID != 0)
                aeras = DatabaseHelper.LiteDatabase.ReadAeras(NationID);

            wertermittlung = DatabaseHelper.LiteDatabase.ReportingWert(Liste, NationID, settings.CurrentFaktor, settings.Preise, nationsx, aeras);
        }

        private void bgwCalculate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            wertermittlung[wertermittlung.Count - 1].Nation = Localization.GetTranslation(Name, "Gesamt");

            List<Aera> aeras = new List<Aera>();
            List<KeyValuePair<int, string>> nation = new List<KeyValuePair<int, string>>();

            if (NationID == 0)
            {
                for (int i = 1; i < cboNationen.Items.Count; i++)
                {
                    Aera a = new Aera();
                    a.ID = ((KeyValuePair<int, string>)cboNationen.Items[i]).Key;
                    a.Bezeichnung = ((KeyValuePair<int, string>)cboNationen.Items[i]).Value;
                    aeras.Add(a);
                }

            }
            else
            {
                for (int i = 1; i < cboNationen.Items.Count; i++)
                {
                    Aera a = new Aera();
                    a.ID = ((KeyValuePair<int, string>)cboNationen.Items[i]).Key;
                    a.Bezeichnung = ((KeyValuePair<int, string>)cboNationen.Items[i]).Value;
                    aeras.Add(a);
                }
            }

            KeyValuePair<int, string> n = new KeyValuePair<int, string>(
                ((KeyValuePair<int, string>)cboNationen.SelectedItem).Key, 
                ((KeyValuePair<int, string>)cboNationen.SelectedItem).Value);
            nation.Add(n);

            ReportHelper.SetRelationsWert(grdAnzeige, nation, wertermittlung);
            GridformatReport();

            cboNationen.Enabled = true;
            progressBar.Visible = false;
            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;

            Cursor = Cursors.Default;
        }

        private void GridformatReport()
        {
            try
            {
                grdAnzeige.ChildGroupOptions.ShowAddNewRecordBeforeDetails = false;
                grdAnzeige.ChildGroupOptions.ShowAddNewRecordAfterDetails = false;

                grdAnzeige.Table.ExpandAllGroups();
                grdAnzeige.Table.ExpandAllRecords();
                grdAnzeige.TopLevelGroupOptions.ShowColumnHeaders = false;

                grdAnzeige.TableDescriptor.Columns["Value"].Width = 0;
                grdAnzeige.TableDescriptor.Columns["Key"].Width = 0;
                grdAnzeige.TableDescriptor.Columns["Value"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorNation);
                grdAnzeige.TableDescriptor.Columns["Value"].Appearance.AnyCell.Font.Bold = true;
                grdAnzeige.TableDescriptor.Columns["Value"].Appearance.AnyCell.Font.Size = 10;
                grdAnzeige.TableDescriptor.Columns["Value"].Width = 835;

                grdAnzeige.GetTable("tblReport").DefaultCaptionRowHeight = 0;
                grdAnzeige.GetTable("tblReport").DefaultColumnHeaderRowHeight = 35;
                grdAnzeige.GetTableDescriptor("tblReport").Appearance.AnyHeaderCell.Font.Bold = true;

                var x = grdAnzeige.GetTableDescriptor("tblReport").Columns;

                //grdAnzeige.GetTableDescriptor("tblReport").Columns["NationID"].Width = 0;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nation"].Width = 150;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Width = 85;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nation"].HeaderText = Localization.GetTranslation(Name, "Beschreibung");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].HeaderText = CoinbookHelper.Erhaltungsgrade[0].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].HeaderText = CoinbookHelper.Erhaltungsgrade[1].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].HeaderText = CoinbookHelper.Erhaltungsgrade[2].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].HeaderText = CoinbookHelper.Erhaltungsgrade[3].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].HeaderText = CoinbookHelper.Erhaltungsgrade[4].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].HeaderText = CoinbookHelper.Erhaltungsgrade[5].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].HeaderText = CoinbookHelper.Erhaltungsgrade[6].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].HeaderText = CoinbookHelper.Erhaltungsgrade[8].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].HeaderText = Localization.GetTranslation(Name, "Anzahl");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].HeaderText = Localization.GetTranslation(Name, "Gesamt") + "\n[" + settings.CurrentWährung + "]";

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nation"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.Format = "###,##0.00";
            }
            catch (Exception ex)
            {
            }
        }

        private void grdAnzeige_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            // Applies style for a particular Row.
            if (wertermittlung != null)
            {
                if (e.TableCellIdentity.RowIndex == wertermittlung.Count+1) 
                {
                    e.Style.BackColor = Color.Silver;
                    e.Style.Font.Bold = true;
                }
            }
        }
    }
}

