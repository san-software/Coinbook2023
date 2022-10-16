using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Coinbook.Backup
{

    public partial class frmCloudBackup : Form
    {
        public frmCloudBackup()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);
        }

        public string UpdatePath { get; set; }
        public string BackupPath { get; set; }
        public string Lizenznummer { get; set; }

        public new void ShowDialog(IWin32Window window)
        {
            base.ShowDialog(window);
        }

        private void frmCloudBackup_Shown(object sender, EventArgs e)
        {
            if (MessageBox.Show(LanguageHelper.Localization.GetTranslation(Name, "msgStartCloud"), Helper.Program, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                prgBar.Style = ProgressBarStyle.Marquee;
                prgBar.MarqueeAnimationSpeed = 30;

                bgwWork.RunWorkerAsync();
            }
            else
                Close();
        }

        private void bgwWork_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string file = Helper.AutomaticBackup(Helper.DownloadPath, this);
            Helper.Upload(file);
        }

        private void bgwWork_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }
}
