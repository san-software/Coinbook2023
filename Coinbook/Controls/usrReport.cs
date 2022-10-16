using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Coinbook.Enumerations;
using Coinbook.Helper;

namespace Coinbook
{
    partial class usrReport : UserControl
    {
			public event EventHandler Changed;
			private bool init = true;

        public usrReport()
        {
            InitializeComponent();
       }

        public void Init()
        {
            init = true;
            loadRes();

            //optLetzBen.Checked = Helper.Settings.RepLetztBen;
            if (!optLetzBen.Checked)
            {
                switch (CoinbookHelper.Settings.PrintDestination)
                {
                    case enmPrintDestination.Printer:
                        optPrint.Checked = true;
                        break;

                    case enmPrintDestination.HTML:
                        optHtml.Checked = true;
                        break;

                    case enmPrintDestination.CSV:
                        optCsv.Checked = true;
                        break;

                    default:
                        optPrint.Checked = true;
                        break;
                }
            }

            txtPath.Text = CoinbookHelper.Settings.ReportFolder;

            init = false;
        }

        #region Sprache

        /// <summary>
        /// Spracheinstellungen übernehmen und Texte laden (fals nicht deutsch)
        /// </summary>
        /// <param name="Helper.Settings.Cultur">Culture</param>
        private void loadRes()
        {
            if (CoinbookHelper.Settings.Culture != "de-DE")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(CoinbookHelper.Settings.Culture);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CoinbookHelper.Settings.Culture);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager(typeof(usrReport));

                //labels

                lblAusg.Text = rm.GetString("lblAusgText");
                optCsv.Text = rm.GetString("optCsvText");
                optHtml.Text = rm.GetString("optHtmlText");
                optLetzBen.Text = rm.GetString("optLetztBenText");
                optPrint.Text = rm.GetString("optPrintText");
                btnPath.Text = rm.GetString("butSearchText");

                Update();
            }
        }

        #endregion}

        private void butSearch_Click(object sender, EventArgs e)
        {
					dlgFolder.SelectedPath = txtPath.Text;

					if (dlgFolder.ShowDialog() == DialogResult.OK)
					{
						txtPath.Text = dlgFolder.SelectedPath;
						CoinbookHelper.Settings.ReportFolder = txtPath.Text;
						setChanged();
					}
        }

				private void optLetzBen_CheckedChanged(object sender, EventArgs e)
				{
					//Helper.Settings.RepLetztBen = optLetzBen.Checked;
					setChanged();
				}

				private void CheckedChanged(object sender, EventArgs e)
				{
					if (optPrint.Checked)
						CoinbookHelper.Settings.PrintDestination = enmPrintDestination.Printer;
					else if (optHtml.Checked)
						CoinbookHelper.Settings.PrintDestination = enmPrintDestination.HTML;
					else if (optCsv.Checked)
						CoinbookHelper.Settings.PrintDestination = enmPrintDestination.CSV;

					setChanged();
				}

				private void setChanged()
				{
					if (!init)
						if (Changed != null)
							Changed(null, null);
				}
    }
}
