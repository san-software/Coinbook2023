using Coinbook.Enumerations;
using Coinbook.Lokalisierung;
using Coinbook.Model;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Coinbook.Helper;
using System.Windows.Forms;
using Coinbook.EventHandlers;

namespace Coinbook
{
	public partial class frmKatalogNummer : Form
	{
		public event KatalognummerEventHandler ChangeKatalogNumber;

		private EigeneKatNr katalog;

		/// <summary>
		/// Eigene Katalognummern ablegen / bearbeiten / löschen.
		/// </summary>
		public frmKatalogNummer()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);
		}

		public string CoinbookKatNr { get; set; }
		public int NationID { get; set; }
		public string CoinbookOriginal { get; set; }

		public new void ShowDialog(IWin32Window owner)
		{
			katalog = DatabaseHelper.LiteDatabase.GetKatalogNummer(CoinbookKatNr);

			txtKatNr.Text = katalog.KatNr;
		
			lblCoinbookNummer.Text = CoinbookKatNr;
			btnSave.Enabled = false;
			
			base.ShowDialog(owner);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			DialogResult result = DialogResult.Yes;
			if (btnSave.Enabled)
			{
				string text = LanguageHelper.Localization.GetTranslation(Name, "msgSave");

				result = MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == System.Windows.Forms.DialogResult.Yes)
					btnSave_Click(null, null);
			}

			if (result != DialogResult.Abort)
				Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			katalog.KatNr = txtKatNr.Text;
			katalog.NationID = NationID;
			katalog.ID = katalog.Coinbook;
            DatabaseHelper.LiteDatabase.SaveKatalogNummer(katalog);
			btnSave.Enabled = false;

			if (ChangeKatalogNumber != null && CoinbookHelper.Settings.Katalognummern == enmKatalognummern.Eigen)
				ChangeKatalogNumber(this, new KatalognummerEventArgs(enmKatalogAction.Neu, CoinbookOriginal, txtKatNr.Text));

			CoinbookHelper.Changes = true;

			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DatabaseHelper.LiteDatabase.DeleteKatalogNummer(katalog.ID);
            txtKatNr.Text = String.Empty;
			btnSave.Enabled = false;

			if (ChangeKatalogNumber != null && CoinbookHelper.Settings.Katalognummern == enmKatalognummern.Eigen)
				ChangeKatalogNumber(this, new KatalognummerEventArgs(enmKatalogAction.Delete, CoinbookOriginal, string.Empty));

			Close();
		}

		private void txtKatNr_TextChanged(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
		}
	}
}
