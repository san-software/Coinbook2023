using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Coinbook.Model;
using Syncfusion.WinForms.DataGrid.Styles;
using System.Drawing;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Enums;
using System.Diagnostics;
using Coinbook.Lokalisierung;
using Coinbook.Helper;

namespace Coinbook
{
    public partial class frmPrägeanstalten : Form
    {
        List<PraegeanstaltAnzeige> liste = new List<PraegeanstaltAnzeige>();

        public frmPrägeanstalten()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);

            sfDataGrid1.Style.CellStyle.Font = new GridFontInfo(new Font("Arial", 10));
            sfDataGrid1.Style.HeaderStyle.BackColor = CoinbookHelper.ColorHeader;
            sfDataGrid1.Style.HeaderStyle.Font.Bold = true;
            sfDataGrid1.Style.HeaderStyle.Font.Size = 8;
            sfDataGrid1.HeaderRowHeight = 40;
            sfDataGrid1.Style.HeaderStyle.Borders.Bottom = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.Thin);
            sfDataGrid1.Style.HeaderStyle.Borders.Left = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);

            sfDataGrid1.AllowEditing = false;
            sfDataGrid1.RowHeight = 40;

            sfDataGrid1.AllowResizingColumns = false;
            sfDataGrid1.AllowSorting = false;

            sfDataGrid1.Style.CellStyle.Borders.All = new GridBorder(CoinbookHelper.ColorGridlines, GridBorderWeight.ExtraThin);
            sfDataGrid1.Style.SelectionStyle.BackColor = CoinbookHelper.ColorSelection;
            sfDataGrid1.AutoGenerateColumnsMode = AutoGenerateColumnsMode.SmartReset;

            sfDataGrid1.ToolTipOpening += sfDataGrid1_ToolTipOpening;

            sfDataGrid1.QueryCellStyle += new QueryCellStyleEventHandler(this.sfDataGrid1_QueryCellStyle);
            //sfDataGrid1.DrawCell += new DrawCellEventHandler(this.sfDataGrid1_DrawCell);
            sfDataGrid1.CellClick += new CellClickEventHandler(this.sfDataGrid1_CellClick);
        }

        public int NationID { get; set; }
        public string Nation { get; set; }

        public new void ShowDialog(IWin32Window window)
        {
            var praegeAnstalt = DatabaseHelper.LiteDatabase.ReadPraegestellen(NationID,CoinbookHelper.ModulKey);

            foreach (var item in praegeAnstalt)
            {
                PraegeanstaltAnzeige anstalt = new PraegeanstaltAnzeige();
                anstalt.Land = item.Land;
                anstalt.Muenzzeichen = item.Muenzzeichen + Environment.NewLine + item.Zeit;
                anstalt.Ort = item.Ort + Environment.NewLine + item.Email;
                anstalt.Adresse = item.Adresse + Environment.NewLine + item.Homepage;
                anstalt.Bemerkung = item.Bemerkung;

                liste.Add(anstalt);
            }

            sfDataGrid1.DataSource = liste;

            Text = "Coinbook - " + LanguageHelper.Localization.GetTranslation(Name, Name) + " - " + Nation;

            sfDataGrid1.Columns["Land"].Width = 150;
            sfDataGrid1.Columns["Muenzzeichen"].Width = 100;
            sfDataGrid1.Columns["Ort"].Width = 270;
            sfDataGrid1.Columns["Adresse"].Width = 270;
            sfDataGrid1.Columns["Bemerkung"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

            sfDataGrid1.Columns["Land"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colLand");
            sfDataGrid1.Columns["Muenzzeichen"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colMZZ");
            sfDataGrid1.Columns["Ort"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colOrt");
            sfDataGrid1.Columns["Adresse"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colAdresse");
            sfDataGrid1.Columns["Bemerkung"].HeaderText = LanguageHelper.Localization.GetTranslation(Name, "colBemerkung");

            sfDataGrid1.Columns["Land"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
            sfDataGrid1.Columns["Muenzzeichen"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
            sfDataGrid1.Columns["Ort"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
            sfDataGrid1.Columns["Adresse"].CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
            sfDataGrid1.Columns["Bemerkung"].CellStyle.HorizontalAlignment = HorizontalAlignment.Left;

            sfDataGrid1.Columns["Land"].Visible = (NationID == 12 || NationID == 31);
            base.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sfDataGrid1_ToolTipOpening(object sender, ToolTipOpeningEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                e.ToolTipInfo.Items[0].Text = e.DisplayText;
            }
        }

        private void sfDataGrid1_CellClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (e.DataRow.RowType == RowType.DefaultRow)
            {
                if (e.MouseEventArgs.Button == MouseButtons.Right)
                {
                    //Localization.TranslateContextMenu(Name, cmnuStrip);
                    //cmnuStrip.Show(new Point(MousePosition.X, MousePosition.Y));
                }
                else
                {
                    if (e.DataColumn.GridColumn.MappingName == "Ort")
                    {
                            Cursor = Cursors.WaitCursor;

                        var temp = liste[e.DataRow.Index - 1].Ort;
                        string url = temp.Substring(temp.IndexOf(Environment.NewLine) + 2);

                        if (url != string.Empty)
                        {
                            url = string.Format("mailto:{0}", url);
                            Process.Start(url);
                        }
                    }
                    else if(e.DataColumn.GridColumn.MappingName == "Adresse")
                    {
                        Cursor = Cursors.WaitCursor;

                        var temp = liste[e.DataRow.Index-1].Adresse;
                        string url = temp.Substring(temp.IndexOf(Environment.NewLine)+2);

                        if (url != string.Empty)
                            Process.Start(url);
                    }
                }

                sfDataGrid1.Refresh();
                Cursor = Cursors.Default;
            }
        }

        private void sfDataGrid1_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = CoinbookHelper.ColorAlternateOdd;
            else
                e.Style.BackColor = CoinbookHelper.ColorAlternateEven;
        }
    }
}
