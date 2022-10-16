using Coinbook.Enumerations;
using Coinbook.Helper;
using Coinbook.Lokalisierung;
using Coinbook.Model;
using SAN.Converter;
using SAN.Control;
using Syncfusion.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Styles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using SAN.UIControls;
using Syncfusion.WinForms.DataGrid.Interactivity;
using Coinbook.EventHandlers;

namespace Coinbook
{
    public partial class usrMain : UserControl
    {
        public event EventHandler OverviewLoaded;
        //public event EventHandler MünzdetailsShow;
        public event EventHandler EnableEditMenues;
        public event EventHandler AddMünze;
        public event EventHandler MünzeLöschen;
        public event EventHandler OwnCatalog;
        public event EventHandler OwnPicture;
        public event EventHandler EigenePreise;
        public event EventHandler Auktionen;
        public event EventHandler Prägeanstalten;
        public event EventHandler CoinNext;
        public event EventHandler CoinPrevious;
        public event EventHandler ChangeRecordCount;

        private int dpi = 0;
        private CultureInfo cultureInfo;
        private int currentRow = 0;
        private const int headerRows = 2;
        private Icon coin = null;

        Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
        Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfoAuflage = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
        Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfoComment = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();

        private frmMuenzDetails formMünzdetails;

        public usrMain()
        {
            InitializeComponent();

            if (LanguageHelper.Localization != null)
                LanguageHelper.Localization.UpdateModul(this, "frmMain");

            btnNavigate.Enabled = false;

            formMünzdetails = new frmMuenzDetails();
            formMünzdetails.ChangeOwnPicture += FormMünzdetails_ChangeOwnPicture;
            formMünzdetails.ChangeBestand += FormMünzdetails_ChangeBestand;
            formMünzdetails.ChangeCoin += FormMünzdetails_ChangeCoin;
            formMünzdetails.ChangeKatalogNumber += FormMünzdetails_ChangeKatalogNumber;
            formMünzdetails.HideForm += FormMünzdetails_HideForm;
            formMünzdetails.PreisChanged += FormMünzdetails_PreisChanged;
        }

        private void FormMünzdetails_PreisChanged(object sender, PreisEventArgs args)
        {
            CoinbookHelper.CalculateOwnPrices(currentRow, args);
            grdMain1.Refresh();
        }

        private void FormMünzdetails_HideForm(object sender, EventArgs e)
        {
            panCoins.Enabled = true;
        }

        public int Index { get { return currentRow; } }

        public void FillNations()
        {
            cultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);

            picMünze.PictureName = string.Empty;
            picEigen.PictureName = string.Empty;

            cboNationen.DisplayMember = "Bezeichnung";
            cboNationen.ValueMember = "ID";

            cboÄra.DisplayMember = "Bezeichnung";
            cboÄra.ValueMember = "ID";

            cboGebiete.DisplayMember = "Bezeichnung";
            cboGebiete.ValueMember = "id";

            cboWährung.DisplayMember = "Währung";
            cboWährung.ValueMember = "Währung";

            cboNominale.DisplayMember = "Nominal";
            cboNominale.ValueMember = "Nominal";

            cboJahr.DisplayMember = "Jahrgang";
            cboJahr.ValueMember = "Jahrgang";

            cboNationen.DataSource = CoinbookHelper.Nationen;
            //cboNationen.SelectedValue = CoinbookHelper.Settings.Nation;

            //cboWährung.Enabled = false;
            //cboNominale.Enabled = false;
            //cboJahr.Enabled = false;
        }

        public void Init()
        {
            string coinName = Path.Combine(Path.Combine(Application.StartupPath, "Images"), "Coin.ico");
            coin = new Icon(CoinbookHelper.Coin, new Size(15, 15));

            Graphics graphics;
            graphics = this.CreateGraphics();
            dpi = ConvertEx.ToInt32(graphics.DpiX);

            cboNationen.DataSource = CoinbookHelper.Nationen;
            if (CoinbookHelper.Settings.Nation != 0)
                cboNationen.SelectedValue = CoinbookHelper.Settings.Nation;
            else
                cboNationen.SelectedIndex = 0;

            if (cboNationen.SelectedValue != null)
            {
                cboÄra.DataSource = DatabaseHelper.LiteDatabase.ReadAeras(CoinbookHelper.ModulKey,(int)cboNationen.SelectedValue);
                if (CoinbookHelper.Settings.Ära != 0)
                    cboÄra.SelectedValue = CoinbookHelper.Settings.Ära;
                else
                    if (cboÄra.Items.Count >0 )
                        cboÄra.SelectedIndex = 0;
            }

            if (cboÄra.SelectedValue != null)
            {
                cboGebiete.DataSource = DatabaseHelper.LiteDatabase.ReadRegions(CoinbookHelper.ModulKey, (int)cboÄra.SelectedValue);
                if (CoinbookHelper.Settings.Gebiet != 0)
                {
                    cboGebiete.SelectedValue = CoinbookHelper.Settings.Gebiet;
                    if (cboGebiete.SelectedValue == null)
                        cboGebiete.SelectedIndex = 0;
                }
                else
                    cboGebiete.SelectedIndex = 0;
            }
            else
                cboGebiete.SelectedIndex = -1;

            //Helper.HauptFilter.AeraID = Helper.Settings.Ära;
            //Helper.HauptFilter.GebietID = Helper.Settings.Nation;
            //Helper.HauptFilter.NationID = Helper.Settings.Gebiet;

                //ShowListe();
                //loadFilter();

                //cboWährung.Enabled = true;
                //cboNominale.Enabled = true;
                //cboJahr.Enabled = true;

            //if (cboNationen.SelectedIndex != -1 && cboÄra.SelectedIndex != -1 && cboGebiete.SelectedIndex != -1)
                Navigate();

            CreateStackHeader();

            //switch (dpi)
            //{
            //    case 120: //For 125% fonts
            //        splC.SplitterDistance = 130;
            //        break;

            //    case 144: //For 150% fonts
            //        splC.SplitterDistance = 160;
            //        break;

            //    default:
            //        break;
            //}

            initTooltip();

            toolTipInfoComment.BackColor = Color.Yellow;
            toolTipInfoComment.Header.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            toolTipInfoComment.Header.Image = new Bitmap(Path.Combine(Application.StartupPath, "Icon.ico"));
            toolTipInfoComment.Header.TextAlign = ContentAlignment.MiddleLeft;
            toolTipInfoComment.Header.TextImageRelation = Syncfusion.Windows.Forms.Tools.ToolTipTextImageRelation.ImageBeforeText;
            toolTipInfoComment.Separator = true;

            toolTipInfoAuflage.BackColor = Color.DeepSkyBlue;
            toolTipInfoAuflage.Header.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            toolTipInfoAuflage.Header.Image = new Bitmap(Path.Combine(Application.StartupPath, "Icon.ico"));
            toolTipInfoAuflage.Header.TextAlign = ContentAlignment.MiddleLeft;
            toolTipInfoAuflage.Header.TextImageRelation = Syncfusion.Windows.Forms.Tools.ToolTipTextImageRelation.ImageBeforeText;
            toolTipInfoAuflage.Separator = true;

            toolTipInfo.BackColor = Color.LightYellow;
            toolTipInfo.Header.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            toolTipInfo.Header.Image = new Bitmap(Path.Combine(Application.StartupPath, "Icon.ico"));
            toolTipInfo.Header.TextAlign = ContentAlignment.MiddleLeft;
            toolTipInfo.Header.TextImageRelation = Syncfusion.Windows.Forms.Tools.ToolTipTextImageRelation.ImageBeforeText;
            toolTipInfo.Separator = true;

            grdMain1.AllowResizingColumns = true;
            grdMain1.AllowSorting = false;
            tableLayoutPanel1.SetColumnSpan(grdMain1, 7);
            tableLayoutPanel1.SetRowSpan(grdMain1, 2);

            grdMain1.RowHeight = 24;
            grdMain1.Dock = System.Windows.Forms.DockStyle.Fill;

            grdMain1.AutoGenerateColumnsMode = AutoGenerateColumnsMode.SmartReset;
            grdMain1.Style.CellStyle.Font = new GridFontInfo(new Font("Arial", 10));
            grdMain1.ShowHeaderToolTip = true;
            grdMain1.Style.ToolTipStyle.BackColor = Color.LightYellow;
            grdMain1.EnableDataVirtualization = true;

            btnNavigate.Enabled = CoinbookHelper.IsActivated;

            ResizeMotivColumn();
            ShowSelectedStyle();

            LanguageHelper.Localization.UpdateModul(grdMain1, "frmMain");
            SetGridHeaderErhaltungsgrade();

            Cursor = Cursors.Default;
        }

        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int RegionID { get; set; }
        public string NationText { get { return cboNationen.Text; } }
        public string ÄraText { get { return cboÄra.Text; } }
        public string GebietText { get { return cboGebiete.Text; } }
        public string Currency
        {
            get { return cboWährung.Text.Substring(0,1) == "(" ? null : cboWährung.Text; }
        }
        public string Nominal
        {
            get { return cboNominale.Text.Substring(0, 1) == "(" ? null : cboNominale.Text; }
        }
        public string Jahrgang
        {
            get { return cboJahr.Text.Substring(0, 1) == "(" ? null : cboJahr.Text; }
        }



