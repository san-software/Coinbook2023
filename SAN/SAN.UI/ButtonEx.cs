using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SAN.UI.Controls
{
	/// <summary>
	/// Summary description for ComboBoxEx.
	/// </summary>
	public class ButtonEx : System.Windows.Forms.Button
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;

		public ButtonEx()
		{
			InitializeComponent();

			BackColor = Farbverwaltung.ButtonBackColor;
			ForeColor = Farbverwaltung.ButtonForeColor;
			ImageStretch = false;
			Status = ButtonStatus.Nothing;
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

		//public new MenueHelperImage ImageIndex
		//{
		//  set
		//  {
		//    base.ImageIndex = (int)value;
		//  }
		//}

		[Category("Appearance")]
		public bool ImageStretch
		{
			get;
			set;
		}

		#endregion

		protected override void OnPaint(PaintEventArgs pevent)
		{
			if (Helper.IsDesignMode(this))
			{
				base.OnPaint(pevent);
				return;
			}

			Size size = pevent.ClipRectangle.Size;

			if (ImageStretch)
			{
				size.Height = Size.Height - 8;
				size.Width = Size.Width - 9;

				if (!Stretched)
				{
					if (base.ImageIndex != -1)
					{
						if (ImageList != null)
							Image = resizeImage(ImageList.Images[base.ImageIndex], size);
						Stretched = true;
					}
					else if (base.ImageKey != "")
					{
						if (ImageList != null)
							Image = resizeImage(ImageList.Images[ImageKey], size);
						Stretched = true;
					}
					else if (Image != null)
					{
						Image = resizeImage(Image, size);
						Stretched = true;
					}
				}
			}

			base.OnPaint(pevent);
		}

		private Image resizeImage(Image imgToResize, Size size)
		{
			int sourceWidth = imgToResize.Width;
			int sourceHeight = imgToResize.Height;

			float nPercent = 0;
			float nPercentW = 0;
			float nPercentH = 0;

			nPercentW = ((float)size.Width / (float)sourceWidth);
			nPercentH = ((float)size.Height / (float)sourceHeight);

			if (nPercentH < nPercentW)
				nPercent = nPercentH;
			else
				nPercent = nPercentW;

			int destWidth = (int)(sourceWidth * nPercent);
			int destHeight = (int)(sourceHeight * nPercent);

			Bitmap b = new Bitmap(destWidth, destHeight);
			Graphics g = Graphics.FromImage((Image)b);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;

			g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
			g.Dispose();

			return (Image)b;
		}

		public ButtonStatus Status
		{
			get;
			set;
		}

		public bool Stretched
		{
			get;
			set;
		}
	}
}