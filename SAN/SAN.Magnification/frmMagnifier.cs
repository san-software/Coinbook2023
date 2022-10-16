using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAN.Magnifier
{
	public partial class frmMagnifier : Form
	{
		public frmMagnifier()
		{
			InitializeComponent();

			Left = MagnifierHelper.Config.LocationX;
			Top = MagnifierHelper.Config.LocationY;
			Height = MagnifierHelper.Config.MagnifierHeight;
			Width = MagnifierHelper.Config.MagnifierWidth;
		}

		public void Save()
		{
				MagnifierHelper.Config.LocationX = Left;
				MagnifierHelper.Config.LocationY = Top;
				MagnifierHelper.Config.MagnifierHeight = Height;
				MagnifierHelper.Config.MagnifierWidth = Width;

				MagnifierHelper.SaveConfiguration();
		}

		public new void Show()
		{
			Left = MagnifierHelper.Config.LocationX;
			Top = MagnifierHelper.Config.LocationY;
			Height = MagnifierHelper.Config.MagnifierHeight;
			Width = MagnifierHelper.Config.MagnifierWidth;

			base.Show();
		}

		private void frmMagnifier_FormClosing(object sender, FormClosingEventArgs e)
		{
			Save();
		}
	}
}
