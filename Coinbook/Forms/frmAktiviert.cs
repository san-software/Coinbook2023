using Coinbook.Lokalisierung;
using Splash;
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
  public partial class frmAktiviert : Form
  {
    public frmAktiviert()
    {
      InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

      SplashScreen.CloseForm();
			txtAnzeige.Text = LanguageHelper.Localization.GetTranslation("Keys", "activated");
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      Close();
    }

   }
}
