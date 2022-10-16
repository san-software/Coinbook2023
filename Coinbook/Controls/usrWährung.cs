using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Coinbook.Model;

using SAN.Converter;
using Coinbook.Helper;

namespace Coinbook
{
    partial class usrWährung : UserControl
    {
        public event EventHandler Changed;
        private bool init = true;
        List<Culture> liste = DatabaseHelper.LiteDatabase.ReadCulture();

        public usrWährung()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);
        }

        public void Init()
        {
            LanguageHelper.Localization.UpdateModul(this);

            grdÜbersicht.AutoGenerateColumns = false;

            init = true;

            cboWährung.DataSource = liste;
            cboWährung.DisplayMember = "Bezeichnung";
            cboWährung.ValueMember = "id";

            grdÜbersicht.DataSource = liste;
            grdÜbersicht.Columns["colFaktor"].DefaultCellStyle.Format = "###,##0.00";
            //grdÜbersicht.Columns["colFaktor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //grdÜbersicht.Columns["Waehrung"].Visible = false;
            //grdÜbersicht.Columns["Kultur"].Visible = false;
            //grdÜbersicht.Columns["EN_GB"].Visible = false;
            grdÜbersicht.Columns["colID"].Visible = false;

            cboWährung.SelectedIndex = CoinbookHelper.Settings.CurrentCurrency;
            init = false;
        }

        public void Save()
        {
            grdÜbersicht.EndEdit();
            DatabaseHelper.LiteDatabase.SaveCulture((List<Culture>)grdÜbersicht.DataSource);

            CoinbookHelper.Settings.CurrentCurrency = cboWährung.SelectedIndex;
        }

        private void cboWährung_SelectedIndexChanged(object sender, EventArgs e)
        {
            CoinbookHelper.Settings.CurrentCurrency = cboWährung.SelectedIndex;
            CoinbookHelper.Settings.CurrentWährung = liste[cboWährung.SelectedIndex].Waehrung;
            CoinbookHelper.Settings.CurrentFaktor = liste[cboWährung.SelectedIndex].Faktor;
            setChanged();
        }

        private void grdÜbersicht_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            setChanged();
        }

        private void setChanged()
        {
            if (!init)
                if (Changed != null)
                    Changed(null, null);
        }

    }
}
