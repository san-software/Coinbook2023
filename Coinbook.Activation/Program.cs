using Coinbook.Enumerations;
using Coinbook.Helper;
using Coinbook.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coinbook.Activation
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            enmPrograms parameter = (enmPrograms)Enum.Parse(typeof(enmPrograms), args[0]);

            Settings settings = DatabaseHelper.LiteDatabase.ReadSettings();
            string sprache = settings.Culture.Substring(0, 2);

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook.Activation");
            LanguageHelper.CreateLocalization(resourcePath);
            LanguageHelper.Localization.UpdateLanguage(sprache);

            switch (parameter)
            {
                case enmPrograms.Aktivierung:
                    frmAktivierung form = new frmAktivierung();
                    form.Aktivierungsart = enmAktivierungsArt.Initial;
                    form.Grund = String.Empty;
                    //ArchivHelper.DataPath = DatabaseHelper.LiteDatabase.DataPath;
                    Application.Run(form);
                    break;

                case enmPrograms.NoLicense:
                    enmAktivierungsArt typ = (enmAktivierungsArt)Enum.Parse(typeof(enmAktivierungsArt), args[1]);
                    frmNoLicense form1 = new frmNoLicense();
                    form1.Art = typ;
                    //form1.Art = enmAktivierungsArt.Wrong;
                    Application.Run(form1);
                    break;
            }
        }
    }
}
