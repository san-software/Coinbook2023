using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System.Management;
using SAN.Converter;
using Coinbook.Helper;
using Syncfusion.Windows.Forms;

namespace Coinbook
{
	public partial class frmDBState : Form 
	{
		public frmDBState() 
		{
			InitializeComponent();
      LanguageHelper.Localization.UpdateModul(this);
		}

		public new void ShowDialog(IWin32Window control)
		{
			grdÜbersicht.DataSource = DatabaseHelper.LiteDatabase.ReadStatus(LanguageHelper.Localization.GetTranslation("frmDBState", "msgNoLicense"));

			btnSave.Enabled = false;

			DialogResult = DialogResult.Cancel;

			base.ShowDialog(control);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
      //if (btnSave.Enabled)
      //{
      //    string text = LanguageHelper.Localization.GetTranslation(Name, "msgSave");

      //  if (MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      //    btnSave_Click(null, null);
      //}

			Close();
		}

		private void grdÜbersicht_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			string col = grdÜbersicht.Columns[e.ColumnIndex].Name;

			if (col == "Verwendet")
			{
				if (grdÜbersicht.Rows[e.RowIndex].Cells["LastUpdate"].Value.ToString() == String.Empty)
				{
					if (ConvertEx.ToBoolean(grdÜbersicht.Rows[e.RowIndex].Cells["Verwendet"].Value))
					{
						string text = LanguageHelper.Localization.GetTranslation(Name, "msgOrder");
						text = text.Replace("{0}", grdÜbersicht.Rows[e.RowIndex].Cells["colNation"].Value.ToString());

						if (MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
						{
							frmBrowser form = new frmBrowser();
							form.Adresse = @"http://www.coinbook.de/support/order.php?lang=" + CoinbookHelper.Settings.Culture;
							form.ShowDialog();
						}

						grdÜbersicht.Rows[e.RowIndex].Cells["Verwendet"].Value = false;
					}
				}
				else
					btnSave.Enabled = true;
			}
		}
	}
}
