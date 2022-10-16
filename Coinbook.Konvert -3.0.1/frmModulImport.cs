
using Coinbook.Helper;
using Coinbook.Model;
using SAN.Converter;
using SAN.FileDownloader;
using SAN.FTP;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Coinbook.Modulverwaltung;

namespace Coinbook.Konvert
{
    public partial class frmModulImport : Form
    {
        private frmDownLoader formDownload = new frmDownLoader();
        private frmDownLoader formDownload1 = new frmDownLoader();
        private int max;
        private int count;
        private string downloadPath = String.Empty;
        private string updatePath = string.Empty;
        private string picturePath = string.Empty;
        private int notloaded = 0;
        bool found = false;
        BindingList<ListViewModel> auswahlListe = new BindingList<ListViewModel>();

        FTP ftpClient = new FTP();
        private FTPClass ftpClass = new FTPClass();

        Queue<string> downloadFiles = new Queue<string>();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        //Lizenz lizenz = new Lizenz();
        List<Downloads> lizenzierteModule = new List<Downloads>();

        public frmModulImport()
        {
            InitializeComponent();
            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook");
            LanguageHelper.CreateLocalization(resourcePath);
            LanguageHelper.Localization.UpdateLanguage("de");
            LanguageHelper.Localization.UpdateModul(this);

            downloadPath = Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Downloads");
            updatePath = Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Updater");
            picturePath = Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Bilder");

            lblGesamt.Text = LanguageHelper.Localization.GetTranslation("frmReporting", "Gesamt");

            label1.Text = "Neue Module werden automatisch so gekennzeichnet, daß sie nicht nochmal heruntergeladen werden. "
                + " Es werden also nur die Module heruntergeladen, die noch nicht gekennzeichnet sind. Das verhindert ein unnötiges "
                + "Laden vieler Module, die sich schon in der Datenbank befinden.";
        }

