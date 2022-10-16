using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using SAN.Converter;
using SAN.FTP;
using Coinbook.Enumerations;
using Coinbook.Lokalisierung;
using Syncfusion.Windows.Forms;
using Coinbook.Model;
using System.Linq;
using System.IO;
using Coinbook.Helper;

namespace Coinbook.Modulverwaltung
{
    public partial class frmOrder : Form
    {
        decimal mwstLand = 19;
        decimal mwstDeutschland = 19;
        decimal mwst = 0;
        decimal porto = 12;
        decimal neuSumme = 0;
        decimal updateSumme = 0;
        decimal rabatt = 0;
        decimal rabattU = 0;
        decimal gesamt = 0;
        decimal rabattSatz = 0;
        decimal rabattSatzU = 0;
        XmlNodeList rabattListeNeu;
        XmlNodeList rabattListeUpdate;
        string bestellnummer = BestellungHelper.Bestellnummer;

        frmOrder2 formOrder2 = null;

        public frmOrder()
        {
            InitializeComponent();

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook.Modulverwaltung");
            LanguageHelper.CreateLocalization(resourcePath);

            LanguageHelper.Localization.UpdateLanguage("de");

            LanguageHelper.Localization.UpdateModul(this);

            formOrder2 = new frmOrder2();

            Syncfusion.Windows.Forms.Grid.GridControl grdOrder = new Syncfusion.Windows.Forms.Grid.GridControl();

            try
            {
                FTPClass ftp = new FTPClass();
                if (ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1"))
                {
                    string xml = ftp.DownloadString("Info/preisliste.xml");

                    readPreisliste(xml);

                    lblMwst.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "lblMwst"), string.Format("{0:#0.0}", mwst), string.Format("{0:###0.00}", 0));
                }
                else
                {
                    MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "msgNoOrder"), "Coinbook");
                    Close();
                }
            }
            catch
            {
                MessageBoxAdv.Show("Entweder gibt es Probleme mit dem Internet, oder die Preisliste ist defekt", "Coinbook");
                Close();
            }
        }

        private void readPreisliste(string xml)
        {
            XmlDocument document = new XmlDocument();

            document.LoadXml(xml);
            XmlNode root = document.SelectSingleNode("Root");

            XmlNode modulliste = root.SelectSingleNode("Module");
            XmlNodeList module = modulliste.SelectNodes("modul");

            XmlNode rabattNeu = root.SelectSingleNode("RabattNeu");
            XmlNode rabattUpdate = root.SelectSingleNode("RabattUpdate");

            XmlNode version = root.SelectSingleNode("Version");
            XmlNode info = version.SelectSingleNode("Info");

            var nationen = GetNations();

            List<Bestellung> update = new List<Bestellung>();
            List<Bestellung> neu = new List<Bestellung>();

            lblVersion.Text = info.Attributes[0].Value.ToString();

            for (int j = 0; j < module.Count; j++)
            {
                bool found = false;

                if (Convert.ToInt32(module[j].Attributes["Bestellnummer"].Value) != -1)
                {
                    foreach (var nation in nationen)
                    {
                        string temp = nation.Key;
                        if (temp == "EU 2 Euro-Modul")
                            temp = "EU 2 Euro Modul";

                        if (module[j].Attributes["Name"].Value.ToString() == temp)
                        {
                            Bestellung item = new Bestellung();
                            item.Bestellen = false;
                            item.Nummer = "CBU" + string.Format("{0:000}", Convert.ToInt32(module[j].Attributes["Bestellnummer"].Value));
                            item.Name = module[j].Attributes["Name"].Value.ToString();
                            item.Beschreibung = module[j].Attributes["Beschreibung"].Value.ToString();
                            item.Preis = ConvertEx.ToDecimal(module[j].Attributes["PreisUpdate"].Value);
                            item.Währung = "€";
                            item.Version = module[j].Attributes["Version"].Value.ToString();

                            if (ConvertEx.ToDouble(item.Preis) != 0)
                            {
                                update.Add(item);
                                found = true;
                            }

                            break;
                        }
                    }

                    if (!found && Convert.ToInt32(module[j].Attributes["Bestellnummer"].Value) != 0)
                    {
                        Bestellung item = new Bestellung();
                        item.Bestellen = false;
                        item.Nummer = "CBM" + string.Format("{0:000}", Convert.ToInt32(module[j].Attributes["Bestellnummer"].Value));
                        item.Name = module[j].Attributes["Name"].Value.ToString();
                        item.Beschreibung = module[j].Attributes["Beschreibung"].Value.ToString();
                        item.Preis = ConvertEx.ToDecimal(module[j].Attributes["PreisNeu"].Value);
                        item.Währung = "€";
                        item.Version = module[j].Attributes["Version"].Value.ToString();

                        if (ConvertEx.ToDouble(item.Preis) != 0)
                            neu.Add(item);
                    }
                }
            }

            rabattListeNeu = rabattNeu.SelectNodes("Grenze");
            rabattListeUpdate = rabattUpdate.SelectNodes("Grenze");

            grdUpdate.DataSource = update;
            grdNeu.DataSource = neu;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAlle_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdUpdate.RowCount; i++)
                grdUpdate.Rows[i].Cells["colCheck"].Value = true;

            berechne();
        }

        private void grdUpdate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdUpdate.Columns[e.ColumnIndex].Name == "colCheck")
            {
                grdUpdate.Rows[e.RowIndex].Cells["colCheck"].Value = !ConvertEx.ToBoolean(grdUpdate.Rows[e.RowIndex].Cells["colCheck"].Value);
                berechne();
            }
        }

        private void berechne()
        {
            rabatt = 0;
            gesamt = 0;
            neuSumme = 0;
            updateSumme = 0;
            decimal rabattGrenze = 0;
            decimal rabattGrenzeU = 0;

            for (int i = 0; i < grdUpdate.RowCount; i++)
                if (ConvertEx.ToBoolean(grdUpdate.Rows[i].Cells["colCheck"].Value) == true)
                    updateSumme = updateSumme + ConvertEx.ToDecimal(grdUpdate.Rows[i].Cells["colPreis"].Value);

            for (int i = 0; i < grdNeu.RowCount; i++)
                if (ConvertEx.ToBoolean(grdNeu.Rows[i].Cells["colCheckNeu"].Value) == true)
                    neuSumme = neuSumme + ConvertEx.ToDecimal(grdNeu.Rows[i].Cells["colPreisNeu"].Value);

            for (int i = 0; i < rabattListeNeu.Count; i++)
                if (neuSumme >= ConvertEx.ToDecimal(rabattListeNeu[i].Attributes["Betrag"].Value))
                {
                    rabattGrenze = ConvertEx.ToDecimal(rabattListeNeu[i].Attributes["Betrag"].Value);
                    rabattSatz = ConvertEx.ToDecimal(rabattListeNeu[i].Attributes["Rabatt"].Value);
                }

            rabatt = 0;

            if (neuSumme >= rabattGrenze)
                rabatt = neuSumme * rabattSatz / 100;

            rabattGrenzeU = 99999;
            rabattSatzU = 0;
            for (int i = 0; i < rabattListeUpdate.Count; i++)
                if (updateSumme >= ConvertEx.ToDecimal(rabattListeUpdate[i].Attributes["Betrag"].Value))
                {
                    rabattGrenzeU = ConvertEx.ToDecimal(rabattListeUpdate[i].Attributes["Betrag"].Value);
                    rabattSatzU = ConvertEx.ToDecimal(rabattListeUpdate[i].Attributes["Rabatt"].Value);
                }

            if (updateSumme >= rabattGrenzeU)
                rabattU = updateSumme * rabattSatzU / 100;

            lblSummeUpdate.Text = string.Format("{0:###0.00 €}", updateSumme);
            lblSummeNeu.Text = string.Format("{0:###0.00 €}", neuSumme);

            if (rabatt != 0)
            {
                lblRabatt.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "lblRabatt"), rabattGrenze, rabattSatz);
                lblRabattBetrag.Text = string.Format("{0:###0.00 €}", -rabatt);
            }
            else
                lblRabattBetrag.Text = String.Empty;

            if (rabattU != 0)
            {
                lblRabattU.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "lblRabattU"), rabattSatzU);
                lblRabattBetragU.Text = string.Format("{0:###0.00 €}", -rabattU);
            }
            else
                lblRabattU.Text = String.Empty;

            gesamt = neuSumme + updateSumme - rabatt - rabattU;

            if (optDownload.Checked)
            {
                mwst = mwstLand;
                lblVersand.Text = String.Empty;
            }
            else
            {
                mwst = mwstDeutschland;
                lblVersand.Text = string.Format("{0:###0.00 €}", porto);
                gesamt = gesamt + porto;
            }

            lblGesamt.Text = string.Format("{0:###0.00 €}", gesamt);

            lblMwst.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "lblMwst"), string.Format("{0:#0.0}", mwst), string.Format("{0:###0.00}", gesamt * mwst / 100));
        }

        private void optDownload_CheckedChanged(object sender, EventArgs e)
        {
            berechne();
        }

        private void grdNeu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdNeu.Columns[e.ColumnIndex].Name == "colCheckNeu")
            {
                grdNeu.Rows[e.RowIndex].Cells["colCheckNeu"].Value = !ConvertEx.ToBoolean(grdNeu.Rows[e.RowIndex].Cells["colCheckNeu"].Value);
                berechne();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            List<Artikel> artikelListe = new List<Artikel>();

            for (int i = 0; i < grdUpdate.RowCount; i++)
            {
                if (ConvertEx.ToBoolean(grdUpdate.Rows[i].Cells["colCheck"].Value))
                {
                    Artikel item = new Artikel();
                    item.Artikelnummer = grdUpdate.Rows[i].Cells["colNummer"].Value.ToString();
                    item.Bezeichnung = grdUpdate.Rows[i].Cells["colModul"].Value.ToString();
                    item.Version = grdUpdate.Rows[i].Cells["colVersion"].Value.ToString();
                    item.Preis = string.Format("{0:#0.00}", grdUpdate.Rows[i].Cells["colPreis"].Value) + " " + grdUpdate.Rows[i].Cells["colWaehrung"].Value.ToString();
                    artikelListe.Add(item);
                }
            }

            for (int i = 0; i < grdNeu.RowCount; i++)
            {
                if (ConvertEx.ToBoolean(grdNeu.Rows[i].Cells["colCheckNeu"].Value))
                {
                    Artikel item = new Artikel();
                    item.Artikelnummer = grdNeu.Rows[i].Cells["colNummerNeu"].Value.ToString();
                    item.Bezeichnung = grdNeu.Rows[i].Cells["colModulNeu"].Value.ToString();
                    item.Version = grdNeu.Rows[i].Cells["colVersionNeu"].Value.ToString();
                    item.Preis = string.Format("{0:#0.00}", grdNeu.Rows[i].Cells["colPreisNeu"].Value) + " " + grdNeu.Rows[i].Cells["colWaehrungNeu"].Value.ToString();
                    artikelListe.Add(item);
                }
            }

            formOrder2.Bestellnummer = bestellnummer;
            formOrder2.ArtikelListe = artikelListe;
            formOrder2.UpdateSumme = updateSumme;
            formOrder2.NeuSumme = neuSumme;
            formOrder2.Rabattsatz = rabattSatz;
            formOrder2.Rabatt = rabatt + rabattU;
            formOrder2.GesamtSumme = gesamt;
            formOrder2.Mwst = mwst;
            formOrder2.Steuer = gesamt * mwst / 100;

            if (optVersand.Checked)
                formOrder2.Versand = porto;

            if (optDownload.Checked)
                formOrder2.Lieferung = enmLieferung.Download;
            else
                formOrder2.Lieferung = enmLieferung.Versand;

            if (optPaypal.Checked)
                formOrder2.Zahlung = enmZahlung.Paypal;
            else
                formOrder2.Zahlung = enmZahlung.Vorkasse;

            TopMost = false;

            //formOrder2.TopMost = true;
            formOrder2.ShowDialog();
            //formOrder2.TopMost = false;

            TopMost = true;
            Cursor = Cursors.Default;

            if (formOrder2.Cancel)
                Close();
        }

        private void frmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            formOrder2.Hide();
        }

        private List<Nation> GetNations(string all = null)
        {
            var nationen = DatabaseHelper.LiteDatabase.ReadNationen();

            nationen = nationen.OrderBy(x => x.Bezeichnung).ToList();

            if (!string.IsNullOrEmpty(all))
            {
                Nation item = new Nation();
                item.ID = 0;
                item.Bezeichnung = all;
                nationen.Insert(0, item);
            }

            return nationen;
        }
    }
}

