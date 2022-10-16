using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class DBVersion
	{
		public const string Table = "tblDBVersion";
		public string colVersion { get; set; }
	}
}
