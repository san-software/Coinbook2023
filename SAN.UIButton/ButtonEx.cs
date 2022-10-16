using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace SAN.Control
{
	/// <summary>
	/// Summary description for ComboBoxEx.
	/// </summary>
	public class ButtonEx : Button
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;

		public ButtonEx()
		{
			InitializeComponent();

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
            if (isDesignMode())
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

        //Prüft ob sich das Control gerade im designmode befindet
        private bool isDesignMode()
        {
			if (Application.ExecutablePath.IndexOf("devenv.exe", StringComparison.OrdinalIgnoreCase) > -1)
			{
				return true;
			}
			return false;


			//bool result = false;
   //         //Solange noch ein Control zum prüfen vorhanden ist
   //         while (this != null)
   //         {
   //             PropertyInfo siteProperty = this.GetType().GetProperty("Site");              //Die Site Eigenschaft des Controls auslesen

   //             //Falls die .Site Eigenschaft gefunden wurde
   //             if (siteProperty != null)
   //             {
   //                 ISite site = siteProperty.GetGetMethod().Invoke(this, new object[0]) as ISite;                   //Eigenschaftswert auslesen

   //                 //Falls eine Site Eigenschaft vorhanden ist
   //                 if (site != null)
   //                     //Wenn sich das Control im DesignMode befindet
   //                     if (site.DesignMode)
   //                         result = true;                          //Eins der Controls befindet sich noch im Design Mode
   //             }
   //             //control = Parent;               //Parent auslesen, und auch hier die .DesignMode überprüfen
   //         }

   //         return result;          //Kein Control befand sich im Designmode  ---> false
        }
    }
}