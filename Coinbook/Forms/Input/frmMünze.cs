using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using Coinbook.Model;
using Coinbook.Enumerations;
using SAN.Converter;
using LiteDB.Database;
using Coinbook.Lokalisierung;
using Syncfusion.Windows.Forms;
using Coinbook.Helper;
using Coinbook.EventHandlers;

namespace Coinbook
{
    public partial class frmMünze : Form
    {
        private CultureInfo cultureInfo = CultureInfo.GetCultureInfo(CoinbookHelper.Settings.Culture);

        public event CoinEventHandler ChangeCoin;

        public frmMünze()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);
        }

        public Sammlung Coin { get; set; }
        public int Index { get; set; }
        public string Nation { get; set; }
        public string Ära { get; set; }
        public string Gebiet { get; set; }
        public bool Edit { get; set; }
        public string Caller { get; set; }
        public int Anzahl { get; set; }

        public new DialogResult ShowDialog(IWin32Window window)
        {
            decimal preis = 0;
            decimal katalogpreis = 0;

            cboErhaltungsgrad.DisplayMember = "Erhaltung";
            cboErhaltungsgrad.ValueMember = "ErhaltungsgradID";
            cboErhaltungsgrad.DataSource = CoinbookHelper.Erhaltungsgrade;

            if (Coin != null)
            {
                cboErhaltungsgrad.SelectedValue = Coin.Erhaltung;
                txtKatNrEigen.Text = Coin.KatNr;
            }
            txtAblage.Text = CoinbookHelper.Settings.Ablage;
            txtKaufdatum.Text = DateTime.Today.ToShortDateString();
            txtAnzahl.Text = "1";
            txtAnzahl.Enabled = !Edit;

            katalogpreis = GetKatalogPreis(enmPreise.Katalogpreise);
            preis = katalogpreis;

            if (CoinbookHelper.Settings.Preise == enmPreise.EigenePreise)
                preis = GetEigenerPreis(enmPreise.Katalogpreise);

            if (preis == 0)
                txtKatalogpreis.Text = String.Empty;
            else
                txtKatalogpreis.Text = String.Format(cultureInfo, "{0:###,##0.00}", preis*CoinbookHelper.Settings.CurrentFaktor);

            if (CoinbookHelper.Settings.Preisvorgabe)
                txtKaufpreis.Text = String.Format(cultureInfo, "{0:###,##0.00}", preis * CoinbookHelper.Settings.CurrentFaktor);

            if (preis != 0)
                txtEigenerPreis.Text = String.Format(cultureInfo, "{0:###,##0.00}", preis);

            lblGebietText.Text = string.Format("{0} - {1} - {2}", Nation, Ära, Gebiet);
            lblMünzeText.Text = string.Format("{0} {1} - {2} - {3}", 
                CoinbookHelper.MuenzkatalogFiltered[Index].Nominal, 
                CoinbookHelper.MuenzkatalogFiltered[Index].Waehrung, 
                CoinbookHelper.MuenzkatalogFiltered[Index].Jahrgang, 
                CoinbookHelper.MuenzkatalogFiltered[Index].Muenzzeichen);

            lblWährung.Text = CoinbookHelper.Settings.CurrentWährung;
            lblWährung1.Text = CoinbookHelper.Settings.CurrentWährung;
            lblWaehrung2.Text = CoinbookHelper.Settings.CurrentWährung;

            loadDetails();

            btnSave.Enabled = cboErhaltungsgrad.Text != string.Empty;

            Graphics graphics;
            graphics = this.CreateGraphics();
            int dpi = ConvertEx.ToInt32(graphics.DpiX);

            switch (dpi)
            {
                case 120: //For 125% fonts
                    Width = 940;
                    Height = 490;
                    break;

                case 144: //For 150% fonts
                    break;

                default:
                    break;
            }

            Text = LanguageHelper.Localization.GetTranslation(Name, "frmMünze2");

            return base.ShowDialog();
        }

        private void loadDetails()
        {
            if (Edit)
            {
                chkDoublette.Checked = Coin.Doublette;
                chkDoublette.Tag = Coin.Doublette;
                txtAblage.Text = Coin.Ablage;
                txtKaufdatum.Text = Coin.Kaufdatum;
                txtKaufort.Text = Coin.Kaufort;
                txtVerkäufer.Text = Coin.Verkaeufer;
                txtKaufpreis.Text = String.Format(cultureInfo, "{0:###,##0.00}", Coin.Kaufpreis);

                txtKatNrEigen.Text = Coin.KatNrEigen;
                txtKommentar.Text = Coin.Kommentar;
                chkFehlerhaft.Checked = Coin.Fehlerhaft;
                txtFehlerNotiz.Text = Coin.FehlerText;

                if (!Edit)
                    btnSave.Enabled = false;
            }
        }

        /// <summary>
        /// Ohne speichern schließen
        /// </summary>
        /// 
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void chkFehlerhaft_CheckedChanged(object sender, EventArgs e)
        {
            txtFehlerNotiz.Enabled = chkFehlerhaft.Checked;

            if (!chkFehlerhaft.Checked)
                txtFehlerNotiz.Text = string.Empty;

            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Anzahl = ConvertEx.ToInt32(txtAnzahl.Text);

            // Speichere Sammlung
            for (int i = 0; i < Anzahl; i++)
            {
                if (!Edit)
                    Coin.ID = 0;

                Coin.Ablage = txtAblage.Text;
                Coin.Doublette = chkDoublette.Checked;
                Coin.EigenerPreis = ConvertEx.ToDecimal(txtEigenerPreis.Text);
                Coin.Erhaltung = (int)cboErhaltungsgrad.SelectedValue;
                Coin.Fehlerhaft = chkFehlerhaft.Checked;
                Coin.FehlerText = txtFehlerNotiz.Text;
                Coin.Kaufdatum = txtKaufdatum.Text;
                Coin.Kaufort = txtKaufort.Text;
                Coin.Kaufpreis = ConvertEx.ToDecimal(txtKaufpreis.Text);
                Coin.Verkaeufer = txtVerkäufer.Text;
                Coin.Kommentar = txtKommentar.Text;
                Coin.KatNrEigen = txtKatNrEigen.Text;
                Coin.Picture = "";
                Coin.Erhaltungsgrad = cboErhaltungsgrad.Text;
                Coin.Katalogpreis = ConvertEx.ToDecimal(txtKatalogpreis.Text);

                DatabaseHelper.LiteDatabase.SaveSammlung(Coin);
            }

            //Bestand speichern
            if (!Edit)
            {
                DatabaseHelper.LiteDatabase.SaveBestand(
                    Coin, 
                    Anzahl, 
                    CoinbookHelper.MuenzkatalogFiltered[Index].RegionID, 
                    CoinbookHelper.MuenzkatalogFiltered[Index].AeraID, 
                    CoinbookHelper.MuenzkatalogFiltered[Index].NationID);

                if (ChangeCoin != null)
                    ChangeCoin(this, new CoinEventArgs(Index, Coin, Anzahl));
            }

            CoinbookHelper.Changes = true;

            btnSave.Enabled = false;

            DialogResult = DialogResult.OK;
        }

        private new void TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = (cboErhaltungsgrad.SelectedIndex != -1);
        }

        private void frmMünze_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSave.Enabled)
            {
                string text = LanguageHelper.Localization.GetTranslation(Name, "msgSave");

                if (MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    btnSave_Click(null, null);
            }
        }

        public decimal GetKatalogPreis(enmPreise preistyp)
        {

            decimal preis = 0;

            switch (Coin.Erhaltung)
            {
                case 1:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].SPreis;
                    break;

                case 2:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].SPPreis;
                    break;

                case 3:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].SSPreis;
                    break;

                case 4:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].SSPPreis;
                    break;

                case 5:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].VZPreis;
                    break;

                case 6:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].VZPPreis;
                    break;

                case 7:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].STNPreis;
                    break;

                case 8:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].STHPreis;
                    break;

                case 9:
                    preis = CoinbookHelper.MuenzkatalogFiltered[Index].PPPreis;
                    break;
            }

            return preis;
        }

        public decimal GetEigenerPreis(enmPreise preistyp)
        {
            decimal preis = 0;
            var preise = DatabaseHelper.LiteDatabase.GetEigenePreise(CoinbookHelper.MuenzkatalogFiltered[Index].GUID);

            if (preise != null)
            {
                switch (Coin.Erhaltung)
                {
                    case 1:
                        preis = preise.SPreis;
                        break;

                    case 2:
                        preis = preise.SPPreis;
                        break;

                    case 3:
                        preis = preise.SSPreis;
                        break;

                    case 4:
                        preis = preise.SSPPreis;
                        break;

                    case 5:
                        preis = preise.VZPreis;
                        break;

                    case 6:
                        preis = preise.VZPPreis;
                        break;

                    case 7:
                        preis = preise.STNPreis;
                        break;

                    case 8:
                        preis = preise.STHPreis;
                        break;

                    case 9:
                        preis = preise.PPPreis;
                        break;
                }
            }
            return preis;
        }
    }
}

