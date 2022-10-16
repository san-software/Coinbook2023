using System;
using System.Data;
using System.Windows.Forms;
using Coinbook.Lokalisierung;
using Coinbook.Model;
using System.Collections.Generic;

using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Tools;
using System.ComponentModel;
using System.Linq;
using Syncfusion.Windows.Forms.Tools.Design;
using System.Threading;
using System.Globalization;
using Coinbook.Helper;

namespace Coinbook
{
    partial class frmFehllisten : Form
    {
        List<Fehlliste> fehlliste;
        List<Nation> nations = new List<Nation>();
        List<Aera> aeras = new List<Aera>();
        List<Gebiet> gebiete = new List<Gebiet>();

        public frmFehllisten()
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

            cboNation.DataSource = CoinbookHelper.Nationen;
            cboNation.SelectedValue = NationID;

            cboÄra.DataSource = CoinbookHelper.GetAeras(NationID);
            cboÄra.SelectedValue = ÄraID;

            cboÄra.DataSource = CoinbookHelper.GetRegions(ÄraID);
            cboGebiet.SelectedValue = GebietID;

            cboNation.DataSource = CoinbookHelper.GetNations();
            cboNation.SelectedValue = NationID;
            cboNation_SelectedIndexChanged(null, null);

            cboÄra.SelectedValue = ÄraID;
            cboÄra_SelectedIndexChanged(null, null);

            cboGebiet.SelectedValue = GebietID;
            cboGebiet_SelectedIndexChanged(null, null);

            grdAnzeige.Visible = false;
            btnWork.Enabled = false;

            lblAnzeige.Text =  string.Format(Localization.GetTranslation("Keys","msgLoad"), cboNation.Text);

            Text = string.Format("{0} {1} - {2} - {3}", Localization.GetTranslation("Keys", "Fehllisten"), ((Nation)cboNation.SelectedItem).Bezeichnung,
                ((Aera)cboÄra.SelectedItem).Bezeichnung, ((Gebiet)cboGebiet.SelectedItem).Bezeichnung);

            xxx();
            bgwCalculate.RunWorkerAsync();

            base.ShowDialog(window);
        }

        public int NationID { get; set; }
        public int ÄraID { get; set; }
        public int GebietID { get; set; }

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

