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
using System.Collections.Generic;
using Coinbook.Model;
using System.Linq;

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
            LanguageHelper.Localization.UpdateModul(this);

            cboNation.ValueMember = "iD";
            cboNation.DisplayMember = "Bezeichnung";

            cboÄra.ValueMember = "iD";
            cboÄra.DisplayMember = "Bezeichnung";

            cboGebiet.ValueMember = "iD";
            cboGebiet.DisplayMember = "Bezeichnung";

            progressBar.WaitingGradientEnabled = false;
            progressBar.ProgressStyle = ProgressBarStyles.WaitingGradient;
            progressBar.WaitingGradientInterval = 1;
            progressBar.WaitingGradientWidth = 300;
            progressBar.GradientEndColor = Color.Blue;
            progressBar.GradientStartColor = Color.AliceBlue;
            progressBar.Visible = false;
        }

        public new void ShowDialog(IWin32Window window)
        {
            Cursor = Cursors.WaitCursor;

            switch (ReportTyp)
            {
                case enmReportTyp.ReportSammlung:
                    Text = "Reporting " + LanguageHelper.Localization.GetTranslation(Name, "Sammlung");
                    break;

                case enmReportTyp.ReportDoubletten:
                    Text = "Reporting " + LanguageHelper.Localization.GetTranslation(Name, "Doubletten");
                    break;
            }
            cboNation.DataSource = CoinbookHelper.GetNations(LanguageHelper.Localization.GetTranslation("Keys", "allNations"));
            cboNation.SelectedValue = NationID;
            cboNation_SelectedIndexChanged(null, null);

            cboÄra.SelectedValue = ÄraID;
            cboÄra_SelectedIndexChanged(null, null);

            cboGebiet.SelectedValue = GebietID;
            cboGebiet_SelectedIndexChanged(null, null);

            grdAnzeige.Visible = false;

            lblAnzeige.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgLoad"), cboNation.Text);

            Cursor = Cursors.Default;

            base.ShowDialog(window);
        }

        public int NationID { get; set; }
        public int ÄraID { get; set; }
        public int GebietID { get; set; }
        public enmReportTyp ReportTyp { get; set; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            while (bgwCalculate.IsBusy)
            {
                string temp = LanguageHelper.Localization.GetTranslation("Keys", "msgWaitForBackgroundWorker");
                AutoClosingMessageBox.Show(temp, Application.ProductName);
                Thread.Sleep(1000);
            }

            Close();
        }

        private void cboNation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNation.SelectedValue == null)
                cboNation.SelectedIndex = 0;

            NationID = (int)cboNation.SelectedValue;

            cboÄra.DataSource = CoinbookHelper.GetAeras(NationID, LanguageHelper.Localization.GetTranslation("Keys", "allÄras"));
            cboÄra_SelectedIndexChanged(null, null);
        }

        private void cboÄra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ÄraID = (int)cboÄra.SelectedValue;
            cboGebiet.DataSource = CoinbookHelper.GetRegions(ÄraID, LanguageHelper.Localization.GetTranslation("Keys", "allAreas"));
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

                grdAnzeige.GetTableDescriptor("tblReport").Columns["KatNr"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "KatNr");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzz"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Muenzz");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Jahrgang");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Nominal");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Waehrung");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Motiv"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Motiv");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Erhaltung"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Erhaltung");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Anzahl");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Preis"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Preis");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Gesamt");

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

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Farbe"].Width = 0;

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
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgCreatePDF");
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
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgExcportExcel");
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
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgCreateCSV");
            lblAnzeige.Visible = true;

            ReportHelper.ReportToCSV(dt);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgPrint");
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

            if (e.TableCellIdentity.Column != null && e.TableCellIdentity.TableCellType == GridTableCellType.ColumnHeaderCell)
            {
                e.Style.Themed = false;
                e.Style.BackColor = Color.Silver;
            }

            int row = e.TableCellIdentity.RowIndex + 1;

            if (e.TableCellIdentity.Column != null
                    && (e.TableCellIdentity.TableCellType == GridTableCellType.RecordFieldCell
                        || e.TableCellIdentity.TableCellType == GridTableCellType.AlternateRecordFieldCell
                        || e.TableCellIdentity.TableCellType == GridTableCellType.NestedTableCell))
            {
                object farbe = e.TableCellIdentity.DisplayElement.ParentRecord.GetValue("Farbe");

                if (farbe != null)
                {
                    enmColorFlag flag = (enmColorFlag)Enum.Parse(typeof(enmColorFlag), farbe.ToString());

                    if (flag != enmColorFlag.None && (e.TableCellIdentity.Column.Name == "Preis" || e.TableCellIdentity.Column.Name == "Gesamt"))
                        {
                        e.Style.Themed = false;
                        e.Style.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void bgwCalculate_DoWork(object sender, DoWorkEventArgs e)
        {
            string cmd = String.Empty;
            List<Report2> reportliste = new List<Report2>();

            var bestand = DatabaseHelper.LiteDatabase.Reporting2(
                ReportTyp, 
                NationID, 
                ÄraID, 
                GebietID, 
                CoinbookHelper.Settings.Preise,
                CoinbookHelper.Settings.CurrentFaktor,
                CoinbookHelper.Nationen,
                CoinbookHelper.ModulKey);

            switch (ReportTyp)
            {
                case enmReportTyp.ReportSammlung:
                    foreach (var item in bestand)
                    {
                        if (item.S != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "S", ReportTyp));
                        if (item.SP != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "SP", ReportTyp));
                        if (item.SS != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "SS", ReportTyp));
                        if (item.SSP != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "SSP", ReportTyp));
                        if (item.VZ != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "VZ", ReportTyp));
                        if (item.VZP != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "VZP", ReportTyp));
                        if (item.STN != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "STN", ReportTyp));
                        if (item.STH != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "STH", ReportTyp));
                        if (item.PP != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "PP", ReportTyp));
                    }
                    break;

                case enmReportTyp.ReportDoubletten:
                    foreach (var item in bestand)
                    {
                        if (item.DS != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "S", ReportTyp));
                        if (item.DSP != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "SP", ReportTyp));
                        if (item.DSS != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "SS", ReportTyp));
                        if (item.DSSP != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "SSP", ReportTyp));
                        if (item.DVZ != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "VZ", ReportTyp));
                        if (item.DVZP != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "VZP", ReportTyp));
                        if (item.DSTN != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "STN", ReportTyp));
                        if (item.DSTH != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "STH", ReportTyp));
                        if (item.DPP != 0) reportliste.Add(ReportHelper.CoinSplitToErhaltungsgrad(item, "PP", ReportTyp));
                    }
                    break;
            }

            reportliste = reportliste.OrderBy(a => a.KatNr).ThenBy(b => b.Nominal).ThenBy(c => c.Jahrgang).ThenBy(d => d.Muenzz).ToList();

            dt = ReportHelper.Report2Table(reportliste);

            var nations = DatabaseHelper.LiteDatabase.ReadNationen();
            if (NationID != 0)
                nations.RemoveAll(x => x.ID != NationID);

            dtNation = ReportHelper.GetReportNations(reportliste, nations);              //Nations
            dtÄra = ReportHelper.GetReportÄras(reportliste,nations);                     //Ära
            dtGebiet = ReportHelper.GetReportGebiete(reportliste, nations);              //Gebiet
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
                MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "noCoins"), "Reporting", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnWork.Enabled = true;

            Cursor = Cursors.Default;
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            btnWork.Enabled = false;

            progressBar.WaitingGradientEnabled = true;
            lblAnzeige.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgLoad"), cboNation.Text);

            progressBar.Visible = true;
            lblAnzeige.Visible = true;

            bgwCalculate.RunWorkerAsync();
        }
    }
}

