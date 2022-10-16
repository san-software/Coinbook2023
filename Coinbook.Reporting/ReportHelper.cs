using Coinbook.Enumerations;
using Coinbook.Helper;
using Coinbook.Lokalisierung;
using Coinbook.Model;
using Ookii.Dialogs.WinForms;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.GroupingGridExcelConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using LiteDB.Database;

namespace Coinbook
{
    public static class ReportHelper
    {
        private static string Caption = string.Empty;
        private static string formName = string.Empty;
        private static GridPDFConverter pdfConverter = null;
        private static bool pdfLandscape=true;

        public static void ReportToPDF(GridControlBase grdAnzeige, Form form, string caption, bool landscape = false)
        {
            Caption = caption;
            formName = form.Name;

            VistaSaveFileDialog dlgSave = new VistaSaveFileDialog();

            dlgSave.Filter = "PDF-Dokumente|*.pdf";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".pdf"; // Default file extension
            dlgSave.Title = Localization.GetTranslation("Keys", "msgSavePDF");
            dlgSave.FileName = "Untitled";

            if (dlgSave.ShowDialog(form) != DialogResult.Cancel)
            {
                form.Invalidate();
                Application.DoEvents();

                if (File.Exists(dlgSave.FileName))
                {
                    try
                    {
                        File.Delete(dlgSave.FileName);
                    }
                    catch (SystemException e)
                    {
                        MessageBoxAdv.Show(form, e.Message, "Coinbook", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                form.Cursor = Cursors.WaitCursor;

                pdfConverter = new GridPDFConverter();
                PdfDocument pdfdoc = new PdfDocument();
                PdfLayoutFormat format = new PdfLayoutFormat();

                if (landscape)
                    pdfdoc.PageSettings.Orientation = PdfPageOrientation.Landscape;
                else
                    pdfdoc.PageSettings.Orientation = PdfPageOrientation.Portrait;

                pdfLandscape = landscape;

                pdfConverter.Exporting += new GridPDFConverter.PDFExportingEventHandler(pdfConverter_Exporting);
                pdfConverter.DrawPDFHeader += new GridPDFConverter.DrawPDFHeaderFooterEventHandler(pdfConverter_DrawPDFHeader);
                pdfConverter.DrawPDFFooter += new GridPDFConverter.DrawPDFHeaderFooterEventHandler(pdfConverter_DrawPDFFooter);

                pdfConverter.ShowFooter = true;
                pdfConverter.ShowHeader = true;
                format.Layout = PdfLayoutType.OnePage;

                pdfConverter.ExportToPdf(dlgSave.FileName, grdAnzeige);

                string text = string.Format(Localization.GetTranslation("Keys", "msgSaved"), dlgSave.FileName);
                MessageBoxAdv.Show(text, "Coinbook", MessageBoxButtons.OK, MessageBoxIcon.Information);

                System.Diagnostics.Process.Start(dlgSave.FileName);                //Launching the PDF file using the default Application.[Acrobat Reader]

                pdfConverter.Exporting -= new GridPDFConverter.PDFExportingEventHandler(pdfConverter_Exporting);
                pdfConverter.DrawPDFHeader -= new GridPDFConverter.DrawPDFHeaderFooterEventHandler(pdfConverter_DrawPDFHeader);
                pdfConverter.DrawPDFFooter -= new GridPDFConverter.DrawPDFHeaderFooterEventHandler(pdfConverter_DrawPDFFooter);
            }
        }

        private static void pdfConverter_Exporting(object sender, PDFExportingEventArgs e)
        {
            if (pdfLandscape)
            {
                e.PdfDocument.PageSettings.Width = 870;
                e.PdfDocument.PageSettings.Height = 650;
            }
            else
            {
                e.PdfDocument.PageSettings.Width = 650;
                e.PdfDocument.PageSettings.Height = 900;
            }
        }

        //create the pdf header
        private static void pdfConverter_DrawPDFHeader(object sender, PDFHeaderFooterEventArgs e)
        {
            PdfPageTemplateElement header = e.HeaderFooterTemplate;

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
            float doubleHeight = font.Height * 2;
            Color activeColor = Color.FromArgb(44, 71, 120);
            SizeF imageSize = new SizeF(110f, 35f);

            PdfSolidBrush brush = new PdfSolidBrush(activeColor);

            PdfPen pen = new PdfPen(Color.Black, 3f);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);

            //Set formattings for the text
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;

            //Draw title
            header.Graphics.DrawString(Caption, font, brush, new RectangleF(0, 0, header.Width, header.Height), format);
            brush = new PdfSolidBrush(Color.Gray);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

            format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Bottom;

            //Draw some lines in the header
            pen = new PdfPen(Color.Black, 0.7f);
            header.Graphics.DrawLine(pen, 0, 0, header.Width, 0);
            pen = new PdfPen(Color.Black, 2f);
            header.Graphics.DrawLine(pen, 0, 03, header.Width + 3, 03);
            pen = new PdfPen(Color.Black, 2f);
            header.Graphics.DrawLine(pen, 0, header.Height - 3, header.Width, header.Height - 3);
            header.Graphics.DrawLine(pen, 0, header.Height, header.Width, header.Height);

        }

        private static void pdfConverter_DrawPDFFooter(object sender, PDFHeaderFooterEventArgs e)
        {
            //e.HeaderFooterTemplate.Height = 300;

            PdfPageTemplateElement footer = e.HeaderFooterTemplate;
            PdfSolidBrush brush = new PdfSolidBrush(Color.Black);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold);
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Bottom;
            footer.Graphics.DrawString(DateTime.Now.ToShortDateString(), font, brush, new RectangleF(40, footer.Height - 20, footer.Width, footer.Height), format);

            format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Right;
            format.LineAlignment = PdfVerticalAlignment.Bottom;

            //Create page number field
            PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

            //Create page count field
            PdfPageCountField pageCount = new PdfPageCountField(font, brush);

            PdfAutomaticField[] auto = { pageNumber, pageCount };

            string page = Localization.GetTranslation(formName, "Seite");
            string from = Localization.GetTranslation(formName, "von");
            string temp = string.Format("{0} {1} {2} {3}", page, pageNumber, from, pageCount);
            PdfCompositeField compositeField = new PdfCompositeField("page {0} to {1}", auto);

            compositeField.Bounds = footer.Bounds;
            compositeField.Draw(footer.Graphics, new PointF(400, footer.Height - 20));
        }

        public static void ReportToExcel(GridGroupingControl grdAnzeige, Form form, string caption, enmReporting typ)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();

            dlgSave.Filter = "Excel-Dokumente|*.xls;*.xlsx";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".xls"; // Default file extension
            dlgSave.Title = Localization.GetTranslation("Keys", "msgSaveExcel");
            dlgSave.FileName = "Untitled";

             if (dlgSave.ShowDialog() != DialogResult.Cancel)
            {
                form.Invalidate();
                Application.DoEvents();

                form.Cursor = Cursors.WaitCursor;

                Caption = caption;
                formName = form.Name;

                GridGroupingExcelConverterControl excelConverter = new GridGroupingExcelConverterControl();

                GroupingGridExcelConverterControl converter = new GroupingGridExcelConverterControl();
                ExcelExportingOptions exportingOptions = new ExcelExportingOptions();

                exportingOptions.ExportGroupSummary = true;
                exportingOptions.ExportTableSummary = true;

                converter.ExportCaptionSummary = true; //    .ExportCaptionSummary = true;
                converter.GroupingGridToExcel(grdAnzeige, dlgSave.FileName, Syncfusion.GridExcelConverter.ConverterOptions.Visible);

                switch (typ)
                {
                    case enmReporting.Wertermittlung:
                        ExcelFormatWertermittlung(dlgSave.FileName);
                        break;

                    case enmReporting.Reporting:
                        ExcelFormatReporting(dlgSave.FileName);
                        break;

                    case enmReporting.Reporting2:
                        ExcelFormatReporting2(dlgSave.FileName);
                        break;

                    case enmReporting.Fehllisten:
                        ExcelFormatFehlliste(dlgSave.FileName);
                        break;
                }

                string text = string.Format(Localization.GetTranslation("Keys", "msgSaved"), dlgSave.FileName);
                MessageBoxAdv.Show(text, "Coinbook", MessageBoxButtons.OK, MessageBoxIcon.Information);

                System.Diagnostics.Process.Start(dlgSave.FileName);
            }
        }

