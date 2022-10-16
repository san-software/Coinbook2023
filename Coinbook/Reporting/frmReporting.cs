using Coinbook.Enumerations;
using Coinbook.Helper;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using Coinbook.Model;

namespace Coinbook
{
	partial class frmReporting : Form
	{
		//private Nationen database;
		private DataTable dt;
		private DataTable dtNation;
		private DataTable dtÄra;
		private DataTable dtGebiet;

		string pattern1 = string.Format("##");
		string pattern2 = string.Format("#,##0.00");

		List<Report> reportliste = new List<Report>();

		public frmReporting()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

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
			progressBar.GradientEndColor = Color.Blue;
			progressBar.GradientStartColor = Color.AliceBlue;
			progressBar.TextVisible = false;
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
			lblAnzeige.Visible = false;
			progressBar.Visible = false;

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
				ReportHelper.FormatReportGroups(grdAnzeige, 1127, 1109, 1091);

				grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzz"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Muenzz");
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Jahrgang");
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Nominal");
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Währung");

				grdAnzeige.GetTableDescriptor("tblReport").Columns["NationID"].Width = 0;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["AeraID"].Width = 0;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].Width = 79;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].Width = 40;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].Width = 50;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzz"].Width = 50;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].Width = 28;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Width = 60;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Width = 80;

				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[0].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[1].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[2].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[3].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[4].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[5].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[6].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].HeaderText = CoinbookHelper.Erhaltungsgrade[8].Erhaltung + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";

				grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].HeaderText = CoinbookHelper.Erhaltungsgrade[1].Erhaltung;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].HeaderText = CoinbookHelper.Erhaltungsgrade[3].Erhaltung;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].HeaderText = CoinbookHelper.Erhaltungsgrade[5].Erhaltung;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].HeaderText = CoinbookHelper.Erhaltungsgrade[6].Erhaltung;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "Gesamt") + "\n[" + CoinbookHelper.Settings.CurrentWährung + "]";

				grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorColumns);

				grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;

				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;

				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.Format = "###,##0.00";
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.Format = "###,##0.00";

				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
				grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);

				grdAnzeige.GetTableDescriptor("tblReport").Columns["Farbe"].Width = 0;

				GridSummaryColumnDescriptor s = SyncfusionHelper.SummaryColumnDescriptor("S", TypeCode.Int32);
				GridSummaryColumnDescriptor sp = SyncfusionHelper.SummaryColumnDescriptor("SP", TypeCode.Int32);
				GridSummaryColumnDescriptor ss = SyncfusionHelper.SummaryColumnDescriptor("SS", TypeCode.Int32);
				GridSummaryColumnDescriptor ssp = SyncfusionHelper.SummaryColumnDescriptor("SSP", TypeCode.Int32);
				GridSummaryColumnDescriptor vz = SyncfusionHelper.SummaryColumnDescriptor("VZ", TypeCode.Int32);
				GridSummaryColumnDescriptor vzp = SyncfusionHelper.SummaryColumnDescriptor("VZP", TypeCode.Int32);
				GridSummaryColumnDescriptor stn = SyncfusionHelper.SummaryColumnDescriptor("STN", TypeCode.Int32);
				GridSummaryColumnDescriptor sth = SyncfusionHelper.SummaryColumnDescriptor("STH", TypeCode.Int32);
				GridSummaryColumnDescriptor pp = SyncfusionHelper.SummaryColumnDescriptor("PP", TypeCode.Int32);
				GridSummaryColumnDescriptor gesamt = SyncfusionHelper.SummaryColumnDescriptor("Gesamt", TypeCode.Double);

				GridSummaryRowDescriptor srd = new GridSummaryRowDescriptor();
				srd.Appearance.AnyCell.BackColor = CoinbookHelper.ColorSumme;
				srd.SummaryColumns.Add(s);
				srd.SummaryColumns.Add(sp);
				srd.SummaryColumns.Add(ss);
				srd.SummaryColumns.Add(ssp);
				srd.SummaryColumns.Add(vz);
				srd.SummaryColumns.Add(vzp);
				srd.SummaryColumns.Add(stn);
				srd.SummaryColumns.Add(sth);
				srd.SummaryColumns.Add(pp);
				srd.SummaryColumns.Add(gesamt);
				srd.Title = "Summe";
				grdAnzeige.GetTableDescriptor("tblReport").SummaryRows.Add(srd);

				GridSummaryColumnDescriptor total = SyncfusionHelper.SummaryColumnDescriptor("GESAMT", TypeCode.Double);

				GridSummaryRowDescriptor srdNation = new GridSummaryRowDescriptor();
				srdNation.Appearance.AnyCell.BackColor = CoinbookHelper.ColorSumme;
				srdNation.SummaryColumns.Add(total);
			}
			catch (Exception ex)
			{
				MessageBoxAdv.Show(ex.Message);
			}
		}

		private void btnPDF_Click(object sender, EventArgs e)
		{
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgCreatePDF");
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
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgExcportExcel");
			lblAnzeige.Visible = true;
			Application.DoEvents();

			ReportHelper.ReportToExcel(grdAnzeige, this, Text, enmReporting.Reporting);

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

			ReportHelper.ReportToPrinter(grdAnzeige.TableControl, this, true);

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

			if (e.TableCellIdentity.Column != null 
					&& (e.TableCellIdentity.TableCellType == GridTableCellType.RecordFieldCell
						|| e.TableCellIdentity.TableCellType == GridTableCellType.AlternateRecordFieldCell
						|| e.TableCellIdentity.TableCellType == GridTableCellType.NestedTableCell))
			{
				int row = e.TableCellIdentity.RowIndex + 1;
				int index = row - 3;
				object farbe = e.TableCellIdentity.DisplayElement.ParentRecord.GetValue("Farbe");

				if (farbe != null)
				{
                    enmColorFlag flag = (enmColorFlag)Enum.Parse(typeof(enmColorFlag), farbe.ToString());

					if (row > 2 && index < reportliste.Count)
					{
						var col = e.TableCellIdentity.Column.Name;
						bool color = false;

						if (col == "SPreis") color = (flag & enmColorFlag.S) == enmColorFlag.S ? true : false;
						if (col == "SPPreis") color = (flag & enmColorFlag.SP) == enmColorFlag.SP ? true : false;
						if (col == "SSPreis") color = (flag & enmColorFlag.SS) == enmColorFlag.SS ? true : false;
						if (col == "SPPreis") color = (flag & enmColorFlag.SSP) == enmColorFlag.SSP ? true : false;
						if (col == "VZPreis") color = (flag & enmColorFlag.VZ) == enmColorFlag.VZ ? true : false;
						if (col == "VZPPreis") color = (flag & enmColorFlag.VZP) == enmColorFlag.VZP ? true : false;
						if (col == "STNPreis") color = (flag & enmColorFlag.STN) == enmColorFlag.STN ? true : false;
						if (col == "STHPreis") color = (flag & enmColorFlag.STH) == enmColorFlag.STH ? true : false;
						if (col == "PPPreis") color = (flag & enmColorFlag.PP) == enmColorFlag.PP ? true : false;
						if (col == "Gesamt") color = flag != enmColorFlag.None;

						if (color)
						{
							e.Style.Themed = false;
							e.Style.BackColor = Color.Yellow;
						}
					}
				}
			}
		}

		private void bgwCalculate_DoWork(object sender, DoWorkEventArgs e)
		{
			reportliste = DatabaseHelper.LiteDatabase.Reporting(
				ReportTyp, 
				NationID, 
				ÄraID, 
				GebietID,
				CoinbookHelper.Settings.CurrentFaktor,
				CoinbookHelper.Nationen,
				CoinbookHelper.Settings.Preise,
				CoinbookHelper.ModulKey);

			dt = ReportHelper.ReportTable(reportliste);

			var nations = DatabaseHelper.LiteDatabase.ReadNationen();
			if (NationID != 0)
				nations.RemoveAll(x => x.ID != NationID);

			dtNation = ReportHelper.GetReportNations(reportliste,nations);              //Nations
			dtÄra = ReportHelper.GetReportÄras(reportliste, nations);
			dtGebiet = ReportHelper.GetReportGebiete(reportliste,nations);              //Gebiet
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

		private void btnSerialize_Click(object sender, EventArgs e)
		{
			FileDialog dlg = new SaveFileDialog();
			dlg.AddExtension = true;
			dlg.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string path = Path.GetDirectoryName(dlg.FileName);

				// Create xml writer for adding the info to the xml file.
				XmlTextWriter xmlWriter = new XmlTextWriter(Path.Combine(path, dlg.FileName), System.Text.Encoding.UTF8);
				xmlWriter.Formatting = System.Xml.Formatting.Indented;

				// Write Grid schema to the xml file.
				grdAnzeige.WriteXmlSchema(xmlWriter);
				xmlWriter.Close();

				dt.WriteXml(Path.Combine(path,"Report.xml"), XmlWriteMode.WriteSchema);
				dtNation.WriteXml(Path.Combine(path, "Nation.xml"), XmlWriteMode.WriteSchema);
				dtÄra.WriteXml(Path.Combine(path, "Aera.xml"), XmlWriteMode.WriteSchema);
				dtGebiet.WriteXml(Path.Combine(path, "Gebiet.xml"), XmlWriteMode.WriteSchema);
			}
		}
	}
}

