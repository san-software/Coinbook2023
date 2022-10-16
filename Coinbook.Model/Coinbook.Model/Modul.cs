using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Modul
	{
		public const string Table = "tblModule";
		public int ID { get; set; }
		public int ModulID { get; set; }
		public int NationID { get; set; }
		public string Typ { get; set; }
		public string Sprache { get; set; }
		public string Text { get; set; }
	}
}
