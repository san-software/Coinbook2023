using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAN.Logging
{

    /// <summary>
    ///   A simplistic logger.
    /// </summary>
    public static class Logger
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static int indentLevel;

        public static void Indent()
        {
            indentLevel++;
        }

        public static void Unindent()
        {
            indentLevel--;
        }

        public static void WriteLine(string message, LogLevels level)
        {
            if (level == LogLevels.Info || level == LogLevels.All)
                log.Info(message);

            if (level == LogLevels.Debug || level == LogLevels.All)
                log.Debug(message);

            if (level == LogLevels.Error || level == LogLevels.Fatal)
                log.Error(message);

            if (level == LogLevels.Warn || level == LogLevels.All)
                log.Warn(message);
        }
    }
}
