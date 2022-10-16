using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using AutoUpdater;
using System.IO;
using System.Diagnostics;
using SAN.Magnifier;
using Syncfusion.Windows.Forms;
using Coinbook.Model;
using Coinbook.Enumerations;
using Coinbook.Lokalisierung;
using Coinbook.Sprache;
using SAN.Plugin;
using Coinbook.Helper;
using SAN.UIControls;
using Coinbook.Import;
using Coinbook.EventHandlers;
using System.IO.MemoryMappedFiles;

namespace Coinbook
{

    public partial class frmMain : Form
    {
        //private SQL sql = new SQL();
        private AutoUpdate updater;
        private frmMagnifier magnifierForm;
        private ModulImport modulImport;
        private bool updateEnable = false;
        private string updateValue = string.Empty;
        private Magnifier magnifier;
        //private frmMuenzDetails formMünzdetails;
        private PluginServices plugins;

        public frmMain(enmAktivierung activated, AutoUpdate updater)
        {
            InitializeComponent();

            loadPlugins();

            LanguageHelper.Localization.UpdateModul(this);
            LanguageHelper.Localization.TranslateContextMenu(Name, mnuWert);
            LanguageHelper.Localization.TranslateContextMenu(Name, mnuPrint);
            createLanguageMenu();

            if (Directory.GetFiles(Path.Combine(CoinbookHelper.DataPath, "Katalog")).Length == 0)
                modulUpdates();

            ctlMain.FillNations();

            this.updater = updater;

            Text = "Coinbook - Version " + Application.ProductVersion;

            if (!CoinbookHelper.Settings.Maximized)
            {
                Top = CoinbookHelper.Settings.Top;
                Left = CoinbookHelper.Settings.Left;
                Width = CoinbookHelper.Settings.Width;
                Height = CoinbookHelper.Settings.Height;
                WindowState = FormWindowState.Maximized;
            }

            btnUpdate.Enabled = false;
            mnuUpdate.Enabled = false;

            if (activated == enmAktivierung.gesperrt)
                CoinbookHelper.IsActivated = false;

            if (activated == enmAktivierung.warten)
                CoinbookHelper.IsActivated = true;

            if (activated == enmAktivierung.aktiviert)
                CoinbookHelper.IsActivated = true;

            mnuModule.Enabled = CoinbookHelper.IsActivated;
            mnuWert.Enabled = CoinbookHelper.IsActivated;
            mnuWerteDoublette.Enabled = CoinbookHelper.IsActivated;
            mnuWerteSammlung.Enabled = CoinbookHelper.IsActivated;
            mnuReportingSammlung.Enabled = CoinbookHelper.IsActivated;
            mnuReportingFehllisten.Enabled = CoinbookHelper.IsActivated;
            mnuReportingDoubletten.Enabled = CoinbookHelper.IsActivated;
            mnuKostenDoubletten.Enabled = CoinbookHelper.IsActivated;
            mnuKostenSammlung.Enabled = CoinbookHelper.IsActivated;
            mnuEdit.Enabled = CoinbookHelper.IsActivated;
            mnuWeb.Enabled = CoinbookHelper.IsActivated;
            mnuNavigation.Enabled = CoinbookHelper.IsActivated;
            btnPrägeanstalt.Enabled = CoinbookHelper.IsActivated;
            btnPrint1.Enabled = CoinbookHelper.IsActivated;
            btnWert.Enabled = CoinbookHelper.IsActivated;
            btnSettings.Enabled = CoinbookHelper.IsActivated;
            btnKurse.Enabled = CoinbookHelper.IsActivated;
            btnBackup.Enabled = CoinbookHelper.IsActivated;
            mnuImportCB2006.Enabled = CoinbookHelper.IsActivated;
            mnuReorg.Enabled = CoinbookHelper.IsActivated;
            mnuImport2x.Enabled = CoinbookHelper.IsActivated;

            if (!string.IsNullOrEmpty(CoinbookHelper.Settings.CloudBackup))
            {
                var temp = CoinbookHelper.Settings.CloudBackup.Split('|');
                mnuCloudBackupBestellen.Enabled = !(DateTime.Now >= Convert.ToDateTime(temp[1]) && DateTime.Now <= Convert.ToDateTime(temp[2]));
            }

            string file = Path.Combine(CoinbookHelper.DataPath, "Coinbook.mdb");
            mnuImportCoinbook3.Enabled = File.Exists(file);

            modulImport = new ModulImport();

            modulImport.ModulProcess += ModulImport_ModulProcess;
            modulImport.TableProcess += ModulImport_TableProcess;
            modulImport.ModulReady += ModulImport_ModulReady;
            modulImport.DownloadReady += ModulImport_DownloadReady;

            mnuPrägeanstalten.Enabled = (ctlMain.NationID != -1 && ctlMain.AeraID != -1 && ctlMain.RegionID != -1);
            EnableEditMenues(false, null);

            ctlMain.ChangeRecordCount += CtlMain_ChangeRecordCount;
            ctlMain.Init();

            Cursor = Cursors.Default;
        }

        private void CtlMain_ChangeRecordCount(object sender, EventArgs e)
        {
            lblRecords.Text = sender.ToString();
            //lblRecords.Text = Convert.ToInt32(sender).ToString();
        }

