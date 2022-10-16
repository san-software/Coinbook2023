using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Downloads
	{
        public int ID { get; set; }
        public string Lizenz { get; set; }
        public string Jahr { get; set; }
        public string Bezeichnung { get; set; }
        public string OldLizenz { get; set; }
        public string Datum { get; set; }
        public string Key { get; set; }
        
        [Ignore]
        public string Url { get; set; }

        [Ignore]
        public string Target { get; set; }
    }
}
