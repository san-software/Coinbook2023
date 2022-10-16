using SAN.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postsharp.LogMethodAttribute
{

    /// <summary>
    ///   A simplistic logger.
    /// </summary>
    public static class Logger
    {
        private static string space = " ";
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

        public static void WriteLine(string message, LogLevels level, List<ParameterModel> parameter)
        {
            //if (indentLevel < 0)
            //        indentLevel = 0;
            //Console.Write(new string(' ', 3 * indentLevel));
            //Console.WriteLine(message);

            string text = "";
            foreach (var p in parameter)
            {
                text += string.Format("{0}{1}:{2}", space, p.Name, p.Value);
            }

            if (level == LogLevels.Info )
                log.Info(message + text);

            if (level == LogLevels.Debug )
                log.Debug(message + text);

            if (level == LogLevels.Error)
                log.Error(message + text);

            if (level == LogLevels.Warn)
                log.Warn(message + text);



        }
    }
}
