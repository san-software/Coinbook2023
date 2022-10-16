using Coinbook.Lokalisierung;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using System;
using System.Collections.Generic;
using Syncfusion.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Linq;
using LiteDB.Database;
using System.Windows.Forms;
using SAN.Converter;
using Syncfusion.WinForms.DataGrid.Events;
using Coinbook.Model;
using Coinbook.Helper;
using Coinbook.EventHandlers;
using System.ComponentModel;

namespace Coinbook
{
    public partial class frmQuickInput : Form
    {
        List<Katalog3> katalog;
        BindingList<QuickInput> liste2;
        List<Katalog3> temp;
        List<Katalog3> katalog2;

        private int dpi = 0;
        private List<QuickInput> liste = new List<QuickInput>();
        private char[] zahlen = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private char[] datum = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
        private char[] preis = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' };
        private int nation = 0;
        private int gebiet = 0;
        private int aera = 0;
        private bool sammlung = true;
        private Lite liteDatabase = new Lite();

        public event CoinEventHandler ChangeBestand;

        public frmQuickInput(int nation, int aera, int gebiet, bool sammlung, string currency = null, string nominal= null, string jahr = null)
        {
            this.nation = nation;
            this.gebiet = gebiet;
            this.aera = aera;
            this.sammlung = sammlung;

            InitializeComponent();

            if (!sammlung)
            {
                lblSammlung.Text = "Duplikate";
                lblSammlung.ForeColor = Color.Red;
            }
            else
            {
                lblSammlung.Text = "Sammlung";
                lblSammlung.ForeColor = Color.Black;
            }

            lblNation.Text = CoinbookHelper.Nationen.FirstOrDefault(x => x.ID == nation).Bezeichnung;
            lblAera.Text = CoinbookHelper.Aeras.FirstOrDefault(x => x.ID == aera).Bezeichnung;
            lblRegion.Text = CoinbookHelper.Regions.FirstOrDefault(x => x.ID == gebiet).Bezeichnung;

            temp = DatabaseHelper.LiteDatabase.ReadQuickKatalog(lblNation.Text, aera, gebiet);

            if (currency != null)
                temp = temp.FindAll(x => x.Waehrung == currency).ToList();

            if (nominal != null)
                temp = temp.FindAll(x => x.Nominal == nominal).ToList();

            if (jahr != null)
                    temp = temp.FindAll(x => x.Jahrgang == jahr).ToList();
    
            katalog = temp;

            foreach (var item in katalog)
            {
                var xxx = DatabaseHelper.LiteDatabase.GetBestand(item.GUID);

                QuickInput quick = new QuickInput();
                quick.Datum = string.Empty;
                quick.Preis = string.Empty;
                quick.Changed = 0;
                quick.Jahrgang = item.Jahrgang + Environment.NewLine + item.Muenzzeichen;
                quick.Katnr = item.KatNr;
                quick.Motiv = item.Motiv;

                if (sammlung)
                {
                    quick.S = xxx.S == 0 ? string.Empty : xxx.S.ToString();
                    quick.SP = xxx.SP == 0 ? string.Empty : xxx.SP.ToString();
                    quick.SS = xxx.SS == 0 ? string.Empty : xxx.SS.ToString();
                    quick.SSP = xxx.SSP == 0 ? string.Empty : xxx.SSP.ToString();
                    quick.VZ = xxx.VZ == 0 ? string.Empty : xxx.VZ.ToString();
                    quick.VZP = xxx.VZP == 0 ? string.Empty : xxx.VZP.ToString();
                    quick.STN = xxx.STN == 0 ? string.Empty : xxx.STN.ToString();
                    quick.STH = xxx.STH == 0 ? string.Empty : xxx.STH.ToString();
                    quick.PP = xxx.PP == 0 ? string.Empty : xxx.PP.ToString();
                }
                else
                {
                    quick.S = xxx.S == 0 ? string.Empty : xxx.DS.ToString();
                    quick.SP = xxx.SP == 0 ? string.Empty : xxx.DSP.ToString();
                    quick.SS = xxx.SS == 0 ? string.Empty : xxx.DSS.ToString();
                    quick.SSP = xxx.SSP == 0 ? string.Empty : xxx.DSSP.ToString();
                    quick.VZ = xxx.VZ == 0 ? string.Empty : xxx.DVZ.ToString();
                    quick.VZP = xxx.VZP == 0 ? string.Empty : xxx.DVZP.ToString();
                    quick.STN = xxx.STN == 0 ? string.Empty : xxx.DSTN.ToString();
                    quick.STH = xxx.STH == 0 ? string.Empty : xxx.DSTH.ToString();
                    quick.PP = xxx.PP == 0 ? string.Empty : xxx.DPP.ToString();

                }

                quick.Währung = item.Nominal + Environment.NewLine + item.Waehrung;
                quick.AuflagePP = item.AuflagePP;
                quick.AuflageSTH = item.AuflageSTH;
                quick.Guid = item.GUID;
                quick.HinweisKZ = item.HinweisKZ;
                quick.PS = item.SPreis;
                quick.PSP = item.SPPreis;
                quick.PSS = item.SSPreis;
                quick.PSSP = item.SSPPreis;
                quick.PVZ = item.VZPreis;
                quick.PVZP = item.VZPPreis;
                quick.PSTN = item.STNPreis;
                quick.PSTH = item.STHPreis;
                quick.PPP = item.PPPreis;

                liste.Add(quick);
            }

            liste = liste.FindAll(x => x.S == string.Empty && x.SP == string.Empty && x.SS == string.Empty && x.SSP == string.Empty && x.VZ == string.Empty && x.VZP == string.Empty
                && x.STN == string.Empty && x.STH == string.Empty && x.PP == string.Empty).ToList();

            liste2= new BindingList<QuickInput>(liste);

            grdInput.SelectionController = new CustomSelectionController(grdInput);
            grdInput.DataSource = liste2;

            setGridColumns();
            CreateStackHeader();

            txtRows.Text = liste2.Count.ToString();

            Graphics graphics;
            graphics = this.CreateGraphics();
            dpi = ConvertEx.ToInt32(graphics.DpiX);

            btnSave.Enabled = false;
        }

