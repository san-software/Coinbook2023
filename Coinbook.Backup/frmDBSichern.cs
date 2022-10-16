using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace Coinbook.Backup
{

    public partial class frmDBSichern : Form
    {
        private List<string> files = new List<string>();

        public frmDBSichern()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);
            txtPath.Text = Helper.TargetPath;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Helper.AutomaticBackup(txtPath.Text, this);

            MessageBox.Show("Datensixcherung wurde ausgeführt");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dlgPath.ShowDialog() != DialogResult.Cancel)
                txtPath.Text = dlgPath.SelectedPath;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
