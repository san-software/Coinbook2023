using Coinbook.Lokalisierung;
using System;
using System.Windows.Forms;
using Coinbook.Helper;
using Coinbook.EventHandlers;

namespace Coinbook
{

	partial class frmSettings : Form
	{
		public event EventHandler LanguageChanged;
		public event EventHandler ErhaltungChanged;

		public frmSettings()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

			btnSave.Enabled = false;
		}

		private void Changed(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
		}

		public new void ShowDialog(IWin32Window window)
		{
			ctlAllgemein.Init();
			ctlInternational.Init();
			ctlEigneEinstellungen.Init();
			ctlUpdates.Init();

			base.ShowDialog(window);
		}

		public void SaveData()
		{
			DialogResult = DialogResult.OK;
		}

		public void AbortSave()
		{
			DialogResult = DialogResult.Cancel;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);
			btnSave.Enabled = false;
		}

		private void ctlAllgemein_PreisStyleChanged(object sender, PreisStyleEventArgs args)
		{
			////if (PreisStyleChanged != null)
			////	PreisStyleChanged(this, args);
		}

		private void ctlInternational_LanguageChanged(object sender, EventArgs e)
		{
			LanguageHelper.Localization.UpdateLanguage(CoinbookHelper.Settings.Culture.Substring(0, 2));
			LanguageHelper.Localization.UpdateModul(this);

			if (LanguageChanged != null)
				LanguageChanged(this, new EventArgs());
		}

		private void ctlInternational_ErhaltungChanged(object sender, EventArgs e)
		{
			if (ErhaltungChanged != null)
				ErhaltungChanged(this, new EventArgs());
		}
	}
}
