using Coinbook.Enumerations;
using System;
using System.IO;
using System.Windows.Forms;

namespace Coinbook.Import
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

			if (args.Length == 0)
			{
				try
				{
					if (!File.Exists(@"c:\Programdata\Coinbook\Coinbook.db"))
						Application.Run(new frmImportCoinbook30());
				}
				catch (Exception ex)
				{
					var x = ex;
				}
			}
			else
			{
				switch (args[0].ToString())
				{
					case "Import":
						{
							frmImport form = new frmImport("DE");
							form.ShowDialog();
						}
						break;

					case "Import2":
						{
							frmImport2 form = new frmImport2("DE");
							form.ShowDialog();
						}
						break;

					case "Import3":
						{
							frmImportCoinbook30 form = new frmImportCoinbook30(false);
							form.ShowDialog();
						}
						break;
				}
			}
		}
	}
}
