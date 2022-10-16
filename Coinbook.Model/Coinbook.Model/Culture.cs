using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Culture
	{
		public const string Table = "tblCulture";
		public int ID { get; set; }
		public string Bezeichnung { get; set; }
		public string EN_GB { get; set; }
		public string Waehrung { get; set; }
		public string Kultur { get; set; }
		public Decimal Faktor { get; set; }
	}
}
