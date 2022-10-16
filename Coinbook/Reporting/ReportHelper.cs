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
using Coinbook.Helper;
using Coinbook.Enumerations;

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
            dlgSave.Title = LanguageHelper.Localization.GetTranslation("Keys", "msgSavePDF");
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
                        MessageBoxAdv.Show(form, e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgSaved"), dlgSave.FileName);
                MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            string page = LanguageHelper.Localization.GetTranslation(formName, "Seite");
            string from = LanguageHelper.Localization.GetTranslation(formName, "von");
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
            dlgSave.Title = LanguageHelper.Localization.GetTranslation("Keys", "msgSaveExcel");
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

                string text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgSaved"), dlgSave.FileName);
                MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            dlgSave.Title = LanguageHelper.Localization.GetTranslation("Keys", "msgSaveCSV");
            dlgSave.FileName = "Untitled";

            if (dlgSave.ShowDialog() != DialogResult.Cancel)
            {
                Application.DoEvents(); 
                CoinbookHelper.WriteDataTable(dlgSave.FileName, dt, ';');
                string text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgSaved"),dlgSave.FileName);
                MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void ReportToCSV<T>(List<T> list)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();

            if (list == null || list.Count == 0) return;

            dlgSave.Filter = "CSV-Dokumente|*.csv";
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".csv"; // Default file extension
            dlgSave.Title = LanguageHelper.Localization.GetTranslation("Keys", "msgSaveCSV");
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

                string text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgSaved"), dlgSave.FileName);
                MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            e.Graphics.DrawString(string.Format(LanguageHelper.Localization.GetTranslation(formName, "Seite") + " {0} " + LanguageHelper.Localization.GetTranslation(formName, "von") + " {1}",
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

        public static DataTable GetReportNations(List<Report> reportListe, List<Nation> nations)
        {
            DataTable dt = new DataTable("tblNation");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));

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

            return dt;
        }

        public static DataTable GetReportNations(List<Report2> reportListe, List<Nation> nations)
        {
            DataTable dt = new DataTable("tblNation");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));

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

            return dt;
        }

        private static DataRow nationAddRow(Nation nation, DataRow row)
        {
            row["NationID"] = nation.ID;
            row["Bezeichnung"] = nation.Bezeichnung;

            return row;
        }

        public static DataTable GetReportÄras(List<Report> reportListe, List<Nation> nations)
        {
            DataTable dt = new DataTable("tblÄra");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));
            dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

            foreach (var item in nations)
            {
                var aeras = DatabaseHelper.LiteDatabase.ReadAeras(item.Key);
                foreach (var aera in aeras)
                    if (reportListe.FirstOrDefault(x => x.AeraID == aera.ID) != null)
                        dt.Rows.Add(aeraAddRow(aera, dt.NewRow()));
            }

            return dt;
        }

        public static DataTable GetReportÄras(List<Report2> reportListe, List<Nation> nations)
        {
            DataTable dt = new DataTable("tblÄra");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("NationID", typeof(int)));
            dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

            foreach (var item in nations)
            {
                var aeras = DatabaseHelper.LiteDatabase.ReadAeras(item.Key);
                foreach (var aera in aeras)
                    if (reportListe.FirstOrDefault(x => x.AeraID == aera.ID) != null)
                        dt.Rows.Add(aeraAddRow(aera, dt.NewRow()));
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

        public static DataTable GetReportGebiete(List<Report> reportListe, List<Nation> nations)
        {
            DataTable dt = new DataTable("tblGebiet");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("GebietID", typeof(int)));
            dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

            foreach (var item in nations)
            {
                var regions = DatabaseHelper.LiteDatabase.ReadRegions(item.Key);
                foreach (var region in regions)
                    if (reportListe.FirstOrDefault(x => x.GebietID == region.ID) != null)
                        dt.Rows.Add(gebietAddRow(region, dt.NewRow()));
            }

            return dt;
        }

        public static DataTable GetReportGebiete(List<Report2> reportListe, List<Nation> nations)
        {
            DataTable dt = new DataTable("tblGebiet");
            dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
            dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
            dt.Columns.Add(new DataColumn("GebietID", typeof(int)));
            dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

            foreach (var item in nations)
            {
                var regions = DatabaseHelper.LiteDatabase.ReadRegions(item.Key);
                foreach (var region in regions)
                    if (reportListe.FirstOrDefault(x => x.GebietID == region.ID) != null)
                        dt.Rows.Add(gebietAddRow(region, dt.NewRow()));
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
            dt.Columns.Add(new DataColumn("Farbe", typeof(object)));

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
                row["Farbe"] = item.Farbe;

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
            dt.Columns.Add(new DataColumn("Farbe", typeof(string)));

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
                row["Erhaltung"] = item.Erhaltung;
                row["Motiv"] = item.Motiv;
                row["Anzahl"] = item.Anzahl;
                row["Preis"] = item.Preis;
                row["KatNr"] = item.KatNr;
                row["Gesamt"] = item.Gesamt; // != 0 ? string.Format("{0:###,###.00}", item.Gesamt) : string.Empty;
                row["Farbe"] = item.Farbe;

                dt.Rows.Add(row);
            }

            return dt;
        }

        public static Report2 CoinSplitToErhaltungsgrad(Bestand item, string erhaltungsgrad, enmReportTyp typ)
        {
            Report2 r = new Report2();
            r.NationID = item.NationID;
            r.AeraID = item.AeraID;
            r.GebietID = item.GebietID;
            r.Guid = item.Guid;
            r.Jahrgang = item.Jahrgang;
            r.Muenzz = item.Muenzzeichen;
            r.Nominal = item.Nominal;
            r.Waehrung = item.Waehrung;
            r.KatNr = item.KatNr;
            r.Motiv = item.Motiv == null ? "" : item.Motiv;
            r.Farbe = item.Farbe;

            switch (erhaltungsgrad)
            {
                case "S":
                    r.Preis = item.PS;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.S : item.DS;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[0].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.S) == enmColorFlag.S ? enmColorFlag.S : enmColorFlag.None;
                    break;

                case "SP":
                    r.Preis = item.PSP;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.SP : item.DSP;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[1].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.SP) == enmColorFlag.SP ? enmColorFlag.SP : enmColorFlag.None;
                    break;

                case "SS":
                    r.Preis = item.PSS;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.SS : item.DSS;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[2].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.SS) == enmColorFlag.SS ? enmColorFlag.SS : enmColorFlag.None;
                    break;

                case "SSP":
                    r.Preis = item.PSSP;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.SSP : item.DSSP;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[3].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.SSP) == enmColorFlag.SSP ? enmColorFlag.SSP : enmColorFlag.None;
                    break;

                case "VZ":
                    r.Preis = item.PVZ;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.VZ : item.DVZ;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[4].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.VZ) == enmColorFlag.VZ ? enmColorFlag.VZ : enmColorFlag.None;
                    break;

                case "VZP":
                    r.Preis = item.PVZP;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.VZP : item.DVZP;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[5].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.VZP) == enmColorFlag.VZP ? enmColorFlag.VZP : enmColorFlag.None;
                    break;

                case "STN":
                    r.Preis = item.PSTN;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.STN : item.DSTN;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[6].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.STN) == enmColorFlag.STN ? enmColorFlag.STN : enmColorFlag.None;
                    break;

                case "STH":
                    r.Preis = item.PSTH;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.STH : item.DSTH;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[7].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.STH) == enmColorFlag.STH ? enmColorFlag.STH : enmColorFlag.None;
                    break;

                case "PP":
                    r.Preis = item.PPP;
                    r.Anzahl = typ == enmReportTyp.ReportSammlung ? item.PP : item.DPP;
                    r.Erhaltung = CoinbookHelper.Erhaltungsgrade[8].Erhaltung;
                    r.Farbe = (item.Farbe & enmColorFlag.PP) == enmColorFlag.PP ? enmColorFlag.PP : enmColorFlag.None;
                    break;
            }

            r.Gesamt = r.Anzahl * r.Preis;
            return r;
        }

        public static void SetRelationsFehlliste(GridGroupingControl grdAnzeige, List<Nation> nations, List<Aera> aeras, List<Gebiet> gebiete, int nationID, int aeraID, int regionID, List<Fehlliste> fehlliste)
        {
            GridRelationDescriptor aeraRelationDescriptor;
            GridRelationDescriptor gebietRelationDescriptor;
            int result = 0;

            if (aeraID != 0)
                result = aeras.RemoveAll(x => x.ID != aeraID);

            if (regionID != 0)
                result = gebiete.RemoveAll(x => x.ID != regionID);

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
    }
}
