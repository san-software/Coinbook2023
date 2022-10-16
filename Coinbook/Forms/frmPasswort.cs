using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coinbook.Helper;

namespace Coinbook
{
	public partial class frmPasswort : Form
	{
		public frmPasswort()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;

			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (txtPasswort.Text == CoinbookHelper.Settings.Passwort)
				DialogResult = DialogResult.OK;
			else
				DialogResult = DialogResult.No;

			Close();
		}
	}
}
