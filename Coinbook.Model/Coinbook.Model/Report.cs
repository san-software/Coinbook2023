using Coinbook.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class Report
    {
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int GebietID { get; set; }
        public string Waehrung { get; set; }
        public string Nominal { get; set; }
        public string Jahrgang { get; set; }
        public string Muenzz { get; set; }
        public string GUID { get; set; }
        public int S { get; set; }
        public decimal SPreis { get; set; }
        public int SP { get; set; }
        public decimal SPPreis { get; set; }
        public int SS { get; set; }
        public decimal SSPreis { get; set; }
        public int SSP { get; set; }
        public decimal SSPPreis { get; set; }
        public int VZ { get; set; }
        public decimal VZPreis { get; set; }
        public int VZP { get; set; }
        public decimal VZPPreis { get; set; }
        public int STN { get; set; }
        public decimal STNPreis { get; set; }
        public int STH { get; set; }
        public decimal STHPreis { get; set; }
        public int PP { get; set; }
        public decimal PPPreis { get; set; }
        public decimal Gesamt { get; set; }
        public string KatNr { get; set; }
        public enmColorFlag Farbe { get; set; }

    }
}
