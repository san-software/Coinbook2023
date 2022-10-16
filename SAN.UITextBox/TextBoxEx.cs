using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SAN.Converter;
using System.Drawing;

namespace SAN.Control
{
	/// <summary>
	/// Summary description for TextBoxEx.
	/// </summary>
	public class TextBoxEx : System.Windows.Forms.TextBox
	{
		#region Windows-API
		/* Deklaration der benötigten API-Funktion für Cursorverwaltung*/
		[DllImport("User32.Dll")]
		private static extern int SendMessage(IntPtr hWnd, int msg,	int wParam, int lParam);

		const int EM_LINEINDEX = 0xBB;
		const int EM_LINEFROMCHAR = 0xC9;
		#endregion

		#region Deklarationen
		private System.ComponentModel.Container components = null;
		private TextBoxTyp typ = TextBoxTyp.Text;
		private short nachkommaStellen = 0;
		private bool showClipBoard = true;
		private bool isValid = false;
		private string translation = "";
		private string regExpression = "";
		private bool enter2Tab = true;
		private bool sendChangeEvent = true;
		public event EventHandler OnEnterKey;
		public new event EventHandler TextChanged;
		public event EventHandler TextFilled;

		#endregion

		//public delegate void TextBoxExEventHandler(Object sender, bool e); 