        #region Grid Einstellungen
        public void SetGridColumnWidth()
        {
            switch (dpi)
            {
                case 120:
                    grdInput.Columns["KatNr"].Width = 80;
                    grdInput.Columns["Jahrgang"].Width = 70;
                    grdInput.Columns["HinweisKZ"].Width = 45;
                    grdInput.Columns["S"].Width = 45;
                    grdInput.Columns["SP"].Width = 45;
                    grdInput.Columns["SS"].Width = 45;
                    grdInput.Columns["SSP"].Width = 45;
                    grdInput.Columns["VZ"].Width = 45;
                    grdInput.Columns["VZP"].Width = 45;
                    grdInput.Columns["STN"].Width = 45;
                    grdInput.Columns["STH"].Width = 45;
                    grdInput.Columns["PP"].Width = 45;
                    grdInput.Columns["Datum"].Width = 80;
                    grdInput.Columns["Preis"].Width = 80;
                    break;

                default:
                    grdInput.Columns["Katnr"].Width = 70;
                    grdInput.Columns["Jahrgang"].Width = 60;
                    grdInput.Columns["HinweisKZ"].Width = 40;
                    grdInput.Columns["S"].Width = 40;
                    grdInput.Columns["SP"].Width = 40;
                    grdInput.Columns["SS"].Width = 40;
                    grdInput.Columns["SSP"].Width = 40;
                    grdInput.Columns["VZ"].Width = 40;
                    grdInput.Columns["VZP"].Width = 40;
                    grdInput.Columns["STN"].Width = 40;
                    grdInput.Columns["STH"].Width = 40;
                    grdInput.Columns["PP"].Width = 40;
                    grdInput.Columns["Datum"].Width = 80;
                    grdInput.Columns["Preis"].Width = 80;
                    break;
            }

            grdInput.Columns["Motiv"].Width = 240;
        }

