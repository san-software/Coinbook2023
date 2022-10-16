using Coinbook.Enumerations;
using Coinbook.Helper;
using Coinbook.Lokalisierung;
using Coinbook.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coinbook.Modulverwaltung
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CoinbookHelper.Settings = DatabaseHelper.LiteDatabase.ReadSettings();

            enmPrograms parameter = (enmPrograms)Enum.Parse(typeof(enmPrograms), args[0]);

            Settings settings = DatabaseHelper.LiteDatabase.ReadSettings();
            string sprache = settings.Culture.Substring(0, 2);

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook.ModulVerwaltung");
            LanguageHelper.CreateLocalization(resourcePath);
            LanguageHelper.Localization.UpdateLanguage(sprache);

            switch (parameter)
            {
                case enmPrograms.ModulImport:
                    ArchivHelper.DataPath = DatabaseHelper.LiteDatabase.DataPath; 
                    Application.Run(new frmModulImport());
                    Environment.ExitCode = 0;
                    break;

                case enmPrograms.ModulBestellung:
                    Application.Run(new frmOrder());
                    break;

                case enmPrograms.AboBestellung:
                    Application.Run(new frmOrderCloudBackup(settings));
                    break;
            }
        }
    }
}