        /// <summary>
        /// Diese Funktion dekomprimiert eine ZIP-Datei.
        /// </summary>
        /// <param name="FileName">Die Datei die dekomprimiert werden soll.</param>
        /// <param name="OutputDir">Das Verzeichnis in dem die Dateien dekomprimiert werden sollen.</param>
        public void DecompressFile(string FileName, string OutputDir)
        {
             FileStream ZFS = new FileStream(FileName, FileMode.Open);
            ICSharpCode.SharpZipLib.Zip.ZipInputStream ZIN = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(ZFS);
            ZIN.Password = "magixx-1-xxigam";

            ICSharpCode.SharpZipLib.Zip.ZipEntry ZipEntry = default(ICSharpCode.SharpZipLib.Zip.ZipEntry);

            byte[] Buffer = new byte[4097];
            int ByteLen = 0;
            FileStream FS = null;

            string InZipDirName = null;
            string InZipFileName = null;
            string TargetFileName = null;

            do
            {
                ZipEntry = ZIN.GetNextEntry();
                if (ZipEntry == null) break;

                InZipDirName = Path.GetDirectoryName(ZipEntry.Name);
                InZipFileName = Path.GetFileName(ZipEntry.Name);

                string extention = Path.GetExtension(InZipFileName);
                InZipFileName = Path.GetFileNameWithoutExtension(InZipFileName);

                if (InZipFileName.LastIndexOf("-") > -1)
                {
                    InZipFileName = InZipFileName.Substring(0, InZipFileName.LastIndexOf("-"));
                    InZipFileName = InZipFileName.Substring(InZipFileName.LastIndexOf("-") + 1);
                }

                InZipFileName = InZipFileName + extention;

                if (!Directory.Exists(Path.Combine(OutputDir, InZipDirName)))
                    Directory.CreateDirectory(Path.Combine(DatabaseHelper.LiteDatabase.DataPath, InZipDirName));

                if (InZipDirName == String.Empty)
                    TargetFileName = Path.Combine(OutputDir, InZipFileName).Replace("„", "ä");
                else
                    TargetFileName = Path.Combine(Path.Combine(DatabaseHelper.LiteDatabase.DataPath, InZipDirName), InZipFileName).Replace("„", "ä");

                if (InZipFileName != String.Empty)
                {
                    FS = new FileStream(TargetFileName, FileMode.Create);
                    do
                    {
                        ByteLen = ZIN.Read(Buffer, 0, Buffer.Length);
                        FS.Write(Buffer, 0, ByteLen);
                    }
                    while (!(ByteLen <= 0));
                    FS.Close();
                }
            }
            while (true);

            ZIN.Close();
            ZFS.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.ExitCode = 1;
            Close();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            btnCheck.Enabled = false;
            notloaded = 0;
            string localPath = String.Empty;

            // Lösche Update-Path
            string[] files = Directory.GetFiles(updatePath);
            foreach (string item in files)
            {
                FileInfo info = new FileInfo(item);
                info.IsReadOnly = false;
                File.Delete(item);
            }

            //Lösche Download-Path
            files = Directory.GetFiles(Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Downloads"));
            foreach (string item in files)
            {
                FileInfo info = new FileInfo(item);
                info.IsReadOnly = false;
                File.Delete(item);
            }

            if (optDVD.Checked) 
            {
                dlgFolder.Description = LanguageHelper.Localization.GetTranslation(Name, "msgChoose");

                dlgFolder.ShowDialog(this);

                if (dlgFolder.SelectedPath != String.Empty)
                    localPath = dlgFolder.SelectedPath;
            }

            //if (localPath == string.Empty)
            //{
            //    formDownload.CalculateTotalProgress = true;
            //    formDownload.DeleteCompletedFilesAfterCancel = true;
            //    formDownload.Add("http://coinbook.de/Downloads/Module/2018.3/Nation-2018.3.zip", Path.Combine(Helper.UpdatePath, "Nation-2018.3.zip"));
            //    formDownload.ShowDialog(IWin32Window window);
            //}

            formDownload = new frmDownLoader();
            formDownload.CalculateTotalProgress = true;
            formDownload.DeleteCompletedFilesAfterCancel = true;
            //formDownload.Localisation = LanguageHelper.Localization;

            var module = DatabaseHelper.LiteDatabase.ReadModulLizenzen();

            lizenzierteModule.Clear();
            Downloads item1 = new Downloads();
            item1.ID = 0;
            item1.Jahr = "2021.3";
            item1.Lizenz = "Allgemein";
            item1.Bezeichnung = "Allgemein";
            item1.Key = "Allgemein";
            lizenzierteModule.Add(item1);

            foreach (var m in module)
            {
                if (Convert.ToInt32(m.Jahr.Substring(0, 4)) >= 2017)
                {
                    Downloads item = new Downloads();
                    item.ID = m.id;
                    item.Jahr = m.Jahr;
                    item.Lizenz = m.Lizenz;
                    item.Bezeichnung = m.Lizenz;
                    item.Key = m.Key;
                    lizenzierteModule.Add(item);
                }
            }

            if (auswahlListe.FirstOrDefault(x => x.Check == true) != null)
            {
                foreach (var m in auswahlListe)
                    if (!m.Check)
                        if(lizenzierteModule.FirstOrDefault(x => x.ID == m.ID) != null)
                            lizenzierteModule.RemoveAll(x => x.ID == m.ID);
            }
            else
            {
                var downloads = DatabaseHelper.LiteDatabase.ReadDownloads(CoinbookHelper.ModulKey);
                foreach (var m in downloads)
                {
                    var l = lizenzierteModule.FirstOrDefault(x => x.ID == m.ID);
                    if (l != null)
                    {
                        l.Datum = m.Datum;
                        l.Lizenz = m.Jahr;
                        l.OldLizenz = m.OldLizenz;
                    }
                }
            }

            if (optDVD.Checked) 
                loadFromDVD();
            else 
                loadFromCloud();
        }

        private void entpacken(string path)
        {
            string[] temp = new string[2];

            string[] files = Directory.GetFiles(path, "*.zip");
            if (files.Length == 0)
                MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation(Name, "msgNoModul"), "Coinbook", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                for (int i = 0; i < files.Length; i++)
                {
                    string temp2 = Path.GetFileNameWithoutExtension(files[i]);
                    int pos = temp2.LastIndexOf("-");
                    temp[0] = temp2.Substring(0, pos);
                    temp[1] = temp2.Substring(pos + 1);

                    var t = lizenzierteModule.FirstOrDefault(x => x.Key == temp[0]);
                    if(t != null && t.Jahr == temp[1])
                    {
                        if (temp[0] == "Allgemein")
                            t.ID = 999;
                        dictionary.Add(temp[0], temp[1]);
                    }
                }

                if (dictionary.Count == 0)
                {
                    string text = LanguageHelper.Localization.GetTranslation(Name, "msgNewImport");

                    if (MessageBoxAdv.Show(text, "Coinbook", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        for (int i = 0; i < files.Length; i++)
                        {
                            temp = Path.GetFileNameWithoutExtension(files[i]).Split('-');
                            dictionary.Add(temp[0], temp[1]);
                        }
                }

                bgwImport.RunWorkerAsync(path);
            }
        }

        private void bgwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            unzip();
        }

        private void unzip()
        {
            count = 1;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 0;
            bgwImport.ReportProgress(dictionary.Count, parameter);

            foreach (KeyValuePair<string, string> item in dictionary)
            {
                string file = Path.Combine(Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Updater"), item.Key + "-" + item.Value + ".zip");
                if (!File.Exists(file))
                    continue;

                FileInfo info = new FileInfo(file);
                if (info.Length== 0)
                {
                    File.Delete(file);
                    continue;
                }


                {
                    FileInfo f = new FileInfo(file);
                    String fileDate = f.CreationTime.ToString();

                    parameter = new ProgressParameter();

                    parameter.Label = LanguageHelper.Localization.GetTranslation(Name, "msgEntpacke") + " " + Path.GetFileName(file);
                    parameter.Command = 1;
                    bgwImport.ReportProgress(count, parameter);

                    ArchivHelper.DecompressFile(file, downloadPath, "","magixx-1-xxigam");

                    parameter.Command = 1;
                    bgwImport.ReportProgress(count, parameter);

                    parameter.Command = 3;
                    parameter.Max = 100;
                    parameter.Text = parameter.Text = LanguageHelper.Localization.GetTranslation(Name, "msgEntpacke") + " " + Path.GetFileName(file);
                    bgwImport.ReportProgress(0, parameter);

                    count++;

                    if (file.Contains("Allgemein"))
                    {
                        importCulture("tblCulture.xml");
                        importErhaltung("tblErhaltungsgrad.xml");
                    }
                    else
                    {
                        int modul = getModulID;

                        var xml = Directory.GetFiles(downloadPath, "*.xml");

                        foreach (var item3 in xml)
                        {
                            var item2 = Path.GetFileName(item3);

                            if (item2.Contains("tblAera"))
                                importAera(item2, modul);

                            if (item2.Contains("tblGebiet"))
                                importRegion(item2, modul);

                            if (item2.Contains("tblCB_DB"))
                                importKatalog(item2, modul);

                            if (item2.Contains("tblModule"))
                                importModule(item2, modul);

                            if (item2.Contains("tblPrägeanstalt"))
                                importPraegeanstalt(item2, modul);

                            if (item2.Contains("tblTexte"))
                            {
                                importTexte(item2, modul, "EN");
                                File.Delete(item3);
                            }
                        }
                            var zip = Directory.GetFiles(downloadPath, "*.zip");
                        //string bilder = string.Format("{0}-{1}-{2}.zip", item.Key, "Bilder", item.Value).Replace("ä", "ae").Replace(" ", "_").Replace("Ö", "Oe");
                        importPictures(zip[0]);
                    }
                    File.Delete(file);

                    parameter.Command = 4;
                    parameter.Max = 0;
                    parameter.Text = string.Empty;
                    bgwImport.ReportProgress(0, parameter);

                    var t = lizenzierteModule.FirstOrDefault(x => x.Key == item.Key);
                    if (item.Key == "Allgemein")
                        t.ID = 999;

                    Downloads downloads = new Downloads();
                    downloads.ID = t.ID;
                    downloads.Jahr = t.Jahr;
                    downloads.Key = t.Key;
                    downloads.OldLizenz = item.Value;
                    downloads.Datum = fileDate;
                    downloads.Bezeichnung = t.Bezeichnung;
                    downloads.Lizenz = item.Value;

                    DatabaseHelper.LiteDatabase.SaveDownloads(downloads);
                }
            }
        }

        private int getModulID
        {
            get
            {
                int result = 0;
                string file = Path.Combine(downloadPath, "modul.xml");
                XmlDocument document = new XmlDocument();

                document.Load(file);

                XmlNode root = document.SelectSingleNode("DocumentElement");
                XmlNode modul = root.SelectSingleNode("Modul");
                result = Convert.ToInt32(modul.SelectSingleNode("id").InnerText);

                File.Delete(file);

                return result;
            }
        }

        private void importPictures(string file)
        {
            int countTable = 0;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = Path.GetFileName(file) + " " + LanguageHelper.Localization.GetTranslation("Keys", "unzipPictures");
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            parameter = new ProgressParameter();

            parameter.Label = LanguageHelper.Localization.GetTranslation(Name, "msgEntpacke") + " " + Path.GetFileName(file);
            parameter.Command = 1;
            bgwImport.ReportProgress(count, parameter);

            ArchivHelper.DecompressFile(file, picturePath, "","magixx-1-xxigam");
            File.Delete(file);

            //string[] files = Directory.GetFiles(downloadPath);

            //for (int i = 0; i < files.Length; i++)
            //{
            //    countTable++;
            //    parameter.Command = 4;
            //    parameter.Max = files.Length;
            //    parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "copying") +" " + Path.GetFileName(files[i]);
            //    bgwImport.ReportProgress(files.Length, parameter);

            //    if (File.Exists(Path.Combine(CoinbookHelper.Picturepath, Path.GetFileName(files[i]))))
            //        try
            //        {
            //            File.Delete(Path.Combine(CoinbookHelper.Picturepath, Path.GetFileName(files[i])));
            //            File.Move(files[i], Path.Combine(CoinbookHelper.Picturepath, Path.GetFileName(files[i])));
            //        }
            //        catch { }
            //}
        }

        private void bgwImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressParameter parameter = (ProgressParameter)e.UserState;

            if (!string.IsNullOrEmpty(parameter.Label))
                txtAnzeige.Text = parameter.Label;

            switch (parameter.Command)
            {
                case 0:         //set maxValue
                    if (parameter.Max > 0)
                        progressBar.MaxValue = Convert.ToInt32(parameter.Max);
                    else
                        progressBar.MaxValue = e.ProgressPercentage;
                    break;

                case 1:         // set value
                    progressBar.Value = e.ProgressPercentage;
                    break;

                case 2:         //set maxValue
                    pgbBar.Text = parameter.Text;
                    break;

                case 3:         // set value
                    pgbBar.Value = e.ProgressPercentage;
                    pgbBar.Text = string.Format("{0} {1} / {2}", parameter.Text, e.ProgressPercentage, parameter.Max);
                    pgbBar.MaxValue = Convert.ToInt32(parameter.Max);
                    break;

                case 4:         // set value
                    pgbBar.Value = e.ProgressPercentage;
                    pgbBar.Text = parameter.Text;
                    pgbBar.MaxValue = Convert.ToInt32(parameter.Max);
                    break;
            }
        }

        private void bgwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtAnzeige.Text = string.Empty;
            progressBar.Value = 100;
            TopMost = false;
            MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "msgReady"), "Coinbook");

