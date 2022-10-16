using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using SAN.Converter;
using System.Configuration;

namespace SAN.UI.Controls
{
	/// <summary>
	/// Summary description for clsFarbverwaltung.
	/// </summary>
	public class Farbverwaltung
	{
		private static Color backColor = SystemColors.Control;
		private static Color inputBackColor = Color.White;
		private static Color inputForeColor = SystemColors.ControlText;
		private static Color displayForeColor = SystemColors.ControlText;
		private static Color displayBackColor = Color.White;
		private static Color captionBackColor = SystemColors.ActiveCaption;
		private static Color captionForeColor = SystemColors.ActiveCaptionText;
		private static Color gridLineColor = Color.Black;
		private static Color inputErrorBackColor = Color.White;
		private static Color inputErrorForeColor = SystemColors.ControlText;
		private static Color gridBackColorOdd = Color.White;
		private static Color gridBackColorEven = Color.White;
		private static Color gridBackColorInputOdd = Color.White;
		private static Color gridBackColorInputEven = Color.White;
		private static Color gridForeColorOdd = Color.Black;
		private static Color gridForeColorEven = Color.Black;
		private static Color gridForeColorInputOdd = Color.Black;
		private static Color gridForeColorInputEven = Color.Black;
		private static Color gridBackColorCurrentRow = SystemColors.ActiveCaption;
		private static Color gridForeColorCurrentRow = Color.Black;
		private static Color buttonBackColor = SystemColors.Control;
		private static Color buttonForeColor = Color.Black;
		private static Color pflichtBackColor = Color.White;
		private static Color pflichtForeColor = SystemColors.ControlText;
		private static Color progressbarForeColor = Color.Black;
		private static Color progressbarFillColor = Color.Cyan;

		private static int alpha=0;
		private static int red=1;
		private static int green=2;
		private static int blue=3;

		private static char[] delimiter = ",".ToCharArray();

		private static XmlDocument xmldoc = new XmlDocument();

		private static string configFile = "color.config";

		private static string filename = "";

		static Farbverwaltung()
		{
			try
			{
			filename = Helper.HomePath + configFile;
			
			if (!File.Exists(filename))
				filename= Application.StartupPath.ToString() + "/" + configFile;

			if (!File.Exists(filename))
				filename = ConfigurationManager.AppSettings["ColorFile"];
			if (File.Exists(filename))
				loadColor(filename);
			}
			catch (Exception)
			{
			}
		}

		public static void SetDefault()
		{
			if (File.Exists(filename))
				File.Delete(filename);

			loadColor(configFile);
		}

		public static Color BackColor
		{
			set {backColor = value;}
			get {return backColor;}
		}

		public static Color InputBackColor
		{
			set {inputBackColor = value;}
			get {return inputBackColor;}
		}

		public static Color InputErrorBackColor
		{
			set {inputErrorBackColor = value;}
			get {return inputErrorBackColor;}
		}

		public static Color InputForeColor
		{
			set {inputForeColor = value;}
			get {return inputForeColor;}
		}

		public static Color InputErrorForeColor
		{
			set {inputErrorForeColor = value;}
			get {return inputErrorForeColor;}
		}

		public static Color PflichtBackColor
		{
			set
			{
				pflichtBackColor = value;
			}
			get
			{
				return pflichtBackColor;
			}
		}

		public static Color PflichtForeColor
		{
			set
			{
				pflichtForeColor = value;
			}
			get
			{
				return pflichtForeColor;
			}
		}

		public static Color DisplayBackColor
		{
			set {displayBackColor = value;}
			get {return displayBackColor;}
		}

		public static Color DisplayForeColor
		{
			set {displayForeColor = value;}
			get {return displayForeColor;}
		}

		public static Color CaptionBackColor
		{
			set {captionBackColor = value;}
			get {return captionBackColor;}
		}

		public static Color CaptionForeColor
		{
			set {captionForeColor = value;}
			get {return captionForeColor;}
		}

		public static Color GridLineColor
		{
			set {gridLineColor = value;}
			get {return gridLineColor;}
		}

		public static Color GridBackColorOdd
		{
			set {gridBackColorOdd = value;}
			get {return gridBackColorOdd;}
		}

		public static Color GridBackColorEven
		{
			set {gridBackColorEven = value;}
			get {return gridBackColorEven;}
		}

		public static Color GridBackColorInputOdd
		{
			set {gridBackColorInputOdd = value;}
			get {return gridBackColorInputOdd;}
		}

		public static Color GridBackColorInputEven
		{
			set {gridBackColorInputEven = value;}
			get {return gridBackColorInputEven;}
		}

		public static Color GridForeColorOdd
		{
			set {gridForeColorOdd = value;}
			get {return gridForeColorOdd;}
		}

		public static Color GridForeColorEven
		{
			set {gridForeColorEven = value;}
			get {return gridForeColorEven;}
		}

		public static Color GridForeColorInputOdd
		{
			set {gridForeColorInputOdd = value;}
			get {return gridForeColorInputOdd;}
		}

