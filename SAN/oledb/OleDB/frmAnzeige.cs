using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OleDB
{
	public partial class frmAnzeige : Form
	{
		public frmAnzeige()
		{
			InitializeComponent();
		}

		public string Anzeige
		{
			set
			{
				label1.Text = value;
				Application.DoEvents();
			}
		}
	}
}
