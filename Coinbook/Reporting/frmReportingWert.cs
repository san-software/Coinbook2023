using Coinbook.Enumerations;
using Coinbook.Helper;
using Coinbook.Model;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Coinbook
{
    partial class frmReportingWert : Form
    {
        private List<Wertermittlung> wertermittlung;
        private List<KeyValuePair<int, string>> nationen = new List<KeyValuePair<int, string>>();

        public frmReportingWert()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);

            progressBar.WaitingGradientEnabled = true;
            progressBar.ProgressStyle = ProgressBarStyles.WaitingGradient;
            progressBar.WaitingGradientInterval = 1;
            progressBar.WaitingGradientWidth = 300;
            progressBar.GradientEndColor = Color.Blue;
            progressBar.GradientStartColor = Color.AliceBlue;
            progressBar.TextVisible = false;
            progressBar.Visible = false;

            grdAnzeige.TableModel.RowHeights.ResizeToFit(GridRangeInfo.Row(0), GridResizeToFitOptions.ResizeCoveredCells);
            lblAnzeige.Text = String.Empty;
            labelEx1.Text = String.Empty;
        }

        public new void ShowDialog(IWin32Window owner)
        {
            Cursor = Cursors.WaitCursor;

            switch (Liste)
            {
                case enmReportTyp.WerteSammlung:
                    Text = LanguageHelper.Localization.GetTranslation(Name, "Wert") + " " + LanguageHelper.Localization.GetTranslation(Name, "Sammlung");
                    break;
                case enmReportTyp.WerteDoubletten:
                    Text = LanguageHelper.Localization.GetTranslation(Name, "Wert") + " " + LanguageHelper.Localization.GetTranslation(Name, "Doubletten");
                    break;
                case enmReportTyp.KostenSammlung:
                    Text = LanguageHelper.Localization.GetTranslation(Name, "Kosten") + " " + LanguageHelper.Localization.GetTranslation(Name, "Sammlung");
                    break;
                case enmReportTyp.KostenDoubletten:
                    Text = LanguageHelper.Localization.GetTranslation(Name, "Kosten") + " " + LanguageHelper.Localization.GetTranslation(Name, "Doubletten");
                    break;
            }

            cboNationen.DisplayMember = "Bezeichnung";
            cboNationen.ValueMember = "ID";
            cboNationen.DataSource = CoinbookHelper.GetNations(LanguageHelper.Localization.GetTranslation("Keys", "allNations")); 
            cboNationen.SelectedIndex = 0;
			
			btnWork.Enabled = true;
            Cursor = Cursors.Default;

            base.ShowDialog(owner);
        }

        public int NationID { get; set; }
        public enmReportTyp Liste { get; set; }

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

        private void btnPDF_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgCreatePDF");
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
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgExcportExcel");
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
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgCreateCSV");
            lblAnzeige.Visible = true;
            progressBar.Visible = true;

            ReportHelper.ReportToCSV(wertermittlung);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
            progressBar.Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgPrint");
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
                aeras = DatabaseHelper.LiteDatabase.ReadAeras(CoinbookHelper.ModulKey, NationID);

            wertermittlung = DatabaseHelper.LiteDatabase.ReportingWert(
                Liste, NationID, 
                CoinbookHelper.Settings.CurrentFaktor, 
                CoinbookHelper.Settings.Preise, 
                CoinbookHelper.Nationen, 
                aeras, 
                CoinbookHelper.ModulKey);
        }

        private void bgwCalculate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            wertermittlung[wertermittlung.Count - 1].Nation = LanguageHelper.Localization.GetTranslation(Name, "Gesamt");

            List<Aera> aeras = new List<Aera>();
            List<KeyValuePair<int, string>> nation = new List<KeyValuePair<int, string>>();

            if (NationID != 0)
            {
                foreach (Nation item in cboNationen.Items)
                {
                    Aera a = new Aera();
                    a.ID = item.ID;
                    a.Bezeichnung = item.Bezeichnung;
                    aeras.Add(a);
                }
            }

            KeyValuePair<int, string> n = new KeyValuePair<int, string>(
                ((Nation)cboNationen.SelectedItem).ID,
                ((Nation)cboNationen.SelectedItem).Bezeichnung);
            nation.Add(n);

            ReportHelper.SetRelationsWert(grdAnzeige, nation, wertermittlung);
            GridformatReport();

            cboNationen.Enabled = true;
            progressBar.Visible = false;
            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;

            Cursor = Cursors.Default;
			btnWork.Enabled = true;
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

                //grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorHeader);

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

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nation"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Beschreibung");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].HeaderText = CoinbookHelper.Erhaltungsgrade[0].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].HeaderText = CoinbookHelper.Erhaltungsgrade[1].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].HeaderText = CoinbookHelper.Erhaltungsgrade[2].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].HeaderText = CoinbookHelper.Erhaltungsgrade[3].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].HeaderText = CoinbookHelper.Erhaltungsgrade[4].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].HeaderText = CoinbookHelper.Erhaltungsgrade[5].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].HeaderText = CoinbookHelper.Erhaltungsgrade[6].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].HeaderText = CoinbookHelper.Erhaltungsgrade[8].Erhaltung;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Anzahl"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Anzahl");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Gesamt") + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";

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
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Farbe"].Width = 0;
            }
            catch (Exception ex)
            {
                var x = ex;
            }
        }

        private void grdAnzeige_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            // Applies style for a particular Row.
            if (wertermittlung != null)
            {
                if (e.TableCellIdentity.RowIndex == wertermittlung.Count + 1)
                {
                    e.Style.BackColor = Color.Silver;
                    e.Style.Font.Bold = true;
                }

                if (e.TableCellIdentity.Column != null && e.TableCellIdentity.TableCellType == GridTableCellType.ColumnHeaderCell)
                {
                    e.Style.Themed = false;
                    e.Style.BackColor = Color.Silver;
                }

                if (e.TableCellIdentity.Column != null && e.TableCellIdentity.Column.Name == "Gesamt")
                {
                    int row = e.TableCellIdentity.RowIndex + 1;
                    if (row > 2 && row - 3 < wertermittlung.Count)
                    {
                        Console.WriteLine(row - 3 + " - " + wertermittlung[row - 3].Farbe);

                        if (wertermittlung[row - 3].Farbe != enmColorFlag.None)
                        {
                            e.Style.Themed = false;
                            e.Style.BackColor = Color.Yellow;
                        }
                    }
                }
            }
        }

        private void Ausführen_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
			btnWork.Enabled = false;

            progressBar.WaitingGradientEnabled = true;
            lblAnzeige.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgLoad"), cboNationen.Text);

            progressBar.Visible = true;
            lblAnzeige.Visible = true;
            cboNationen.Enabled = false;
                
            NationID = (int)cboNationen.SelectedValue;
            CoinbookHelper.ModulKey = ((Nation)cboNationen.SelectedItem).Key;

            grdAnzeige.DataSource = null;

            bgwCalculate.RunWorkerAsync();
        }
    }
}

