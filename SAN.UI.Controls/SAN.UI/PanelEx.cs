using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SAN.UI;

namespace SAN.UI.Controls
{
	public partial class PanelEx : Panel
	{
		private bool enabled = true;
		private Color backcolor = Farbverwaltung.BackColor;

		public PanelEx()
		{
			base.BackColor = backcolor;
			InitializeComponent();
		}

		 //Backcolor von Originalklasse überschreiben
		public new Color BackColor
		{
		  get
		  {
		    return backcolor;
		  }
		  set
		  {
				if (value == Color.Empty)
					value = Farbverwaltung.BackColor;

				backcolor = value;
		    base.BackColor = backcolor;
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

				foreach(Control c in base.Controls)
				{
					if (c.GetType().Name != "LabelEx" && c.GetType().Name != "Label")
						c.Enabled = value;
				}
			}
		}
	}
}
