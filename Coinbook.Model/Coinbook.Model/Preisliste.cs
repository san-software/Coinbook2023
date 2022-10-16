using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Preisliste
	{
		public const string Table = "tblPreisliste";
		public string Bestellnummer { get; set; }
		public string Modul { get; set; }
		public Decimal Preis { get; set; }
		public string Waehrung { get; set; }
		public string Jahr { get; set; }
	}
}
