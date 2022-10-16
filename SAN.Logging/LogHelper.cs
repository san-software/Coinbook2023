using System.IO;
using System.Xml;
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
                //if (logSettings == null)
                //{
                    if (File.Exists(ConfigFile))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(LogModel));

                        using (Stream reader = new FileStream(ConfigFile, FileMode.Open))
                            logSettings = (LogModel)serializer.Deserialize(reader);

                        serializer = null;
                    }
                    else
                        logSettings = new LogModel();
                //}
                return logSettings;
            }
            set
            {
                logSettings = value;
            }
        }

        public static string ConfigFile { get { return Path.Combine(@"C:\Programdata\Coinbook", "LogSettings.config"); } }

        public static void SaveSettings()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LogModel));

            XmlTextWriter xmlWriter = new XmlTextWriter(ConfigFile, System.Text.Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            serializer.Serialize(xmlWriter, logSettings);
            xmlWriter.Close();
        }
    }
}