        protected void enableButton(bool boolEnable)
        {
            ctlMain.EnableButton(boolEnable);
            mnuNavigation.Enabled = boolEnable;
            mnuPrägeanstalten.Enabled = true;
            Update();
        }

        /// <summary>
        /// Eintellungen in Settings schreiben.
        /// </summary>
        private void SaveSettings()
        {
            if (WindowState == FormWindowState.Maximized)
                CoinbookHelper.Settings.Maximized = true;
            else
            {
                CoinbookHelper.Settings.Width = Width;
                CoinbookHelper.Settings.Height = Height;
                CoinbookHelper.Settings.Left = Left;
                CoinbookHelper.Settings.Top = Top;
                CoinbookHelper.Settings.Maximized = false;
            }
        }

        /// <summary>
        /// Münzedetails anzeigen Menübar
        /// </summary>
        /// 
        private void mnuMünzdetails_Click(object sender, EventArgs e)
        {
            ctlMain.ShowDetails(ctlMain.Index);
        }

        /// <summary>
        /// Einstellungen öffnen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuSettings.Text);
            frmSettings form = new frmSettings();

            form.LanguageChanged += Form_LanguageChanged;
            form.ErhaltungChanged += Form_ErhaltungChanged;

            form.ShowDialog(this);

            ctlMain.ShowSelectedStyle();

            form.LanguageChanged -= Form_LanguageChanged;
            form.ErhaltungChanged -= Form_ErhaltungChanged;

            if (DatabaseHelper.LiteDatabase.Count("tblAera", CoinbookHelper.ModulKey) != 0)
                ctlMain.Navigate();

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void Form_ErhaltungChanged(object sender, EventArgs e)
        {
            CoinbookHelper.Erhaltungsgrade = DatabaseHelper.LiteDatabase.ReadErhaltungsgrade(CoinbookHelper.Settings.International);

            ctlMain.SetGridHeaderErhaltungsgrade();
        }

        private void Form_LanguageChanged(object sender, EventArgs e)
        {
            LanguageHelper.Localization.UpdateLanguage(CoinbookHelper.Settings.Culture.Substring(0, 2));
            LanguageHelper.Localization.UpdateModul(this);

            CoinbookHelper.Neustart();
        }

        #region Navigation
        private void menuNavigation(object sender, EventArgs e)
        {
            ctlMain.MenuNavigation(sender, e);
        }
        #endregion

        /// <summary>
        /// Münze hinzufügen
        /// </summary>
        /// 
        private void mnuMünzeAdd_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuMünzeAdd.Text);
            Application.DoEvents();

            int index = ctlMain.Index;
            string guid = CoinbookHelper.MuenzkatalogFiltered[index].GUID;

            frmMünze form = new frmMünze();
            Sammlung coin = new Sammlung();

            coin.Erhaltung = -1;
            coin.Guid = guid;

