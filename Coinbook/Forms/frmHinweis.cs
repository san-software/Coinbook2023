using Coinbook.Helper;
using Coinbook.Lokalisierung;
using LiteDB.Database;
using System;
using System.Windows.Forms;


namespace Coinbook
{
	public partial class frmHinweis : Form
	{
		public frmHinweis()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);
		}

		public string Guid { get; set; }
		public new void Show(IWin32Window window)
		{
			var hinweis = DatabaseHelper.LiteDatabase.GetHinweis(Guid, LanguageHelper.Localization.Language, CoinbookHelper.ModulKey);           
																			  
			txtBesonderheit.Text = hinweis.Besonderheit;
			txtKommentar.Text = hinweis.Kommentar;
			base.Show(window);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
