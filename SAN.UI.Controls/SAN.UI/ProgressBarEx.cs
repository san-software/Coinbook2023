using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SAN.UI.Controls
{
	public class ProgressBarEx : Control
	{
		#region "  Gradient Mode  "

		public enum GradientMode
		{
			None,
			Vertical,
			VerticalCenter,
			Horizontal,
			HorizontalCenter,
			Diagonal
		} 

		#endregion

		#region "  ProgressbarStyleEx  "

		public enum ProgressbarStyleEx
		{
		  Default,
		  Marquee
		}

		#endregion

		#region "  Constructor  "

		private const string CategoryName = "Xp ProgressBar";
		
		public ProgressBarEx()
		{}

		#endregion

		#region "  Private Fields  "

		private Color mColor1 = Color.FromArgb(170, 240, 170);
		private Color mColor2 = Color.FromArgb(10, 150, 10);
		private Color mColorBackGround = Color.White;
		private Color mColorText = Color.Black;
		private Image mDobleBack = null;
		private GradientMode mGradientStyle = GradientMode.VerticalCenter;
		private int mMax = 100;
		private int mMin = 0;
		private int mValue = 50;
		private byte mBlockDistance = 2;
		private byte mBlockWidth = 6;
		private System.Drawing.ContentAlignment alignment = System.Drawing.ContentAlignment.MiddleCenter;
		private ProgressbarStyleEx style = ProgressbarStyleEx.Default;

		#endregion
		
		#region "  Dispose  "

		protected override void Dispose(bool disposing)
		{
			if (! this.IsDisposed)
			{
				if (mDobleBack != null)
				{
					mDobleBack.Dispose();
				}
				if (mBrush1 != null)
				{
					mBrush1.Dispose();
				}

				if (mBrush2 != null)
				{
					mBrush2.Dispose();
				}

				base.Dispose(disposing);
			}
		}

		#endregion

		#region "  Colors   "

		[Category(CategoryName)]
		[Description("The Back Color of the Progress Bar")]
		public override Color BackColor
		{
			get { return mColorBackGround; }
			set
			{
				mColorBackGround = value;
				this.InvalidateBuffer(true);
			}
		}

		[Category(CategoryName)]
		[Description("The Border Color of the gradient in the Progress Bar")]
		public Color BorderColor
		{
			get { return mColor1; }
			set
			{
				mColor1 = value;
				this.InvalidateBuffer(true);
			}
		}

		[Category(CategoryName)]
		[Description("The Center Color of the gradient in the Progress Bar")]
		public Color BarColor
		{
			get { return mColor2; }
			set
			{
				mColor2 = value;
				this.InvalidateBuffer(true);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Description("Set to TRUE to reset all colors like the Windows XP Progress Bar ®")]
		[Category(CategoryName)]
		[DefaultValue(false)]
		public bool ColorsXP
		{
			get { return false; }
			set
			{
				BorderColor = Color.FromArgb(170, 240, 170);
				BarColor = Color.FromArgb(10, 150, 10);
				BackColor = Color.White;
			}
		}

		[Category(CategoryName)]
		[Description("The Color of the text displayed in the Progress Bar")]
		public override Color ForeColor
		{
			get { return mColorText; }
			set
			{
				mColorText = value;

				if (this.Text != String.Empty)
					this.Invalidate();
			}
		}

		#endregion

		#region "  Value   "

		[RefreshProperties(RefreshProperties.Repaint)]
		[Category(CategoryName)]
		[Description("The Current Position of the Progress Bar")]
		public int Value
		{
			get { return mValue; }
			set
			{
				if (value > mMax)
				{
					mValue = mMax;
				}
				else if (value < mMin)
				{
					mValue = mMin;
				}
				else
				{
					mValue = value;
				}
				this.Invalidate();
			}
		}

		[RefreshProperties(RefreshProperties.Repaint)]
		[Category(CategoryName)]
		[Description("The Max Position of the Progress Bar")]
		public int MaxValue
		{
			get { return mMax; }
			set
			{
				if (value > mMin)
				{
					mMax = value;

					if (mValue > mMax)
						Value = mMax;

					this.InvalidateBuffer(true);
				}
			}
		}

		[RefreshProperties(RefreshProperties.Repaint)]
		[Category(CategoryName)]
		[Description("The Min Position of the Progress Bar")]
		public int MinValue
		{
			get { return mMin; }
			set
			{
				if (value < mMax)
				{
					mMin = value;

					if (mValue < mMin)
						Value = mMin;
					this.InvalidateBuffer(true);
				}
			}
		}

		[Category(CategoryName)]
		[Description("The number of Pixels between two Blocks in Progress Bar")]
		[DefaultValue((byte) 2)]
		public byte BlockDistance
		{
			get { return mBlockDistance; }
			set
			{
				if (value >= 0)
				{
					mBlockDistance = value;
					this.InvalidateBuffer(true);
				}
			}
		}

		#endregion

		#region  "  Progress Style   "

		[Category(CategoryName)]
		[Description("The Style of the gradient bar in Progress Bar")]
		[DefaultValue(GradientMode.VerticalCenter)]
		public GradientMode GradientStyle
		{
			get { return mGradientStyle; }
			set
			{
				if (mGradientStyle != value)
				{
					mGradientStyle = value;
					CreatePaintElements();
					this.Invalidate();
				}
			}
		}

		[Category(CategoryName)]
		[Description("The Style of  Progress Bar")]
		[DefaultValue(ProgressbarStyleEx.Default)]
		public ProgressbarStyleEx Style
		{
			get
			{
				return style;
			}
			set
			{
				style = value;
			}
		}


		[Category(CategoryName)]
		[Description("The number of Pixels of the Blocks in Progress Bar")]
		[DefaultValue((byte) 6)]
		public byte BlockWidth
		{
			get { return mBlockWidth; }
			set
			{
				if (value > 0)
				{
					mBlockWidth = value;
					this.InvalidateBuffer(true);
				}
			}
		}

		#endregion

		#region "  BackImage  "

		[RefreshProperties(RefreshProperties.Repaint)]
		[Category(CategoryName)]
		public override Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set
			{
				base.BackgroundImage = value;
				InvalidateBuffer();
			}
		}

		#endregion

		#region "  Text Override  "

		[Category(CategoryName)]
		[Description("The Text displayed in the Progress Bar")]
		[DefaultValue("")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (base.Text != value)
				{
					base.Text = value;
					this.Invalidate();
				}
			}
		}

		#endregion

		#region "  Text Shadow  "

		private bool mTextShadow = true;

		[Category(CategoryName)]
		[Description("Set the Text shadow in the Progress Bar")]
		[DefaultValue(true)]
		public bool TextShadow
		{
			get { return mTextShadow; }
			set
			{
				mTextShadow = value;
				this.Invalidate();
			}
		}

		#endregion

		#region "  Text Shadow Alpha  "

		private byte mTextShadowAlpha = 150;

		[Category(CategoryName)]
		[Description("Set the Alpha Channel of the Text shadow in the Progress Bar")]
		[DefaultValue((byte) 150)]
		public byte TextShadowAlpha
		{
			get { return mTextShadowAlpha; }
			set
			{
				if (mTextShadowAlpha != value)
				{
					mTextShadowAlpha = value;
					this.TextShadow = true;
				}
			}
		}

		#endregion

		#region "  Paint Methods  "

		#region "  OnPaint  "

		protected override void OnPaint(PaintEventArgs e)
		{
			switch (Style)
			{
				case ProgressbarStyleEx.Default:
					paintDefault(e);
					break;

				case ProgressbarStyleEx.Marquee:
					paintMarquee(e);
					break;
			}
		}

		private void paintDefault(PaintEventArgs e)
		{
			if (!this.IsDisposed)
			{
				int mBlockTotal = mBlockWidth + mBlockDistance;
				float mUtilWidth = this.Width - 6 + mBlockDistance;

				if (mDobleBack == null)
				{
					mUtilWidth = this.Width - 6 + mBlockDistance;
					int mMaxBlocks = (int)(mUtilWidth / mBlockTotal);
					this.Width = 6 + mBlockTotal * mMaxBlocks;

					mDobleBack = new Bitmap(this.Width, this.Height);

					Graphics g2 = Graphics.FromImage(mDobleBack);

					CreatePaintElements();

					g2.Clear(mColorBackGround);

					if (this.BackgroundImage != null)
					{
						TextureBrush textuBrush = new TextureBrush(this.BackgroundImage, WrapMode.Tile);
						g2.FillRectangle(textuBrush, 0, 0, this.Width, this.Height);
						textuBrush.Dispose();
					}
					//					g2.DrawRectangle(mPenOut2, outnnerRect2);
					//					g2.DrawRectangle(mPenOut, outnnerRect);
					g2.DrawRectangle(mPenIn, innerRect);
					g2.Dispose();

				}

				Image ima = new Bitmap(mDobleBack);

				Graphics gtemp = Graphics.FromImage(ima);

				int mCantBlocks = (int)((((float)mValue - mMin) / (mMax - mMin)) * mUtilWidth / mBlockTotal);

				for (int i = 0; i < mCantBlocks; i++)
				{
					DrawBlock(gtemp, i);
				}

				if (this.Text != String.Empty)
				{
					gtemp.TextRenderingHint = TextRenderingHint.AntiAlias;
					DrawCenterString(gtemp, this.ClientRectangle);
				}

				e.Graphics.DrawImage(ima, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle, GraphicsUnit.Pixel);
				ima.Dispose();
				gtemp.Dispose();

			}

		}

		private void paintMarquee(PaintEventArgs e)
		{
			if (!this.IsDisposed)
			{
				int mBlockTotal = mBlockWidth + mBlockDistance;
				float mUtilWidth = this.Width - 6 + mBlockDistance;

				if (mDobleBack == null)
				{
					mUtilWidth = this.Width - 6 + mBlockDistance;
					int mMaxBlocks = (int)(mUtilWidth / mBlockTotal);
					this.Width = 6 + mBlockTotal * mMaxBlocks;

					mDobleBack = new Bitmap(this.Width, this.Height);

					Graphics g2 = Graphics.FromImage(mDobleBack);

					CreatePaintElements();

					g2.Clear(mColorBackGround);

					if (this.BackgroundImage != null)
					{
						TextureBrush textuBrush = new TextureBrush(this.BackgroundImage, WrapMode.Tile);
						g2.FillRectangle(textuBrush, 0, 0, this.Width, this.Height);
						textuBrush.Dispose();
					}
					//					g2.DrawRectangle(mPenOut2, outnnerRect2);
					//					g2.DrawRectangle(mPenOut, outnnerRect);
					g2.DrawRectangle(mPenIn, innerRect);
					g2.Dispose();

				}

				Image ima = new Bitmap(mDobleBack);

				Graphics gtemp = Graphics.FromImage(ima);

				int mCantBlocks = (int)((((float)mValue - mMin) / (mMax - mMin)) * mUtilWidth / mBlockTotal);

				for (int i = 0; i < mCantBlocks; i++)
				{
					DrawBlock(gtemp, i);
				}

				if (this.Text != String.Empty)
				{
					gtemp.TextRenderingHint = TextRenderingHint.AntiAlias;
					DrawCenterString(gtemp, this.ClientRectangle);
				}

				Rectangle rectangle = e.ClipRectangle;

				//if (style == ProgressbarStyleEx.Marquee)
				//{
				//  rectangle.X = rectangle.Width - 20;
				//  rectangle.Width = 20;
				//}
				
				e.Graphics.DrawImage(ima, rectangle.X, rectangle.Y, rectangle, GraphicsUnit.Pixel);
				ima.Dispose();
				gtemp.Dispose();

			}

		}


		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		#endregion

		#region "  OnSizeChange  "

		protected override void OnSizeChanged(EventArgs e)
		{
			if (!this.IsDisposed)
			{
				if (this.Height < 12)
				{
					this.Height = 12;
				}

				base.OnSizeChanged(e);
				this.InvalidateBuffer(true);
			}

		}

		protected override Size DefaultSize
		{
			get { return new Size(100, 29); }
		}


		#endregion

		#region "  More Draw Methods  "

		private void DrawBlock(Graphics g, int number)
		{
			if (mGradientStyle == GradientMode.None)
			{
				g.FillRectangle(mBrush3, 4 + number*(mBlockDistance + mBlockWidth), mBlockRect1.Y, mBlockWidth, mBlockRect1.Height);
			}
			else
			{
				g.FillRectangle(mBrush1, 4 + number*(mBlockDistance + mBlockWidth), mBlockRect1.Y + 1, mBlockWidth, mBlockRect1.Height);
				g.FillRectangle(mBrush2, 4 + number*(mBlockDistance + mBlockWidth), mBlockRect2.Y + 1, mBlockWidth, mBlockRect2.Height - 1);
			}
		}

		private void InvalidateBuffer()
		{
			InvalidateBuffer(false);
		}

		private void InvalidateBuffer(bool InvalidateControl)
		{
			if (mDobleBack != null)
			{
				mDobleBack.Dispose();
				mDobleBack = null;
			}

			if (InvalidateControl)
				this.Invalidate();
		}

		private void DisposeBrushes()
		{
			if (mBrush1 != null)
			{
				mBrush1.Dispose();
				mBrush1 = null;
			}

			if (mBrush2 != null)
			{
				mBrush2.Dispose();
				mBrush2 = null;
			}

			if (mBrush3 != null)
			{
				mBrush3.Dispose();
				mBrush3 = null;
			}

		}

		private void DrawCenterString(Graphics gfx, Rectangle box)
		{
			float left = 0;
			float top = 0;

			SizeF ss = gfx.MeasureString(this.Text, this.Font);

			switch (alignment)
			{
				case 	System.Drawing.ContentAlignment.MiddleCenter:
					left = box.X + (box.Width - ss.Width)/2;
					top = box.Y + (box.Height - ss.Height)/2 + 2;
					break;

				case System.Drawing.ContentAlignment.MiddleLeft:
					left = box.X + 3;
					top = box.Y + (box.Height - ss.Height)/2 + 2;
					break;

				case System.Drawing.ContentAlignment.MiddleRight:
					left = box.X + box.Width - ss.Width - 2;
					top = box.Y + (box.Height - ss.Height)/2 + 2;
					break;
				
				case System.Drawing.ContentAlignment.BottomCenter:
					left = box.X + (box.Width - ss.Width)/2;
					top = box.Y + box.Height - ss.Height;
					break;

				case System.Drawing.ContentAlignment.BottomLeft:
					left = box.X + 3;
					top = box.Y + box.Height - ss.Height;
					break;

				case System.Drawing.ContentAlignment.BottomRight:
					left = box.X + box.Width - ss.Width - 2;
					top = box.Y + box.Height - ss.Height;
					break;

				case System.Drawing.ContentAlignment.TopCenter:
					left = box.X + (box.Width - ss.Width)/2;
					top = box.Y + 2;
					break;

				case System.Drawing.ContentAlignment.TopLeft:
					left = box.X + 3;
					top = box.Y + 2;
					break;

				case System.Drawing.ContentAlignment.TopRight:
					left = box.X + box.Width - ss.Width - 2;
					top = box.Y + 2;
					break;
			}

			if (mTextShadow)
			{
				SolidBrush mShadowBrush = new SolidBrush(Color.FromArgb(mTextShadowAlpha, Color.Black));
				gfx.DrawString(this.Text, this.Font, mShadowBrush, left + 1, top + 1);
				mShadowBrush.Dispose();
			}
			SolidBrush mTextBrush = new SolidBrush(mColorText);
			gfx.DrawString(this.Text, this.Font, mTextBrush, left, top);
			mTextBrush.Dispose();
		}

		#endregion

		#region "  CreatePaintElements   "

		private Rectangle innerRect;
		private Brush mBrush3;
		private LinearGradientBrush mBrush1;
		private LinearGradientBrush mBrush2;
		private Pen mPenIn = new Pen(Color.FromArgb(239, 239, 239));

		private Pen mPenOut = new Pen(Color.FromArgb(104, 104, 104));
		private Pen mPenOut2 = new Pen(Color.FromArgb(190, 190, 190));

		private Rectangle mBlockRect1;
		private Rectangle mBlockRect2;
		private Rectangle outnnerRect;
		private Rectangle outnnerRect2;

		private void CreatePaintElements()
		{
			DisposeBrushes();

			switch (mGradientStyle)
			{
				case GradientMode.VerticalCenter:
					mBlockRect1 = new Rectangle(0,2,mBlockWidth,this.Height/2 + (int) (this.Height*0.05));
					mBrush1 = new LinearGradientBrush(mBlockRect1, mColor1, mColor2, LinearGradientMode.Vertical);
					mBlockRect2 = new Rectangle(0,mBlockRect1.Bottom - 1,mBlockWidth,this.Height - mBlockRect1.Height - 4);
					mBrush2 = new LinearGradientBrush(mBlockRect2, mColor2, mColor1, LinearGradientMode.Vertical);
					break;

				case GradientMode.Vertical:
					mBlockRect1 = new Rectangle(0,2,mBlockWidth,this.Height - 5);
					mBrush1 = new LinearGradientBrush(mBlockRect1, mColor1, mColor2, LinearGradientMode.Vertical);
					mBlockRect2 = new Rectangle(-100,-100,1,1);
					mBrush2 = new LinearGradientBrush(mBlockRect2, mColor2, mColor1, LinearGradientMode.Horizontal);
					break;

				case GradientMode.Horizontal:
					mBlockRect1 = new Rectangle(0,2,mBlockWidth,this.Height - 5);
					mBrush1 = new LinearGradientBrush(this.ClientRectangle, mColor1, mColor2, LinearGradientMode.Horizontal);
					mBlockRect2 = new Rectangle(-100,-100,1,1);
					mBrush2 = new LinearGradientBrush(mBlockRect2, Color.Red, Color.Red, LinearGradientMode.Horizontal);
					break;

				case GradientMode.HorizontalCenter:
					mBlockRect1 = new Rectangle(0,2,mBlockWidth,this.Height - 5);
					mBrush1 = new LinearGradientBrush(this.ClientRectangle, mColor1, mColor2, LinearGradientMode.Horizontal);
					mBrush1.SetBlendTriangularShape(0.5f);
					mBlockRect2 = new Rectangle(-100,-100,1,1);
					mBrush2 = new LinearGradientBrush(mBlockRect2, Color.Red, Color.Red, LinearGradientMode.Horizontal);
					break;

				case GradientMode.Diagonal:
					mBlockRect1 = new Rectangle(0,2,mBlockWidth,this.Height - 5);
					mBrush1 = new LinearGradientBrush(this.ClientRectangle, mColor1, mColor2, LinearGradientMode.ForwardDiagonal);
					mBlockRect2 = new Rectangle(-100,-100,1,1);
					mBrush2 = new LinearGradientBrush(mBlockRect2, Color.Red, Color.Red, LinearGradientMode.Horizontal);
					break;

				case GradientMode.None:
					mBlockRect1 = new Rectangle(0,2,mBlockWidth,this.Height - 5);
					mBrush3 = new System.Drawing.SolidBrush(mColor2);
					break;

				default:
					mBrush3 = new System.Drawing.SolidBrush(mColor2);
					mBrush1 = new LinearGradientBrush(mBlockRect1, mColor1, mColor2, LinearGradientMode.Vertical);
					mBrush2 = new LinearGradientBrush(mBlockRect2, mColor2, mColor1, LinearGradientMode.Vertical);
					break;
			}

			innerRect = new Rectangle(this.ClientRectangle.X + 1,this.ClientRectangle.Y,this.ClientRectangle.Width - 2,this.ClientRectangle.Height - 2);
			outnnerRect = new Rectangle(this.ClientRectangle.X,this.ClientRectangle.Y,this.ClientRectangle.Width - 1,this.ClientRectangle.Height - 1);
			outnnerRect2 = new Rectangle(this.ClientRectangle.X + 1,this.ClientRectangle.Y + 1,this.ClientRectangle.Width,this.ClientRectangle.Height);
		}

		#endregion

		#endregion

		public System.Drawing.ContentAlignment TextAlignment
		{
			get {return alignment;}
			set {alignment = value;}
		}

	}

}