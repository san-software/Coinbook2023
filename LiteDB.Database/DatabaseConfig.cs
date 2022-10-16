using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDB.Database
{
	public class DatabaseConfig
	{
		public string DatenbankTyp { get; set; }
		public string Provider { get; set; }
		public string DataSource { get; set; }
		public string Password { get; set; }
		public string User { get; set; }
		public string Datapath { get; set; }
	}
}
