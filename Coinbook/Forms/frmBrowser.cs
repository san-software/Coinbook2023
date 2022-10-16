using Coinbook.Lokalisierung;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Coinbook
{
	public partial class frmBrowser : Form
	{
		public frmBrowser()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);
		}

		public string Adresse { get; set; }
		public string Caption { get; set; }

		public new void ShowDialog()
		{
			Text = Caption;
			webBrowser1.Navigate(Adresse);

			base.ShowDialog();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

	}
}
