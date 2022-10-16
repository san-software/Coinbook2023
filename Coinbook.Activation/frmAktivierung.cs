using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using SAN.FTP;
using Coinbook.Enumerations;
using Coinbook.Lokalisierung;
using Syncfusion.Windows.Forms;
using Coinbook.Helper;

namespace Coinbook.Activation
{
	public partial class frmAktivierung : Form
	{
		public frmAktivierung()
		{
			InitializeComponent();
			ctlEigneEinstellungen.ReadOnly = false;
			ctlEigneEinstellungen.Init();

			LanguageHelper.Localization.UpdateModul(this);

			//Lizenz lizenz = new Lizenz();
			//lblAnzeige.Text = string.Format(lblAnzeige.Text, lizenz.ExpireDate);
		}

		public enmAktivierungsArt Aktivierungsart { get; set; }
		public string Grund { get; set; }

		private void btnAktivate_Click(object sender, EventArgs e)
		{
			ctlEigneEinstellungen.Save();

			string bit = "32 bit";
			if (Environment.OSVersion.Platform.ToString().Contains("64"))
				bit = "64 bit";

			XmlDocument document = new XmlDocument();
			XmlElement root = document.CreateElement("Element", "Root", String.Empty);
			XmlNode aktivierung = document.CreateElement(String.Empty, "Aktivierung", String.Empty);

			document.AppendChild(root);
			root.AppendChild(aktivierung);

			XmlAttribute vorname = document.CreateAttribute("Vorname");
			vorname.Value = CoinbookHelper.Settings.Vorname;
			aktivierung.Attributes.Append(vorname);

			XmlAttribute nachname = document.CreateAttribute("Nachname");
			nachname.Value = CoinbookHelper.Settings.Nachname;
			aktivierung.Attributes.Append(nachname);

			XmlAttribute plz = document.CreateAttribute("PLZ");
			plz.Value = CoinbookHelper.Settings.PLZ;
			aktivierung.Attributes.Append(plz);

			XmlAttribute ort = document.CreateAttribute("Ort");
			ort.Value = CoinbookHelper.Settings.Ort;
			aktivierung.Attributes.Append(ort);

			XmlAttribute land = document.CreateAttribute("Land");
			land.Value = CoinbookHelper.Settings.Land;
			aktivierung.Attributes.Append(land);

			XmlAttribute strasse = document.CreateAttribute("Strasse");
			strasse.Value = CoinbookHelper.Settings.Strasse;
			aktivierung.Attributes.Append(strasse);

			XmlAttribute betriebssystem = document.CreateAttribute("System");
			betriebssystem.Value = CoinbookHelper.GetWindwosClientVersion + "/" + bit;
			aktivierung.Attributes.Append(betriebssystem);

			XmlAttribute email = document.CreateAttribute("Email");
			email.Value = CoinbookHelper.Settings.Mail;
			aktivierung.Attributes.Append(email);

			XmlAttribute telefon = document.CreateAttribute("Telefon");
			telefon.Value = CoinbookHelper.Settings.Telefon;
			aktivierung.Attributes.Append(telefon);

			XmlAttribute datum = document.CreateAttribute("Datum");
			datum.Value = DateTime.Now.ToShortDateString();
			aktivierung.Attributes.Append(datum);

			XmlAttribute lizenzkey = document.CreateAttribute("Lizenzkey");
			lizenzkey.Value = CoinbookHelper.Settings.Lizenzkey;
			aktivierung.Attributes.Append(lizenzkey);

			XmlAttribute bemerkung = document.CreateAttribute("Bemerkung");
			bemerkung.Value = Grund;
			aktivierung.Attributes.Append(bemerkung);

			XmlAttribute version = document.CreateAttribute("Version");
			version.Value = Application.ProductVersion;
			aktivierung.Attributes.Append(version);

			XmlAttribute serial = document.CreateAttribute("Serial");
			serial.Value = CoinbookHelper.CPUID;
			aktivierung.Attributes.Append(serial);

			XmlAttribute art = document.CreateAttribute("Aktivierungsart");
			art.Value = Aktivierungsart.ToString();
			aktivierung.Attributes.Append(art);

			XmlAttribute vorgang = document.CreateAttribute("Vorgang");
			vorgang.Value = string.Format("{0:yyMMdd}", DateTime.Now) + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
			aktivierung.Attributes.Append(vorgang);

			string file = Path.Combine(CoinbookHelper.UpdatePath, "Aktivierung-" + CoinbookHelper.Settings.Lizenzkey + ".xml");

			document.Save(file);

			FTPClass ftp = new FTPClass();
			if (ftp.Connect(ftp.FTPParameter.URL, ftp.FTPParameter.Transfer, ftp.FTPParameter.TransferPasswort))
            {
                ftp.SetWorkingDirectory("Aktivierung");
                ftp.Upload(file, Path.GetFileName(file));
                ftp.Disconnect();

                File.Delete(file);

                CoinbookHelper.Settings.Activated = "angefordert";

                MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "msgAktivierungSent"),Application.ProductName);

			}
            else
                MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "msgAktivierungNotSent"),Application.ProductName);

            Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void frmAktivierung_Shown(object sender, EventArgs e)
		{
			ctlEigneEinstellungen.HidePassword();

    //        Lizenz lizenz = new Lizenz();
    //        var temp = lblAnzeige.Text;
				//lblAnzeige.Text = temp.Replace("{0}", lizenz.ExpireDate);
		}
	}
}