        private static void ExcelFormatWertermittlung(string file)
        {
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workbook = engine.Excel.Workbooks.Open(file);
            IWorksheet sheet = workbook.Worksheets[0];

            IRange usedRange = sheet.UsedRange;
            int Space = 5;
            int row = usedRange.Row + Space;
            int column = usedRange.Column;
            int lastRow = usedRange.LastRow + Space;
            int lastCol = usedRange.LastColumn;

            sheet.Range["F1:AF1"].AutofitRows();
            sheet.UsedRange["B:B"].ColumnWidth = 30;
            sheet.DeleteRow(2);
            sheet.DeleteColumn(1);

            workbook.SaveAs(file);
            workbook.Close();
            engine.Dispose();
        }

        private static void ExcelFormatReporting(string file)
        {
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workbook = engine.Excel.Workbooks.Open(file);
            IWorksheet sheet = workbook.Worksheets[0];

            IRange usedRange = sheet.UsedRange;
            int Space = 5;
            int row = usedRange.Row + Space;
            int column = usedRange.Column;
            int lastRow = usedRange.LastRow + Space;
            int lastCol = usedRange.LastColumn;

            sheet.UsedRange["A:C"].ColumnWidth = 3;

            for (int i = lastRow; i > 0; i--)
            {
                if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value.ToString().StartsWith("tblÄra:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value.ToString().StartsWith("tblGebiet:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value.ToString().StartsWith("tblReport:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["E" + i.ToString() + ":E" + i.ToString()].Value.ToString().StartsWith("Sortierung"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value.ToString().StartsWith("Sortierung"))
                    sheet.DeleteRow(i);
            }

            sheet.DeleteColumn(5);
            sheet.DeleteColumn(4);

            lastRow = usedRange.LastRow + Space;

            for (int i = 1; i < lastRow; i++)
            {
                if (sheet.UsedRange["A" + i.ToString() + ":A" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["A" + i.ToString() + ":Z" + i.ToString()].Merge();

                if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["B" + i.ToString() + ":Z" + i.ToString()].Merge();

                if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["C" + i.ToString() + ":Z" + i.ToString()].Merge();
            }

            sheet.DeleteRow(1);

            workbook.SaveAs(file);
            workbook.Close();
            engine.Dispose();
        }


        private static void ExcelFormatReporting2(string file)
        {
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workbook = engine.Excel.Workbooks.Open(file);
            IWorksheet sheet = workbook.Worksheets[0];

            IRange usedRange = sheet.UsedRange;
            int Space = 5;
            int row = usedRange.Row + Space;
            int column = usedRange.Column;
            int lastRow = usedRange.LastRow + Space;
            int lastCol = usedRange.LastColumn;

            sheet.Range["A1:AF1"].AutofitRows();
            sheet.UsedRange["A:C"].ColumnWidth = 3;

            for (int i = lastRow; i > 0; i--)
            {
                if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value.ToString().StartsWith("tblÄra:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value.ToString().StartsWith("tblGebiet:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value.ToString().StartsWith("tblReport:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["E" + i.ToString() + ":E" + i.ToString()].Value.ToString().StartsWith("Sortierung"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value.ToString().StartsWith("Sortierung"))
                    sheet.DeleteRow(i);
            }

            sheet.DeleteColumn(5);
            sheet.DeleteColumn(4);

            lastRow = usedRange.LastRow + Space;

            for (int i = 1; i < lastRow; i++)
            {
                if (sheet.UsedRange["A" + i.ToString() + ":A" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["A" + i.ToString() + ":M" + i.ToString()].Merge();

                if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["B" + i.ToString() + ":M" + i.ToString()].Merge();

                if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["C" + i.ToString() + ":M" + i.ToString()].Merge();
            }

            sheet.UsedRange["J:J"].ColumnWidth = 20;

            workbook.SaveAs(file);
            workbook.Close();
            engine.Dispose();
        }

        private static void ExcelFormatFehlliste(string file)
        {
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workbook = engine.Excel.Workbooks.Open(file);
            IWorksheet sheet = workbook.Worksheets[0];

            IRange usedRange = sheet.UsedRange;
            int Space = 5;
            int row = usedRange.Row + Space;
            int column = usedRange.Column;
            int lastRow = usedRange.LastRow + Space;
            int lastCol = usedRange.LastColumn;

            for (int i = lastRow; i > 0; i--)
            {
                if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value.ToString().StartsWith("tblÄra:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value.ToString().StartsWith("tblGebiet:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value.ToString().StartsWith("tblReport:"))
                    sheet.DeleteRow(i);

                if (sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value.ToString().StartsWith("Sortierung"))
                    sheet.DeleteRow(i);
            }

            sheet.UsedRange["A:C"].ColumnWidth = 3;

            lastRow = usedRange.LastRow + Space;

            for (int i = 1; i < lastRow; i++)
            {
                if (sheet.UsedRange["A" + i.ToString() + ":A" + i.ToString()].Value.ToString() != "")
                {
                    sheet.UsedRange["A" + i.ToString() + ":A" + i.ToString()].Value = sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value;
                    sheet.UsedRange["A" + i.ToString() + ":A" + i.ToString()].CellStyle = sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].CellStyle;
                    sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value = "";
                }

                if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value.ToString() != "")
                {
                    sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value = sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value;
                    sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].CellStyle = sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].CellStyle;
                    sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value = "";
                }

                if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value.ToString() != "")
                {
                    sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value = sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value;
                    sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].CellStyle = sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].CellStyle;
                    sheet.UsedRange["D" + i.ToString() + ":D" + i.ToString()].Value = "";
                }
            }

            sheet.DeleteColumn(5);
            sheet.DeleteColumn(4);

            for (int i = 1; i < lastRow; i++)
            {
                if (sheet.UsedRange["A" + i.ToString() + ":A" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["A" + i.ToString() + ":R" + i.ToString()].Merge();

                if (sheet.UsedRange["B" + i.ToString() + ":B" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["B" + i.ToString() + ":R" + i.ToString()].Merge();

                if (sheet.UsedRange["C" + i.ToString() + ":C" + i.ToString()].Value != String.Empty)
                    sheet.UsedRange["C" + i.ToString() + ":R" + i.ToString()].Merge();
            
                if (sheet.UsedRange["A" + i.ToString() + ":A" + i.ToString()].Value.ToString() != "")
                    sheet.DeleteRow(i + 1);
            }

            sheet.ShowColumn(4, true);

            workbook.SaveAs(file);
            workbook.Close();
            engine.Dispose();
        }
        public static void ReportToCSV(DataTable dt)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();

            dlgSave.Filter = "CSV-Dokumente|*.csv";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".csv"; // Default file extension
            dlgSave.Title = Localization.GetTranslation("Keys", "msgSaveCSV");
            dlgSave.FileName = "Untitled";

            if (dlgSave.ShowDialog() != DialogResult.Cancel)
            {
                Application.DoEvents(); 
                CoinbookHelper.WriteDataTable(dlgSave.FileName, dt, ';');
                string text = string.Format(Localization.GetTranslation("Keys", "msgSaved"),dlgSave.FileName);
                MessageBoxAdv.Show(text, "Coinbook", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void ReportToCSV<T>(List<T> list)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();

            if (list == null || list.Count == 0) return;

            dlgSave.Filter = "CSV-Dokumente|*.csv";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".csv"; // Default file extension
            dlgSave.Title = Localization.GetTranslation("Keys", "msgSaveCSV");
            dlgSave.FileName = "Untitled";

            if (dlgSave.ShowDialog() != DialogResult.Cancel)
            {
                Application.DoEvents();
                //get type from 0th member
                Type t = list[0].GetType();
                string newLine = Environment.NewLine;

                if (!File.Exists(dlgSave.FileName)) File.Create(dlgSave.FileName);

                using (var sw = new StreamWriter(dlgSave.FileName))
                {
                    //make a new instance of the class name we figured out to get its props
                    object o = Activator.CreateInstance(t);
                    //gets all properties
                    PropertyInfo[] props = o.GetType().GetProperties();

                    //foreach of the properties in class above, write out properties this is the header row
                    sw.Write(string.Join(";", props.Select(d => d.Name).ToArray()) + newLine);

                    //this acts as datarow
                    foreach (T item in list)
                    {
                        props = item.GetType().GetProperties();
                        string row = string.Empty;

                        foreach (var p in props)
                            row = row + (p.GetValue(item, null) != null ? p.GetValue(item, null).ToString() : String.Empty) + ";";

                        row = row.TrimEnd(';') + newLine;
                        sw.Write(row);
                    }
                }

                string text = string.Format(Localization.GetTranslation("Keys", "msgSaved"), dlgSave.FileName);
                MessageBoxAdv.Show(text, "Coinbook", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void ReportToPrinter(GridControlBase grdAnzeige, Form form, bool landscape = false)
        {
            formName = form.Name;
            GridPrintDocumentAdv pd = new GridPrintDocumentAdv(grdAnzeige);

            pd.DrawGridPrintFooter += new Syncfusion.GridHelperClasses.GridPrintDocumentAdv.DrawGridHeaderFooterEventHandler(pd_DrawGridPrintFooter);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;
            printDialog.AllowSomePages = true;

            pd.DefaultPageSettings.Landscape = landscape;
            pd.DefaultPageSettings.Margins.Left = 25;
            pd.DefaultPageSettings.Margins.Right = 25;
            pd.DefaultPageSettings.Margins.Top = 25;
            pd.DefaultPageSettings.Margins.Bottom = 25;
            pd.ScaleColumnsToFitPage = true;
            pd.PagesToFit = 1;
            pd.DocumentName = form.Text;

            pd.HeaderHeight = 40;
            pd.HeaderPrintStyleInfo.Text = form.Text;
            pd.HeaderPrintStyleInfo.Font.Bold = true;
            pd.HeaderPrintStyleInfo.Font.Size = 20;
            pd.HeaderPrintStyleInfo.HorizontalAlignment = GridHorizontalAlignment.Left;

            pd.FooterHeight = 30;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                form.Invalidate();
                Application.DoEvents();

                //List<int> pages = new List<int>();
                //for (int i = printDialog.PrinterSettings.FromPage; i <= printDialog.PrinterSettings.ToPage; i++)
                //    pages.Add(i);
                //pd.PagesToPrint = pages;

                //pd.DefaultPageSettings.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
                //pd.DefaultPageSettings.PrinterSettings.FromPage = printDialog.PrinterSettings.FromPage;
                //pd.DefaultPageSettings.PrinterSettings.ToPage = printDialog.PrinterSettings.ToPage;
                pd.Print();
            }
        }

        private static void pd_DrawGridPrintFooter(object sender, Syncfusion.GridHelperClasses.GridPrintHeaderFooterTemplateArgs e)
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
            e.Graphics.DrawString(string.Format(Localization.GetTranslation(formName, "Seite") + " {0} " + Localization.GetTranslation(formName, "von") + " {1}",
                e.PageNumber, e.PageCount), font, brush, new Point(footer.Right - 100, footer.Bottom-5), format);
            //Dispose resources
            format.Dispose();
            font.Dispose();
            brush.Dispose();
        }

        public static void SetRelations(GridGroupingControl grdAnzeige, DataTable dtNation, DataTable dtÄra, DataTable dtGebiet, DataTable dt)
        {
            grdAnzeige.DataSource = null;

            GridRelationDescriptor äraRelationDescriptor = new GridRelationDescriptor();
            äraRelationDescriptor.ChildTableName = "tblÄra";    // same as SourceListSetEntry.Name for childTable (see below)
            äraRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            äraRelationDescriptor.RelationKeys.Add("NationID", "NationID");

            GridRelationDescriptor gebietRelationDescriptor = new GridRelationDescriptor();
            gebietRelationDescriptor.ChildTableName = "tblGebiet";    // same as SourceListSetEntry.Name for childTable (see below)
            gebietRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            gebietRelationDescriptor.RelationKeys.Add("AeraID", "AeraID");

            GridRelationDescriptor reportRelationDescriptor = new GridRelationDescriptor();
            reportRelationDescriptor.ChildTableName = "tblReport";    // same as SourceListSetEntry.Name for childTable (see below)
            reportRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            reportRelationDescriptor.RelationKeys.Add("GebietID", "GebietID");

            // Add relation to ParentTable 
            grdAnzeige.TableDescriptor.Relations.Clear();
            grdAnzeige.TableDescriptor.Relations.Add(äraRelationDescriptor);
            äraRelationDescriptor.ChildTableDescriptor.Relations.Add(gebietRelationDescriptor);
            gebietRelationDescriptor.ChildTableDescriptor.Relations.Add(reportRelationDescriptor);

            // Register any DataTable/IList with SourceListSet, so that RelationDescriptor can resolve the name
            grdAnzeige.Engine.SourceListSet.Clear();
            grdAnzeige.Engine.SourceListSet.Add("tblNation", dtNation);
            grdAnzeige.Engine.SourceListSet.Add("tblÄra", dtÄra);
            grdAnzeige.Engine.SourceListSet.Add("tblGebiet", dtGebiet);
            grdAnzeige.Engine.SourceListSet.Add("tblReport", dt);

            //Addin DataSource to the gridgroupingcontrol.
            grdAnzeige.DataSource = dtNation;
        }

        public static void FormatReportGroups(GridGroupingControl grdAnzeige, int nationWidth, int aeraWidth, int gebietWidth)
        {
            grdAnzeige.Table.ExpandAllGroups();
            grdAnzeige.Table.ExpandAllRecords();

            grdAnzeige.TableDescriptor.Columns["Bezeichnung"].HeaderText = "Nation";
            grdAnzeige.TableDescriptor.Columns["NationID"].HeaderText = "ID";
            grdAnzeige.TableDescriptor.Columns["NationID"].Width = 0;
            grdAnzeige.ChildGroupOptions.ShowAddNewRecordAfterDetails = false;
            grdAnzeige.ChildGroupOptions.ShowAddNewRecordBeforeDetails = false;

            grdAnzeige.GetTable("tblNation").DefaultColumnHeaderRowHeight = 0;

            grdAnzeige.GetTableDescriptor("tblNation").Columns["Bezeichnung"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorNation);
            grdAnzeige.GetTableDescriptor("tblNation").Columns["Bezeichnung"].Appearance.AnyCell.Font.Bold = true;
            grdAnzeige.GetTableDescriptor("tblNation").Columns["Bezeichnung"].Appearance.AnyCell.Font.Size = 10;
            grdAnzeige.GetTableDescriptor("tblNation").Columns["Bezeichnung"].Width = nationWidth;

            grdAnzeige.GetTable("tblÄra").DefaultColumnHeaderRowHeight = 0;
            grdAnzeige.GetTable("tblÄra").DefaultCaptionRowHeight = 0;
            grdAnzeige.GetTable("tblÄra").DefaultEmptySectionHeight = 0;

            grdAnzeige.GetTableDescriptor("tblÄra").Columns["Bezeichnung"].ReadOnly = true;
            grdAnzeige.GetTableDescriptor("tblÄra").Columns["Bezeichnung"].HeaderText = "Ära";
            grdAnzeige.GetTableDescriptor("tblÄra").Columns["AeraID"].Width = 0;
            grdAnzeige.GetTableDescriptor("tblÄra").Columns["Sortierung"].Width = 0;

            grdAnzeige.GetTableDescriptor("tblÄra").Columns["Bezeichnung"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorÄra);
            grdAnzeige.GetTableDescriptor("tblÄra").Columns["Bezeichnung"].Appearance.AnyCell.Font.Bold = true;
            grdAnzeige.GetTableDescriptor("tblÄra").Columns["Bezeichnung"].Appearance.AnyCell.Font.Size = 10;
            grdAnzeige.GetTableDescriptor("tblÄra").Columns["Bezeichnung"].Width = aeraWidth;

            grdAnzeige.GetTable("tblGebiet").DefaultColumnHeaderRowHeight = 0;
            grdAnzeige.GetTable("tblGebiet").DefaultCaptionRowHeight = 0;
            grdAnzeige.GetTable("tblGebiet").DefaultEmptySectionHeight = 0;

            grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].ReadOnly = true;
            grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].HeaderText = "Gebiet";
            grdAnzeige.GetTableDescriptor("tblGebiet").Columns["GebietID"].Width = 0;
            grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Sortierung"].Width = 0;

            grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].Appearance.AnyCell.Interior = new Syncfusion.Drawing.BrushInfo(CoinbookHelper.ColorGebiet);
            grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].Appearance.AnyCell.Font.Bold = true;
            grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].Appearance.AnyCell.Font.Size = 10;
            grdAnzeige.GetTableDescriptor("tblGebiet").Columns["Bezeichnung"].Width = gebietWidth;

            grdAnzeige.GetTable("tblReport").DefaultCaptionRowHeight = 0;
            grdAnzeige.GetTable("tblReport").DefaultRecordRowHeight = 20;
            grdAnzeige.GetTable("tblReport").DefaultColumnHeaderRowHeight = 30;

            grdAnzeige.GetTableDescriptor("tblReport").Appearance.AnyHeaderCell.Font.Bold = true;
            grdAnzeige.GetTableDescriptor("tblReport").Appearance.AnySummaryCell.Font.Bold = true;
        }

        public static DataTable GetReportNations(List<Report> reportListe, int nationID)
        {
            
            DataTable dt = new DataTable("tblNation");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));

            var nations = CoinbookHelper.Nationen;

            if (nationID != 0)
            {
                var nation = nations.FirstOrDefault(x => x.ID == nationID);
                var row = dt.NewRow();
                row["NationID"] = nation.ID;
                row["Bezeichnung"] = nation.Bezeichnung;
                dt.Rows.Add(row);
            }
            else
            {
                foreach (var nation in nations)
                {
                    if (reportListe.FirstOrDefault(x => x.NationID == nation.ID) != null)
                    {
                        var row = dt.NewRow();
                        row["NationID"] = nation.ID;
                        row["Bezeichnung"] = nation.Bezeichnung;
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }

        public static DataTable GetReportNations(List<Report2> reportListe, int nationID)
        {

            DataTable dt = new DataTable("tblNation");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));

            var nations = CoinbookHelper.Nationen;

            if (nationID != 0)
            {
                var nation = nations.FirstOrDefault(x => x.ID == nationID);
                var row = dt.NewRow();
                row["NationID"] = nation.ID;
                row["Bezeichnung"] = nation.Bezeichnung;
                dt.Rows.Add(row);
            }
            else
            {
                foreach (var nation in nations)
                {
                    if (reportListe.FirstOrDefault(x => x.NationID == nation.ID) != null)
                    {
                        var row = dt.NewRow();
                        row["NationID"] = nation.ID;
                        row["Bezeichnung"] = nation.Bezeichnung;
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }

        private static DataRow nationAddRow(Nation nation, DataRow row)
        {
            row["NationID"] = nation.ID;
            row["Bezeichnung"] = nation.Bezeichnung;

            return row;
        }

        public static DataTable GetReportÄras(List<Report> reportListe, int nationID, int aeraID)
        {
            DataTable dt = new DataTable("tblÄra");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));
            dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

            var aeras = CoinbookHelper.Aeras;

            if (aeraID != 0)
            {
                var aera = aeras.FirstOrDefault(x => x.ID == aeraID);
                dt.Rows.Add(aeraAddRow(aera, dt.NewRow()));
            }
            else
            {
                if (nationID == 0)
                {
                    foreach (var aera in aeras)
                        if (reportListe.FirstOrDefault(x => x.AeraID == aera.ID) != null)
                            dt.Rows.Add(aeraAddRow(aera, dt.NewRow()));
                }
                else
                {
                    foreach (var aera in aeras)
                        if (reportListe.FirstOrDefault(x => x.AeraID == aera.ID) != null)
                            dt.Rows.Add(aeraAddRow(aera, dt.NewRow()));
                }
            }

            return dt;
        }

        public static DataTable GetReportÄras(List<Report2> reportListe, int nationID, int aeraID)
        {
            DataTable dt = new DataTable("tblÄra");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));
            dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

            var aeras = CoinbookHelper.Aeras;

            if (aeraID != 0)
            {
                var aera = aeras.FirstOrDefault(x => x.ID == aeraID);
                dt.Rows.Add(aeraAddRow(aera, dt.NewRow()));
            }
            else
            {
                if (nationID == 0)
                {
                    foreach (var aera in aeras)
                        if (reportListe.FirstOrDefault(x => x.AeraID == aera.ID) != null) 
                            dt.Rows.Add(aeraAddRow(aera, dt.NewRow()));
                }
                else
                {
                    foreach (var aera in aeras)
                        if (reportListe.FirstOrDefault(x => x.AeraID == aera.ID) != null) 
                            dt.Rows.Add(aeraAddRow(aera, dt.NewRow()));
                }
            }

            return dt;
        }