		public TextBoxEx()
		{
			InitializeComponent();

			this.AcceptsReturn = true;
			this.AcceptsTab = true;

			isValid = true;
			setColor();

			//base.ContextMenu = MenueHelper.Context;
			base.TextChanged += new EventHandler(TextBoxEx_TextChanged);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.TextChanged -= new EventHandler(TextBoxEx_TextChanged);

			if (disposing)
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

		#region Checks
		private bool checkCharNumeric(int keyCode)
		{
			bool result = true;

			if (keyCode < 43 || keyCode > 57)
				result =false;
			else
			{
				if (keyCode == 46 || keyCode == 47)
					result =false;
			}

			if (nachkommaStellen == 0 && keyCode == 44)
				result = false;

			if (keyCode == 8)
				result = true;

			return result;
		}
		#endregion

		#region overriddens stuff

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			string text = base.Text;

			switch ((Keys)e.KeyChar)
			{
				case Keys.Enter: //13
					if (OnEnterKey != null)
						OnEnterKey(this, new EventArgs());

					if (Enter2Tab)
					{
						e.Handled = true;
						SendKeys.Send("{tab}");
					}
					break;

				case Keys.Back: //8
					if (ColumnIndex > 0)
					{
						if (ColumnIndex < text.Length - base.SelectionLength)
						{
							if (base.SelectionLength !=0)
								text = text.Substring(0,ColumnIndex) + text.Substring(ColumnIndex + base.SelectionLength);
							else
								text = text.Substring(0,ColumnIndex-1) + text.Substring(ColumnIndex);
						}
						else
						{
							if (base.SelectionLength !=0)
								text = text.Substring(0,ColumnIndex);
							else
								text = text.Substring(0,ColumnIndex-1);
						}
					}

					if (!checkInput(text,typ,nachkommaStellen))
					{
						e.Handled = true;
						text = text.Substring(0,text.Length-1);
					}
					isValid = Helper.CheckValid(text,typ);
					setColor();
					break;

				default:
					if (ColumnIndex > 0)
					{
						if (ColumnIndex < text.Length - base.SelectionLength)
						{
							if (base.SelectionLength !=0)
								text = text.Substring(0,ColumnIndex) + e.KeyChar.ToString() + text.Substring(ColumnIndex + base.SelectionLength);
							else
								text = text.Substring(0,ColumnIndex) + e.KeyChar.ToString() + text.Substring(ColumnIndex);
						}
						else
						{
							if (base.SelectionLength !=0)
								text = text.Substring(0,ColumnIndex) +  e.KeyChar.ToString();
							else
								text = text +  e.KeyChar.ToString();
						}
					}
					else
						text = e.KeyChar.ToString();

					if (!checkInput(text,typ,nachkommaStellen))
					{
						e.Handled = true;
						text = text.Substring(0,text.Length-1);
					}

					if (Typ == TextBoxTyp.User)
					{
						if (!Helper.IsValidUser(text,RegularExpression))
						{
							e.Handled = true;
							text = text.Substring(0, text.Length - 1);
						}
					}

					isValid = Helper.CheckValid(text,typ);
					setColor();
					break;
			}
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			string text = base.Text;
			switch (e.KeyCode)
			{
				case Keys.Delete:
					if (!checkInput(text,typ,nachkommaStellen))
					{
						e.Handled = true;
						text = text.Substring(0,text.Length-1);
					}
					isValid = Helper.CheckValid(text,typ);
					setColor();
					break;

				case Keys.Enter:
					//if (OnEnterKey != null)
					//  OnEnterKey(this, new EventArgs());
					//e.KeyCode = Keys.Tab;
					e.SuppressKeyPress = false;
					break;

				case Keys.Tab:
					e.SuppressKeyPress = false;
					break;

				case Keys.Down:
					if (!Multiline)
					{
						e.Handled = true;
						SendKeys.Send("{tab}");
					}
					break;

				case Keys.Up:
					if (!Multiline)
					{
						e.Handled = true;
						SendKeys.Send("+{tab}");
					}
					break;
			}

			base.OnKeyUp(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			string text = base.Text;
			switch (e.KeyCode)
			{
				//case Keys.Delete:
				//  if (!Helper.CheckInput(text, typ, nachkommaStellen))
				//  {
				//    e.Handled = true;
				//    text = text.Substring(0, text.Length - 1);
				//  }
				//  isValid = Helper.CheckValid(text, typ);
				//  setColor();
				//  break;

				case Keys.Enter:
					if (OnEnterKey != null)
						OnEnterKey(this, new EventArgs());
					e.SuppressKeyPress = false;
					break;

				case Keys.Down:
					if (OnEnterKey != null)
						OnEnterKey(this, new EventArgs());
					e.SuppressKeyPress = false;
					break;

				//case Keys.Tab:
				//  e.SuppressKeyPress = false;
				//  break;
			}

			base.OnKeyDown(e);
		}

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

		protected override void OnGotFocus(EventArgs e)
		{
			isValid = Helper.CheckValid(Text, typ);
			setColor();
			base.OnGotFocus(e);
		}

		protected override void OnLostFocus(EventArgs e)
		{
			if (typ == TextBoxTyp.Text && translation != "" && !ReadOnly)
				translate();
																		
			base.OnLostFocus(e);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			setColor();
		}

		protected override void OnContextMenuChanged(EventArgs e)
		{
			base.OnContextMenuChanged(e);
		}

		#endregion

		#region Properties
		[Browsable(false)]
		public string Number
		{
			get
			{
				double result = 0;

				if (typ == TextBoxTyp.Numeric)
				{
					if (this.Text.Length != 0)
						result = Convert.ToDouble(this.Text);
				}
				return result.ToString();
			}
		}

		// Index der aktuellen Zeile ermitteln
		[Browsable(false)]
		public int RowIndex
		{
			get {return SendMessage(this.Handle, EM_LINEFROMCHAR, -1, 0);}
		}
							
		// Start-Position der aktuellen Zeile ermitteln
		[Browsable(false)]
		public int RowStartIndex
		{
			get {return SendMessage(this.Handle,	EM_LINEINDEX, -1, 0);}
		}

		// Index der aktuellen Spalte berechnen
		[Browsable(false)]
		public int ColumnIndex
		{
			get {return base.SelectionStart-RowStartIndex;}
		}

		public override string Text
		{
			get {return base.Text;}
			set
			{
				sendChangeEvent = false;
				OldText = base.Text;

				if (NumberFormat != null && NumberFormat != "" && value != null && value != "")
					value = string.Format(NumberFormat, ConvertEx.ToDouble(value));

				base.Text = value;
				isValid = true;
				setColor();
				if (value != null)
				{
					if (value.Length != 0)
					{
						if (typ != TextBoxTyp.Text && typ != TextBoxTyp.NoCheck)
							isValid = Helper.CheckValid(value, typ);
						setColor();
					}
				}
				sendChangeEvent = true;

				if (TextFilled != null)
					TextFilled(this, null);
			}
		}

		[Browsable(false)]
		[ReadOnly(true)]
		public long TextLong
		{
			get
			{
				return ConvertEx.ToInt32(base.Text);
			}
			set
			{
				Text = value.ToString();
			}
		}

		[Browsable(false)]
		[ReadOnly(true)]
		public Double TextDouble
		{
			get
			{
				return ConvertEx.ToDouble(base.Text);
			}
			set
			{
				Text = value.ToString();
			}
		}
				
		public bool HasContent
		{
			get
			{
				return (Text.Length != 0);
			}
		}

		//public override void Clear()
		//{
		//  base.Clear();
		//  OldText = "";
		//}

		[Browsable(false)]
		public double NumberDouble
		{
			get
			{
				double result = 0;

				if (typ == TextBoxTyp.Numeric)
				{
					if (this.Text.Length != 0)
						result = Convert.ToDouble(this.Text);
				}
				return result;
			}
		}

		[Category("Appearance")]
		public bool ShowClipBoard
		{
			get {return showClipBoard;}
			set {showClipBoard=value;}
		}

		[Category("Appearance")]
		public TextBoxTyp Typ
		{
			set {typ=value;}
			get {return typ;}
		}

		[Category("Appearance")]
		public short NachkommaStellen
		{
			set{nachkommaStellen = value;}
			get{return nachkommaStellen;}
		}
		
		//public new bool ReadOnly
		//{
		//  get {return base.ReadOnly;}
		//  set
		//  {
		//    base.ReadOnly = value;
		//  }
		//}

		public bool IsPflichtfeld
		{
			get;
			set;
		}
		
		private void setColor()
		{
			//string boxTyp = "";

			if (ReadOnly)
			{
				base.BackColor = Color.White;
				base.ForeColor = Color.Black;
			}
			else
			{
				base.BackColor = Color.White;
				base.ForeColor = Color.Black;
				if (!isValid)
				{
					base.BackColor = Color.OrangeRed;
					base.ForeColor = Color.Black;
					//if (base.Focused && MenueHelper.HasStatusbar)  
					//{
					//  switch (typ)
					//  {
					//    case TextBoxTyp.Email:
					//      boxTyp = "Email";
					//      break;
					//    case TextBoxTyp.Telefon:
					//      boxTyp = "Telefon/Fax";
					//      break;
					//    case TextBoxTyp.Url:
					//      boxTyp = "Homepage";
					//      break;
					//  }
					//  MenueHelper.Statusbar.Status = "Keine gültige Eingabe für " + boxTyp;
					//}
				}
				if (IsPflichtfeld)
				{
					base.BackColor = Color.LightYellow;
					base.ForeColor = Color.Black;
				}
			}
		}

		public string Translation
		{
			get {return translation;}
			set {translation = value;}
		}

		#endregion

		private void translate()
		{
			int pos;
			string source;
			string replace;

			string[] array = translation.Split('|');
			for (int counter = 0; counter < array.Length; counter++)
			{
				pos = array[counter].IndexOf(";");
				source = array[counter].Substring(0,pos);
				replace = array[counter].Substring(pos+1);
				Text = Text.Replace(source,replace);
			}
		}

		public string RegularExpression
		{
			get {return regExpression;}
			set {regExpression = value;}
		}

		[Browsable(false)]
		public int IntValue
		{
			get
			{
				return ConvertEx.ToInt32(Text);
			}
		}

		[Browsable(false)]
		public double ValueDouble
		{
			get
			{
				Double result;
				Double.TryParse(Text, out result);

				return result;
			}
		}

		public override ContextMenu ContextMenu
		{
			get;
			set;
		}

		[Browsable(false)]
		public String OldText
		{
			get;
			set;
		}

		public bool Enter2Tab
		{
			get
			{
				return enter2Tab;
			}
			set
			{
				enter2Tab = value;
			}
		}

		private void TextBoxEx_TextChanged(object sender, EventArgs e)
		{
			if (sendChangeEvent && TextChanged != null)
				TextChanged(sender, e);
		}

		public int Column
		{
			get;
			set;
		}

		public int Row
		{
			get;
			set;
		}

		public string NumberFormat
		{
			get; set;
		}

		private bool checkInput(string text, TextBoxTyp typ, int nachkommaStellen)
		{
			bool result = true;
			char[] array;
			int slash = 0;
			int minus = 0;
			int notANumber = 0;

			switch (typ)
			{
				case TextBoxTyp.Numeric:
					result = isValidNumber(text, nachkommaStellen);
					break;

				case TextBoxTyp.Telefon:
					if (text == "")
						result = true;
					else
					{
						array = text.ToCharArray();
						for (int counter = 0; counter < array.Length; counter++)
						{
							if (array[counter] == '/')
								slash++;
							else if (array[counter] == '-')
								minus++;
							else if (!Char.IsNumber(array[counter]))
								notANumber++;
						}
						if (notANumber != 0 || slash > 1)
							result = false;
						else
							result = true;
					}
					break;
			}

			return result;
		}

		private bool isValidNumber(string text, int nachkommaStellen)
		{
			Single num;
			bool result = true;
			int nk = 0;

			try
			{
				num = Single.Parse(text);
			}
			catch
			{
				result = false;
			}

			if (text.Length == 0 || text == "+" || text == "-")
				result = true;

			text = text.Replace(".", ",");

			if (nachkommaStellen == 0 && text.IndexOf(",") > -1)
				result = false;

			if (text.IndexOf(",") != -1)
				nk = text.Length - text.IndexOf(",") - 1;

			if (nk > nachkommaStellen)
				result = false;

			return result;
		}
	}
}	
