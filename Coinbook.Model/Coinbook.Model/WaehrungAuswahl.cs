using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class WaehrungAuswahl
    {
        public string Waehrung { get; set; }
        public int GebietID { get; set; }
        public int NationID { get; set; }
        public string Nominal { get; set; }
        public string Jahrgang { get; set; }
    }
}