        private static DataRow aeraAddRow(Aera aera, DataRow row)
        {
            row["NationID"] = aera.NationID;
            row["Bezeichnung"] = aera.Bezeichnung;
            row["AeraID"] = aera.ID;
            row["Sortierung"] = aera.Sortierung;

            return row;
        }

        public static DataTable GetReportGebiete(List<Report> reportListe, int nationID, int aeraID, int gebietID)
        {
            DataTable dt = new DataTable("tblGebiet");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("GebietID", typeof(int)));
            dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

            var gebiete = CoinbookHelper.Regions;

            if (gebietID != 0)
            {
                var gebiet = gebiete.FirstOrDefault(x => x.ID == gebietID);
                if (gebiet != null)
                    dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
            }
            else
            {
                if (nationID == 0)
                {
                    foreach (var gebiet in gebiete)
                        if(reportListe.FirstOrDefault(x => x.GebietID == gebiet.ID) != null)
                            dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
                }
                else
                {
                    foreach (var gebiet in gebiete)
                        if(reportListe.FirstOrDefault(x => x.GebietID == gebiet.ID) != null)
                            dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
                }
            }

            return dt;
        }

        public static DataTable GetReportGebiete(List<Report2> reportListe, int nationID, int aeraID, int gebietID)
        {
            DataTable dt = new DataTable("tblGebiet");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("GebietID", typeof(int)));
            dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

