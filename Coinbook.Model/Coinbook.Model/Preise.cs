using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Preise
	{
		public const string Table = "tblPreise";

		[IgnoreID]
		public int ID { get; set; }

		public string GUID { get; set; }
		public Decimal SPreis { get; set; }
		public Decimal SPPreis { get; set; }
		public Decimal SSPreis { get; set; }
		public Decimal SSPPreis { get; set; }
		public Decimal VZPreis { get; set; }
		public Decimal VZPPreis { get; set; }
		public Decimal STNPreis { get; set; }
		public Decimal STHPreis { get; set; }
		public Decimal PPPreis { get; set; }
		public int NationID { get; set; }
	}
}
