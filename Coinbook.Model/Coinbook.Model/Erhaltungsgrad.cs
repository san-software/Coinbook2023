using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Erhaltungsgrad
	{
		public int ID { get; set; }
		public int ErhaltungsgradID { get; set; }
		public string Sprache { get; set; }
		public string Erhaltung { get; set; }
		public string Bezeichnung { get; set; }
		public string Land { get; set; }
	}
}
