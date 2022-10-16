using SAN.FTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coinbook.Backup
{
    public partial class frmCloudRestore : Form
    {
        public frmCloudRestore()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);

            FTPClass ftp = new FTPClass();
            if (ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1"))
            {
                ftp.SetWorkingDirectory("Backup");
                var files = ftp.Files(Helper.Lizenznummer, ".*");
                lstBackups.DataSource = files;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            restore();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstBackups_DoubleClick(object sender, EventArgs e)
        {
            restore();
        }

        private void restore()
        {
            FTPClass ftp = new FTPClass();
            if (ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1"))
            {
                ftp.SetWorkingDirectory("Backup");
                ftp.SetWorkingDirectory(Helper.Lizenznummer);
                var result = ftp.Download(lstBackups.SelectedItem.ToString(), Path.Combine(Helper.BackupPath, lstBackups.SelectedItem.ToString()));

                if (result == enmFTPFile.FileDownloadOK)
                    Helper.Restore(Path.Combine(Helper.BackupPath, lstBackups.SelectedItem.ToString()));

                File.Delete(Path.Combine(Helper.BackupPath, lstBackups.SelectedItem.ToString()));
            }
        }
    }
}
