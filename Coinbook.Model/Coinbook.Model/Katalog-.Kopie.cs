using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class Katalog
    {
        public const string Table = "tblKatalog";

        [Ignore]
        public string btnEdit { get; set; }
        public string KatNr { get; set; }
        public string Waehrung { get; set; }
        public string Nominal { get; set; }
        public string Motiv { get; set; }
        public string Jahrgang { get; set; }
        public string Muenzzeichen { get; set; }

        [Ignore]
        public string Hinweis { get; set; }
        public string Auflage { get; set; }
        public string AuflageSTH { get; set; }
        public string AuflagePP { get; set; }
        public int ID { get; set; }
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int GebietID { get; set; }
        public decimal SPreis { get; set; }
        public decimal SPPreis { get; set; }
        public decimal SSPreis { get; set; }
        public decimal SSPPreis { get; set; }
        public decimal VZPreis { get; set; }
        public decimal VZPPreis { get; set; }
        public decimal STNPreis { get; set; }
        public decimal STHPreis { get; set; }
        public decimal PPPreis { get; set; }
        public decimal Gewicht { get; set; }
        public decimal Durchmesser { get; set; }
        public decimal Dicke { get; set; }
        public string GUID { get; set; }
        public string AusserKurs { get; set; }
        public string InKurs { get; set; }
        public string Gepraegt { get; set; }
        public string Kommentar { get; set; }
        public string Rand { get; set; }
        public string Ausgabeanlass { get; set; }
        public string Material { get; set; }
        public string Legierung { get; set; }
        public string Picture { get; set; }
        public string Typ { get; set; }
        public string Referenz { get; set; }
        public string Aversbeschreibung { get; set; }
        public string Besonderheit { get; set; }
        public string Reversbeschreibung { get; set; }
        public string AehnlicheMotive { get; set; }
        public string AversEntwurf { get; set; }
        public string ReversEntwurf { get; set; }
        public string Form { get; set; }
        public string Orientation { get; set; }
        public string LPStandS { get; set; }
        public string LPStandSP { get; set; }
        public string LPStandSS { get; set; }
        public string LPStandSSP { get; set; }
        public string LPStandVZ { get; set; }
        public string LPStandVZP { get; set; }
        public string LPStandSTN { get; set; }
        public string LPStandSTH { get; set; }
        public string LPStandPP { get; set; }
        public string Praegeort { get; set; }
        public bool LPS { get; set; }
        public bool LPSP { get; set; }
        public bool LPSS { get; set; }
        public bool LPSSP { get; set; }
        public bool LPVZ { get; set; }
        public bool LPVZP { get; set; }
        public bool LPSTN { get; set; }
        public bool LPSTH { get; set; }
        public bool LPPP { get; set; }
        public bool AusserkursBOOL { get; set; }
        
        [Ignore]
        public string HinweisKZ { get; set; }
        [Ignore]
        public string Farbe { get; set; }
        [Ignore]
        public string OriginalKatNr { get; set; }
        [Ignore]
        public string OwnPicture { get; set; }
        [Ignore]
        public string S { get; set; }

        [Ignore]
        public string SP { get; set; }

        [Ignore]
        public string SS { get; set; }

        [Ignore]
        public string SSP { get; set; }

        [Ignore]
        public string VZ { get; set; }

        [Ignore]
        public string VZP { get; set; }

        [Ignore]
        public string STN { get; set; }

        [Ignore]
        public string STH { get; set; }

        [Ignore]
        public string PP { get; set; }

        [Ignore]
        public string SummeS { get; set; }

        [Ignore]
        public string SummePP { get; set; }
        public string Bearbeitungsdatum { get; set; }
    }
}