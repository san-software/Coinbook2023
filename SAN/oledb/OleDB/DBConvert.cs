using System;
using System.Collections.Generic;
using System.Text;

namespace OleDB
{
	class DBConvert
	{
		//private string datenbankTyp = string.Empty;

		public DBConvert()
		{
			//this.datenbankTyp = datenbankTyp;
		}

        public string DBDate(Databases datenbankTyp, object wert)
		{
			string result = "Null";

			if (wert != DBNull.Value)
			{
				try
				{
					DateTime datum = Convert.ToDateTime(wert);
					string day = datum.Day.ToString();
					string month = datum.Month.ToString();
					string year = datum.Year.ToString();

					switch (datenbankTyp)
					{
						case Databases.MSAccess:
							result= "#" + month.PadLeft(2, '0') + "/" + day.PadLeft(2, '0') + "/" + year.PadLeft(4, '0') + "#";
							break;

						case Databases.SQLServer:
							result = "'"+year.PadLeft(4, '0') + month.PadLeft(2, '0')+ day.PadLeft(2, '0') +"'";
							break;
					}
				}
				catch
				{
				}
			}

			return result;
		}

        public string DBDateTime(Databases datenbankTyp, object wert)
		{
			string result = DBDate(datenbankTyp,wert);

			if (wert != DBNull.Value)
			{
				try
				{
					DateTime datum = Convert.ToDateTime(wert);
					string day = datum.Day.ToString();
					string month = datum.Month.ToString();
					string year = datum.Year.ToString();
					string hour = datum.Hour.ToString();
					string minute = datum.Minute.ToString();
					string second = datum.Second.ToString();

					switch (datenbankTyp)
					{
						case Databases.MSAccess:
							result = "'" + day.PadLeft(2, '0') + "." + month.PadLeft(2, '0') + "." + year.PadLeft(4, '0') + " ";
							break;

						case Databases.SQLServer:
							result = "'" + year.PadLeft(4, '0') + month.PadLeft(2, '0') + day.PadLeft(2, '0') + " ";
							break;

						case Databases.SQLite:
							result = "'" + year.PadLeft(4, '0') + "-" +month.PadLeft(2, '0') + "-" + day.PadLeft(2, '0') + " ";
							break;

					}

					result = result + hour.PadLeft(2, '0') + ":" + minute.PadLeft(2, '0') + ":" + second.PadLeft(2, '0') + "'";
				}
				catch
				{
				}
			}
			
			return result;
		}

        public string DBBool(Databases datenbankTyp, object wert)
		{
			string result = "Null";

			if (wert != DBNull.Value)
			{
				switch (datenbankTyp)
				{
					case Databases.MSAccess:
						if ((bool)wert)
							result = "true";
						else
							result = "false";
						break;

					case Databases.SQLServer:
						if ((bool)wert)
							result = "1";
						else
							result = "0";
						break;

					case Databases.SQLite:
						if ((bool)wert)
							result = "true";
						else
							result = "false";
						break;

				}

			}
			return result;
		}

		public string DBNumber(object wert)
		{
			if (wert != DBNull.Value)
				return wert.ToString();
			else
				return "Null";
		}

		public string DBNumber(double p_Value)
		{
			string text;

			if (!Double.IsNaN(p_Value))
				text = Convert.ToString(p_Value).Replace(",", ".");
			else
				text = "Null";

			return text;
		}

		public string DBString(object wert)
		{
			string text = "''";

			if (wert == DBNull.Value)
				text = "Null";
			else
			{
				if (wert.ToString().Length != 0)
					text = "'" + wert.ToString().Replace("'", "''") + "'";
			}

			return text;
		}
	}
}
