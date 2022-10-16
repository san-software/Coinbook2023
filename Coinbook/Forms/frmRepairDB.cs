using Coinbook.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coinbook.Helper;

namespace Coinbook
{
    public partial class frmRepairDB : Form
    {
        public frmRepairDB()
        {
            InitializeComponent();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<string, Bestand> dictionary = new Dictionary<string, Bestand>();

            DatabaseHelper.LiteDatabase.ClearCollection("tblBestand", "Sammlung");

            var sammlung = DatabaseHelper.LiteDatabase.ReadSammlungShort();

            bgw.ReportProgress(0, sammlung.Count);

            int i = 0;
            foreach (var item in sammlung)
            {
                i++;
                bgw.ReportProgress(i);

                if (!dictionary.ContainsKey(item.Guid))
                {
                    var coin = DatabaseHelper.LiteDatabase.GetCoinFromGuid(item.Guid,CoinbookHelper.ModulKey);
                    if (coin == null)
                        continue;

                    Bestand bestand = new Bestand();
                    bestand.Guid = item.Guid;
                    bestand.NationID = coin.NationID;
                    bestand.AeraID = coin.AeraID;
                    bestand.GebietID = coin.RegionID;
                    dictionary.Add(item.Guid, bestand);
                }

                if (!item.Doublette)
                {
                    switch (item.Erhaltung)
                    {
                        case 1: dictionary[item.Guid].S += 1; break;
                        case 2: dictionary[item.Guid].SP += 1; break;
                        case 3: dictionary[item.Guid].SS += 1; break;
                        case 4: dictionary[item.Guid].SSP += 1; break;
                        case 5: dictionary[item.Guid].VZ += 1; break;
                        case 6: dictionary[item.Guid].VZP += 1; break;
                        case 7: dictionary[item.Guid].STN += 1; break;
                        case 8: dictionary[item.Guid].STH += 1; break;
                        case 9: dictionary[item.Guid].PP += 1; break;
                    }
                }
                else
                {
                    switch (item.Erhaltung)
                    {
                        case 1: dictionary[item.Guid].DS += 1; break;
                        case 2: dictionary[item.Guid].DSP += 1; break;
                        case 3: dictionary[item.Guid].DSS += 1; break;
                        case 4: dictionary[item.Guid].DSSP += 1; break;
                        case 5: dictionary[item.Guid].DVZ += 1; break;
                        case 6: dictionary[item.Guid].DVZP += 1; break;
                        case 7: dictionary[item.Guid].DSTN += 1; break;
                        case 8: dictionary[item.Guid].DSTH += 1; break;
                        case 9: dictionary[item.Guid].DPP += 1; break;
                    }
                }
            }

            var xxx = dictionary.Values.ToList();

            DatabaseHelper.LiteDatabase.BulkInsertBestand(dictionary.Values.ToList());
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
                progress.Maximum = Convert.ToInt32(e.UserState);
            else
                progress.Value = e.ProgressPercentage;
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CoinbookHelper.Changes = true;
            MessageBox.Show("Datenbank wurde repariert");
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bgw.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
