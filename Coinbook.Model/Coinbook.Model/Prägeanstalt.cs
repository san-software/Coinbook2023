using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Praegeanstalt
	{
		public const string Table = "tblPraegeanstalt";
		public int ID { get; set; }
		public int Nation { get; set; }
		public string Muenzzeichen { get; set; }
		public string Zeit { get; set; }
		public string Ort { get; set; }
		public string Adresse { get; set; }
		public string Email { get; set; }
		public string Homepage { get; set; }
		public string Bemerkung { get; set; }
		public string Land { get; set; }
	}
}
