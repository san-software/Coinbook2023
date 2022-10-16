
using Coinbook.Helper;
using Coinbook.Model;
using Coinbook.ModulVerwaltung;
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

namespace Coinbook.Modulverwaltung
{
    public partial class frmModulImport : Form
    {
        private frmDownLoader formDownload = new frmDownLoader();
        private frmDownLoader formDownload1 = new frmDownLoader();
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
            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook.Modulverwaltung");
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
            lblAnzeige.Visible = false;
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
                else
                    return;
            }

            formDownload = new frmDownLoader();
            formDownload.CalculateTotalProgress = true;
            formDownload.DeleteCompletedFilesAfterCancel = true;
            formDownload.Localisation = LanguageHelper.Localization;

            var module = DatabaseHelper.LiteDatabase.ReadModulLizenzen();

            lizenzierteModule.Clear();
            //Downloads item1 = new Downloads();
            //item1.ID = 0;
            //item1.Jahr = "2030.1";
            //item1.Lizenz = "Allgemein";
            //item1.Bezeichnung = "Allgemein";
            //item1.Key = "Allgemein";
            //lizenzierteModule.Add(item1);

            foreach (var m in module)
            {
                if (Convert.ToInt32(m.Jahr.Substring(0, 4)) >= 2017)
                {
                    //if (m.id == 12 && m.Jahr == "2022.1")
                    //{
                    //    List<string> aeras = new List<string>();
                    //    string filter = m.Key.Replace("ä", "ae").Replace(" ", "_");

                    //    FTPClass ftp = new FTPClass();
                    //    if (ftp.Connect(ftp.FTPParameter.URL, ftp.FTPParameter.Admin, ftp.FTPParameter.Passwort, false))
                    //    {
                    //        aeras = ftp.Files($"/Downloads/Module/{m.Jahr}", ".*");
                    //        aeras = aeras.FindAll(x => x.StartsWith(filter)).ToList();
                    //    }

                    //    //var aeras = DatabaseHelper.LiteDatabase.ReadAeras(m.Key, m.id);
                    //    foreach (var a in aeras)
                    //    {
                    //        lizenzierteModule.Add(createSplittedModul(a, m));
                    //        Console.WriteLine(createSplittedModul(a, m).Key);
                    //    }
                    //}
                    //else
                    //{
                        lizenzierteModule.Add(createModul(m));
                    //}
                }
            }

            lizenzierteModule = lizenzierteModule.OrderBy(x => x.Key).ToList();

