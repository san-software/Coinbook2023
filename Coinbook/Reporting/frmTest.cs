using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SAN.Converter;
using System.IO;
using SAN.UI.DataGridView;
using Office = NetOffice.OfficeApi;
using Excel = NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;
using System.Globalization;
using System.Diagnostics;

using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using System.Text.RegularExpressions;
using Syncfusion.GroupingGridExcelConverter;
using Syncfusion.DocIO;
using Syncfusion.GridHelperClasses;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.XlsIO;
using Syncfusion.Pdf.Parsing;
using Coinbook.Enumerations;
using Coinbook.Model;
using Coinbook.Helper;

namespace Coinbook
{
	partial class frmTest : Form
	{
		string pattern1 = string.Format("##");
		string pattern2 = string.Format("#,##0.00");

		private List<Wertermittlung> wertermittlung = new List<Wertermittlung>();
		private List<KeyValuePair<int, string>> nationen = new List<KeyValuePair<int, string>>();

		public frmTest()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

			cboNationen.ValueMember = "iD";
			cboNationen.DisplayMember = "Bezeichnung";
		}

		public new void ShowDialog(IWin32Window window)
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

			var nations = CoinbookHelper.GetNations(LanguageHelper.Localization.GetTranslation("Keys", "allNations"));

			cboNationen.DisplayMember = "Value";
			cboNationen.ValueMember = "Key";
			cboNationen.DataSource = nations;
			cboNationen.SelectedIndex = 0;

			foreach (var item in nations)
				nationen.Add(new KeyValuePair<int, string>(item.ID, item.Bezeichnung));

			SetRelations(grdAnzeige, nationen, wertermittlung);

			bgwCalculate.RunWorkerAsync(((Nation)cboNationen.SelectedItem).ID);

