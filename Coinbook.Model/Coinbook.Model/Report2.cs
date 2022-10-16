using Coinbook.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class Report2
    {
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int GebietID { get; set; }
        public string Waehrung { get; set; }
        public string Nominal { get; set; }
        public string Jahrgang { get; set; }
        public string Muenzz { get; set; }
        public string Motiv { get; set; }
        public string KatNr { get; set; }
        public string Erhaltung { get; set; }
        public string Guid { get; set; }
        public int Anzahl { get; set; }
        public decimal Preis { get; set; }
        public decimal Gesamt { get; set; }
        public enmColorFlag Farbe { get; set; }
    }
}
