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
        public LogLevels Level { get; set; }
        public bool Fatal { get; set; }
        public bool On { get; set; }
        public string LogFile { get; set; }
        public bool LogInfo { get; set; }
        public bool LogWarn { get; set; }
        public bool LogDebug { get; set; }
        public bool LogError { get; set; }
    }
}