		public static Color GridForeColorInputEven
		{
			set {gridForeColorInputEven = value;}
			get {return gridForeColorInputEven;}
		}

		public static Color GridBackColorCurrentRow
		{
			set
			{
				gridBackColorCurrentRow = value;
			}
			get
			{
				return gridBackColorCurrentRow;
			}
		}

		public static Color GridForeColorCurrentRow
		{
			set
			{
				gridForeColorCurrentRow = value;
			}
			get
			{
				return gridForeColorCurrentRow;
			}
		}

		public static Color ButtonBackColor
		{
			set
			{
				buttonBackColor = value;
			}
			get
			{
				return buttonBackColor;
			}
		}

		public static Color ButtonForeColor
		{
			set
			{
				buttonForeColor = value;
			}
			get
			{
				return buttonForeColor;
			}
		}

		public static Color ProgressbarForeColor
		{
			set
			{
				progressbarForeColor = value;
			}
			get
			{
				return progressbarForeColor;
			}
		}

		public static Color ProgressbarFillColor
		{
			set
			{
				progressbarFillColor = value;
			}
			get
			{
				return progressbarFillColor;
			}
		}

		public static void Save()
		{
			setColor("BackColor",backColor);			// Hintergrund-Farbe 
			setColor("InputBackColor",inputBackColor);			// Hintergrund-Farbe Eingabefeld während der Eingabe
			setColor("InputForeColor",inputForeColor);			// Schrift-Farbe Eingabefeld während der Eingabe
			setColor("DisplayBackColor",displayBackColor);			// Hintergrundfarbe Anzeige-Feld (Label)
			setColor("DisplayForeColor",displayForeColor);			// Schriftfarbe Anzeige-Feld (Label)
			setColor("CaptionBackColor",captionBackColor);			// Hintergrundfarbe Überschrift
			setColor("CaptionForeColor",captionForeColor);			// Schriftfarbe Überschrift
			setColor("GridLineColor",gridLineColor);			// Farbe der Grid-Linien
			setColor("InputErrorBackColor",inputErrorBackColor);			// Hintergrund-Farbe Eingabefeld während der Eingabe (error)
			setColor("InputErrorForeColor",inputErrorForeColor);		// Schriftfarbe Eingabefeld während der Eingabe (error)
			setColor("gridBackColorOdd",gridBackColorOdd);			// Hintergrund-Farbe ungerade Zeile im Grid
			setColor("gridBackColorEven",gridBackColorEven);	
			setColor("gridBackColorInputOdd",gridBackColorInputOdd);	
			setColor("gridBackColorInputEven",gridBackColorInputEven);	
			setColor("gridForeColorOdd",gridForeColorOdd);	
			setColor("gridForeColorEven",gridForeColorEven);	
			setColor("gridForeColorInputOdd",gridForeColorInputOdd);
			setColor("gridForeColorInputEven", gridForeColorInputEven);
			setColor("gridBackColorCurrentRow", gridBackColorCurrentRow);
			setColor("buttonBackColor", buttonBackColor);
			setColor("buttonForeColor", buttonForeColor);
			setColor("PflichtBackColor", pflichtBackColor);			// Hintergrund-Farbe Pflichtfeld während der Eingabe
			setColor("PflichtForeColor", pflichtForeColor);			// Schrift-Farbe Pflichtfeld während der Eingabe
			setColor("ProgressbarFillColor", progressbarFillColor);			// Progressbar-farben
			setColor("ProgressbarForeColor", progressbarForeColor);			// Progressbar-farben

			xmldoc.Save(filename);
		}

		private static void loadColor(string file)
		{
			//if (ExistConfigFile(file))
			{
				xmldoc.Load(file);

				backColor = getColor("BackColor", backColor);													// Hintergrundfarbe
				inputBackColor = getColor("InputBackColor", inputBackColor);					// Hintergrund-Farbe Eingabefeld während der Eingabe
				inputErrorBackColor = getColor("InputErrorBackColor", inputErrorBackColor);	// Hintergrund-Farbe Eingabefeld während der Eingabe
				inputForeColor = getColor("InputForeColor", inputForeColor);					// Schrift-Farbe Eingabefeld während der Eingabe
				inputErrorForeColor = getColor("InputErrorForeColor", inputErrorForeColor);	// Schrift-Farbe Eingabefeld während der Eingabe
				displayBackColor = getColor("DisplayBackColor", displayBackColor);		// Hintergrundfarbe Anzeige-Feld (Label)
				displayForeColor = getColor("DisplayForeColor", displayForeColor);		// Schriftfarbe Anzeige-Feld (Label)
				captionBackColor = getColor("CaptionBackColor", captionBackColor);		// Hintergrundfarbe Überschrift
				gridLineColor = getColor("GridLineColor", gridLineColor);							// Farbe der Grid-Linien
				captionForeColor = getColor("CaptionForeColor", captionForeColor);
				pflichtBackColor = getColor("PflichtBackColor", pflichtBackColor);		// Hintergrund-Farbe Eingabefeld während der Eingabe
				pflichtForeColor = getColor("PflichtForeColor", PflichtForeColor);		// Hintergrund-Farbe Eingabefeld während der Eingabe

				gridBackColorOdd = getColor("gridBackColorOdd", gridBackColorOdd);
				gridBackColorEven = getColor("gridBackColorEven", gridBackColorEven);
				gridBackColorInputOdd = getColor("gridBackColorInputOdd", gridBackColorInputOdd);
				gridBackColorInputEven = getColor("gridBackColorInputEven", gridBackColorInputEven);
				gridForeColorOdd = getColor("gridForeColorOdd", gridForeColorOdd);
				gridForeColorEven = getColor("gridForeColorEven", gridForeColorEven);
				gridForeColorInputOdd = getColor("gridForeColorInputOdd", gridForeColorInputOdd);
				gridForeColorInputEven = getColor("gridForeColorInputEven", gridForeColorInputEven);
				gridBackColorCurrentRow = getColor("gridBackColorCurrentRow", gridBackColorCurrentRow);

				buttonBackColor = getColor("buttonBackColor", buttonBackColor);
				buttonForeColor = getColor("buttonForeColor", buttonForeColor);
				progressbarFillColor = getColor("ProgressbarFillColor", progressbarFillColor);			// Progressbar-farben
				progressbarForeColor = getColor("ProgressbarForeColor", progressbarForeColor);			// Progressbar-farben
			}
		}

