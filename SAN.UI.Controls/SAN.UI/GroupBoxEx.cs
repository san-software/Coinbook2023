using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace SAN.UI.Controls
{
	/// <summary>A special custom rounding GroupBox with many painting features.</summary>
	//[ToolboxBitmap(typeof(GroupBoxEx), "SAN.UI.GroupBoxEx.bmp")]
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	public class GroupBoxEx : System.Windows.Forms.UserControl
	{
		#region Enumerations

		/// <summary>A special gradient enumeration.</summary>
		public enum GroupBoxGradientMode
		{
			None = 4,			/// <summary>Specifies no gradient mode.</summary>
			BackwardDiagonal = 3,			/// <summary>Specifies a gradient from upper right to lower left.</summary>
			ForwardDiagonal = 2,			/// <summary>Specifies a gradient from upper left to lower right.</summary>
			Horizontal = 0,			/// <summary>Specifies a gradient from left to right.</summary>
			Vertical = 1			/// <summary>Specifies a gradient from top to bottom.</summary>
		}
		#endregion

		#region Constants
		int SweepAngle = 90;		/// <summary>The sweep angle of the arc.</summary>
		#endregion

		#region Variables

		private System.ComponentModel.Container components = null;
		private int V_RoundCorners = 10;
		private string V_GroupTitle = "The GroupBoxEx";
		private System.Drawing.Color V_BorderColor = Color.Black;
		private float V_BorderThickness = 1;
		private bool V_ShadowControl = false;
		//private System.Drawing.Color backgroundColor = Color.Transparent;
		private System.Drawing.Color V_BackgroundGradientColor = Color.White;
		private GroupBoxGradientMode V_BackgroundGradientMode = GroupBoxGradientMode.None;
		private System.Drawing.Color V_ShadowColor = Color.DarkGray;
		private int V_ShadowThickness = 3;
		private System.Drawing.Image V_GroupImage = null;
		private System.Drawing.Color V_CustomGroupBoxColor = Color.White;
		private bool V_PaintGroupBox = false;
		private bool enabled = true;

		#endregion

		#region Properties

		/// <summary>This feature will paint the group title background to the specified color if PaintGroupBox is set to true.</summary>
		[Category("Appearance"), Description("This feature will paint the group title background to the specified color if PaintGroupBox is set to true.")]
		public Color GroupBoxBackColor
		{
			get
			{
				return V_CustomGroupBoxColor;
			}
			set
			{
				V_CustomGroupBoxColor = value;
				this.Refresh();
			}
		}

		/// <summary>This feature will paint the group title background to the GroupBoxBackColor.</summary>
		[Category("Appearance"), Description("This feature will paint the group title background to the GroupBoxBackColor.")]
		public bool PaintGroupBox{get{return V_PaintGroupBox;} set{V_PaintGroupBox=value; this.Refresh();}}

		/// <summary>This feature can add a 16 x 16 image to the group title bar.</summary>
		[Category("Appearance"), Description("This feature can add a 16 x 16 image to the group title bar.")]
		public Image GroupImage{get{return V_GroupImage;} set{V_GroupImage=value; this.Refresh();}}

		/// <summary>This feature will change the control's shadow color.</summary>
		[Category("Appearance"), Description("This feature will change the control's shadow color.")]
		public Color ShadowColor{get{return V_ShadowColor;} set{V_ShadowColor=value; this.Refresh();}}

		/// <summary>This feature will change the size of the shadow border.</summary>
		[Category("Appearance"), Description("This feature will change the size of the shadow border.")]
		public int ShadowThickness
		{
			get{return V_ShadowThickness;} 
			set
			{
				if(value>10)
				{
					V_ShadowThickness=10;
				}
				else
				{
					if(value<1){V_ShadowThickness=1;}
					else{V_ShadowThickness=value; }
				}

				this.Refresh();
			}
		}

		/// <summary>This feature can be used in combination with BackColor to create a gradient background.</summary>
		[Category("Appearance"), Description("This feature can be used in combination with BackColor to create a gradient background.")]
		public Color BackgroundGradientColor
		{
			get
			{
				return V_BackgroundGradientColor;
			}
			set
			{
				V_BackgroundGradientColor = value;
				this.Refresh();
			}
		}
		
		/// <summary>This feature turns on background gradient painting.</summary>
		[Category("Appearance"), Description("This feature turns on background gradient painting.")]
		public GroupBoxGradientMode BackgroundGradientMode
		{
			get
			{
				return V_BackgroundGradientMode;
			}
			set
			{
				V_BackgroundGradientMode = value;
				this.Refresh();
			}
		}

		/// <summary>This feature will round the corners of the control.</summary>
		[Category("Appearance"), Description("This feature will round the corners of the control.")]
		public int RoundCorners
		{
			get{return V_RoundCorners;} 
			set
			{
				if(value>25)
					V_RoundCorners=25;
				else
				{
					if(value<1)
						V_RoundCorners=1;
					else
						V_RoundCorners=value;
				}
				
				this.Refresh();
			}
		}

		/// <summary>This feature will add a group title to the control.</summary>
		[Category("Appearance"), Description("This feature will add a group title to the control.")]
		public string GroupTitle{get{return V_GroupTitle;} set{V_GroupTitle=value; this.Refresh();}}

		/// <summary>This feature will allow you to change the color of the control's border.</summary>
		[Category("Appearance"), Description("This feature will allow you to change the color of the control's border.")]
		public Color BorderColor{get{return V_BorderColor;} set{V_BorderColor=value; this.Refresh();}}

		/// <summary>This feature will allow you to set the control's border size.</summary>
		[Category("Appearance"), Description("This feature will allow you to set the control's border size.")]
		public float BorderThickness
		{
			get{return V_BorderThickness;} 
			set
			{
				if(value>3)
					V_BorderThickness=3;
				else
				{
					if(value<1){V_BorderThickness=1;}
					else{V_BorderThickness=value;}
				}
				this.Refresh();
			}
		}

		/// <summary>This feature will allow you to turn on control shadowing.</summary>
		[Category("Appearance"), Description("This feature will allow you to turn on control shadowing.")]
		public bool ShadowControl{get{return V_ShadowControl;} set{V_ShadowControl=value; this.Refresh();}}

		#endregion

		#region Constructor
		
		/// <summary>This method will construct a new GroupBox control.</summary>
		public GroupBoxEx()
		{
			InitializeStyles();
			InitializeGroupBox();
			base.BorderStyle = BorderStyle.None;

			//base.BackColor = Farbverwaltung.BackColor;
			//base.ForeColor = Farbverwaltung.DisplayForeColor;
		}

	
		#endregion

		#region DeConstructor

		/// <summary>This method will dispose of the GroupBox control.</summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing){if(components!=null){components.Dispose();}}
			base.Dispose(disposing);
		}


		#endregion

		#region Initialization

		/// <summary>This method will initialize the controls custom styles.</summary>
		private void InitializeStyles()
		{
			//Set the control styles----------------------------------
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			//--------------------------------------------------------
		}


		/// <summary>This method will initialize the GroupBox control.</summary>
		private void InitializeGroupBox()
		{
			components = new System.ComponentModel.Container();
			this.Resize+=new EventHandler(GroupBox_Resize);
			this.DockPadding.All = 20;
			this.Name = "GroupBox";
			this.Size = new System.Drawing.Size(368, 288);
		}

	
		#endregion

		#region Protected Methods
		
		/// <summary>Overrides the OnPaint method to paint control.</summary>
		/// <param name="e">The paint event arguments.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			PaintBack(e.Graphics);
			PaintGroupText(e.Graphics);
		}

		#endregion

		#region Private Methods

		/// <summary>This method will paint the group title.</summary>
		/// <param name="g">The paint event graphics object.</param>
		private void PaintGroupText(System.Drawing.Graphics g)
		{
			//Check if string has something-------------
			if(this.GroupTitle==string.Empty){return;}
			//------------------------------------------

			//Set Graphics smoothing mode to Anit-Alias-- 
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//-------------------------------------------

			//Declare Variables------------------
			SizeF StringSize = g.MeasureString(this.GroupTitle, this.Font);
			Size StringSize2 = StringSize.ToSize();
			if(this.GroupImage!=null){StringSize2.Width+=18;}
			int ArcWidth = this.RoundCorners;
			int ArcHeight = this.RoundCorners;
			int ArcX1 = 20;
			int ArcX2 = (StringSize2.Width+34) - (ArcWidth + 1);
			int ArcY1 = 0;
			int ArcY2 = 24 - (ArcHeight + 1);
			System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
			System.Drawing.Brush BorderBrush = new SolidBrush(this.BorderColor);
			System.Drawing.Pen BorderPen = new Pen(BorderBrush, this.BorderThickness);
			System.Drawing.Drawing2D.LinearGradientBrush BackgroundGradientBrush = null;
			System.Drawing.Brush BackgroundBrush = (this.PaintGroupBox) ? new SolidBrush(this.GroupBoxBackColor) : 
				new SolidBrush(Farbverwaltung.BackColor);
			System.Drawing.SolidBrush TextColorBrush = new SolidBrush(this.ForeColor);
			System.Drawing.SolidBrush ShadowBrush = null;
			System.Drawing.Drawing2D.GraphicsPath ShadowPath  = null;
			//-----------------------------------

			//Check if shadow is needed----------
			if(this.ShadowControl)
			{
				ShadowBrush = new SolidBrush(this.ShadowColor);
				ShadowPath = new System.Drawing.Drawing2D.GraphicsPath();
				ShadowPath.AddArc(ArcX1+(this.ShadowThickness-1), ArcY1+(this.ShadowThickness-1), ArcWidth, ArcHeight, 180, SweepAngle); // Top Left
				ShadowPath.AddArc(ArcX2+(this.ShadowThickness-1), ArcY1+(this.ShadowThickness-1), ArcWidth, ArcHeight, 270, SweepAngle); //Top Right
				ShadowPath.AddArc(ArcX2+(this.ShadowThickness-1), ArcY2+(this.ShadowThickness-1), ArcWidth, ArcHeight, 360, SweepAngle); //Bottom Right
				ShadowPath.AddArc(ArcX1+(this.ShadowThickness-1), ArcY2+(this.ShadowThickness-1), ArcWidth, ArcHeight, 90, 	SweepAngle); //Bottom Left
				ShadowPath.CloseAllFigures();

				//Paint Rounded Rectangle------------
				g.FillPath(ShadowBrush, ShadowPath);
				//-----------------------------------
			}
			//-----------------------------------

			//Create Rounded Rectangle Path------
			path.AddArc(ArcX1, ArcY1, ArcWidth, ArcHeight, 180, SweepAngle); // Top Left
			path.AddArc(ArcX2, ArcY1, ArcWidth, ArcHeight, 270, SweepAngle); //Top Right
			path.AddArc(ArcX2, ArcY2, ArcWidth, ArcHeight, 360, SweepAngle); //Bottom Right
			path.AddArc(ArcX1, ArcY2, ArcWidth, ArcHeight, 90, SweepAngle); //Bottom Left
			path.CloseAllFigures(); 
			//-----------------------------------

			//Check if Gradient Mode is enabled--
			if(this.PaintGroupBox)
			{
				//Paint Rounded Rectangle------------
				g.FillPath(BackgroundBrush, path);
				//-----------------------------------
			}
			else
			{
				if(this.BackgroundGradientMode==GroupBoxGradientMode.None)
				{
					//Paint Rounded Rectangle------------
					g.FillPath(BackgroundBrush, path);
					//-----------------------------------
				}
				else
				{
					BackgroundGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), Farbverwaltung.BackColor, 
						this.BackgroundGradientColor, (LinearGradientMode)this.BackgroundGradientMode);
				
					//Paint Rounded Rectangle------------
					g.FillPath(BackgroundGradientBrush, path);
					//-----------------------------------
				}
			}
			//-----------------------------------

			//Paint Borded-----------------------
			g.DrawPath(BorderPen, path);
			//-----------------------------------

			//Paint Text-------------------------
			int CustomStringWidth = (this.GroupImage!=null) ? 44 : 28;
			g.DrawString(this.GroupTitle, this.Font, TextColorBrush, CustomStringWidth, 5);
			//-----------------------------------

			if(this.GroupImage!=null)
				g.DrawImage(this.GroupImage, 28,4, 16, 16);			//Draw GroupImage if there is one----

			//Destroy Graphic Objects------------
			if(path!=null){path.Dispose();}
			if(BorderBrush!=null){BorderBrush.Dispose();}
			if(BorderPen!=null){BorderPen.Dispose();}
			if(BackgroundGradientBrush!=null){BackgroundGradientBrush.Dispose();}
			if(BackgroundBrush!=null){BackgroundBrush.Dispose();}
			if(TextColorBrush!=null){TextColorBrush .Dispose();}
			if(ShadowBrush!=null){ShadowBrush.Dispose();}
			if(ShadowPath!=null){ShadowPath.Dispose();}
			//-----------------------------------
		}

		
		/// <summary>This method will paint the control.</summary>
		/// <param name="g">The paint event graphics object.</param>
		private void PaintBack(System.Drawing.Graphics g)
		{
			//Set Graphics smoothing mode to Anit-Alias-- 
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//-------------------------------------------

			//Declare Variables------------------
			int ArcWidth = this.RoundCorners * 2;
			int ArcHeight = this.RoundCorners * 2;
			int ArcX1 = 0;
			int ArcX2 = (this.ShadowControl) ? (this.Width - (ArcWidth + 1))-this.ShadowThickness : this.Width - (ArcWidth + 1);
			int ArcY1 = 0;  //10
			int ArcY2 = (this.ShadowControl) ? (this.Height - (ArcHeight + 1))-this.ShadowThickness : this.Height - (ArcHeight + 1);
			GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
			GraphicsPath region = new System.Drawing.Drawing2D.GraphicsPath();
			Brush BorderBrush = new SolidBrush(this.BorderColor);
			Pen BorderPen = new Pen(BorderBrush, this.BorderThickness);
			LinearGradientBrush BackgroundGradientBrush = null;
			Brush BackgroundBrush = new SolidBrush(Farbverwaltung.BackColor);
			SolidBrush ShadowBrush = null;
			GraphicsPath ShadowPath  = null;

			//-----------------------------------

			//Check if shadow is needed----------
			if(this.ShadowControl)
			{
				ShadowBrush = new SolidBrush(this.ShadowColor);
				ShadowPath = new System.Drawing.Drawing2D.GraphicsPath();
				ShadowPath.AddArc(ArcX1+this.ShadowThickness, ArcY1+this.ShadowThickness, ArcWidth, ArcHeight, 180, SweepAngle); // Top Left
				ShadowPath.AddArc(ArcX2+this.ShadowThickness, ArcY1+this.ShadowThickness, ArcWidth, ArcHeight, 270, SweepAngle); //Top Right
				ShadowPath.AddArc(ArcX2+this.ShadowThickness, ArcY2+this.ShadowThickness, ArcWidth, ArcHeight, 360, SweepAngle); //Bottom Right
				ShadowPath.AddArc(ArcX1+this.ShadowThickness, ArcY2+this.ShadowThickness, ArcWidth, ArcHeight, 90, SweepAngle); //Bottom Left
				ShadowPath.CloseAllFigures();

				g.FillPath(ShadowBrush, ShadowPath);				//Paint Rounded Rectangle------------
			}

				//Create Rounded Rectangle Path------
				path.AddArc(ArcX1, ArcY1, ArcWidth, ArcHeight, 180, SweepAngle); // Top Left
				path.AddArc(ArcX2, ArcY1, ArcWidth, ArcHeight, 270, SweepAngle); //Top Right
				path.AddArc(ArcX2, ArcY2, ArcWidth, ArcHeight, 360, SweepAngle); //Bottom Right
				path.AddArc(ArcX1, ArcY2, ArcWidth, ArcHeight, 90, SweepAngle); //Bottom Left
				path.CloseAllFigures();

			// Rand für Control wenn ein BackgroundImage definiert ist
			if (BackgroundImage != null)
			{
				region.AddArc(ArcX1, ArcY1, ArcWidth+1, ArcHeight+1, 180, SweepAngle); // Top Left
				region.AddArc(ArcX2, ArcY1, ArcWidth+1, ArcHeight+1, 270, SweepAngle); //Top Right
				region.AddArc(ArcX2, ArcY2, ArcWidth+1, ArcHeight+1, 360, SweepAngle); //Bottom Right
				region.AddArc(ArcX1, ArcY2, ArcWidth+1, ArcHeight+1, 90, SweepAngle); //Bottom Left
				region.CloseAllFigures();

				Region = new Region(region);				// Apply new region
			}
			else
			{
				//Check if Gradient Mode is enabled--
				if(this.BackgroundGradientMode==GroupBoxGradientMode.None)
					g.FillPath(BackgroundBrush, path);				//Paint Rounded Rectangle------------
				else
				{
					BackgroundGradientBrush = new LinearGradientBrush(new Rectangle(0,0,this.Width,this.Height),
						Farbverwaltung.BackColor, this.BackgroundGradientColor, (LinearGradientMode)this.BackgroundGradientMode);
					
					g.FillPath(BackgroundGradientBrush, path);				//Paint Rounded Rectangle------------
				}
			}

			//Paint Borded-----------------------
			g.DrawPath(BorderPen, path);
			//-----------------------------------

			//Destroy Graphic Objects------------
			if (region != null)
				region.Dispose();

			if (path != null)
				path.Dispose();

			if (BorderBrush != null)
				BorderBrush.Dispose();

			if(BorderPen!=null){BorderPen.Dispose();}
			if(BackgroundGradientBrush!=null){BackgroundGradientBrush.Dispose();}
			if(BackgroundBrush!=null){BackgroundBrush.Dispose();}
			if(ShadowBrush!=null){ShadowBrush.Dispose();}
			if(ShadowPath!=null){ShadowPath.Dispose();}
			//-----------------------------------
		}

	
		/// <summary>This method fires when the GroupBox resize event occurs.</summary>
		/// <param name="sender">The object the sent the event.</param>
		/// <param name="e">The event arguments.</param>
		private void GroupBox_Resize(object sender, EventArgs e)
		{
			this.Refresh();
		}


		#endregion

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			if (BackgroundImage != null)
				base.OnPaintBackground(pevent);
			else
			{
				RectangleF r = pevent.Graphics.VisibleClipBounds;
				Brush b = new SolidBrush(Farbverwaltung.BackColor);

				pevent.Graphics.FillRectangle(b, r.X, r.Y, r.Width, r.Height);
			}
		}

		public new bool Enabled
		{
			get
			{
				return enabled;
			}

			set
			{
				enabled = value;

				foreach (Control c in base.Controls)
				{
					if (c.GetType().Name != "LabelEx" && c.GetType().Name != "Label")
						c.Enabled = value;
				}
			}
		}
	}
}
