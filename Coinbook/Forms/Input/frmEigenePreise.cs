using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SAN.Converter;
using Coinbook.Model;
using LiteDB.Database;
using Coinbook.Helper;
using Coinbook.EventHandlers;

namespace Coinbook
{
	public partial class frmEigenePreise : Form
	{
		private int dpi = 0;
		BindingList<EigenerPreis> eigenerPreis = null;
		public event PreisEventHandler PreisChanged;

		public frmEigenePreise()
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
			eigenerPreis = DatabaseHelper.LiteDatabase.ReadEigenePreise(GUID, "Sammlung");
			grdPreise.DataSource = eigenerPreis;

			grdPreise.Columns["colID"].Visible = false;
			grdPreise.Columns["colSammlungID"].Visible = false;

			grdPreise.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			grdPreise.ColumnHeadersDefaultCellStyle.Font = new Font(grdPreise.Font.FontFamily, grdPreise.Font.Size, FontStyle.Bold);
			grdPreise.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
			grdPreise.Columns["colPreis"].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);

			switch (dpi)
			{
				case 120:
					grdPreise.Columns["colErhaltungsGrad"].Width = 100;
					break;

				default:
					grdPreise.Columns["colErhaltungsGrad"].Width = 90;
					break;
			}

			lblGebietText.Text = Nation + Environment.NewLine + Ära + Environment.NewLine + Gebiet;
			lblMünzeText.Text = Nennwert + " " + Währung + " - " + Jahr + " - " + Münzzeichen;

			btnSave.Enabled = false;

			grdPreise.Columns["colPreis"].HeaderText = CoinbookHelper.Settings.CurrentWährung;

			base.ShowDialog(owner);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;

			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			grdPreise.EndEdit();
			var p = DatabaseHelper.LiteDatabase.SaveOwnPrices(GUID, (BindingList<EigenerPreis>)grdPreise.DataSource);

			if (PreisChanged != null)
			{
				PreisEventArgs args = new PreisEventArgs(GUID, p.SPreis, p.SPPreis, p.SSPreis,
					p.SSPPreis, p.VZPreis, p.VZPPreis, p.STNPreis, p.STHPreis, p.PPPreis);
				PreisChanged(this, args);
			}

			CoinbookHelper.Changes = true;
		}

		private void grdPreise_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			btnSave.Enabled = true;
		}
	}
}
