using Coinbook.Lokalisierung;
using Coinbook.Model;
using LiteDB.Database;
using SAN.Converter;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Coinbook.Helper;
using System.Collections.Generic;

namespace Coinbook
{
	public partial class frmAuktion : Form
	{
		BindingList<Auktion> auktionen;

		private int dpi = 0;

		private CultureInfo cultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);

		public frmAuktion()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

			Graphics graphics;
			graphics = this.CreateGraphics();
			dpi = ConvertEx.ToInt32(graphics.DpiX);
		}

		public string GUID { get; set; }
		public string Nation { get; set; }
		public string Ära { get; set; }
		public string Gebiet { get; set; }
		public string Nennwert { get; set; }
		public string Währung { get; set; }
		public string Jahr { get; set; }
		public string Münzzeichen { get; set; }

		public new void ShowDialog(IWin32Window owner)
		{
			auktionen = DatabaseHelper.LiteDatabase.ReadAuktionen(GUID, "Sammlung");
			grdAuktionen.DataSource = auktionen;

			initGrid();
			btnSave.Enabled = false;
			lblGebietText.Text = Nation + " - " + Ära + " - " + Gebiet;
			lblMünzeText.Text = Nennwert + " " + Währung + " - " + Jahr + " - " + Münzzeichen;

			base.ShowDialog(owner);
		}

		private void initGrid()
		{
			((DataGridViewComboBoxColumn)grdAuktionen.Columns["colErhaltungsgrad"]).DataSource = CoinbookHelper.Erhaltungsgrade;
			((DataGridViewComboBoxColumn)grdAuktionen.Columns["colErhaltungsgrad"]).ValueMember = "id";
			((DataGridViewComboBoxColumn)grdAuktionen.Columns["colErhaltungsgrad"]).DisplayMember = "Erhaltung";

			grdAuktionen.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			grdAuktionen.ColumnHeadersDefaultCellStyle.Font = new Font(grdAuktionen.Font.FontFamily, grdAuktionen.Font.Size, FontStyle.Bold);
			grdAuktionen.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;

			grdAuktionen.Columns["colPreis"].DefaultCellStyle.FormatProvider = cultureInfo;
			grdAuktionen.Columns["colDat"].DefaultCellStyle.FormatProvider = cultureInfo;
			grdAuktionen.Columns["colDat"].DefaultCellStyle.Format = cultureInfo.DateTimeFormat.ShortDatePattern;

			grdAuktionen.AutoGenerateColumns = false;

			switch (dpi)
			{
				case 120:
					grdAuktionen.Columns["colErhaltungsgrad"].Width = 95;
					grdAuktionen.Columns["colPreis"].Width = 100;
					grdAuktionen.Columns["colDat"].Width = 100;
					break;

				default:
					grdAuktionen.Columns["colErhaltungsgrad"].Width = 85;
					grdAuktionen.Columns["colPreis"].Width = 80;
					grdAuktionen.Columns["colDat"].Width = 70;
					break;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			Auktion item = new Auktion();
			item.ID = 0;
			item.Erhaltungsgrad = 1;
			item.Guid = GUID;
			item.Preis = 0;
			item.Datum = string.Empty;
			item.Auktionshaus = string.Empty;
			item.Auktionator = string.Empty;

			auktionen.Add(item);

			grdAuktionen.Refresh();
		}

		private void grdAuktionen_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			string col = grdAuktionen.Columns[e.ColumnIndex].Name;

			if (col == "Erhaltungsgrad")
			{
				grdAuktionen.Rows[e.RowIndex].Cells[col].Value = 1;
				grdAuktionen.Rows[e.RowIndex].Cells["Guid"].Value = GUID;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			grdAuktionen.EndEdit();
			DatabaseHelper.LiteDatabase.SaveAuktionen(auktionen);
			CoinbookHelper.Changes = true;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DatabaseHelper.LiteDatabase.DeleteAuktion(auktionen[grdAuktionen.CurrentRow.Index].ID);
			grdAuktionen.Rows.Remove(grdAuktionen.CurrentRow);

			btnDelete.Enabled = (grdAuktionen.Rows.Count != 0);
		}

		private void grdAuktionen_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			btnSave.Enabled = true;
		}

	}
}
