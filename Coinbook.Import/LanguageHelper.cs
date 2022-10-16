using Coinbook.Lokalisierung;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coinbook.Import
{
    public static class LanguageHelper
    {
        public static void CreateLocalization(string resourcePath)
        {
            Localization = new Localization(resourcePath);
        }

        public static Localization Localization { get; set; }
    }
}

