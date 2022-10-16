using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Coinbook.Lokalisierung;
using Coinbook.Helper;

namespace Coinbook
{
	partial class usrEigeneEinst : UserControl
	{
		public event EventHandler Changed;
		private bool init = true;

		public usrEigeneEinst()
		{
			InitializeComponent();
		}

		public void Init()
		{
			init = true;
			LanguageHelper.Localization.UpdateModul(this);

			if (CoinbookHelper.Settings.Culture == "de-DE")
				cboLangSelect.SelectedIndex = 0;
			else
				cboLangSelect.SelectedIndex = 1;

			txtEMail.Text = CoinbookHelper.Settings.Mail;
			txtVorname.Text = CoinbookHelper.Settings.Vorname;
			txtNachname.Text = CoinbookHelper.Settings.Nachname;
			txtOrt.Text = CoinbookHelper.Settings.Ort;
			txtPlz.Text = CoinbookHelper.Settings.PLZ;
			txtStraße.Text = CoinbookHelper.Settings.Strasse;
			txtLizenz.Text = CoinbookHelper.Settings.Lizenzkey;
			txtLand.Text = CoinbookHelper.Settings.Land;
			txtTelefon.Text = CoinbookHelper.Settings.Telefon;
			txtLand.Text = CoinbookHelper.Settings.Land;
			txtPasswort.Text = CoinbookHelper.Settings.Passwort;

			init = false;
		}

		public bool ReadOnly
		{
			set
			{
				txtEMail.ReadOnly = value;
				txtVorname.ReadOnly = value;
				txtNachname.ReadOnly = value;
				txtOrt.ReadOnly = value;
				txtPlz.ReadOnly = value;
				txtStraße.ReadOnly = value;
				txtLand.ReadOnly = value;
			}
		}

		private void setChanged()
		{
			if (!init)
				if (Changed != null)
					Changed(null, null);
		}

		private void cboLangSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboLangSelect.SelectedIndex == 0)
			{
				CoinbookHelper.Settings.Culture = "de-DE";
			}
			else
				CoinbookHelper.Settings.Culture = "en-US";

			setChanged();
		}

		public void HidePassword()
		{
			lblPasswort.Visible = false;
			txtPasswort.Visible = false;
		}

		public void Save()
		{
			CoinbookHelper.Settings.Mail = txtEMail.Text;
			CoinbookHelper.Settings.Vorname = txtVorname.Text;
			CoinbookHelper.Settings.Nachname = txtNachname.Text;
			CoinbookHelper.Settings.Ort = txtOrt.Text;
			CoinbookHelper.Settings.PLZ = txtPlz.Text;
			CoinbookHelper.Settings.Strasse = txtStraße.Text;
			CoinbookHelper.Settings.Lizenzkey = txtLizenz.Text;
			CoinbookHelper.Settings.Land = txtLand.Text;
			CoinbookHelper.Settings.Telefon = txtTelefon.Text;
			CoinbookHelper.Settings.Land = txtLand.Text;
			CoinbookHelper.Settings.Passwort = txtPasswort.Text;

			DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);
		}

		private new void TextChanged(object sender, EventArgs e)
		{
			CoinbookHelper.Settings.Passwort = txtPasswort.Text;
			setChanged();
		}
	}
}