            if (auswahlListe.FirstOrDefault(x => x.Check == true) != null)
            {
                foreach (var m in auswahlListe)
                    if (!m.Check)
                        if (lizenzierteModule.FirstOrDefault(x => x.ID == m.ID) != null)
                            lizenzierteModule.RemoveAll(x => x.ID == m.ID);
            }
            else
            {
                var downloads = DatabaseHelper.LiteDatabase.ReadDownloads();
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

        private Downloads createModul(Settings2 m, int id = -1)
        {
            Downloads item = new Downloads();
            item.ID = m.id;
            item.Jahr = m.Jahr;
            item.Lizenz = m.Lizenz;
            item.Bezeichnung = m.Lizenz;

            if (id != -1)
                item.Key = m.Key + $"-{id}";
            else
                item.Key = m.Key;

            return item;
        }

        private Downloads createSplittedModul(string file, Settings2 m)
        {
            file = Path.GetFileNameWithoutExtension(file);
            var array = file.Split('-');

            int id = Convert.ToInt32(array[1]);
            if (id < 100)
                id = 900 + id;

            Downloads item = new Downloads();
            item.ID = id;
            item.Jahr = m.Jahr;
            item.Lizenz = m.Lizenz;
            item.Bezeichnung = m.Lizenz;
            item.Key = m.Key + $"-{array[1]}";

            return item;
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

                    var t = lizenzierteModule.FirstOrDefault(x => x.Key == temp2);
                    dictionary.Add(temp2, temp2);
                }

                if (dictionary.Count == 0)
                {
                    string text = LanguageHelper.Localization.GetTranslation(Name, "msgNewImport");

                    if (MessageBoxAdv.Show(text, "Coinbook", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        for (int i = 0; i < files.Length; i++)
                        {
                            string temp2 = Path.GetFileNameWithoutExtension(files[i]);
                            dictionary.Add(temp2, temp2);
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
                Console.WriteLine(item.Key);

                CoinbookHelper.ModulKey = item.Key;

                string file = Path.Combine(Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Updater"), item.Value + ".zip");
                if (!File.Exists(file))
                    continue;

                FileInfo info = new FileInfo(file);
                if (info.Length == 0)
                {
                    File.Delete(file);
                    continue;
                }

                FileInfo f = new FileInfo(file);
                String fileDate = f.CreationTime.ToString();

                parameter = new ProgressParameter();

                parameter.Label = LanguageHelper.Localization.GetTranslation(Name, "msgEntpacke") + " " + Path.GetFileName(file);
                parameter.Command = 1;
                bgwImport.ReportProgress(count, parameter);

                ArchivHelper.DecompressFile(file, downloadPath, "", "magixx-1-xxigam");

                File.Delete(file);

                parameter.Command = 1;
                bgwImport.ReportProgress(count, parameter);

                parameter.Command = 3;
                parameter.Max = 5;
                parameter.Text = parameter.Text = LanguageHelper.Localization.GetTranslation(Name, "msgEntpacke") + " " + Path.GetFileName(file);
                bgwImport.ReportProgress(0, parameter);

                count++;

                string target = item.Key.Replace("_", " ").Replace("ae", "ä").Replace("Oe", "Ö").Replace("ue", "ü").Replace("oe", "ö");
                string source = item.Key.Replace(" ", "_").Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");
                source = Path.Combine(downloadPath, source + ".db");
                target = Path.Combine(DatabaseHelper.LiteDatabase.DataPath, "Katalog", item.Key + ".db");
                File.Copy(source, target, true);
                File.Delete(source);

                var zip = Directory.GetFiles(downloadPath, "Bilder.zip");
                if (zip.Length > 0)
                {
                    //string bilder = string.Format("{0}-{1}-{2}.zip", item.Key, "Bilder", item.Value).Replace("ä", "ae").Replace(" ", "_").Replace("Ö", "Oe");
                    importPictures(zip[0]);
               
                    File.Delete(zip[0]);
                }

                parameter.Command = 4;
                parameter.Max = 0;
                parameter.Text = string.Empty;
                bgwImport.ReportProgress(0, parameter);

                var t = lizenzierteModule.FirstOrDefault(x => x.Key == item.Key);

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

        private void importPictures(string file)
        {
            ProgressParameter parameter = new ProgressParameter();
            parameter.Command = 4;
            parameter.Text = Path.GetFileName(file) + " " + LanguageHelper.Localization.GetTranslation("Keys", "unzipPictures");
            parameter.Max = 100;
            bgwImport.ReportProgress(0, parameter);

            parameter = new ProgressParameter();

            parameter.Label = LanguageHelper.Localization.GetTranslation(Name, "msgEntpacke") + " " + Path.GetFileName(file);
            parameter.Command = 1;
            bgwImport.ReportProgress(count, parameter);

            ArchivHelper.DecompressFile(file, picturePath, "", "magixx-1-xxigam");
            File.Delete(file);
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
 
        private void btnClearDownloads_Click(object sender, EventArgs e)
        {
            DatabaseHelper.LiteDatabase.ClearDownloads();
        }

        private void btnResetSingleDownload_Click(object sender, EventArgs e)
        {
            grdModule.Visible = true;

            var temp = getNations();
            auswahlListe.Clear();

            foreach (var item in temp)
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
                    file1 = file1 + ".zip".Replace(" ", "_");

                    string file = ArchivHelper.Zipfile(item.Key, item.Jahr);
                    file = item.Key + ".zip";
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
                    file1 = file1 + ".zip";

                    //item.Url = "http://coinbook.de/Downloads/Module2022/" + item.Jahr + "/" + file1;;
                    item.Url = "Downloads/Module2022/" + item.Jahr + "/" + file1;
                    item.Target = Path.Combine(updatePath, item.Key + "-" + item.Jahr + ".zip");

                    //item.Url = "Downloads/Module2022/" + file1;
                    item.Target = Path.Combine(updatePath, item.Key + ".zip");

                    if (item.Jahr.Equals(item.OldLizenz) && !string.IsNullOrEmpty(item.Datum))
                    {
                        var newDate = ftpClass.GetModifiedTime(item.Url);
                        if (newDate <= Convert.ToDateTime(item.Datum))
                            lizenzierteModule.Remove(item);
                    }
                    else
                        formDownload.Add("http://coinbook.de/" + item.Url, item.Target);
                }

                if (lizenzierteModule.Count == 0)
                {
                    lblAnzeige.Text = "Alle Module wurden schon heruntergeladen"; // LanguageHelper.Localization.GetTranslation("Keys", "downloded");
                    lblAnzeige.Visible = true;
                    btnCheck.Enabled = true;
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
}

