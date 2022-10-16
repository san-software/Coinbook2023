using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class HauptFilter
    {
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int GebietID { get; set; }
    }

    public class Filter
    {
        public string Waehrung { get; set; }
        public string Nominal { get; set; }
        public string Jahrgang { get; set; }
    }
}