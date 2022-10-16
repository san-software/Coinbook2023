using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class CoinBestand
    {
        public CoinBestand()
        {
            SPreis = 0;
            SPPreis = 0;
            SSPreis = 0;
            SSPPreis = 0;
            VZPreis = 0;
            VZPPreis = 0;
            STNPreis = 0;
            STHPreis = 0;
            PPPreis = 0;
            LPS = false;
            LPSP = false;
            LPSS = false;
            LPSSP = false;
            LPVZ = false;
            LPVZP = false;
            LPSTN = false;
            LPSTH = false;
            LPPP = false;
        }

        public string S { get; set; }
        public string SP { get; set; }
        public string SS { get; set; }
        public string SSP { get; set; }
        public string VZ { get; set; }
        public string VZP { get; set; }
        public string STN { get; set; }
        public string STH { get; set; }
        public string PP { get; set; }
        public string SummeS { get; set; }
        public string SummePP { get; set; }
        public string Farbe { get; set; }
        public string GUID { get; set; }
        public decimal SPreis { get; set; }
        public decimal SPPreis { get; set; }
        public decimal SSPreis { get; set; }
        public decimal SSPPreis { get; set; }
        public decimal VZPreis { get; set; }
        public decimal VZPPreis { get; set; }
        public decimal STNPreis { get; set; }
        public decimal STHPreis { get; set; }
        public decimal PPPreis { get; set; }
        public bool LPS { get; set; }
        public bool LPSP { get; set; }
        public bool LPSS { get; set; }
        public bool LPSSP { get; set; }
        public bool LPVZ { get; set; }
        public bool LPVZP { get; set; }
        public bool LPSTN { get; set; }
        public bool LPSTH { get; set; }
        public bool LPPP { get; set; }
    }
}