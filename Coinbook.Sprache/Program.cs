using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Coinbook.Database;
using Coinbook.Helper;

namespace Coinbook.Sprache
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// 

        [STAThread]
        static void Main(string[] args)
        {
            string sprache = "";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CoinbookHelper.Settings = DatabaseHelper.LiteDatabase.ReadSettings();

            string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook");

            CoinbookHelper.DataPath = DatabaseHelper.LiteDatabase.DataPath;

            sprache = "DE";
            if (args.Length == 1)
                sprache = "DE";

            frmProgress form = new frmProgress(sprache);
            form.ShowDialog();
            form.Close();
        }
    }
}
