/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Marcel
 * Datum: 05.07.2011
 * Zeit: 15:52
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using Coinbook.Lokalisierung;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Coinbook
{
	/// <summary>
	/// Description of frmInfo.
	/// </summary>
	public partial class frmInfo : Form 
	{
		public frmInfo()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);
		}
		
	public string Info {get;set;}

	public new void ShowDialog(IWin32Window window)
{
		txtInfo.Text = Info;

			this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width,Screen.PrimaryScreen.WorkingArea.Height - this.Height);

			base.ShowDialog();
		}
	}
}