            Environment.ExitCode = 0;
            Close();
        }

        private void importAera(string file, int nationID)
        {
            List<Aera> aeras = new List<Aera>();

            int countTable = 0;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "loadÄra");
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(downloadPath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblAera");

            foreach (XmlNode node in nodes)
            {
                countTable++;
                parameter.Command = 3;
                parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "executeEra");
                bgwImport.ReportProgress(countTable, parameter);

                Aera aera = new Aera();
                aera.ID = ConvertEx.ToInt32(node.SelectSingleNode("ID").InnerText);
                aera.Bezeichnung = node.SelectSingleNode("DE_DE").InnerText;
                aera.NationID = ConvertEx.ToInt32(node.SelectSingleNode("NAT").InnerText);
                aera.Sortierung = ConvertEx.ToInt32(node.SelectSingleNode("Sortierung").InnerText);

                aeras.Add(aera);
            }
            DatabaseHelper.LiteDatabase.BulkUpsertAera(aeras, CoinbookHelper.ModulKey);

            File.Delete(Path.Combine(downloadPath, file));
        }

        private void importRegion(string file, int nationID)
        {
            List<Gebiet> gebiete = new List<Gebiet>();

            int countTable = 0;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "loadRegions");
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(downloadPath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblGebiet");

            foreach (XmlNode node in nodes)
            {
                countTable++;
                parameter.Command = 3;
                bgwImport.ReportProgress(countTable, parameter);

                Gebiet region = new Gebiet();
                region.ID = ConvertEx.ToInt32(node.SelectSingleNode("ID").InnerText);
                region.Bezeichnung = node.SelectSingleNode("DE_DE").InnerText;
                region.NationID = ConvertEx.ToInt32(node.SelectSingleNode("NAT").InnerText);
                region.Sortierung = ConvertEx.ToInt32(node.SelectSingleNode("Sortierung").InnerText);
                region.AeraID = ConvertEx.ToInt32(node.SelectSingleNode("Aera").InnerText);

                gebiete.Add(region);
            }

            DatabaseHelper.LiteDatabase.BulkUpsertRegion(gebiete, CoinbookHelper.ModulKey);

            File.Delete(Path.Combine(downloadPath, file));
        }

        private void importKatalog(string file, int nationID)
        {
            int countTable = 0;
            List<Katalog2> liste = new List<Katalog2>();
            List<MünzDetail> details = new List<MünzDetail>();
            List<Texte> texte = new List<Texte>();

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "loadCatalogue");
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(downloadPath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblCB_DB");

            parameter.Text = "lösche Katalog"; // LanguageHelper.Localization.GetTranslation("Keys", "loadCatalogue");
            DatabaseHelper.LiteDatabase.DeleteKatalog(nationID,"Main");

            foreach (XmlNode node in nodes)
            {
                countTable++;
                parameter.Command = 3;
                parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "executeCatalogue");
                bgwImport.ReportProgress(countTable, parameter);

                Katalog2 katalog = new Katalog2();
                MünzDetail detail = new MünzDetail();
                Texte text = new Texte();

                katalog.NationID = ConvertEx.ToInt32(node.SelectSingleNode("Nation_ID").InnerText);
                katalog.AeraID = ConvertEx.ToInt32(node.SelectSingleNode("Aera_ID").InnerText);
                katalog.GebietID = ConvertEx.ToInt32(node.SelectSingleNode("Gebiet_ID").InnerText);

                if (node.SelectSingleNode("Jahrgang") != null)
                    katalog.Jahrgang = node.SelectSingleNode("Jahrgang").InnerText;
                else
                    katalog.Jahrgang = string.Empty;

                if (node.SelectSingleNode("KatNr") != null)
                    katalog.KatNr = node.SelectSingleNode("KatNr").InnerText;
                else
                    katalog.KatNr = string.Empty;

                katalog.SPreis = 0;
                katalog.SPPreis = 0;
                katalog.SSPreis = 0;
                katalog.SSPPreis = 0;
                katalog.VZPreis = 0;
                katalog.VZPPreis = 0;
                katalog.STNPreis = 0;
                katalog.STHPreis = 0;
                katalog.PPPreis = 0;

                if (node.SelectSingleNode("Erh_S_Preis") != null)
                    katalog.SPreis = decimal.Parse(node.SelectSingleNode("Erh_S_Preis").InnerText, CultureInfo.InvariantCulture);

                if (node.SelectSingleNode("Erh_SP_Preis") != null)
                    katalog.SPPreis = decimal.Parse(node.SelectSingleNode("Erh_SP_Preis").InnerText, CultureInfo.InvariantCulture);

                if (node.SelectSingleNode("Erh_SS_Preis") != null)
                katalog.SSPreis = decimal.Parse(node.SelectSingleNode("Erh_SS_Preis").InnerText, CultureInfo.InvariantCulture);

                if (node.SelectSingleNode("Erh_SSP_Preis") != null)
                    katalog.SSPPreis = decimal.Parse(node.SelectSingleNode("Erh_SSP_Preis").InnerText, CultureInfo.InvariantCulture);

                if (node.SelectSingleNode("Erh_VZ_Preis") != null)
                    katalog.VZPreis = decimal.Parse(node.SelectSingleNode("Erh_VZ_Preis").InnerText, CultureInfo.InvariantCulture);

                if (node.SelectSingleNode("Erh_VZP_Preis") != null)
                    katalog.VZPPreis = decimal.Parse(node.SelectSingleNode("Erh_VZP_Preis").InnerText, CultureInfo.InvariantCulture);

                if(node.SelectSingleNode("Erh_STN_Preis") != null)
                    katalog.STNPreis = decimal.Parse(node.SelectSingleNode("Erh_STN_Preis").InnerText, CultureInfo.InvariantCulture);

                if(node.SelectSingleNode("Erh_STH_Preis") != null)
                    katalog.STHPreis = decimal.Parse(node.SelectSingleNode("Erh_STH_Preis").InnerText, CultureInfo.InvariantCulture);

                if(node.SelectSingleNode("Erh_PP_Preis") != null)
                    katalog.PPPreis = decimal.Parse(node.SelectSingleNode("Erh_PP_Preis").InnerText, CultureInfo.InvariantCulture);

                katalog.Auflage = node.SelectSingleNode("Auflage").InnerText;
                katalog.AuflageSTH = node.SelectSingleNode("AuflageSTH").InnerText;
                katalog.AuflagePP = node.SelectSingleNode("AuflagePP").InnerText;
                katalog.GUID = node.SelectSingleNode("RepID").InnerText;
                katalog.ID = node.SelectSingleNode("RepID").InnerText;

                string kommentar = string.Empty;
                if (node.SelectSingleNode("Kommentar") != null)
                    kommentar = node.SelectSingleNode("Kommentar").InnerText;
                if (kommentar == null)
                    kommentar = string.Empty;

                string besonderheit = string.Empty;
                if (node.SelectSingleNode("Besonderheit") != null)
                    besonderheit = node.SelectSingleNode("Besonderheit").InnerText;
                if (besonderheit == null)
                    besonderheit = string.Empty;

                katalog.HinweisKZ = string.Empty;
                if (kommentar.Trim() + besonderheit.Trim() != string.Empty)  
                    katalog.HinweisKZ ="!";

                if (node.SelectSingleNode("Münzzeichen") != null)
                    katalog.Muenzzeichen = node.SelectSingleNode("Münzzeichen").InnerText;
                else
                    katalog.Muenzzeichen = string.Empty;

                if (node.SelectSingleNode("Nominal") != null)
                    katalog.Nominal = node.SelectSingleNode("Nominal").InnerText;
                else
                    katalog.Nominal = string.Empty;

                if (node.SelectSingleNode("Währung") != null)
                    katalog.Waehrung = node.SelectSingleNode("Währung").InnerText;
                else
                    katalog.Waehrung = string.Empty;

                if (node.SelectSingleNode("Motiv") != null)
                    katalog.Motiv = node.SelectSingleNode("Motiv").InnerText;
                else
                    katalog.Motiv = "";

                if (node.SelectSingleNode("Picture") != null)
                    katalog.Picture = node.SelectSingleNode("Picture").InnerText;
                else
                    katalog.Picture = "";

                if (node.SelectSingleNode("LPS") != null)
                    katalog.LPS = ConvertEx.ToBoolean(node.SelectSingleNode("LPS").InnerText);
                else
                    katalog.LPS = false;

                if (node.SelectSingleNode("LPSP") != null)
                    katalog.LPSP = ConvertEx.ToBoolean(node.SelectSingleNode("LPSP").InnerText);
                else
                    katalog.LPSP = false;

                if (node.SelectSingleNode("LPSS") != null)
                    katalog.LPSS = ConvertEx.ToBoolean(node.SelectSingleNode("LPSS").InnerText);
                else
                    katalog.LPSS = false;

                if (node.SelectSingleNode("LPSSP") != null)
                    katalog.LPSSP = ConvertEx.ToBoolean(node.SelectSingleNode("LPSSP").InnerText);
                else
                    katalog.LPSSP = false;

                if (node.SelectSingleNode("LPVZ") != null)
                    katalog.LPVZ = ConvertEx.ToBoolean(node.SelectSingleNode("LPVZ").InnerText);
                else
                    katalog.LPVZ = false;

                if (node.SelectSingleNode("LPVZP") != null)
                    katalog.LPVZP = ConvertEx.ToBoolean(node.SelectSingleNode("LPVZP").InnerText);
                else
                    katalog.LPVZP = false;

                if (node.SelectSingleNode("LPSTN") != null)
                    katalog.LPSTN = ConvertEx.ToBoolean(node.SelectSingleNode("LPSTN").InnerText);
                else
                    katalog.LPSTN = false;

                if (node.SelectSingleNode("LPSTH") != null)
                    katalog.LPSTH = ConvertEx.ToBoolean(node.SelectSingleNode("LPSTH").InnerText);
                else
                    katalog.LPSTH = false;

                if (node.SelectSingleNode("LPPP") != null)
                    katalog.LPPP = ConvertEx.ToBoolean(node.SelectSingleNode("LPPP").InnerText);
                else
                    katalog.LPPP = false;

                //------------------------
                detail.Gewicht = decimal.Parse(node.SelectSingleNode("Gewicht").InnerText, CultureInfo.InvariantCulture);
                detail.Durchmesser = decimal.Parse(node.SelectSingleNode("Durchmesser").InnerText, CultureInfo.InvariantCulture);
                detail.Dicke = decimal.Parse(node.SelectSingleNode("Dicke").InnerText, CultureInfo.InvariantCulture);
                detail.NationID = ConvertEx.ToInt32(node.SelectSingleNode("Nation_ID").InnerText);
                detail.GUID = node.SelectSingleNode("RepID").InnerText;
                detail.ID = node.SelectSingleNode("RepID").InnerText;
                detail.AeraID = ConvertEx.ToInt32(node.SelectSingleNode("Aera_ID").InnerText);
                detail.GebietID = ConvertEx.ToInt32(node.SelectSingleNode("Gebiet_ID").InnerText);

                if (node.SelectSingleNode("AusserKurs") != null)
                    detail.AusserKurs = node.SelectSingleNode("AusserKurs").InnerText;
                else
                    detail.AusserKurs = string.Empty;

                if (node.SelectSingleNode("InKurs") != null)
                    detail.InKurs = node.SelectSingleNode("InKurs").InnerText;
                else
                    detail.InKurs = string.Empty;

                if (node.SelectSingleNode("geprägt") != null)
                    detail.Gepraegt = node.SelectSingleNode("geprägt").InnerText;
                else
                    detail.Gepraegt = string.Empty;

                if (node.SelectSingleNode("Material") != null)
                    detail.Material = node.SelectSingleNode("Material").InnerText;
                else
                    detail.Material = "";

                if (node.SelectSingleNode("Legierung") != null)
                    detail.Legierung = node.SelectSingleNode("Legierung").InnerText;
                else
                    detail.Legierung = "";

                if (node.SelectSingleNode("Form") != null)
                    detail.Form = node.SelectSingleNode("Form").InnerText;
                else
                    detail.Form = "";

                if (node.SelectSingleNode("Orientation") != null)
                    detail.Orientation = node.SelectSingleNode("Orientation").InnerText;
                else
                    detail.Orientation = "";

                if (node.SelectSingleNode("Referenz") != null)
                    detail.Referenz = node.SelectSingleNode("Referenz").InnerText;
                else
                    detail.Referenz = "";

                if (node.SelectSingleNode("LPStandS") != null)
                    detail.LPStandS = node.SelectSingleNode("LPStandS").InnerText;
                else
                    detail.LPStandS = "";

                if (node.SelectSingleNode("LPStandSP") != null)
                    detail.LPStandSP = node.SelectSingleNode("LPStandSP").InnerText;
                else
                    detail.LPStandSP = "";

                if (node.SelectSingleNode("LPStandSS") != null)
                    detail.LPStandSS = node.SelectSingleNode("LPStandSS").InnerText;
                else
                    detail.LPStandSS = "";

                if (node.SelectSingleNode("LPStandSSP") != null)
                    detail.LPStandSSP = node.SelectSingleNode("LPStandSSP").InnerText;
                else
                    detail.LPStandSSP = "";

                if (node.SelectSingleNode("LPStandVZ") != null)
                    detail.LPStandVZ = node.SelectSingleNode("LPStandVZ").InnerText;
                else
                    detail.LPStandVZ = "";

                if (node.SelectSingleNode("LPStandVZP") != null)
                    detail.LPStandVZP = node.SelectSingleNode("LPStandVZP").InnerText;
                else
                    detail.LPStandVZP = "";

                if (node.SelectSingleNode("LPStandSTN") != null)
                    detail.LPStandSTN = node.SelectSingleNode("LPStandSTN").InnerText;
                else
                    detail.LPStandSTN = "";

                if (node.SelectSingleNode("LPStandSTH") != null)
                    detail.LPStandSTH = node.SelectSingleNode("LPStandSTH").InnerText;
                else
                    detail.LPStandSTH = "";

                if (node.SelectSingleNode("LPStandPP") != null)
                    detail.LPStandPP = node.SelectSingleNode("LPStandPP").InnerText;
                else
                    detail.LPStandPP = "";

                if (node.SelectSingleNode("AusserkursBool") != null)
                {
                    detail.AusserkursBOOL = ConvertEx.ToBoolean(node.SelectSingleNode("AusserkursBool").InnerText);
                    detail.AusserKursBool = detail.AusserkursBOOL;
                }
                else
                {
                    detail.AusserkursBOOL = false;
                    detail.AusserKursBool = false;
                }

                if (node.SelectSingleNode("BearbeitungsDatum") != null)
                    detail.Bearbeitungsdatum = Convert.ToDateTime(node.SelectSingleNode("BearbeitungsDatum").InnerText).ToShortDateString();
                else
                    detail.Bearbeitungsdatum = "";

                //----------------------------
                text.ID = node.SelectSingleNode("RepID").InnerText; 
                text.GUID = node.SelectSingleNode("RepID").InnerText; 
                text.NationID = ConvertEx.ToInt32(node.SelectSingleNode("Nation_ID").InnerText);

                if (node.SelectSingleNode("Aversbeschreibung") != null)
                    text.Aversbeschreibung = node.SelectSingleNode("Aversbeschreibung").InnerText;
                else
                    text.Aversbeschreibung = string.Empty;

                    text.Besonderheit = besonderheit;

                if (node.SelectSingleNode("Reversbeschreibung") != null)
                    text.Reversbeschreibung = node.SelectSingleNode("Reversbeschreibung").InnerText;
                else
                    text.Reversbeschreibung = string.Empty;

                    text.Kommentar = kommentar;

                if (node.SelectSingleNode("Rand") != null)
                    text.Rand = node.SelectSingleNode("Rand").InnerText;
                else
                    text.Rand = "";

                if (node.SelectSingleNode("Ausgabeanlass") != null)
                    text.Ausgabeanlass = node.SelectSingleNode("Ausgabeanlass").InnerText;
                else
                    text.Ausgabeanlass = "";

                if (node.SelectSingleNode("ÄhnlicheMotive") != null)
                    text.AehnlicheMotive = node.SelectSingleNode("ÄhnlicheMotive").InnerText;
                else
                    text.AehnlicheMotive = "";

                if (node.SelectSingleNode("AversEntwurf") != null)
                    text.AversEntwurf = node.SelectSingleNode("AversEntwurf").InnerText;
                else
                    text.AversEntwurf = "";

                if (node.SelectSingleNode("ReversEntwurf") != null)
                    text.ReversEntwurf = node.SelectSingleNode("ReversEntwurf").InnerText;
                else
                    text.ReversEntwurf = "";

                if (node.SelectSingleNode("Typ") != null)
                    text.Typ = node.SelectSingleNode("Typ").InnerText;
                else
                    text.Typ = "";

                if (node.SelectSingleNode("Prägeort") != null)
                    text.Praegeort = node.SelectSingleNode("Prägeort").InnerText;
                else
                    text.Praegeort = "";

                liste.Add(katalog);
                details.Add(detail);
                texte.Add(text);
            }

            parameter.Command = 3;
            parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "saveCatalogue");
            bgwImport.ReportProgress(countTable, parameter);

            DatabaseHelper.LiteDatabase.UpsertKatalog(liste, CoinbookHelper.ModulKey);
            DatabaseHelper.LiteDatabase.BulkUpsertDetails(details, CoinbookHelper.ModulKey);
            DatabaseHelper.LiteDatabase.BulkUpsertTexteDE(texte, CoinbookHelper.ModulKey);

            File.Delete(Path.Combine(downloadPath, file));
        }

        private void importModule(string file, int nationID)
        {
            List<Modul> module = new List<Modul>();

            int countTable = 0;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "loadModule");
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(downloadPath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblModule");

            foreach (XmlNode node in nodes)
            {
                countTable++;
                parameter.Command = 3;
                parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "executeModule");
                bgwImport.ReportProgress(countTable, parameter);

                Modul item = new Modul();
                item.ModulID = ConvertEx.ToInt32(node.SelectSingleNode("id").InnerText);
                item.Typ = node.SelectSingleNode("typ").InnerText;
                item.Sprache = node.SelectSingleNode("sprache").InnerText;
                item.Text = node.SelectSingleNode("text").InnerText;
                item.NationID = ConvertEx.ToInt32(node.SelectSingleNode("Nation").InnerText);

                module.Add(item);
            }

            DatabaseHelper.LiteDatabase.BulkUpsertModule(module, CoinbookHelper.ModulKey);

            File.Delete(Path.Combine(downloadPath, file));
        }

        private void importPraegeanstalt(string file, int nationID)
        {
            int countTable = 0;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "loadMint");
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(downloadPath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblPrägeanstalt");

            foreach (XmlNode node in nodes)
            {
                countTable++;
                parameter.Command = 3;
                parameter.Text = LanguageHelper.Localization.GetTranslation("Keys", "executeMint");
                bgwImport.ReportProgress(countTable, parameter);

                Praegeanstalt item = new Praegeanstalt();
                item.ID = ConvertEx.ToInt32(node.SelectSingleNode("id").InnerText);
                item.Nation = ConvertEx.ToInt32(node.SelectSingleNode("Nation").InnerText);
                item.Muenzzeichen = node.SelectSingleNode("Münzzeichen").InnerText;
                item.Ort = node.SelectSingleNode("Ort").InnerText;
                item.Adresse = node.SelectSingleNode("Adresse").InnerText;
                item.Email = node.SelectSingleNode("Email").InnerText;
                item.Homepage = node.SelectSingleNode("Homepage").InnerText;

                if (node.SelectSingleNode("Bemerkung") != null)
                    item.Bemerkung = node.SelectSingleNode("Bemerkung").InnerText;

                if (node.SelectSingleNode("Caption") != null)
                    item.Land = node.SelectSingleNode("Caption").InnerText;

                if (node.SelectSingleNode("Land") != null)
                    item.Land = node.SelectSingleNode("Land").InnerText;

                if (node.SelectSingleNode("Zeit") != null)
                    item.Zeit = node.SelectSingleNode("Zeit").InnerText;

                DatabaseHelper.LiteDatabase.SavePraegeanstalt(item, CoinbookHelper.ModulKey);
            }

            File.Delete(Path.Combine(downloadPath, file));
        }

        private void importTexte(string file, int nationID, string sprache)
        {
            Dictionary<string, Texte> texte = new Dictionary<string, Texte>();
            Texte item;

            int countTable = 0;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "loadText"), sprache);
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(downloadPath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblTexte");

            DatabaseHelper.LiteDatabase.DeleteTexte(nationID, CoinbookHelper.ModulKey);

            foreach (XmlNode node in nodes)
            {
                countTable++;
                parameter.Command = 3;
                parameter.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "executeText"), sprache);
                bgwImport.ReportProgress(countTable, parameter);

                if (node.SelectSingleNode("sprache").InnerText == sprache)
                {
                    string guid = node.SelectSingleNode("guid").InnerText;

                    if (texte.ContainsKey(guid))
                        item = texte[guid];
                    else
                    {
                        item = new Texte { GUID = guid, NationID = nationID, ID = guid };
                        texte.Add(guid, item);
                    }

                    switch (node.SelectSingleNode("typ").InnerText.Replace("Ä", "Ae").Replace("ä", "ae").Replace("_", ""))
                    {
                        case "Aversbeschreibung":
                            item.Aversbeschreibung = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Besonderheit":
                            item.Besonderheit = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Reversbeschreibung":
                            item.Reversbeschreibung = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Kommentar":
                            item.Kommentar = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Rand":
                            item.Rand = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Ausgabeanlass":
                            item.Ausgabeanlass = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Material":
                            item.Material = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Legierung":
                            item.Legierung = node.SelectSingleNode("text").InnerText;
                            break;

                        case "AehnlicheMotive":
                            item.AehnlicheMotive = node.SelectSingleNode("text").InnerText;
                            break;

                        case "AversEntwurf":
                            item.AversEntwurf = node.SelectSingleNode("text").InnerText;
                            break;

                        case "ReversEntwurf":
                            item.ReversEntwurf = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Form":
                            item.Form = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Orientation":
                            item.Orientation = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Typ":
                            item.Typ = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Referenz":
                            item.Referenz = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Praegeort":
                            item.Praegeort = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Motiv":
                            item.Motiv = node.SelectSingleNode("text").InnerText;
                            break;
                    }
                }
            }

            parameter.Command = 4;
            parameter.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "saveText"), sprache);
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            parameter.Command = 3;
            parameter.Text = string.Format(LanguageHelper.Localization.GetTranslation("Keys", "saveText"), sprache);
            bgwImport.ReportProgress(countTable, parameter);

            if (sprache == "DE")
            {
                DatabaseHelper.LiteDatabase.BulkUpsertTexteDE(texte.Values.ToList(), CoinbookHelper.ModulKey);
            }
            else
            {
                DatabaseHelper.LiteDatabase.BulkUpsertTexteEN(texte.Values.ToList(), CoinbookHelper.ModulKey);
            }
        }

        private void importCulture(string file)
        {
            List<Culture> cultureList = new List<Culture>();

            int countTable = 0;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = "load Culture";
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(downloadPath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblCulture");

            foreach (XmlNode node in nodes)
            {
                countTable++;
                parameter.Command = 3;
                bgwImport.ReportProgress(countTable, parameter);

                Culture culture = new Culture();
                culture.ID = ConvertEx.ToInt32(node.SelectSingleNode("id").InnerText);
                culture.Bezeichnung = node.SelectSingleNode("Bezeichnung").InnerText;
                culture.EN_GB = node.SelectSingleNode("EN_GB").InnerText;
                culture.Faktor = decimal.Parse(node.SelectSingleNode("Faktor").InnerText, CultureInfo.InvariantCulture);
                culture.Waehrung = node.SelectSingleNode("Waehrung").InnerText;
                culture.Kultur = string.Empty;
                if (node.SelectSingleNode("Kultur")!= null)
                culture.Kultur = node.SelectSingleNode("Kultur").InnerText;

                cultureList.Add(culture);
            }

            DatabaseHelper.LiteDatabase.BulkUpsertCulture(cultureList);

            File.Delete(Path.Combine(downloadPath, file));
        }

        private void importErhaltung(string file)
        {
            List<Erhaltungsgrad> erhaltungsgrade = new List<Erhaltungsgrad>();

            int countTable = 0;

            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = "load Erhaltungsgrad";
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(downloadPath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblErhaltungsgrad");

            int id = 0;
            foreach (XmlNode node in nodes)
            {
                countTable++;
                parameter.Command = 3;
                bgwImport.ReportProgress(countTable, parameter);

                id++;
                Erhaltungsgrad erhaltungsgrad = new Erhaltungsgrad();
                erhaltungsgrad.ID = id;
                erhaltungsgrad.ErhaltungsgradID = ConvertEx.ToInt32(node.SelectSingleNode("id").InnerText);
                erhaltungsgrad.Bezeichnung = node.SelectSingleNode("Bezeichnung").InnerText;
                erhaltungsgrad.Sprache = node.SelectSingleNode("Sprache").InnerText;
                erhaltungsgrad.Erhaltung = node.SelectSingleNode("Erhaltung").InnerText;
                erhaltungsgrad.Land = node.SelectSingleNode("Land").InnerText;

                erhaltungsgrade.Add(erhaltungsgrad);
            }

            DatabaseHelper.LiteDatabase.BulkUpsertErhaltungsgrade(erhaltungsgrade);

            File.Delete(Path.Combine(downloadPath, file));
        }

        private void btnClearDownloads_Click(object sender, EventArgs e)
        {
            DatabaseHelper.LiteDatabase.ClearDownloads();
        }

        private void btnResetSingleDownload_Click(object sender, EventArgs e)
        {
            grdModule.Visible = true;

            var temp = getNations();
            auswahlListe.Clear();

            foreach(var item in temp)
                auswahlListe.Add(new ListViewModel() { Bezeichnung = item.Bezeichnung, ID = item.ID, Key = item.Key, Check = false });

            grdModule.DataSource = auswahlListe;
            grdModule.Columns["ID"].Visible = false;
            grdModule.Columns["Key"].Visible = false;
        }

        public static List<Nation> getNations(string all = null)
        {
            var result = DatabaseHelper.LiteDatabase.ReadNationen();

            result = result.OrderBy(x => x.Bezeichnung).ToList();

            if (!string.IsNullOrEmpty(all))
            {
                Nation item = new Nation();
                item.ID = 0;
                item.Bezeichnung = all;
                result.Insert(0, item);
            }

            return result;
        }

        private void loadFromDVD()
        {
            if (dlgFolder.SelectedPath != String.Empty)
            {
                found = false;
                int counter = 0;
                foreach (var item in lizenzierteModule)
                {
                    string file1 = item.Key.Replace(" ", "_");
                    file1 = file1.Replace("ä", "ae");
                    file1 = file1.Replace("Ö", "Oe");
                    file1 = file1 + "-" + item.Jahr + ".zip".Replace(" ", "_");

                    string file = ArchivHelper.Zipfile(item.Key, item.Jahr);
                    file = item.Key + "-" + item.Jahr + ".zip";
                    if (File.Exists(Path.Combine(dlgFolder.SelectedPath, Path.Combine(item.Jahr, file1))))
                    {
                        counter++;
                        txtAnzeige.Text = Path.GetFileName(file);
                        progressBar.Value = counter * 100 / lizenzierteModule.Count;
                        Application.DoEvents();
                        string p = Path.Combine(Path.Combine(dlgFolder.SelectedPath, item.Jahr), file1);
                        File.Copy(p, Path.Combine(updatePath, Path.GetFileName(file)));
                        FileInfo fInfo = new FileInfo(Path.Combine(updatePath, file));
                        fInfo.IsReadOnly = false;
                        found = true;
                    }
                }

                if (!found)
                {
                    TopMost = false;
                    MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "noModules"), "Coinbook");
                    return;
                }

                if (lizenzierteModule.Count == 0)
                {
                    MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "downloded"), "Coinbook");
                    Close();
                }
                else
                    entpacken(updatePath);
            }
        }

        private void loadFromCloud()
        {
            {
                string host = "www.coinbook.de";
                string user = "ftp12564714-Admin";
                string passwort = "Magixx-1";

                if (!ftpClass.Connect(host, user, passwort))
                {
                    MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "noInternet"), "Coinbook");
                    Close();
                    return;
                }

                for (int i = lizenzierteModule.Count - 1; i >= 0; i--)
                {
                    var item = lizenzierteModule[i];
                    string file1 = item.Key.Replace(" ", "_");
                    file1 = file1.Replace("ä", "ae");
                    file1 = file1.Replace("Ö", "Oe");
                    file1 = file1 + "-" + item.Jahr + ".zip";

                    //item.Url = "http://coinbook.de/Downloads/Module/" + item.Jahr + "/" + file1;;
                    item.Url = "Downloads/Module/" + item.Jahr + "/" + file1;
                    item.Target = Path.Combine(updatePath, item.Key + "-" + item.Jahr + ".zip");

                    if (item.Jahr.Equals(item.OldLizenz) && !string.IsNullOrEmpty(item.Datum))
                    {
                        var newDate = ftpClass.GetModifiedTime(item.Url);
                        if (newDate <= Convert.ToDateTime(item.Datum))
                            lizenzierteModule.Remove(item);
                    }
                    else
                        formDownload.Add("http://coinbook.de/" + item.Url, item.Target);

                }

                if (lizenzierteModule.Count == 1)
                {
                    MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation("Keys", "downloded"), "Coinbook");
                    Close();
                }
                else
                {
                    formDownload.ShowDialog();

                    if (formDownload.DialogResult == true)
                        entpacken(updatePath);
                }
            }
        }
    }

    public class ListViewModel
    {
        public bool Check { get; set; }
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Key { get; set; }
    }
}
