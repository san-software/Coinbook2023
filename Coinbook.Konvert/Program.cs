using System;
using System.Windows.Forms;
using System.Data;
using OleDB;

namespace Coinbook.Konvert
{
  /// <summary>
  /// Class with program entry point.
  /// </summary>
  static class Program
  {
    /// <summary>
    /// Program entry point.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      OleDBConnection.Application = "Coinbook";
			DBConnect result = OleDBConnection.Init;

      if (result == DBConnect.OK)
        Application.Run(new frmKonvert());

      //MessageBoxAdv.Show("Das Programm wird beendet", "", MessageBoxButtons.OK);
    }

    private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
      //frmException frm = new frmException(e);
      //frm.ShowDialog();
    }

  }
}