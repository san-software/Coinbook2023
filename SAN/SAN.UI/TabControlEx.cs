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
	public class TabControlEx : System.Windows.Forms.TabControl
	{
		private System.ComponentModel.Container components = null;
		private Color foreColorActive = Color.Black;
		private Color backColorActive = Color.DarkGray;
		private Color backColor = Color.Beige;
		private HorizontalAlignment hAlignment = HorizontalAlignment.Center;
		private VerticalAlignment vAlignment = VerticalAlignment.Top;

		public TabControlEx()
		{
			base.DrawMode = TabDrawMode.OwnerDrawFixed;

			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
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

		#region Tabreiter Zeichnen
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			Font font = null;
			Brush backBrush;			//For background color
			Brush foreBrush = null;			//For forground color
			int w = 0;
			int h = 0;
			Rectangle r = e.Bounds;
			Rectangle bounds = e.Bounds;
			string[] separator = new String[] { "\n" };
			string[] text = base.TabPages[e.Index].Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

			TabPageEx t = base.TabPages[e.Index] as TabPageEx;
			if (t != null && !t.Visible)
				text = new string[1];

			if (text.Length == 0)
				text = new String[1];

  		switch (this.Alignment)
			{
				case TabAlignment.Top:
					r = new Rectangle(bounds.X, 4, bounds.Width, this.ItemSize.Height - 4);
					break;
				case TabAlignment.Left:
					r = new Rectangle(4, bounds.Y + 2, bounds.Width-4, this.ItemSize.Height - 4);
					break;
				case TabAlignment.Right:
					//bounds.Width = bounds.Width - 3;
					r = new Rectangle(bounds.X, bounds.Y, bounds.Width, this.ItemSize.Height - 4);
					break;

				case TabAlignment.Bottom:
					break;
			}

			//This construct will help you to deside which tab page have current focus to change the style.
			backBrush = new System.Drawing.SolidBrush(backColor);
			foreBrush = new SolidBrush(e.ForeColor);
			font = new Font(e.Font.FontFamily, 8);

			if (e.Index == base.SelectedIndex)
			{
				//font = new Font(e.Font.FontFamily, 8, FontStyle.Bold);
				backBrush = new System.Drawing.SolidBrush(backColorActive);
				//backBrush = new System.Drawing.SolidBrush(Color.Red);
				foreBrush = new System.Drawing.SolidBrush(foreColorActive);
				//bounds.X = bounds.X + 3;
				//bounds.Y = bounds.Y + 3;
				//bounds.Width = bounds.Width - 6;
				//bounds.Height = bounds.Height - 6;

				bounds.X = bounds.X + 3;
				bounds.Y = bounds.Y + 4;
				bounds.Width = bounds.Width - 6;
				bounds.Height = bounds.Height - 6;

			}
			else if (t != null && !t.Visible)
			{
				backBrush = new System.Drawing.SolidBrush(Parent.BackColor);
				bounds.X = bounds.X - 1;
				bounds.Y = bounds.Y - 1;
				bounds.Width = bounds.Width + 3;
				bounds.Height = bounds.Height + 4;
			}

			// Wenn Tab disabled dann ausgrauen
			if (t != null && !t.Enabled)
				foreBrush = new SolidBrush(Color.Gray);


			h = Convert.ToInt32(e.Graphics.MeasureString(text[0], font).Height) * text.Length;
			int left = bounds.Left;
			int width = r.Width;
			int top = bounds.Top;

			for (int row = 0; row < text.Length; row++)
			{
				w = Convert.ToInt32(e.Graphics.MeasureString(text[row], font).Width);

				switch (HAlignment)
				{
					case HorizontalAlignment.Center:
						r.X = (width - w) / 2;
						r.Width = bounds.Width - r.X+2;
						r.X = r.X + left;
						break;
					case HorizontalAlignment.Left:
						r.X = left;
						r.Width = bounds.Width;
						break;
					case HorizontalAlignment.Right:
						if (w > width)
							r.Width = width;
						else
							r.Width = w+2;

						r.X = left + width - w;
						break;
				}

				switch (VAlignment)
				{
					case VerticalAlignment.Top:
						r.Y=top + 2;
						r.Height= bounds.Height;
						break;
					case VerticalAlignment.Center:
						r.Y =(bounds.Height - h) / 2;
						r.Height = bounds.Height - r.Y + 4;
						r.Y = r.Y + top;
						break;
					case VerticalAlignment.Bottom:
						r.Y = top + bounds.Height - h;
						r.Height = h + 2;
						break;
				}

				r.Y = r.Y + (h / text.Length)* row;

				if (e.Index == base.SelectedIndex)
					r.Y--;

				//This will help you to fill the interior portion of selected tabpage.
				e.Graphics.FillRectangle(backBrush, bounds);
				e.Graphics.DrawString(text[row], font, foreBrush, r);
				Application.DoEvents();

			}

			if(e.Index == base.SelectedIndex)
			{
				font.Dispose();
				backBrush.Dispose();
			}
			else
			{
				backBrush.Dispose();
				foreBrush.Dispose();
			}
		}
		#endregion

		#region Properties
		[Category("Behavior")]
		public Color TabForeColorActive
		{
			get {return foreColorActive;}
			set {foreColorActive = value;}
		}

		[Category("Behavior")]
		public Color TabBackColorActive
		{
			get {return backColorActive;}
			set {backColorActive = value;}
		}

		[Category("Behavior")]
		public Color TabBackColor
		{
			get {return backColor;}
			set {backColor = value;}
		}

		public new bool Enabled
		{
			get {return base.Enabled;}
			set
			{
				base.Enabled = value;
			}
		}

		[Category("Appearance")]
		public HorizontalAlignment HAlignment
		{
			get
			{
				return hAlignment;
			}
			set
			{
				hAlignment = value;
				Invalidate();
			}
		}

		[Category("Appearance")]
		public VerticalAlignment VAlignment
		{
			get
			{
				return vAlignment;
			}
			set
			{
				vAlignment = value;
				Invalidate();
			}
		}
		#endregion

		protected override void OnSelecting(TabControlCancelEventArgs e)
		{
			TabPageEx t = e.TabPage as TabPageEx;

			if (t != null && (!t.Enabled || !t.Visible))
				e.Cancel=true;

			e.TabPage.BackColor = Farbverwaltung.BackColor;

			base.OnSelecting(e);
		}

		public TabPageEx TabPage(string value)
		{
			return (TabPageEx)this.TabPages[value];
		}

		public TabPageEx TabPage(int value)
		{
			return (TabPageEx)this.TabPages[value];
		}

	}
}	
