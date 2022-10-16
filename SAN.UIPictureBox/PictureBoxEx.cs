using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace SAN.Control
{
	/// <summary>
	/// Summary description for ComboBoxEx.
	/// </summary>
	public class PictureBoxEx : PictureBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;

		public PictureBoxEx()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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

		[Category("Behavior")]
		public string PictureName { get; set; }

		#endregion

	}
}