using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Coinbook
{
	public partial class frmMessage : Form
	{
		ModulImport modulImport;

		public frmMessage()
		{
			InitializeComponent();
		}

		public void Show(ModulImport modulImport)
		{
			this.modulImport = modulImport;

			bgw.RunWorkerAsync();

			ShowDialog();
		}

		private void bgw_DoWork(object sender, DoWorkEventArgs e)
		{
			do
			{
				Thread.Sleep(10000);
			} while (modulImport.IsRunning);
		}

		private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Close();
		}
	}
}
