using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace Coinbook.Backup
{
    public partial class frmDBRestore : Form
    {
        private string fileName = String.Empty;
        private List<string> fileList = new List<string>();

        public frmDBRestore()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);
        }

        public new void ShowDialog()
        {
            base.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dlgOpen.InitialDirectory = Helper.TargetPath;
            dlgOpen.FileName = "*.zip";
            dlgOpen.Title = LanguageHelper.Localization.GetTranslation(Name, "msgRestore");

            if (dlgOpen.ShowDialog() != DialogResult.Cancel)
            {
                Helper.Restore(dlgOpen.FileName);

                MessageBoxAdv.Show(LanguageHelper.Localization.GetTranslation(Name, "msgOk"), Application.ProductName);
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

