using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class EigeneKatNr
	{
		public const string Table = "tblEigeneKatNr";
		public string ID { get; set; }
		public string Coinbook { get; set; }
		public string KatNr { get; set; }
		public int NationID { get; set; }
	}
}
