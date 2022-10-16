using SAN.FTP;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coinbook.Backup
{
    internal static class Helper
    {
        public static string Program {get;set;}
        public static string DataPath { get; set; }
        public static string BackupPath { get; set; }
        public static string TargetPath { get; set; }
        public static string DownloadPath { get; set; }
        public static string Language { get; set; } = "de";
        public static bool Automatic { get; set; } = false;
        public static bool CloudBackup { get; set; } = false;
        public static string[] Files { get; set; }
        public static string ABO { get; set; }
        public static string Lizenznummer { get; set; }
        public static string Von { get; set; }
        public static string Bis { get; set; }
        public static bool Active { get; set; } = false;

        public static string AutomaticBackup(string targetPath, IWin32Window owner)
        {
            List<string> files = new List<string>();

            if (!Directory.Exists(Helper.BackupPath))
                Directory.CreateDirectory(Helper.BackupPath);

            foreach (string file in Helper.Files)
                files.Add(Path.Combine(Helper.DataPath, file));

            string[] delete = Directory.GetFiles(Helper.BackupPath, "*.*");
            foreach (string file in delete)
                File.Delete(file);

            foreach (string file in files)
                File.Copy(file, Path.Combine(Helper.BackupPath, Path.GetFileName(file)));

            string zipfile = Path.Combine(targetPath, $"{Helper.Program}-Backup-" + DateTime.Now.ToString("yyyyMMdd") + ".zip");

            if (File.Exists(zipfile))
                File.Delete(zipfile);

            ZipFile.CreateFromDirectory(Helper.BackupPath, zipfile);

            return zipfile;
        }

        public static void Restore(string file)
        {
            using (ZipArchive archive = ZipFile.OpenRead(file))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (File.Exists(Path.Combine(Helper.DataPath, entry.Name)))
                        File.Delete(Path.Combine(Helper.DataPath, entry.Name));
                }
            }

            ZipFile.ExtractToDirectory(file, Helper.DataPath);

            //MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation(Name, "msgOk"), Application.ProductName);
        }

        public static void Upload(string backupFile)
        {
            var result = false;

            FTPClass ftp = new FTPClass();
            if (ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1"))
            {
                ftp.SetWorkingDirectory("Backup");
                ftp.CreateDirectory(Lizenznummer);
                ftp.SetWorkingDirectory(Lizenznummer);

                result = ftp.Upload(backupFile, Path.GetFileName(backupFile));
                ftp.Disconnect();
            }

            File.Delete(backupFile);
        }

        public static string ObjectToString(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static object StringToObject(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
