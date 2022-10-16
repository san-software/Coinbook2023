using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Nation
	{
		public int ID { get; set; }
		public string Bezeichnung { get; set; }
		public string Bestellnummer { get; set; }
		public bool InUse { get; set; }
        public string Key { get; set; }
        public string Mapping { get; set; }

    }
}