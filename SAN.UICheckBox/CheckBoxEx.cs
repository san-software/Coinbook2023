using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace SAN.Control
{
	/// <summary>
	/// Summary description for ComboBoxEx.
	/// </summary>
	public class CheckBoxEx : System.Windows.Forms.CheckBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;
		private MenuGlyph typChecked = MenuGlyph.Checkmark;
		private MenuGlyph typIndeterminate = MenuGlyph.Bullet;
		private Color backColor = Color.White;
		private Color foreColor = Color.Black;

		public CheckBoxEx()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			BackColorCheck = Color.White;
			ForeColorCheck =Color.Black;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
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

		#region Properties

		//public new bool Enabled
		//{
		//  get {return base.Enabled;}
		//  set 
		//  {
		//    if (Recht != RechteHelper.Recht.Write)
		//      value = false;
		//    base.Enabled = value;
		//  }
		//}

		public bool ReadOnly
		{
			get;
			set;
		}
		#endregion

		protected override void OnClick(EventArgs e)  
		{
			if(!ReadOnly)
				base.OnClick(e);

			Changed = true;
		}

		[Browsable(false)]
		public bool Changed
		{
			get;
			set;
		}

		public new bool Checked
		{
			get
			{
				return base.Checked;
			}

			set
			{
				base.Checked = value;
				Changed = false;
			}
		}

		//Hintergrundfarbe für das Häckchen
		public Color BackColorCheck
		{
			get
			{
				return backColor;
			}
			set
			{
				backColor = value;
			}
		}

		//Vordergrundfarbe für das Häckchen
		public Color ForeColorCheck
		{
			get
			{
				return foreColor;
			}
			set
			{
				foreColor = value;
			}
		}

		//Typ für das Checked-Häckchen
		public MenuGlyph TypChecked
		{
			get
			{
				return typChecked;
			}
			set
			{
				typChecked = value;
			}
		}

		//Typ für das Indeterminate-Häckchen
		public MenuGlyph TypIndeterminate
		{
			get
			{
				return typIndeterminate;
			}
			set
			{
				typIndeterminate = value;
			}
		}

		public int Row
		{
			get;
			set;
		}

		public int Column
		{
			get;
			set;
		}

		#region Events

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			BackColorCheck =Color.White;
			ForeColorCheck = Color.Black;

			if (BackColorCheck != Color.White)
			{
				Rectangle rect = new Rectangle(new Point(this.ClientRectangle.X, this.ClientRectangle.Y), new Size(10, 10));
				e.Graphics.FillRectangle(new SolidBrush(BackColorCheck), rect.X + 2, rect.Y + 3, rect.Width - 1, rect.Height - 1);

				switch (CheckState)
				{
					case CheckState.Checked:
						ControlPaint.DrawMenuGlyph(e.Graphics, rect.X, rect.Y, rect.Width + 4, rect.Height + 4, typChecked, 
							ForeColorCheck, Color.Transparent);
						break;

					case CheckState.Indeterminate:
						ControlPaint.DrawMenuGlyph(e.Graphics, rect.X, rect.Y, rect.Width + 4, rect.Height + 4, typIndeterminate, 
							ForeColorCheck, Color.Transparent);
						break;
				}
			}
		}
		#endregion


	}
}