            form.Nation = ctlMain.NationText;
            form.Gebiet = ctlMain.GebietText;
            form.Ära = ctlMain.ÄraText;
            form.Coin = coin;
            form.Edit = false;
            form.Index = index;
            form.Caller = "frmMain";

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                CoinbookHelper.InsertIntoMünzkatalog(form.Coin, form.Anzahl);
                ctlMain.Refresh();
            }

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void Form_ChangeBestand(object sender, CoinEventArgs args)
        {
            CoinbookHelper.InsertIntoMünzkatalog(args.Coin, args.Anzahl);

            ctlMain.Refresh();
        }

        /// <summary>
        /// Münzprägeanstalten anzeigen
        /// </summary>
        ///
        private void mnuPrägeanstalten_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuPrägeanstalten.Text);
            Application.DoEvents();

            frmPrägeanstalten form = new frmPrägeanstalten();
            form.NationID = ctlMain.NationID;
            form.Nation = ctlMain.NationText;
            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void tsbutSettings_Click(object sender, EventArgs e)
        {
            mnuSettings.PerformClick();
        }

        /// <summary>
        /// Eigene Katalognummern bearbeiten
        /// </summary>
        private void mnuEigeneKatalognummern_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuEigeneKatalognummern.Text);
            Application.DoEvents(); 

            var item = ctlMain.SelectedItem;

            frmKatalogNummer form = new frmKatalogNummer();

            form.ChangeKatalogNumber += Form_ChangeKatalogNumber;

            form.CoinbookKatNr = item.KatNr;
            form.NationID = ctlMain.NationID;
            form.CoinbookOriginal = item.OriginalKatNr;

            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
            form.ChangeKatalogNumber -= Form_ChangeKatalogNumber;
        }

        private void Form_ChangeKatalogNumber(object sender, KatalognummerEventArgs args)
        {
            ctlMain.ChangeKatalogNumber(args);
        }

        /// <summary>
        /// Keyevents abfangen
        /// </summary>
        /// 
        private void GetKeyState_Down(object sender, KeyEventArgs e)
        {
            //ctlMain.Funktionstasten(e.KeyData);
            ctlMain.MenuNavigation(this,e);
        }

        /// <summary>
        /// Modul bestellen
        /// </summary>
        /// 
        private void mnuOrder_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuOrder.Text);
            Application.DoEvents();

            MessageBoxNonmodal messageBox = new MessageBoxNonmodal("Coinbook ModulVerwaltung wird geladen", "Coinbook", 10);
            messageBox.Show(this, 2);

            string program = "Coinbook.Modulverwaltung.exe";
            CoinbookHelper.StartProgram(program, enmPrograms.ModulBestellung.ToString());

            messageBox.Close();

            Cursor = Cursors.Default;
            Enabled = true;
            Application.DoEvents();
        }

        /// <summary>
        /// About anzeigen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuAbout.Text);
            Application.DoEvents();

            frmAbout form = new frmAbout();
            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Reporting
        /// </summary>
        /// 
        private void mnuReportingSammlung_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuReportingSammlung.Text);
            Application.DoEvents();

            callStatistik(enmReportTyp.ReportSammlung);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuReportingDoubletten_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuReportingDoubletten.Text);
            Application.DoEvents();

            callStatistik(enmReportTyp.ReportDoubletten);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuReportingFehllisten_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuReportingFehllisten.Text);
            Application.DoEvents();

            callFehllisten(enmReportTyp.ReportFehllisten);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuWerteSammlung_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuWerteSammlung.Text);
            Application.DoEvents();

            callStatistikWert(enmReportTyp.WerteSammlung);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuWerteDoublette_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuWerteDoublette.Text);
            Application.DoEvents();

            callStatistikWert(enmReportTyp.WerteDoubletten);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuDBStatus_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuDBStatus.Text);
            Application.DoEvents();

            frmDBState form = new frmDBState();
            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            mnuPrint.Show(MousePosition.X, MousePosition.Y);
            Application.DoEvents();
        }

        private void mnuWert_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            mnuWert.Show(MousePosition.X, MousePosition.Y);
            Application.DoEvents();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            BackupModel model = new BackupModel();
            model.AutomaticBackup = false;
            model.Cloud = false;

            backup(model);
        }

        private void backup(BackupModel model)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), btnBackup.Text);
            Application.DoEvents();

            MessageBoxNonmodal messageBox = new MessageBoxNonmodal("Datensicherung wird geladen", "Coinbook", 10);
            messageBox.Show(this, 2);

            model.Program = Application.ProductName;
            model.DataPath = CoinbookHelper.DataPath;
            model.BackupPath = CoinbookHelper.BackupPath;
            model.TargetPath = CoinbookHelper.Settings.UpdatePath;
            model.DownloadPath = CoinbookHelper.DownloadPath;
            model.Language = CoinbookHelper.Settings.Culture;
            model.Files = new string[] { "Coinbook.db", "Coinbook-Sammlung.db" };
            model.License = CoinbookHelper.Settings.Lizenzkey;
            model.ABO = CoinbookHelper.Abo; //"CloudBackup|01.04.2022|30.04.2022";              

            //string file = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string file = Path.Combine(Path.GetTempPath(),"Backup.xml");

            SystemHelper.SerializeObject(file,model);

            CoinbookHelper.StartProgram("Coinbook.Backup.exe", file);

            model = SystemHelper.DeSerializeObject<BackupModel>(file);

            CoinbookHelper.Settings.UpdatePath = model.TargetPath;
            DatabaseHelper.LiteDatabase.SaveSettings(CoinbookHelper.Settings);

            File.Delete(file);

            messageBox.Close();

            Cursor = Cursors.Default;
            Enabled = true;
            Application.DoEvents();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (magnifierForm != null)
                magnifierForm.Save();

            ctlMain.ChangeRecordCount -= CtlMain_ChangeRecordCount;

            CoinbookHelper.Settings.Nation = ctlMain.NationID;
            CoinbookHelper.Settings.Ära = ctlMain.AeraID;
            CoinbookHelper.Settings.Gebiet = ctlMain.RegionID;

            CoinbookHelper.Settings.Maximized = (WindowState == FormWindowState.Maximized);
            CoinbookHelper.Settings.Height = Height;
            CoinbookHelper.Settings.Width = Width;
            CoinbookHelper.Settings.Top = Top;
            CoinbookHelper.Settings.Left = Left;

            CoinbookHelper.Settings.ColumnWidth = ctlMain.GetColumnWidth;

            DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);

            //Automatischer Modul-Update Prozess überwachen
            if (modulImport.IsRunning)
            {
                modulImport.CancelUpdate();
                frmMessage frm = new frmMessage();
                frm.Show(modulImport);
            }

            if (CoinbookHelper.Changes)
            {
                BackupModel model = new BackupModel();

                if (!string.IsNullOrEmpty(model.ABO))
                    model.Cloud = true;

                // Abfrage Backup machen
                if (CoinbookHelper.Settings.BackupByQuit)
                {
                    string text = LanguageHelper.Localization.GetTranslation("Keys", "msgDatensicherung");
                    if (MessageBoxAdv.Show(this, text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        model.AutomaticBackup = true;
                }

                if (model.Cloud || model.AutomaticBackup)
                {
                    Cursor = Cursors.WaitCursor;

                    lblStatusleiste.Text = LanguageHelper.Localization.GetTranslation("Keys", "msgBackup");
                    Application.DoEvents();

                    MessageBoxNonmodal messageBox = new MessageBoxNonmodal(LanguageHelper.Localization.GetTranslation("Keys", "msgLoadBackup"), "Coinbook", 10);
                    messageBox.Show(this);

                    backup(model);

                    messageBox.Close();

                    Cursor = Cursors.Default;
                    Application.DoEvents();
                }
            }

            DatabaseHelper.LiteDatabase.Flush("Sammlung");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), btnUpdate.Text);
            Application.DoEvents();

            updater.Update();

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuPicture_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuPicture.Text);
            Application.DoEvents();

            string guid = ctlMain.SelectedItem.GUID;
            int index = CoinbookHelper.MuenzkatalogFiltered.ToList().FindIndex(y => y.GUID == guid);
            EigeneBilder pic = DatabaseHelper.LiteDatabase.GetOwnPicture(guid);

            frmPicture form = new frmPicture();
            form.Anzeige = pic.ShowPicture;
            form.Picture = pic.DateiName;
            form.Guid = guid;

            form.ShowDialog(this);

            index = CoinbookHelper.Muenzkatalog1.ToList().FindIndex(y => y.GUID == guid);
            CoinbookHelper.Muenzkatalog1[index].OwnPicture = form.Picture;

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuPreise_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuPreise.Text);
            Application.DoEvents();

            var x = ctlMain.SelectedItem;
            int index = CoinbookHelper.MuenzkatalogFiltered.ToList().FindIndex(y => y.GUID == x.GUID);

            frmEigenePreise form = new frmEigenePreise();
            form.GUID = x.GUID;
            form.Nation = ctlMain.NationText;
            form.Ära = ctlMain.ÄraText;
            form.Gebiet = ctlMain.GebietText;
            form.Nennwert = x.Nominal;
            form.Währung = x.Waehrung;
            form.Münzzeichen = x.Muenzzeichen;
            form.Jahr = x.Jahrgang;

            form.PreisChanged += Form_PreisChanged;

            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();

            form.PreisChanged -= Form_PreisChanged;
        }

        private void btnKurse_Click(object sender, EventArgs e)
        {
            changeKurse();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            ctlMain.NextCoin();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            ctlMain.PreviousCoin();
        }

        /// <summary>
        /// Daten importieren
        /// </summary>
        private void mnuImport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Enabled = false;
            
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuImportCB2006.Text);
            Application.DoEvents();

            //MessageBoxNonmodal messageBox = new MessageBoxNonmodal("Coinbook Import wird geladen", "Coinbook", 10);
            //messageBox.Show(this, 2);
            
            frmImport form = new frmImport("DE");
            form.ShowDialog();
            //messageBox.Close();

            lblStatusleiste.Text = string.Empty;

            Cursor = Cursors.Default;
            Enabled = true;
            Application.DoEvents();
        }

        private void mnuReorg_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuReorg.Text);
            Application.DoEvents();

            var form = new frmRepairDB();
            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuKostenSammlung_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuKostenSammlung.Text);
            Application.DoEvents();

            callStatistikWert(enmReportTyp.KostenSammlung);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuKostenDoubletten_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuKostenDoubletten.Text);
            Application.DoEvents();

            callStatistikWert(enmReportTyp.KostenDoubletten);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        public DialogResult InfoUpdate(string value)
        {
            btnUpdate.Enabled = true;
            mnuUpdate.Enabled = true;

            btnUpdate.ToolTipText = value;
            lblStatusleiste.Text = value;

            lblStatusleiste.ForeColor = Color.Red;

            Sprachausgabe s = new Sprachausgabe();

            s.Volume = 100;
            s.PlayMP3("update-DE.mp3");

            DialogResult result = MessageBoxAdv.Show(value.Replace("{##}", Environment.NewLine), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                updater.Update();

            return result;
        }

        private void mnuModulUpdates_Click(object sender, EventArgs e)
        {
            modulUpdates();
        }

        private void modulUpdates()
        {
            Cursor = Cursors.WaitCursor;
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuModulUpdates.Text);
            Application.DoEvents();

            MessageBoxNonmodal messageBox = new MessageBoxNonmodal(LanguageHelper.Localization.GetTranslation("Keys", "msgLoadModulverwaltung"), "Coinbook", 10);
            var result = CoinbookHelper.StartProgram(Path.Combine(Application.StartupPath, "Coinbook.ModulVerwaltung.exe"), enmPrograms.ModulImport.ToString());

            messageBox.Close();

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
            Cursor = Cursors.Default;

            if (result == 0)
                CoinbookHelper.Neustart();
        }

        private void mnuKurse_Click(object sender, EventArgs e)
        {
            changeKurse();
        }

        private void callFehllisten(enmReportTyp typ)
        {
            Cursor = Cursors.WaitCursor;

            frmFehllisten form = new frmFehllisten();
            form.NationID = ctlMain.NationID;
            form.ÄraID = ctlMain.AeraID;
            form.GebietID = ctlMain.RegionID;
            form.ShowDialog(this);

            Cursor = Cursors.Default;
        }

        private void callStatistik(enmReportTyp typ)
        {
            Cursor = Cursors.WaitCursor;

            frmReporting form = new frmReporting();
            form.ReportTyp = typ;
            form.NationID = ctlMain.NationID;
            form.ÄraID = ctlMain.AeraID;
            form.GebietID = ctlMain.RegionID;
            form.ShowDialog(this);

            Cursor = Cursors.Default;
        }

        private void callTest(enmReportTyp typ)
        {
            //Application.DoEvents();
            //Cursor = Cursors.WaitCursor;

            //frmTest form = new frmTest();
            //form.Liste = typ;
            //form.NationID = ctlMain.NationID;
            //form.ShowDialog(this);

            //Application.DoEvents();

            //Cursor = Cursors.Default;
        }

        private void callStatistikWert(enmReportTyp typ)
        {
            Cursor = Cursors.WaitCursor;

            frmReportingWert form = new frmReportingWert();
            form.Liste = typ;
            form.NationID = ctlMain.NationID;
            form.ShowDialog(this);

            Cursor = Cursors.Default;
        }

        private void mnuImport2x_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Enabled = false;

            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuImport2x.Text);
            Application.DoEvents();

            //MessageBoxNonmodal messageBox = new MessageBoxNonmodal("Coinbook Import wird geladen", "Coinbook", 10);
            //messageBox.Show(this, 2);

            frmImport2 form = new frmImport2("DE");
            form.ShowDialog();
            //messageBox.Close();

            Cursor = Cursors.Default;
            Enabled = true;
            Application.DoEvents();
        }

        private void mnuImport2XML_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Enabled = false;

            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuImport2XML.Text);
            Application.DoEvents();

            //MessageBoxNonmodal messageBox = new MessageBoxNonmodal("Coinbook Import wird geladen", "Coinbook", 10);
            //messageBox.Show(this, 2);

            frmImport2XML form = new frmImport2XML("DE");
            form.ShowDialog();

            var form1 = new frmRepairDB();
            form1.ShowDialog(this);

            MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "msgReady"));
            //messageBox.Close();5

            Cursor = Cursors.Default;
            Enabled = true;
            Application.DoEvents();
        }


        private void mnuAktivieren_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuAktivieren.Text);
            Application.DoEvents();

            CoinbookHelper.StartProgram("Coinbook.Activation.exe", enmPrograms.Aktivierung.ToString());

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            enableUpdate();

            //int id = (int)cboNationen.SelectedValue;
            //cboÄra.DataSource = Helper.Aeras(id);

            //if (DatabaseHelper.LiteDatabase.Count("tblAera") == 0)
            //{
            //    mnuModulUpdates_Click(null, null);
            //    CoinbookHelper.Neustart();
            //}
            //else
            //{
            //    if (CoinbookHelper.Settings.ModulAutoUpdate)
            //    {
            //        if (modulImport.Import()) //TODO
            //        {
            //            mnuModulUpdates.Enabled = false;
            //            mnuUpdate.Enabled = false;
            //        }
            //        else
            //            lblStatusleiste.Text = LanguageHelper.Localization.GetTranslation("Keys", "noConnection");
            //    }

            //    ctlMain.Setxxxx();
            //}
        }

        void Localization_ChangeLangauge(object sender, EventArgs e)
        {
            ctlMain.loadNationen();
            ctlMain.LoadÄra();
            //ctlMain.loadGebiete();
        }

        private void mnuHandbuchPDF_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuHandbuchPDF.Text);
            Application.DoEvents();
            
            frmPDF form = new frmPDF();
            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuUpdateVonCD_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuUpdateVonCD.Text);
            Application.DoEvents();

            frmCDUpdate form = new frmCDUpdate();
            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuLupeSettings_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuLupeSettings.Text);
            Application.DoEvents();

            if (magnifierForm != null)
            {
                magnifierForm.Close();
                magnifierForm.Hide();
            }

            MagnifierHelper.ConfigFileName = Path.Combine(CoinbookHelper.DataPath, "Magnifier.xml");
            MagnifierHelper.LoadConfiguration();
            frmMagnifierConfiguration form = new frmMagnifierConfiguration();
            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuLupe_Click(object sender, EventArgs e)
        {
            //Application.DoEvents(); 

            mnuLupe.Checked = !mnuLupe.Checked;
            btnMagnifier.Checked = mnuLupe.Checked;

            if (mnuLupe.Checked)
            {
                MagnifierHelper.ConfigFileName = Path.Combine(CoinbookHelper.DataPath, "Magnifier.xml");
                MagnifierHelper.LoadConfiguration();
                if (magnifierForm == null)
                    magnifierForm = new frmMagnifier();
                magnifier = new Magnifier(magnifierForm);
                magnifierForm.Show();
            }
            else
            {
                magnifier = null;
                if (magnifierForm != null)
                {
                    magnifierForm.Hide();
                    magnifierForm.Close();
                    magnifierForm = null;
                }
            }

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuTeamviewer_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuTeamviewer.Text);
            Application.DoEvents();

            string file = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "TeamViewerQS.exe");
            if (File.Exists(file))
                Process.Start(file);
            else
                MessageBoxAdv.Show("Teamviewer wurde nicht gefunden", Application.ProductName);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        #region Web
        /// <summary>
        /// News Coinbook laden
        /// </summary>
        /// 
        private void mnuNews_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuNews.Text);
            Application.DoEvents(); 

            string url;

            if (LanguageHelper.Localization.Language == "de")
                url = @"http://www.coinbook.de/neuigkeiten.html";
            else
                url = @"http://www.coinbook.de/en/news.html";

            Process.Start(url);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        /// <summary>
        /// Onlinehilfe laden
        /// </summary>
        /// 
        private void mnuWebOrder_Click(object sender, EventArgs e)
        {
            string url;

            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuWebOrder.Text);
            Application.DoEvents();

            if (LanguageHelper.Localization.Language == "de")
                url = @"http://www.coinbook.de/module-lizenzen.html";
            else
                url = @"http://www.coinbook.de/en/module-licenses.html";

            Process.Start(url);

            //lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        /// <summary>
        /// faq anzeigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFAQ_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuFAQ.Text);
            Application.DoEvents();

            string url;

            if (LanguageHelper.Localization.Language == "de")
                url = @"http://www.coinbook.de/faq.html";
            else
                url = @"http://www.coinbook.de/en/faq.html";

            Process.Start(url);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        /// <summary>
        /// supportseite aufrufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSupport_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuSupport.Text);
            Application.DoEvents();

            string url;

            if (LanguageHelper.Localization.Language == "de")
                url = @"http://www.coinbook.de/kontakt-support.html";
            else
                url = @"http://www.coinbook.de/en/contact-support.html";

            Process.Start(url);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        /// <summary>
        /// gästebuch aufrufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuGästebuch_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuGästebuch.Text);
            Application.DoEvents();

            string url;

            if (LanguageHelper.Localization.Language == "de")
                url = @"http://www.coinbook.de/gaestebuch.php";
            else
                url = @"http://www.coinbook.de/en/guestbook.php";

            Process.Start(url);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }
        #endregion

        public void EnableUpdate(bool enable, string value)
        {
            updateEnable = enable;
            updateValue = value;
        }

        private void enableUpdate()
        {
            btnUpdate.Enabled = updateEnable;
            mnuUpdate.Enabled = updateEnable;

            if (updateEnable)
            {
                btnUpdate.ToolTipText = updateValue;
                lblStatusleiste.Text = updateValue;

                lblStatusleiste.ForeColor = Color.Red;
            }
            else
            {
                btnUpdate.ToolTipText = string.Empty;
                lblStatusleiste.Text = string.Empty;

                lblStatusleiste.ForeColor = CoinbookHelper.ColorText;
            }
        }

        #region automatischer Modulimport
        private void ModulImport_ModulProcess(object sender, ProgressChangedEventArgs e)
        {
            pgbModul.Visible = true;
            pgbModul.Value = e.ProgressPercentage;
            pgbModul.CustomText = string.Format("{0} - {1:###}%", e.UserState, e.ProgressPercentage);
        }

        private void ModulImport_TableProcess(object sender, ProgressChangedEventArgs e)
        {
            pgbDetails.Visible = true;
            pgbDetails.Value = e.ProgressPercentage;
            pgbDetails.CustomText = string.Format("{0} - {1:###}%", e.UserState, e.ProgressPercentage);
        }

        private void ModulImport_ModulReady(object sender, ProgressChangedEventArgs e)
        {
            splashPanel1.Size = new Size(300, 250);
            Point location = new Point();
            location.Y = this.Height - splashPanel1.Height - 20;
            location.X = this.Width - splashPanel1.Width - 30;

            location.Y = 100;
            location.X = 100;

            splashPanel1.ShowSplash(location, this, false);

            if (labelEx1.Text.Length == 0)
                labelEx1.Text = e.UserState.ToString();
            else
                labelEx1.Text = labelEx1.Text + Environment.NewLine + e.UserState.ToString();
        }

        private void ModulImport_DownloadReady(object sender, ProgressChangedEventArgs e)
        {
            if (pgbModul.Visible)
            {
                splashPanel1.Size = new Size(300, 250);
                splashPanel1.ShowSplash();

                if (labelEx1.Text.Length == 0)
                    labelEx1.Text = e.UserState.ToString();
                else
                    labelEx1.Text = e.UserState.ToString();

                pgbDetails.Visible = false;
                pgbDetails.Value = 0;
                pgbDetails.CustomText = string.Empty;

                pgbModul.Visible = false;
                pgbModul.Value = 0;
                pgbModul.CustomText = string.Empty;

                mnuModulUpdates.Enabled = true;
                mnuUpdate.Enabled = true;
            }
        }
        #endregion automatischer Modulimport

        private void mnuCreateDatabase_Click(object sender, EventArgs e)
        {
            Application.DoEvents();

            //Coinbook.CreateDB.frmCreateDatabase frmCreate = new Coinbook.CreateDB.frmCreateDatabase();
            //frmCreate.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuSwitchDatabase_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuSwitchDatabase.Text);
            Application.DoEvents();

            //Coinbook.CreateDB.frmSwitchDatabase frmSwitch = new CreateDB.frmSwitchDatabase();
            //frmSwitch.ShowDialog(this);

            //if (frmSwitch.DialogResult == DialogResult.OK)
            //{
            //    //Neustart
            //}

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuReporting2Doubletten_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuReporting2Doubletten.Text);
            Application.DoEvents();

            callReporting2(enmReportTyp.ReportDoubletten);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuReporting2Sammlung_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuReporting2Sammlung.Text);
            Application.DoEvents();

            callReporting2(enmReportTyp.ReportSammlung);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void callReporting2(enmReportTyp typ)
        {
            Application.DoEvents();
            Cursor = Cursors.WaitCursor;

            frmReporting2 form = new frmReporting2();
            form.ReportTyp = typ;
            form.NationID = ctlMain.NationID;
            form.ÄraID = ctlMain.AeraID;
            form.GebietID = ctlMain.RegionID;
            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
            Cursor = Cursors.Default;
        }

        private void loadPlugins()
        {
            mnuExtras.Enabled = false;
            plugins = new PluginServices(Path.Combine(Application.StartupPath, "Plugins"));
            //plugins = new PluginServices(Application.StartupPath);

            //Make sure there's a selected Plugin
            foreach (AvailablePlugin selectedPlugin in plugins.AvailablePlugins)
            {
                selectedPlugin.Instance.Host = this;
                selectedPlugin.Instance.BackupPath = CoinbookHelper.BackupPath;
                selectedPlugin.Instance.UpdatePath = CoinbookHelper.UpdatePath;
                selectedPlugin.Instance.Programm = Application.ProductName;
                selectedPlugin.Instance.Lizenz = CoinbookHelper.Settings.Lizenzkey;
                selectedPlugin.Instance.DataPath = CoinbookHelper.DataPath;

                selectedPlugin.Instance.Initialize("CloudBackup|01.01.2021|31.12.2021");
                //selectedPlugin.Instance.Initialize("CloudBackup|01.01.2020|31.12.2020");

               
                if (selectedPlugin.Instance.HasMenu)
                {
                    mnuExtras.Enabled = true;
                    mnuExtras.DropDownItems.Add(selectedPlugin.Instance.MenuEntry);
                }
            }
        }
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            ctlMain.ResizeMotivColumn();
        }

        private void mnuColumnWidth_Click(object sender, EventArgs e)
        {
            Application.DoEvents();

            if (DatabaseHelper.LiteDatabase.Count("tblAera", CoinbookHelper.ModulKey) != 0)
            {
                ctlMain.SetGridColumnWidth();
                ctlMain.ResizeMotivColumn();
                ctlMain.SaveColumnWidth();
            }

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuDeleteCoin_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuDeleteCoin.Text);
            Application.DoEvents();

            string guid = ctlMain.SelectedItem.GUID;
            int index = CoinbookHelper.MuenzkatalogFiltered.ToList().FindIndex(y => y.GUID == guid);

            frmMünzeDelete form = new frmMünzeDelete();
            form.ChangeBestand += Form_ChangeBestand;

            form.GUID = guid;
            form.Nation = ctlMain.NationText;
            form.Gebiet = ctlMain.GebietText;
            form.Ära = ctlMain.ÄraText;
            form.Caller = "frmMain";
            form.Index = index;

            form.ShowDialog(this);
            form.ChangeBestand -= Form_ChangeBestand;

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuAuktionen_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuAuktionen.Text);
            Application.DoEvents();

            var item = ctlMain.SelectedItem;

            frmAuktion form = new frmAuktion();

            form.GUID = item.GUID;
            form.Nation = ctlMain.NationText;
            form.Ära = ctlMain.ÄraText;
            form.Gebiet = ctlMain.GebietText;
            form.Nennwert = item.Nominal;
            form.Währung = item.Waehrung;
            form.Münzzeichen = item.Muenzzeichen;
            form.Jahr = item.Jahrgang;

            form.ShowDialog(this);

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void Form_PreisChanged(object sender, PreisEventArgs args)
        {
            CoinbookHelper.CalculateOwnPrices(ctlMain.Index, args);
            ctlMain.Refresh();
        }

        private void createLanguageMenu()
        {
            var sprachmenue = LanguageHelper.Localization.Languages;

            foreach (var item in sprachmenue)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Text = item.Sprache;
                menuItem.Image = item.Flagge;
                menuItem.Tag = item.Key;
                menuItem.Enabled = true;

                if (CoinbookHelper.Settings.Culture.Substring(0, 2) == menuItem.Tag.ToString())
                {
                    menuItem.Enabled = false;
                    menuItem.BackColor = Color.GreenYellow;
                }

                mnuLanguage.DropDownItems.Add(menuItem);

                menuItem.Click += MenuItem_Click;
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in mnuLanguage.DropDownItems)
            {
                item.Enabled = true;
                item.BackColor = Color.Silver;
            }

            ToolStripMenuItem temp = (ToolStripMenuItem)sender;
            temp.Enabled = false;
            temp.BackColor = Color.GreenYellow;

            Application.DoEvents();

            switch (temp.Tag.ToString())
            {
                case "de":
                    CoinbookHelper.Settings.Culture = "de-DE";
                    break;

                case "en":
                    CoinbookHelper.Settings.Culture = "en-US";
                    break;
            }

            DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);

            Language changeLanguage = new Language();
            changeLanguage.StartChangeLanguage(temp.Tag.ToString());

            var sprachmenue = LanguageHelper.Localization.Languages;

            for (int i = 0; i < sprachmenue.Count; i++)
                mnuLanguage.DropDownItems[i].Text = sprachmenue[i].Sprache;

            Application.DoEvents();
            Form_LanguageChanged(null, null);
        }

        private void ctlMain_OverviewLoaded(object sender, EventArgs e)
        {
            int count = (int)sender;

            lblRecords.Text = count.ToString();
            mnuMünzdetails.Enabled = (count > 0);
            mnuMünzeAdd.Enabled = (count > 0);
            mnuDeleteCoin.Enabled = (count > 0);
            mnuEigeneKatalognummern.Enabled = (count > 0);
            mnuPicture.Enabled = (count > 0);
            mnuPreise.Enabled = (count > 0);
            mnuAuktionen.Enabled = true;
        }

        private void EnableEditMenues(object sender, EventArgs e)
        {
            bool enabled = (bool)sender;
            mnuMünzdetails.Enabled = enabled;
            mnuMünzeAdd.Enabled = enabled;
            mnuDeleteCoin.Enabled = enabled;
            mnuEigeneKatalognummern.Enabled = enabled;
            mnuPicture.Enabled = enabled;
            mnuPreise.Enabled = enabled;
            mnuAuktionen.Enabled = enabled;
        }

        private void changeKurse()
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuKurse.Text);
            frmKurse form = new frmKurse();

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                ctlMain.CreateStackHeader();
                if (ctlMain.RowCount > 0)
                    CoinbookHelper.Neustart();
            }

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuLogSettings_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuLogSettings.Text);
            SAN.Logging.frmLogSettings form = new SAN.Logging.frmLogSettings();

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                ctlMain.CreateStackHeader();
                if (ctlMain.RowCount > 0)
                    CoinbookHelper.Neustart();
            }

            lblStatusleiste.Text = string.Empty;
            Application.DoEvents();
        }

        private void mnuInportCoinbook3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Enabled = false;

            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuImportCoinbook3.Text);
            Application.DoEvents();

            MessageBoxNonmodal messageBox = new MessageBoxNonmodal("Coinbook Import wird geladen", "Coinbook", 10);
            messageBox.Show(this, 2);

            frmImportCoinbook30 form = new frmImportCoinbook30(true);
            form.ShowDialog(this);
            messageBox.Close();

            Cursor = Cursors.Default;
            Enabled = true;
            Application.DoEvents();
        }

        private void mnuCloudBackupBestellen_Click(object sender, EventArgs e)
        {
            lblStatusleiste.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "msgStart"), mnuCloudBackupBestellen.Text);
            Application.DoEvents();

            CoinbookHelper.Abo = "";

            if (string.IsNullOrEmpty(CoinbookHelper.Abo))
            {
                MessageBoxNonmodal messageBox = new MessageBoxNonmodal("Coinbook ModulVerwaltung wird geladen", "Coinbook", 10);
                messageBox.Show(this,2);

                CoinbookHelper.StartProgram("Coinbook.Modulverwaltung.exe", enmPrograms.AboBestellung.ToString());

                //frmOrderCloudBackup form = new frmOrderCloudBackup(CoinbookHelper.Settings);
                //form.ShowDialog(this);

                messageBox.Close();
            }
            else
            {
                string text = "Sie haben schon das Abonnement für CloubBackup. Daher wird ist erneute Bestellung nicht möglich";
                string caption = "CloudBackup bestellen";
                MessageBoxAdv.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Cursor = Cursors.Default;
            Enabled = true;
            Application.DoEvents();
        }

        private void MessageBox_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void mnuDatabaseTransfer_Click(object sender, EventArgs e)
        {
            frmDataTransfer form = new frmDataTransfer();
            form.ShowDialog(this);
        }

        private void mnuQuickEditSammlung_Click(object sender, EventArgs e)
        {
            frmQuickInput form = new frmQuickInput(ctlMain.NationID,ctlMain.AeraID,ctlMain.RegionID, true, ctlMain.Currency, ctlMain.Nominal, ctlMain.Jahrgang);
            form.ChangeBestand += Form_ChangeBestand;
            form.ShowDialog(this);
            form.ChangeBestand -= Form_ChangeBestand;
        }

        private void mnuQuickEditDubletten_Click(object sender, EventArgs e)
        {
            frmQuickInput form = new frmQuickInput(ctlMain.NationID, ctlMain.AeraID, ctlMain.RegionID, false, ctlMain.Currency, ctlMain.Nominal, ctlMain.Jahrgang);
            form.ChangeBestand += Form_ChangeBestand;
            form.ShowDialog(this);
            form.ChangeBestand -= Form_ChangeBestand;
        }
    }

    [Serializable()]
    public class BackupModel
    {
        public string Program { get; set; }
        public string DataPath { get; set; }
        public string BackupPath { get; set; }
        public string TargetPath { get; set; }
        public string Language { get; set; }
        public bool AutomaticBackup { get; set; }
        public string[] Files { get; set; }
        public string License { get; set; }
        public bool Cloud { get; set; }
        public string ABO { get; set; }
        public string DownloadPath { get; set; }
    }
}