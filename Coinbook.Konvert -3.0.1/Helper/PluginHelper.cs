using SAN.FTP;
using SAN.Plugin;
using System;
using System.IO;
using System.Windows.Forms;
using Coinbook.Helper;

namespace Coinbook
{
    public static class PluginHelper
    {
        public static AvailablePlugins Plugins { get; set; }

        public static void CheckPlugins(string license)
        {
            if (!string.IsNullOrEmpty(license))
            {
                var temp = license.Split('|')[0];
                if (!File.Exists(Path.Combine(Application.StartupPath, "Plugins", temp + ".dll")))
                    downloadPlugin(temp);
                else if (pluginNewVersion(temp))
                {
                    File.Delete(Path.Combine(Application.StartupPath, "Plugins", temp + ".dll"));
                    downloadPlugin(temp);
                }

                LoadPlugins();
            }
        }

        public static void LoadPlugins()
        {
            Plugins = new PluginServices(Path.Combine(Application.StartupPath, "Plugins")).AvailablePlugins;
            //Plugins = new PluginServices(Application.StartupPath).AvailablePlugins;

            //Make sure there's a selected Plugin
            foreach (AvailablePlugin selectedPlugin in Plugins)
            {
                selectedPlugin.Instance.BackupPath = CoinbookHelper.BackupPath;
                selectedPlugin.Instance.UpdatePath = CoinbookHelper.UpdatePath;
                selectedPlugin.Instance.Programm = Application.ProductName;
                selectedPlugin.Instance.Lizenz = CoinbookHelper.Settings.Lizenzkey;
                selectedPlugin.Instance.DataPath = CoinbookHelper.DataPath;
                selectedPlugin.Instance.Initialize(CoinbookHelper.Abo);
            }
        }

        private static bool downloadPlugin(string plugin)
        {
            //FTPClass ftp = new FTPClass();

            //string file = plugin +".dll";
            //var target = Path.Combine(Application.StartupPath, "Plugins", file);
            //var modifydate = Convert.ToDateTime("01.01.1900");
            //if (File.Exists(target))
            //    modifydate = File.GetCreationTime(target);
            //enmFTPFile result = ftp.Download("Downloads/Coinbook/" + file, target, modifydate);

            //return (result == enmFTPFile.FileDownloadOK);
            return false;
        }

        private static bool pluginNewVersion(string plugin)
        {
            return false;
        }
    }
}
