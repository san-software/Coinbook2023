using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.GroupingGridExcelConverter;
using Syncfusion.GridHelperClasses;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.XlsIO;
using Coinbook.Enumerations;
using Coinbook.Lokalisierung;
using Syncfusion.Windows.Forms;
using System.ComponentModel;
using Syncfusion.Windows.Forms.Tools;
using System.Threading;
using Coinbook.Helper;

namespace Coinbook
{
    partial class frmReporting2 : Form
    {
        private DataTable dt;
        private DataTable dtNation;
        private DataTable dtÄra;
        private DataTable dtGebiet;

        public frmReporting2()
        {
            InitializeComponent();
            Localization.UpdateModul(this);

            cboNation.ValueMember = "iD";
            cboNation.DisplayMember = "Bezeichnung";

            cboÄra.ValueMember = "iD";
            cboÄra.DisplayMember = "Bezeichnung";

            cboGebiet.ValueMember = "iD";
            cboGebiet.DisplayMember = "Bezeichnung";

            progressBar.WaitingGradientEnabled = true;
            progressBar.ProgressStyle = ProgressBarStyles.WaitingGradient;
            progressBar.WaitingGradientInterval = 1;
            progressBar.WaitingGradientWidth = 300;
            progressBar.GradientEndColor = System.Drawing.Color.IndianRed;
            progressBar.GradientStartColor = System.Drawing.Color.MistyRose;
            progressBar.TextVisible = false;
        }

        public new void ShowDialog(IWin32Window window)
        {
            Cursor = Cursors.WaitCursor;

            switch (Liste)
            {
                case enmReportTyp.ReportSammlung:
                    Text = "Reporting " + Localization.GetTranslation(Name, "Sammlung");
                    break;

                case enmReportTyp.ReportDoubletten:
                    Text = "Reporting " + Localization.GetTranslation(Name, "Doubletten");
                    break;
            }
            cboNation.DataSource = CoinbookHelper.GetNations(Localization.GetTranslation("Keys", "allNations"));
            cboNation.SelectedValue = NationID;
            cboNation_SelectedIndexChanged(null, null);

            cboÄra.SelectedValue = ÄraID;
            cboÄra_SelectedIndexChanged(null, null);

            cboGebiet.SelectedValue = GebietID;
            cboGebiet_SelectedIndexChanged(null, null);

            grdAnzeige.Visible = false;
            btnWork.Enabled = false;

            lblAnzeige.Text = string.Format(Localization.GetTranslation("Keys", "msgLoad"), cboNation.Text);

            bgwCalculate.RunWorkerAsync();

            base.ShowDialog(window);
        }


        public int NationID { get; set; }
        public int ÄraID { get; set; }
        public int GebietID { get; set; }
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
            if (cboNation.SelectedValue == null)
                cboNation.SelectedIndex = 0;

            NationID = (int)cboNation.SelectedValue;

            cboÄra.DataSource = CoinbookHelper.GetAeras(NationID, Localization.GetTranslation("Keys", "allÄras"));
            cboÄra_SelectedIndexChanged(null, null);
        }

