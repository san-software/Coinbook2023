using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using SAN.UI.Menue;

namespace SAN.UI.Controls
{
	public partial class SubFormEx: Form
	{
		protected bool noMDI = false;
		private string caption = "";

		public SubFormEx()
		{
			InitializeComponent();
			this.BackColor = Farbverwaltung.BackColor;

			if (!Application.StartupPath.EndsWith(@"\Common7\IDE"))
			{
				if (Parent == null)
					initEvents();
			}

			MinimizeBox = false;
			MaximizeBox = false;
		}

		private void initEvents()
		{
			MenueHelper.New += new EventHandler(New);
			MenueHelper.Save += new EventHandler(Save);
			MenueHelper.Undo += new EventHandler(Undo);
			MenueHelper.Filter += new ToolBarButtonClickEventHandler(Filter);
			MenueHelper.Print += new EventHandler(Print);
			MenueHelper.Preview += new EventHandler(Preview);
			MenueHelper.Delete += new EventHandler(Delete);
			MenueHelper.PDF += new EventHandler(PDF);
			MenueHelper.Powerpoint += new EventHandler(Powerpoint);
			MenueHelper.ExcelExport += new EventHandler(ExcelExport);
			MenueHelper.FormClose += new EventHandler(MenueHelper_FormClose);
		}

		public void DisposeEvents()
		{
			MenueHelper.New -= new EventHandler(New);
			MenueHelper.Save -= new EventHandler(Save);
			MenueHelper.Undo -= new EventHandler(Undo);
			MenueHelper.Filter -= new ToolBarButtonClickEventHandler(Filter);
			MenueHelper.Print -= new EventHandler(Print);
			MenueHelper.Preview -= new EventHandler(Preview);
			MenueHelper.Delete -= new EventHandler(Delete);
			MenueHelper.PDF -= new EventHandler(PDF);
			MenueHelper.Powerpoint -= new EventHandler(Powerpoint);
			MenueHelper.ExcelExport -= new EventHandler(ExcelExport);
			MenueHelper.FormClose -= new EventHandler(MenueHelper_FormClose);
		}

		public virtual void Undo(object sender, EventArgs e)
		{
		}

		public virtual void Save(object sender, EventArgs e)
		{
		}

		public virtual void New(object sender, EventArgs e)
		{
		}

		public virtual void Print(object sender, EventArgs e)
		{
		}

		public virtual void Preview(object sender, EventArgs e)
		{
		}

		public virtual void Filter(object sender, ToolBarButtonClickEventArgs e)
		{
		}

		public virtual void Delete(object sender, EventArgs e)
		{
		}

		public virtual void PDF(object sender, EventArgs e)
		{
		}

		public virtual void Powerpoint(object sender, EventArgs e)
		{
		}

		public virtual void ExcelExport(object sender, EventArgs e)
		{
		}

		public virtual void MenueHelper_FormClose(object sender, EventArgs e)
		{
			if (CloseForm)
				Close();
			else
				Hide();
		}

		protected override void OnClosed(EventArgs e)
		{
			if (Parent != null)
			{
				MenueHelper.MenueFreeze(true);

				if (MenueHelper.AskSave == DialogResult.Yes)
					Save(null, new EventArgs());

				DisposeEvents();

				MenueHelper.ClickButton(MenueHelperButtons.Filter, false);
				MenueHelper.Statusbar.RecordsClear();
				MenueHelper.BusyOff();
			}

			base.OnClosed(e);
			MenueHelper.Text = "";
		}

		//protected override void OnLoad(EventArgs e)
		//{
		//  WindowState = FormWindowState.Normal;

		//  if (!Helper.IsDesignMode(this))
		//  {
		//    if (!noMDI)
		//    {
		//      if (Parent != null)
		//      {
		//        //Size = new Size(MenueHelper.MDIClientSize.Width + 3, MenueHelper.MDIClientSize.Height);
		//        //Left = -3;
		//        //Top = 0;
		//      }
		//    }
		//  }

		//  base.OnLoad(e);
		//}

		//"Disable the 'X'"
		//protected override CreateParams CreateParams
		//{
		//  get
		//  {
		//    CreateParams cp = base.CreateParams;
		//    const int CS_NOCLOSE = 0x200;

		//    if (Text != "")
		//      if (Parent != null)
		//        cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;		//"Disable the 'X'"

		//    if (!Application.StartupPath.EndsWith(@"\Common7\IDE"))
		//      cp.ExStyle |= 0x02000000;   //Flicker free Form Painting

		//    return cp;
		//  }
		//}

		public void EnableSaveButton(bool enable)
		{
			MenueHelper.Enabled(MenueHelperButtons.Save, enable);
			MenueHelper.Enabled(MenueHelperButtons.Undo, enable);
			Application.DoEvents();
		}

		public string Caption
		{
			set
			{
				MenueHelper.Text = value;
				caption = value;
			}
			get
			{
				return caption;
			}
		}

		// Wenn true --> Form wird bei Menübutton "Close" geschlossen 
		// Wenn false --> Form wird bei Menübutton "Close" nur ausgeblendet (Hidden)
		public bool CloseForm
		{
			get;
			set;
		}
	}
}
