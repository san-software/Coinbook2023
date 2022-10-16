using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Settings2
	{
		public const string Table = "tblSettings2";
		public int id { get; set; }
		public string Lizenz { get; set; }
		public string Jahr { get; set; }
		public string Key { get; set; }
	}

}
