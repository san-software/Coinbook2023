using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SAN.Converter;

namespace SAN.UI.Controls
{
	/// <summary>
	/// Summary description for ComboBoxEx.
	/// </summary>
	public delegate void AfterSelectEventHandler();

	public class ComboBoxEx : System.Windows.Forms.ComboBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;
		System.Drawing.ContentAlignment alignment = System.Drawing.ContentAlignment.TopLeft;

		private bool clear = false;
		private bool showClipBoard = true;
		private bool readOnly = false;
		private ComboBoxStyle dropDownStyle = ComboBoxStyle.DropDownList;

		private ColType columnType = ColType.SingleColumn;
		private DataTable dataTable = null;
		private DataRow selectedRow = null;
		private string columnsToDisplay = "";
		private string displayValue = "";
		private DataRow[] dataRows = null;
		private ColumnHeaderStyle headerStyle = ColumnHeaderStyle.None;
		private bool gridLinesMultiColumn = false;
		private int maxDropDownItems = 10;
		private bool dontDoEvent = false;

		//private struct ItemInt
		//{
		//  public int key;
		//  public string value;
		//}

		//private struct ItemString
		//{
		//  public string key;
		//  public string value;
		//}

		public event AfterSelectEventHandler AfterSelectEvent;

		public enum ColType
		{
			SingleColumn,
			MultiColumn
		}

		public ComboBoxEx()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			setColor();
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

		#region Overidden stuff