            var gebiete = CoinbookHelper.Regions;

            if (gebietID != 0)
            {
                var gebiet = gebiete.FirstOrDefault(x => x.ID == gebietID);
                if (gebiet != null)
                    dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
            }
            else
            {
                if (nationID == 0)
                {
                    foreach (var gebiet in gebiete)
                        if (reportListe.FirstOrDefault(x => x.GebietID == gebiet.ID) != null) 
                            dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
                }
                else
                {
                    foreach (var gebiet in gebiete)
                        if (reportListe.FirstOrDefault(x => x.GebietID == gebiet.ID) != null)
                            dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
                }
            }

            return dt;
        }

        private static DataRow gebietAddRow(Gebiet gebiet, DataRow row)
        {
            row["GebietID"] = gebiet.ID;
            row["Bezeichnung"] = gebiet.Bezeichnung;
            row["AeraID"] = gebiet.AeraID;
            row["Sortierung"] = gebiet.Sortierung;

            return row;
        }

        public static DataTable ReportTable(List<Report> reportListe)
        {
            DataTable dt = new DataTable("tblReport");
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("GebietID", typeof(int)));
            dt.Columns.Add(new DataColumn("Waehrung", typeof(string)));
            dt.Columns.Add(new DataColumn("Nominal", typeof(string)));
            dt.Columns.Add(new DataColumn("Jahrgang", typeof(string)));
            dt.Columns.Add(new DataColumn("Muenzz", typeof(string)));
            dt.Columns.Add(new DataColumn("S", typeof(string)));
            dt.Columns.Add(new DataColumn("SPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("SP", typeof(string)));
            dt.Columns.Add(new DataColumn("SPPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("SS", typeof(string)));
            dt.Columns.Add(new DataColumn("SSPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("SSP", typeof(string)));
            dt.Columns.Add(new DataColumn("SSPPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("VZ", typeof(string)));
            dt.Columns.Add(new DataColumn("VZPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("VZP", typeof(string)));
            dt.Columns.Add(new DataColumn("VZPPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("STN", typeof(string)));
            dt.Columns.Add(new DataColumn("STNPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("STH", typeof(string)));
            dt.Columns.Add(new DataColumn("STHPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("PP", typeof(string)));
            dt.Columns.Add(new DataColumn("PPPreis", typeof(string)));
            dt.Columns.Add(new DataColumn("Gesamt", typeof(string)));

            foreach (var item in reportListe)
            {
                DataRow row = dt.NewRow();
                row["NationID"] = item.NationID;
                row["AeraID"] = item.AeraID;
                row["GebietID"] = item.GebietID;
                row["Waehrung"] = item.Waehrung;
                row["Nominal"] = item.Nominal;
                row["Jahrgang"] = item.Jahrgang;
                row["Muenzz"] = item.Muenzz;
                row["S"] = item.S != 0 ? item.S.ToString() : string.Empty;
                row["SPreis"] = item.SPreis != 0 ? string.Format("{0:###,###.00}", item.SPreis) : string.Empty;
                row["SP"] = item.SP != 0 ? item.SP.ToString() : string.Empty;
                row["SPPreis"] = item.SPPreis != 0 ? string.Format("{0:###,###.00}", item.SPPreis) : string.Empty;
                row["SS"] = item.SS != 0 ? item.SS.ToString() : string.Empty;
                row["SSPreis"] = item.SSPreis != 0 ? string.Format("{0:###,###.00}", item.SSPreis) : string.Empty;
                row["SSP"] = item.SSP != 0 ? item.SSP.ToString() : string.Empty;
                row["SSPPreis"] = item.SSPPreis != 0 ? string.Format("{0:###,###.00}", item.SSPPreis) : string.Empty;
                row["VZ"] = item.VZ != 0 ? item.VZ.ToString() : string.Empty;
                row["VZPreis"] = item.VZPreis != 0 ? string.Format("{0:###,###.00}", item.VZPreis) : string.Empty;
                row["VZP"] = item.VZP != 0 ? item.VZP.ToString() : string.Empty;
                row["VZPPreis"] = item.VZPPreis != 0 ? string.Format("{0:###,###.00}", item.VZPPreis) : string.Empty;
                row["STN"] = item.STN != 0 ? item.STN.ToString() : string.Empty;
                row["STNPreis"] = item.STNPreis != 0 ? string.Format("{0:###,###.00}", item.STNPreis) : string.Empty;
                row["STH"] = item.STH != 0 ? item.STH.ToString() : string.Empty;
                row["STHPreis"] = item.STHPreis != 0 ? string.Format("{0:###,###.00}", item.STHPreis) : string.Empty;
                row["PP"] = item.PP != 0 ? item.PP.ToString() : string.Empty;
                row["PPPreis"] = item.PPPreis != 0 ? string.Format("{0:###,###.00}", item.PPPreis) : string.Empty;
                row["Gesamt"] = item.Gesamt != 0 ? string.Format("{0:###,###.00}", item.Gesamt) : string.Empty;

                dt.Rows.Add(row);
            }

            return dt;
        }

        public static DataTable Report2Table(List<Report2> reportListe)
        {
            DataTable dt = new DataTable("tblReport");
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("GebietID", typeof(int)));
            dt.Columns.Add(new DataColumn("KatNr", typeof(string)));
            dt.Columns.Add(new DataColumn("Waehrung", typeof(string)));
            dt.Columns.Add(new DataColumn("Nominal", typeof(string)));
            dt.Columns.Add(new DataColumn("Jahrgang", typeof(string)));
            dt.Columns.Add(new DataColumn("Muenzz", typeof(string)));
            dt.Columns.Add(new DataColumn("Erhaltung", typeof(string)));
            dt.Columns.Add(new DataColumn("Motiv", typeof(string)));
            dt.Columns.Add(new DataColumn("Anzahl", typeof(int)));
            dt.Columns.Add(new DataColumn("Preis", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Gesamt", typeof(decimal)));

            foreach (var item in reportListe)
            {
                DataRow row = dt.NewRow();
                row["NationID"] = item.NationID;
                row["AeraID"] = item.AeraID;
                row["GebietID"] = item.GebietID;
                row["Waehrung"] = item.Waehrung;
                row["Nominal"] = item.Nominal;
                row["Jahrgang"] = item.Jahrgang;
                row["Muenzz"] = item.Muenzz;
                row["Erhaltung"] = CoinbookHelper.Erhaltungsgrade[item.Erhaltung - 1].Erhaltung;
                row["Motiv"] = item.Motiv;
                row["Anzahl"] = item.Anzahl;
                row["Preis"] = item.Preis;
                row["KatNr"] = item.KatNr;
                row["Gesamt"] = item.Gesamt != 0 ? string.Format("{0:###,###.00}", item.Gesamt) : string.Empty;

                dt.Rows.Add(row);
            }

            return dt;
        }

        public static void SetRelationsFehlliste(GridGroupingControl grdAnzeige, List<Nation> nations, List<Aera> aeras, List<Gebiet> gebiete, List<Fehlliste> fehlliste)
        {
            GridRelationDescriptor aeraRelationDescriptor;
            GridRelationDescriptor gebietRelationDescriptor;

            grdAnzeige.DataSource = null;
            grdAnzeige.TableDescriptor.Relations.Clear();

            GridRelationDescriptor relationDescriptor = new GridRelationDescriptor();
            relationDescriptor.ChildTableName = "tblAera";    // same as SourceListSetEntry.Name for childTable (see below)
            relationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            relationDescriptor.RelationKeys.Add("ID", "NationID");
            grdAnzeige.TableDescriptor.Relations.Add(relationDescriptor);   //Relation to Nation 

            if (gebiete.Count != 0)
            {
                aeraRelationDescriptor = new GridRelationDescriptor();
                aeraRelationDescriptor.ChildTableName = "tblGebiet";    // same as SourceListSetEntry.Name for childTable (see below)
                aeraRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
                aeraRelationDescriptor.RelationKeys.Add("ID", "AeraID");
                relationDescriptor.ChildTableDescriptor.Relations.Add(aeraRelationDescriptor);  //Relation to Ära

                gebietRelationDescriptor = new GridRelationDescriptor();
                gebietRelationDescriptor.ChildTableName = "tblReport";    // same as SourceListSetEntry.Name for childTable (see below)
                gebietRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
                gebietRelationDescriptor.RelationKeys.Add("ID", "GebietID");
                aeraRelationDescriptor.ChildTableDescriptor.Relations.Add(gebietRelationDescriptor);    //Relation to Gebiet
            }
            else
            {
                aeraRelationDescriptor = new GridRelationDescriptor();
                aeraRelationDescriptor.ChildTableName = "tblReport";    // same as SourceListSetEntry.Name for childTable (see below)
                aeraRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
                aeraRelationDescriptor.RelationKeys.Add("ID", "AeraID");
                relationDescriptor.ChildTableDescriptor.Relations.Add(aeraRelationDescriptor);  //Relation to Ära
            }

            // Register any DataTable/IList with SourceListSet, so that RelationDescriptor can resolve the name
            grdAnzeige.Engine.SourceListSet.Clear();
            grdAnzeige.Engine.SourceListSet.Add("tblNation", nations);
            grdAnzeige.Engine.SourceListSet.Add("tblAera", aeras);

            if (gebiete.Count != 0)
                grdAnzeige.Engine.SourceListSet.Add("tblGebiet", gebiete);

            grdAnzeige.Engine.SourceListSet.Add("tblReport", fehlliste);

            //Addin DataSource to the gridgroupingcontrol.
            grdAnzeige.DataSource = nations;
        }

        public static void SetRelationsWert(GridGroupingControl grdAnzeige, List<KeyValuePair<int,string>> nations, List<Wertermittlung> wertermittlung)
        {
            grdAnzeige.DataSource = null;
            grdAnzeige.TableDescriptor.Relations.Clear();

            GridRelationDescriptor aeraRelationDescriptor = new GridRelationDescriptor();
            aeraRelationDescriptor.ChildTableName = "tblReport";    // same as SourceListSetEntry.Name for childTable (see below)
            aeraRelationDescriptor.RelationKind = RelationKind.RelatedMasterDetails;
            aeraRelationDescriptor.RelationKeys.Add("Key", "NationID");
            grdAnzeige.TableDescriptor.Relations.Add(aeraRelationDescriptor);   //Relation to Nation 

            // Register any DataTable/IList with SourceListSet, so that RelationDescriptor can resolve the name
            grdAnzeige.Engine.SourceListSet.Clear();
            grdAnzeige.Engine.SourceListSet.Add("tblNation", nations);
            grdAnzeige.Engine.SourceListSet.Add("tblReport", wertermittlung);

            //Addin DataSource to the gridgroupingcontrol.
            grdAnzeige.DataSource = nations;
        }

        //public static List<Wertermittlung> ReportingWert(enmReportTyp reportTyp,
        //                                  int nation,
        //                                  decimal faktor,
        //                                  enmPreise settings,
        //                                  List<Nation> nationen,
        //                                  List<Aera> aeras)
        //{
        //    List<Wertermittlung> wertermittlung = null;

        //    if (reportTyp == enmReportTyp.KostenSammlung || reportTyp == enmReportTyp.KostenDoubletten)
        //        settings = enmPreise.Kaufpreise;

        //    if (settings == enmPreise.Kaufpreise && reportTyp == enmReportTyp.WerteSammlung)
        //        reportTyp = enmReportTyp.KostenSammlung;

        //    if (settings == enmPreise.Kaufpreise && reportTyp == enmReportTyp.WerteDoubletten)
        //        reportTyp = enmReportTyp.KostenDoubletten;

        //    using (var db = new LiteDatabase(Connectionstring))
        //    {
        //        var collection = db.GetCollection<Bestand>("tblBestand");
        //        var bestand = nation == 0 ? collection.FindAll().ToList() : collection.Find(x => x.NationID == nation).ToList();

        //        switch (reportTyp)
        //        {
        //            case enmReportTyp.WerteSammlung:
        //                bestand = Helper.GetKatalogPreise(db, bestand, nation, settings, faktor);

        //                foreach (var item in bestand)
        //                    item.Gesamt = item.S * item.PS + item.PSP * item.SP + item.PSS * item.SS
        //                        + item.PSSP * item.SSP + item.PVZ * item.VZ + item.PVZP * item.VZP
        //                        + item.PSTN * item.STN + item.PSTH * item.STH + item.PPP * item.PP;

        //                wertermittlung = Werteberechnung(bestand, nation, nationen, aeras, false);
        //                break;

        //            case enmReportTyp.WerteDoubletten:
        //                bestand = GetKatalogPreise(db, bestand, nation, settings, faktor);

        //                foreach (var item in bestand)
        //                    item.Gesamt = item.DS * item.PS + item.PSP * item.DSP + item.PSS * item.DSS
        //                        + item.PSSP * item.DSSP + item.PVZ * item.DVZ + item.PVZP * item.DVZP
        //                        + item.PSTN * item.DSTN + item.PSTH * item.DSTH + item.PPP * item.DPP;

        //                wertermittlung = Werteberechnung(bestand, nation, nationen, aeras, true);
        //                break;

        //            case enmReportTyp.KostenSammlung:
        //                bestand = GetKaufpreise(db, bestand, false);
        //                wertermittlung = Werteberechnung(bestand, nation, nationen, aeras, false);
        //                break;

        //            case enmReportTyp.KostenDoubletten:
        //                bestand = GetKaufpreise(db, bestand, true);
        //                wertermittlung = Werteberechnung(bestand, nation, nationen, aeras, true);
        //                break;
        //        }

        //        for (int i = 0; i < wertermittlung.Count; i++)
        //        {
        //            wertermittlung[i].NationID = nation;
        //            if (wertermittlung[i].S == 0) wertermittlung[i].S = null;
        //            if (wertermittlung[i].SP == 0) wertermittlung[i].SP = null;
        //            if (wertermittlung[i].SS == 0) wertermittlung[i].SS = null;
        //            if (wertermittlung[i].SSP == 0) wertermittlung[i].SSP = null;
        //            if (wertermittlung[i].VZ == 0) wertermittlung[i].VZ = null;
        //            if (wertermittlung[i].VZP == 0) wertermittlung[i].VZP = null;
        //            if (wertermittlung[i].STN == 0) wertermittlung[i].STN = null;
        //            if (wertermittlung[i].STH == 0) wertermittlung[i].STH = null;
        //            if (wertermittlung[i].PP == 0) wertermittlung[i].PP = null;
        //        }
        //        return wertermittlung;
        //    }
        //}

        //internal static List<Bestand> GetKatalogPreise(LiteDatabase db, List<Bestand> bestand, int nation, enmPreise settings, decimal faktor)
        //{
        //    List<Katalog2> katalogpreise;

        //    var collectionKatalogPreise = db.GetCollection<Katalog2>("tblKatalog");

        //    if (nation == 0)
        //        katalogpreise = collectionKatalogPreise.FindAll().ToList();
        //    else
        //        katalogpreise = collectionKatalogPreise.Find(Query.EQ("NationID", nation)).ToList();

        //    for (int i = 0; i < bestand.Count; i++)
        //    {
        //        var p = katalogpreise.FirstOrDefault(x => x.GUID == bestand[i].Guid);

        //        if (p != null)
        //        {
        //            bestand[i].PS = p.SPreis * faktor;
        //            bestand[i].PSP = p.SPPreis * faktor;
        //            bestand[i].PSS = p.SSPreis * faktor;
        //            bestand[i].PSSP = p.SSPPreis * faktor;
        //            bestand[i].PVZ = p.VZPreis * faktor;
        //            bestand[i].PVZP = p.VZPPreis * faktor;
        //            bestand[i].PSTN = p.STNPreis * faktor;
        //            bestand[i].PSTH = p.STHPreis * faktor;
        //            bestand[i].PPP = p.PPPreis * faktor;
        //            bestand[i].Jahrgang = p.Jahrgang;
        //            bestand[i].Muenzzeichen = p.Muenzzeichen;
        //            bestand[i].Nominal = p.Nominal;
        //            bestand[i].Waehrung = p.Waehrung;
        //            bestand[i].KatNr = p.KatNr;
        //            bestand[i].Motiv = p.Motiv;
        //            bestand[i].AeraID = p.AeraID;
        //            bestand[i].GebietID = p.GebietID;
        //            bestand[i].NationID = p.NationID;

        //            switch (Convert.ToInt16(bestand[i].Erhaltung))
        //            {
        //                case 1:
        //                    bestand[i].Preis = bestand[i].PS;
        //                    break;

        //                case 2:
        //                    bestand[i].Preis = bestand[i].PSP;
        //                    break;

        //                case 3:
        //                    bestand[i].Preis = bestand[i].PSS;
        //                    break;

        //                case 4:
        //                    bestand[i].Preis = bestand[i].PSSP;
        //                    break;

        //                case 5:
        //                    bestand[i].Preis = bestand[i].PVZ;
        //                    break;

        //                case 6:
        //                    bestand[i].Preis = bestand[i].PVZP;
        //                    break;

        //                case 7:
        //                    bestand[i].Preis = bestand[i].PSTN;
        //                    break;

        //                case 8:
        //                    bestand[i].Preis = bestand[i].PSTH;
        //                    break;

        //                case 9:
        //                    bestand[i].Preis = bestand[i].PPP;
        //                    break;
        //            }
        //        }
        //    }

        //    if (settings == enmPreise.EigenePreise)
        //        bestand = GetOwnPrices(db, bestand, nation);

        //    return bestand;
        //}

        //internal static List<Bestand> GetKaufpreise(LiteDatabase db, List<Bestand> bestand, bool doublette)
        //{
        //    var collectionSammlung = db.GetCollection<Sammlung>("tblSammlung");
        //    var kaufpreise = collectionSammlung.Find(x => x.Doublette == doublette).ToList();

        //    for (int i = 0; i < bestand.Count; i++)
        //    {
        //        var preise = kaufpreise.Where(a => a.Guid == bestand[i].Guid).ToList();

        //        foreach (var item in preise)
        //            bestand[i].Gesamt += item.Kaufpreis;
        //    }

        //    return bestand;
        //}

        //internal static List<Bestand> GetOwnPrices(LiteDatabase db, List<Bestand> bestand, int nationID)
        //{
        //    List<Preise> preise;

        //    var collectionPreise = db.GetCollection<Preise>("tblPreise");

        //    if (nationID == 0)
        //        preise = collectionPreise.FindAll().ToList();
        //    else
        //        preise = collectionPreise.Find(Query.EQ("NationID", nationID)).ToList();

        //    for (int i = 0; i < bestand.Count; i++)
        //    {
        //        var p = preise.FirstOrDefault(x => x.GUID == bestand[i].Guid);

        //        if (p != null)
        //        {
        //            if (p.SPreis != 0)
        //                bestand[i].PS = p.SPreis;

        //            if (p.SPPreis != 0)
        //                bestand[i].PSP = p.SPPreis;

        //            if (p.SSPreis != 0)
        //                bestand[i].PSS = p.SSPreis;

        //            if (p.SSPPreis != 0)
        //                bestand[i].PSSP = p.SSPPreis;

        //            if (p.VZPreis != 0)
        //                bestand[i].PVZ = p.VZPreis;

        //            if (p.VZPPreis != 0)
        //                bestand[i].PVZP = p.VZPPreis;

        //            if (p.STNPreis != 0)
        //                bestand[i].PSTN = p.STNPreis;

        //            if (p.STHPreis != 0)
        //                bestand[i].PSTH = p.STHPreis;

        //            if (p.PPPreis != 0)
        //                bestand[i].PPP = p.PPPreis;
        //        }
        //    }

        //    return bestand;
        //}

        //internal static List<Wertermittlung> Werteberechnung(List<Bestand> bestand, int nation, List<Nation> nationen, List<Aera> aeras, bool doublette)
        //{
        //    List<Wertermittlung> wertermittlung;

        //    if (nation == 0)
        //        wertermittlung = doublette ? WerteberechnungDoublettenNation(bestand, nationen) : WerteberechnungSammlungNation(bestand, nationen);
        //    else
        //        wertermittlung = doublette ? WerteberechnungDoublettenAera(bestand, aeras) : WerteberechnungSammlungAera(bestand, aeras);

        //    wertermittlung = BerechnetSummenzeile(wertermittlung);

        //    return wertermittlung;
        //}

        //internal static List<Wertermittlung> BerechnetSummenzeile(List<Wertermittlung> wertermittlung)
        //{
        //    wertermittlung = wertermittlung.Where(a => a.Anzahl != 0).ToList();

        //    Wertermittlung summe = new Wertermittlung { S = 0, SP = 0, SS = 0, SSP = 0, VZ = 0, VZP = 0, STN = 0, STH = 0, PP = 0, Gesamt = 0, Anzahl = 0, Nation = "Gesamt" };
        //    foreach (var item in wertermittlung)
        //    {
        //        summe.S += item.S;
        //        summe.SP += item.SP;
        //        summe.SS += item.SS;
        //        summe.SSP += item.SSP;
        //        summe.VZ += item.VZ;
        //        summe.VZP += item.VZP;
        //        summe.STN += item.STN;
        //        summe.STH += item.STH;
        //        summe.PP += item.PP;
        //        summe.Anzahl += item.Anzahl;
        //        summe.Gesamt += item.Gesamt;
        //    }
        //    wertermittlung.Add(summe);

        //    return wertermittlung;
        //}

        //internal static List<Wertermittlung> WerteberechnungSammlungNation(List<Bestand> bestand, List<Nation> nationen)
        //{
        //    var temp = from b in bestand
        //               group b by b.NationID into g
        //               select new
        //               {
        //                   Nation = "",
        //                   S = g.Sum(x => x.S),
        //                   SP = g.Sum(x => x.SP),
        //                   SS = g.Sum(x => x.SS),
        //                   SSP = g.Sum(x => x.SSP),
        //                   VZ = g.Sum(x => x.VZ),
        //                   VZP = g.Sum(x => x.VZP),
        //                   STN = g.Sum(x => x.STN),
        //                   STH = g.Sum(x => x.STH),
        //                   PP = g.Sum(x => x.PP),
        //                   Anzahl = g.Sum(x => x.S)
        //                       + g.Sum(x => x.SP)
        //                       + g.Sum(x => x.SS)
        //                       + g.Sum(x => x.SSP)
        //                       + g.Sum(x => x.VZ)
        //                       + g.Sum(x => x.VZP)
        //                       + g.Sum(x => x.STN)
        //                       + g.Sum(x => x.STH)
        //                       + g.Sum(x => x.PP),
        //                   Betrag = g.Sum(x => x.Gesamt),
        //                   NationID = g.Key
        //               };

        //    List<Wertermittlung> wertermittlung = new List<Wertermittlung>();


        //    foreach (var item in temp)
        //    {
        //        if (item.NationID != 0)
        //        {
        //            var nation = nationen.FirstOrDefault(x => x.ID == item.NationID);

        //            if (nation != null)
        //            {
        //                Wertermittlung w = new Wertermittlung();
        //                w.S = item.S;
        //                w.SP = item.SP;
        //                w.SS = item.SS;
        //                w.SSP = item.SSP;
        //                w.VZ = item.VZ;
        //                w.VZP = item.VZP;
        //                w.STN = item.STN;
        //                w.STH = item.STH;
        //                w.PP = item.PP;
        //                w.Gesamt = item.Betrag;
        //                w.Anzahl = item.Anzahl;
        //                w.NationID = item.NationID;
        //                w.Nation = nation.Bezeichnung;

        //                wertermittlung.Add(w);
        //            }
        //        }
        //    }

        //    wertermittlung = wertermittlung.OrderBy(x => x.Nation).ToList();

        //    return wertermittlung;
        //}

        //internal static List<Wertermittlung> WerteberechnungSammlungAera(List<Bestand> bestand, List<Aera> aeras)
        //{
        //    var temp = from b in bestand
        //               group b by b.AeraID into g
        //               select new
        //               {
        //                   Nation = "",
        //                   S = g.Sum(x => x.S),
        //                   SP = g.Sum(x => x.SP),
        //                   SS = g.Sum(x => x.SS),
        //                   SSP = g.Sum(x => x.SSP),
        //                   VZ = g.Sum(x => x.VZ),
        //                   VZP = g.Sum(x => x.VZP),
        //                   STN = g.Sum(x => x.STN),
        //                   STH = g.Sum(x => x.STH),
        //                   PP = g.Sum(x => x.PP),
        //                   Anzahl = g.Sum(x => x.S)
        //                       + g.Sum(x => x.SP)
        //                       + g.Sum(x => x.SS)
        //                       + g.Sum(x => x.SSP)
        //                       + g.Sum(x => x.VZ)
        //                       + g.Sum(x => x.VZP)
        //                       + g.Sum(x => x.STN)
        //                       + g.Sum(x => x.STH)
        //                       + g.Sum(x => x.PP),
        //                   Betrag = g.Sum(x => x.Gesamt),
        //                   NationID = g.Key
        //               };

        //    List<Wertermittlung> wertermittlung = new List<Wertermittlung>();


        //    foreach (var item in temp)
        //    {
        //        if (item.NationID != 0)
        //        {
        //            Wertermittlung w = new Wertermittlung();
        //            w.S = item.S;
        //            w.SP = item.SP;
        //            w.SS = item.SS;
        //            w.SSP = item.SSP;
        //            w.VZ = item.VZ;
        //            w.VZP = item.VZP;
        //            w.STN = item.STN;
        //            w.STH = item.STH;
        //            w.PP = item.PP;
        //            w.Gesamt = item.Betrag;
        //            w.Anzahl = item.Anzahl;
        //            w.Nation = aeras.FirstOrDefault(x => x.ID == item.NationID).Bezeichnung;
        //            w.NationID = item.NationID;

        //            wertermittlung.Add(w);
        //        }
        //    }

        //    wertermittlung = wertermittlung.OrderBy(x => x.Nation).ToList();

        //    return wertermittlung;
        //}

        //internal static List<Wertermittlung> WerteberechnungDoublettenNation(List<Bestand> bestand, List<Nation> nationen)
        //{
        //    var temp = from b in bestand
        //               group b by b.NationID into g
        //               select new
        //               {
        //                   Nation = "",
        //                   S = g.Sum(x => x.DS),
        //                   SP = g.Sum(x => x.DSP),
        //                   SS = g.Sum(x => x.DSS),
        //                   SSP = g.Sum(x => x.DSSP),
        //                   VZ = g.Sum(x => x.DVZ),
        //                   VZP = g.Sum(x => x.DVZP),
        //                   STN = g.Sum(x => x.DSTN),
        //                   STH = g.Sum(x => x.DSTH),
        //                   PP = g.Sum(x => x.DPP),
        //                   Anzahl = g.Sum(x => x.DS)
        //                       + g.Sum(x => x.DSP)
        //                       + g.Sum(x => x.DSS)
        //                       + g.Sum(x => x.DSSP)
        //                       + g.Sum(x => x.DVZ)
        //                       + g.Sum(x => x.DVZP)
        //                       + g.Sum(x => x.DSTN)
        //                       + g.Sum(x => x.DSTH)
        //                       + g.Sum(x => x.DPP),
        //                   Betrag = g.Sum(x => x.Gesamt),
        //                   NationID = g.Key
        //               };

        //    List<Wertermittlung> wertermittlung = new List<Wertermittlung>();


        //    foreach (var item in temp)
        //    {
        //        if (item.NationID != 0)
        //        {
        //            Wertermittlung w = new Wertermittlung();
        //            w.S = item.S;
        //            w.SP = item.SP;
        //            w.SS = item.SS;
        //            w.SSP = item.SSP;
        //            w.VZ = item.VZ;
        //            w.VZP = item.VZP;
        //            w.STN = item.STN;
        //            w.STH = item.STH;
        //            w.PP = item.PP;
        //            w.Gesamt = item.Betrag;
        //            w.Anzahl = item.Anzahl;
        //            w.Nation = nationen.FirstOrDefault(x => x.ID == item.NationID).Bezeichnung;
        //            w.NationID = item.NationID;

        //            wertermittlung.Add(w);
        //        }
        //    }

        //    wertermittlung = wertermittlung.OrderBy(x => x.Nation).ToList();

        //    return wertermittlung;
        //}

        //internal static List<Wertermittlung> WerteberechnungDoublettenAera(List<Bestand> bestand, List<Aera> aeras)
        //{
        //    var temp = from b in bestand
        //               group b by b.AeraID into g
        //               select new
        //               {
        //                   Nation = "",
        //                   S = g.Sum(x => x.DS),
        //                   SP = g.Sum(x => x.DSP),
        //                   SS = g.Sum(x => x.DSS),
        //                   SSP = g.Sum(x => x.DSSP),
        //                   VZ = g.Sum(x => x.DVZ),
        //                   VZP = g.Sum(x => x.DVZP),
        //                   STN = g.Sum(x => x.DSTN),
        //                   STH = g.Sum(x => x.DSTH),
        //                   PP = g.Sum(x => x.DPP),
        //                   Anzahl = g.Sum(x => x.DS)
        //                       + g.Sum(x => x.DSP)
        //                       + g.Sum(x => x.DSS)
        //                       + g.Sum(x => x.DSSP)
        //                       + g.Sum(x => x.DVZ)
        //                       + g.Sum(x => x.DVZP)
        //                       + g.Sum(x => x.DSTN)
        //                       + g.Sum(x => x.DSTH)
        //                       + g.Sum(x => x.DPP),
        //                   Betrag = g.Sum(x => x.Gesamt),
        //                   NationID = g.Key
        //               };

        //    List<Wertermittlung> wertermittlung = new List<Wertermittlung>();


        //    foreach (var item in temp)
        //    {
        //        if (item.NationID != 0)
        //        {
        //            Wertermittlung w = new Wertermittlung();
        //            w.S = item.S;
        //            w.SP = item.SP;
        //            w.SS = item.SS;
        //            w.SSP = item.SSP;
        //            w.VZ = item.VZ;
        //            w.VZP = item.VZP;
        //            w.STN = item.STN;
        //            w.STH = item.STH;
        //            w.PP = item.PP;
        //            w.Gesamt = item.Betrag;
        //            w.Anzahl = item.Anzahl;
        //            w.Nation = aeras.FirstOrDefault(x => x.ID == item.NationID).Bezeichnung;
        //            w.NationID = item.NationID;

        //            wertermittlung.Add(w);
        //        }
        //    }

        //    wertermittlung = wertermittlung.OrderBy(x => x.Nation).ToList();

        //    return wertermittlung;
        //}
    }
}