        public Katalog3 SelectedItem
        {
            get
            {
                return (Katalog3)grdMain1.SelectedItem;
            }
        }
        public int RowCount { get { return grdMain1.RowCount; } }

        private void cboNationen_SelectedValueChanged(object sender, EventArgs e)
        {
            cboWährung.Enabled = false;
            cboNominale.Enabled = false;
            cboJahr.Enabled = false;

            if (cboNationen.SelectedValue != null)
            {
                NationID = (int)cboNationen.SelectedValue;
                CoinbookHelper.ModulKey = ((Nation)cboNationen.SelectedItem).Key;

                CoinbookHelper.Aeras = DatabaseHelper.LiteDatabase.ReadAeras(CoinbookHelper.ModulKey);
                CoinbookHelper.Regions = DatabaseHelper.LiteDatabase.ReadRegions(CoinbookHelper.ModulKey);

                if (CoinbookHelper.Muenzkatalog != null)
                {
                    //Helper.Muenzkatalog.Clear();
                    grdMain1.BackColor = Color.White;
                }

                cboÄra.DataSource = CoinbookHelper.GetAeras((int)cboNationen.SelectedValue).ToList();
                if (cboÄra.Items.Count > 0)
                    cboÄra.SelectedIndex = 0;
                cboÄra_SelectedValueChanged(null, null);
            }
        }

        private void cboGebiete_SelectedValueChanged(object sender, EventArgs e)
        {
            cboWährung.Enabled = false;
            cboNominale.Enabled = false;
            cboJahr.Enabled = false;

            if (cboGebiete.SelectedValue != null)
            {
                RegionID = (int)cboGebiete.SelectedValue;
                CoinbookHelper.Settings.Gebiet = RegionID;

                if (CoinbookHelper.Muenzkatalog != null)
                    CoinbookHelper.Muenzkatalog.Clear();

                DatabaseHelper.LiteDatabase.InitListen(CoinbookHelper.ModulKey);
                CoinbookHelper.Currencies = DatabaseHelper.LiteDatabase.ReadCurrencyListe;
                CoinbookHelper.Nominale = DatabaseHelper.LiteDatabase.ReadNominalListe;
                CoinbookHelper.Jahre = DatabaseHelper.LiteDatabase.ReadJahrgangsListe;

                if (cboWährung.Items.Count > 0)
                    cboWährung.SelectedIndex = 0;

                if (cboNominale.Items.Count > 0)
                    cboNominale.SelectedIndex = 0;

                if (cboJahr.Items.Count > 0)
                    cboJahr.SelectedIndex = 0;

                btnNavigate.Enabled = (NationID != -1 && AeraID != -1 && RegionID != -1);

                if (btnNavigate.Enabled)
                    Navigate();

            }
        }

        private void cboÄra_SelectedValueChanged(object sender, EventArgs e)
        {
            cboWährung.Enabled = false;
            cboNominale.Enabled = false;
            cboJahr.Enabled = false;

            if (cboÄra.SelectedValue != null)
            {
                AeraID = (int)cboÄra.SelectedValue;
                CoinbookHelper.Settings.Ära = AeraID;

                cboGebiete.DataSource = CoinbookHelper.GetRegions((int)cboÄra.SelectedValue).ToList();
                cboGebiete.SelectedIndex = 0;
                
                if (CoinbookHelper.Muenzkatalog != null)
                    CoinbookHelper.Muenzkatalog.Clear();
           }
        }

        private void grdMain1_CurrentCellActivating(object sender, CurrentCellActivatingEventArgs e)
        {
            grdMain1.CurrentItem = e.DataRow.RowData;
            showPicture();
            ShowOwnPicture();
        }

        private void grdMain1_ToolTipOpening(object sender, ToolTipOpeningEventArgs e)
        {
            if (e.RowIndex == 1)
                e.ToolTipInfo.Items[0].Text = toolTipHeader(e.Column.MappingName);
            else
                e.ToolTipInfo.Items[0].Text = toolTipTable(e.Column.MappingName, e.DisplayText);
        }

        private void grdMain1_DrawCell(object sender, Syncfusion.WinForms.DataGrid.Events.DrawCellEventArgs e)
        {
            try
            {
                int stackheaderRow = 0;

                if (e.DataRow.RowType == RowType.StackedHeaderRow)
                {
                    stackheaderRow = e.RowIndex;

                    if (e.ColumnIndex == 8)
                        e.Style.BackColor = CoinbookHelper.ColorHeader1;

                    if (e.ColumnIndex == 11)
                        e.Style.BackColor = CoinbookHelper.ColorHeader2;

                    if (e.ColumnIndex == 20)
                        e.Style.BackColor = CoinbookHelper.ColorHeader3;
                }

                if (e.DataRow.RowType == RowType.HeaderRow)
                    stackheaderRow = e.RowIndex;

                if (e.DataRow.RowType == RowType.DefaultRow)
                {
                    int rowNumber = e.RowIndex - grdMain1.StackedHeaderRows.Count - 1;

                    switch (e.Column.MappingName)
                    {
                        case "btnEdit":
                            showIcon(CoinbookHelper.Lupe, e.Bounds, e.Graphics, e.Style.BackColor);
                            e.Handled = true;

                            break;

                        case "HinweisKZ":
                            if (((Katalog3)grdMain1.View.Records.GetItemAt(rowNumber)).HinweisKZ == "!")
                            {
                                showIcon(CoinbookHelper.Hinweis, e.Bounds, e.Graphics, e.Style.BackColor);
                                e.Handled = true;
                            }
                            break;

                        case "PP":
                            {
                                if ((e.DataRow.RowData as Katalog3).AuflagePP == "n/a")
                                    e.Style.BackColor = CoinbookHelper.ColorBlocked;

                                if (CoinbookHelper.Settings.SelectedStyle == enmSelectedStyle.Icon)
                                    drawCoin(e);
                            }
                            break;

                        case "SummeS":
                            if (e.DisplayText == "n. def." || e.DisplayText == "NaN")
                                e.DisplayText = string.Empty;

                            if (e.DisplayText != string.Empty)
                                if (((Katalog3)grdMain1.View.Records.GetItemAt(rowNumber)).Farbe == "1")
                                    e.Style.BackColor = CoinbookHelper.ColorOwnPrices;
                            break;

                        case "SummePP":
                            if (e.DisplayText == "n. def." || e.DisplayText == "NaN")
                                e.DisplayText = string.Empty;

                            var item = (e.DataRow.RowData as Katalog3);
                            if (item.AuflagePP == "n/a" && item.AuflageSTH == "n/a")
                                    e.Style.BackColor = CoinbookHelper.ColorBlocked;

                            if (item.Farbe == "1" && e.DisplayText != string.Empty)
                                e.Style.BackColor = CoinbookHelper.ColorOwnPrices;
                            break;

                        case "STH":
                            if ((e.DataRow.RowData as Katalog3).AuflageSTH == "n/a")
                                e.Style.BackColor = CoinbookHelper.ColorBlocked;

                            if (CoinbookHelper.Settings.SelectedStyle == enmSelectedStyle.Icon)
                                drawCoin(e);

                            break;

                        case "Auflage":
                            if (e.DisplayText == "n/a" || e.DisplayText == "n/k" || e.DisplayText == "n/u")
                                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
                            else if (e.DisplayText != String.Empty)
                                e.DisplayText = e.DisplayText.Replace(".", cultureInfo.NumberFormat.CurrencyGroupSeparator);
                            break;

                        case "AuflagePP":
                        case "AuflageSTH":
                            if (e.DisplayText == "n/a" || e.DisplayText == "n/k" || e.DisplayText == "n/u")
                            {
                                e.Style.HorizontalAlignment = HorizontalAlignment.Center;
                                e.Style.BackColor = CoinbookHelper.ColorBlocked;
                            }
                            else if (e.DisplayText != String.Empty)
                                e.DisplayText = e.DisplayText.Replace(".", cultureInfo.NumberFormat.CurrencyGroupSeparator);
                            break;

                        case "S":
                        case "SP":
                        case "SS":
                        case "SSP":
                        case "VZ":
                        case "VZP":
                        case "STN":
                            if (CoinbookHelper.Settings.SelectedStyle == enmSelectedStyle.Icon)
                                drawCoin(e);
                            break;
                    }

                }
            }
            catch { }
        }

