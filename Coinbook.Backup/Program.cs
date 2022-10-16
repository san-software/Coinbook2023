using Coinbook.Enumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coinbook.Model;
using Fclp;
using System.IO.MemoryMappedFiles;

namespace Coinbook.Backup
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// <Pareter>
        /// args[0] = Programmname
        /// args[1] = Datenpfad
        /// args[2] = BackupPfad zum Zusammenstellen der Backup-ZIP-Datei
        /// args[3] = Zielpfad der Backupdatei
        /// args[4] = Sprache
        /// args[5] = True = automatisches Backup ohne Abfragen - False = manuelles Backup mit Abfragen
        /// args[6} = Array mit zu sichernden Dateien
        /// args[7] = CloudBackup False oder True
        /// args[8] = Lizenznummer
        /// </Parameter>
        [STAThread]
        static void Main(string[] args)
        {
            string file = args[0];
            BackupModel model = SystemHelper.DeSerializeObject<BackupModel>(file);

            //MessageBox.Show(file);

            Helper.Files = model.Files;
            Helper.Program = model.Program;
            Helper.DataPath = model.DataPath;
            Helper.BackupPath = model.BackupPath;
            Helper.Language = model.Language;
            Helper.TargetPath = model.TargetPath;
            Helper.Lizenznummer = model.License;
            Helper.Automatic = model.AutomaticBackup;
            Helper.CloudBackup = model.Cloud;
            Helper.ABO=model.ABO;
            Helper.DownloadPath = model.DownloadPath;

            if (!string.IsNullOrEmpty(Helper.ABO))
            {
                var temp = Helper.ABO.Split('|');
                Helper.Von = temp[1];
                Helper.Bis = temp[2];
                Helper.Active = (DateTime.Now >= Convert.ToDateTime(Helper.Von) && DateTime.Now <= Convert.ToDateTime(Helper.Bis));

                //MessageBox.Show(Helper.Von);
            }

            Application.SetCompatibleTextRenderingDefault(false);

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook.Backup");
            LanguageHelper.CreateLocalization(resourcePath);
            LanguageHelper.Localization.UpdateLanguage(Helper.Language);


            if (!Helper.Automatic && !Helper.CloudBackup)
                Application.Run(new frmMain());
            else
            {
                if (Helper.Automatic)
                {
                    file = Helper.AutomaticBackup(Helper.TargetPath, null);
                }

                if (Helper.CloudBackup)
                {
                    file = Helper.AutomaticBackup(Helper.DownloadPath, null);
                    Helper.Upload(file);
                }
            }
        }
    }
}
