using System;
using System.Windows.Forms;
using System.IO;
using Coinbook.Helper;

namespace Coinbook
{
  public partial class frmLizenz : Form
  {
    public frmLizenz()
    {
      InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);
			lblAnzeige.Text = String.Empty;
      btnOK.Enabled = false;
    }

    public string Lizenzdatei { get; set; }

    private void btnOK_Click(object sender, EventArgs e)
    {
      Cursor = Cursors.WaitCursor;
      bool result =false;
      pgbBar.Style = ProgressBarStyle.Marquee;

      LizenzVerwaltung l = new LizenzVerwaltung();
      l.URL = "http://www.Coinbook.de/Downloads/Personalisierung/" + txtLizenz.Text;
      l.Lizenzdatei = Path.Combine(CoinbookHelper.DataPath, "Modul.lic");

      if (l.FileExists)
        result = l.LoadLizenz;

      if (result)
      {
        lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgInstalled");

        btnOK.Enabled = false;
      }
      else
      {
        lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgNoLizense");
      }

      pgbBar.Style = ProgressBarStyle.Blocks;

      btnWeiter.Enabled = true;
      Cursor = Cursors.Default;
    }

    private void txtLizenz_TextChanged(object sender, EventArgs e)
    {
      btnOK.Enabled = (txtLizenz.Text.Length == 36);
    }

    private void btnWeiter_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Environment.Exit(1);
    }

  }
}