        public void SetGridHeaderErhaltungsgrade()
        {
            if (grdInput.Columns.Count == 0)
                return;

            try
            {
                grdInput.Columns["S"].HeaderText = CoinbookHelper.Erhaltungsgrade[0].Erhaltung;
                grdInput.Columns["SP"].HeaderText = CoinbookHelper.Erhaltungsgrade[1].Erhaltung;
                grdInput.Columns["SS"].HeaderText = CoinbookHelper.Erhaltungsgrade[2].Erhaltung;
                grdInput.Columns["SSP"].HeaderText = CoinbookHelper.Erhaltungsgrade[3].Erhaltung;
                grdInput.Columns["VZ"].HeaderText = CoinbookHelper.Erhaltungsgrade[4].Erhaltung;
                grdInput.Columns["VZP"].HeaderText = CoinbookHelper.Erhaltungsgrade[5].Erhaltung;
                grdInput.Columns["STN"].HeaderText = CoinbookHelper.Erhaltungsgrade[6].Erhaltung;
                grdInput.Columns["STH"].HeaderText = CoinbookHelper.Erhaltungsgrade[7].Erhaltung;
                grdInput.Columns["PP"].HeaderText = CoinbookHelper.Erhaltungsgrade[8].Erhaltung;
            }
            catch { };

            grdInput.Style.HeaderStyle.Borders.Bottom = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.Thin);
            grdInput.Style.HeaderStyle.Borders.Left = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
            grdInput.Style.StackedHeaderStyle.Borders.Left = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
        }

