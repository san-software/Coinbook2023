using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coinbook.Modulverwaltung
{
  public class Bestellung
  {
		public bool Bestellen { get; set; }
    public string Nummer { get; set; }
    public string Name { get; set; }
    public decimal Preis { get; set; }
    public string Währung { get; set; }
    public string Version { get; set; }
    public string Beschreibung { get; set; }
  }
}
