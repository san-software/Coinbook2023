using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace OleDB
{
	public class DBConfig
	{
        string _datapath;
		public string Datapath
        {
            get
            {
                return _datapath;
            }
            set
            {
                _datapath = value;
            }
        }
		public Databases[] DatenbankTyp { get; set; }
		public string[] Provider { get; set; }
		public string[] DataBase { get; set; }
		public string[] Host { get; set; }
		public string[] Schema { get; set; }
		public string[] User { get; set; }
		public string[] Password { get; set; }
    public int Anzahl { get; set; }

		public void readConnectionFromXML(string file, string application)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("database.config");

			XmlNode settings = xmlDoc.SelectSingleNode("Settings");
			//XmlNodeList databases = settings.SelectNodes("DataBase");
			XmlNodeList databases = settings.ChildNodes;

			XmlNode datapath = settings.SelectSingleNode("Datapath");
            if (datapath != null)
            {
                Datapath = datapath.InnerText;

                if (Datapath == "CommonApplicationData")
                    Datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), application);

                if (Datapath == "CommonDocuments")
                  Datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), application);

                if (Datapath == "UserAppDataPath")
                {
                    Datapath = Application.UserAppDataPath;
                    Datapath = Datapath.Substring(0, Datapath.IndexOf(Application.CompanyName)) + Application.ProductName;
                }

                if (Datapath == "LocalUserAppDataPath")
                {
                    Datapath = Application.LocalUserAppDataPath;
                    Datapath = Datapath.Substring(0, Datapath.IndexOf(Application.CompanyName)) + Application.ProductName;
                }
            }
            else
                Datapath = "";

			Anzahl = databases.Count - 1;

			//Definiere Arrays für die Datenbanken
			DatenbankTyp = new Databases[Anzahl];
			Provider = new string[Anzahl];
			DataBase = new string[Anzahl];
			Host = new string[Anzahl];
			Schema = new string[Anzahl];
			User = new string[Anzahl];
			Password = new string[Anzahl];

			for (int counter = 0; counter < Anzahl; counter++)
			{
				XmlAttributeEx attribute = new XmlAttributeEx(databases[counter].Attributes);

				if (attribute.ExistAttribute("DatenbankTyp"))
					DatenbankTyp[counter] = (Databases)Enum.Parse(typeof(Databases), attribute.Attribute("DatenbankTyp").Value.ToString());

				if (attribute.ExistAttribute("Provider"))
					Provider[counter] = attribute.Attribute("Provider").Value;

				if (attribute.ExistAttribute("DataSource"))
				{
					DataBase[counter] = attribute.Attribute("DataSource").Value;
					if (!string.IsNullOrEmpty(file))
						DataBase[counter] = file;
				}

				if (attribute.ExistAttribute("Host"))
					Host[counter] = attribute.Attribute("Host").Value;

				if (attribute.ExistAttribute("Schema"))
					Schema[counter] = attribute.Attribute("Schema").Value;

				if (attribute.ExistAttribute("User"))
					User[counter] = attribute.Attribute("User").Value;

				if (DatenbankTyp[counter] == Databases.MSAccess)
					DataBase[counter] = Datapath + @"\" + DataBase[counter];

				if (DatenbankTyp[counter] == Databases.SQLite)
					DataBase[counter] = Datapath + "/" + DataBase[counter];

				if (attribute.ExistAttribute("Password"))
					Password[counter] = attribute.Attribute("Password").Value;
			}
		}

		public void readConnectionFromConfig(string file)
		{
			Anzahl = Convert.ToInt32(ConfigurationManager.AppSettings["Datenbanken"]);
			if (Anzahl == 0)
				Anzahl = 1;

			//Definiere Arrays für die Datenbanken
			DatenbankTyp = new Databases[Anzahl];
			Provider = new string[Anzahl];
			DataBase = new string[Anzahl];
			Host = new string[Anzahl];
			Schema = new string[Anzahl];
			Password = new string[Anzahl];

			for (int counter = 0; counter < Anzahl; counter++)
			{
				if (counter == 0)
				{
					DatenbankTyp[counter] = (Databases)Enum.Parse(typeof(Databases), ConfigurationManager.AppSettings["DatenbankTyp"]);
					Provider[counter] = ConfigurationManager.AppSettings["Provider"];
					DataBase[counter] = ConfigurationManager.AppSettings["Data Source"];
					Host[counter] = ConfigurationManager.AppSettings["Host"];
					Schema[counter] = ConfigurationManager.AppSettings["Schema"];
				}
				else
				{
					DatenbankTyp[counter] = (Databases)Enum.Parse(typeof(Databases), ConfigurationManager.AppSettings["DatenbankTyp" + Convert.ToString((counter + 1))]);
					Provider[counter] = ConfigurationManager.AppSettings["Provider" + Convert.ToString((counter + 1))];
					DataBase[counter] = ConfigurationManager.AppSettings["Data Source" + Convert.ToString((counter + 1))];
					Host[counter] = ConfigurationManager.AppSettings["Host" + Convert.ToString((counter + 1))];
					Schema[counter] = ConfigurationManager.AppSettings["Schema" + Convert.ToString((counter + 1))];
				}

				if (!string.IsNullOrEmpty(file))
					DataBase[counter] = file;
			}
		}


	}
}