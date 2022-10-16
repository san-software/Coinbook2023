using Coinbook.Lokalisierung;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Coinbook
{
    public partial class frmPDF : Form
    {
        public frmPDF()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);

            //string file = string.Format("Coinbook-{0}.pdf", LanguageHelper.Localization.Language);

            //pdfViewer.Load(file);
            //pdfViewer.ToolbarSettings.OpenButton.IsVisible = false;
            //pdfViewer.ToolbarSettings.SaveButton.IsVisible = false;
        }

        public void ShowDialog(string file)
        {
            //string file = string.Format("Coinbook-{0}.pdf", LanguageHelper.Localization.Language);

            pdfViewer.Load(file);
            pdfViewer.ToolbarSettings.OpenButton.IsVisible = false;
            pdfViewer.ToolbarSettings.SaveButton.IsVisible = false;

            base.ShowDialog();
        }

        public new void ShowDialog(IWin32Window owner)
        {
            string file = string.Format("Coinbook-{0}.pdf", LanguageHelper.Localization.Language);

            pdfViewer.Load(file);
            pdfViewer.ToolbarSettings.OpenButton.IsVisible = false;
            pdfViewer.ToolbarSettings.SaveButton.IsVisible = false;

            base.ShowDialog(owner);
        }
    }
}