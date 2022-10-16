using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Data;
using SAN.Converter;
using System.IO;
using System.Management;
using System.Windows.Forms;
using System.Net.Mail;
using System.Xml;
using SAN.FTP;
using System.Globalization;
using Coinbook.Model;

namespace Coinbook.HelperProject
{
    public static class Helper
    {
        public static string Pfad { get; set; }
        public static string Cultur { get; set; }
        //public static DataSet Settings { get; set; }
        public static int User { get; set; }

        public static string Datapath
        {
            get
            {
                return Pfad; // +@"data";
            }
        }

        public static string BackupPath
        {
            get
            {
                return Path.Combine(Pfad, "Backup");
            }
        }

        public static string UpdatePath
        {
            get
            {
                return Path.Combine(Pfad, "Updater");
            }
        }

        public static string DownloadPath
        {
            get
            {
                return Path.Combine(Pfad, "Downloads");
            }
        }

        public static string InfoPath
        {
            get
            {
                return System.IO.Path.Combine(Pfad, "Info");
            }
        }

        public static string Picturepath
        {
            get
            {
                return System.IO.Path.Combine(Pfad, "Bilder");
            }
        }

				public static string CurrentLanguage { get; set; }
				public static CultureInfo CultureInfo { get; set; }

				public static string Sammlungsanzeige(string text, bool doublette, int menge)
        {
            int s = 0;
            int d = 0;
            string tmp = text;

            if (tmp != String.Empty)
            {
                if (tmp.IndexOf("/") == -1)
                    s = ConvertEx.ToInt32(tmp);
                else
                {
                    s = ConvertEx.ToInt32(tmp.Substring(0, tmp.IndexOf("/")));
                    d = ConvertEx.ToInt32(tmp.Substring(tmp.IndexOf("/") + 1));
                }
            }

            if (!doublette)
                s = s + menge;
            else
                d = d + menge;

            tmp = s.ToString() + "/" + d.ToString();
            if (tmp == "0/0")
                tmp = string.Empty;

            return tmp;

        }

        public static void WriteDataTable(string path, object datatable, char seperator)
        {
            //using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            //{
            //    int numberOfColumns = datatable.Columns.Count;

            //    for (int i = 0; i < numberOfColumns; i++)
            //    {
            //        sw.Write(datatable.Columns[i]);
            //        if (i < numberOfColumns - 1)
            //            sw.Write(seperator);
            //    }
            //    sw.Write(sw.NewLine);

            //    foreach (DataRow dr in datatable.Rows)
            //    {
            //        for (int i = 0; i < numberOfColumns; i++)
            //        {
            //            sw.Write(dr[i].ToString());

            //            if (i < numberOfColumns - 1)
            //                sw.Write(seperator);
            //        }
            //        sw.Write(sw.NewLine);
            //    }
            //}
        }

		public static List<Erhaltungsgrad> Erhaltungsgrade { get; set; }
		public static List<Nation> Nationen { get; set; }
		public static List<Aera> Aeras { get; set; }
		public static List<Gebiet> Regions { get; set; }
		public static List<Katalog> Muenzkatalog { get; set; }
		public static List<Sammlung> SammlungListe { get; set; }
		public static List<Sammlung> DoublettenListe { get; set; }
		//public static List<DetailOverview> DetailListe { get; set; }

		//public static void ReadErhaltungsgrade(Database database, string sprache)
		//{
		//    List<Erhaltungsgrad> e = new List<Erhaltungsgrad>();
		//    string cmd = "Select Erhaltungsgrad from tblErhaltungsgrad where Sprache = '" + sprache + "' order by id";
		//    DataTable dt = database.GetDataTable(cmd);
		//    for (int i = 0; i < dt.Rows.Count; i++)
		//        e.Add(dt.Rows[i][0].ToString());

		//    Erhaltungsgrade = e;
		//}

        public static string CPUID
        {
            get
            {
                string cpuid = string.Empty;
                ManagementClass man = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = man.GetInstances();
                foreach (ManagementObject mob in moc)
                {
                    if (cpuid == String.Empty)
                    {
                        // Nimmt vom ersten CPU die ID und bricht dann ab. 
                        cpuid = mob.Properties["processorID"].Value.ToString();
                        break;
                    }
                }
                return cpuid;
            }
        }

