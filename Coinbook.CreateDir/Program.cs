using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace Coinbook.CreateDir
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Coinbook");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Coinbook","Info");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Coinbook","Downloads");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Coinbook","Updater");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Coinbook","Bilder");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Coinbook", "Backup");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
