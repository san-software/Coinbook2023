using System.IO;
using System.Xml.Serialization;

namespace SAN.Logging
{
    public static class LogHelper
    {
        private static LogModel logSettings = null;

        public static LogModel LogSettings
        {
            get
            {
                var file = "LogSettings.config";

                if (logSettings == null)
                {
                    if (File.Exists(file))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(LogModel));

                        using (Stream reader = new FileStream(file, FileMode.Open))
                            logSettings = (LogModel)serializer.Deserialize(reader);

                        serializer = null;
                    }
                    else
                        logSettings = new LogModel();
                }
                return logSettings;
            }
            set
            {
                logSettings = value;
            }
        }
    }
}
