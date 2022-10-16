using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Modulverwaltung
{
    public static class BestellungHelper
    {
        public static string Bestellnummer
        {
            get
            {
                return string.Format("{0:yyMMdd}", DateTime.Now) + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
            }
        }
    }
}
