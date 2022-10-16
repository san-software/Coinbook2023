using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SAN.Magnifier
{
	public static class MagnifierHelper
	{
		private static string configFileName = "configData.xml";
		public static string ConfigFileName
		{
			get
			{
				return configFileName;
			}
			set
			{
				configFileName = value;
			}
		}

		public static void LoadConfiguration()
		{
			try
			{
				if (config == null)
					config = new Configuration();

				config = (Configuration)XmlUtility.Deserialize(config.GetType(), MagnifierHelper.ConfigFileName);
			}
			catch
			{
				config = new Configuration();
			}
		}

		private static Configuration config;
		public static Configuration Config
		{
			get
			{
				return config;
			}
		}

		public static void SaveConfiguration()
		{
			try
			{
				XmlUtility.Serialize(Config, ConfigFileName);
			}
			catch (Exception e)
			{
				Console.WriteLine("Serialization problem: " + e.Message);
			}
		}

		public static Point LastCursorPosition{get;set;}
			


	}
}