		public static bool ExistUserConfigFile
		{
			get {return File.Exists(filename);}
		}

		public static bool ExistConfigFile(string file)
		{
				return File.Exists(file);
		}

		private static Color getColor(string farbe, Color vorbelegung)
		{
			Color result = vorbelegung;;

			object node =	xmldoc.SelectSingleNode("//configuration/colorSettings/" + farbe);
			if (node != null)
			{
				string f = xmldoc.SelectSingleNode("//configuration/colorSettings/" + farbe).InnerText;
				if (f.IndexOf(",") != -1)
				{
					char[] delimiter = ",".ToCharArray();
					string[] color;

					color = f.Split(delimiter, 4);
					result = Color.FromArgb(ConvertEx.ToInt32(color[alpha]), ConvertEx.ToInt32(color[red]), ConvertEx.ToInt32(color[green]),
						ConvertEx.ToInt32(color[blue]));
				}
				else
				{
					result = Color.FromName(f);
				}
			}
			return result;
		}

		private static void setColor(string farbe, Color color)
		{
			XmlNode root = xmldoc.SelectSingleNode("//configuration/colorSettings");
			XmlNode node = xmldoc.SelectSingleNode("//configuration/colorSettings/" + farbe);
			XmlElement element;

			if (node == null)
			{
				element = xmldoc.CreateElement(null,farbe,null);
				element.InnerText = color.A.ToString() + "," + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
				root.AppendChild(element);
			}
			else
				xmldoc.SelectSingleNode("//configuration/colorSettings/" + farbe).InnerText 
					=	color.A.ToString() + "," + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
		}

		public static Color GetColor(string farbe, Color vorbelegung)
		{
			Color result = vorbelegung;

			if (farbe.IndexOf("[") != -1)		// farbe liegt im Format "Color [DarkBlue]" oder "Color [A=255, R=0, G=128, B=0]" vor
			{
				farbe = farbe.Substring(farbe.IndexOf('[') + 1);
				farbe = farbe.Substring(0, farbe.Length - 1);
			}

			if (farbe.IndexOf("=") != -1)		// farbe liegt im Format "A=255, R=0, G=128, B=0" vor
			{
				farbe = farbe.Replace("A=", "");
				farbe = farbe.Replace("R=", "");
				farbe = farbe.Replace("G=", "");
				farbe = farbe.Replace("B=", "");
			}

			if (farbe != "")
			{
				if (farbe.IndexOf(",") != -1)// farbe liegt im Format "255, 0, 128, 0" vor
				{
					char[] delimiter = ",".ToCharArray();
					string[] color;

					color = farbe.Split(delimiter, 4);
					result = Color.FromArgb(ConvertEx.ToInt32(color[alpha]), ConvertEx.ToInt32(color[red]), ConvertEx.ToInt32(color[green]),
						ConvertEx.ToInt32(color[blue]));

					if (GetKnownColor(result) != "")
						result = Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), GetKnownColor(result)));
				}
				else
					result = Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), farbe));  //Farbe liegt als Name vor
			}
			else
				result = Color.Black;

			return result;
		}

		public static string SetColor(Color color)
		{
			return color.A.ToString() + "," + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
		}

		public static string GetKnownColor(Color color)
		{
			string result = "";
			Color temp;

			Array colors = Enum.GetValues(typeof(KnownColor));
			foreach (KnownColor knownColor in colors)
			{
				temp = Color.FromKnownColor(knownColor);
				if (temp.ToArgb() == color.ToArgb() && !temp.IsSystemColor)
				{
					result = knownColor.ToString();
					break;
				}
			}

			return result;
		}
	}
}