        private void grdMain1_CellClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (e.DataRow.RowType == RowType.DefaultRow)
            {
                SelectRow(e.DataRow.RowIndex - 2);
                grdMain1.Refresh();

                if (e.MouseEventArgs.Button == MouseButtons.Right)
                {
                    LanguageHelper.Localization.TranslateContextMenu(Name, cmnuStrip);
                    cmnuStrip.Show(new Point(MousePosition.X, MousePosition.Y));
                }
                else
                {
                    switch (e.DataColumn.GridColumn.MappingName)
                    {
                        case "btnEdit":
                            Cursor = Cursors.WaitCursor;
                            ShowDetails(e.DataRow.Index - 2);
                            break;

                        case "HinweisKZ":
                            var temp = (Katalog3)e.DataRow.RowData;
                            if (temp.HinweisKZ == "!")
                            {
                                frmHinweis form = new frmHinweis();
                                form.Guid = temp.GUID;
                                form.Show(this);
                            }
                            break;
                    }
                }

                //grdMain1.Refresh();
                Cursor = Cursors.Default;
            }
        }

        private void grdMain1_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.DataRow.RowType == RowType.DefaultRow)
            {
                if (e.RowIndex % 2 == 0)
                    e.Style.BackColor = CoinbookHelper.ColorAlternateOdd;
                else
                    e.Style.BackColor = CoinbookHelper.ColorAlternateEven;

                //if (CoinbookHelper.MuenzkatalogFiltered[e.RowIndex-2].Selected)
                //    e.Style.BackColor = Color.Red;
            }
        }

        private void grdMain1_QueryRowHeight(object sender, QueryRowHeightEventArgs e)
        {
            if (CoinbookHelper.Settings.Exemplarsammler)
            {
                //int nationID =  (int)cboNationen.SelectedValue;
                //int aeraID=(int)cboÄra.SelectedValue;
                //int gebietID=(int)cboGebiete.SelectedValue;
                //DataTable dt = Database.Database.Instance.GetDataTable(sql.Motivsammlung(nationID, aeraID, gebietID));
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //	string währung = dt.Rows[i]["Währung"].ToString();
                //	string nennwert = dt.Rows[i]["Nominal"].ToString();
                //	string jahrgang = dt.Rows[i]["Jahrgang"].ToString();

                Katalog3 temp = (Katalog3)grdMain1.View.Records.GetItemAt(e.RowIndex);

                var provider = grdMain1.View.GetPropertyAccessProvider();

                if (temp != null)
                {
                    if (temp.S == "" && temp.SP == "" && temp.SS == "" && temp.SSP == "" && temp.VZ == "" && temp.VZP == "" && temp.STN == "" && temp.STH == "" && temp.PP == "")
                    {
                        e.Height = 0;
                        e.Handled = true;
                    }
                }
                //if (grdMain1.RowCount > 1)
                //{
                //	grdMain1.SelectRows(2, 2);
                //	currentRow = 0;
                //	grdMain1.Refresh();
                //}
                //else
                //	currentRow = -1;

                //grdMain1.Focus();

                //Cursor = Cursors.Default;
            }
            else
            {
                if (e.RowIndex < grdMain1.StackedHeaderRows.Count)
                {
                    e.Height = 22;
                    e.Handled = true;
                }

                if (grdMain1.TableControl.GetHeaderIndex() == e.RowIndex)
                {
                    e.Height = 22;
                    e.Handled = true;
                }
            }
        }

        private void grdMain1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdMain1.SelectedItems.Count > 0)
            {
                EnableEditMenues(true, null);

                showPicture();
                ShowOwnPicture();
            }
        }

        private void grdMain1_ColumnResizing(object sender, ColumnResizingEventArgs e)
        {
            string temp = String.Empty;
            for (int i = 0; i < grdMain1.Columns.Count; i++)
                temp = temp + grdMain1.Columns[i].Width.ToString() + "|";
            temp = temp.Substring(0, temp.Length - 1);
            CoinbookHelper.Settings.ColumnWidth = temp;
        }

        public void ShowListe()
        {
            Cursor = Cursors.WaitCursor;

            loadListe();

            btnNavigate.Enabled = CoinbookHelper.MuenzkatalogFiltered.Count > 0;

            if (EnableEditMenues != null)
            {
                bool enabled = (CoinbookHelper.MuenzkatalogFiltered.Count > 0 && grdMain1.SelectedItems.Count > 0);
                EnableEditMenues(enabled, null);
            }

            if (CoinbookHelper.Settings.Preise == enmPreise.EigenePreise)
            {
                for (int i = 0; i < CoinbookHelper.MuenzkatalogFiltered.Count; i++)
                {
                    var preise = DatabaseHelper.LiteDatabase.ReadEigenePreise(CoinbookHelper.MuenzkatalogFiltered[i].GUID , "Sammlung");

                    PreisEventArgs args = new PreisEventArgs(CoinbookHelper.MuenzkatalogFiltered[i].GUID, preise[0].Preis, preise[1].Preis, preise[2].Preis,
                             preise[3].Preis, preise[4].Preis, preise[5].Preis, preise[6].Preis, preise[7].Preis, preise[8].Preis);

                    CoinbookHelper.CalculateOwnPrices(i, args);
                }
            }

            grdMain1.DataSource = CoinbookHelper.MuenzkatalogFiltered;

            if (ChangeRecordCount != null)
                ChangeRecordCount(CoinbookHelper.MuenzkatalogFiltered.Count, null);

            setGridColumns();

            if (OverviewLoaded != null)
                OverviewLoaded(CoinbookHelper.MuenzkatalogFiltered.Count, null);

            grdMain1.SelectedItems.Clear();

            grdMain1.Invalidate();
            grdMain1.Focus();
            picMünze.Image = null;
            picEigen.Image = null;

            //if (grdMain1.RowCount > 2)
            //{
            //    var temp = CoinbookHelper.MuenzkatalogFiltered[0];
            //    //var temp = (Katalog3)grdMain1.View.Records.GetItemAt(0);
            //    grdMain1.SelectedItem = temp;
            //    SelectRow(0);
            //}
            //else
                currentRow = -1;

            //CreateStackHeader();

              Cursor = Cursors.Default;
        }

        public void WriteCell(string cell, string text)
        {
            grdMain1.View.GetPropertyAccessProvider().SetValue(grdMain1.GetRecordAtRowIndex(currentRow), grdMain1.Columns[cell].MappingName, text);

        }

        private void loadFilter()
        {
            try
            {
                List<string> währung = CoinbookHelper.Muenzkatalog1.Select(s => s.Waehrung).Distinct().ToList();
                List<string> nominal = CoinbookHelper.Muenzkatalog1.Select(s => s.Nominal).Distinct().ToList();
                List<string> jahr = CoinbookHelper.Muenzkatalog1.Select(s => s.Jahrgang).Distinct().ToList();
                jahr.Sort();

                währung.Insert(0, LanguageHelper.Localization.GetTranslation("Keys", "alle"));
                nominal.Insert(0, LanguageHelper.Localization.GetTranslation("Keys", "alle"));
                jahr.Insert(0, LanguageHelper.Localization.GetTranslation("Keys", "alle"));

                cboWährung.DataSource = währung;
                cboNominale.DataSource = nominal;
                cboJahr.DataSource = jahr;

                cboWährung.Enabled = true;
                cboNominale.Enabled = true;
                cboJahr.Enabled = true;

                cboWährung.SelectedIndex = 0;
                cboNominale.SelectedIndex = 0;
                cboJahr.SelectedIndex = 0;
            }
            catch (SystemException ex)
            {
                MessageBoxAdv.Show(ex.Message, Application.ProductName);
            }
        }


        #region Aeras laden

        /// <summary>
        /// Liste der Aeren laden
        /// </summary>
        /// 
        public void LoadÄra()
        {
            if (cboNationen.SelectedValue != null)
            {
                CoinbookHelper.Settings.Nation = (int)cboNationen.SelectedValue;

                cboÄra.DataSource = CoinbookHelper.GetAeras(CoinbookHelper.Settings.Nation).ToList();
                cboÄra.SelectedValue = CoinbookHelper.Settings.Ära;
            }
        }

        #endregion

        #region Nationen laden

        /// <summary>
        /// Liste der Nationen laden
        /// </summary>
        /// 
        public void loadNationen()
        {
            //Helper.Nationen = Database.Database.Instance.ReadNationen();
            cboNationen.DataSource = CoinbookHelper.Nationen;
            cboNationen.SelectedValue = CoinbookHelper.Settings.Nation;
        }

        #endregion

        #region Grid Einstellungen

        public void SetGridColumnWidth()
        {
            switch (dpi)
            {
                case 120:
                    grdMain1.Columns["btnEdit"].MinimumWidth = 25;
                    grdMain1.Columns["HinweisKZ"].MinimumWidth = 40;

                    grdMain1.Columns["btnEdit"].Width = 25;
                    grdMain1.Columns["KatNr"].Width = 80;
                    grdMain1.Columns["Nominal"].Width = 67;
                    grdMain1.Columns["Jahrgang"].Width = 50;
                    grdMain1.Columns["HinweisKZ"].Width = 45;
                    grdMain1.Columns["Muenzzeichen"].Width = 60;
                    grdMain1.Columns["Auflage"].Width = 100;
                    grdMain1.Columns["AuflageSTH"].Width = 100;
                    grdMain1.Columns["S"].Width = 45;
                    grdMain1.Columns["SP"].Width = 45;
                    grdMain1.Columns["SS"].Width = 45;
                    grdMain1.Columns["SSP"].Width = 45;
                    grdMain1.Columns["VZ"].Width = 45;
                    grdMain1.Columns["VZP"].Width = 45;
                    grdMain1.Columns["STN"].Width = 45;
                    grdMain1.Columns["STH"].Width = 45;
                    grdMain1.Columns["PP"].Width = 45;
                    grdMain1.Columns["SummeS"].Width = 120;
                    grdMain1.Columns["SummePP"].Width = 120;
                    break;

                default:
                    grdMain1.Columns["btnEdit"].MinimumWidth = 25;
                    grdMain1.Columns["HinweisKZ"].MinimumWidth = 40;

                    grdMain1.Columns["btnEdit"].Width = 25;
                    grdMain1.Columns["KatNr"].Width = 70;
                    grdMain1.Columns["Nominal"].Width = 52;
                    grdMain1.Columns["Jahrgang"].Width = 40;
                    grdMain1.Columns["HinweisKZ"].Width = 40;
                    grdMain1.Columns["Muenzzeichen"].Width = 50;
                    grdMain1.Columns["Auflage"].Width = 90;
                    grdMain1.Columns["AuflageSTH"].Width = 90;
                    grdMain1.Columns["S"].Width = 40;
                    grdMain1.Columns["SP"].Width = 40;
                    grdMain1.Columns["SS"].Width = 40;
                    grdMain1.Columns["SSP"].Width = 40;
                    grdMain1.Columns["VZ"].Width = 40;
                    grdMain1.Columns["VZP"].Width = 40;
                    grdMain1.Columns["STN"].Width = 40;
                    grdMain1.Columns["STH"].Width = 40;
                    grdMain1.Columns["PP"].Width = 40;
                    grdMain1.Columns["SummeS"].Width = 120;
                    grdMain1.Columns["SummePP"].Width = 120;
                    break;
            }

            grdMain1.Columns["Motiv"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        }

        public void SetGridHeaderErhaltungsgrade()
        {
            if (grdMain1.Columns.Count == 0)
                return;

            try
            {
                grdMain1.Columns["S"].HeaderText = CoinbookHelper.Erhaltungsgrade[0].Erhaltung;
                grdMain1.Columns["SP"].HeaderText = CoinbookHelper.Erhaltungsgrade[1].Erhaltung;
                grdMain1.Columns["SS"].HeaderText = CoinbookHelper.Erhaltungsgrade[2].Erhaltung;
                grdMain1.Columns["SSP"].HeaderText = CoinbookHelper.Erhaltungsgrade[3].Erhaltung;
                grdMain1.Columns["VZ"].HeaderText = CoinbookHelper.Erhaltungsgrade[4].Erhaltung;
                grdMain1.Columns["VZP"].HeaderText = CoinbookHelper.Erhaltungsgrade[5].Erhaltung;
                grdMain1.Columns["STN"].HeaderText = CoinbookHelper.Erhaltungsgrade[6].Erhaltung;
                grdMain1.Columns["STH"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung;
                grdMain1.Columns["PP"].HeaderText = CoinbookHelper.Erhaltungsgrade[8].Erhaltung;
            }
            catch { };

            grdMain1.Columns["btnEdit"].HeaderText = String.Empty;

            grdMain1.Style.HeaderStyle.Borders.Bottom = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.Thin);
            grdMain1.Style.HeaderStyle.Borders.Left = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
            //grdMain1.Style.StackedHeaderStyle.Borders.Left = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);

            grdMain1.Columns["Auflage"].HeaderText = CoinbookHelper.Erhaltungsgrade[6].Erhaltung;
            grdMain1.Columns["AuflageSTH"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung;
            grdMain1.Columns["AuflagePP"].HeaderText = CoinbookHelper.Erhaltungsgrade[8].Erhaltung;
            grdMain1.Columns["SummeS"].HeaderText = CoinbookHelper.Erhaltungsgrade[0].Erhaltung + " - " + CoinbookHelper.Erhaltungsgrade[6].Erhaltung;
            grdMain1.Columns["SummePP"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung + " + " + CoinbookHelper.Erhaltungsgrade[8].Erhaltung;
        }

        public void CreateStackHeader()
        {
            //Creating object for a stacked header row.
            var stackedHeaderRow1 = new StackedHeaderRow();

            //Adding stacked column to stacked columns collection available in stacked header row object.
            stackedHeaderRow1.StackedColumns.Add(new StackedColumn() { ChildColumns = "Auflage,AuflageSTH,AuflagePP", HeaderText = LanguageHelper.Localization.GetTranslation("Keys", "Auflage")});
            stackedHeaderRow1.StackedColumns.Add(new StackedColumn() { ChildColumns = "S,SP,SS,SSP,VZ,VZP,STN,STH,PP", HeaderText = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") });
            //stackedHeaderRow1.StackedColumns.Add(new StackedColumn() { ChildColumns = "BS,BSP,BSS,BSSP,BVZ,BVZP,BSTN,BSTH,BPP", HeaderText = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") });
            stackedHeaderRow1.StackedColumns.Add(new StackedColumn() { ChildColumns = "SummeS,SummePP", HeaderText = LanguageHelper.Localization.GetTranslation("Keys", "Summe") + " [" + CoinbookHelper.Settings.CurrentWährung + "]" });     //TODO

            //Adding stacked header row object to stacked header row collection available in SfDataGrid.
            grdMain1.StackedHeaderRows.Clear();
            grdMain1.StackedHeaderRows.Add(stackedHeaderRow1);

            grdMain1.Style.StackedHeaderStyle.BackColor = CoinbookHelper.ColorHeader;
            grdMain1.Style.StackedHeaderStyle.Font.Bold = true;
            grdMain1.Style.StackedHeaderStyle.Font.Size = 10;
            grdMain1.Style.StackedHeaderStyle.TextColor = CoinbookHelper.ColorText;

            grdMain1.Style.StackedHeaderStyle.Borders.Bottom = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.Medium);
            grdMain1.Style.StackedHeaderStyle.Borders.Left = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
            grdMain1.Style.StackedHeaderStyle.Borders.Right = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
            grdMain1.Style.StackedHeaderStyle.Borders.Top = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);

            grdMain1.CellRenderers.Remove("StackedHeader");
            grdMain1.CellRenderers.Add("StackedHeader", new CustomStackedHeaderCellRenderer());

            grdMain1.CellRenderers.Remove("TextBox");
            grdMain1.CellRenderers.Add("TextBox", new CustomTextBoxCellRenderer());
        }

        /// <summary>
        /// DataGridView Spalten an Einstellungen anpassen
        /// </summary>
        private void setGridColumns()
        {
            try
            {
                if (grdMain1.Columns.Count > 0)
                {
                    grdMain1.AllowEditing = false;
                    grdMain1.RowHeight = 24;

                    grdMain1.Style.CellStyle.Borders.All = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
                    grdMain1.Style.SelectionStyle.BackColor = CoinbookHelper.ColorSelection;

                    grdMain1.Columns["Nominal"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["Jahrgang"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["Muenzzeichen"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["SummeS"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
                    grdMain1.Columns["SummePP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
                    grdMain1.Columns["Auflage"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
                    grdMain1.Columns["AuflageSTH"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
                    grdMain1.Columns["AuflagePP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

                    grdMain1.Columns["S"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["SP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["SS"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["SSP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["VZ"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["VZP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["STN"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["STH"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                    grdMain1.Columns["PP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

                    grdMain1.Columns["KatNr"].MinimumWidth = 60;
                    grdMain1.Columns["Jahrgang"].MinimumWidth = 50;
                    grdMain1.Columns["S"].MinimumWidth = 30;
                    grdMain1.Columns["SP"].MinimumWidth = 30;
                    grdMain1.Columns["SS"].MinimumWidth = 30;
                    grdMain1.Columns["SSP"].MinimumWidth = 30;
                    grdMain1.Columns["VZ"].MinimumWidth = 30;
                    grdMain1.Columns["VZP"].MinimumWidth = 30;
                    grdMain1.Columns["STN"].MinimumWidth = 30;
                    grdMain1.Columns["STH"].MinimumWidth = 30;
                    grdMain1.Columns["PP"].MinimumWidth = 30;

                    grdMain1.Columns["Waehrung"].MinimumWidth = 30;
                    grdMain1.Columns["Nominal"].MinimumWidth = 30;
                    grdMain1.Columns["Muenzzeichen"].MinimumWidth = 20;
                    grdMain1.Columns["Auflage"].MinimumWidth = 50;
                    grdMain1.Columns["AuflageSTH"].MinimumWidth = 50;
                    grdMain1.Columns["AuflagePP"].MinimumWidth = 50;
                    grdMain1.Columns["SummeS"].MinimumWidth = 50;
                    grdMain1.Columns["SummePP"].MinimumWidth = 50;
                    grdMain1.Columns["Motiv"].MinimumWidth = 90;

                    SetGridColumnWidth();
                    setColumnVisible();
                    SetGridHeaderErhaltungsgrade();

                    //if (Helper.Settings.ColumnWidth != string.Empty)
                    //{
                    //    string[] temp = Helper.Settings.ColumnWidth.Split('|');
                    //    int count = temp.Length;
                    //    if (count > grdMain1.Columns.Count)
                    //        count = grdMain1.Columns.Count;

                    //    for (int i = 0; i < count; i++)
                    //        grdMain1.Columns[i].Width = ConvertEx.ToInt32(temp[i]);

                    //}

                    grdMain1.Columns["Motiv"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

                    grdMain1.Style.HeaderStyle.BackColor = CoinbookHelper.ColorHeader;
                    grdMain1.Style.HeaderStyle.Font.Bold = true;
                    grdMain1.Style.HeaderStyle.Font.Size = 8;
                    grdMain1.HeaderRowHeight = 50;

                    grdMain1.Columns["Auflage"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader1;
                    grdMain1.Columns["AuflageSTH"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader1;
                    grdMain1.Columns["AuflagePP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader1;

                    grdMain1.Columns["S"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                    grdMain1.Columns["SP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                    grdMain1.Columns["SS"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                    grdMain1.Columns["SSP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                    grdMain1.Columns["VZ"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                    grdMain1.Columns["VZP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                    grdMain1.Columns["STN"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                    grdMain1.Columns["STH"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                    grdMain1.Columns["PP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;

                    grdMain1.Columns["SummeS"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader3;
                    grdMain1.Columns["SummePP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader3;

                    grdMain1.Columns["Waehrung"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader4;
                    grdMain1.Columns["Nominal"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader4;
                    grdMain1.Columns["Muenzzeichen"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader4;
                    grdMain1.Columns["Motiv"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader4;
                    grdMain1.Columns["Jahrgang"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader4;
                    grdMain1.Columns["KatNr"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader4;
                    grdMain1.Columns["HinweisKZ"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader4;
                }
            }
            catch(SystemException e)
            {
                var x = e;
            }
        }

        private void setColumnVisible()
        {
            for (int i = 1; i < grdMain1.Columns.Count; i++)
                grdMain1.Columns[i].Visible = false;

            grdMain1.Columns["KatNr"].Visible = true;
            grdMain1.Columns["Waehrung"].Visible = true;
            grdMain1.Columns["Nominal"].Visible = true;
            grdMain1.Columns["Motiv"].Visible = true;
            grdMain1.Columns["Jahrgang"].Visible = true;
            grdMain1.Columns["Muenzzeichen"].Visible = true;
            grdMain1.Columns["HinweisKZ"].Visible = true;
            grdMain1.Columns["Auflage"].Visible = true;
            grdMain1.Columns["AuflageSTH"].Visible = true;
            grdMain1.Columns["AuflagePP"].Visible = true;
            grdMain1.Columns["SummeS"].Visible = true;
            grdMain1.Columns["SummePP"].Visible = true;
            grdMain1.Columns["S"].Visible = true;
            grdMain1.Columns["SP"].Visible = true;
            grdMain1.Columns["SS"].Visible = true;
            grdMain1.Columns["SSP"].Visible = true;
            grdMain1.Columns["VZ"].Visible = true;
            grdMain1.Columns["VZP"].Visible = true;
            grdMain1.Columns["STN"].Visible = true;
            grdMain1.Columns["STH"].Visible = true;
            grdMain1.Columns["PP"].Visible = true;
        }
        #endregion

        private void initTooltip()
        {
            grdMain1.ToolTipOption.ToolTipMode = ToolTipMode.TrimmedCells;

            //grdMain1.Columns["Auflage"].ShowToolTip = true;
            //grdMain1.Columns["AuflageSTH"].ShowToolTip = true;
            //grdMain1.Columns["AuflagePP"].ShowToolTip = true;
            //grdMain1.Columns["btnEdit"].ShowToolTip = true;
            //grdMain1.Columns["SummePP"].ShowToolTip = true;
            //grdMain1.Columns["SummeS"].ShowToolTip = true;

            grdMain1.ToolTipOption.InitialDelay = 1000;
            grdMain1.ToolTipOption.AutoPopDelay = 7000;

            grdMain1.Style.ToolTipStyle.BackColor = Color.AliceBlue;
            grdMain1.Style.ToolTipStyle.ForeColor = Color.Black;
            grdMain1.Style.ToolTipStyle.BorderThickness = 1;
            grdMain1.Style.ToolTipStyle.BorderColor = Color.Black;
            grdMain1.Style.ToolTipStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            grdMain1.ToolTipOption.ShadowVisible = true;
            grdMain1.ToolTipOption.ToolTipMode = ToolTipMode.AllCells;

            grdMain1.ShowToolTip = true;
        }

        private void loadListe()
        {

            //CoinbookHelper.Settings.Nation = (int)cboNationen.SelectedValue;
            //if (cboÄra.SelectedValue != null)
            //    CoinbookHelper.Settings.Ära = (int)cboÄra.SelectedValue;
            //else
            //    CoinbookHelper.Settings.Ära = -1;

            //if (cboGebiete.SelectedValue != null)
            //    CoinbookHelper.Settings.Gebiet = (int)cboGebiete.SelectedValue;
            //else
            //    CoinbookHelper.Settings.Gebiet = -1;

            CoinbookHelper.Muenzkatalog1.Clear();

            CoinbookHelper.HauptFilter.NationID = (int)cboNationen.SelectedValue;
            CoinbookHelper.HauptFilter.AeraID = (int)cboÄra.SelectedValue;
            CoinbookHelper.HauptFilter.GebietID = (int)cboGebiete.SelectedValue;

            CoinbookHelper.Filter.Waehrung = cboWährung.Text;
            CoinbookHelper.Filter.Nominal = cboNominale.Text;
            CoinbookHelper.Filter.Jahrgang = cboJahr.Text;

            CoinbookHelper.Muenzkatalog1 = DatabaseHelper.LiteDatabase.ReadCoins(CoinbookHelper.HauptFilter,
                                                                                 CoinbookHelper.Settings.SelectedStyle,
                                                                                 CoinbookHelper.Settings.CurrentFaktor,
                                                                                 CoinbookHelper.Settings.Preise, CoinbookHelper.ModulKey);

            if (CoinbookHelper.Settings.Katalognummern == enmKatalognummern.Eigen)
                readOwnKalalogNummern();
        }

        private void readOwnKalalogNummern()
        {
            var nummern = DatabaseHelper.LiteDatabase.ReadKatalogNummern("Sammlung",(int)cboNationen.SelectedValue);

            foreach (var item in nummern)
                ChangeKatalogNumber(new KatalognummerEventArgs(enmKatalogAction.Neu, item.Coinbook, item.KatNr));
        }

        public void ChangeKatalogNumber(KatalognummerEventArgs args)
        {
            switch (args.Action)
            {
                case enmKatalogAction.Neu:
                    {
                        for (int i = 0; i < CoinbookHelper.Muenzkatalog1.Count; i++)
                            if (CoinbookHelper.Muenzkatalog1[i].OriginalKatNr == args.Original)
                                CoinbookHelper.Muenzkatalog1[i].KatNr = args.New;
                    }
                    break;

                case enmKatalogAction.Delete:
                    {
                        for (int i = 0; i < CoinbookHelper.Muenzkatalog1.Count; i++)
                            if (CoinbookHelper.Muenzkatalog1[i].OriginalKatNr == args.Original)
                                CoinbookHelper.Muenzkatalog1[i].KatNr = CoinbookHelper.Muenzkatalog1[i].OriginalKatNr;
                    }
                    break;
            }

            grdMain1.Refresh();
        }

        public void ResizeMotivColumn()
        {
            double width = 0;
            if (grdMain1.Columns.Count > 0)
            {
                foreach (var item in grdMain1.Columns)
                    if (item.Visible && item.MappingName != "Motiv")
                    {
                        width = width + item.ActualWidth;
                    }

                grdMain1.Columns["Motiv"].Width = grdMain1.Width - width - 20;
            }
        }

        public void ShowSelectedStyle()
        {
            switch (CoinbookHelper.Settings.SelectedStyle)
            {
                case enmSelectedStyle.DoublettenOnly:
                    lblBestandAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "DoublettenOnly");
                    break;

                case enmSelectedStyle.Icon:
                    lblBestandAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "Icon");
                    break;

                case enmSelectedStyle.SammlungOnly:
                    lblBestandAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "SammlungOnly");
                    break;

                case enmSelectedStyle.SammlungUndDoubletten:
                    lblBestandAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "SammlungUndDoubletten");
                    break;
            }
        }

        private void showSuperToolTip()
        {
            //int x = Cursor.Position.X;
            //int y = Cursor.Position.Y;
            //toolTipInfo.Separator = true;
            //stpToolTip.Show(toolTipInfo, new Point(x, y),3000);
        }

        private void showPicture()
        {
            string file = String.Empty;

            Katalog3 temp = (Katalog3)grdMain1.CurrentItem;

            if (temp != null)
            {
                file = Path.Combine(CoinbookHelper.Picturepath, temp.Picture);

                if (file != picMünze.PictureName)
                {
                    picMünze.PictureName = file;

                    if (File.Exists(file))
                        picMünze.Image = new Bitmap(file);
                    else
                        picMünze.Image = null;
                }
            }
        }

        public void ShowOwnPicture()
        {
            string ownFile = String.Empty;

            Katalog3 temp = (Katalog3)grdMain1.CurrentItem;

            if (temp != null)
            {
                if (!string.IsNullOrEmpty(temp.OwnPicture))
                    ownFile = temp.OwnPicture;
                //file = Path.Combine(Helper.Picturepath, temp.Picture);

                if (ownFile != picEigen.PictureName)
                {
                    picEigen.PictureName = ownFile;

                    if (File.Exists(ownFile))
                        picEigen.Image = new Bitmap(ownFile);
                    else
                        picEigen.Image = null;
                }
            }
        }

        private string toolTipHeader(string columnName)
        {
            string result = string.Empty;

            switch (columnName)
            {
                case "Auflage":
                    result = string.Format("{0} '{1}'", LanguageHelper.Localization.GetTranslation("Keys", "Auflage"), CoinbookHelper.Erhaltungsgrade[6].Bezeichnung);
                    break;

                case "AuflageSTH":
                    result = string.Format("{0} '{1}'", LanguageHelper.Localization.GetTranslation("Keys", "Auflage"), CoinbookHelper.Erhaltungsgrade[7].Bezeichnung);
                    break;

                case "AuflagePP":
                    result = string.Format("{0} '{1}'", LanguageHelper.Localization.GetTranslation("Keys", "Auflage"), CoinbookHelper.Erhaltungsgrade[8].Bezeichnung);
                    break;

                case "SummePP":
                    result = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "Summen"),
                        CoinbookHelper.Erhaltungsgrade[7].Bezeichnung,
                        CoinbookHelper.Erhaltungsgrade[8].Bezeichnung);
                    break;

                case "SummeS":
                    result = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "Summen"),
                         CoinbookHelper.Erhaltungsgrade[0].Bezeichnung,
                        CoinbookHelper.Erhaltungsgrade[6].Bezeichnung);
                    break;

                case "S":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[0].Bezeichnung + "'";
                    break;

                case "SP":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[1].Bezeichnung + "'";
                    break;

                case "SS":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[2].Bezeichnung + "'";
                    break;

                case "SSP":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[3].Bezeichnung + "'";
                    break;

                case "VZ":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[4].Bezeichnung + "'";
                    break;

                case "VZP":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[5].Bezeichnung + "'";
                    break;

                case "STN":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[6].Bezeichnung + "'";
                    break;

                case "STH":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[7].Bezeichnung + "'";
                    break;

                case "PP":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") + " '" + CoinbookHelper.Erhaltungsgrade[8].Bezeichnung + "'";
                    break;
            }

            return result;
        }

        private string toolTipTable(string columnName, string displayText)
        {
            string result = string.Empty;

            switch (columnName)
            {
                case "Auflage":
                case "AuflageSTH":
                case "AuflagePP":
                    switch (displayText)
                    {
                        case "n/a":
                            result = LanguageHelper.Localization.GetTranslation("Keys", "nichtgeprägt");
                            break;

                        case "n/u":
                            result = LanguageHelper.Localization.GetTranslation("Keys", "nochunbekannt");
                            break;

                        case "n/k":
                            result = LanguageHelper.Localization.GetTranslation("Keys", "unbekannt");
                            break;
                    }
                    break;

                case "btnEdit":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "CoinDetails");
                    break;

                case "HinweisKZ":
                    result = LanguageHelper.Localization.GetTranslation("Keys", "Hinweis");
                    break;

                case "SummePP":
                    if (displayText != string.Empty && displayText != "n. def." && displayText != "NaN")
                    {
                        var temp = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "Summen"),
                           CoinbookHelper.Erhaltungsgrade[7].Bezeichnung,
                           CoinbookHelper.Erhaltungsgrade[8].Bezeichnung); result = temp + Environment.NewLine + Environment.NewLine + string.Format("{0:#####.00 €}", Convert.ToDecimal(displayText));
                    }
                    break;

                case "SummeS":
                    if (displayText != string.Empty && displayText != "n. def." && displayText != "NaN")
                    {
                        var temp = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "Summen"),
                            CoinbookHelper.Erhaltungsgrade[0].Bezeichnung,
                           CoinbookHelper.Erhaltungsgrade[6].Bezeichnung);
                        result = temp + Environment.NewLine + Environment.NewLine + string.Format("{0:#####.00 €}", Convert.ToDecimal(displayText));
                    }
                    break;
            }

            return result;
        }

        private void drawCoin(DrawCellEventArgs e)
        {
            if (e.Style.BackColor != CoinbookHelper.ColorBlocked)
                e.Style.BackColor = (e.RowIndex % 2 == 0) ? CoinbookHelper.ColorAlternateOdd : CoinbookHelper.ColorAlternateEven;

            if (e.DisplayText == "0")
                e.DisplayText = string.Empty;

            if (e.DisplayText != String.Empty)
            {
                showIcon(coin, e.Bounds, e.Graphics, e.Style.BackColor);
                e.Handled = true;
            }
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

        public void SelectRow(int row)
        {
            if (CoinbookHelper.MuenzkatalogFiltered != null && CoinbookHelper.MuenzkatalogFiltered.Count != 0)
            {
                grdMain1.Focus();
                grdMain1.TableControl.Focus();
                grdMain1.TableControl.Select();

                grdMain1.SelectedItems.Clear();
                if (row > 0) grdMain1.SelectedItem = grdMain1.View.Records.GetItemAt(row);
                //grdMain1.SelectRows(row, row);


                //grdMain1.TableControl.ScrollRows.ScrollInView(row);
                //grdMain1.TableControl.UpdateScrollBars();

                //grdMain1.SelectedItems.Add(grdMain1.View.Records[row]);

                //grdMain1.SelectedIndex = row;

                grdMain1.Invalidate();
                currentRow = row;

                showPicture();
                ShowOwnPicture();
            }
        }

        #region Load data to view
        /// <summary>	Detailsansicht der Münze öffnen. </summary>
        public void ShowDetails(int index, string program = "Münz-Details")
        {
            if (grdMain1.RowCount > 0)
            {
                var text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), program);

                //lblStatusleiste.Text = xxxx;
                //Application.DoEvents();

                MessageBoxNonmodal messagebox = new MessageBoxNonmodal(text, "Coinbook", 10);
                panCoins.Enabled = false;
                Katalog3 temp = (Katalog3)grdMain1.View.Records.GetItemAt(index);

                formMünzdetails.Guid = temp.GUID;
                formMünzdetails.Index = index;
                formMünzdetails.Nation = cboNationen.Text;
                formMünzdetails.Ära = cboÄra.Text;
                formMünzdetails.Gebiet = cboGebiete.Text;

                formMünzdetails.Show(this);
                messagebox.Close();
                Application.DoEvents();
            }
        }
        #endregion

        private void picMünze_Click(object sender, EventArgs e)
        {
            PictureBoxEx pic = (PictureBoxEx)sender;

            if (pic.PictureName != String.Empty && pic.Image != null)
            {
                frmMünzbild form = new frmMünzbild();

                Katalog3 row = (Katalog3)grdMain1.CurrentItem;

                form.Bild = pic.PictureName;
                form.Münze = string.Format("{0} - {1} - {2} - {3} - {4} - {5}", row.KatNr, row.Nominal, row.Waehrung, cboNationen.Text, cboÄra.Text, cboGebiete.Text);
                form.IsOwnPicture = (pic.Name == "picEigen");
                form.ShowDialog(this);
            }
        }

        private void picMünze_MouseEnter(object sender, EventArgs e)
        {
            if (grdMain1.RowCount == 0)
                return;

            string text = string.Empty;

            if (grdMain1.CurrentItem != null)
            {
                string file = Path.Combine(CoinbookHelper.Picturepath, ((Katalog3)grdMain1.CurrentItem).Picture);
                var copyright = ImageHelper.GetFullEXIF(file);

                if (copyright.Count == 1)
                    text = LanguageHelper.Localization.GetTranslation("frmMuenzdetails", "lblCopyright");
                else
                    text = copyright[1].data.Trim();

                toolTip1.SetToolTip(this.picMünze, text);
            }
            else
            {
                toolTip1.SetToolTip(this.picMünze, "");
            }
        }

        private void picMünze_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CoinbookHelper.Filter.Waehrung = cboWährung.Text;
            CoinbookHelper.Filter.Nominal = cboNominale.Text;
            CoinbookHelper.Filter.Jahrgang = cboJahr.Text;

            grdMain1.DataSource = CoinbookHelper.MuenzkatalogFiltered;

            if (ChangeRecordCount != null)
                ChangeRecordCount(CoinbookHelper.MuenzkatalogFiltered.Count, null);

            setGridColumns();
        }

        private void btnNavigate_Click(object sender, EventArgs e)
        {
            Navigate();
        }

        public void Navigate()
        {
            if (cboWährung.Items.Count > 0)
                cboWährung.SelectedIndex = 0;

            if (cboNominale.Items.Count > 0)
                cboNominale.SelectedIndex = 0;

            if (cboJahr.Items.Count > 0)
                cboJahr.SelectedIndex = 0;

            cboWährung.Enabled = true;
            cboNominale.Enabled = true;
            cboJahr.Enabled = true;

            if (cboNationen.SelectedValue == null)
                cboNationen.SelectedIndex = 0;

            HauptFilter filter = new HauptFilter { AeraID = CoinbookHelper.Settings.Ära, GebietID = CoinbookHelper.Settings.Gebiet, NationID = NationID };
            if (cboNationen.SelectedValue != null && cboÄra.SelectedValue != null && cboGebiete.SelectedValue != null)
            {
                picMünze.Image = null;
                picEigen.Image = null;
                picMünze.PictureName = String.Empty;
                picEigen.PictureName = String.Empty;
                ShowListe();
                loadFilter();
            }

            setGridColumns();

            grdMain1.SelectedItem = null;
            SelectRow(0);

            if (grdMain1.RowCount > 0)
            {
                int index = 0;
                currentRow = 0;
                grdMain1.SelectedIndex = index;

                showPicture();
                ShowOwnPicture();
            }

            //grdMain1.Focus();
            //grdMain1.TableControl.ScrollRows.ScrollInView(grdMain1.TableControl.ResolveToRowIndex(index));
            //grdMain1.TableControl.UpdateScrollBars();
            //grdMain1.SelectedIndex = index;

        }

        private void showHinweisTooltip()
        {
            toolTipInfo.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            toolTipInfo.Header.Text = "SuperToolTip with \r\nGradient Look And Feel";
            toolTipInfo.Header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            toolTipInfo.Header.TextMargin = new System.Windows.Forms.Padding(5);
            toolTipInfo.Body.Text = "Select a color to experience the\r\nGradient look and feel of SuperTooltip.";
            toolTipInfo.Body.TextMargin = new System.Windows.Forms.Padding(5);
            toolTipInfo.Footer.Text = "Appealing look and feel with various \r\ngradient colors.";
            toolTipInfo.Footer.TextMargin = new System.Windows.Forms.Padding(5);
            toolTipInfo.Separator = true;
        }

        #region Navigation
        public void MenuNavigation(enmMenuItems action)
        {
            switch (action)
            {
                case enmMenuItems.NationBack:
                    if (cboNationen.SelectedIndex > 0)
                    {
                        cboNationen.SelectedIndex -= 1;
                        Focus();
                        Navigate();
                    }
                    break;

                case enmMenuItems.NationNext:
                    if (cboNationen.SelectedIndex < cboNationen.Items.Count-1)
                    { 
                        cboNationen.SelectedIndex += 1;
                        Focus();
                        Navigate();
                    }
                    break;

                case enmMenuItems.AeraBack:
                    if (cboÄra.SelectedIndex > 0)
                    { 
                        cboÄra.SelectedIndex -= 1;
                        Focus();
                        Navigate();
                    }
                    break;

                case enmMenuItems.AeraNext:
                    if (cboÄra.SelectedIndex < cboÄra.Items.Count-1)
                    { 
                        cboÄra.SelectedIndex += 1;
                        Focus();
                        Navigate();
                    }
                    break;

                case enmMenuItems.RegionBack:
                    if (cboGebiete.SelectedIndex > 0)
                    { 
                        cboGebiete.SelectedIndex -= 1;
                        Focus();
                        Navigate();
                    }
                    break;

                case enmMenuItems.RegionNext:
                    if (cboGebiete.SelectedIndex < cboGebiete.Items.Count-1)
                    { 
                        cboGebiete.SelectedIndex += 1;
                        Focus();
                        Navigate();
                    }
                    break;
            }
        }

        /// <summary>
        /// in Währung rückwärts bewegen
        /// </summary>
        /// 
        public void WaehrungZurueck()
        {
            if (grdMain1.RowCount == 0)
                return;

            if (currentRow == -1)
                return;

            String währung = CoinbookHelper.MuenzkatalogFiltered[currentRow].Waehrung;

            for (int i = currentRow; i > 0; i--)
            {
                if (CoinbookHelper.MuenzkatalogFiltered[i].Waehrung != währung)
                {
                    SelectRow(i);
                    break;
                }
            }
        }

        /// <summary>
        /// in Währung vorwärts bewegen
        /// </summary>
        /// 
        public void WaehrungVor()
        {
            if (grdMain1.RowCount != 0 && currentRow != -1)
            {
                String währung = CoinbookHelper.MuenzkatalogFiltered[currentRow].Waehrung;

                int row = currentRow + 1;
                for (int i = row; i < CoinbookHelper.MuenzkatalogFiltered.Count; i++)
                {
                    if (CoinbookHelper.MuenzkatalogFiltered[i].Waehrung != währung)
                    {
                        SelectRow(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// in Münzwert rückwärts bewegen
        /// </summary>
        /// 
        public void MuenzwertZurueck()
        {
            if (grdMain1.RowCount == 0)
                return;

            if (currentRow == -1)
                return;

            String nominal = CoinbookHelper.MuenzkatalogFiltered[currentRow].Nominal;

            for (int i = currentRow; i >= 0; i--)
            {
                if (CoinbookHelper.MuenzkatalogFiltered[i].Nominal != nominal)
                {
                    SelectRow(i);
                    break;
                }
            }
        }

        /// <summary>
        /// in Münzwert vorwärts bewegen
        /// </summary>
        /// 
        public void MuenzwertVor()
        {
            if (grdMain1.RowCount == 0)
                return;

            if (currentRow == -1)
                return;

            String nominal = CoinbookHelper.MuenzkatalogFiltered[currentRow].Nominal;

            int row = currentRow + 1;
            for (int i = row; i < CoinbookHelper.MuenzkatalogFiltered.Count; i++)
            {
                if (CoinbookHelper.MuenzkatalogFiltered[i].Nominal != nominal)
                {
                    SelectRow(i);
                    break;
                }
            }
        }

        /// <summary>
        /// in Jahr rückwärts bewegen
        /// </summary>
        ///
        public void JahrZurueck()
        {
            if (grdMain1.RowCount == 0)
                return;

            if (currentRow == -1)
                return;

            String jahrgang = CoinbookHelper.MuenzkatalogFiltered[currentRow].Jahrgang;

            for (int i = currentRow; i >= 0; i--)
            {
                if (CoinbookHelper.MuenzkatalogFiltered[i].Jahrgang != jahrgang)
                {
                    SelectRow(i);
                    break;
                }
            }
        }

        /// <summary>
        /// in Jahr vorwärts bewegen
        /// </summary>
        /// 
        public void JahrVor()
        {
            if (grdMain1.RowCount == 0)
                return;

            if (currentRow == -1)
                return;

            String jahrgang = CoinbookHelper.MuenzkatalogFiltered[currentRow].Jahrgang;

            int row = currentRow + 1;
            for (int i = row; i < CoinbookHelper.MuenzkatalogFiltered.Count; i++)
            {
                if (CoinbookHelper.MuenzkatalogFiltered[i].Jahrgang != jahrgang)
                {
                    SelectRow(i);
                    break;
                }
            }
        }

        public void NextCoin()
        {
            int row = currentRow + 1;
            for (int i = row; i < grdMain1.RowCount - 2; i++)
            {
                Katalog3 temp = (Katalog3)grdMain1.View.Records.GetItemAt(i);
                if (temp.SummeS != "" || temp.SummePP != "")
                {
                    SelectRow(i);
                    break;
                }
            }
        }

        public void PreviousCoin()
        {
            int row = currentRow - 1;
            for (int i = row; i > 0; i--)
            {
                Katalog3 temp = (Katalog3)grdMain1.View.Records.GetItemAt(i);
                if (temp.SummeS != "" || temp.SummePP != "")
                {
                    SelectRow(i);
                    break;
                }
            }
        }
        #endregion

        public string GetColumnWidth
        {
            get
            {
                string result = String.Empty;
                for (int i = 0; i < grdMain1.Columns.Count; i++)
                    result = result + grdMain1.Columns[i].Width.ToString() + "|";

                if (result.Length > 1)
                    result = result.Substring(0, result.Length - 1);

                return result;
            }
        }

        public void EnableButton(bool boolEnable)
        {
            cboNationen.Enabled = boolEnable;
            cboÄra.Enabled = boolEnable;
            cboGebiete.Enabled = boolEnable;
            //cboJahr.Enabled = boolEnable;
            //cboWährung.Enabled = boolEnable;
            //cboNominale.Enabled = boolEnable;
            //splC.Panel2.Enabled = boolEnable;

            btnNavigate.Enabled = true;
            Update();
        }

        public void Setxxxx()
        {
            cboNationen.SelectedValue = CoinbookHelper.Settings.Nation;
            if (cboNationen.SelectedIndex == -1 && cboNationen.Items.Count != 0)
                cboNationen.SelectedIndex = 0;

            LoadÄra();
            cboÄra.SelectedValue = CoinbookHelper.Settings.Ära;
            if (cboÄra.SelectedIndex == -1 && cboÄra.Items.Count != 0)
                cboÄra.SelectedIndex = 0;

            //loadGebiete();
            cboGebiete.SelectedValue = CoinbookHelper.Settings.Gebiet;
            if (cboGebiete.SelectedIndex == -1 && cboGebiete.Items.Count != 0)
                cboGebiete.SelectedIndex = 0;

            cboWährung.Enabled = true;
            cboNominale.Enabled = true;
            cboJahr.Enabled = true;
        }

        public void SelectedItemsClear()
        {
            grdMain1.SelectedItems.Clear();
        }

        private void cmnuMünzdetails_Click(object sender, EventArgs e)
        {
            ShowDetails(grdMain1.CurrentCell.RowIndex-2);
        }

        private void cmnuMünzeAdd_Click(object sender, EventArgs e)
        {
            if (AddMünze != null)
                AddMünze(this, null);
        }

        private void cmenuDeleteCoin_Click(object sender, EventArgs e)
        {
            if (MünzeLöschen != null)
                MünzeLöschen(this, null);
        }

        private void cmnuEigeneKatalognummern_Click(object sender, EventArgs e)
        {
            if (OwnCatalog != null)
                OwnCatalog(this, null);
        }

        private void cmnuPicture_Click(object sender, EventArgs e)
        {
            if (OwnPicture != null)
                OwnPicture(this, null);
        }

        private void cmnuPreise_Click(object sender, EventArgs e)
        {
            if (EigenePreise != null)
                EigenePreise(this, null);
        }

        private void cmnuAuktionen_Click(object sender, EventArgs e)
        {
            if (Auktionen != null)
                Auktionen(this, null);

        }

        private void cmnuPrägeanstalten_Click(object sender, EventArgs e)
        {
            if (Prägeanstalten != null)
                Prägeanstalten(this, null);
        }

        private void cmnuUp_Click(object sender, EventArgs e)
        {
            if (CoinNext != null)
                CoinNext(this, null);
        }

        private void cmnuDown_Click(object sender, EventArgs e)
        {
            if (CoinPrevious != null)
                CoinPrevious(this, null);
        }

        public void SaveColumnWidth()
        {
            string temp = String.Empty;
            for (int i = 0; i < grdMain1.Columns.Count; i++)
                temp = temp + grdMain1.Columns[i].Width.ToString() + "|";
            temp = temp.Substring(0, temp.Length - 1);
            CoinbookHelper.Settings.ColumnWidth = temp;

            DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);
        }

        private void FormMünzdetails_ChangeBestand(object sender, CoinEventArgs args)
        {
            CoinbookHelper.InsertIntoMünzkatalog(args.Coin, args.Anzahl);
            grdMain1.Refresh();
        }

        private void FormMünzdetails_ChangeCoin(object sender, CoinEventArgs args)
        {
            SelectRow(args.Index);
        }

        private void FormMünzdetails_ChangeOwnPicture(object sender, EventArgs e)
        {
            ShowOwnPicture();
        }

        private void FormMünzdetails_ChangeKatalogNumber(object sender, KatalognummerEventArgs args)
        {
            ChangeKatalogNumber(args);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            formMünzdetails.ChangeOwnPicture -= FormMünzdetails_ChangeOwnPicture;
            formMünzdetails.ChangeBestand -= FormMünzdetails_ChangeBestand;
            formMünzdetails.ChangeCoin -= FormMünzdetails_ChangeCoin;
            formMünzdetails.ChangeKatalogNumber -= FormMünzdetails_ChangeKatalogNumber;
            formMünzdetails.HideForm -= FormMünzdetails_HideForm;

            base.OnHandleDestroyed(e);
        }

        public void MenuNavigation(object sender, EventArgs e)
        {
            string name = string.Empty;

            enmMenuItems item = enmMenuItems.Nothing;

            var temp1 = sender as ToolStripButton;
            if (temp1 != null)
                name = temp1.Name;
            else
            {
                var temp2 = sender as ToolStripMenuItem;
                if (temp2 != null)
                    name = temp2.Name;
            }

            switch (name)
            {
                case "btnNationZurueck":
                case "mnuNationZurueck":
                    MenuNavigation(enmMenuItems.NationBack);
                    break;

                case "btnNationVor":
                case "mnuNationVor":
                    MenuNavigation(enmMenuItems.NationNext);
                    break;

                case "btnÄraZurueck":
                case "mnuÄraZurueck":
                    MenuNavigation(enmMenuItems.AeraBack);
                    break;

                case "btnÄraVor":
                case "mnuÄraVor":
                    MenuNavigation(enmMenuItems.AeraNext);
                    break;

                case "btnGebietZurueck":
                case "mnuGebietZurueck":
                    MenuNavigation(enmMenuItems.RegionBack);
                    break;

                case "btnGebietVor":
                case "mnuGebietVor":
                    MenuNavigation(enmMenuItems.RegionNext);
                    break;

                case "btnWaehrungVor":
                    if (DatabaseHelper.LiteDatabase.Count("tblAera", CoinbookHelper.ModulKey) != 0)
                        WaehrungVor();
                    break;

                case "btnWaehrungZurueck":
                    if (DatabaseHelper.LiteDatabase.Count("tblAera", CoinbookHelper.ModulKey) != 0)
                        WaehrungZurueck();
                    break;

                case "btnMuenzwertVor":
                    if (DatabaseHelper.LiteDatabase.Count("tblAera", CoinbookHelper.ModulKey) != 0)
                        MuenzwertVor();
                    break;

                case "btnMuenzwertZurueck":
                    if (DatabaseHelper.LiteDatabase.Count("tblAera", CoinbookHelper.ModulKey) != 0)
                        MuenzwertZurueck();
                    break;

                case "btnJahrZurueck":
                    if (DatabaseHelper.LiteDatabase.Count("tblAera", CoinbookHelper.ModulKey) != 0)
                        JahrZurueck();
                    break;

                case "btnJahrVor":
                    if (DatabaseHelper.LiteDatabase.Count("tblAera", CoinbookHelper.ModulKey) != 0)
                        JahrVor();
                    break;
            }
        }
    }
}


