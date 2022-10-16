using Coinbook.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Wertermittlung
	{
		public Wertermittlung()
		{
			Farbe = enmColorFlag.None;
		}

		public string Nation { get; set; }
		public int? S { get; set; }
		public int? SP { get; set; }
		public int? SS { get; set; }
		public int? SSP { get; set; }
		public int? VZ { get; set; }
		public int? VZP { get; set; }
		public int? STN { get; set; }
		public int? STH { get; set; }
		public int? PP { get; set; }
		public int Anzahl { get; set; }
		public decimal Gesamt { get; set; }
		public int NationID { get; set; }
		public enmColorFlag Farbe { get;set; }

	}
}