        public static void ClearPath(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
                File.Delete(file);
        }

        public static string ConvertFileName(string text)
        {
            return text.Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue").Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("ß", "ss").Replace(" ", "_");
        }

        //public static void Aktivieren()
        //{
        //  string bit = "32";

        //  MailMessage mail = new MailMessage();
        //  mail.From = new MailAddress("san-software@web.de"); //Absender 
        //  //mail.From = new MailAddress(Settings.Mail); //Absender 
        //  //mail.To.Add("san-software@gmx.de"); //Empfänger 
        //  mail.To.Add("Bestellung@Coinbook.de"); //Empfänger 

        //  if (Environment.OSVersion.Platform.ToString().Contains("64"))
        //    bit = "64";

        //  mail.Subject = "Coinbook Aktivierung #" + cpuID + "#" + Settings.Vorname + "#" + Settings.Nachname + "#" + Settings.Mail + "#" + GetWindwosClientVersion + "#" + bit + "#" + Application.ProductVersion;
        //  mail.Body = Settings.Vorname + Environment.NewLine + Settings.Nachname + Environment.NewLine + Settings.Mail + Environment.NewLine + GetWindwosClientVersion + Environment.NewLine + bit + "#" + Application.ProductVersion;

        //  SmtpClient client = new SmtpClient("smtp.web.de", 587); //SMTP Server von Hotmail und Outlook. 

        //  try
        //  {
        //    client.Credentials = new System.Net.NetworkCredential("san-software@web.de", "magixx");//Anmeldedaten für den SMTP Server 
        //    client.EnableSsl = true; //Die meisten Anbieter verlangen eine SSL-Verschlüsselung 
        //    client.Send(mail); //Senden 

        //    Settings.Activated = "angefordert";
        //    Settings.Save();

        //    MessageBox.Show("Die Aktivierung von Coinbook wurde angefordert." + Environment.NewLine + Environment.NewLine + "Die Aktivierung sollte innerhalb von 2-3 Werktagen erfolgen.");
        //  }
        //  catch (Exception ex)
        //  {
        //    string text = "Die Aktivierung konnte aus folgenden Gründen nicht angefordert werden" + Environment.NewLine + Environment.NewLine
        //      + ex.Message + Environment.NewLine + Environment.NewLine
        //      + "Bitte teilen Sie dem Coinbook-Support dieses Problem und die nachfolgend angezeige Aktivierungsnummer mit." + Environment.NewLine + Environment.NewLine
        //      + "Aktivierungsnummer: " + cpuID + Environment.NewLine + Environment.NewLine
        //      + "Mit Hile dieser Nummer kann die Aktivierung vom Support manuell vorgenommen werden";
        //    MessageBox.Show(text);
        //  }
        //}

        public static string GetWindwosClientVersion
        {
            get
            {
                string result = "unbekannt";

                int major = System.Environment.OSVersion.Version.Major;
                int minor = System.Environment.OSVersion.Version.Minor;
                int build = System.Environment.OSVersion.Version.Build;


                if (major == 4 && minor == 0 && build == 950)
                    result = "Win95 Release 1";
                else if (major == 4 && minor == 0 && build == 1111)
                    result = "Win95 Release 2";
                else if (major == 4 && minor == 3 && (build == 1212 || build == 1213 || build == 1214))
                    result = "Win95 Release 2.1";
                else if (major == 4 && minor == 10 && build == 1998)
                    result = "Win98";
                else if (major == 4 && minor == 10 && build == 2222)
                    result = "Win98 Second Edition";
                else if (major == 4 && minor == 90)
                    result = "WinMe";
                else if (major == 5 && minor == 0)
                    result = "Win2000";
                else if (major == 5 && minor == 1 && build == 2600)
                    result = "WinXP";
                else if (major == 5 && minor == 2 && build == 2600)
                    result = "WinXP/64";
                else if (major == 6 && minor == 0)
                    result = "Vista";
                else if (major == 6 && minor == 1)
                    result = "Win7";
                else if (major == 6 && minor == 2)
                    result = "Win8";
                else if (major == 6 && minor == 3)
                    result = "Win8.1 Update 1";
                else if (major == 10 && minor == 0)
                    result = "Win10";

                return result;
            }
        }

        public static bool IsNumeric(string text)
        {
            float output;

            text = text.Replace(".", String.Empty).Replace(",", String.Empty);
            return float.TryParse(text, out output);
        }

