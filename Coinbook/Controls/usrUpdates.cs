using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Coinbook.Enumerations;
using Coinbook.Helper;

namespace Coinbook
{
	partial class usrUpdates : UserControl
	{
		public event EventHandler Changed;
		private bool init = true;
		List<KeyValuePair<string, string>> liste;

		public usrUpdates()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);
		}

		public void Init()
		{
			init = true;
			LanguageHelper.Localization.UpdateModul(this);

			chkModulAutoUpdate.Checked = CoinbookHelper.Settings.ModulAutoUpdate;

			init = false;
		}

		private void chkModulAutoUpdate_CheckedChanged(object sender, EventArgs e)
		{
			CoinbookHelper.Settings.ModulAutoUpdate = chkModulAutoUpdate.Checked;

			if (Changed != null)
				Changed(null, null);
		}

		private void setChanged()
		{
			if (!init)
				if (Changed != null)
					Changed(null, null);
		}

	}
}
