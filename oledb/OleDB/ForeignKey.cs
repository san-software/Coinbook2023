using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OleDB
{
	public class ForeignKey
	{
		public string Table { get; set; }
		public string Field { get; set; }
		public string RefernceTable { get; set; }
		public string ReferenceField { get; set; }
	}
}
