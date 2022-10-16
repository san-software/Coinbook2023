using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Coinbook.Enumerations;
using Coinbook.Lokalisierung;

namespace Coinbook.Activation
{
	public partial class frmNoLicense : Form
	{
		public frmNoLicense()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

			btnWork.Enabled = false;
		}

		public string Value { get; set; }
		public enmAktivierungsArt Art { get; set; }

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public new void ShowDialog()
		{
			string text = "";
			//txtAnzeige.Text = LanguageHelper.Localization.GetTranslation("Lizenz", "msgNoSerial").Replace("{0}", Helper.cpuID).Replace("{1}", Value);

			switch (Art)
			{
				case enmAktivierungsArt.Expired:
					text = LanguageHelper.Localization.GetTranslation(Name, "Expired");
					break;

				case enmAktivierungsArt.Initial:
					text = LanguageHelper.Localization.GetTranslation(Name, "Initial");
					optPC1.Checked = true;
					txtBegründung.Text = LanguageHelper.Localization.GetTranslation(Name, "itemInitial");
                    btnWork_Click(null, null);
                    break;

				case enmAktivierungsArt.Retry:
					text = "Retry";
					optActivate.Checked = true;
					break;

				case enmAktivierungsArt.Wrong:
					text = LanguageHelper.Localization.GetTranslation(Name, "Wrong");
					break;
			}

			txtAnzeige.Text = text;

            if (Art != enmAktivierungsArt.Initial)
                base.ShowDialog();
            else
                Close();
		}

		private void CheckedChanged(object sender, EventArgs e)
		{
			bool result = false;

			if (optPC1.Checked)
			{
				result = true;
				btnWork.Text = LanguageHelper.Localization.GetTranslation(Name, "PC1");
			}

			if (optPC2.Checked)
			{
				result = true;
				btnWork.Text = LanguageHelper.Localization.GetTranslation(Name, "PC2");
			}

			if (optPC3.Checked)
			{
				result = true;
				btnWork.Text = LanguageHelper.Localization.GetTranslation(Name, "PC3");
			}

			if (optActivate.Checked)
			{
				result = true;
				btnWork.Text = LanguageHelper.Localization.GetTranslation(Name, "Activate");
			}

			if (optBuy.Checked)
			{
				btnWork.Text = LanguageHelper.Localization.GetTranslation(Name, "Buy");
				btnWork.Enabled = true;
			}
			else
				btnWork.Enabled = (txtBegründung.Text != string.Empty && result);
		}

		private void btnWork_Click(object sender, EventArgs e)
		{
			if (optBuy.Checked)
			{
				string url;

				if (LanguageHelper.Localization.Language == "de")
					url = @"http://coinbook.de/coinbook-lizenz.html";
				else
					url = @"http://coinbook.de/coinbook-lizenz.html";

				Process.Start(url);
			}
			else
			{
				enmAktivierungsArt aktivierungsart = enmAktivierungsArt.PC1;

				if (optPC1.Checked)
					aktivierungsart = enmAktivierungsArt.PC1;
				else if (optPC2.Checked)
					aktivierungsart = enmAktivierungsArt.PC2;
				else if (optPC1.Checked)
					aktivierungsart = enmAktivierungsArt.PC1;
				else if (optActivate.Checked)
					aktivierungsart = enmAktivierungsArt.Retry;

				frmAktivierung form = new frmAktivierung();
				form.Aktivierungsart = aktivierungsart;
				form.Grund = txtBegründung.Text;
				form.ShowDialog();
				//Helper.AktivierungAnfordern(aktivierungsart, txtBegründung.Text);
			}

			Close();
		}
	}
}