			base.ShowDialog(window);
		}

		public int NationID { get; set; }
		
		public enmReportTyp Liste { get; set; }

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cboNation_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboNationen.SelectedValue == null)
				cboNationen.SelectedIndex = 0;
		}

		//private void reportTable(int nationID)
		//{
		//	string cmd = String.Empty;

		//	var reportliste = Helper.LiteDatabase.Reporting(Liste, nationID, äraID, gebietID, Helper.Settings.Preise, Helper.Settings.CurrentFaktor);
		//	dt = Helper.ReportTable(reportliste);

		//	//dtNation = Helper.GetReportNations(reportliste, (int)cboNationen.SelectedValue);              //Nations

		//	SetRelations(grdAnzeige, dt);
		//	GridformatReport();

		//	if (dt.Rows.Count == 0 && Visible)
		//		MessageBoxAdv.Show(LanguageHelper.Localization("Keys", "noCoins"), "Reporting", MessageBoxButtons.OK, MessageBoxIcon.Information);

		//	Cursor = Cursors.Default;
		//}

		private void GridformatReport()
		{
			//try
			//{
			//	ReportHelper.FormatReportGroups(grdAnzeige, 1127, 1109, 1091);

			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzz"].HeaderText = LanguageHelper.Localization(Name, "Muenzz");
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].HeaderText = LanguageHelper.Localization(Name, "Jahrgang");
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].HeaderText = LanguageHelper.Localization(Name, "Nominal");
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].HeaderText = LanguageHelper.Localization(Name, "Währung");

			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["NationID"].Width = 0;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["AeraID"].Width = 0;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Waehrung"].Width = 79;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].Width = 40;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].Width = 50;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Muenzz"].Width = 50;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].Width = 28;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Width = 60;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Width = 80;

			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].HeaderText = Helper.Erhaltungsgrade[0].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].HeaderText = Helper.Erhaltungsgrade[1].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].HeaderText = Helper.Erhaltungsgrade[2].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].HeaderText = Helper.Erhaltungsgrade[3].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].HeaderText = Helper.Erhaltungsgrade[4].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].HeaderText = Helper.Erhaltungsgrade[5].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].HeaderText = Helper.Erhaltungsgrade[6].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].HeaderText = Helper.Erhaltungsgrade[7].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].HeaderText = Helper.Erhaltungsgrade[8].Bezeichnung + "\n[" + Helper.Settings.CurrentWährung + "]";

			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].HeaderText = Helper.Erhaltungsgrade[1].Erhaltung;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].HeaderText = Helper.Erhaltungsgrade[3].Erhaltung;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].HeaderText = Helper.Erhaltungsgrade[5].Erhaltung;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].HeaderText = Helper.Erhaltungsgrade[6].Erhaltung;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].HeaderText = Helper.Erhaltungsgrade[7].Erhaltung;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].HeaderText = LanguageHelper.Localization(Name, "Gesamt") + "\n[" + Helper.Settings.CurrentWährung + "]";

			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(Helper.ColorColumns);

			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Nominal"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Jahrgang"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["S"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SS"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZ"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STN"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STH"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["PP"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Center;

			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["SSPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["VZPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STNPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["STHPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["PPPreis"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
			//	grdAnzeige.GetTableDescriptor("tblReport").Columns["Gesamt"].Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;

			//	GridSummaryColumnDescriptor s = Helper.SummaryColumnDescriptor("S", TypeCode.Int32);
			//	GridSummaryColumnDescriptor sp = Helper.SummaryColumnDescriptor("SP", TypeCode.Int32);
			//	GridSummaryColumnDescriptor ss = Helper.SummaryColumnDescriptor("SS", TypeCode.Int32);
			//	GridSummaryColumnDescriptor ssp = Helper.SummaryColumnDescriptor("SSP", TypeCode.Int32);
			//	GridSummaryColumnDescriptor vz = Helper.SummaryColumnDescriptor("VZ", TypeCode.Int32);
			//	GridSummaryColumnDescriptor vzp = Helper.SummaryColumnDescriptor("VZP", TypeCode.Int32);
			//	GridSummaryColumnDescriptor stn = Helper.SummaryColumnDescriptor("STN", TypeCode.Int32);
			//	GridSummaryColumnDescriptor sth = Helper.SummaryColumnDescriptor("STH", TypeCode.Int32);
			//	GridSummaryColumnDescriptor pp = Helper.SummaryColumnDescriptor("PP", TypeCode.Int32);
			//	GridSummaryColumnDescriptor gesamt = Helper.SummaryColumnDescriptor("Gesamt", TypeCode.Double);

			//	GridSummaryRowDescriptor srd = new GridSummaryRowDescriptor();
			//	srd.Appearance.AnyCell.BackColor = Helper.ColorSumme;
			//	srd.SummaryColumns.Add(s);
			//	srd.SummaryColumns.Add(sp);
			//	srd.SummaryColumns.Add(ss);
			//	srd.SummaryColumns.Add(ssp);
			//	srd.SummaryColumns.Add(vz);
			//	srd.SummaryColumns.Add(vzp);
			//	srd.SummaryColumns.Add(stn);
			//	srd.SummaryColumns.Add(sth);
			//	srd.SummaryColumns.Add(pp);
			//	srd.SummaryColumns.Add(gesamt);
			//	srd.Title = "Summe";
			//	grdAnzeige.GetTableDescriptor("tblReport").SummaryRows.Add(srd);

			//	GridSummaryColumnDescriptor total = Helper.SummaryColumnDescriptor("GESAMT", TypeCode.Double);

			//	GridSummaryRowDescriptor srdNation = new GridSummaryRowDescriptor();
			//	srdNation.Appearance.AnyCell.BackColor = Helper.ColorSumme;
			//	srdNation.SummaryColumns.Add(total);
			//}
			//catch (Exception ex)
			//{
			//	MessageBoxAdv.Show(ex.Message);
			//}
		}

		private void btnPDF_Click(object sender, EventArgs e)
		{
			ReportHelper.ReportToPDF(grdAnzeige.TableControl, this, Text);		
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			//ReportHelper.ReportToExcel(grdAnzeige);
		}

		private void btnCSV_Click(object sender, EventArgs e)
		{
			ReportHelper.ReportToCSV(wertermittlung);
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			ReportHelper.ReportToPrinter(grdAnzeige.TableControl, this, true);
		}

		private void frmReporting_Shown(object sender, EventArgs e)
		{
			//if (dt.Rows.Count == 0)
			//	MessageBoxAdv.Show(LanguageHelper.Localization("Keys", "noCoins"), "Reporting", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		public void SetRelations(GridGroupingControl grdAnzeige, List<KeyValuePair<int, string>> nationen, List<Wertermittlung> wertermittlung)
		{
			grdAnzeige.DataSource = wertermittlung;

			GridRelationDescriptor parentToChildRelationDescriptor = new GridRelationDescriptor();
			parentToChildRelationDescriptor.ChildTableName = "tblReport";    // same as SourceListSetEntry.Name for childTable (see below)
			parentToChildRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
			parentToChildRelationDescriptor.RelationKeys.Add("NationID", "NationID");

			// Add relation to ParentTable 
			grdAnzeige.TableDescriptor.Relations.Clear();
			grdAnzeige.TableDescriptor.Relations.Add(parentToChildRelationDescriptor);

			// Register any DataTable/IList with SourceListSet, so that RelationDescriptor can resolve the name
			grdAnzeige.Engine.SourceListSet.Clear();
			grdAnzeige.Engine.SourceListSet.Add("tblNation", nationen);
			grdAnzeige.Engine.SourceListSet.Add("tblReport", wertermittlung);

			//Addin DataSource to the gridgroupingcontrol.
			grdAnzeige.DataSource = nationen;
		}

		private void bgwCalculate_DoWork(object sender, DoWorkEventArgs e)
		{
			List<Aera> aeras = null;

			if ((int)e.Argument != 0)
				aeras = CoinbookHelper.GetAeras((int)e.Argument);

			wertermittlung = DatabaseHelper.LiteDatabase.ReportingWert(Liste, (int)e.Argument, CoinbookHelper.Settings.CurrentFaktor, CoinbookHelper.Settings.Preise, CoinbookHelper.Nationen, aeras);
		}

		private void bgwCalculate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

		}
	}
}

