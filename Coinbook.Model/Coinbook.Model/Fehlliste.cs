using Coinbook.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class Fehlliste
    {
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int GebietID { get; set; }
        public string GUID { get; set; }
        public string KatNr { get; set; }
        public string Waehrung { get; set; }
        public string Nominal { get; set; }
        public string Motiv { get; set; }
        public string Jahrgang { get; set; }
        public string Muenzzeichen { get; set; }
        public decimal? SPreis { get; set; }
        public decimal? SPPreis { get; set; }
        public decimal? SSPreis { get; set; }
        public decimal? SSPPreis { get; set; }
        public decimal? VZPreis { get; set; }
        public decimal? VZPPreis { get; set; }
        public decimal? STNPreis { get; set; }
        public decimal? STHPreis { get; set; }
        public decimal? PPPreis { get; set; }
        [Ignore]
        public enmColorFlag Farbe { get; set; }

    }
}