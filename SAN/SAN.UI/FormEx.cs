using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAN.UI.Controls
{
	public partial class FormEx: Form
	{
		protected bool noMDI = false;
		private string caption = "";

		public FormEx()
		{
			InitializeComponent();
			this.BackColor = Farbverwaltung.BackColor;

			MinimizeBox = false;
			MaximizeBox = false;

			Cursor = Cursors.Default;
		}

		//protected override void OnClosed(EventArgs e)
		//{
		//  if (Parent != null)
		//  {
		//    if (MenueHelper.AskSave == DialogResult.Yes)
		//      Save(null, new EventArgs());

		//  }

		//  base.OnClosed(e);
		//}

		protected override void OnLoad(EventArgs e)
		{
			WindowState = FormWindowState.Maximized;

			if (!Helper.IsDesignMode(this))
			{
				if (!noMDI)
				{
					//if (Parent != null)
					//{
					//  //Size = new Size(MenueHelper.MDIClientSize.Width + 3, MenueHelper.MDIClientSize.Height);
					//  Left = -3;
					//  Top = 0;
					//}
				}
			}

			base.OnLoad(e);
		}

		//"Disable the 'X'"
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				const int CS_NOCLOSE = 0x200;

				if (Text != "")
					if (Parent != null)
						cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;		//"Disable the 'X'"

				if (!Application.StartupPath.EndsWith(@"\Common7\IDE"))
					cp.ExStyle |= 0x02000000;   //Flicker free Form Painting

				return cp;
			}
		}

		public string Caption
		{
			set
			{
				//MenueHelper.Text = value;
				caption = value;
			}
			get
			{
				return caption;
			}
		}

		public bool DontRestoreToolbar
		{
			get;
			set;
		}

		public virtual void ExcelExport(object sender, EventArgs e)
		{
		}

		public virtual void Print(object sender, EventArgs e)
		{
		}

		public virtual void Preview(object sender, EventArgs e)
		{
		}

		public virtual void PDF(object sender, EventArgs e)
		{
		}

		public virtual void Filter(object sender, ToolBarButtonClickEventArgs e)
		{
		}

		public virtual void Umschlag(object sender, EventArgs e)
		{
		}

	}
}
