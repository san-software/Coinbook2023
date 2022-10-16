using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class EigenerPreis
	{
		public const string Table = "tblEigenerPreis";
		//public int ID { get; set; }
		//public int ID_Erh { get; set; }
		//public string ID_Kat { get; set; }
		public string Erhaltung { get; set; }
		public Decimal Preis { get; set; }
	}
}
