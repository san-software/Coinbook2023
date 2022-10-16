using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using log4net;

namespace Coinbook.Logging
{
    public class Log4NetTraceListener : TraceListener
    {
        private readonly ILog _log;

        public Log4NetTraceListener(string provider)
        {
            _log = LogManager.GetLogger(provider);
        }

        public override void Write(string message)
        {
            if (_log == null)
                return;
            if (!string.IsNullOrWhiteSpace(message))
                _log.Info(message);
        }

        public override void WriteLine(string message)
        {
            if (_log != null)
                if (!string.IsNullOrWhiteSpace(message))
                    _log.Info(message);
        }
    }
}
