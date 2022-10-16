using SAN.FTP;
using System;
using System.IO;
using System.Management;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Coinbook.Helper;

namespace Coinbook
{
    class Feedback :IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Send()
        {
            FeedbackModel model = new FeedbackModel();

			model.Vorname = CoinbookHelper.Settings.Vorname;
			model.Nachname = CoinbookHelper.Settings.Nachname;
			model.PLZ = CoinbookHelper.Settings.PLZ;
			model.Ort = CoinbookHelper.Settings.Ort;
			model.Strasse = CoinbookHelper.Settings.Strasse;
			model.Email = CoinbookHelper.Settings.Mail;
			model.Datum = DateTime.Now.ToShortDateString();
			model.Lizenzkey = CoinbookHelper.Settings.Lizenzkey;
			model.Telefon = CoinbookHelper.Settings.Telefon;
            model.Culture = CoinbookHelper.Settings.Culture;
            model.Betriebssystem = CoinbookHelper.GetWindwosClientVersion + "/" + Environment.OSVersion.Platform.ToString(); //.Contains("64") ? "64 bit": "32 bit";
            model.Program = Application.ProductName + " " + Application.ProductVersion;
            model.Serial = CoinbookHelper.CPUID;
            model.PC = ExistingHardware();

            string file = Path.Combine(CoinbookHelper.UpdatePath, "Feedback-" + CoinbookHelper.Settings.Lizenzkey + ".xml");
            SerializeObject(file, model);

            FTPClass ftp = new FTPClass();
            if (ftp.Connect(ftp.FTPParameter.URL, ftp.FTPParameter.Transfer, ftp.FTPParameter.TransferPasswort))
            {
                ftp.SetWorkingDirectory("Feedback");
                ftp.Upload(file, Path.GetFileName(file));
                ftp.Disconnect();

                File.Delete(file);
            }
        }

        private Hardware ExistingHardware()
        {
            Hardware pc = new Hardware();

            ManagementObjectSearcher system = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectCollection queryCollection1 = system.Get();
            foreach (ManagementObject mo in queryCollection1)
            {
                pc.Hersteller = mo["manufacturer"].ToString();
                pc.Modell = mo["model"].ToString();
                pc.Typ = mo["systemtype"].ToString();
                pc.FreierSpeicher = mo["totalphysicalmemory"].ToString();
            }

            return pc;
        }

        private void SerializeObject(string filename, FeedbackModel model)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FeedbackModel));
            
            Stream fs = new FileStream(filename, FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
 
            serializer.Serialize(writer, model);
            writer.Close();
        }
    }

    public class FeedbackModel
    {
        public string Betriebssystem { get; set; }
        public string Program { get; set; }
        public string Lizenzkey { get; set; }
        public string Serial { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string Strasse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Datum { get; set; }
        public string Culture { get; set; }
        public Hardware PC { get; set; }
    }

    public class Hardware
    {
        public string Hersteller { get; set; }
        public string Modell { get; set; }
        public string Typ { get; set; }
        public string FreierSpeicher { get; set; }

    }
}