        private void cboÄra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ÄraID = (int)cboÄra.SelectedValue;
            cboGebiet.DataSource = CoinbookHelper.GetRegions(ÄraID, Localization.GetTranslation("Keys", "allAreas"));
            cboGebiet_SelectedIndexChanged(null, null);
        }

        private void cboGebiet_SelectedIndexChanged(object sender, EventArgs e)
        {
            GebietID = (int)cboGebiet.SelectedValue;
        }

        private void GridformatReport()
        {
            try
            {
                ReportHelper.FormatReportGroups(grdAnzeige, 785, 767, 749);

                grdAnzeige.GetTableDescriptor("tblReport").Columns["KatNr"].HeaderText = Localization.GetTranslation(Name, "KatNr");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzz"].HeaderText = Localization.GetTranslation(Name, "Muenzz");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].HeaderText = Localization.GetTranslation(Name, "Jahrgang");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].HeaderText = Localization.GetTranslation(Name, "Nominal");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].HeaderText = Localization.GetTranslation(Name, "Waehrung");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Motiv"].HeaderText = Localization.GetTranslation(Name, "Motiv");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Erhaltung"].HeaderText = Localization.GetTranslation(Name, "Erhaltung");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].HeaderText = Localization.GetTranslation(Name, "Anzahl");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Preis"].HeaderText = Localization.GetTranslation(Name, "Preis");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].HeaderText = Localization.GetTranslation(Name, "Gesamt");

                grdAnzeige.GetTableDescriptor("tblReport").Columns["NationID"].Width = 0;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["AeraID"].Width = 0;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["KatNr"].Width = 69;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].Width = 75;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].Width = 40;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].Width = 50;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzz"].Width = 50;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Motiv"].Width = 175;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Erhaltung"].Width = 100;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].Width = 50;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Preis"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Width = 80;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Preis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Preis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Preis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.Format = "###,##0.00";

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Preis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);

                GridSummaryColumnDescriptor anzahl = SyncfusionHelper.SummaryColumnDescriptor("Anzahl", TypeCode.Int32);
                GridSummaryColumnDescriptor gesamt = SyncfusionHelper.SummaryColumnDescriptor("Gesamt", TypeCode.Double);

                GridSummaryRowDescriptor srd = new GridSummaryRowDescriptor();
                srd.Appearance.AnyCell.BackColor = CoinbookHelper.ColorSumme;
                srd.SummaryColumns.Add(anzahl);
                srd.SummaryColumns.Add(gesamt);
                srd.Title = "Summe";
                grdAnzeige.GetTableDescriptor("tblReport").SummaryRows.Add(srd);

                GridSummaryColumnDescriptor total = SyncfusionHelper.SummaryColumnDescriptor("GESAMT", TypeCode.Double);

                GridSummaryRowDescriptor srdNation = new GridSummaryRowDescriptor();
                srdNation.Appearance.AnyCell.BackColor = CoinbookHelper.ColorSumme;
                srdNation.SummaryColumns.Add(total);
            }
            catch
            {
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgCreatePDF");
            lblAnzeige.Visible = true;
            Application.DoEvents();

            ReportHelper.ReportToPDF(grdAnzeige.TableControl, this, Text);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
            Application.DoEvents();

            this.Cursor = Cursors.Default;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgExcportExcel");
            lblAnzeige.Visible = true;
            Application.DoEvents();

            ReportHelper.ReportToExcel(grdAnzeige, this, Text,enmReporting.Reporting2);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
            Application.DoEvents();

            this.Cursor = Cursors.Default;
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgCreateCSV");
            lblAnzeige.Visible = true;

            ReportHelper.ReportToCSV(dt);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgPrint");
            lblAnzeige.Visible = true;

            ReportHelper.ReportToPrinter(grdAnzeige.TableControl, this);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
        }

        private void grdAnzeige_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.TableCellIdentity.TableCellType == GridTableCellType.RecordPlusMinusCell)
            {
                Record r = e.TableCellIdentity.DisplayElement.ParentRecord as Record;
                if (r != null && r.NestedTables.Count > 0 && r.NestedTables[0].ChildTable.FilteredChildNodeCount == 0)
                {
                    e.Style.CellType = "Static";
                }
            }
        }

        private void bgwCalculate_DoWork(object sender, DoWorkEventArgs e)
        {
            string cmd = String.Empty;

            var reportliste = DatabaseHelper.LiteDatabase.Reporting2(Liste, NationID, ÄraID, GebietID, 
                CoinbookHelper.Settings.Preise, CoinbookHelper.Settings.CurrentFaktor,CoinbookHelper.Settings.Katalognummern);
            dt = ReportHelper.Report2Table(reportliste);

            dtNation = ReportHelper.GetReportNations(reportliste, NationID);              //Nations
            dtÄra = ReportHelper.GetReportÄras(reportliste, NationID, ÄraID);                             //Ära
            dtGebiet = ReportHelper.GetReportGebiete(reportliste, NationID, ÄraID, GebietID);              //Gebiet
        }

        private void bgwCalculate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ReportHelper.SetRelations(grdAnzeige, dtNation, dtÄra, dtGebiet, dt);
            GridformatReport();

            progressBar.WaitingGradientEnabled = false;
            progressBar.Visible = false;

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;

            grdAnzeige.Visible = true;
            if (dt.Rows.Count == 0 && Visible)
                MessageBoxAdv.Show(Localization.GetTranslation("Keys", "noCoins"), "Reporting", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnWork.Enabled = true;

            Cursor = Cursors.Default;
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            btnWork.Enabled = false;

            progressBar.WaitingGradientEnabled = true;
            lblAnzeige.Text = string.Format(Localization.GetTranslation("Keys", "msgLoad"), cboNation.Text);

            progressBar.Visible = true;
            lblAnzeige.Visible = true;

            bgwCalculate.RunWorkerAsync();
        }
    }
}

