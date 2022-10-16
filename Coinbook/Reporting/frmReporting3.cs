using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.GroupingGridExcelConverter;
using Syncfusion.GridHelperClasses;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.XlsIO;
using Coinbook.Model;
using Coinbook.Enumerations;

namespace Coinbook
{
    partial class frmReporting : Form
    {
        string pattern1 = string.Format("##");
        string pattern2 = string.Format("#,##0.00");

        private Color colorNation = Color.IndianRed;
        private Color colorÄra = Color.AliceBlue;
        private Color colorGebiet = Color.LightGreen;
        private Color colorColumns = Color.Silver;
        private Color colorSumme = Color.Gold;

        private DataTable dtReport;
        private DataTable dtNations;
        private DataTable dtÄras;
        private DataTable dtGebiete;

        List<KeyValuePair<int, string>> nationen = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> aeras = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> gebiete = new List<KeyValuePair<int, string>>();

        public frmReporting()
        {
            InitializeComponent();
            Localization.UpdateModul(this);
        }

        public new void ShowDialog()
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


            nationen.Add(new KeyValuePair<int, string>(0, "(alle Nationen)"));
            foreach (Model.Nation item in Helper.Nationen)
                nationen.Add(new KeyValuePair<int, string>(item.ID, item.Bezeichnung));

            cboNationen.DisplayMember = "Value";
            cboNationen.ValueMember = "Key";
            cboNationen.Sorted = true;
            cboNationen.DataSource = nationen;
            cboNationen.SelectedIndex = 0;

            cboÄra.DisplayMember = "Value";
            cboÄra.ValueMember = "Key";
            aeras.Add(new KeyValuePair<int, string>(0, "(alle Äras)"));
            cboÄra.DataSource = aeras;
            cboÄra.SelectedIndex = 0;

            cboGebiete.DisplayMember = "Value";
            cboGebiete.ValueMember = "Key";
            gebiete.Add(new KeyValuePair<int, string>(0, "(alle Gebiete)"));
            cboGebiete.DataSource = gebiete;
            cboGebiete.SelectedIndex = 0;

            NationID = 0;
            ÄraID = 0;
            GebietID = 0;

            reportTable(NationID, ÄraID, GebietID);

            initReport();
            showReport();

            base.ShowDialog();
        }

        public int NationID { get; set; }
        public int ÄraID { get; set; }
        public int GebietID { get; set; }

