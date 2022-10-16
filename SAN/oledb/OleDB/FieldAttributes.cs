using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OleDB
{
	public class FieldAttributes
	{
		public string Table { get; set; }
		public string Field { get; set; }
		public DataTyp DataType { get; set; }
		public long Size { get; set; }
		public int NumericPrecision { get; set; }
		public int NumericScale { get; set; }
		public bool Unique { get; set; }
		public bool Key { get; set; }
		public bool AllowDBNull { get; set; }
		public bool AutoIncrement { get; set; }
		public string DataTypeDotNet { get; set; }
	}
}
