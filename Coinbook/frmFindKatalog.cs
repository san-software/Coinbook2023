using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using OleDB;

namespace Coinbook
{
	public partial class frmFindKatalog : Form
	{

		private OleDBZugriff katalog;

		/// Katalognummer für Münze suchen
		public frmFindKatalog()
		{
			InitializeComponent();
			Localization.UpdateModul(this);
		}

		public string stringKatNr { get; set; }

		public new void ShowDialog()
		{
			katalog = new OleDBZugriff();
			katalog.Tabelle = "stblEigeneKatNr";

			katalog.Binding.DataBindingInit("select * from stblEigeneKatNr");

			grdKatalog.DataSource = katalog.Binding.DataBinding;
			grdKatalog.Columns["id"].Visible = false;
			grdKatalog.Columns["katNr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			grdKatalog.ReadOnly = true;

			base.ShowDialog();
		}

		private void Schliessen_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void grdKatalog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			stringKatNr = grdKatalog.Rows[grdKatalog.CurrentRow.Index].Cells["KatNr"].Value.ToString();
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
