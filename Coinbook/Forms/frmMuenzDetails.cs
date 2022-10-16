using Coinbook.Enumerations;
using Coinbook.EventHandlers;
using Coinbook.Helper;
using Coinbook.Lokalisierung;
using Coinbook.Model;
using SAN.Converter;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace Coinbook
{
	partial class frmMuenzDetails : Form
	{
		private string guid = String.Empty;
		private string picture = String.Empty;
		private string ownPicture = String.Empty;
		private bool showOwnPicture = false;
		private int dpi = 0;
		private string katNr = String.Empty;
		private MünzDetail muenzDetails = null;
		private Bestand bestand = null;
		private List<CoinDetail> coinDetails = new List<CoinDetail>();
		private Texte texte;

		private CultureInfo cultureInfo;

		Icon editIcon = Coinbook.Properties.Resources.edit;
		Icon addIcon = Coinbook.Properties.Resources.Add;

		public event CoinEventHandler CoinTest;
		public event CoinEventHandler ChangeCoin;
		public event CoinEventHandler ChangeBestand;
		public event EventHandler ChangeOwnPicture;
		public event KatalognummerEventHandler ChangeKatalogNumber;
		public event PreisEventHandler PreisChanged;
		public event EventHandler HideForm;

		public frmMuenzDetails()
		{
			InitializeComponent();

			if (!(LicenseManager.UsageMode == LicenseUsageMode.Designtime))
			{
				LanguageHelper.Localization.UpdateModul(this);
				cultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);
			}

			Graphics graphics = this.CreateGraphics();
			dpi = ConvertEx.ToInt32(graphics.DpiX);

			lblDatum.BackColor = Color.FromArgb(0, Color.Transparent);

			grdMuenze.AutoGenerateColumnsMode = AutoGenerateColumnsMode.SmartReset;
			grdMuenze.Style.CellStyle.Font = new GridFontInfo(new Font("Arial", 10));
			grdMuenze.Style.CellStyle.Borders.All = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
			grdMuenze.RowHeight = 24;

			grdMuenze.ToolTipOpening += grdMuenze_ToolTipOpening;

			grdSammlung.AutoGenerateColumns = false;
			grdDoubletten.AutoGenerateColumns = false;


			initTooltip();
		}

		public new void Show(IWin32Window owner)
		{
			lblMaxCount.Text = CoinbookHelper.MuenzkatalogFiltered.Count.ToString();
			lblNationText.Text = Nation;
			lblÄraText.Text = Ära;
			lblGebietText.Text = Gebiet;

			switch (CoinbookHelper.Settings.MünzdetailIndex)
			{
				case enmMünzdetailIndex.LastUsed:
					try
					{
						tabMuenzDetails.SelectTab(CoinbookHelper.Settings.MünzTab);
					}
					catch (SystemException e)
					{
						var x = e;
						tabMuenzDetails.SelectTab("tabDetails");
					}
					break;

				case enmMünzdetailIndex.Details:
					tabMuenzDetails.SelectTab("tabDetails");
					break;

				case enmMünzdetailIndex.Entwurf:
					tabMuenzDetails.SelectTab("tabBeschreibungen");
					break;

				case enmMünzdetailIndex.Kommentar:
					tabMuenzDetails.SelectTab("tabKommentare");
					break;

				case enmMünzdetailIndex.Sammlung:
					tabMuenzDetails.SelectTab("tabSammlung");
					break;

				case enmMünzdetailIndex.Bild:
					tabMuenzDetails.SelectTab("tabBild");
					break;
			}

			loadDetails();
			showDetails();

			setColumnHeaders();

			txtPicture_TextChanged(null, null);

			base.Show(owner);
		}

		public int Index { get; set; }
		public string Nation { get; set; }
		public string Ära { get; set; }
		public string Gebiet { get; set; }
		public string Guid { get; set; }

		#region Navbar Navigation

		/// <summary>
		/// ertses Element der Liste aufrufen
		/// </summary>
		/// 
		private void btnFirstItem_Click(object sender, EventArgs e)
		{
			if (Index > 0)
			{
				Index = 0;
				this.Cursor = Cursors.WaitCursor;
				Guid = CoinbookHelper.MuenzkatalogFiltered[Index].GUID;
				loadDetails();
				showDetails();
				this.Cursor = Cursors.Default;

				if (ChangeCoin != null)
					ChangeCoin(this, new CoinEventArgs(Index, null, 0));
			}
		}

		/// <summary>
		/// vorheriges Element aufrufen
		/// </summary>
		/// 
		private void btnPrevious_Click(object sender, EventArgs e)
		{
			if (Index > 0)
			{
				Index--;
				Guid = CoinbookHelper.MuenzkatalogFiltered[Index].GUID;
				this.Cursor = Cursors.WaitCursor;
				loadDetails();
				showDetails();
				this.Cursor = Cursors.Default;

				if (ChangeCoin != null)
					ChangeCoin(this, new CoinEventArgs(Index, null, 0));
			}
		}

		/// <summary>
		/// nächstes elment aufrufen
		/// </summary>
		/// 
		private void btnNext_Click(object sender, EventArgs e)
		{
			if (Index < CoinbookHelper.MuenzkatalogFiltered.Count - 1)
			{
				Index++;

				Guid = CoinbookHelper.MuenzkatalogFiltered[Index].GUID;
				this.Cursor = Cursors.WaitCursor;
				loadDetails();
				showDetails();
				this.Cursor = Cursors.Default;

				if (ChangeCoin != null)
					ChangeCoin(this, new CoinEventArgs(Index, null, 0));
			}
		}

		/// <summary>
		/// Letztes Element der Liste Aufrufen
		/// </summary>
		/// 
		private void btnLast_Click(object sender, EventArgs e)
		{
			int row = Index;

			if (Index < CoinbookHelper.MuenzkatalogFiltered.Count - 1)
			{
				Index = CoinbookHelper.MuenzkatalogFiltered.Count - 1;
				Guid = CoinbookHelper.MuenzkatalogFiltered[Index].GUID;
				this.Cursor = Cursors.WaitCursor;
				loadDetails();
				showDetails();
				this.Cursor = Cursors.Default;

				for (int i = row; i <= 33; i++)
					if (ChangeCoin != null)
						ChangeCoin(this, new CoinEventArgs(i, null, 0));
			}
		}

		#endregion

		private void btnClose_Click(object sender, EventArgs e)
		{
			CoinbookHelper.Settings.MünzTab = tabMuenzDetails.SelectedTab.Name;

			Hide();
			if (HideForm != null)
				HideForm(this, null);
		}

		private void txtPicture_TextChanged(object sender, EventArgs e)
		{
			if (!showOwnPicture || ownPicture == String.Empty)
			{
				if (CoinbookHelper.MuenzkatalogFiltered[Index].Picture != String.Empty)
				{
					string file = Path.Combine(CoinbookHelper.Picturepath, CoinbookHelper.MuenzkatalogFiltered[Index].Picture);
					if (File.Exists(file))
					{
						Bitmap image = new Bitmap(file);
						picMünze.Image = image;
						picMünzeKlein.Image = image;

						var copyright = ImageHelper.GetFullEXIF(file);

						if (copyright.Count == 1)
							lblCopyright.Text = LanguageHelper.Localization.GetTranslation("frmMuenzdetails", "lblCopyright");
						else
							lblCopyright.Text = "Copyright: " + copyright[1].data;
					}
					else
					{
						picMünze.Image = null;
						picMünzeKlein.Image = null;
						lblCopyright.Text = string.Empty;
					}
				}
			}
		}

		public void loadDetails()
		{
			texte = DatabaseHelper.LiteDatabase.GetHinweis(Guid, LanguageHelper.Localization.Language, CoinbookHelper.ModulKey);
			muenzDetails = DatabaseHelper.LiteDatabase.GetMuenzDetails(Guid, CoinbookHelper.ModulKey);
			bestand = DatabaseHelper.LiteDatabase.GetBestand(Guid);

			coinDetails.Clear();

			for (int i = 1; i <= CoinbookHelper.Erhaltungsgrade.Count; i++)
			{
				CoinDetail item = new CoinDetail();

				item.ID = i;
				item.Erhaltungsgrad = CoinbookHelper.Erhaltungsgrade[i - 1].Erhaltung;

				var x = CoinbookHelper.Erhaltungsgrade;
				var y = DatabaseHelper.LiteDatabase.ReadKaufpreise(Guid);

				if (y.ContainsKey(i))
					item.KaufPreis = y[i] == 0 ? string.Empty : string.Format("{0:###,##0.00}", y[i]);

				coinDetails.Add(loadMünzenListe(item, i));
			}

			CoinbookHelper.MünzDetailListe = new BindingList<CoinDetail>(coinDetails);

			var preise = DatabaseHelper.LiteDatabase.ReadEigenePreise(Guid, "Sammlung");


			if (preise.Sum(x => Convert.ToDecimal(x.Preis)) > 0)
			{
				PreisEventArgs args = new PreisEventArgs(Guid, preise[0].Preis, preise[1].Preis, preise[2].Preis,
						 preise[3].Preis, preise[4].Preis, preise[5].Preis, preise[6].Preis, preise[7].Preis, preise[8].Preis);

				CoinbookHelper.CalculateOwnPrices(Index, args);
				Form_PreisChanged(null, args);
			}

            loadSammlungsliste();
			loadDoublettenliste();
		}

		public void showDetails()
		{
			txtPosition.Text = (Index + 1).ToString();
			txtGuid.Text = Guid;

			txtWaehrung.Text = CoinbookHelper.MuenzkatalogFiltered[Index].Waehrung;
			txtNominal.Text = CoinbookHelper.MuenzkatalogFiltered[Index].Nominal;
			txtJahr.Text = CoinbookHelper.MuenzkatalogFiltered[Index].Jahrgang;
			txtMünzzeichen.Text = CoinbookHelper.MuenzkatalogFiltered[Index].Muenzzeichen;
			txtKatNr.Text = CoinbookHelper.MuenzkatalogFiltered[Index].KatNr;
			txtPicture.Text = CoinbookHelper.MuenzkatalogFiltered[Index].Picture;
			txtAuflage.Text = showAuflage(CoinbookHelper.MuenzkatalogFiltered[Index].Auflage);
			txtAuflageSTH.Text = showAuflage(CoinbookHelper.MuenzkatalogFiltered[Index].AuflageSTH);
			txtAuflagePP.Text = showAuflage(CoinbookHelper.MuenzkatalogFiltered[Index].AuflagePP);

            if (!string.IsNullOrEmpty(texte.Motiv))
                txtMotiv.Text = texte.Motiv;
            else
                txtMotiv.Text = CoinbookHelper.MuenzkatalogFiltered[Index].Motiv;

			txtBesonderheit.Text = texte.Besonderheit;
			txtRand.Text = texte.Rand;
			txtAversbeschreibung.Text = texte.Aversbeschreibung;
			txtAversentwurf.Text = texte.AversEntwurf;
			txtÄhnlicheMotive.Text = texte.AehnlicheMotive;
			txtAusgabeanlass.Text = texte.Ausgabeanlass;
			txtKommentare.Text = texte.Kommentar;
			txtReversbeschreibung.Text = texte.Reversbeschreibung;
			txtReversentwurf.Text = texte.ReversEntwurf;
			txtBesonderheiten.Text = texte.Besonderheit;
			txtAusgabeanlass.Text = texte.Ausgabeanlass;
			txtKommentare.Text = texte.Kommentar;
			txtReversbeschreibung.Text = texte.Reversbeschreibung;
			txtReversentwurf.Text = texte.ReversEntwurf;
			txtBesonderheiten.Text = texte.Besonderheit;
			txtPrägeort.Text = texte.Praegeort;

			txtMaterial.Text = muenzDetails.Material;
			txtLegierung.Text = muenzDetails.Legierung;
			txtAusserKurs.Text = muenzDetails.AusserKurs;
			txtGeprägt.Text = muenzDetails.Gepraegt;
			txtInKurs.Text = muenzDetails.InKurs;
			txtGewicht.Text = showNumber(muenzDetails.Gewicht);
			txtDicke.Text = showNumber(muenzDetails.Dicke);
			txtDurchmesser.Text =muenzDetails.Durchmesser;
			txtPicture_TextChanged(null, null);
			txtAusserKursBool.Text = muenzDetails.AusserKursBool ? "ja" : "";
			lblStandText.Text = muenzDetails.Bearbeitungsdatum;
			//txtTyp.Text = muenzDetail.Typ;
			txtForm.Text = muenzDetails.Form;
			txtOrientation.Text = muenzDetails.Orientation;
			txtReferenz.Text = muenzDetails.Referenz;

			katNr = CoinbookHelper.MuenzkatalogFiltered[Index].KatNr;

			try
			{
				if (!string.IsNullOrEmpty(CoinbookHelper.MuenzkatalogFiltered[Index].OwnPicture))
				{
					if (File.Exists(CoinbookHelper.MuenzkatalogFiltered[Index].OwnPicture))
						picOwn.Image = new Bitmap(CoinbookHelper.MuenzkatalogFiltered[Index].OwnPicture);
				}
				else
					picOwn.Image = null;
			}
			catch { }

			grdMuenze.DataSource = CoinbookHelper.MünzDetailListe;

			initMain1();
		}

		private void initMain1()
		{
			grdMuenze.Columns["KatalogPreis"].HeaderStyle.HorizontalAlignment = HorizontalAlignment.Center;
			grdMuenze.Columns["Sammlung"].HeaderStyle.HorizontalAlignment = HorizontalAlignment.Center;
			grdMuenze.Columns["Doubletten"].HeaderStyle.HorizontalAlignment = HorizontalAlignment.Center;
			grdMuenze.Columns["SammlungGesamt"].HeaderStyle.HorizontalAlignment = HorizontalAlignment.Center;
			grdMuenze.Columns["DoublettenGesamt"].HeaderStyle.HorizontalAlignment = HorizontalAlignment.Center;
			grdMuenze.Columns["KaufPreis"].HeaderStyle.HorizontalAlignment = HorizontalAlignment.Center;
			grdMuenze.Columns["Erhaltungsgrad"].HeaderStyle.HorizontalAlignment = HorizontalAlignment.Center;

			grdMuenze.Columns["KatalogPreis"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
			grdMuenze.Columns["Sammlung"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
			grdMuenze.Columns["Doubletten"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
			grdMuenze.Columns["SammlungGesamt"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
			grdMuenze.Columns["DoublettenGesamt"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
			grdMuenze.Columns["KaufPreis"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
			grdMuenze.Columns["Erhaltungsgrad"].CellStyle.HorizontalAlignment = HorizontalAlignment.Left;

			switch (dpi)
			{
				case 120:
					grdMuenze.Width = 692;
					grdMuenze.Columns["Sammlung"].Width = 80;
					grdMuenze.Columns["Doubletten"].Width = 80;
					grdMuenze.Columns["KatalogPreis"].Width = 100;
					grdMuenze.Columns["SammlungGesamt"].Width = 110;
					grdMuenze.Columns["DoublettenGesamt"].Width = 110;
					break;

				default:
					grdMuenze.Columns["Sammlung"].Width = 75;
					grdMuenze.Columns["Doubletten"].Width = 75;
					grdMuenze.Columns["KatalogPreis"].Width = 90;
					grdMuenze.Columns["SammlungGesamt"].Width = 100;
					grdMuenze.Columns["DoublettenGesamt"].Width = 100;
					break;
			}

			grdMuenze.Style.HeaderStyle.Borders.Bottom = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.Thin);
			grdMuenze.Style.HeaderStyle.Borders.Left = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);

			grdMuenze.Style.HeaderStyle.BackColor = CoinbookHelper.ColorHeader;
			grdMuenze.Style.HeaderStyle.Font.Bold = true;
			grdMuenze.Style.HeaderStyle.Font.Size = 9;
			grdMuenze.HeaderRowHeight = 40;
		}

		private string showAuflage(string value)
		{
			string text = value != null ? value : string.Empty;
			long temp;

			bool result = Int64.TryParse(text.Replace(".", String.Empty), out temp);
			if (result)
				text = String.Format(cultureInfo, "{0:###,###,###}", temp);

			if (text == "n/a")
				text = LanguageHelper.Localization.GetTranslation("frmMain", "TTAuflage");

			if (text == "n/u")
				text = LanguageHelper.Localization.GetTranslation("frmMain", "TTAuflagenu");

			if (text == "n/k")
				text = LanguageHelper.Localization.GetTranslation("frmMain", "TTAuflagekn");
			return text;
		}

		private string showNumber(Decimal value)
		{
			string text = string.Empty;

			if (value != 0)
				text = String.Format("{0:####0.000}", value).Replace(",", cultureInfo.NumberFormat.PercentDecimalSeparator);

			return text;
		}

		private void FormMünze_ChangeCoin(object sender, CoinEventArgs args)
		{

			if (args.Coin.Doublette)
			{
				int temp = (ConvertEx.ToInt32(CoinbookHelper.MünzDetailListe[args.Coin.Erhaltung - 1].Doubletten) + args.Anzahl);
				CoinbookHelper.MünzDetailListe[args.Coin.Erhaltung - 1].Doubletten = temp != 0 ? temp.ToString() : string.Empty;
			}
			else
			{
				int temp = (ConvertEx.ToInt32(CoinbookHelper.MünzDetailListe[args.Coin.Erhaltung - 1].Sammlung) + args.Anzahl);
				CoinbookHelper.MünzDetailListe[args.Coin.Erhaltung - 1].Sammlung = temp != 0 ? temp.ToString() : string.Empty;
			}

			berechneSummen(CoinbookHelper.MünzDetailListe[args.Coin.Erhaltung - 1], args.Coin.Erhaltung);

			grdMuenze.Refresh();

			if (ChangeBestand != null)
				ChangeBestand(this, new CoinEventArgs(Index, args.Coin, args.Anzahl));
		}

		private void grdSammlung_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (grdSammlung.Columns[e.ColumnIndex].Name == "btnSammlung")
			{
				frmMünze formAdd = new frmMünze();

				Sammlung coin = CoinbookHelper.SammlungListe[e.RowIndex];
				coin.Guid = Guid;
				formAdd.Coin = coin;

				formAdd.Nation = lblNationText.Text;
				formAdd.Gebiet = lblGebietText.Text;
				formAdd.Ära = lblÄraText.Text;
				formAdd.Index = Index;
				formAdd.Edit = true;

				formAdd.ShowDialog(this);

				if (formAdd.DialogResult == DialogResult.OK)
				{
					Thread.Sleep(500);
					showDetails();
				}
			}
		}

		private void grdDoubletten_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (grdDoubletten.Columns[e.ColumnIndex].Name == "btnEditDoublette")
			{
				frmMünze formAdd = new frmMünze();

				Sammlung coin = CoinbookHelper.DoublettenListe[e.RowIndex];
				coin.Guid = Guid;
				coin.Doublette = true;
				formAdd.Coin = coin;

				formAdd.Nation = lblNationText.Text;
				formAdd.Gebiet = lblGebietText.Text;
				formAdd.Ära = lblÄraText.Text;
				formAdd.Index = Index;
				formAdd.Edit = true;

				formAdd.ShowDialog(this);

				if (formAdd.DialogResult == DialogResult.OK)
				{
					Thread.Sleep(500);
					showDetails();
				}
			}
		}

		private void btnPicture_Click(object sender, EventArgs e)
		{
			frmPicture form = new frmPicture();
			form.ChangePicture += Form_ChangePicture;

			form.Guid = Guid;
			EigeneBilder pic = DatabaseHelper.LiteDatabase.GetOwnPicture(Guid);

			form.Anzeige = pic.ShowPicture;
			form.Picture = pic.DateiName;

			form.ShowDialog(this);

			form.ChangePicture -= Form_ChangePicture;

		}

		private void Form_ChangePicture(object sender, PictureEventArgs args)
		{
			switch (args.Action)
			{
				case PictureAction.Insert:
				case PictureAction.Replace:
					CoinbookHelper.Muenzkatalog1[Index].OwnPicture = args.Bild.DateiName;
					picOwn.Image = new Bitmap(args.Bild.DateiName);
					break;

				case PictureAction.Delete:
					CoinbookHelper.Muenzkatalog1[Index].OwnPicture = string.Empty;
					picOwn.Image = null;
					break;
			}

			if (ChangeOwnPicture != null)
				ChangeOwnPicture(this, null);
		}

		private void ShowPicBig(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				tabMuenzDetails.SelectedTab = this.tabBild;
		}

		/// <summary>
		/// MouseDoubleClick Pic groß
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GetClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				tabMuenzDetails.SelectedTab = this.tabDetails;
		}

		private void NavDirekt(object sender, KeyEventArgs e)
		{
			int pos = ConvertEx.ToInt32(txtPosition.Text);

			if (pos > 0 && pos <= CoinbookHelper.MuenzkatalogFiltered.Count)
			{
				Index = pos - 1;
				Cursor = Cursors.WaitCursor;
				Guid = CoinbookHelper.MuenzkatalogFiltered[Index].GUID;
				loadDetails();
				showDetails();
				Cursor = Cursors.Default;

				if (ChangeCoin != null)
					ChangeCoin(this, new CoinEventArgs(Index, null, 0));
			}
		}

		private void btnPreise_Click(object sender, EventArgs e)
		{
			frmEigenePreise form = new frmEigenePreise();
			form.GUID = Guid;
			form.Nation = lblNationText.Text;
			form.Ära = lblÄraText.Text;
			form.Gebiet = lblGebietText.Text;
			form.Nennwert = txtNominal.Text;
			form.Währung = txtWaehrung.Text;
			form.Münzzeichen = txtMünzzeichen.Text;
			form.Jahr = txtJahr.Text;

			form.PreisChanged += Form_PreisChanged;

			form.ShowDialog(this);

			form.PreisChanged -= Form_PreisChanged;
		}

		private void btnAuktion_Click(object sender, EventArgs e)
		{
			frmAuktion form = new frmAuktion();

			form.GUID = Guid;
			form.Nation = lblNationText.Text;
			form.Ära = lblÄraText.Text;
			form.Gebiet = lblGebietText.Text;
			form.Nennwert = txtNominal.Text;
			form.Währung = txtWaehrung.Text;
			form.Münzzeichen = txtMünzzeichen.Text;
			form.Jahr = txtJahr.Text;

			form.ShowDialog(this);
		}

		private void btnKatalognummern_Click(object sender, EventArgs e)
		{
			frmKatalogNummer form = new frmKatalogNummer();

			form.ChangeKatalogNumber += Form_ChangeKatalogNumber;

			form.CoinbookKatNr = CoinbookHelper.MuenzkatalogFiltered[Index].KatNr;
			form.NationID = CoinbookHelper.MuenzkatalogFiltered[Index].NationID;
			form.CoinbookOriginal = CoinbookHelper.MuenzkatalogFiltered[Index].OriginalKatNr;

			form.ShowDialog(this);

			form.ChangeKatalogNumber -= Form_ChangeKatalogNumber;
		}
		private void initTooltip()
		{
			grdMuenze.ToolTipOption.ToolTipMode = ToolTipMode.TrimmedCells;

			grdMuenze.Columns["btnSammlungAdd"].ShowToolTip = true;
			grdMuenze.Columns["Erhaltungsgrad"].ShowToolTip = true;
			grdMuenze.Columns["KatalogPreis"].ShowToolTip = true;

			grdMuenze.ToolTipOption.InitialDelay = 1000;
			grdMuenze.ToolTipOption.AutoPopDelay = 7000;

			grdMuenze.Style.ToolTipStyle.BackColor = Color.LightYellow;
			grdMuenze.Style.ToolTipStyle.ForeColor = Color.Black;
			grdMuenze.Style.ToolTipStyle.BorderThickness = 1;
			grdMuenze.Style.ToolTipStyle.BorderColor = Color.Black;
			grdMuenze.Style.ToolTipStyle.Font = new Font("Arial", 10, FontStyle.Bold);

			grdMuenze.ToolTipOption.ShadowVisible = true;
			grdMuenze.ToolTipOption.ToolTipMode = ToolTipMode.AllCells;

			grdMuenze.ShowToolTip = true;
		}

		private void grdMuenze_ToolTipOpening(object sender, ToolTipOpeningEventArgs e)
		{
			try
			{
				if (e.ToolTipInfo.Items.Count > 0)
				{
					switch (e.Column.MappingName)
					{
						case "Erhaltungsgrad":
							e.ToolTipInfo.Items[0].Text = "Erhaltungsgrad '" + CoinbookHelper.Erhaltungsgrade[e.RowIndex - 1].Bezeichnung + "'";
							break;

						case "btnSammlungAdd":
							e.ToolTipInfo.Items[0].Text = LanguageHelper.Localization.GetTranslation(Name, "btnSammlungAddToolTip");
							break;

						case "KatalogPreis":
							if (coinDetails[e.RowIndex].Farbe == Color.Yellow)
								e.ToolTipInfo.Items[0].Text = "Eigener Preis";
							else if (coinDetails[e.RowIndex].Farbe == Color.GreenYellow)
								e.ToolTipInfo.Items[0].Text = "Liebhaberpreis " + coinDetails[e.RowIndex].LiebhaberPreisStand;
							else
								e.ToolTipInfo.Items[0].Text = string.Empty;
							break;

						default:
							e.ToolTipInfo.Items[0].Text = string.Empty;
							break;
					}
				}
			}
			catch { }
		}

		private void setColumnHeaders()
		{
			grdMuenze.Columns["KatalogPreis"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colPreis") + "[" + CoinbookHelper.Settings.CurrentWährung + "]";
			grdMuenze.Columns["SammlungGesamt"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colSammlungGesamt") + "[" + CoinbookHelper.Settings.CurrentWährung + "]";
			grdMuenze.Columns["DoublettenGesamt"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colDoublettenGesamt") + "[" + CoinbookHelper.Settings.CurrentWährung + "]";
			grdMuenze.Columns["KaufPreis"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colKaufpreis") + "[" + CoinbookHelper.Settings.CurrentWährung + "]";
			grdMuenze.Columns["Erhaltungsgrad"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colErhaltungsgrad");
			grdMuenze.Columns["Sammlung"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colSammlung");
			grdMuenze.Columns["Doubletten"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colDoubletten");
			grdMuenze.Columns["btnSammlungAdd"].HeaderText = "";

			grdSammlung.Columns["colSammlungErhaltungsgrad"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colErhaltungsgrad");
			grdSammlung.Columns["colKaufpreisSammlung"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colKaufpreis") + "[" + CoinbookHelper.Settings.CurrentWährung + "]";
			grdSammlung.Columns["colKatalogPreisSammlung"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colKatalogPreis") + "[" + CoinbookHelper.Settings.CurrentWährung + "]";

			grdDoubletten.Columns["colDoubletteErhaltungsgrad"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colErhaltungsgrad");
			grdDoubletten.Columns["colKaufpreisDoublette"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colKaufpreis") + "[" + CoinbookHelper.Settings.CurrentWährung + "]";
			grdDoubletten.Columns["colKatalogPreisDoublette"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colKatalogPreis") + "[" + CoinbookHelper.Settings.CurrentWährung + "]";
		}

		private void grdSammlung_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == 0)
			{
				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				e.Graphics.DrawIcon(editIcon, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
				e.Handled = true;
			}

			if (e.RowIndex >= 0 && e.ColumnIndex == 7)
			{
				if ((int)grdSammlung.Rows[e.RowIndex].Cells["colSammlungFarbe"].Value == 1)
					grdSammlung.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Yellow;
			}
		}

		private void grdDoubletten_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == 0)
			{
				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				e.Graphics.DrawIcon(editIcon, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
				e.Handled = true;
			}

			if (e.RowIndex >= 0 && e.ColumnIndex == 7)
			{
				if ((int)grdDoubletten.Rows[e.RowIndex].Cells["colDoublettenFarbe"].Value == 1)
					grdDoubletten.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Yellow;
			}
		}

		private void loadSammlungsliste()
		{
			CoinbookHelper.SammlungListe = CoinbookHelper.LoadSammlungsliste(Guid, Index, LiteDB.Database.enmDoublette.Sammlung);

			grdSammlung.DataSource = CoinbookHelper.SammlungListe;

			grdSammlung.Columns["colKaufpreisSammlung"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			grdSammlung.Columns["colKatalogpreisSammlung"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			grdSammlung.Columns["colKaufpreisSammlung"].DefaultCellStyle.FormatProvider = cultureInfo;
			grdSammlung.Columns["colKatalogpreisSammlung"].DefaultCellStyle.FormatProvider = cultureInfo;
			grdSammlung.Columns["colSammlungKaufdatum"].DefaultCellStyle.FormatProvider = cultureInfo;

			grdSammlung.Columns["colSammlungKaufdatum"].DefaultCellStyle.Format = cultureInfo.DateTimeFormat.ShortDatePattern;

			grdSammlung.Columns["colKaufpreisSammlung"].Visible = true;

			if (dpi == 120)
			{
				grdSammlung.Columns["colSammlungErhaltungsgrad"].Width = 60;
				grdSammlung.Columns["colSammlungKaufdatum"].Width = 90;
				grdSammlung.Columns["colSammlungKatNr"].Width = 115;
			}
			else
			{
				grdSammlung.Columns["colSammlungErhaltungsgrad"].Width = 50;
				grdSammlung.Columns["colSammlungKaufdatum"].Width = 70;
				grdSammlung.Columns["colSammlungKatNr"].Width = 95;
			}
		}

		private void loadDoublettenliste()
		{
			CoinbookHelper.DoublettenListe = CoinbookHelper.LoadSammlungsliste(Guid, Index, LiteDB.Database.enmDoublette.Doublette);

			grdDoubletten.DataSource = CoinbookHelper.DoublettenListe;

			grdDoubletten.Columns["btnEditDoublette"].HeaderText = "";

			grdDoubletten.Columns["colKaufpreisDoublette"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			grdDoubletten.Columns["colKatalogpreisDoublette"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

			grdDoubletten.Columns["colKaufpreisDoublette"].DefaultCellStyle.FormatProvider = cultureInfo;
			grdDoubletten.Columns["colKatalogpreisDoublette"].DefaultCellStyle.FormatProvider = cultureInfo;
			grdDoubletten.Columns["colDoubletteKaufdatum"].DefaultCellStyle.FormatProvider = cultureInfo;

			grdDoubletten.Columns["colDoubletteKaufdatum"].DefaultCellStyle.Format = cultureInfo.DateTimeFormat.ShortDatePattern;

			grdDoubletten.Columns["colKatalogpreisDoublette"].Visible = true;

			if (dpi == 120)
			{
				grdDoubletten.Columns["colDoubletteErhaltungsgrad"].Width = 60;
				grdDoubletten.Columns["colDoubletteKaufdatum"].Width = 90;
				grdDoubletten.Columns["colDoubletteKatNrEigen"].Width = 115;
			}
			else
			{
				grdDoubletten.Columns["colDoubletteErhaltungsgrad"].Width = 50;
				grdDoubletten.Columns["colDoubletteKaufdatum"].Width = 70;
				grdDoubletten.Columns["colDoubletteKatNrEigen"].Width = 95;
			}
		}

		public CoinDetail loadMünzenListe(CoinDetail item, int erhaltungsgrad)
		{
			switch (erhaltungsgrad)
			{
				case 1:
					item.Sammlung = bestand.S == 0 ? string.Empty : bestand.S.ToString();
					item.Doubletten = bestand.DS == 0 ? string.Empty : bestand.DS.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPS;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].SPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].SPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;

				case 2:
					item.Sammlung = bestand.SP == 0 ? string.Empty : bestand.SP.ToString();
					item.Doubletten = bestand.DSP == 0 ? string.Empty : bestand.DSP.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPSP;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].SPPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].SPPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;

				case 3:
					item.Sammlung = bestand.SS == 0 ? string.Empty : bestand.SS.ToString();
					item.Doubletten = bestand.DSS == 0 ? string.Empty : bestand.DSS.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPSS;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].SSPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].SSPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;

				case 4:
					item.Sammlung = bestand.SSP == 0 ? string.Empty : bestand.SSP.ToString();
					item.Doubletten = bestand.DSSP == 0 ? string.Empty : bestand.DSSP.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPSSP;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].SSPPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].SSPPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;

				case 5:
					item.Sammlung = bestand.VZ == 0 ? string.Empty : bestand.VZ.ToString();
					item.Doubletten = bestand.DVZ == 0 ? string.Empty : bestand.DVZ.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPVZ;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].VZPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].VZPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;

				case 6:
					item.Sammlung = bestand.VZP == 0 ? string.Empty : bestand.VZP.ToString();
					item.Doubletten = bestand.DVZP == 0 ? string.Empty : bestand.DVZP.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPVZP;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].VZPPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].VZPPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;

				case 7:
					item.Sammlung = bestand.STN == 0 ? string.Empty : bestand.STN.ToString();
					item.Doubletten = bestand.DSTN == 0 ? string.Empty : bestand.DSTN.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPSTN;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].STNPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].STNPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;

				case 8:
					item.Sammlung = bestand.STH == 0 ? string.Empty : bestand.STH.ToString();
					item.Doubletten = bestand.DSTH == 0 ? string.Empty : bestand.DSTH.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPSTH;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].STHPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].STHPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;

				case 9:
					item.Sammlung = bestand.PP == 0 ? string.Empty : bestand.PP.ToString();
					item.Doubletten = bestand.DPP == 0 ? string.Empty : bestand.DPP.ToString();
					item.Liebhaberpreis = CoinbookHelper.MuenzkatalogFiltered[Index].LPPP;
					item.LiebhaberPreisStand = muenzDetails.LPStandS;
					item.KatalogPreis = CoinbookHelper.MuenzkatalogFiltered[Index].PPPreis == 0
										? string.Empty
										: string.Format("{0:###,##0.00}", CoinbookHelper.MuenzkatalogFiltered[Index].PPPreis * CoinbookHelper.Settings.CurrentFaktor);
					break;
			}

			berechneSummen(item, erhaltungsgrad);

			return item;
		}

		private void berechneSummen(CoinDetail item, int erhaltungsgrad)
		{
			item.SammlungGesamt = string.Empty;
			if (item.Sammlung != string.Empty)
			{
				decimal katalogPreis = 0;
				if (!string.IsNullOrEmpty(item.KatalogPreis))
					katalogPreis = Convert.ToDecimal(item.KatalogPreis);

				var betrag = Convert.ToDecimal(item.Sammlung) * katalogPreis;
				if (betrag != 0)
					item.SammlungGesamt = string.Format("{0:###,##0.00}", betrag);
				else
					item.SammlungGesamt = string.Empty;
			}

			item.DoublettenGesamt = string.Empty;
			if (item.Doubletten != string.Empty)
			{
				decimal katalogPreis = 0;
				if (!string.IsNullOrEmpty(item.KatalogPreis))
					katalogPreis = Convert.ToDecimal(item.KatalogPreis);

				var betrag = Convert.ToDecimal(item.Doubletten) * katalogPreis;
				if (betrag != 0)
					item.DoublettenGesamt = string.Format("{0:###,##0.00}", betrag);
				else
					item.DoublettenGesamt = string.Empty;
			}
		}

		private void grdMuenze_DrawCell(object sender, Syncfusion.WinForms.DataGrid.Events.DrawCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (e.RowIndex == 9 && CoinbookHelper.MuenzkatalogFiltered[Index].AuflagePP == "n/a" && CoinbookHelper.MünzDetailListe[8].KatalogPreis == string.Empty)
						e.Style.BackColor = Color.Gray;

					if (e.RowIndex == 8 && CoinbookHelper.MuenzkatalogFiltered[Index].AuflageSTH == "n/a" && CoinbookHelper.MünzDetailListe[7].KatalogPreis == string.Empty)
						e.Style.BackColor = Color.Gray;

					if (e.RowIndex == 7 && CoinbookHelper.MuenzkatalogFiltered[Index].Auflage == "n/a" && CoinbookHelper.MünzDetailListe[6].KatalogPreis == string.Empty)
						e.Style.BackColor = Color.Gray;

					if (e.Style.BackColor != Color.Gray)
					{
						switch (grdMuenze.Columns[e.ColumnIndex].MappingName)
						{
							case "btnSammlungAdd":
								showIcon(addIcon, e.Bounds, e.Graphics, e.Style.BackColor);
								e.Handled = true;
								break;

							case "KatalogPreis":
								if (coinDetails[e.RowIndex - 1].Farbe != null)
									e.Style.BackColor = coinDetails[e.RowIndex - 1].Farbe;
								break;

							case "SammlungGesamt":
							case "DoublettenGesamt":
								if (coinDetails[e.RowIndex - 1].Farbe != null && e.DisplayText != string.Empty)
									e.Style.BackColor = coinDetails[e.RowIndex - 1].Farbe;
								break;
						}
					}
				}
			}
			catch { }
		}

		private void showIcon(Icon icon, Rectangle rec, Graphics graphics, Color backcolor)
		{
			Rectangle r = new Rectangle(rec.Left + 1, rec.Top + 1, rec.Width - 2, rec.Height - 2);
			graphics.FillRectangle(new SolidBrush(backcolor), r);

			r = new Rectangle(rec.Left, rec.Top, rec.Width - 1, rec.Height - 1);
			graphics.DrawRectangle(new Pen(Color.Black, 1), r);

			int width = (rec.Width - icon.Width) / 2;
			if (width < 0)
				width = 0;

			int height = (rec.Height - icon.Height) / 2;
			if (height < 0)
				Height = 0;

			r = new Rectangle(rec.Left + width, rec.Top + height, rec.Width = icon.Width, rec.Height = icon.Height);
			graphics.DrawIcon(icon, r);
		}

		private void grdMuenze_CellClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
		{
			try
			{
				if (e.DataRow.RowType == RowType.DefaultRow && e.MouseEventArgs.Button == MouseButtons.Left)
				{
					if (e.DataRow.RowIndex == 9 && CoinbookHelper.MuenzkatalogFiltered[Index].AuflagePP == "n/a")
						return;

					if (e.DataRow.RowIndex == 8 && CoinbookHelper.MuenzkatalogFiltered[Index].AuflageSTH == "n/a")
						return;

					if (e.DataRow.RowIndex == 7 && CoinbookHelper.MuenzkatalogFiltered[Index].Auflage == "n/a")
						return;

					if (e.DataColumn.GridColumn.MappingName == "btnSammlungAdd")
					{
						Sammlung coin = new Sammlung();

						coin.Guid = Guid;
						//coin.ID = -1;
						coin.Erhaltung = e.DataRow.RowIndex;
						coin.KatNr = txtKatNr.Text;

						frmMünze formAdd = new frmMünze();
						formAdd.ChangeCoin += FormMünze_ChangeCoin;

						formAdd.Nation = lblNationText.Text;
						formAdd.Gebiet = lblGebietText.Text;
						formAdd.Ära = lblÄraText.Text;
						formAdd.Coin = coin;
						formAdd.Index = Index;

						formAdd.ShowDialog(this);

						formAdd.ChangeCoin -= FormMünze_ChangeCoin;

						grdMuenze.Refresh();

						loadSammlungsliste();
						loadDoublettenliste();

						grdSammlung.Refresh();
						grdDoubletten.Refresh();
					}
				}
			}
			catch (SystemException ex)
			{
				MessageBox.Show(ex.Message + " in grdMuenze_CellClick");
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			frmMünzeDelete formDel = new frmMünzeDelete();
			formDel.ChangeCoin += FormMünze_ChangeCoin;

			formDel.Nation = lblNationText.Text;
			formDel.Gebiet = lblGebietText.Text;
			formDel.Ära = lblÄraText.Text;
			formDel.Index = Index;
			formDel.GUID = Guid;

			formDel.ShowDialog(this);

			formDel.ChangeCoin -= FormMünze_ChangeCoin;
		}

		private void Form_ChangeKatalogNumber(object sender, KatalognummerEventArgs args)
		{
			if (args.Action == enmKatalogAction.Neu)
				txtKatNr.Text = args.New;
			else
			{
				args.New = CoinbookHelper.MuenzkatalogFiltered[Index].OriginalKatNr;
				txtKatNr.Text = CoinbookHelper.MuenzkatalogFiltered[Index].OriginalKatNr;
			}

			if (ChangeKatalogNumber != null)
				ChangeKatalogNumber(this, args);
		}

		private void Form_PreisChanged(object sender, PreisEventArgs args)
		{
			if (PreisChanged != null)
				PreisChanged(this, args);

			if (args.SPreis != 0)
			{
				coinDetails[0].KatalogPreis = string.Format("{0:#,###.00}", args.SPreis);
				coinDetails[0].Farbe = Color.Yellow;
			}

			if (args.SPPreis != 0)
			{
				coinDetails[1].KatalogPreis = string.Format("{0:#,###.00}", args.SPPreis);
				coinDetails[1].Farbe = Color.Yellow;
			}

			if (args.SSPreis != 0)
			{
				coinDetails[2].KatalogPreis = string.Format("{0:#,###.00}", args.SSPreis);
				coinDetails[2].Farbe = Color.Yellow;
			}

			if (args.SSPPreis != 0)
			{
				coinDetails[3].KatalogPreis = string.Format("{0:#,###.00}", args.SSPPreis);
				coinDetails[3].Farbe = Color.Yellow;
			}

			if (args.VZPreis != 0)
			{
				coinDetails[4].KatalogPreis = string.Format("{0:#,###.00}", args.VZPreis);
				coinDetails[4].Farbe = Color.Yellow;
			}

			if (args.VZPPreis != 0)
			{
				coinDetails[5].KatalogPreis = string.Format("{0:#,###.00}", args.VZPPreis);
				coinDetails[5].Farbe = Color.Yellow;
			}

			if (args.STNPreis != 0)
			{
				coinDetails[6].KatalogPreis = string.Format("{0:#,###.00}", args.STNPreis);
				coinDetails[6].Farbe = Color.Yellow;
			}

			if (args.STHPreis != 0)
			{
				coinDetails[7].KatalogPreis = string.Format("{0:#,###.00}", args.STHPreis);
				coinDetails[7].Farbe = Color.Yellow;
			}

			if (args.PPPreis != 0)
			{
				coinDetails[8].KatalogPreis = string.Format("{0:#,###.00}", args.PPPreis);
				coinDetails[8].Farbe = Color.Yellow;
			}

			for (int i = 0; i < 9; i++)
			{
				if (ConvertEx.ToDecimal(coinDetails[i].Sammlung) != 0)
					coinDetails[i].SammlungGesamt = string.Format("{0:#,###.00}",
						ConvertEx.ToDecimal(coinDetails[i].KatalogPreis) * ConvertEx.ToDecimal(coinDetails[i].Sammlung));
				else
					coinDetails[i].SammlungGesamt = string.Empty;

				if (ConvertEx.ToDecimal(coinDetails[i].Doubletten) != 0)
					coinDetails[i].DoublettenGesamt = string.Format("{0:#,###.00}",
						ConvertEx.ToDecimal(coinDetails[i].KatalogPreis) * ConvertEx.ToDecimal(coinDetails[i].Doubletten));
				else
					coinDetails[i].DoublettenGesamt = string.Empty;
			}

			grdMuenze.Refresh();

			if (PreisChanged != null)
				PreisChanged(Index, args);
		}

		private void picMünzeKlein_MouseEnter(object sender, EventArgs e)
		{
			string text = string.Empty;

			string file = Path.Combine(CoinbookHelper.Picturepath, txtPicture.Text);
			var copyright = ImageHelper.GetFullEXIF(file);

			if (copyright.Count == 1)
				text = LanguageHelper.Localization.GetTranslation("frmMuenzdetails", "lblCopyright");
			else
				text = copyright[1].data.Trim();

			toolTip1.ToolTipTitle = "Copyright";
			toolTip1.SetToolTip(picMünzeKlein, text);
		}

		private void btnPicture_MouseEnter(object sender, EventArgs e)
		{
			toolTip1.ToolTipTitle = btnPicture.Text;
			toolTip1.SetToolTip(btnPicture, LanguageHelper.Localization.GetTranslation(Name, "infoImage"));
		}

		private void btnAuktionen_MouseEnter(object sender, EventArgs e)
		{
			toolTip1.ToolTipTitle = btnAuktionen.Text;
			toolTip1.SetToolTip(btnAuktionen, LanguageHelper.Localization.GetTranslation(Name, "infoAuktion"));
		}

		private void btnPreise_MouseEnter(object sender, EventArgs e)
		{
			toolTip1.ToolTipTitle = btnPreise.Text;
			toolTip1.SetToolTip(btnPreise, LanguageHelper.Localization.GetTranslation(Name, "infoPreis"));
		}

		private void btnDeleteCoin_MouseEnter(object sender, EventArgs e)
		{
			toolTip1.ToolTipTitle = btnDeleteCoin.Text;
			toolTip1.SetToolTip(btnDeleteCoin, LanguageHelper.Localization.GetTranslation(Name, "infoDelete"));
		}

		private void btnKatalognummern_MouseEnter(object sender, EventArgs e)
		{
			toolTip1.ToolTipTitle = btnKatalognummern.Text;
			toolTip1.SetToolTip(btnKatalognummern, LanguageHelper.Localization.GetTranslation(Name, "infoKatalognummern"));
		}
	}
}

