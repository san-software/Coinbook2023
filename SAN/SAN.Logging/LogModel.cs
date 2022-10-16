using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAN.Logging
{
    public class LogModel
    {
        public bool All { get; set; }
        public bool Debug { get; set; }
        public bool Info { get; set; }
        public bool Warn { get; set; }
        public bool Error { get; set; }
        public bool Fatal { get; set; }
        public bool Off { get; set; }
        public string LogFile { get; set; }
    }
}

