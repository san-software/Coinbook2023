using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Gebiet
	{
		public const string Table = "tblGebiet";
		public int ID { get; set; }
		public string Bezeichnung { get; set; }
		public int NationID { get; set; }
		public int Sortierung { get; set; }
		public int AeraID { get; set; }
	}
}