        public static string Zipfile(string modul, string jahr)
        {
            string zipfile = modul.Replace(" ", "_") + "-" + jahr + ".zip";
            zipfile = zipfile.Replace(" ", "_");
            zipfile = zipfile.Replace("ä", "ae");
            zipfile = zipfile.Replace("Ö", "Oe");

            return zipfile;
        }
      /// <summary>
        /// Diese Funktion dekomprimiert eine ZIP-Datei.
        /// </summary>
        /// <param name="FileName">Die Datei die dekomprimiert werden soll.</param>
        /// <param name="OutputDir">Das Verzeichnis in dem die Dateien dekomprimiert werden sollen.</param>
        public static void DecompressFile(string FileName, string OutputDir)
        {
            FileStream ZFS = new FileStream(FileName, FileMode.Open);
            ICSharpCode.SharpZipLib.Zip.ZipInputStream ZIN = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(ZFS);
            ZIN.Password = "magixx-1-xxigam";//TODO Passwort verschlüsseln

			ICSharpCode.SharpZipLib.Zip.ZipEntry ZipEntry = default(ICSharpCode.SharpZipLib.Zip.ZipEntry);

            byte[] Buffer = new byte[4097];
            int ByteLen = 0;
            FileStream FS = null;

            string InZipDirName = null;
            string InZipFileName = null;
            string TargetFileName = null;

            do
            {
                ZipEntry = ZIN.GetNextEntry();
                if (ZipEntry == null) break;

                InZipDirName = Path.GetDirectoryName(ZipEntry.Name);
                InZipFileName = Path.GetFileName(ZipEntry.Name);

                if (!Directory.Exists(Path.Combine(OutputDir, InZipDirName)))
                    Directory.CreateDirectory(Path.Combine(Datapath, InZipDirName));

                if (InZipDirName == String.Empty)
                    TargetFileName = Path.Combine(OutputDir, InZipFileName).Replace("„", "ä");
                else
                    TargetFileName = Path.Combine(Path.Combine(Datapath, InZipDirName), InZipFileName).Replace("„", "ä");

                if (InZipFileName != String.Empty)
                {
                    FS = new FileStream(TargetFileName, FileMode.Create);
                    do
                    {
                        ByteLen = ZIN.Read(Buffer, 0, Buffer.Length);
                        FS.Write(Buffer, 0, ByteLen);
                    }
                    while (!(ByteLen <= 0));
                    FS.Close();
                }
            }
            while (true);

            ZIN.Close();
            ZFS.Close();
        }

		//public static void CompactDataBase()
		//{
		//	OleDBConnection.CloseAllConnections();

		//	string sourceFile = OleDBConnection.File;

		//	string destinationFile = Path.Combine(Path.GetDirectoryName(sourceFile), Path.GetFileNameWithoutExtension(sourceFile)) + ".tmp";
		//	string source = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourceFile + ";Jet OLEDB:Engine Type=5;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";
		//	string destination = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationFile + ";Jet OLEDB:Engine Type=5;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";

		//	dynamic JROEng = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("JRO.JetEngine"));

		//	try
		//	{
		//		JROEng.CompactDatabase(source, destination);

		//		File.Delete(sourceFile);
		//		File.Copy(destinationFile, sourceFile);
		//		File.Delete(destinationFile);
		//	}
		//	catch (SystemException ex)
		//	{
		//	}

		//	DBConnect result = OleDBConnection.Init;
		//}

		//public static void AktivierungAnfordern(enmAktivierungsArt aktivierungsart, string grund)
		//{

		//	string bit = "32 bit";
		//	if (Environment.OSVersion.Platform.ToString().Contains("64"))
		//		bit = "64 bit";

		//	XmlDocument document = new XmlDocument();
		//	XmlElement root = document.CreateElement("Element", "Root", String.Empty);
		//	XmlNode aktivierung = document.CreateElement(String.Empty, "Aktivierung", String.Empty);

		//	document.AppendChild(root);
		//	root.AppendChild(aktivierung);

		//	XmlAttribute vorname = document.CreateAttribute("Vorname");
		//	vorname.Value = Settings.Vorname;
		//	aktivierung.Attributes.Append(vorname);