        private void GridformatReport(int gebietCount)
        {
            try
            {
                grdAnzeige.ChildGroupOptions.ShowAddNewRecordBeforeDetails = false;
                grdAnzeige.ChildGroupOptions.ShowAddNewRecordAfterDetails = false;

                grdAnzeige.Table.ExpandAllGroups();
                grdAnzeige.Table.ExpandAllRecords();
                grdAnzeige.TopLevelGroupOptions.ShowColumnHeaders = false;

                grdAnzeige.TableDescriptor.Columns["Bestellnummer"].Width = 0;
                grdAnzeige.TableDescriptor.Columns["InUse"].Width = 0;
                grdAnzeige.TableDescriptor.Columns["Key"].Width = 0;
                grdAnzeige.TableDescriptor.Columns["Mapping"].Width = 0;
                grdAnzeige.TableDescriptor.Columns["ID"].Width = 0;
                grdAnzeige.TableDescriptor.Columns["Bezeichnung"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorNation);
                grdAnzeige.TableDescriptor.Columns["Bezeichnung"].Appearance.AnyCell.Font.Bold = true;
                grdAnzeige.TableDescriptor.Columns["Bezeichnung"].Appearance.AnyCell.Font.Size = 10;
                grdAnzeige.TableDescriptor.Columns["Bezeichnung"].Width = 1134;

                grdAnzeige.GetTable("tblAera").DefaultCaptionRowHeight = 0;
                grdAnzeige.GetTable("tblAera").DefaultColumnHeaderRowHeight = 0;
                grdAnzeige.GetTableDescriptor("tblAera").Columns["Bezeichnung"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorÄra);
                grdAnzeige.GetTableDescriptor("tblAera").Columns["Bezeichnung"].Appearance.AnyCell.Font.Bold = true;
                grdAnzeige.GetTableDescriptor("tblAera").Columns["Bezeichnung"].Appearance.AnyCell.Font.Size = 10;
                grdAnzeige.GetTableDescriptor("tblAera").Columns["Bezeichnung"].Width = 1116;
                grdAnzeige.GetTableDescriptor("tblAera").Columns["ID"].Width = 0;
                grdAnzeige.GetTableDescriptor("tblAera").Columns["Sortierung"].Width = 0;

                grdAnzeige.GetTable("tblGebiet").DefaultCaptionRowHeight = 0;
                grdAnzeige.GetTable("tblGebiet").DefaultColumnHeaderRowHeight = 0;
                grdAnzeige.GetTableDescriptor("tblGebiet").Columns["ID"].Width = 0;
                grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Sortierung"].Width = 0;
                grdAnzeige.GetTableDescriptor("tblGebiet").Columns["NationID"].Width = 0;
                grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorGebiet);
                grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].Appearance.AnyCell.Font.Bold = true;
                grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].Appearance.AnyCell.Font.Size = 10;
                grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].Width = 1098;

                grdAnzeige.GetTable("tblReport").DefaultCaptionRowHeight = 0;
                grdAnzeige.GetTable("tblReport").DefaultColumnHeaderRowHeight = 35;
                grdAnzeige.GetTableDescriptor("tblReport").Appearance.AnyHeaderCell.Font.Bold = true;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["NationID"].Width = 0;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["AeraID"].Width = 0;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["KatNr"].HeaderText = Localization.GetTranslation(Name, "KatNr");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Motiv"].HeaderText = Localization.GetTranslation(Name, "Motiv");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzzeichen"].HeaderText = Localization.GetTranslation(Name, "Muenzz");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].HeaderText = Localization.GetTranslation(Name, "Jahrgang");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].HeaderText = Localization.GetTranslation(Name, "Nominal");
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].HeaderText = Localization.GetTranslation(Name, "Währung");

                grdAnzeige.GetTableDescriptor("tblReport").Columns["KatNr"].Width = 65;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Motiv"].Width = 135;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].Width = 100;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].Width = 50;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].Width = 85;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzzeichen"].Width = 60;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Width = 67;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Width = 67;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Width = 67;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Width = 67;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Width = 67;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Width = 67;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Width = 67;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Width = 67;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Width = 67;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[0].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[1].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[2].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[3].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[4].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[5].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[6].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[8].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";

                grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;

                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Appearance.AnyCell.Format = "###,##0.00";
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.Format = "###,##0.00";

                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
                grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show(ex.Message);
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgCreatePDF");
            lblAnzeige.Visible = true;
            Application.DoEvents();

            ReportHelper.ReportToPDF(grdAnzeige.TableControl, this, Text, true);

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

            ReportHelper.ReportToExcel(grdAnzeige, this, Text, enmReporting.Fehllisten);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
            Application.DoEvents();

            this.Cursor = Cursors.Default;
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgCreateCSV");
            lblAnzeige.Visible = true;

            ReportHelper.ReportToCSV(fehlliste);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            lblAnzeige.Text = Localization.GetTranslation("Keys", "msgPrint");
            lblAnzeige.Visible = true;

            ReportHelper.ReportToPrinter(grdAnzeige.TableControl, this, true);

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;
        }

        private void bgwCalculate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            fehlliste = DatabaseHelper.LiteDatabase.Fehllisten(NationID, ÄraID, GebietID, CoinbookHelper.Settings.CurrentFaktor, CoinbookHelper.Settings.Preise);
            fehlliste = fehlliste.OrderBy(a => a.KatNr).ThenBy(b => b.Nominal).ThenBy(c => c.Jahrgang).ThenBy(d => d.Muenzzeichen).ToList();
        }

        private void bgwCalculate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Text = string.Format("{0} {1} - {2} - {3}", Localization.GetTranslation("Keys", "Fehllisten"), 
                ((Model.Nation)cboNation.SelectedItem).Bezeichnung,
                ((Model.Aera)cboÄra.SelectedItem).Bezeichnung, 
                ((Model.Gebiet)cboGebiet.SelectedItem).Bezeichnung);

            ReportHelper.SetRelationsFehlliste(grdAnzeige, nations, aeras, gebiete, fehlliste);
            GridformatReport(gebiete.Count);

            progressBar.WaitingGradientEnabled = false;
            progressBar.Visible = false;

            lblAnzeige.Text = string.Empty;
            lblAnzeige.Visible = false;

            grdAnzeige.Visible = true;
            if (fehlliste.Count == 0 && Visible)
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

            xxx();
            bgwCalculate.RunWorkerAsync();
        }
 
        private void cboGebiet_SelectedIndexChanged(object sender, EventArgs e)
        {
            GebietID = (int)cboGebiet.SelectedValue;
        }

        private void xxx()
        {
            nations.Clear();
            nations.Add((Nation)cboNation.SelectedItem);

            aeras.Clear();
            if (ÄraID == 0)
                aeras = CoinbookHelper.Aeras;
            else
                aeras.Add((Aera)cboÄra.SelectedItem);

            //Gebiete
            if (ÄraID == 0)
            {
                gebiete = CoinbookHelper.Regions.Where(s => s.NationID == NationID).ToList();
                gebiete = gebiete.OrderBy(y => y.AeraID).ThenBy(x => x.Sortierung).ToList();
            }
            else if (GebietID == 0)
                gebiete = CoinbookHelper.GetRegions(ÄraID);
            else
            {
                gebiete.Clear();
                gebiete.Add((Gebiet)cboGebiet.SelectedItem);
            }

        }
    }
}