        public void CreateStackHeader()
        {
            //Creating object for a stacked header row.
            var stackedHeaderRow1 = new StackedHeaderRow();
            
            //Adding stacked column to stacked columns collection available in stacked header row object.
            //stackedHeaderRow1.StackedColumns.Add(new StackedColumn() { ChildColumns = "Katnr,Währung,Motiv,Jahrgang,HinweisKZ", HeaderText = LanguageHelper.Localization.GetTranslation("Keys", "Münze") });
            stackedHeaderRow1.StackedColumns.Add(new StackedColumn() { ChildColumns = "Katnr,Währung,Motiv,Jahrgang,HinweisKZ", HeaderText = "Münze" });
            stackedHeaderRow1.StackedColumns.Add(new StackedColumn() { ChildColumns = "S,SP,SS,SSP,VZ,VZP,STN,STH,PP", HeaderText = LanguageHelper.Localization.GetTranslation("Keys", "Erhaltungsgrade") });
            stackedHeaderRow1.StackedColumns.Add(new StackedColumn() { ChildColumns = "Datum,Preis", HeaderText = string.Empty });

            //Adding stacked header row object to stacked header row collection available in SfDataGrid.
            grdInput.StackedHeaderRows.Clear();
            grdInput.StackedHeaderRows.Add(stackedHeaderRow1);

            grdInput.Style.StackedHeaderStyle.BackColor = CoinbookHelper.ColorHeader;
            grdInput.Style.StackedHeaderStyle.Font.Bold = true;
            grdInput.Style.StackedHeaderStyle.Font.Size = 10;
            grdInput.Style.StackedHeaderStyle.TextColor = CoinbookHelper.ColorText;

            grdInput.Style.StackedHeaderStyle.Borders.Bottom = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.Medium);
            grdInput.Style.StackedHeaderStyle.Borders.Left = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
            grdInput.Style.StackedHeaderStyle.Borders.Right = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
            grdInput.Style.StackedHeaderStyle.Borders.Top = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);

            grdInput.CellRenderers.Remove("StackedHeader");
            grdInput.CellRenderers.Add("StackedHeader", new CustomStackedHeaderCellRenderer());

            grdInput.CellRenderers.Remove("TextBox");
            grdInput.CellRenderers.Add("TextBox", new CustomTextBoxCellRenderer());
        }

        /// <summary>
        /// DataGridView Spalten an Einstellungen anpassen
        /// </summary>
        private void setGridColumns()
        {
            if (grdInput.Columns.Count > 0)
            {
                grdInput.AllowEditing = true;
                grdInput.RowHeight = 40;
                grdInput.EditorSelectionBehavior = EditorSelectionBehavior.SelectAll;
                grdInput.Style.CellStyle.Font.Size = 10;
                grdInput.ValidationMode = GridValidationMode.InEdit;

                grdInput.Style.CellStyle.Borders.All = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
                grdInput.Style.SelectionStyle.BackColor = CoinbookHelper.ColorSelection;

                grdInput.Columns["Jahrgang"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["Währung"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

                grdInput.Columns["S"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["SP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["SS"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["SSP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["VZ"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["VZP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["STN"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["STH"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["PP"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["Datum"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
                grdInput.Columns["Preis"].CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

                grdInput.Columns["Katnr"].MinimumWidth = 60;
                grdInput.Columns["Jahrgang"].MinimumWidth = 50;
                grdInput.Columns["S"].MinimumWidth = 30;
                grdInput.Columns["SP"].MinimumWidth = 30;
                grdInput.Columns["SS"].MinimumWidth = 30;
                grdInput.Columns["SSP"].MinimumWidth = 30;
                grdInput.Columns["VZ"].MinimumWidth = 30;
                grdInput.Columns["VZP"].MinimumWidth = 30;
                grdInput.Columns["STN"].MinimumWidth = 30;
                grdInput.Columns["STH"].MinimumWidth = 30;
                grdInput.Columns["PP"].MinimumWidth = 30;

                grdInput.Columns["Währung"].MinimumWidth = 30;
                grdInput.Columns["Motiv"].MinimumWidth = 90;

                SetGridColumnWidth();
                SetGridHeaderErhaltungsgrade();

                grdInput.Columns["Motiv"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

                grdInput.Style.HeaderStyle.BackColor = CoinbookHelper.ColorHeader;
                grdInput.Style.HeaderStyle.Font.Bold = true;
                grdInput.Style.HeaderStyle.Font.Size = 8;
                grdInput.HeaderRowHeight = 50;

                grdInput.Columns["S"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                grdInput.Columns["SP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                grdInput.Columns["SS"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                grdInput.Columns["SSP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                grdInput.Columns["VZ"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                grdInput.Columns["VZP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                grdInput.Columns["STN"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                grdInput.Columns["STH"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;
                grdInput.Columns["PP"].HeaderStyle.BackColor = CoinbookHelper.ColorHeader2;

                grdInput.Columns["Datum"].HeaderStyle.BackColor = Color.Aqua;
                grdInput.Columns["Preis"].HeaderStyle.BackColor = Color.Aqua;

                grdInput.Columns["Datum"].ValidationMode = GridValidationMode.InEdit;
                grdInput.Columns["Preis"].ValidationMode = GridValidationMode.InEdit;
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            grdInput.CurrentCell.EndEdit();

            for (int i = 0; i < liste.Count; i++)
            {
                var item = liste[i];

                if (item.Changed != 0)
                {
                    if (item.S != string.Empty) 
                        saveCoin(1, "S", ConvertEx.ToInt32(item.S), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);

                    if (item.SP != string.Empty) 
                        saveCoin(2, "SP", ConvertEx.ToInt32(item.SP), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);

                    if (item.SS != string.Empty) 
                        saveCoin(3, "SS", ConvertEx.ToInt32(item.SS), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);

                    if (item.SSP != string.Empty) 
                        saveCoin(4, "SSP", ConvertEx.ToInt32(item.SSP), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);

                    if (item.VZ != string.Empty) 
                        saveCoin(5, "VZ", ConvertEx.ToInt32(item.VZ), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);

                    if (item.VZP != string.Empty) 
                        saveCoin(6, "VZP", ConvertEx.ToInt32(item.VZP), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);

                    if (item.STN != string.Empty) 
                        saveCoin(7, "STN", ConvertEx.ToInt32(item.STN), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);

                    if (item.STH != string.Empty) 
                        saveCoin(8, "STH", ConvertEx.ToInt32(item.STH), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);

                    if (item.PP != string.Empty) 
                        saveCoin(9, "PP", ConvertEx.ToInt32(item.PP), item.Guid, item.Datum, ConvertEx.ToDecimal(item.Preis), item.Katnr,i);
                }
            }

            Close();
        }

        private void saveCoin(int erhaltung, string erhaltungsgrad, int anzahl, string guid, string datum,
            decimal preis, string katNr, int index)
        {
            Sammlung coin = null;

            // Speichere Sammlung
            for (int i = 0; i < anzahl; i++)
            {
                coin = new Sammlung();
                coin.ID = 0;
                coin.Ablage = string.Empty;
                coin.Doublette = !sammlung;
                coin.EigenerPreis = 0;
                coin.Erhaltung = erhaltung;
                coin.Fehlerhaft = false;
                coin.FehlerText = string.Empty;
                coin.Kaufdatum = datum;
                coin.Kaufort = string.Empty;
                coin.Kaufpreis = preis;
                coin.Verkaeufer = string.Empty;
                coin.Kommentar = string.Empty;

                if (CoinbookHelper.Settings.KatalognummernAnzeige)
                    coin.KatNrEigen = katNr;
                else
                    coin.KatNrEigen = string.Empty;

                coin.Picture = string.Empty;
                coin.Erhaltungsgrad = erhaltungsgrad;
                coin.Katalogpreis = preis;
                coin.Guid = guid;
                coin.NationID = nation;

                liteDatabase.SaveSammlung(coin);
            }

            //Bestand speichern
            liteDatabase.SaveBestand(coin, anzahl, gebiet, aera, nation);

            if (ChangeBestand != null)
                ChangeBestand(this, new CoinEventArgs(index, coin, anzahl));
            CoinbookHelper.Changes = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdInput_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.DataRow.RowType == RowType.DefaultRow)
            {
                if (e.RowIndex % 2 == 0)
                    e.Style.BackColor = CoinbookHelper.ColorAlternateOdd;
                else
                    e.Style.BackColor = CoinbookHelper.ColorAlternateEven;
            }
        }

        private void grdInput_DrawCell(object sender, Syncfusion.WinForms.DataGrid.Events.DrawCellEventArgs e)
        {
            try
            {
                if (e.DataRow.RowType == RowType.DefaultRow)
                {
                    switch (e.Column.MappingName)
                    {
                        case "HinweisKZ":
                            if ((e.DataRow.RowData as QuickInput).HinweisKZ == "!")
                            {
                                showIcon(CoinbookHelper.Hinweis, e.Bounds, e.Graphics, e.Style.BackColor);
                                e.Handled = true;
                            }
                            break;

                        case "PP":
                            if ((e.DataRow.RowData as QuickInput).AuflagePP == "n/a")
                            {
                                drawBackColor(e.Bounds, e.Graphics, Color.Gray);
                                e.Handled = true;
                            }
                            break;

                        case "STH":
                            if ((e.DataRow.RowData as QuickInput).AuflageSTH == "n/a")
                            {
                                drawBackColor(e.Bounds, e.Graphics, Color.Gray);
                                e.Handled = true;
                            }
                            break;
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

        private void drawBackColor(Rectangle rec, Graphics graphics, Color backcolor)
        {
            Rectangle r = new Rectangle(rec.Left + 1, rec.Top + 1, rec.Width - 2, rec.Height - 2);
            graphics.FillRectangle(new SolidBrush(backcolor), r);

            r = new Rectangle(rec.Left, rec.Top, rec.Width - 1, rec.Height - 1);
            graphics.DrawRectangle(new Pen(Color.Black, 1), r);
        }

        private void grdInput_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.DataRow.RowType == RowType.DefaultRow)
            {
                switch (e.DataColumn.GridColumn.MappingName)
                {
                    case "HinweisKZ":
                        if ((e.DataRow.RowData as QuickInput).HinweisKZ == "!")
                        {
                            frmHinweis form = new frmHinweis();
                            form.Guid = (e.DataRow.RowData as QuickInput).Guid;
                            form.ShowDialog(this);
                        }
                        break;
                }

                grdInput.Refresh();
                Cursor = Cursors.Default;
            }
        }

        private void grdInput_CurrentCellBeginEdit(object sender, CurrentCellBeginEditEventArgs e)
        {
            if (e.DataColumn.GridColumn.MappingName == "PP")
                if ((e.DataRow.RowData as QuickInput).AuflagePP == "n/a")
                    e.Cancel = true;

            if (e.DataColumn.GridColumn.MappingName == "STH")
                if ((e.DataRow.RowData as QuickInput).AuflageSTH == "n/a")
                    e.Cancel = true;
        }

        private void grdInput_CurrentCellKeyPress(object sender, CurrentCellKeyPressEventArgs e)
        {
            string currentCellValue = grdInput.CurrentCell.CellRenderer.GetControlValue().ToString();

            e.KeyPressEventArgs.Handled = false;

            switch (grdInput.CurrentCell.Column.MappingName)
            {
                case "S":
                case "SP":
                case "SS":
                case "SSP":
                case "VZ":
                case "VZP":
                case "STN":
                case "STH":
                case "PP":
                    if (zahlen.Contains(e.KeyPressEventArgs.KeyChar))
                        e.KeyPressEventArgs.Handled = !(currentCellValue.Length < 2);
                    else
                        e.KeyPressEventArgs.Handled = true;
                    break;

                case "Datum":
                    if (datum.Contains(e.KeyPressEventArgs.KeyChar))
                        e.KeyPressEventArgs.Handled = !(currentCellValue.Length < 10);
                    else
                        e.KeyPressEventArgs.Handled = true;
                    break;

                case "Preis":
                    if (preis.Contains(e.KeyPressEventArgs.KeyChar))
                        e.KeyPressEventArgs.Handled = !(currentCellValue.Length < 10);
                    else
                        e.KeyPressEventArgs.Handled = true;
                    break;
            }
        }

        private void grdInput_CurrentCellKeyUp(object sender, CurrentCellKeyEventArgs e)
        {
            if (e.KeyEventArgs.KeyCode == Keys.Back)
            {
                //TODO
            }
        }

        private void grdInput_CurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs e)
        {
            int index = grdInput.CurrentCell.RowIndex-2;
            decimal preis = 0;

            liste2[index].Changed = ConvertEx.ToInt32(liste2[index].S) + ConvertEx.ToInt32(liste2[index].SP) + ConvertEx.ToInt32(liste2[index].SS) + ConvertEx.ToInt32(liste2[index].SSP)
                + ConvertEx.ToInt32(liste2[index].VZ) + ConvertEx.ToInt32(liste2[index].VZP) + ConvertEx.ToInt32(liste2[index].STN) + ConvertEx.ToInt32(liste2[index].STH) 
                + ConvertEx.ToInt32(liste2[index].PP);

            if (liste2[index].Changed != 0)
            {
                liste2[index].Datum= DateTime.Now.ToShortDateString();
                btnSave.Enabled = true;
            }
            else
                liste2[index].Datum = string.Empty;

            preis = ConvertEx.ToInt32(liste2[index].S) * liste2[index].PS
                + ConvertEx.ToInt32(liste2[index].S) * liste2[index].PSP
                + ConvertEx.ToInt32(liste2[index].S) * liste2[index].PSS
                + ConvertEx.ToInt32(liste2[index].S) * liste2[index].PSSP
                + ConvertEx.ToInt32(liste2[index].S) * liste2[index].PVZ
                + ConvertEx.ToInt32(liste2[index].S) * liste2[index].PVZP
                + ConvertEx.ToInt32(liste2[index].S) * liste2[index].PSTN
                + ConvertEx.ToInt32(liste2[index].S) * liste2[index].PSTH
                + ConvertEx.ToInt32(liste2[index].S) * liste2[index].PPP;

            liste2[index].Preis = preis != 0 ? string.Format("{0:###,##0.00}", preis) : string.Empty;
        }

        private void grdInput_CurrentCellValidating(object sender, CurrentCellValidatingEventArgs e)
        {
            switch (grdInput.CurrentCell.Column.MappingName)
            {
                case "Datum":
                    if (e.NewValue.ToString() != string.Empty)
                    {
                        DateTime test = new DateTime();
                        if (!DateTime.TryParse(e.NewValue.ToString(), out test))
                        {
                            e.IsValid = false;
                            e.ErrorMessage = "Das erfasste Datum ist ungültig";

                            MessageBoxAdv.Show(this, e.ErrorMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    break;

                case "Preis":
                    if (e.NewValue.ToString() != string.Empty)
                    {
                        Decimal test = new Decimal();
                        if (!Decimal.TryParse(e.NewValue.ToString(), out test))
                        {
                            e.IsValid = false;
                            e.ErrorMessage = "Der erfasste Preis ist ungültige";

                            MessageBoxAdv.Show(this, e.ErrorMessage, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        }
                    }

                    break;
            }
        }

        private void optChooseBestand(object sender, EventArgs e)
        {
          
        }
    }
}