using System;
using System.Windows.Forms;
using Coinbook.Enumerations;
using Coinbook.Lokalisierung;
using Coinbook.Helper;
using Coinbook.EventHandlers;

namespace Coinbook
{
	partial class usrAllgemein : UserControl
	{
		public event EventHandler Changed;
		public event PreisStyleEventHandler PreisStyleChanged;
		private bool init = true;

		public usrAllgemein()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);
		}

		public void Init()
		{
			LanguageHelper.Localization.UpdateModul(this);

			init = true;

			if (CoinbookHelper.Settings.NatFirst)
				optErsteNat.Checked = true;
			else
				optLastPos.Checked = true;

			chkOwnKatalog.Checked = CoinbookHelper.Settings.KatalognummernAnzeige;

			//if (!chkOwnKatalog.Checked)
			//	Helper.Settings.Katalognummern = enmKatalognummern.Coinbook;

			switch (CoinbookHelper.Settings.Katalognummern)
			{
				case enmKatalognummern.Coinbook:
					optCoinbookNummern.Checked = true;
					break;

				case enmKatalognummern.Eigen:
					optEigeneNummern.Checked = true;
					break;
			}

			switch (CoinbookHelper.Settings.SelectedStyle)
			{
				case enmSelectedStyle.SammlungUndDoubletten:
					optStandard.Checked = true;
					break;
				case enmSelectedStyle.DoublettenOnly:
					optDoubletten.Checked = true;
					break;
				case enmSelectedStyle.Icon:
					optIcon.Checked = true;
					break;
				case enmSelectedStyle.SammlungOnly:
					optSammlung.Checked = true;
					break;
			}

			chkExemplar.Checked = CoinbookHelper.Settings.Exemplarsammler;

			switch (CoinbookHelper.Settings.Preise)
			{
				case enmPreise.EigenePreise:
					optEigenePreise.Checked = true;
					break;
				case enmPreise.Katalogpreise:
					optKatalogpreise.Checked = true;
					break;
				case enmPreise.Kaufpreise:
					optKaufpreise.Checked = true;
					break;
			}

			init = false;
		}

		/// <summary>
		/// Listeneinstellungen übernehmen
		/// </summary>
		/// 
		private void GetCheckStateListe(object sender, EventArgs e)
		{
			if (!init)
			{
				if (optStandard.Checked)
					CoinbookHelper.Settings.SelectedStyle = enmSelectedStyle.SammlungUndDoubletten;

				if (optDoubletten.Checked)
					CoinbookHelper.Settings.SelectedStyle = enmSelectedStyle.DoublettenOnly;

				if (optIcon.Checked)
					CoinbookHelper.Settings.SelectedStyle = enmSelectedStyle.Icon;

				if (optSammlung.Checked)
					CoinbookHelper.Settings.SelectedStyle = enmSelectedStyle.SammlungOnly;

				setChanged();
			}
		}

		private void GetStEinstState(object sender, EventArgs e)
		{
			if (!init)
			{
				if (optErsteNat.Checked)
					CoinbookHelper.Settings.NatFirst = true;

				if (optLastPos.Checked)
					CoinbookHelper.Settings.NatFirst = false;

				setChanged();
			}
		}

		private void GetNumberState(object sender, EventArgs e)
		{
			if (!init)
			{
				if (optCoinbookNummern.Checked)
					CoinbookHelper.Settings.Katalognummern = enmKatalognummern.Coinbook;

				if (optEigeneNummern.Checked)
					CoinbookHelper.Settings.Katalognummern = enmKatalognummern.Eigen;

				setChanged();
			}
		}

		private void setChanged()
		{
				if (Changed != null)
					Changed(null, null);
		}

		private void chkExemplar_Click(object sender, EventArgs e)
		{
			if (!init)
			{
				CoinbookHelper.Settings.Exemplarsammler = chkExemplar.Checked;
				setChanged();
			}
		}

		private void Preise_CheckedChanged(object sender, EventArgs e)
		{
			if (!init)
			{
				if (optKatalogpreise.Checked)
					CoinbookHelper.Settings.Preise = enmPreise.Katalogpreise;
				else if (optKaufpreise.Checked)
					CoinbookHelper.Settings.Preise = enmPreise.Kaufpreise;
				else
					CoinbookHelper.Settings.Preise = enmPreise.EigenePreise;

				if (PreisStyleChanged != null)
					PreisStyleChanged(this, new PreisStyleEventArgs(CoinbookHelper.Settings.Preise));

				setChanged();
			}
		}

		private void chkOwnKatalog_CheckedChanged(object sender, EventArgs e)
		{
			grpEigeneKatalognummern.Enabled = chkOwnKatalog.Checked;
			CoinbookHelper.Settings.KatalognummernAnzeige = chkOwnKatalog.Checked;

			if (!init)
			{
				//if (chkOwnKatalog.Checked)
				//{
				//	optCoinbookNummern.Checked = true;
				//}
				//else
				//{
				//	optCoinbookNummern.Checked = false;
				//	optEigeneNummern.Checked = false;
				//}

				setChanged();
			}
		}

	}
}
