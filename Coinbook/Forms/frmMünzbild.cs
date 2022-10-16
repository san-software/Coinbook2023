using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Coinbook.Lokalisierung;

namespace Coinbook
{
    public partial class frmMünzbild : Form
    {
        public frmMünzbild()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public String Bild { get; set; }
        public String Münze { get; set; }
        public bool IsOwnPicture
        {
            set
            {
                lblCopyright.Visible = !value;
            }
        }

        public new void ShowDialog(IWin32Window window)
        {
            if (File.Exists(Bild))
            {
                picMünze.Image = new Bitmap(Bild);

                var copyright = ImageHelper.GetFullEXIF(Bild);

                if (copyright.Count == 1)
                    lblCopyright.Text = LanguageHelper.Localization.GetTranslation("frmMuenzdetails", "lblCopyright");
                else
                    lblCopyright.Text = "Copyright: " + copyright[1].data;
            }

            lblAnzeige.Text = Münze;

            base.ShowDialog();
        }
    }
}
