using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace SAN.UI.Controls
{
	/// <summary>
	/// Zusammenfassung für UserControl1.
	/// </summary>
	public class DateTimePickerEx : System.Windows.Forms.DateTimePicker
	{
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private bool readOnly = false;
		private SolidBrush backBrush = null;
		private SolidBrush foreBrush = null;
		private System.Windows.Forms.Label lblDate;

		#region WindowsAPI
		[DllImport("User32.dll")]
		private static extern int GetWindowLong(IntPtr h, int index);

		[DllImport("User32.dll")]
		private static extern int SetWindowLong(IntPtr h, int index, int value);

		[DllImport("User32.dll")]
		private static extern IntPtr SendMessage(IntPtr h, int msg, int param, int data);

		[DllImport("User32.dll")]
		private static extern int SendMessage(IntPtr h, int msg, int param, ref Rectangle data);

		[DllImport("User32.dll")]
		private static extern int MoveWindow(IntPtr h, int x, int y, int width, int height, bool repaint);

		private const int GWL_STYLE = (-16);
		private const int MCM_FIRST = 0x1000;
		private const int MCM_GETMINREQRECT = (MCM_FIRST + 9);
		private const int MCS_WEEKNUMBERS = 0x4;
		private const int DTM_FIRST = 0x1000;
		private const int DTM_GETMONTHCAL = (DTM_FIRST + 8);
		private const int CB_SHOWDROPDOWN = 0x014F;

		#endregion

		#region Deklarationen
		
		private bool isNull;		// true, when no date shall be displayed (empty DateTimePicker)
		private string nullValue;		// If isNull = true, this value is shown in the DTP
		private DateTimePickerFormat format = DateTimePickerFormat.Long;		// The format of the DateTimePicker control
		private string customFormat;		// The custom format of the DateTimePicker control
		private string formatAsString;		// The format of the DateTimePicker control as string
		private bool showWeekNumbers;
		private bool showClipBoard = true;
		
		#endregion
		
		public DateTimePickerEx() : base()
		{
			// Dieser Aufruf ist für den Windows Form-Designer erforderlich.
			InitializeComponent();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblDate.Name = "lblDate";

			this.Controls.Add(lblDate);

			base.BackColor = Farbverwaltung.InputBackColor;
			base.ForeColor = Farbverwaltung.InputForeColor;

			base.Format = DateTimePickerFormat.Custom;
			NullValue = " ";
			Format = DateTimePickerFormat.Long;
		}

		/// <summary> 
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
					components.Dispose();
			}

			if(disposing && backBrush != null)
				backBrush.Dispose();

			base.Dispose( disposing );
		}

		#region Public properties

		/// <summary>
		/// Gets or sets the date/time value assigned to the control.
		/// </summary>
		/// <value>The DateTime value assigned to the control
		/// </value>
		/// <remarks>
		/// <p>If the <b>Value</b> property has not been changed in code or by the user, it is set
		/// to the current date and time (<see cref="DateTime.Now"/>).</p>
		/// <p>If <b>Value</b> is <b>null</b>, the DateTimePicker shows 
		/// <see cref="NullValue"/>.</p>
		/// </remarks>
		[Bindable(true)]
		[Browsable(false)]
		public new Object Value 
		{
			get 
			{
				if (isNull)
					return DBNull.Value;
				else
					return base.Value;
			}
			set 
			{
				if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
				{
					SetToNullValue();
					base.Value = DateTime.Today;
				}
				else 
				{
					DateTime datum = Convert.ToDateTime(value);
					string text ="";
					
					if (datum < base.MinDate)
						text = "Das zugewiesene Datum "+datum.ToShortDateString() + " ist kleiner als das minimal zugelassene Datum " 
							+ base.MinDate.ToShortDateString();
					if (datum > base.MaxDate)
						text = "Das zugewiesene Datum "+datum.ToShortDateString() + " ist größer als das maximal zugelassene Datum " 
							+ base.MaxDate.ToShortDateString();
					
					if (text.Length != 0)
						MessageBox.Show(text,Name,MessageBoxButtons.OK,MessageBoxIcon.Error);
					else
					{
						SetToDateTimeValue();
						base.Value = datum;
					}
				}

				Changed = false;
			}
		}

		public override String Text
		{
			get
			{
				string result = base.Text;
				if (result.Length == 0)
				{
					if (Value != null)
					{
						switch(Format)
						{
							case DateTimePickerFormat.Long:
								result = base.Value.ToShortDateString();
								break;  
							case DateTimePickerFormat.Short:
								result = base.Value.ToShortDateString();
								break;
							case DateTimePickerFormat.Time:
								result = base.Value.ToShortTimeString();
								break;
							case DateTimePickerFormat.Custom:
								result = string.Format(this.CustomFormat,base.Value);
								break;
						}
					}
					else
						result = NullValue;
				}

				return result;
			}
			set 
			{
				try
				{
					base.Text = value;
				}
				catch
				{
					base.Text = value;
				}

				Changed = false;
			}
		}

			/// <summary>
			/// Gets or sets the format of the date and time displayed in the control.
			/// </summary>
			/// <value>One of the <see cref="DateTimePickerFormat"/> values. The default is 
			/// <see cref="DateTimePickerFormat.Long"/>.</value>
			[Browsable(true)]
			[DefaultValue(DateTimePickerFormat.Long), TypeConverter(typeof(Enum))]
			public new DateTimePickerFormat Format
			{
			get { return format; }
			set
			{
				format = value;
				if (!isNull)
					SetFormat();
				OnFormatChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the custom date/time format string.
		/// <value>A string that represents the custom date/time format. The default is a null
		/// reference (<b>Nothing</b> in Visual Basic).</value>
		/// </summary>
		[Category("Appearance")]
		public new String CustomFormat
		{
			get { return customFormat; }
			set { customFormat = value; }
		}

		/// <summary>
		/// Gets or sets the string value that is assigned to the control as null value. 
		/// </summary>
		/// <value>The string value assigned to the control as null value.</value>
		/// <remarks>
		/// If the <see cref="Value"/> is <b>null</b>, <b>NullValue</b> is
		/// shown in the <b>DateTimePicker</b> control.
		/// </remarks>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("The string used to display null values in the control")]
		[DefaultValue(" ")]
		public String NullValue
		{
			get { return nullValue; }
			set { nullValue = value; }
		}

		[Browsable(false)]
		public bool IsNull
		{
			get {return isNull;}
		}

		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance")]
		public bool ShowWeekNumbers
		{
			get	{return showWeekNumbers;}
			set	{showWeekNumbers = value;}
		}

		[Category("Appearance")]
		public bool ShowClipBoard
		{
			get {return showClipBoard;}
			set {showClipBoard=value;}
		}
		
		public new bool Enabled
		{
			get {return base.Enabled;}
			set
			{
				base.Enabled = value;
			}
		}

		[Browsable(false)]
		public DateTime Date
		{
			get
			{
				return Convert.ToDateTime(base.Value);
			}

			set
			{
				base.Value = value;
			}
		}
		
		public bool ReadOnly
		{
			get {return readOnly;}
			set
			{
				readOnly = value;
				showLabel(value);
			}
		}
		
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override Color BackColor
		{
			get {return base.BackColor;}
			set
			{
				if(backBrush != null)
					backBrush.Dispose();
				base.BackColor = value;
				backBrush = new SolidBrush(BackColor);
				Invalidate();
			}
		}
	
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override Color ForeColor
		{
			get {return base.ForeColor;}
			set
			{
				if(foreBrush != null)
					foreBrush.Dispose();
				base.ForeColor = value;
				foreBrush = new SolidBrush(ForeColor);
				Invalidate();
			}
		}

		[Browsable(false)]
		public bool Changed
		{
			get;
			set;
		}
		#endregion
		
		#region Private methods/properties
		/// <summary>
		/// Stores the current format of the DateTimePicker as string. 
		/// </summary>
		private string FormatAsString
		{
			get { return formatAsString; }
			set 
			{
				formatAsString = value;
				base.CustomFormat = value; }
		}

		/// <summary>
		/// Sets the format according to the current DateTimePickerFormat.
		/// </summary>
		private void SetFormat()
		{
			CultureInfo ci = Thread.CurrentThread.CurrentCulture;
			DateTimeFormatInfo dtf = ci.DateTimeFormat;
			switch (format)
			{
				case DateTimePickerFormat.Long:
					FormatAsString = dtf.LongDatePattern;
					break;  
				case DateTimePickerFormat.Short:
					FormatAsString = dtf.ShortDatePattern;
					break;
				case DateTimePickerFormat.Time:
					FormatAsString = dtf.ShortTimePattern;
					break;
				case DateTimePickerFormat.Custom:
					FormatAsString = this.CustomFormat;
					break;
			}
		}

		/// <summary>
		/// Sets the <b>DateTimePicker</b> to the value of the <see cref="NullValue"/> property.
		/// </summary>
		private void SetToNullValue()
		{
			isNull = true;
			base.CustomFormat = (nullValue == null || nullValue == String.Empty) ? " " : "'" + nullValue + "'";
		}

		/// <summary>
		/// Sets the <b>DateTimePicker</b> back to a non null value.
		/// </summary>
		private void SetToDateTimeValue()
		{
			if (isNull) 
			{
				SetFormat();
				isNull = false;
				base.OnValueChanged(new EventArgs());
			}
		}

		private void showLabel(bool visible)
		{
			lblDate.Visible = visible;
			lblDate.Width = this.Width;
			lblDate.Height = this.Height;
			lblDate.BackColor = Farbverwaltung.DisplayBackColor;
			lblDate.ForeColor = Farbverwaltung.DisplayForeColor;
			lblDate.BringToFront();
			if (this.IsNull)
				lblDate.Text = "";
			else
				lblDate.Text = this.Text;
		}

		#endregion

		#region Events
		/// <summary>
		/// This member overrides <see cref="Control.WndProc"/>.
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			if (isNull)
			{
				if (m.Msg == 0x4e)                         // WM_NOTIFY
				{
					NMHDR nm = (NMHDR)m.GetLParam(typeof(NMHDR));
					if (nm.Code == -746 || nm.Code == -722)  // DTN_CLOSEUP || DTN_?
						SetToDateTimeValue();
				}
			}

			switch (m.Msg)
			{
				case 0x14:			// setzen des Hintergrunds // WM_ERASEBKGND
					Graphics g = Graphics.FromHdc(m.WParam);

					backBrush = new SolidBrush(Farbverwaltung.InputBackColor);

					if(backBrush == null)
							backBrush = new SolidBrush(BackColor);
					
					if (readOnly)
						backBrush = new SolidBrush(Farbverwaltung.DisplayBackColor);

					g.FillRectangle(backBrush, ClientRectangle);
					g.Dispose();

					break;
				
				default:
					base.WndProc(ref m);
					break;
			}
						
		}

		[StructLayout(LayoutKind.Sequential)]
			private struct NMHDR
		{
			public IntPtr HwndFrom;
			public int IdFrom;
			public int Code;
		}

		/// <summary>
		/// This member overrides <see cref="Control.OnKeyDown"/>.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (!readOnly)
			{
				if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
				{
					this.Value = DBNull.Value;
					OnValueChanged(EventArgs.Empty);
					OnTextChanged(EventArgs.Empty);
				}
				base.OnKeyUp(e);
			}
			else
				e.Handled=true;
		}

		protected override void OnValueChanged(EventArgs eventargs)
		{
			base.OnValueChanged(eventargs);
			showLabel(readOnly);
			Changed = true;
		}

		#endregion
		
		#region Vom Komponenten-Designer generierter Code
		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.SuspendLayout();
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(64, 24);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.TabIndex = 0;
			// 
			// UserControl1
			// 
			this.Controls.Add(this.dateTimePicker1);
			this.Name = "UserControl1";
			this.Size = new System.Drawing.Size(424, 128);
			this.ResumeLayout(false);

		}
		#endregion

		#region overidden stuff
		protected override bool IsInputKey(Keys keyData)
		{
			bool result;

			switch (keyData)
			{
				case Keys.Enter:
				case Keys.Down:
				case Keys.Up:
					result = true;
					break;
					
				default:
					result = base.IsInputKey(keyData);
					break;
			}
			return result;
		}
		
		protected override void OnDropDown(EventArgs e)
		{
				IntPtr monthView = SendMessage(Handle, DTM_GETMONTHCAL, 0, 0);
				int style = GetWindowLong(monthView, GWL_STYLE);
				if (ShowWeekNumbers)
					style = style | MCS_WEEKNUMBERS;
				else
					style = style & ~MCS_WEEKNUMBERS;

				Rectangle rect = new Rectangle();
				SetWindowLong(monthView, GWL_STYLE, style);
				SendMessage(monthView, MCM_GETMINREQRECT, 0, ref rect);
				MoveWindow(monthView, 0, 0, rect.Right + 2, rect.Bottom, true);
				base.OnDropDown(e);
		}
		#endregion

		#region Public Methoden
		public void Clear()
		{
			Value = null;
		}
		#endregion

		public int Column { get; set; }
	}
}
