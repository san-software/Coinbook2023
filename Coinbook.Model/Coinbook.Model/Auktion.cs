using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Auktion
	{
		public int ID { get; set; }
		public string Guid { get; set; }
		public int Erhaltungsgrad { get; set; }
		public string Datum { get; set; }
		public Decimal Preis { get; set; }
		public string Auktionator { get; set; }
		public string Auktionshaus { get; set; }
	}
}
