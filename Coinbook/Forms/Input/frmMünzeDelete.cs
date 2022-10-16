using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SAN.Converter;
using System.IO;
using Coinbook.Model;
using Coinbook.Lokalisierung;
using Coinbook.Helper;
using Coinbook.EventHandlers;

namespace Coinbook
{
	public partial class frmMünzeDelete : Form
	{
		//private DialogResult result = DialogResult.Cancel;
		private int dpi = 0;
		Icon eraseIcon = new Icon(Path.Combine(Path.Combine(Application.StartupPath, "Images"), "Erase.ico"));
		private BindingList<Sammlung> sammlungListe;

		public event CoinEventHandler ChangeCoin;
		public event CoinEventHandler ChangeBestand;

		public frmMünzeDelete()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

			Graphics graphics;
			graphics = this.CreateGraphics();
			dpi = ConvertEx.ToInt32(graphics.DpiX);
		}

		public int ID { get; set; }
		public string Nation { get; set; }
		public string Ära { get; set; }
		public string Gebiet { get; set; }
		public string GUID { get; set; }
		public MünzDetail CurrentRow { get; set; }
		public int Index { get; set; }
		public string Caller { get; set; }

		public new void ShowDialog(IWin32Window window)
		{
			loadDetails();

			grdSammlung.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			grdSammlung.ColumnHeadersDefaultCellStyle.Font = new Font(grdSammlung.Font.FontFamily, grdSammlung.Font.Size, FontStyle.Bold);
			grdSammlung.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;

			switch (dpi)
			{
				case 120:
					grdSammlung.Columns["colErhaltungsgrad"].Width = 95;
					grdSammlung.Columns["colDoublette"].Width = 60;
					grdSammlung.Columns["colKaufdatum"].Width = 85;
					grdSammlung.Columns["colKatNr"].Width = 100;
					grdSammlung.Columns["colPreis"].Width = 100;
					break;

				default:
					grdSammlung.Columns["colErhaltungsgrad"].Width = 80;
					grdSammlung.Columns["colDoublette"].Width = 50;
					grdSammlung.Columns["colKaufdatum"].Width = 75;
					grdSammlung.Columns["colKatNr"].Width = 90;
					grdSammlung.Columns["colPreis"].Width = 70;
					break;
			}

			grdSammlung.Columns["Erhaltung"].Visible = false;
			grdSammlung.Columns["Guid"].Visible = false;
			grdSammlung.Columns["Picture"].Visible = false;
			grdSammlung.Columns["ShowPicture"].Visible = false;
			grdSammlung.Columns["Farbe"].Visible = false;
			grdSammlung.Columns["FehlerText"].Visible = false;
			grdSammlung.Columns["KatNr"].Visible = false;
			grdSammlung.Columns["colFehlerhaft"].Visible = false;
			grdSammlung.Columns["EigenerPreis"].Visible = false;
			grdSammlung.Columns["NationID"].Visible = false;

			lblGebietText.Text = Nation + " - " + Ära + " - " + Gebiet;
			lblMünzeText.Text = CoinbookHelper.MuenzkatalogFiltered[Index].Nominal + " " + CoinbookHelper.MuenzkatalogFiltered[Index].Waehrung + " - " 
				+ CoinbookHelper.MuenzkatalogFiltered[Index].Jahrgang + " - " + CoinbookHelper.MuenzkatalogFiltered[Index].Muenzzeichen;

			base.ShowDialog();
		}

		private void grdSammlung_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				string text = LanguageHelper.Localization.GetTranslation(Name, "msgDelete");

				Sammlung coin = sammlungListe[e.RowIndex];

				DatabaseHelper.LiteDatabase.DeleteSammlung(coin.ID);
				DatabaseHelper.LiteDatabase.SaveBestand(
					coin, 
					-1, 
					CoinbookHelper.MuenzkatalogFiltered[Index].RegionID, 
					CoinbookHelper.MuenzkatalogFiltered[Index].AeraID, 
					CoinbookHelper.MuenzkatalogFiltered[Index].NationID);

				grdSammlung.Rows.RemoveAt(e.RowIndex);

				if (ChangeCoin != null)
					ChangeCoin(this, new CoinEventArgs(Index, coin, -1));

				if (Caller == "frmMain" && ChangeBestand != null)
					ChangeBestand(this, new CoinEventArgs(Index, coin, -1));

				CoinbookHelper.Changes = true;
			}
		}

		public void loadDetails()
		{
			sammlungListe = CoinbookHelper.LoadSammlungsliste(GUID, Index);
			grdSammlung.DataSource = sammlungListe;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void grdSammlung_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				switch (grdSammlung.Columns[e.ColumnIndex].Name)
				{
					case "btnDel":
						e.Paint(e.CellBounds, DataGridViewPaintParts.All);
						e.Graphics.DrawIcon(eraseIcon, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
						e.Handled = true;
						break;
				}
			}
		}
	}
}