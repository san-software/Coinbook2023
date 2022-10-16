using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SAN.UI
{
	public partial class StatusBarEx : UserControl
	{
		private int statusWidth=0;
		private int recordsWidth=100;
		private int rolleWidth=90;
		private int dateWidth=70;
		private string dauerstatus = "";

		public StatusBarEx()
		{
			InitializeComponent();

			lblStatus.Text = "";
			lblRolle.Text = "";
			lblRecords.Text = "";
			lblDate.Text = "";

			progressbar.Visible = false;
			ProgressBarColor = Farbverwaltung.ProgressbarFillColor;
			progressbar.ForeColor = Farbverwaltung.ProgressbarForeColor;
		}

		private void StatusBarEx_SizeChanged(object sender, EventArgs e)
		{
			Height = 25;

			statusWidth = this.Width - recordsWidth - rolleWidth - dateWidth - 24;
			panStatus.Left = 0;
			panStatus.Width = statusWidth;
			panStatus.Top = 1;
			panStatus.Height = Height-2;

			panRecords.Left = statusWidth;
			panRecords.Width = recordsWidth;
			panRecords.Top = 1;
			panRecords.Height = Height-2;

			panRolle.Left = statusWidth + recordsWidth;
			panRolle.Width = rolleWidth;
			panRolle.Top = 1;
			panRolle.Height = Height-2;

			panDate.Left = statusWidth + recordsWidth + rolleWidth;
			panDate.Width = dateWidth;
			panDate.Top = 1;
			panDate.Height = Height-2;

			lblStatus.Left = 0;
			lblRolle.Left = 0;
			lblDate.Left = 0;
			lblRecords.Left = 0;

			lblStatus.Top = 3;
			lblRolle.Top = 3;
			lblDate.Top = 3;
			lblRecords.Top = 3;

			lblDate.Left = (panDate.Width - lblDate.Width) / 2;
			lblRecords.Left = (panRecords.Width - lblRecords.Width) / 2;
			lblRolle.Left = (panRolle.Width - lblRolle.Width) / 2;

			progressbar.Left = 0;
			progressbar.Top = 0;
			progressbar.Width = statusWidth;
			progressbar.Height = panStatus.Height-3;
		}

		#region Statusbar

		public string Status
		{
			set
			{
				lblStatus.Text = value;
				lblStatus.Visible = true;
				lblStatus.BringToFront();
				try
				{
					Application.DoEvents();
				}
				catch{}
			}
			get
			{
				return lblStatus.Text;
			}
		}

		public Color StatusBackColor
		{
			set
			{
				panStatus.BackColor = value;
			}
			get
			{
				return panStatus.BackColor;
			}
		}

		public Color StatusForeColor
		{
			set
			{
				lblStatus.ForeColor = value;
			}
			get
			{
				return lblStatus.ForeColor;
			}
		}

		public string DauerStatus
		{
			set
			{
				dauerstatus = value;
				lblStatus.Text = value;
			}
			get
			{
				return dauerstatus;
			}
		}

		public void Clear()
		{
			lblStatus.Text = dauerstatus;
			Application.DoEvents();
		}

		public long Records
		{
			set
			{
				lblRecords.Text = value.ToString() + " Datensätze";
				lblRecords.Left = (panRecords.Width - lblRecords.Width) / 2;
			}
		}

		public void RecordsClear()
		{
			lblRecords.Text = "";
		}

		public string Rolle
		{
			set
			{
				lblRolle.Text = value;
				lblRolle.Left = (panRolle.Width - lblRolle.Width) / 2;
			}
		}

		public void RolleClear()
		{
			lblRolle.Text = "";
		}

		public string Datum
		{
			set
			{
				lblDate.Text = value;
				lblDate.Left = (panDate.Width - lblDate.Width) / 2;
			}
		}

		//public Color ForeColor
		//{
		//  set
		//  {
		//    //barPanelStatus.Style = StatusBarPanelStyle.
		//  }
		//}



		//public  string ToolTipText
		//{
		//  set
		//  {
		//  }
		//}

		//public  string DauerToolTipText
		//{
		//  set
		//  {
		//    dauertooltip = value;
		//  }
		//}
		
		#endregion

		#region Progressbar

		public  int ProgressBarValue
		{
			set 
			{
				progressbar.Value = value;
				progressbar.Refresh();
			}
			get {return progressbar.Value;}
		}

		public  void ProgressBarClear()
		{
			progressbar.Value = 0;
			progressbar.Text = "";
			progressbar.Refresh();
		}

		public  bool ProgressBarVisible
		{
			set 
			{
				lblStatus.Visible = !value;
				progressbar.Visible = value;
				progressbar.Refresh();
			}
		}

		public  ProgressBarEx.ProgressbarStyleEx ProgressBarStyle
		{
			set
			{
				progressbar.Style = value;
			}
			get
			{
				return progressbar.Style;
			}
		}

		public  string ProgressBarText
		{
			set 
			{
				progressbar.Text = value;
				progressbar.Refresh();
			}
		}

		public  Color ProgressBackColor
		{
			set 
			{
				progressbar.BackColor = value;
				progressbar.Refresh();
			}
		}

		public  Color ProgressForeColor
		{
			set 
			{
				progressbar.ForeColor = value;
				progressbar.Refresh();
			}
		}

		public  Color ProgressBarColor
		{
			set 
			{
				progressbar.BarColor = value;
				//progressbar.BorderColor = value;
				progressbar.Refresh();
			}
		}

		public  ProgressBarEx.GradientMode ProgressGradientStyle
		{
			set 
			{
				progressbar.GradientStyle = value;
				progressbar.Refresh();
			}
		}
		
		public  byte ProgressBlockDistance
		{
			set 
			{
				progressbar.BlockDistance = value;
				progressbar.Refresh();
			}
		}

		public byte ProgressBlockWidth
		{
			set
			{
				progressbar.BlockWidth = value;
				progressbar.Refresh();
			}
		}

		public  System.Drawing.ContentAlignment ProgressAlignment
		{
			set
			{
				progressbar.TextAlignment = value;
				progressbar.Refresh();
			}
		}

		public bool ProgressShadow
		{
			set
			{
				progressbar.TextShadow = value;
				progressbar.Refresh();
			}
		}

		public  int ProgressBarMin
		{
			set 
			{
				progressbar.MinValue = value;
				progressbar.Refresh();
			}
			get {return progressbar.Value;}
		}

		public  int ProgressBarMax
		{
			set 
			{
				progressbar.MaxValue = value;
				progressbar.Refresh();
			}
			get {return progressbar.Value;}
		}

		#endregion
	}
}
