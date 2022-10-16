using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class Katalog3
    {
        public Katalog3()
        {
            GUID = string.Empty;
            btnEdit = string.Empty;
            KatNr = string.Empty;
            Waehrung = string.Empty;
            Nominal = string.Empty;
            Motiv = string.Empty;
            Jahrgang = string.Empty;
            Muenzzeichen = string.Empty;
            HinweisKZ = string.Empty;
            Auflage = string.Empty;
            AuflageSTH = string.Empty;
            AuflagePP = string.Empty;
            S = string.Empty;
            SP = string.Empty;
            SS = string.Empty;
            SSP = string.Empty;
            VZ = string.Empty;
            VZP = string.Empty;
            STN = string.Empty;
            STH = string.Empty;
            PP = string.Empty;
            SummeS = string.Empty;
            SummePP = string.Empty;
            Farbe = string.Empty;
            NationID = 0;
            AeraID = 0;
            RegionID = 0;
            SPreis = 0;
            SPPreis = 0;
            SSPreis = 0;
            SSPPreis = 0;
            VZPreis = 0;
            VZPPreis = 0;
            STNPreis = 0;
            STHPreis = 0;
            PPPreis = 0;
            Picture = string.Empty;
            LPS = false;
            LPSP = false;
            LPSS = false;
            LPSSP = false;
            LPVZ = false;
            LPVZP = false;
            LPSTN = false;
            LPSTH = false;
            LPPP = false;
            OwnPicture = string.Empty;
            OriginalKatNr = string.Empty;

        }

        [BsonIgnore]
        public string btnEdit { get; set; }
        public string KatNr { get; set; }
        public string Waehrung { get; set; }
        public string Nominal { get; set; }
        public string Motiv { get; set; }
        public string Jahrgang { get; set; }
        public string Muenzzeichen { get; set; }
        public string HinweisKZ { get; set; }
        public string Auflage { get; set; }
        public string AuflageSTH { get; set; }
        public string AuflagePP { get; set; }
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
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int RegionID { get; set; }
        [BsonId]
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
        public string OwnPicture { get; set; }
        public string OriginalKatNr { get; set; }

        [BsonIgnore]
        public bool Selected { get; set; }
        public string Copyright { get; set; }

    }
}