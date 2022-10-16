using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Currency
	{
		public const string Table = "tblCulture";
		public int ID { get; set; }
		public string Bezeichnung { get; set; }
		public string Waehrung { get; set; }
		public string Kultur { get; set; }
		public decimal Faktor { get; set; }
	}
}
