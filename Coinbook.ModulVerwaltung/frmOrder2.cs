using Coinbook.Enumerations;
using Coinbook.Helper;
using Coinbook.Lokalisierung;
using Coinbook.Model;
using LiteDB.Database;
using SAN.FTP;
using Syncfusion.Drawing;
using Syncfusion.GridHelperClasses;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Coinbook.Modulverwaltung
{
    public partial class frmOrder2 : Form
	{
		int row = 0;
		GridStyleInfo boldStyle = new GridStyleInfo();
		private string updatePath = string.Empty;
		private Settings settings;

		public frmOrder2()
		{
			InitializeComponent();

			updatePath = Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Updater");

			GridformatWert();
			LanguageHelper.Localization.UpdateModul(this);
		}

		private void GridformatWert()
		{
			grdOrder.ColWidths.SetSize(1, 50);
			grdOrder.ColWidths.SetSize(3, 50);
			grdOrder.ColWidths.SetSize(3, 400);
			grdOrder.ColWidths.SetSize(4, 90);
			grdOrder.ColWidths.SetSize(5, 60);

			grdOrder.ColStyles["Gesamt"].Format = "###,##0.00";
			grdOrder.ColStyles["Anzahl"].Format = "###";

			grdOrder.ColCount = 5;
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
			grdOrder.ColStyles[5] = style;

			grdOrder.Invalidate();
			Application.DoEvents();
		}

		private void btnOrder_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			grdOrder.Properties.PrintFrame = true;
			grdOrder.PrintHorizontalLines = false;
			grdOrder.PrintVerticalLines = false;

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
            {
                pd.Print();

                Cursor = Cursors.Default;

                string file = CreateXML();
                createPDF();

                FTPClass ftp = new FTPClass();
                if (ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1"))
                {
                    ftp.SetWorkingDirectory("Bestellung");

                    ftp.Upload(file + ".xml", Path.GetFileName(file) + ".xml");
                    ftp.Upload(file + ".pdf", Path.GetFileName(file) + ".pdf");
                    ftp.Disconnect();

                    if (Zahlung == enmZahlung.Paypal)
                        callPaypal();

                    MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("frmOrder2", "msgOrderSent"));
                }
                else
                    MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("frmOrder2", "msgOrderNotSent"));

                File.Delete(file + ".xml");
                File.Delete(file + ".pdf");

                Cancel = true;
                Hide();
            }

            Cursor = Cursors.Default;
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			Cancel = false;
			Close();
		}

		public bool Cancel { get; set; }

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Cancel = true;
			Close();
		}

		public string Bestellnummer { get; set; }
		public decimal UpdateSumme { get; set; }
		public decimal NeuSumme { get; set; }
		public decimal Rabattsatz { get; set; }
		public decimal Rabatt { get; set; }
		public decimal GesamtSumme { get; set; }
		public decimal Mwst { get; set; }
		public decimal Steuer { get; set; }
		public enmLieferung Lieferung { get; set; }
		public enmZahlung Zahlung { get; set; }
		public List<Artikel> ArtikelListe { get; set; }
		public decimal Versand { get; set; }

		public new void ShowDialog()
		{
			Lite lite = new Lite();
			settings = lite.ReadSettings();
			createBestellung();

			base.ShowDialog();
		}

		private void createBestellung()
		{
			int colFirst = 1;
			int colNummer = 2;
			int colBezeichnung = 3;
			int colVersion = 4;
			int colPreis = 5;

			grdOrder.ClearCells(GridRangeInfo.Table(), true);
			grdOrder.RowCount = 1000;
			row = 0;

			// Set MergeCells direction for the GridControl.
			grdOrder.TableStyle.MergeCell = GridMergeCellDirection.ColumnsInRow;

			// Set merge cells behavior for the Grid.
			grdOrder.Model.Options.MergeCellsMode = GridMergeCellsMode.OnDemandCalculation | GridMergeCellsMode.MergeColumnsInRow;
			grdOrder.Model.Options.MergeCellsLayout = GridMergeCellsLayout.Grid;

			grdOrder.Model.Options.DefaultGridBorderStyle = GridBorderStyle.None;

			row++;
			grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder2", "Bestellnummer") + " " + Bestellnummer;
			grdOrder[row, colBezeichnung].Font.Bold = true;
			grdOrder[row, colBezeichnung].Font.Size = 10;
			grdOrder[row, colBezeichnung].HorizontalAlignment = GridHorizontalAlignment.Center;
			row++;

			row++;
			grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "Adresse","Adresse");
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
			grdOrder[row, colBezeichnung].CellValue = "Modul";
			grdOrder[row, colVersion].CellValue = "Version";
			grdOrder[row, colPreis].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "colPreisUpdate");

			grdOrder[row, colNummer].Font.Bold = true;
			grdOrder[row, colBezeichnung].Font.Bold = true;
			grdOrder[row, colVersion].Font.Bold = true;
			grdOrder[row, colPreis].Font.Bold = true;

			grdOrder[row, colNummer].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);
			grdOrder[row, colBezeichnung].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);
			grdOrder[row, colVersion].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);
			grdOrder[row, colPreis].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);


			for (int i = 0; i < ArtikelListe.Count; i++)
			{
				row++;
				grdOrder[row, colNummer].CellValue = ArtikelListe[i].Artikelnummer;
				grdOrder[row, colBezeichnung].CellValue = ArtikelListe[i].Bezeichnung;
				grdOrder[row, colVersion].CellValue = ArtikelListe[i].Version;
				grdOrder[row, colPreis].CellValue = ArtikelListe[i].Preis;
			}

			if (UpdateSumme != 0)
			{
				row++;
				grdOrder[row, colNummer].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "lblUpdateSumme");
				grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "lblUpdateSumme");
				grdOrder[row, colPreis].CellValue = string.Format("{0:###0.00 €}", UpdateSumme);
			}

			if (NeuSumme != 0)
			{
				row++;
				grdOrder[row, colNummer].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "lblNeuSumme");
				grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "lblNeuSumme");
				grdOrder[row, colPreis].CellValue = string.Format("{0:###0.00 €}", NeuSumme);
			}

			if (Rabatt != 0)
			{
				row++;
				grdOrder[row, colNummer].CellValue = string.Format(LanguageHelper.Localization.GetTranslation("frmOrder", "Rabatt"), Rabattsatz);
				grdOrder[row, colBezeichnung].CellValue = string.Format(LanguageHelper.Localization.GetTranslation("frmOrder", "Rabatt"), Rabattsatz);
				grdOrder[row, colPreis].CellValue = string.Format("{0:###0.00 €}", -Rabatt);
			}

			if (Lieferung == enmLieferung.Versand)
			{
				row++;
				grdOrder[row, colNummer].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "optVersand");
				grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "optVersand");
				grdOrder[row, colPreis].CellValue = string.Format("{0:###0.00 €}", Versand);
			}

			row++;
			grdOrder[row, colNummer].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "lblGesamtSumme");
			grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "lblGesamtSumme");
			grdOrder[row, colPreis].CellValue = string.Format("{0:###0.00 €}", GesamtSumme);
			grdOrder[row, colBezeichnung].Font.Bold = true;
			grdOrder[row, colPreis].Font.Bold = true;
			grdOrder[row, colNummer].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);
			grdOrder[row, colBezeichnung].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);
			grdOrder[row, colVersion].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);
			grdOrder[row, colPreis].Interior = new BrushInfo(GradientStyle.Horizontal, Color.Silver, Color.Silver);

			row++;
			row++;
			grdOrder[row, colBezeichnung].CellValue = string.Format(LanguageHelper.Localization.GetTranslation("frmOrder2", "Mehrwertsteuer"), string.Format("{0:#0.0}", Mwst), string.Format("{0:###0.00}", Steuer));
			grdOrder[row, colNummer].CellValue = string.Format(LanguageHelper.Localization.GetTranslation("frmOrder2", "Mehrwertsteuer"), string.Format("{0:#0.0}", Mwst), string.Format("{0:###0.00}", Steuer));

			if (Lieferung == enmLieferung.Download)
			{
				row++;
				grdOrder[row, colNummer].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "optDownload");
				grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "optDownload");
			}

			switch (Zahlung)
			{
				case enmZahlung.Paypal:
					row++;
					grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder2", "optPaypal");
					break;

				case enmZahlung.Vorkasse:
					row++;
					grdOrder[row, colNummer].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "Überweisung");
					grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "Überweisung");

					row++;
					grdOrder[row, colBezeichnung].CellValue = "Coinbook Verlag";

					row++;
					grdOrder[row, colBezeichnung].CellValue = "Bank : Sparkasse Werra-Meißner";
					row++;
					grdOrder[row, colBezeichnung].CellValue = "IBAN : DE15 5225 0030 0000 0551 78";
					row++;
					grdOrder[row, colBezeichnung].CellValue = "BIC : HELADEF1ESW";

					row++;
					row++;
					grdOrder[row, colNummer].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "Verwendungszweck");
					grdOrder[row, colBezeichnung].CellValue = LanguageHelper.Localization.GetTranslation("frmOrder", "Verwendungszweck");
					break;
			}

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

			XmlAttribute bestellnummer = document.CreateAttribute("Bestellnummer");
			bestellnummer.Value = Bestellnummer;
			adresse.Attributes.Append(bestellnummer);

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
			zahlung.Value = Zahlung.ToString();
			adresse.Attributes.Append(zahlung);

			for (int i = 0; i < ArtikelListe.Count; i++)
			{
				XmlNode modul = document.CreateElement(String.Empty, "Modul", String.Empty);
				module.AppendChild(modul);

				XmlAttribute artikel = document.CreateAttribute("Artikel");
				artikel.Value = ArtikelListe[i].Artikelnummer;
				modul.Attributes.Append(artikel);

				XmlAttribute bezeichnung = document.CreateAttribute("Bezeichnung");
				bezeichnung.Value = ArtikelListe[i].Bezeichnung;
				modul.Attributes.Append(bezeichnung);

				XmlAttribute version = document.CreateAttribute("Version");
				version.Value = ArtikelListe[i].Version;
				modul.Attributes.Append(version);

				XmlAttribute preis = document.CreateAttribute("Preis");
				preis.Value = ArtikelListe[i].Preis;
				modul.Attributes.Append(preis);
			}

			XmlAttribute attribut = document.CreateAttribute("UpdateSumme");
			attribut.Value = UpdateSumme.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("NeuSumme");
			attribut.Value = NeuSumme.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("Rabattsatz");
			attribut.Value = Rabattsatz.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("Rabatt");
			attribut.Value = Rabatt.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("GesamtSumme");
			attribut.Value = GesamtSumme.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("Mwst");
			attribut.Value = Mwst.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("Steuer");
			attribut.Value = Steuer.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("Versand");
			attribut.Value = Versand.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("Lieferung");
			attribut.Value = Lieferung.ToString();
			fuss.Attributes.Append(attribut);

			attribut = document.CreateAttribute("Bemerkung");
			attribut.Value = txtBemerkung.Text;
			fuss.Attributes.Append(attribut);

			string xmlfile = Path.Combine(updatePath, Bestellnummer);

			document.Save(xmlfile + ".xml");

			return xmlfile;
		}

		private void createPDF()
		{
			grdOrder.PrintHorizontalLines = false;
			grdOrder.PrintVerticalLines = false;

			GridPDFConverter pdfConverter = new GridPDFConverter();
			PdfDocument pdfdoc = new PdfDocument();
			PdfLayoutFormat format = new PdfLayoutFormat();

			pdfConverter.ShowHeader = true;
			pdfConverter.ShowFooter = true;

			format.Layout = PdfLayoutType.OnePage;
			pdfdoc.PageSettings.Orientation = PdfPageOrientation.Landscape;

			string file = Path.Combine(updatePath, Bestellnummer);

			pdfConverter.ExportToPdf(file + ".pdf", grdOrder);
		}

		private void callPaypal()
		{
			string url = String.Empty;

			string cmd = "?cmd=_xclick";
			string business = "&business=vertrieb@coinbook.de";			// your paypal email
			string description = string.Format("&item_name=Coinbook-Bestellung%20{0}", Bestellnummer);            // '%20' represents a space. remember HTML!
			string country = "&lc=DE";															// AU, US, etc.
			string currency = "&currency_code=EUR";                 // AUD, USD, etc.
			string amount = string.Format("&amount={0}", GesamtSumme).Replace(",", ".");
			string invoice = string.Format("&invoice={0}", Bestellnummer);

			url = "https://www.paypal.com/cgi-bin/webscr" + cmd + business + country + currency + amount + description + invoice;

            Process.Start(url);
		}
	}
}
