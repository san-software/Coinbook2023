using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class Katalog2
    {
        public string ID { get; set; }
        public string GUID { get; set; }
        public string KatNr { get; set; }
        public string Waehrung { get; set; }
        public string Nominal { get; set; }
        public string Motiv { get; set; }
        public string Jahrgang { get; set; }
        public string Muenzzeichen { get; set; }
        public string Auflage { get; set; }
        public string AuflageSTH { get; set; }
        public string AuflagePP { get; set; }
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int RegionID { get; set; }
        public decimal SPreis { get; set; }
        public decimal SPPreis { get; set; }
        public decimal SSPreis { get; set; }
        public decimal SSPPreis { get; set; }
        public decimal VZPreis { get; set; }
        public decimal VZPPreis { get; set; }
        public decimal STNPreis { get; set; }
        public decimal STHPreis { get; set; }
        public decimal PPPreis { get; set; }
        public string Picture { get; set; }
        public bool LPS { get; set; }
        public bool LPSP { get; set; }
        public bool LPSS { get; set; }
        public bool LPSSP { get; set; }
        public bool LPVZ { get; set; }
        public bool LPVZP { get; set; }
        public bool LPSTN { get; set; }
        public bool LPSTH { get; set; }
        public bool LPPP { get; set; }
        public string HinweisKZ { get; set; }
        public string OwnPicture { get; set; }
        public string OriginalKatNr { get; set; }
        public string Copyright { get; set; }

    }
}