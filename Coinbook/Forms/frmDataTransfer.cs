using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using Syncfusion.Windows.Forms.Tools;
using SAN.FTP;
using Syncfusion.Windows.Forms;
using Coinbook.Helper;

namespace Coinbook
{
    public partial class frmDataTransfer : SfForm
    {
        public frmDataTransfer()
        {
            InitializeComponent();

            ThemeName = "Office2016DarkGray";

            progressBarUpload.Visible = false;
            progressBarUpload.ProgressStyle = ProgressBarStyles.WaitingGradient;
            progressBarUpload.WaitingGradientEnabled = false;

            progressBarDownload.Visible = false;
            progressBarDownload.ProgressStyle = ProgressBarStyles.WaitingGradient;
            progressBarDownload.WaitingGradientEnabled = false;

            string text = "Hier können Sie Ihre Datenbank zur Überprüfung und evtl. Reperatur an den Coinbook-Verlag hochladen."
                + Environment.NewLine + "Bitte benutzen Sie diese Funktion nur in Rücksprache mit unserem Support." + Environment.NewLine 
                + Environment.NewLine + "Denken Sie daran, daß Sie bei einer Reperatur keine Eingaben machen dürfen, "
                + "da beim Downlad nach der Reperatur der Datenbestand auf den Stand vor der Reperatur zurückgesetzt wird.";
            txtUpload.Text = text;

            text = "Hier können Sie nach einer Reperatur der Datenbank durch den Support vom Coinbook-Verlag, "
                + "die reparierierte Datenbank wieder herunterladen. " + Environment.NewLine + Environment.NewLine
                + "Auch hier sollten Sie das nur in Rücksparche mit dem Support tun.";
            txtDownload.Text = text;

            btnDownload.Enabled = false;
            btnUpload.Enabled = false;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            progressBarUpload.Visible = true;
            progressBarUpload.WaitingGradientEnabled = true;
            Enabled = false;

            backgroundWorkerUpload.RunWorkerAsync();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            progressBarDownload.Visible = true;
            progressBarDownload.WaitingGradientEnabled = true;
            Enabled = false;

            backgroundWorkerDownload.RunWorkerAsync();
        }

        private void optUpload_CheckChanged(object sender, EventArgs e)
        {
            btnUpload.Enabled = optUpload.Checked;
        }

        private void optDownload_CheckChanged(object sender, EventArgs e)
        {
            btnDownload.Enabled = optDownload.Checked;
        }

        private void btbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void backgroundWorkerUpload_DoWork(object sender, DoWorkEventArgs e)
        {
            var files = Directory.GetFiles(CoinbookHelper.UpdatePath);

            for (int j = 0; j < files.Length; j++)
                File.Delete(files[j]);

            backgroundWorkerUpload.ReportProgress(0, @"Kopiere Datei C:\ProgramData\Coinbook\Coinbook.db");
            File.Copy(@"C:\ProgramData\Coinbook\Coinbook.db", Path.Combine(CoinbookHelper.UpdatePath, "Coinbook.db"));

            if (File.Exists(@"C:\ProgramData\Coinbook\Coinbook.mdb"))
            {
                backgroundWorkerUpload.ReportProgress(0, @"Kopiere Datei C:\ProgramData\Coinbook\Coinbook.mdb");
                File.Copy(@"C:\ProgramData\Coinbook\Coinbook.mdb", Path.Combine(CoinbookHelper.UpdatePath, "Coinbook.mdb"));
            }

            string zipfile = Path.Combine(CoinbookHelper.BackupPath, "Transfer-" + CoinbookHelper.Settings.Lizenzkey + ".zip");

            FastZip z = new FastZip();
            z.CreateZip(zipfile, CoinbookHelper.UpdatePath, false, String.Empty);

            for (int j = 0; j < files.Length; j++)
                File.Delete(files[j]);

            using (FTPClass ftp = new FTPClass())
            {
                if (ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1"))
                {
                    backgroundWorkerUpload.ReportProgress(0, "Upload läuft");

                    ftp.SetWorkingDirectory("Dateitransfer");
                    ftp.Upload(zipfile, Path.GetFileName(zipfile));
                    ftp.Disconnect();

                    e.Result = "Datei wurde transferiert";
                }
                else
                    e.Result = "Datei wurde nicht transferiert";
            }

            File.Delete(zipfile);
        }

        private void backgroundWorkerUpload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarUpload.Text = e.UserState.ToString();
        }

        private void backgroundWorkerUpload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarUpload.Visible = false;
            progressBarUpload.WaitingGradientEnabled = false;
            Enabled = true;

            MessageBoxAdv.Show(e.Result.ToString());
        }

        private void backgroundWorkerDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            var files = Directory.GetFiles(CoinbookHelper.DownloadPath);

            for (int j = 0; j < files.Length; j++)
                File.Delete(files[j]);

            string zipfile = Path.Combine(CoinbookHelper.DownloadPath, "Transfer-" + CoinbookHelper.Settings.Lizenzkey + ".zip");

            using (FTPClass ftp = new FTPClass())
            {
                if (ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1"))
                {
                    backgroundWorkerDownload.ReportProgress(0, "Download läuft");

                    ftp.SetWorkingDirectory("Dateitransfer");
                    ftp.Download(Path.GetFileName(zipfile), zipfile);
                    ftp.DeleteFile(Path.GetFileName(zipfile));
                    ftp.Disconnect();
                }
            }

            if (File.Exists(zipfile))
            {
                backgroundWorkerDownload.ReportProgress(0, "Dateien werden entpackt");

                FastZip z = new FastZip();
                string fileFilter = null;

                z.ExtractZip(zipfile, CoinbookHelper.UpdatePath, fileFilter);

                e.Result = "Datei wurde heruntergeladen";
            }
            else
                e.Result = "Keine Datei zum herunterladen gefunden";
        }

        private void backgroundWorkerDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarDownload.Text = e.UserState.ToString();
        }

        private void backgroundWorkerDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarDownload.Visible = false;
            progressBarDownload.WaitingGradientEnabled = false;
            Enabled = true;

            MessageBoxAdv.Show(e.Result.ToString());
        }
    }
}
