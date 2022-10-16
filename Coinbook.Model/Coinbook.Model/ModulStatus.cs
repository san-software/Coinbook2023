using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class ModulStatus
	{
		public const string Table = "tblModule";
		public int ID { get; set; }
		public string Nation { get; set; }
		public bool InUse { get; set; }
		public string Jahr { get; set; }
	}
}
