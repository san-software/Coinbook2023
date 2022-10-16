using Coinbook.Helper;
using Coinbook.Lokalisierung;
using Coinbook.Model;
using LiteDB.Database;
using SAN.FTP;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.Drawing;
using Syncfusion.GridHelperClasses;
using Syncfusion.OfficeChartToImageConverter;
using Syncfusion.Pdf;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.PdfViewer;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Coinbook.Modulverwaltung
{
    public partial class frmOrderCloudBackup : Form
    {
        WordDocument document;
        string bestellnummer = BestellungHelper.Bestellnummer;
        string artikelBezeichnung;
        DateTime von;
        DateTime bis;
        decimal artikelPreis;
        decimal mwst;
        decimal steuer;
        private string updatePath = string.Empty;
        private Model.Settings settings;
        private string mandat = "";

        public frmOrderCloudBackup(Model.Settings settings)
        {
            InitializeComponent();

            this.settings = settings;

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook");
            LanguageHelper.CreateLocalization(resourcePath);

            updatePath = Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Updater");

            artikelPreis = 12;
            mwst = 19;
            steuer = artikelPreis / (100 + mwst) * mwst;

            von = Convert.ToDateTime("01" + DateTime.Now.ToShortDateString().Substring(2)).AddMonths(1);
            bis = von.AddMonths(12).AddDays(-1);
            artikelBezeichnung = $"Jahres-Abonnement CloudBackup vom {von.ToShortDateString()} bis {bis.ToShortDateString()}";

            string temp = "Das Abonnement beginnt zum Ersten des nächsten Monats, läuft genau ein Jahr und verlängert sich automatisch um ein Jahr, "
                + "falls es nicht 30 Tage vor Ablauf gekündigt wird. "
                + $"Die Laufzeit bei jetziger Bestellung geht von {von.ToShortDateString()} bis {bis.ToShortDateString()}.";

            lblAbo1.Text = temp;
            lblAbo2.Text = $"Die Jahresgebühr beträgt {artikelPreis:##,##} € incl. {mwst} % Mehrwertsteuer und wird vor Beginn des Abonnements, "
                + "bzw. bei der Verlängerung von Ihrem Konto eingezogen.";
            lblAbo3.Text = "Bitte füllen Sie folgende Daten für das Lastschrift Mandat aus.";

            var bank = DatabaseHelper.LiteDatabase.GetBank();

            txtBank.Text = bank?.Kreditinstitut == null ? string.Empty : bank.Kreditinstitut;
            txtBIC.Text = bank?.BIC == null ? string.Empty : bank.BIC;
            txtIBAN.Text = bank?.IBAN == null ? string.Empty : bank.IBAN;

            GridformatWert();
            createBestellung();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            document = new WordDocument("LastschriftMandat.docx", FormatType.Docx);

            ReplaceField("Mandat", bestellnummer);
            ReplaceField("Name", settings.Vorname + " " + settings.Nachname);
            ReplaceField("Strasse", settings.Strasse);
            ReplaceField("Ort", settings.Ort);
            ReplaceField("Bank", txtBank.Text);
            ReplaceField("BIC", txtBIC.Text);
            ReplaceField("IBAN", txtIBAN.Text);
            ReplaceField("PLZ", settings.PLZ);
            ReplaceField("Datum", DateTime.Now.ToShortDateString());

            Bank bank = new Bank() { BIC = txtBIC.Text, IBAN = txtIBAN.Text, Kreditinstitut = txtBank.Text };

            DatabaseHelper.LiteDatabase.SaveBank(bank);

            CreateXML();
            createPDF();

            if (printBestellung())
            {
                printMandat();
                Close();
                Upload();
            }
            else
                Close();

           
        }

        public void ReplaceField(string field, string value)
        {
            TextSelection textSelection;

            do
            {
                textSelection = document.Find("<<" + field + ">>", false, true);  //Finds the first occurrence of a particular text in the document
                if (textSelection != null)
                {
                    WTextRange textRange = textSelection.GetAsOneRange();   //Gets the found text as single text range
                    textRange.Text = value; //Modifies the text
                }
            } while (textSelection != null);
        }

        private void GridformatWert()
        {
            grdOrder.ColWidths.SetSize(1, 50);
            grdOrder.ColWidths.SetSize(3, 50);
            grdOrder.ColWidths.SetSize(3, 400);
            grdOrder.ColWidths.SetSize(5, 60);

            grdOrder.ColStyles["Preis"].Format = "###,##0.00";

            grdOrder.ColCount = 4;
            grdOrder.Cols.Hidden[0] = true;
            grdOrder.Rows.Hidden[0] = true;

            GridStyleInfo style;
            style = new GridStyleInfo();
            style.HorizontalAlignment = GridHorizontalAlignment.Left;
            grdOrder.ColStyles[2] = style;
            grdOrder.ColStyles[3] = style;
            grdOrder.ColStyles[4] = style;

            style = new GridStyleInfo();
            style.HorizontalAlignment = GridHorizontalAlignment.Right;
            style.Format = "##0.00";
            grdOrder.ColStyles[4] = style;

            grdOrder.Invalidate();
            Application.DoEvents();
        }

        private void createBestellung()
        {
            int colNummer = 2;
            int colBezeichnung = 3;
            int colPreis = 4;

            grdOrder.ClearCells(GridRangeInfo.Table(), true);
            grdOrder.RowCount = 1000;
            int row = 0;

            // Set MergeCells direction for the GridControl.
            grdOrder.TableStyle.MergeCell = GridMergeCellDirection.ColumnsInRow;

            // Set merge cells behavior for the Grid.
            grdOrder.Model.Options.MergeCellsMode = GridMergeCellsMode.OnDemandCalculation | GridMergeCellsMode.MergeColumnsInRow;
            grdOrder.Model.Options.MergeCellsLayout = GridMergeCellsLayout.Grid;

            grdOrder.Model.Options.DefaultGridBorderStyle = GridBorderStyle.None;

            row++;
            grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder2", "Bestellnummer") + bestellnummer;
            grdOrder[row, colBezeichnung].Font.Bold = true;
            grdOrder[row, colBezeichnung].Font.Size = 10;
            grdOrder[row, colBezeichnung].HorizontalAlignment = GridHorizontalAlignment.Center;
            row++;

            row++;
            grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation(Name, "Adresse");
            grdOrder[row, colBezeichnung].Font.Bold = true;

            row++;
            grdOrder[row, colBezeichnung].CellValue = settings.Vorname + " " + settings.Nachname;

            row++;
            grdOrder[row, colBezeichnung].CellValue = settings.Strasse;

            row++;
            grdOrder[row, colBezeichnung].CellValue = settings.Land + "-" + settings.PLZ + " " + settings.Ort;

            row++;
            grdOrder[row, colBezeichnung].CellValue = settings.Mail;

            row++;
            row++;
            grdOrder[row, colNummer].CellValue = "Nr.";
            grdOrder[row, colBezeichnung].CellValue = "Artikel";
            grdOrder[row, colPreis].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "colPreisUpdate");

            grdOrder[row, colNummer].Font.Bold = true;
            grdOrder[row, colBezeichnung].Font.Bold = true;
            grdOrder[row, colPreis].Font.Bold = true;

            grdOrder[row, colNummer].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);
            grdOrder[row, colBezeichnung].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);
            grdOrder[row, colPreis].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);

            row++;
            grdOrder[row, colNummer].CellValue = "CBA0001";
            grdOrder[row, colBezeichnung].CellValue = artikelBezeichnung;
            grdOrder[row, colPreis].CellValue =string.Format("{0:##0.00 €}", artikelPreis);

            row++;
            row++;
            grdOrder[row, colBezeichnung].CellValue = 
                string.Format(LanguageHelper.Localization.GetTranslation("frmOrder2", "Mehrwertsteuer"), string.Format("{0:#0.0}", mwst), string.Format("{0:###0.00}", steuer));

            row++;
            row++;
            grdOrder[row, colBezeichnung].CellValue = "Der Betrag wird per Lastschrift eingezogen";

            row++;
            row++;
            grdOrder[row, colBezeichnung].CellValue = "Die Lieferung des zugehörigen Programmteils erfolgt per Download.";

            grdOrder.RowCount = row;
        }

        public string CreateXML()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("Element", "Root", String.Empty);
            XmlNode adresse = document.CreateElement(String.Empty, "Adresse", String.Empty);
            XmlNode fuss = document.CreateElement(String.Empty, "Fuss", String.Empty);
            XmlNode module = document.CreateElement(String.Empty, "Module", String.Empty);

            document.AppendChild(root);
            root.AppendChild(adresse);
            root.AppendChild(module);
            root.AppendChild(fuss);

            XmlAttribute vorname = document.CreateAttribute("Vorname");
            vorname.Value = settings.Vorname;
            adresse.Attributes.Append(vorname);

            XmlAttribute nachname = document.CreateAttribute("Nachname");
            nachname.Value = settings.Nachname;
            adresse.Attributes.Append(nachname);

            XmlAttribute plz = document.CreateAttribute("PLZ");
            plz.Value = settings.PLZ;
            adresse.Attributes.Append(plz);

            XmlAttribute ort = document.CreateAttribute("Ort");
            ort.Value = settings.Ort;
            adresse.Attributes.Append(ort);

            XmlAttribute land = document.CreateAttribute("Land");
            land.Value = settings.Land;
            adresse.Attributes.Append(land);

            XmlAttribute strasse = document.CreateAttribute("Strasse");
            strasse.Value = settings.Strasse;
            adresse.Attributes.Append(strasse);

            XmlAttribute bestellnr = document.CreateAttribute("Bestellnummer");
            bestellnr.Value = bestellnummer;
            adresse.Attributes.Append(bestellnr);

            XmlAttribute email = document.CreateAttribute("Email");
            email.Value = settings.Mail;
            adresse.Attributes.Append(email);

            XmlAttribute datum = document.CreateAttribute("Datum");
            datum.Value = DateTime.Now.ToShortDateString();
            adresse.Attributes.Append(datum);

            XmlAttribute lizenzkey = document.CreateAttribute("Lizenzkey");
            lizenzkey.Value = settings.Lizenzkey;
            adresse.Attributes.Append(lizenzkey);

            XmlAttribute zahlung = document.CreateAttribute("Zahlung");
            zahlung.Value = "Zahlung per Abbuchung";
            adresse.Attributes.Append(zahlung);

            XmlAttribute mandat1 = document.CreateAttribute("Mandat");
            lizenzkey.Value = mandat;
            adresse.Attributes.Append(mandat1);

            XmlNode modul = document.CreateElement(String.Empty, "Modul", String.Empty);
            module.AppendChild(modul);

            XmlAttribute artikel = document.CreateAttribute("Artikel");
            artikel.Value = "CBA0001";
            modul.Attributes.Append(artikel);

            XmlAttribute bezeichnung = document.CreateAttribute("Bezeichnung");
            bezeichnung.Value = artikelBezeichnung;
            modul.Attributes.Append(bezeichnung);

            XmlAttribute version = document.CreateAttribute("Version");
            version.Value = von.ToShortDateString()+"-"+bis.ToShortDateString();
            modul.Attributes.Append(version);

            XmlAttribute preis = document.CreateAttribute("Preis");
            preis.Value = artikelPreis.ToString();
            modul.Attributes.Append(preis);

            string xmlfile = Path.Combine(updatePath, bestellnummer);

            document.Save(xmlfile + ".xml");

            return xmlfile;
        }

        private void createPDF()
		{
            //Initializes the ChartToImageConverter for converting charts during Word to pdf conversion
            document.ChartToImageConverter = new ChartToImageConverter();
            DocToPDFConverter converter = new DocToPDFConverter(); //Creates an instance of the DocToPDFConverter
            PdfDocument pdfDocument = converter.ConvertToPDF(document); //Converts Word document into PDF document
            string file = Path.Combine(updatePath, bestellnummer);
            pdfDocument.Save(file + ".pdf");

            pdfDocument.Close(false);
            document.Close();
        }

        private bool printBestellung()
        {
            bool result = true;
            GridPrintDocumentAdv pd = new GridPrintDocumentAdv(grdOrder);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;

            pd.DefaultPageSettings.Landscape = false;
            pd.DefaultPageSettings.Margins.Left = 70;
            pd.DefaultPageSettings.Margins.Right = 50;
            pd.DefaultPageSettings.Margins.Top = 25;
            pd.DefaultPageSettings.Margins.Bottom = 50;
            pd.ScaleColumnsToFitPage = true;
            pd.PagesToFit = 1;
            pd.DocumentName = Text;

            pd.HeaderHeight = 40;
            pd.HeaderPrintStyleInfo.Text = LanguageHelper.Localization.GetTranslation(Name, "itemHeader") + DateTime.Now.ToShortDateString(); ;
            pd.HeaderPrintStyleInfo.Font.Bold = true;
            pd.HeaderPrintStyleInfo.Font.Size = 20;
            pd.HeaderPrintStyleInfo.HorizontalAlignment = GridHorizontalAlignment.Left;

            pd.FooterHeight = 30;

            if (printDialog.ShowDialog() == DialogResult.OK)
                pd.Print();
            else
                result = false;

            return result;
        }

        private void printMandat()
        {
            string file = Path.Combine(updatePath, bestellnummer)+".pdf";
            pdfViewer.Load(file);
            pdfViewer.Print(false);
        }

        private void Upload()
        {
            string file = Path.Combine(updatePath, bestellnummer);

            FTPClass ftp = new FTPClass();
            if (ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1"))
            {
                ftp.SetWorkingDirectory("Abo");

                ftp.Upload(file + ".xml", Path.GetFileName(file) + ".xml");
                ftp.Upload(file + ".pdf", Path.GetFileName(file) + ".pdf");
                ftp.Disconnect();

                MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("frmOrder2", "msgOrderSent"));
            }
            else
                MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("frmOrder2", "msgOrderNotSent"));

            File.Delete(file + ".xml");
            File.Delete(file + ".pdf");
        }
    }
}
