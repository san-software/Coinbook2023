using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coinbook.Backup
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);

            mnuCloudBackup.Enabled = Helper.Active;
            mnuCloudRestore.Enabled = Helper.Active;
        }

        private void mnuBackup_Click(object sender, EventArgs e)
        {
            frmDBSichern form = new frmDBSichern();
            form.ShowDialog(this);
        }

        private void mnuRestore_Click(object sender, EventArgs e)
        {
            frmDBRestore form = new frmDBRestore();
            form.ShowDialog(this);
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmAbout form = new frmAbout();
            form.ShowDialog(this);
        }

        private void mnuCloudBackup_Click(object sender, EventArgs e)
        {
            frmCloudBackup form = new frmCloudBackup(); 
            form.ShowDialog(this);
        }

        private void mnuCloudRestore_Click(object sender, EventArgs e)
        {
            frmCloudRestore form = new frmCloudRestore();
            form.ShowDialog(this);
        }
    }
}
