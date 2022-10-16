using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class MünzDetail
	{
        public string ID { get; set; }
        public string GUID { get; set; }
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int GebietID { get; set; }
        public string Form { get; set; }
        public string Orientation { get; set; }
        public string KatNr { get; set; }
        public string Jahrgang { get; set; }
        public string Muenzzeichen { get; set; }
        public string Material { get; set; }
        public string Legierung { get; set; }
        public string AusserKurs { get; set; }
        public string InKurs { get; set; }
        public string Gepraegt { get; set; }
        public decimal Gewicht { get; set; }
        public string Durchmesser { get; set; }
        public decimal Dicke { get; set; }
        public bool AusserKursBool { get; set; }
        public string Bearbeitungsdatum { get; set; }
        public string Referenz { get; set; }
        public string LPStandS { get; set; }
        public string LPStandSP { get; set; }
        public string LPStandSS { get; set; }
        public string LPStandSSP { get; set; }
        public string LPStandVZ { get; set; }
        public string LPStandVZP { get; set; }
        public string LPStandSTN { get; set; }
        public string LPStandSTH { get; set; }
        public string LPStandPP { get; set; }
        public bool LPS { get; set; }
        public bool LPSP { get; set; }
        public bool LPSS { get; set; }
        public bool LPSSP { get; set; }
        public bool LPVZ { get; set; }
        public bool LPVZP { get; set; }
        public bool LPSTN { get; set; }
        public bool LPSTH { get; set; }
        public bool LPPP { get; set; }
        public bool AusserkursFlagL { get; set; }
        public string Copyright { get; set; }

    }
}
