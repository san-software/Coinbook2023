using Coinbook.Enumerations;
using SAN.Converter;
using System;
using System.Windows.Forms;

namespace Coinbook.Reporting
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmReportingWert form = new frmReportingWert();
            form.Liste = (enmReportTyp)Enum.Parse(typeof(enmReportTyp), args[1].ToString());
            form.NationID = ConvertEx.ToInt32(args[2]);
            form.ShowDialog();
            //Application.Run(new frmReportingWert());
        }
    }
}
