using System;
using System.Windows.Forms;

namespace Coinbook.Backup
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);
        }

        public new void ShowDialog(IWin32Window window)
        {

            lblVersion.Text = Application.ProductVersion;
            lblVon.Text = Helper.Von;
            lblBis.Text = Helper.Bis;
            lblLizenz.Text = Helper.Active 
                                ? LanguageHelper.Localization.GetTranslation(Name, "msgAbonementAktiv") 
                                : LanguageHelper.Localization.GetTranslation(Name, "msgAbonementInaktiv");

            lblCopyright.Text = "Copyright © Coinbook-Verlag 2022";

            base.ShowDialog(window);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