		protected override void OnDropDown(System.EventArgs e)
		{
			if (columnType == ColType.MultiColumn)
			{
				if(this.dataTable != null || this.dataRows!= null)
				{
					Point p = this.PointToScreen(new Point(0, this.Bottom - 8));
					ComboBoxMultiColPopup popup = new ComboBoxMultiColPopup(this.dataTable,ref this.selectedRow,columnsToDisplay);
					popup.AfterRowSelectEvent+=new AfterRowSelectEventHandler(MultiColumnComboBox_AfterSelectEvent);
					//popup.Location = new Point(parent.Left + this.Left + 4, parent.Top + this.Bottom + this.Height);
					popup.Location = new Point(p.X, p.Y);
					popup.Width = base.DropDownWidth;
					popup.HeaderStyle = headerStyle;
					//popup.MaxDropDownItems = base.MaxDropDownItems;
					//base.MaxDropDownItems = 1;
					popup.MaxDropDownItems = maxDropDownItems;
					popup.DisplayMember = DisplayMember;
					popup.BringToFront();
					popup.Show();
					if(popup.SelectedRow!=null)
					{
						try
						{
							this.selectedRow = popup.SelectedRow;
							this.displayValue = popup.SelectedRow[DisplayMember].ToString();
							this.Text = this.displayValue;
						}
						catch(Exception e2) 
						{
							MessageBox.Show(e2.Message,"Error");	
						}
					}
					if(AfterSelectEvent!=null)
						AfterSelectEvent();
				}
			}
			else
				base.OnDropDown(e);
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
	
		protected override void OnContextMenuChanged(EventArgs e)
		{
			base.OnContextMenuChanged(e);
		}

		protected override void OnVisibleChanged(EventArgs pe)
		{
			//dontDoEvent = true;
			//if (this.Visible && clear)
			//  base.SelectedIndex = -1;

			//base.OnVisibleChanged(pe);			// Calling the base class 
			//dontDoEvent = false;
		}

		protected override void OnSelectionChangeCommitted(EventArgs pe)
		{
			if (!dontDoEvent)
			{
				clear = (base.SelectedIndex == -1);
				base.OnSelectionChangeCommitted(pe);
			}
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (!dontDoEvent)
			{
				if (base.SelectedIndex != -1)
					base.OnSelectedIndexChanged(e);
				//}
				//else
				//  selectedRow = null;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)  
		{ 
			if(readOnly && (
				e.KeyCode == Keys.Up  || 
				e.KeyCode == Keys.Down  ||
				e.KeyCode == Keys.Delete))
				e.Handled = true;
			else
				base.OnKeyDown (e);
		}

		protected override void OnKeyPress(KeyPressEventArgs e) 
		{
			if(readOnly) 
				e.Handled = true;
			else
				base.OnKeyPress (e);
		}

		#endregion

		#region Sonstiges
		public void Clear()
		{
			if (base.Items.Count > 0)
				base.SelectedIndex = 0;
			base.SelectedIndex = -1;
			clear = true;
		}

		public int xFindIndex(long id)
		{

			DataRowView dataRowView;
			DataRow dataRow;
			int result = -1;

			for (int counter=0;counter <this.Items.Count;counter++)
			{
				dataRowView = (DataRowView)this.Items[counter];
				dataRow = dataRowView.Row;
				if (Convert.ToInt16(dataRow[0,DataRowVersion.Original]) == id)
					result=counter;
			}

			return result;
		}

		public int FindValue(string value)
		{
			DataRowView dataRowView;
			int result = -1;
			string temp;

			value = value.ToLower();

			for (int counter=0;counter <this.Items.Count;counter++)
			{
				dataRowView = (DataRowView)this.Items[counter];
				temp =Convert.ToString(dataRowView.Row[1,DataRowVersion.Original]);

				if (temp.ToLower() == value) result=counter;
			}

			return result;
		}

		public int FindValue(Single value, int col)
		{
			DataRowView dataRowView;
			int result = -1;

			for (int counter=0;counter <this.Items.Count;counter++)
			{
				dataRowView = (DataRowView)this.Items[counter];
				if (value == (Single)dataRowView.Row[col])
				{
					result = counter;
					break;
				}
			}

			return result;
		}

		public int FindID(long value)
		{
			int result = -1;

			for (int counter = 0; counter < this.Items.Count; counter++)
			{
				if (Convert.ToInt16(((DataRowView)this.Items[counter]).Row[0]) == value)
					result = counter;
			}

			return result;
		}

		public string FindValueFromID(long value)
		{
			string result = "";

			for (int counter = 0; counter < this.Items.Count; counter++)
			{
				if (Convert.ToInt16(((DataRowView)this.Items[counter]).Row[0]) == value)
					result = ((DataRowView)this.Items[counter]).Row[1].ToString();
			}

			return result;
		}

		public int FindValueExact(string value)
		{
			DataRowView dataRowView;
			DataRow dataRow;
			int result = -1;

			for (int counter=0;counter <this.Items.Count;counter++)
			{
				dataRowView = (DataRowView)this.Items[counter];
				dataRow = dataRowView.Row;
				if (Convert.ToString(dataRow[1,DataRowVersion.Original]) == value)
					result=counter;
			}

			return result;
		}

		private void setColor()
		{
			if (ReadOnly)
			{
				base.BackColor = Farbverwaltung.DisplayBackColor;
				base.ForeColor = Farbverwaltung.DisplayForeColor;
			}
			else
			{
				base.BackColor = Farbverwaltung.InputBackColor;
				base.ForeColor = Farbverwaltung.InputForeColor;
				if (IsPflichtfeld)
				{
					base.BackColor = Farbverwaltung.PflichtBackColor;
					base.ForeColor = Farbverwaltung.PflichtForeColor;
				}
			}
		}

		#endregion

		#region Properties

		public bool IsPflichtfeld
		{
			get;
			set;
		}

		[Browsable(false)]
		public bool HasContent
		{
			get
			{
				return (Text.Length != 0);
			}
		}

		[Browsable(false)]
		public long ID
		{
			get
			{
				if (this.SelectedIndex > -1)
					return ConvertEx.ToInt64(((DataRowView)this.Items[this.SelectedIndex]).Row[0]);
				else
					return -1;
			}

			set
			{
				DataRowView dataRowView;
				int result = -1;

				for (int counter=0;counter <this.Items.Count;counter++)
				{
					dataRowView = this.Items[counter] as DataRowView;
					if (dataRowView != null)
						if (Convert.ToInt64(dataRowView.Row[0]) == value)
							result = counter;
				}
				dontDoEvent = true;
				this.SelectedIndex = result;
				dontDoEvent = false;
				clear = (result == -1);
			}
		}

		[Browsable(false)]
		public object IDObject
		{
			get
			{
				if (this.SelectedIndex > -1)
					return ((DataRowView)this.Items[this.SelectedIndex]).Row[0];
				else
					return -1;
			}

			set
			{
				DataRowView dataRowView;
				int result = -1;

				for (int counter = 0; counter < this.Items.Count; counter++)
				{
					dataRowView = this.Items[counter] as DataRowView;
					if (dataRowView != null)
						if (dataRowView.Row[0] == value)
							result = counter;
				}
				this.SelectedIndex = result;
				clear = (result == -1);
			}
		}

		[Browsable(false)]
		public string IDString
		{
			get
			{
				if (this.SelectedIndex > -1)
					return ((DataRowView)this.Items[this.SelectedIndex]).Row[0].ToString();
				else
					return "";
			}

			set
			{
				int result = -1;

				for (int counter = 0; counter < this.Items.Count; counter++)
					if (((DataRowView)this.Items[counter]).Row[0].ToString() == value)
						result = counter;

				this.SelectedIndex = result;
				clear = (result == -1);
			}
		}

		[Category("Appearance")]
		public bool ShowClipBoard
		{
			get {return showClipBoard;}
			set {showClipBoard=value;}
		}

		[Category("Appearance")]
		public System.Drawing.ContentAlignment TextAlign
		{
			set{alignment = value;}
			get{return alignment;}
		}
		
		[Category("Behavior")]
		public bool ReadOnly
		{
			get {return readOnly ;}
			set 
			{
				readOnly = value;

				if(value)
				{
					dropDownStyle = base.DropDownStyle;
					base.DropDownStyle = ComboBoxStyle.Simple;
				}
				else
					base.DropDownStyle = dropDownStyle;

				setColor();
			}
		}

		public new ComboBoxStyle DropDownStyle
		{
			get {return base.DropDownStyle;}
			set
			{
				base.DropDownStyle = value;
				dropDownStyle = value;
			}
		}

		public new object DataSource
		{
			get
			{
				if (columnType == ColType.SingleColumn)
					return base.DataSource;
				else
					return dataTable;
			}

			set
			{
				dontDoEvent = true;
				base.DataSource = value;
				if (columnType == ColType.MultiColumn)
				{
					switch ( value.GetType().Name)
					{
						case "DataTable":
							dataTable = (DataTable)value;
							if (dataTable !=null)
								selectedRow=dataTable.NewRow();
							break;
						case "DataSet":
							dataTable = ((DataSet)value).Tables[0];
							if (dataTable !=null)
								selectedRow=dataTable.NewRow();
							break;
						case "DataRow":
							dataRows = (DataRow[])value;
							break;
						default:
							columnType = ColType.SingleColumn;
							break;
					}
				}
				dontDoEvent = false;
			}
		}

		[Category("Appearance")]
		public ColType ColumnType
		{
			get {return columnType;}
			set {columnType = value;}
		}

		[Browsable(false)]
		public DataRow SelectedRow
		{
			get
			{
				selectedRow = null;

				if (base.SelectedIndex != -1)
				{
					switch (DataSource.GetType().Name)
					{
						case "DataTable":
							dataTable = (DataTable)base.DataSource;
							if (dataTable != null)
								selectedRow = dataTable.Rows[base.SelectedIndex];
							break;
						case "DataSet":
							dataTable = ((DataSet)base.DataSource).Tables[0];
							if (dataTable != null)
								selectedRow = dataTable.Rows[base.SelectedIndex];
							break;
						case "DataRow":
							dataRows = (DataRow[])base.DataSource;
							selectedRow = dataRows[base.SelectedIndex];
							break;
					}
				}
				return selectedRow;
			}
		}

		[Category("Appearance")]
		public string ColumnsToDisplay
		{
			set	{columnsToDisplay = value;}
			get {return columnsToDisplay;}
		}
	
		[Browsable(false)]
		public string DisplayValue
		{
			get	{return displayValue;}
		}

		public ColumnHeaderStyle HeaderStyle
		{
			get {return headerStyle;}
			set {headerStyle = value;}
		}

		public bool GridLinesMultiColumn
		{
			get {return gridLinesMultiColumn;}
			set {gridLinesMultiColumn = value;}
		}

		#endregion

		#region Events
		private void MultiColumnComboBox_AfterSelectEvent(object sender, string text, int selectedIndex)
		{
			this.Text = text;
			base.SelectedIndex = selectedIndex;
		}

		#endregion

		public object Value(string col)
		{
			return ((DataRowView)SelectedItem).Row[col];
		}

		public new int MaxDropDownItems
		{
			set
			{
				maxDropDownItems = value;
				if (ColumnType == ColType.MultiColumn)
					base.MaxDropDownItems = 1;
				else
					base.MaxDropDownItems = value;
			}

			get
			{
				return maxDropDownItems;
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

	}
}