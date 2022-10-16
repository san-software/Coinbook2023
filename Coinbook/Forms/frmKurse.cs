using Coinbook.Helper;
using Syncfusion.Windows.Forms;
using System;
using System.Windows.Forms;

namespace Coinbook
{
	public partial class frmKurse : Form
	{
		public frmKurse()
		{
			InitializeComponent();
      LanguageHelper.Localization.UpdateModul(this);

      Text = LanguageHelper.Localization.GetTranslation(Name, Name);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public new void ShowDialog(IWin32Window window)
		{
			ctlWährung.Init();
			btnSave.Enabled = false;
			DialogResult = System.Windows.Forms.DialogResult.Cancel;

			base.ShowDialog();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			save();
		}

		private void ctlWährung_Changed(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
		}

		private void save()
        {
			DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);
			btnSave.Enabled = false;
			ctlWährung.Save();
			DialogResult = DialogResult.OK;
		}

		private void frmKurse_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (btnSave.Enabled)
			{
				string text = LanguageHelper.Localization.GetTranslation(Name, "msgSave");
				if (MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					save();
			}
		}
    }
}
