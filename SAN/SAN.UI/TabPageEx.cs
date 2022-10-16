using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace SAN.UI.Controls
{
	/// <summary>
	/// Summary description for TextBoxEx.
	/// </summary>
	public class TabPageEx : System.Windows.Forms.TabPage
	{
		private System.ComponentModel.Container components = null;
		private bool enabled = true;
		private bool visible = true;

		public TabPageEx()
		{
			InitializeComponent();
			BackColor = Farbverwaltung.BackColor;
			//BackColor = Color.Transparent;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		public new bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
				Parent.Invalidate();
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			if (BackgroundImage != null)
				base.OnPaintBackground(e);
			else
			{
				RectangleF r = e.Graphics.VisibleClipBounds;
				Brush b = new SolidBrush(BackColor);

				e.Graphics.FillRectangle(b, r.X, r.Y, r.Width, r.Height);
			}
			
			//base.OnPaintBackground(e);
		}

		public new bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;
			}
		}
	}
}

