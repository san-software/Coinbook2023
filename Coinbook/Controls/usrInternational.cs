using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Coinbook.Enumerations;
using System.Diagnostics;
using Coinbook.Lokalisierung;
using Syncfusion.Windows.Forms;
using Coinbook.Helper;

namespace Coinbook
{
	partial class usrInternational : UserControl
	{
		public event EventHandler Changed;
		public event EventHandler LanguageChanged;
		public event EventHandler ErhaltungChanged;

		private bool init = true;
		List<KeyValuePair<string, string>> liste;

		public usrInternational()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

			cboErhaltungsgrad.DisplayMember = "Land";
			cboErhaltungsgrad.ValueMember = "Sprache";

			cboSprache.SelectedIndexChanged += new EventHandler(cboSprache_SelectedIndexChanged);
			LanguageHelper.Localization.LanguageChange += new EventHandler(Localization_ChangeLangauge);
		}

		public void Init()
		{
			init = true;
			LanguageHelper.Localization.UpdateModul(this);

			//string sprache =;
			cboSprache.DataSource = LanguageHelper.Localization.Languages;
			cboSprache.DisplayMember = "Sprache";
			cboSprache.ValueMember = "Key";
			for (int i = 0; i < cboSprache.Items.Count; i++)
				if (((ComboStruktur)cboSprache.Items[i]).Key == LanguageHelper.Localization.Language)
					cboSprache.SelectedIndex = i;

			optLetzBen.Checked = CoinbookHelper.Settings.LastUsed;

			if (!optLetzBen.Checked)
			{
				switch (CoinbookHelper.Settings.MünzdetailIndex)
				{
					case enmMünzdetailIndex.Details:
						optMünzdetailTab.Checked = true;
						break;

					case enmMünzdetailIndex.Entwurf:
						optBeschreibungTab.Checked = true;
						break;

					case enmMünzdetailIndex.Kommentar:
						optAusgabeTab.Checked = true;
						break;

					case enmMünzdetailIndex.Sammlung:
						optSammlungTab.Checked = true;
						break;

					case enmMünzdetailIndex.Bild:
						optBildTab.Checked = true;
						break;

					case enmMünzdetailIndex.LastUsed:
						optLetzBen.Checked = true;
						break;

					default:
						optMünzdetailTab.Checked = true;
						break;
				}
			}

			chkKaufpreis.Checked = CoinbookHelper.Settings.Preisvorgabe;


			txtPath.Text = CoinbookHelper.Settings.UpdatePath;
			chkBackupBeiQuit.Checked = CoinbookHelper.Settings.BackupByQuit;

			liste = DatabaseHelper.LiteDatabase.ReadCountries();
			cboErhaltungsgrad.DataSource = liste;

			cboErhaltungsgrad.SelectedIndex = liste.FindIndex(a => a.Key == CoinbookHelper.Settings.International);

			init = false;
		}

		private void cboErhaltungsgrad_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!init)
			{
				CoinbookHelper.Settings.International = liste[cboErhaltungsgrad.SelectedIndex].Key;
				DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);

				if (ErhaltungChanged != null)
					ErhaltungChanged(this, new EventArgs());

				if (Changed != null)
					Changed(this, new EventArgs());
			}
		}

		private void btnBackup_Click(object sender, EventArgs e)
		{
			if (dlgPath.ShowDialog() != DialogResult.Cancel)
			{
				txtPath.Text = dlgPath.SelectedPath;
				CoinbookHelper.Settings.UpdatePath = dlgPath.SelectedPath;

				setChanged();
			}
		}

		private void chkBackupBeiQuit_CheckedChanged(object sender, EventArgs e)
		{
			if (!init)
			{
				CoinbookHelper.Settings.BackupByQuit = chkBackupBeiQuit.Checked;
				setChanged();
			}
		}

		private void GetMuenzDetailState(object sender, EventArgs e)
		{
			if (!init)
			{
				if (optMünzdetailTab.Checked)
					CoinbookHelper.Settings.MünzdetailIndex = enmMünzdetailIndex.Details;
				if (optAusgabeTab.Checked)
					CoinbookHelper.Settings.MünzdetailIndex = enmMünzdetailIndex.Kommentar;
				if (optBeschreibungTab.Checked)
					CoinbookHelper.Settings.MünzdetailIndex = enmMünzdetailIndex.Entwurf;
				if (optSammlungTab.Checked)
					CoinbookHelper.Settings.MünzdetailIndex = enmMünzdetailIndex.Sammlung;
				if (optBildTab.Checked)
					CoinbookHelper.Settings.MünzdetailIndex = enmMünzdetailIndex.Bild;
				if (optLetzBen.Checked)
					CoinbookHelper.Settings.MünzdetailIndex = enmMünzdetailIndex.LastUsed;

				setChanged();
			}
		}

		private void chkKaufpreis_CheckedChanged(object sender, EventArgs e)
		{
			if (!init)
			{
				CoinbookHelper.Settings.Preisvorgabe = chkKaufpreis.Checked;
				setChanged();
			}
		}

		private void setChanged()
		{
				if (Changed != null)
					Changed(null, null);
		}

		private void cboSprache_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();     // Draw the default background

			ComboStruktur item = (ComboStruktur)cboSprache.Items[e.Index];

			// Get the bounds for the first column
			Rectangle r1 = e.Bounds;
			r1.Width /= 2;

			// Draw the colored 16 x 16 square
			Rectangle r = new Rectangle();
			r.X = e.Bounds.Left;
			r.Y = e.Bounds.Top;
			r.Height = e.Bounds.Height;
			r.Width = 32;
			e.Graphics.DrawImage(item.Flagge, r);

			// Get the bounds for the second column
			Rectangle r2 = e.Bounds;
			r2.X = 40;
			r2.Width = e.Bounds.Width - 40;

			// Draw the language on the second column
			using (SolidBrush sb = new SolidBrush(e.ForeColor))
				e.Graphics.DrawString(item.Sprache, e.Font, sb, r2);
		}

		private void cboSprache_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboSprache.SelectedIndex == 0)
				CoinbookHelper.Settings.Culture = "de-DE";
			else
				CoinbookHelper.Settings.Culture = "en-US";

			DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);

			ComboStruktur item = (ComboStruktur)cboSprache.Items[cboSprache.SelectedIndex];

			LanguageHelper.Localization.UpdateLanguage(CoinbookHelper.Settings.Culture.Substring(0,2));
			LanguageHelper.Localization.UpdateModul(this);

			if (LanguageChanged != null)
				LanguageChanged(this, new EventArgs());

			//ProcessStartInfo userInfo = new ProcessStartInfo("Sprache.exe");
			//userInfo.Arguments = cboSprache.Text;
			//userInfo.WindowStyle = ProcessWindowStyle.Normal;
			//Process.Start(userInfo).WaitForExit();		TODO

			//loadNationen();
			//cboNationen.SelectedValue = Helper.Settings.Nation;
			//cboÄra.SelectedValue = Helper.Settings.Ära;
			//cboGebiete.SelectedValue = Helper.Settings.Gebiet;

			//loadListe(false);         //TODO
		}

		void Localization_ChangeLangauge(object sender, EventArgs e)
		{
			//loadNationen();
			//LoadÄra();
			//loadGebiete();
		}
	}
}