		//	XmlAttribute nachname = document.CreateAttribute("Nachname");
		//	nachname.Value = Settings.Nachname;
		//	aktivierung.Attributes.Append(nachname);

		//	XmlAttribute plz = document.CreateAttribute("PLZ");
		//	plz.Value = Settings.PLZ;
		//	aktivierung.Attributes.Append(plz);

		//	XmlAttribute ort = document.CreateAttribute("Ort");
		//	ort.Value = Settings.Ort;
		//	aktivierung.Attributes.Append(ort);

		//	XmlAttribute land = document.CreateAttribute("Land");
		//	land.Value = Settings.Land;
		//	aktivierung.Attributes.Append(land);

		//	XmlAttribute strasse = document.CreateAttribute("Strasse");
		//	strasse.Value = Settings.Strasse;
		//	aktivierung.Attributes.Append(strasse);

		//	XmlAttribute betriebssystem = document.CreateAttribute("System");
		//	betriebssystem.Value = Helper.GetWindwosClientVersion + "/" + bit;
		//	aktivierung.Attributes.Append(betriebssystem);

		//	XmlAttribute email = document.CreateAttribute("Email");
		//	email.Value = Settings.Mail;
		//	aktivierung.Attributes.Append(email);

		//	XmlAttribute datum = document.CreateAttribute("Datum");
		//	datum.Value = DateTime.Now.ToShortDateString();
		//	aktivierung.Attributes.Append(datum);

		//	XmlAttribute lizenzkey = document.CreateAttribute("Lizenzkey");
		//	lizenzkey.Value = Settings.Lizenzkey;
		//	aktivierung.Attributes.Append(lizenzkey);

		//	XmlAttribute bemerkung = document.CreateAttribute("Bemerkung");
		//	bemerkung.Value = grund;
		//	aktivierung.Attributes.Append(bemerkung);

		//	XmlAttribute version = document.CreateAttribute("Version");
		//	version.Value = Application.ProductVersion;
		//	aktivierung.Attributes.Append(version);

		//	XmlAttribute serial = document.CreateAttribute("Serial");
		//	serial.Value = cpuID;
		//	aktivierung.Attributes.Append(serial);

		//	XmlAttribute art = document.CreateAttribute("Aktivierungsart");
		//	art.Value = aktivierungsart.ToString();
		//	aktivierung.Attributes.Append(art);

		//	XmlAttribute vorgang = document.CreateAttribute("Vorgang");
		//	vorgang.Value = string.Format("{0:yyMMdd}", DateTime.Now) + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
		//	aktivierung.Attributes.Append(vorgang);

		//	string file = Path.Combine(Helper.UpdatePath, "Aktivierung-" + Settings.Lizenzkey + ".xml");

		//	document.Save(file);

		//	FTPClass ftp = new FTPClass();
		//	ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1");//TODO Passwort verschlüsseln
		//	ftp.SetWorkingDirectory("Aktivierung");
		//	ftp.Upload(file, Path.GetFileName(file));
		//	ftp.Disconnect();

		//	File.Delete(file);

		//	Settings.Activated = "angefordert";

		//	MessageBox.Show("Die Aktivierungsanforderung wurde übermittelt");
		//}

		public static Color ColorHeader1
		{
			get
			{
				return Color.LightSkyBlue;
			}
		}

		public static Color ColorHeader2
		{
			get
			{
				return Color.LightGreen;
			}
		}

		public static Color ColorHeader3
		{
			get
			{
				return Color.LightCoral;
			}
		}

		public static Color ColorAlternateEven
		{
			get
			{
				return Color.Gainsboro;
			}
		}

		public static Color ColorAlternateOdd
		{ 
			get
			{
				return Color.White;
			}
		}

		public static Color ColorHeader
		{
			get
			{
				return Color.Gainsboro;
			}
		}

		public static Color ColorOwnPrices
		{
			get
			{
				return Color.Yellow;
			}
		}

		public static Color ColorSelection
		{
			get
			{
				return Color.LightSkyBlue;
			}
		}

		public static Color ColorGridlines
		{
			get
			{
				return Color.Black;
			}
		}

		public static Color ColorBlocked
		{
			get
			{
				return Color.Silver;
			}
		}

		public static Color ColorText
		{
			get
			{
				return Color.Black;
			}
		}

		public static string Provider { get; set; }
	}
}
