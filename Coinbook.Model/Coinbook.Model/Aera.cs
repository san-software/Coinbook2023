using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Aera
	{
		public const string Table = "tblAera";
		public int ID { get; set; }
		public string Bezeichnung { get; set; }
		public int NationID { get; set; }
		public int Sortierung { get; set; }
	}
}
