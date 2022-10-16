using System;
using System.IO;
using System.ComponentModel;
using System.Data;
using SAN.FTP;
using System.Collections.Generic;
using Coinbook.Model;
using System.Linq;
using Coinbook.Helper;

namespace Coinbook
{
    class ModulDownloads
    {
        private string host;
        private string user;
        private string passwort;
        private string downloadPath;
        List<Downloads> downloads;

        BackgroundWorker bgw;

        public ModulDownloads(string host, string user, string passwort)
        {
            this.host = host;
            this.user = user;
            this.passwort = passwort;

            downloads = DatabaseHelper.LiteDatabase.ReadDownloads();

            bgw = new BackgroundWorker();

            bgw.DoWork += Bgw_DoWork;
            bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;

            bgw.RunWorkerAsync();
        }

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string lizenz;
            string datum;

            FTPClass ftpClass = new FTPClass();

            ftpClass.Connect(host, user, passwort);

            var nations = CoinbookHelper.GetNations();
            foreach(var item in nations)
            {
                string key = item.Key;

                var loadedNation = downloads.FirstOrDefault(x => x.ID == item.ID);

                if (loadedNation != null)
                {
                    lizenz = loadedNation.Lizenz;
                    datum = loadedNation.Datum;
                }
                else
                {
                    lizenz = string.Empty;
                    datum = string.Empty;
                }

                string source = "Downloads/Module/" + lizenz + "/" + convertName(key) + "-" + lizenz + ".zip";
                string target = Path.Combine(downloadPath, key + "-" + lizenz + ".zip");

                if (datum == string.Empty)
                {
                    if (ftpClass.Download(source, target) != enmFTPFile.FileDownloadOK)
                        File.Delete(target);
                }
                else
                {
                    DateTime date = ftpClass.GetModifiedTime(source);
                    if (date > Convert.ToDateTime(datum))
                    {
                        if (ftpClass.Download(source, target) != enmFTPFile.FileDownloadOK)
                            File.Delete(target);
                    }
                }
            }
        }

        private string convertName(string text)
        {
            return text.Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue").Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("ß", "ss").Replace(" ", "_");
        }
    }

    public class ModulInfo
    {
        public string Key { get; set; }
        public string Lizenz { get; set; }
        public DateTime Datum { get; set; }
    }

    public class FieldInfo
    {
        public FieldInfo(string field, string value, string typ)
        {
            Field = field;
            Value = value;
            Typ = typ;
        }

        public string Field { get; set; }
        public string Value { get; set; }
        public string Typ { get; set; }
    }
}
