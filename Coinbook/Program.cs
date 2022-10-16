//Historie:
// Bei Anlicken des Münzbildes in der Übersichtsmaske öffnet sich ein Fenster mit dem vergrößerten Bildes

//using Coinbook.CreateDB;
using AutoUpdater;
using Coinbook.Helper;
using Coinbook.Model;
using SAN.FTP;
using SAN.Logging;
using SAN.UIControls;
using Splash;
using Syncfusion.Windows.Forms;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Coinbook
{ 
    static class Program
    {
        internal static bool bDataBaseLoked = false;
        //private static frmMain frmmain;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDI3NzIyQDMxMzkyZTMxMmUzMGlEMzJRR0laMzhSWUZnendjb3d5L3dGdk9iT2ZZMDkzT2c5VG5hK3JTUDQ9;NDI3NzIzQDMxMzkyZTMxMmUzMFl4YWMzZlJnTm10b0ZIV0sxaXVOQi90c3BZT3MwaEw4RDF5eTBJM2F1R0k9;NDI3NzI0QDMxMzkyZTMxMmUzMGJpYThWUmR1NnlRTWVIK3AxclZJeWw2OWtMYXRSYys0cm5SZll0Nkc3cDQ9;NDI3NzI1QDMxMzkyZTMxMmUzMEZrSjJ0d0c0UmZUUTN3c1kwSk56RUxnaytSSVRWeFB2c3B5SXh4M2d1bmM9;NDI3NzI2QDMxMzkyZTMxMmUzMEdQaTVHRnVjYnRFM2Q4Y1pxT3Q0ODFHNVR4SCtCeU9nZ2hURUw0OThUNGs9;NDI3NzI3QDMxMzkyZTMxMmUzMEppYlVLK3FqWlpoVTBNZHlxK3RmMUc4MnZHak5IS3AyS3V3OXZZWjFkaHM9;NDI3NzI4QDMxMzkyZTMxMmUzMFRNQ0U2T0M4OGZLUmQyNkg1S2tSc3ZhemlMNTYzOVkyTU9QTGUycTFWUUU9;NDI3NzI5QDMxMzkyZTMxMmUzMEs4ZXlENWdoNm5nQzFlTGlQLytoNGFZS2l6QVc1UWtLMXJndmVlbjdVbGs9;NDI3NzMwQDMxMzkyZTMxMmUzMFB1MEtJNHQzRnFBSWVCbm9tbWlCWkNQWlhKMkJTaGd1ZXVyb045bzNBT3c9;NDI3NzMxQDMxMzkyZTMxMmUzMEVHR04yZXdBWFB4RXJhMTNKMnNyNDl3dm5yQkhnekNVQVE0WS80Y25xZFE9");

            SfSkinManager.LoadAssembly(typeof(Syncfusion.WinForms.Themes.Office2016Theme).Assembly);
            SfSkinManager.LoadAssembly(typeof(Syncfusion.WinForms.Themes.Office2019Theme).Assembly);
            SfSkinManager.LoadAssembly(typeof(Syncfusion.HighContrastTheme.WinForms.HighContrastTheme).Assembly);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LogHelper.LogSettings.All = true;

            LocalizationProvider.Provider = new SFLocalizer();

            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Default;
            MessageBoxAdv.CaptionAlign = HorizontalAlignment.Center;
            MessageBoxAdv.DropShadow = true;

            //LoggingServices.DefaultBackend = new PostSharp.Patterns.Diagnostics.Backends.Console.ConsoleLoggingBackend();

            //// Configure Log4Net
            //XmlConfigurator.Configure(new FileInfo("log4net.config"));

            //// Configure PostSharp Logging to use Log4Net
            //LoggingServices.DefaultBackend = new Log4NetLoggingBackend();

            SplashScreen.ShowSplashScreen();

            CoinbookHelper.Coin = new Icon(Path.Combine(Path.Combine(Application.StartupPath, "Images"), "Coin.ico"));
            CoinbookHelper.Hinweis = new Icon(Path.Combine(Path.Combine(Application.StartupPath, "Images"), "Hinweis.ico"));
            CoinbookHelper.Lupe = new Icon(Path.Combine(Path.Combine(Application.StartupPath, "Images"), "Lupe.ico"));
            CoinbookHelper.CoinbookIcon = new Icon(Path.Combine(Application.StartupPath, "Icon.ico"));

            CoinbookHelper.Settings = DatabaseHelper.LiteDatabase.ReadSettings();
            string sprache = CoinbookHelper.Settings.Culture.Substring(0, 2);

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook");

            LanguageHelper.CreateLocalization(resourcePath);
            LanguageHelper.Localization.UpdateLanguage(sprache);

            CoinbookHelper.DataPath = DatabaseHelper.LiteDatabase.DataPath;

            if (File.Exists(Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Coinbook-log.db")))
                File.Delete(Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Coinbook-log.db"));

            if (CoinbookHelper.Settings.Programmversion != Application.ProductVersion)
            {
                CoinbookHelper.Settings.Programmversion = Application.ProductVersion;
                using (Feedback feedback = new Feedback())
                    feedback.Send();
            }

            AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += new UnhandledExceptionEventHandler(Application_ThreadException);
            var x1 = SAN.Logging.LogHelper.LogSettings;

            if (!File.Exists(Path.Combine(CoinbookHelper.DataPath, "modul.lic")))
            {
                SplashScreen.CloseForm();

                setSplashStatus("msgLizenzExists");
                frmLizenz form = new frmLizenz();
                form.ShowDialog();
            }

            if (!string.IsNullOrEmpty(CoinbookHelper.Settings.Passwort))
            {
                frmPasswort frm = new frmPasswort();
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK)
                    return;
            }

            downloadFiles();

            Lizenz lizenz = new Lizenz();

            setSplashStatus("msgDatenbank");
            string text = setSplashStatus("msgLizenz");
            SplashScreen.Lizenz = text + Environment.NewLine + CoinbookHelper.Settings.Vorname + " " + CoinbookHelper.Settings.Nachname;

            Application.DoEvents();

            //MessageBox.Show("1");
            setSplashStatus("msgDatenbank");

            if (!Directory.Exists(CoinbookHelper.DataPath))
                CoinbookHelper.DataPath = Application.StartupPath.TrimEnd(@"\".ToCharArray()) + @"\";

            CoinbookHelper.EigeneBilderListe = new List<EigeneBilder>();

            List<System.Diagnostics.Process> listProcesses = new List<System.Diagnostics.Process>();
            listProcesses.AddRange(System.Diagnostics.Process.GetProcesses());
            listProcesses.ForEach(
                    o =>
                    {
                        if (o.ProcessName.CompareTo(System.Diagnostics.Process.GetCurrentProcess().ProcessName) == 0)
                        {
                            Application.ExitThread();
                        }
                    }
            );

            //MessageBox.Show("2");

            setSplashStatus("msgLizenzCheck");
            lizenz.ReadModulLizenzen();

            if (!lizenz.Restart)
            {
                checkForPluginDownload();

                CoinbookHelper.Nationen = DatabaseHelper.LiteDatabase.ReadNationen();
                CoinbookHelper.Erhaltungsgrade = DatabaseHelper.LiteDatabase.ReadErhaltungsgrade(CoinbookHelper.Settings.International);

                //MessageBox.Show("3");

                AutoUpdate updater = new AutoUpdate();
                updater.DataPath = CoinbookHelper.DataPath;
                updater.Company = Application.CompanyName;
                updater.Title = Application.ProductName;
                updater.Version = new Version(Application.ProductVersion);
                updater.UpdateConfigFile = Path.Combine(CoinbookHelper.InfoPath, "update.cfg");

                //setSplashStatus("msgInitialize");
                //frmmain = new Coinbook.frmMain(lizenz.Activated, updater);

                //Prüfe auf Update
                setSplashStatus("msgUpdate");
                //SplashScreen.FormVisible(false);
                DialogResult resultUpdate = updater.CheckForUpdate(LanguageHelper.Localization.GetTranslation("Keys", "msgInstallVersion"));
                //SplashScreen.FormVisible(true);

                //prüfe auf plugins
                setSplashStatus("msgUpdate");
                PluginHelper.CheckPlugins(CoinbookHelper.Abo);
                SplashScreen.CloseForm();

                //MessageBox.Show("4");

                setSplashStatus("msgInitialize");

                var katalogPath = Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Katalog");
                if (!Directory.Exists(katalogPath))
                {
                    using (Coinbook.Import.Class1 class1 = new Coinbook.Import.Class1())
                        class1.xxx(katalogPath);
                }

                CoinbookHelper.Nationen = DatabaseHelper.LiteDatabase.ReadNationen();
                CoinbookHelper.Muenzkatalog1 = new BindingList<Katalog3>();

                var frmmain = new Coinbook.frmMain(lizenz.Activated, updater);

                switch (resultUpdate)
                {
                    case DialogResult.Yes:
                        updater.Update();
                        frmmain.EnableUpdate(true, String.Empty);
                        break;

                    case DialogResult.No:
                        frmmain.EnableUpdate(true, updater.Text);
                        break;
                }

                //MessageBox.Show("5");

                SplashScreen.CloseForm();
                Application.Run(frmmain);
            }
            //}
            //else
            //    MessageBoxAdv.Show(setSplashStatus("msgQuit"),"Coinbbok");              TODO
        }

        private static string setSplashStatus(string key)
        {
            string text = LanguageHelper.Localization.GetTranslation("SplashScreen", key);
            SplashScreen.SetStatus(text, false);

            return text;
        }

        private static string setSplashStatus(string key, string parameter, string defaultText = "")
        {
            string text = string.Format(LanguageHelper.Localization.GetTranslation("SplashScreen", key),parameter, defaultText);
            SplashScreen.SetStatus(text, false);

            return text;
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //frmException frm = new frmException(e);
            //frm.ShowDialog();
        }

        private static void downloadFiles()
        {
            string file;
            string target;
            string source;
            DateTime modifydate = Convert.ToDateTime("01.01.1900");
            enmFTPFile result;

            FTPClass ftp = new FTPClass();
            if (ftp.Connect(ftp.FTPParameter.URL, ftp.FTPParameter.Admin, ftp.FTPParameter.Passwort, false))
            {
                //Lizenzdatei
                target = Path.Combine(CoinbookHelper.DataPath, "Modul.xxx");
                source = Path.Combine(CoinbookHelper.DataPath, "Modul.lic");

                if (File.Exists(source))
                    modifydate = File.GetCreationTime(source);

                result = ftp.Download("Downloads/Personalisierung/" + CoinbookHelper.Settings.Lizenzkey, target, modifydate);

                if (result == enmFTPFile.FileDownloadOK)
                {
                    if (File.Exists(source))
                        File.Delete(source);
                    File.Copy(target, source);
                    File.Delete(target);
                }
                else
                    File.Delete(target);

                //Falls Lizenz aktiviert wurde dann anzeigen und Aktivierung auf ok setzen
                if (CoinbookHelper.Settings.Activated == "angefordert" && result == enmFTPFile.FileDownloadOK)
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(source);
                    XmlNode license = document.LastChild;

                    if (license.Attributes["Serial"].Value.ToString() != "x")
                    {
                        frmAktiviert form = new frmAktiviert();
                        form.ShowDialog();

                        CoinbookHelper.Settings.Activated = "OK";
                        DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);
                    }
                }

                //ProgrammUpdate 
                file = "update.cfg";
                target = Path.Combine(CoinbookHelper.InfoPath, file);
                modifydate = Convert.ToDateTime("01.01.1900");
                if (File.Exists(target))
                    modifydate = File.GetCreationTime(target);
                result = ftp.Download("Downloads/Coinbook/" + file, target, modifydate);

                //Releasenotes fürs Update
                file = "Releasenotes.rtf";
                target = Path.Combine(CoinbookHelper.InfoPath, file);
                modifydate = Convert.ToDateTime("01.01.1900");
                if (File.Exists(target))
                    modifydate = File.GetCreationTime(target);
                result = ftp.Download("Downloads/Coinbook/" + file, target, modifydate);

                ////Nation.zip kopieren
                //file = "Nation.zip";
                //target = Path.Combine(CoinbookHelper.InfoPath, file);
                //modifydate = Convert.ToDateTime("01.01.1900");
                //if (File.Exists(target))
                //    modifydate = File.GetCreationTime(target);
                //result = ftp.Download("Downloads/Module2022/" + file, target, modifydate);

                //if (result == enmFTPFile.FileDownloadOK)
                //{
                //    ArchivHelper.DecompressFile(target, CoinbookHelper.DownloadPath,"", "magixx-1-xxigam");
                //    CoinbookHelper.ImportNation(Path.Combine(CoinbookHelper.DownloadPath, "tblNation.xml"));
                //}

                //Allgemein.zip kopieren
                file = "Allgemein.zip";
                target = Path.Combine(CoinbookHelper.InfoPath, file);
                modifydate = Convert.ToDateTime("01.01.1900");
                if (File.Exists(target))
                    modifydate = File.GetCreationTime(target);
                result = ftp.Download("Downloads/Module2022/" + file, target, modifydate);

                if (result == enmFTPFile.FileDownloadOK)
                {
                    ArchivHelper.DecompressFile(target, CoinbookHelper.DownloadPath, "", "magixx-1-xxigam");
                    DatabaseHelper.LiteDatabase.ImportAllgemein(CoinbookHelper.DownloadPath);
                    File.Delete(Path.Combine(CoinbookHelper.DownloadPath, "Allgemein.db"));
                }

                ftp.Disconnect();
            }
        }

        private static void checkForPluginDownload()
        {
            enmFTPFile result = enmFTPFile.FileDownloadOK;

            if (!string.IsNullOrEmpty(CoinbookHelper.Abo))
            {
                using (FTPClass ftp = new FTPClass())
                {
                    if (ftp.Connect(ftp.FTPParameter.URL, ftp.FTPParameter.Admin, ftp.FTPParameter.Passwort, false))
                    {
                        DateTime modifydate = Convert.ToDateTime("01.01.1900");
                        var abo = CoinbookHelper.Abo.Split('|');

                        var file = abo[0] + ".dll";
                        var target = Path.Combine(CoinbookHelper.PluginPath, file);
                        var pluginExists = File.Exists(target);

                        if (!pluginExists)
                        {
                            setSplashStatus("InstallierePlugin",abo[0], "Installiere Plugin {0}");

                            result = ftp.Download("Downloads/Plugins/" + file, target);
                        }
                        else
                        {
                            setSplashStatus("PrüfePluginAufUpdates", abo[0], "Prüfe Plugin {0} auf Updates");

                            modifydate = Convert.ToDateTime("01.01.1900");
                            if (File.Exists(target))
                                modifydate = File.GetCreationTime(target);
                            result = ftp.Download("Downloads/Plugins/" + file, target, modifydate);
                        }

                        target = Path.Combine(CoinbookHelper.InfoPath, file);
                    }
                }
            }
        }
    }

    public static class ModuleInitializer
    {
        public static void Initialize()
        {
            //onsole.WriteLine(typeof(SomeTypeInsideTheAssembly).FullName);
            //code goes here
        }
    }

}