        public enmReportTyp Liste { get; set; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboNation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNationen.SelectedValue != null)
            {
                int nation = (int)cboNationen.SelectedValue;
                //cboÄra.DisplayMember = "Bezeichnung";
                //cboÄra.ValueMember = "ID";

                aeras.Clear();
                aeras.Add(new KeyValuePair<int, string>(0, "(alle Äras)"));
                List<Model.Aera> temp = Helper.GetAeras(nation);
                foreach (Model.Aera item in temp)
                    aeras.Add(new KeyValuePair<int, string>(item.ID, item.Bezeichnung));
                cboÄra.DataSource = aeras;
                cboÄra.SelectedIndex = 0;
                cboÄra_SelectedIndexChanged(null, null);
            }
        }

        private void cboÄra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboÄra.SelectedValue != null)
            {
                int gebiet = (int)cboÄra.SelectedValue;
                //cboÄra.DisplayMember = "Bezeichnung";
                //cboÄra.ValueMember = "ID";

                gebiete.Clear();
                gebiete.Add(new KeyValuePair<int, string>(0, "(alle Gebiete)"));
                List<Model.Gebiet> temp = Helper.GetRegions(gebiet);
                foreach (Model.Gebiet item in temp)
                    gebiete.Add(new KeyValuePair<int, string>(item.ID, item.Bezeichnung));
                cboGebiete.DataSource = gebiete;
                cboGebiete.SelectedIndex = 0;
                cboGebiet_SelectedIndexChanged(null, null);
            }
        }

        private void cboGebiet_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            int nationID = (int)cboNationen.SelectedValue;
            int aeraID = (int)cboÄra.SelectedValue;
            int gebietID = (int)cboGebiete.SelectedValue;

            reportTable(nationID, aeraID, gebietID);
            showReport();
        }

        private void reportTable(int nationID, int äraID, int gebietID)
        {
            dtNations = Database.Database.Instance.GetReportingNations(nationID);              //Nations
            dtÄras = Database.Database.Instance.GetReportingAeras(nationID, äraID);        //Ära
            dtGebiete = Database.Database.Instance.GetReportingGebiete(nationID, äraID, gebietID);              //Gebiet
            dtReport = Database.Database.Instance.Reporting(Liste, nationID, äraID, gebietID, Settings.Preise, Settings.CurrentFaktor);

            dtNations.TableName = "tblNation";

            //setRelations();
            //GridformatReport();
            //testTabelle();

            if (dtReport.Rows.Count == 0 && Visible)
                MessageBox.Show(Localization.GetTranslation("Keys", "noCoins"), "Reporting", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Cursor = Cursors.Default;
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            dlgSave.Filter = "PDF-Dokumente|*.pdf";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".pdf"; // Default file extension
            dlgSave.Title = "Speichern als PDF-Datei";
            dlgSave.FileName = "Untitled";

            if (dlgSave.ShowDialog() != DialogResult.Cancel)
            {
                GridPDFConverter pdfConvertor = new GridPDFConverter();
                PdfDocument pdfdoc = new PdfDocument();
                PdfLayoutFormat format = new PdfLayoutFormat();

                pdfConvertor.Exporting += new GridPDFConverter.PDFExportingEventHandler(pdfConvertor_Exporting);
                pdfConvertor.DrawPDFHeader += new GridPDFConverter.DrawPDFHeaderFooterEventHandler(pdfConvertor_DrawPDFHeader);

                format.Layout = PdfLayoutType.OnePage;
                pdfdoc.PageSettings.Orientation = PdfPageOrientation.Landscape;

                pdfConvertor.ExportToPdf(dlgSave.FileName, grdAnzeige.TableControl);

                //Launching the PDF file using the default Application.[Acrobat Reader]
                System.Diagnostics.Process.Start(dlgSave.FileName);

                pdfConvertor.Exporting -= new GridPDFConverter.PDFExportingEventHandler(pdfConvertor_Exporting);
                pdfConvertor.DrawPDFHeader -= new GridPDFConverter.DrawPDFHeaderFooterEventHandler(pdfConvertor_DrawPDFHeader);
            }
        }

        void pdfConvertor_Exporting(object sender, PDFExportingEventArgs e)
        {
            e.PdfDocument.PageSettings.Width = 870;
        }

        //create the pdf header
        void pdfConvertor_DrawPDFHeader(object sender, PDFHeaderFooterEventArgs e)
        {
            PdfPageTemplateElement header = e.HeaderFooterTemplate;

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
            float doubleHeight = font.Height * 2;
            Color activeColor = Color.FromArgb(44, 71, 120);
            SizeF imageSize = new SizeF(110f, 35f);

            PdfSolidBrush brush = new PdfSolidBrush(activeColor);

            PdfPen pen = new PdfPen(Color.DarkBlue, 3f);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);

            //Set formattings for the text
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;

            //Draw title
            header.Graphics.DrawString(Text, font, brush, new RectangleF(0, 0, header.Width, header.Height), format);
            brush = new PdfSolidBrush(Color.Gray);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

            format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Bottom;

            //Draw some lines in the header
            pen = new PdfPen(Color.DarkBlue, 0.7f);
            header.Graphics.DrawLine(pen, 0, 0, header.Width, 0);
            pen = new PdfPen(Color.DarkBlue, 2f);
            header.Graphics.DrawLine(pen, 0, 03, header.Width + 3, 03);
            pen = new PdfPen(Color.DarkBlue, 2f);
            header.Graphics.DrawLine(pen, 0, header.Height - 3, header.Width, header.Height - 3);
            header.Graphics.DrawLine(pen, 0, header.Height, header.Width, header.Height);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            dlgSave.Filter = "Excel-Dokumente|*.xls;*.xlsx";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".xls"; // Default file extension
            dlgSave.Title = "Speichern als Excel-Datei";
            dlgSave.FileName = "Untitled";

            if (dlgSave.ShowDialog() != DialogResult.Cancel)
            {
                GridGroupingExcelConverterControl excelConverter = new GridGroupingExcelConverterControl();

                GroupingGridExcelConverterControl converter = new GroupingGridExcelConverterControl();
                ExcelExportingOptions exportingOptions = new ExcelExportingOptions();

                converter.ExportCaptionSummary = true; //    .ExportCaptionSummary = true;

                Syncfusion.GridExcelConverter.ConverterOptions c = new Syncfusion.GridExcelConverter.ConverterOptions();

                converter.GroupingGridToExcel(grdAnzeige, dlgSave.FileName, c);

                ExcelEngine engine = new ExcelEngine();
                IWorkbook workbook = engine.Excel.Workbooks.Open(dlgSave.FileName);
                IWorksheet sheet = workbook.Worksheets[0];

                IRange usedRange = sheet.UsedRange;
                int Space = 5;
                int row = usedRange.Row + Space;
                int column = usedRange.Column;
                int lastRow = usedRange.LastRow + Space;
                int lastCol = usedRange.LastColumn;

                sheet.Range["F1:AF1"].AutofitRows();

                sheet.UsedRange["A1:C1" + lastRow.ToString()].ColumnWidth = 4;
                sheet.UsedRange["D1:H1" + lastRow.ToString()].ColumnWidth = 0;
                sheet.UsedRange["M1:M1" + lastRow.ToString()].ColumnWidth = 0;

                for (int i = 1; i < lastRow; i++)
                {
                    if (sheet.UsedRange["A" + i.ToString() + ":A" + i.ToString()].Value != String.Empty)
                        sheet.UsedRange["A" + i.ToString() + ":AF" + i.ToString()].Merge();

                    if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value != String.Empty)
                        sheet.UsedRange["B" + i.ToString() + ":AF" + i.ToString()].Merge();

                    if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value != String.Empty)
                        sheet.UsedRange["C" + i.ToString() + ":AF" + i.ToString()].Merge();
                }

                for (int i = lastRow; i > 0; i--)
                {
                    if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value.ToString().StartsWith("tblÄra:"))
                        sheet.DeleteRow(i);

                    if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value.ToString().StartsWith("tblGebiet:"))
                        sheet.DeleteRow(i);

                    if (sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value.ToString().StartsWith("tblReport:"))
                        sheet.DeleteRow(i);
                }

                workbook.SaveAs(dlgSave.FileName);
                workbook.Close();
                engine.Dispose();

                System.Diagnostics.Process.Start(dlgSave.FileName);
            }
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            dlgSave.Filter = "Word-Dokumente|*.doc;*.docx";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".doc"; // Default file extension
            dlgSave.Title = "Speichern als Word-Datei";
            dlgSave.FileName = "Untitled";

            if (dlgSave.ShowDialog() != DialogResult.Cancel)
            {
                GroupingGridWordConverter converter = new GroupingGridWordConverter();
                converter.UseColumnHeaderText = true;
                converter.ExportStyle = true;

                converter.GroupingGridToWord(dlgSave.FileName, grdAnzeige);

                System.Diagnostics.Process.Start(dlgSave.FileName);
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            dlgSave.Filter = "CSV-Dokumente|*.csv";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".csv"; // Default file extension
            dlgSave.Title = "Speichern als CSV-Datei";
            dlgSave.FileName = "Untitled";

            if (dlgSave.ShowDialog() != DialogResult.Cancel)
            {
                Helper.WriteDataTable(dlgSave.FileName, dtReport, ';');
                MessageBox.Show("Die Datei " + dlgSave.FileName + " wurde gespeichert", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GridPrintDocumentAdv pd = new GridPrintDocumentAdv(grdAnzeige.TableControl);

            pd.DrawGridPrintFooter += new Syncfusion.GridHelperClasses.GridPrintDocumentAdv.DrawGridHeaderFooterEventHandler(pd_DrawGridPrintFooter);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;

            pd.DefaultPageSettings.Landscape = true;
            pd.DefaultPageSettings.Margins.Left = 5;
            pd.DefaultPageSettings.Margins.Right = 20;
            pd.DefaultPageSettings.Margins.Top = 25;
            pd.DefaultPageSettings.Margins.Bottom = 25;
            pd.ScaleColumnsToFitPage = true;
            pd.PagesToFit = 1;
            pd.DocumentName = Text;

            pd.HeaderHeight = 40;
            pd.HeaderPrintStyleInfo.Text = Text;
            pd.HeaderPrintStyleInfo.Font.Bold = true;
            pd.HeaderPrintStyleInfo.Font.Size = 20;
            pd.HeaderPrintStyleInfo.HorizontalAlignment = GridHorizontalAlignment.Left;

            pd.FooterHeight = 30;

            if (printDialog.ShowDialog() == DialogResult.OK)
                pd.Print();
        }

        /// <summary>
        /// Applying WeightedSummary for Given Columns
        /// </summary>
        private GridSummaryColumnDescriptor SummaryColumnDescriptor(string sourceCol)
        {
            GridSummaryColumnDescriptor wgtSumCol = new GridSummaryColumnDescriptor();
            wgtSumCol.Name = sourceCol.ToString(); //special name following the convention above
            wgtSumCol.DataMember = sourceCol; //the column this summary is applied to
            wgtSumCol.DisplayColumn = sourceCol; //where thissummary is displayed
            wgtSumCol.Format = "{Sum:###.##0.00}"; //what is displayed in the summary
            wgtSumCol.SummaryType = SummaryType.DoubleAggregate; //marks this as a CustomSummary
            wgtSumCol.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            wgtSumCol.MaxLength = 6;

            return wgtSumCol;
        }

        private void frmReporting_Shown(object sender, EventArgs e)
        {
            if (dtReport.Rows.Count == 0)
                MessageBox.Show(Localization.GetTranslation("Keys", "noCoins"), "Reporting", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void pd_DrawGridPrintFooter(object sender, Syncfusion.GridHelperClasses.GridPrintHeaderFooterTemplateArgs e)
        {
            //Get the rectangle area to draw
            Rectangle footer = e.DrawRectangle;
            //Draw copy right
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            Font font = new Font("Helvetica", 12);
            SolidBrush brush = new SolidBrush(Color.Black);
            e.Graphics.DrawString("Datum :" + DateTime.Now.ToShortDateString(), font, brush, GridUtil.CenterPoint(footer), format);
            //Draw page number
            format.LineAlignment = StringAlignment.Far;
            format.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(string.Format(Localization.GetTranslation(Name, "Seite") + " {0} " + Localization.GetTranslation(Name, "von") + " {1}", e.PageNumber, e.PageCount), font, brush, new Point(footer.Right - 100, footer.Bottom - 10), format);
            //Dispose resources
            format.Dispose();
            font.Dispose();
            brush.Dispose();
        }

        private void showReport()
        {
            //Same as SourceListSetEntry.Name for Child Table.
            GridRelationDescriptor nationToAeraRelationDescriptor = new GridRelationDescriptor();
            nationToAeraRelationDescriptor.ChildTableName = "AeraTable";
            nationToAeraRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            nationToAeraRelationDescriptor.RelationKeys.Add("ID", "NATIONID");

            //Adds relation to Parent Table.
            grdAnzeige.TableDescriptor.Relations.Add(nationToAeraRelationDescriptor);

            GridRelationDescriptor aeraToGebietRelationDescriptor = new GridRelationDescriptor();
            aeraToGebietRelationDescriptor.ChildTableName = "GebietTable";
            aeraToGebietRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            aeraToGebietRelationDescriptor.RelationKeys.Add("ID", "AERAID");

            //Adds relation to Child Table.
            nationToAeraRelationDescriptor.ChildTableDescriptor.Relations.Add(aeraToGebietRelationDescriptor);

            //Same as SourceListSetEntry.Name for Grand Child Table.
            GridRelationDescriptor xxxRelationDescriptor = new GridRelationDescriptor();
            xxxRelationDescriptor.ChildTableName = "ReportTable";
            xxxRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            xxxRelationDescriptor.RelationKeys.Add("ID", "GEBIETID");

            //Adds relation to Child Table.
            aeraToGebietRelationDescriptor.ChildTableDescriptor.Relations.Add(xxxRelationDescriptor);

            // Register any DataTable/IList with SourceListSet, so that RelationDescriptor can resolve the name
            this.grdAnzeige.Engine.SourceListSet.Clear();
            this.grdAnzeige.Engine.SourceListSet.Add("NationsTable", dtNations);
            this.grdAnzeige.Engine.SourceListSet.Add("AeraTable", dtÄras);
            this.grdAnzeige.Engine.SourceListSet.Add("GebietTable", dtGebiete);
            this.grdAnzeige.Engine.SourceListSet.Add("ReportTable", dtReport);

            this.grdAnzeige.DataSource = dtNations;

            grdAnzeige.Table.ExpandAllGroups();
            grdAnzeige.Table.DefaultCaptionRowHeight = 0;


            grdAnzeige.TableDescriptor.VisibleColumns.Remove("ID");
            grdAnzeige.GetTableDescriptor("AeraTable").VisibleColumns.Remove("ID");
            grdAnzeige.GetTableDescriptor("AeraTable").VisibleColumns.Remove("SORTIERUNG");

            grdAnzeige.GetTableDescriptor("GebietTable").VisibleColumns.Remove("ID");
            grdAnzeige.GetTableDescriptor("GebietTable").VisibleColumns.Remove("SORTIERUNG");

            grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("NATIONID");
            grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("AERAID");
            grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("NATION");
            grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("AERA");
            grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("GEBIET");
            grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("GUID");

            grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorNation);
            grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Bold = true;
            grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Size = 10;
            grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].Width = 1157;
            grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].HeaderText = "Nation";

            var aeraTable = grdAnzeige.GetTable("AeraTable");
            aeraTable.DefaultColumnHeaderRowHeight = 0;
            aeraTable.DefaultEmptySectionHeight = 0; ;

            aeraTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorÄra);
            aeraTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Bold = true;
            aeraTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Size = 10;
            aeraTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].Width = 1140;
            aeraTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].HeaderText = "Ära";

            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorÄra);
            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Bold = true;
            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Size = 10;
            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Width = 1140;
            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].HeaderText = "Ära";

            //grdAnzeige.GetTable("AeraTable").DefaultColumnHeaderRowHeight = 0;
            //grdAnzeige.GetTable("AeraTable").DefaultCaptionRowHeight = 0;
            //grdAnzeige.GetTable("AeraTable").DefaultEmptySectionHeight = 0;

            var gebietTable = grdAnzeige.GetTable("GebietTable");
            gebietTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorGebiet);
            gebietTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Bold = true;
            gebietTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Size = 10;
            gebietTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].Width = 1122;
            gebietTable.ParentTableDescriptor.Columns["BEZEICHNUNG"].HeaderText = "Gebiet";

            grdAnzeige.GetTableDescriptor("ReportTable").Appearance.AnyHeaderCell.Font.Bold = true;
            grdAnzeige.GetTableDescriptor("ReportTable").Appearance.AnySummaryCell.Font.Bold = true;

            var reportTable = grdAnzeige. GetTable("ReportTable");
            reportTable.DefaultCaptionRowHeight = 0;
            reportTable.DefaultEmptySectionHeight = 0;

            grdAnzeige.GetTableDescriptor("ReportTable").Columns["MUENZZ"].HeaderText = Localization.GetTranslation(Name, "Muenzz");
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["JAHRGANG"].HeaderText = Localization.GetTranslation(Name, "Jahrgang");
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["NOMINAL"].HeaderText = Localization.GetTranslation(Name, "Nominal");
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["WAEHRUNG"].HeaderText = Localization.GetTranslation(Name, "Währung");

            grdAnzeige.GetTableDescriptor("ReportTable").Columns["WAEHRUNG"].Width = 79;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["JAHRGANG"].Width = 40;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["NOMINAL"].Width = 50;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["MUENZZ"].Width = 50;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["S"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SP"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SS"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSP"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZ"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZP"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STN"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STH"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["PP"].Width = 28;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STHPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].Width = 60;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["GESAMT"].Width = 110;

            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].HeaderText = Helper.Erhaltungsgrade[0].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPPREIS"].HeaderText = Helper.Erhaltungsgrade[1].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].HeaderText = Helper.Erhaltungsgrade[2].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPPREIS"].HeaderText = Helper.Erhaltungsgrade[3].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].HeaderText = Helper.Erhaltungsgrade[4].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPPREIS"].HeaderText = Helper.Erhaltungsgrade[5].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].HeaderText = Helper.Erhaltungsgrade[6].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STHPREIS"].HeaderText = Helper.Erhaltungsgrade[7].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].HeaderText = Helper.Erhaltungsgrade[8].Erhaltung + "\n[" + Settings.CurrentWährung + "]";

            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SP"].HeaderText = Helper.Erhaltungsgrade[1].Erhaltung;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSP"].HeaderText = Helper.Erhaltungsgrade[3].Erhaltung;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZP"].HeaderText = Helper.Erhaltungsgrade[5].Erhaltung;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STN"].HeaderText = Helper.Erhaltungsgrade[6].Erhaltung;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STH"].HeaderText = Helper.Erhaltungsgrade[7].Erhaltung;
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["GESAMT"].HeaderText = Localization.GetTranslation(Name, "Gesamt") + "\n[" + Settings.CurrentWährung + "]";

            grdAnzeige.GetTableDescriptor("ReportTable").Columns["S"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZ"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STN"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["PP"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);

            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STHPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["GESAMT"].Appearance.AnyCell.Format = "###,##0.00";

            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["STHPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            grdAnzeige.GetTableDescriptor("ReportTable").Columns["GESAMT"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);

            GridSummaryColumnDescriptor s = Helper.SummaryColumnDescriptor("S", TypeCode.Int32);
            GridSummaryColumnDescriptor sp = Helper.SummaryColumnDescriptor("SP", TypeCode.Int32);
            GridSummaryColumnDescriptor ss = Helper.SummaryColumnDescriptor("SS", TypeCode.Int32);
            GridSummaryColumnDescriptor ssp = Helper.SummaryColumnDescriptor("SSP", TypeCode.Int32);
            GridSummaryColumnDescriptor vz = Helper.SummaryColumnDescriptor("VZ", TypeCode.Int32);
            GridSummaryColumnDescriptor vzp = Helper.SummaryColumnDescriptor("VZP", TypeCode.Int32);
            GridSummaryColumnDescriptor stn = Helper.SummaryColumnDescriptor("STN", TypeCode.Int32);
            GridSummaryColumnDescriptor sth = Helper.SummaryColumnDescriptor("STH", TypeCode.Int32);
            GridSummaryColumnDescriptor pp = Helper.SummaryColumnDescriptor("PP", TypeCode.Int32);
            GridSummaryColumnDescriptor gesamt = Helper.SummaryColumnDescriptor("GESAMT", TypeCode.Double);

            GridSummaryRowDescriptor srd = new GridSummaryRowDescriptor();
            srd.Appearance.AnyCell.BackColor = colorSumme;
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
            grdAnzeige.GetTableDescriptor("ReportTable").SummaryRows.Add(srd);

            GridSummaryRowDescriptor srdAera = new GridSummaryRowDescriptor();
            srdAera.Appearance.AnyCell.BackColor = colorSumme;
            srdAera.SummaryColumns.Add(s);
            srdAera.SummaryColumns.Add(sp);
            srdAera.SummaryColumns.Add(ss);
            srdAera.SummaryColumns.Add(ssp);
            srdAera.SummaryColumns.Add(vz);
            srdAera.SummaryColumns.Add(vzp);
            srdAera.SummaryColumns.Add(stn);
            srdAera.SummaryColumns.Add(sth);
            srdAera.SummaryColumns.Add(pp);
            srdAera.SummaryColumns.Add(gesamt);
            grdAnzeige.GetTableDescriptor("GebietTable").SummaryRows.Add(srdAera);

            GridSummaryRowDescriptor srdNation = new GridSummaryRowDescriptor();
            srdNation.Appearance.AnyCell.BackColor = colorSumme;
            srdNation.SummaryColumns.Add(s);
            srdNation.SummaryColumns.Add(sp);
            srdNation.SummaryColumns.Add(ss);
            srdNation.SummaryColumns.Add(ssp);
            srdNation.SummaryColumns.Add(vz);
            srdNation.SummaryColumns.Add(vzp);
            srdNation.SummaryColumns.Add(stn);
            srdNation.SummaryColumns.Add(sth);
            srdNation.SummaryColumns.Add(pp);
            srdNation.SummaryColumns.Add(gesamt);
            grdAnzeige.GetTableDescriptor("AeraTable").SummaryRows.Add(srdNation);

            GridSummaryRowDescriptor srdxxx = new GridSummaryRowDescriptor();
            srdxxx.Appearance.AnyCell.BackColor = colorSumme;
            srdxxx.SummaryColumns.Add(s);
            srdxxx.SummaryColumns.Add(sp);
            srdxxx.SummaryColumns.Add(ss);
            srdxxx.SummaryColumns.Add(ssp);
            srdxxx.SummaryColumns.Add(vz);
            srdxxx.SummaryColumns.Add(vzp);
            srdxxx.SummaryColumns.Add(stn);
            srdxxx.SummaryColumns.Add(sth);
            srdxxx.SummaryColumns.Add(pp);
            srdxxx.SummaryColumns.Add(gesamt);
            grdAnzeige.TableDescriptor.SummaryRows.Add(srdxxx);

            grdAnzeige.Table.ExpandAllRecords();
        }

        private void initReport()
        {
            //Same as SourceListSetEntry.Name for Child Table.
            GridRelationDescriptor nationToAeraRelationDescriptor = new GridRelationDescriptor();
            nationToAeraRelationDescriptor.ChildTableName = "AeraTable";
            nationToAeraRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            nationToAeraRelationDescriptor.RelationKeys.Add("ID", "NATIONID");

            //Adds relation to Parent Table.
            grdAnzeige.TableDescriptor.Relations.Add(nationToAeraRelationDescriptor);

            GridRelationDescriptor aeraToGebietRelationDescriptor = new GridRelationDescriptor();
            aeraToGebietRelationDescriptor.ChildTableName = "GebietTable";
            aeraToGebietRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            aeraToGebietRelationDescriptor.RelationKeys.Add("ID", "AERAID");

            //Adds relation to Child Table.
            nationToAeraRelationDescriptor.ChildTableDescriptor.Relations.Add(aeraToGebietRelationDescriptor);

            //Same as SourceListSetEntry.Name for Grand Child Table.
            GridRelationDescriptor xxxRelationDescriptor = new GridRelationDescriptor();
            xxxRelationDescriptor.ChildTableName = "ReportTable";
            xxxRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            xxxRelationDescriptor.RelationKeys.Add("ID", "GEBIETID");

            //Adds relation to Child Table.
            aeraToGebietRelationDescriptor.ChildTableDescriptor.Relations.Add(xxxRelationDescriptor);

            //// Register any DataTable/IList with SourceListSet, so that RelationDescriptor can resolve the name
            //this.grdAnzeige.Engine.SourceListSet.Add("NationsTable", dtNations);
            //this.grdAnzeige.Engine.SourceListSet.Add("AeraTable", dtÄras);
            //this.grdAnzeige.Engine.SourceListSet.Add("GebietTable", dtGebiete);
            //this.grdAnzeige.Engine.SourceListSet.Add("ReportTable", dtReport);

            //this.grdAnzeige.DataSource = dtNations;

            //grdAnzeige.Table.ExpandAllGroups();


            //grdAnzeige.TableDescriptor.VisibleColumns.Remove("ID");
            //grdAnzeige.GetTableDescriptor("AeraTable").VisibleColumns.Remove("ID");
            //grdAnzeige.GetTableDescriptor("AeraTable").VisibleColumns.Remove("SORTIERUNG");

            //grdAnzeige.GetTableDescriptor("GebietTable").VisibleColumns.Remove("ID");
            //grdAnzeige.GetTableDescriptor("GebietTable").VisibleColumns.Remove("SORTIERUNG");

            //grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("NATIONID");
            //grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("AERAID");
            //grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("NATION");
            //grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("AERA");
            //grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("GEBIET");
            //grdAnzeige.GetTableDescriptor("ReportTable").VisibleColumns.Remove("GUID");

            //grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorNation);
            //grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Bold = true;
            //grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Size = 10;
            //grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].Width = 1157;
            //grdAnzeige.TableDescriptor.Columns["BEZEICHNUNG"].HeaderText = "Nation";

            ////grdAnzeige.GetTable("tblÄra").DefaultColumnHeaderRowHeight = 0;
            //grdAnzeige.Table.DefaultCaptionRowHeight = 0;
            ////grdAnzeige.GetTable("tblÄra").DefaultEmptySectionHeight = 0;

            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorÄra);
            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Bold = true;
            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Size = 10;
            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Width = 1140;
            //grdAnzeige.GetTable("AeraTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].HeaderText = "Ära";

            ////grdAnzeige.GetTable("AeraTable").DefaultColumnHeaderRowHeight = 0;
            ////grdAnzeige.GetTable("AeraTable").DefaultCaptionRowHeight = 0;
            ////grdAnzeige.GetTable("AeraTable").DefaultEmptySectionHeight = 0;

            //grdAnzeige.GetTable("GebietTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorGebiet);
            //grdAnzeige.GetTable("GebietTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Bold = true;
            //grdAnzeige.GetTable("GebietTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Appearance.AnyCell.Font.Size = 10;
            //grdAnzeige.GetTable("GebietTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].Width = 1122;
            //grdAnzeige.GetTable("GebietTable").ParentTableDescriptor.Columns["BEZEICHNUNG"].HeaderText = "Gebiet";

            //grdAnzeige.GetTableDescriptor("ReportTable").Appearance.AnyHeaderCell.Font.Bold = true;
            //grdAnzeige.GetTableDescriptor("ReportTable").Appearance.AnySummaryCell.Font.Bold = true;

            ////grdAnzeige.GetTable("ReportTable").DefaultColumnHeaderRowHeight = 0;
            //grdAnzeige.GetTable("ReportTable").DefaultCaptionRowHeight = 0;
            //grdAnzeige.GetTable("ReportTable").DefaultEmptySectionHeight = 0;

            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["MUENZZ"].HeaderText = Localization.GetTranslation(Name, "Muenzz");
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["JAHRGANG"].HeaderText = Localization.GetTranslation(Name, "Jahrgang");
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["NOMINAL"].HeaderText = Localization.GetTranslation(Name, "Nominal");
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["WAEHRUNG"].HeaderText = Localization.GetTranslation(Name, "Währung");

            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["WAEHRUNG"].Width = 79;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["JAHRGANG"].Width = 40;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["NOMINAL"].Width = 50;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["MUENZZ"].Width = 50;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["S"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SP"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SS"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSP"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZ"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZP"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STN"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STH"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["PP"].Width = 28;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STHPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].Width = 60;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["GESAMT"].Width = 110;

            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].HeaderText = Helper.Erhaltungsgrade[0].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPPREIS"].HeaderText = Helper.Erhaltungsgrade[1].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].HeaderText = Helper.Erhaltungsgrade[2].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPPREIS"].HeaderText = Helper.Erhaltungsgrade[3].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].HeaderText = Helper.Erhaltungsgrade[4].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPPREIS"].HeaderText = Helper.Erhaltungsgrade[5].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].HeaderText = Helper.Erhaltungsgrade[6].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STHPREIS"].HeaderText = Helper.Erhaltungsgrade[7].Erhaltung + "\n[" + Settings.CurrentWährung + "]";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].HeaderText = Helper.Erhaltungsgrade[8].Erhaltung + "\n[" + Settings.CurrentWährung + "]";

            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SP"].HeaderText = Helper.Erhaltungsgrade[1].Erhaltung;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSP"].HeaderText = Helper.Erhaltungsgrade[3].Erhaltung;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZP"].HeaderText = Helper.Erhaltungsgrade[5].Erhaltung;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STN"].HeaderText = Helper.Erhaltungsgrade[6].Erhaltung;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STH"].HeaderText = Helper.Erhaltungsgrade[7].Erhaltung;
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["GESAMT"].HeaderText = Localization.GetTranslation(Name, "Gesamt") + "\n[" + Settings.CurrentWährung + "]";

            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["S"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZ"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STN"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["PP"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(colorColumns);

            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STHPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].Appearance.AnyCell.Format = "###,##0.00";
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["GESAMT"].Appearance.AnyCell.Format = "###,##0.00";

            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SPPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["SSPPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["VZPPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STNPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["STHPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["PPPREIS"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);
            //grdAnzeige.GetTableDescriptor("ReportTable").Columns["GESAMT"].Appearance.AnyCell.CultureInfo = CultureInfo.GetCultureInfo(Settings.Culture);

            //GridSummaryColumnDescriptor s = Helper.SummaryColumnDescriptor("S", TypeCode.Int32);
            //GridSummaryColumnDescriptor sp = Helper.SummaryColumnDescriptor("SP", TypeCode.Int32);
            //GridSummaryColumnDescriptor ss = Helper.SummaryColumnDescriptor("SS", TypeCode.Int32);
            //GridSummaryColumnDescriptor ssp = Helper.SummaryColumnDescriptor("SSP", TypeCode.Int32);
            //GridSummaryColumnDescriptor vz = Helper.SummaryColumnDescriptor("VZ", TypeCode.Int32);
            //GridSummaryColumnDescriptor vzp = Helper.SummaryColumnDescriptor("VZP", TypeCode.Int32);
            //GridSummaryColumnDescriptor stn = Helper.SummaryColumnDescriptor("STN", TypeCode.Int32);
            //GridSummaryColumnDescriptor sth = Helper.SummaryColumnDescriptor("STH", TypeCode.Int32);
            //GridSummaryColumnDescriptor pp = Helper.SummaryColumnDescriptor("PP", TypeCode.Int32);
            //GridSummaryColumnDescriptor gesamt = Helper.SummaryColumnDescriptor("GESAMT", TypeCode.Double);

            //GridSummaryRowDescriptor srd = new GridSummaryRowDescriptor();
            //srd.Appearance.AnyCell.BackColor = colorSumme;
            //srd.SummaryColumns.Add(s);
            //srd.SummaryColumns.Add(sp);
            //srd.SummaryColumns.Add(ss);
            //srd.SummaryColumns.Add(ssp);
            //srd.SummaryColumns.Add(vz);
            //srd.SummaryColumns.Add(vzp);
            //srd.SummaryColumns.Add(stn);
            //srd.SummaryColumns.Add(sth);
            //srd.SummaryColumns.Add(pp);
            //srd.SummaryColumns.Add(gesamt);
            //grdAnzeige.GetTableDescriptor("ReportTable").SummaryRows.Add(srd);

            //GridSummaryRowDescriptor srdAera = new GridSummaryRowDescriptor();
            //srdAera.Appearance.AnyCell.BackColor = colorSumme;
            //srdAera.SummaryColumns.Add(s);
            //srdAera.SummaryColumns.Add(sp);
            //srdAera.SummaryColumns.Add(ss);
            //srdAera.SummaryColumns.Add(ssp);
            //srdAera.SummaryColumns.Add(vz);
            //srdAera.SummaryColumns.Add(vzp);
            //srdAera.SummaryColumns.Add(stn);
            //srdAera.SummaryColumns.Add(sth);
            //srdAera.SummaryColumns.Add(pp);
            //srdAera.SummaryColumns.Add(gesamt);
            //grdAnzeige.GetTableDescriptor("GebietTable").SummaryRows.Add(srdAera);

            //GridSummaryRowDescriptor srdNation = new GridSummaryRowDescriptor();
            //srdNation.Appearance.AnyCell.BackColor = colorSumme;
            //srdNation.SummaryColumns.Add(s);
            //srdNation.SummaryColumns.Add(sp);
            //srdNation.SummaryColumns.Add(ss);
            //srdNation.SummaryColumns.Add(ssp);
            //srdNation.SummaryColumns.Add(vz);
            //srdNation.SummaryColumns.Add(vzp);
            //srdNation.SummaryColumns.Add(stn);
            //srdNation.SummaryColumns.Add(sth);
            //srdNation.SummaryColumns.Add(pp);
            //srdNation.SummaryColumns.Add(gesamt);
            //grdAnzeige.GetTableDescriptor("AeraTable").SummaryRows.Add(srdNation);

            //GridSummaryRowDescriptor srdxxx = new GridSummaryRowDescriptor();
            //srdxxx.Appearance.AnyCell.BackColor = colorSumme;
            //srdxxx.SummaryColumns.Add(s);
            //srdxxx.SummaryColumns.Add(sp);
            //srdxxx.SummaryColumns.Add(ss);
            //srdxxx.SummaryColumns.Add(ssp);
            //srdxxx.SummaryColumns.Add(vz);
            //srdxxx.SummaryColumns.Add(vzp);
            //srdxxx.SummaryColumns.Add(stn);
            //srdxxx.SummaryColumns.Add(sth);
            //srdxxx.SummaryColumns.Add(pp);
            //srdxxx.SummaryColumns.Add(gesamt);
            //grdAnzeige.TableDescriptor.SummaryRows.Add(srdxxx);

            //grdAnzeige.Table.ExpandAllRecords();
        }
    }
}

