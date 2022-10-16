using System;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using System.Xml;

namespace SAN.Control
{
	/// <summary>
	/// Summary description for Helper.
	/// </summary>
	public class Helper
	{
		[DllImport("user32.dll")]
		private static extern int GetSystemMenu(int hwnd, int revert);

		[DllImport("user32.dll")]
		private static extern int GetMenuItemCount(int menu);

		[DllImport("user32.dll")]
		private static extern int RemoveMenu(int menu, int position, int flags);

		[DllImport("user32.dll")]
		private static extern int DeleteMenu(int menu, int position, int flags);

		[DllImport("User32")]
		private static extern bool SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		public static extern void ReleaseCapture();

		private const int MF_BYPOSITION = 0x0400;
		private const int MF_DISABLED = 0x00000002;
		private const int WM_NCLBUTTONDOWN = 0xA1; // 161
		private const int HTCAPTION = 2;

		private static string homeDrive = ConfigurationManager.AppSettings["Datapath"];
		private static string logFile = ConfigurationManager.AppSettings["LogFile"];

		public enum SysMenuItem
		{
			SysMenuItemRemove = 0,				// Wiederherstellen 
			SysMenuItemMove = 1,					// Verschieben 
			SysMenuItemSize = 2,					// 2 = Grösse ändern 
			SysMenuItemMinimize = 3,     // 3 = Minimieren 
			SysMenuItemMaximize = 4,     // 4 = Maximieren 
			SysMenuItemSeparator = 5,    // 5 = Trennlinie 
			SysMenuItemClose = 6					// 6 = Schliessen
		}

		static Helper()
		{
		}

		public static byte[] ReadFile(string fileName)
		{
			FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

			byte[] byteFeld = new Byte[stream.Length];
			stream.Read(byteFeld, 0, byteFeld.Length);

			stream.Close();
			stream.Dispose();

			return byteFeld;
		}

		public static void WriteFile(string fileName, byte[] byteFeld)
		{
			FileStream filestream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
			BinaryWriter writer = new BinaryWriter(filestream);
			writer.Write(byteFeld);
			writer.Flush();
			writer.Close();
		}

		public static string HomePath
		{
			get
			{
				if (homeDrive == null)
					homeDrive = "";

				if (homeDrive.Length > 0)
				{
					if (homeDrive.Substring(homeDrive.Length - 1, 1) != "/")
						homeDrive = homeDrive + "/";
				}
				else
				{
					if (DriveExists("H"))
						homeDrive = "H:/" + Application.ProductName + "/";
					else
						homeDrive = Path.GetTempPath() + Application.ProductName + "/";
				}

				if (!Directory.Exists(homeDrive))
					Directory.CreateDirectory(homeDrive);

				return homeDrive;
			}
		}

		public static string PCName
		{
			get
			{
				return Environment.MachineName;
			}
		}

		public static string LogFile
		{
			get
			{
				return logFile;
			}
		}

		public static void DrawFrame3D(Graphics g, int frameLeft, int frameTop, int frameWidth, int frameHeight)
		{
			Pen pen;
			int penWidth = 4;
			int left = frameLeft + 2;
			int top = frameTop + 2;
			int bottom = frameHeight - 2;
			int right = frameWidth - 2;

			pen = new Pen(Color.Blue, penWidth);

			g.DrawLine(pen, frameLeft, top, frameWidth, top);					// oben
			g.DrawLine(pen, left, frameLeft, left, frameHeight);			// links
			g.DrawLine(pen, frameLeft, bottom, frameWidth, bottom);		// unten
			g.DrawLine(pen, right, frameLeft, right, frameHeight);		// rechts


			penWidth = 1;
			pen = new Pen(Color.DodgerBlue, penWidth);
			left = 2;
			top = 2;
			bottom = bottom - 1;
			right = right - 1;

			g.DrawLine(pen, left, top, right, top);							// oben
			g.DrawLine(pen, left, top, left, bottom);						// links
			g.DrawLine(pen, left, bottom, right, bottom);				// unten
			g.DrawLine(pen, right, top, right, bottom);					// rechts
		}

		/// <summary>
		/// Disable a MenuItem. If "handle" is the handle of 
		/// a WinForm, then the close Button is disabled
		/// </summary>
		public static void DisableMenuItem(int handle)
		{
			int menu = GetSystemMenu(handle, 0);
			int count = GetMenuItemCount(menu);
			RemoveMenu(menu, count - 1, MF_DISABLED | MF_BYPOSITION);
		}

		public static string TempFileName
		{
			get
			{
				return HomePath + DateTime.Now.Ticks.ToString() + ".tmp";
			}
		}

		public static string Format(Double value, string format)
		{
			if (Double.IsNaN(value))
				return "";
			else
				return value.ToString(format);
		}

		// Farben von Bildern auf andere Farben mappen
		public static Bitmap ChangeBackColor(Bitmap sourceBitmap, Color color)
		{
			// Neues Bitmap mit den Ausmaßen des Originals erzeugen
			Bitmap destBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

			// ColorMap-Array für die Transformation der Farben erzeugen
			ColorMap[] map = new ColorMap[1];
			map[0] = new ColorMap();
			map[0].OldColor = sourceBitmap.GetPixel(0, 0);
			map[0].NewColor = color;

			// Grafik auf dem Bitmap-Objekt ausgeben und dabei ein neues ImageAttributes-Objekt mit dem ColorMap-Objekt übergeben 
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetRemapTable(map);
			Graphics g = Graphics.FromImage(destBitmap);
			g.DrawImage(sourceBitmap, new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), 0, 0, sourceBitmap.Width, sourceBitmap.Height,
				GraphicsUnit.Pixel, imageAttributes);
			g.Dispose();

			return destBitmap;
		}

		// Return true if text is in valid e-mail format.
		public static bool IsValidEmail(string text)
		{
			if (text.Length == 0)
				return true;
			else
				//return Regex.IsMatch(text, @"^([\w-\.]+)@([\w-]+)\.([a-zA-Z]{2,4}$");
				return Regex.IsMatch(text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
		}

		// Return true if text is in valid url format.
		public static bool IsValidUrl(string text)
		{
			if (text.Length == 0)
				return true;
			else
				return Regex.IsMatch(text, @"^[wW-]{3}(\.)([0-9a-zA-Z\.\-]{3,50})(\.)[a-zA-Z]{2,4}$");

		}

		// Return true if text is in valid url format.
		public static bool IsValidUser(string text, string regEx)
		{
			if (text.Length == 0 || regEx.Length == 0)
				return true;
			else
				return Regex.IsMatch(text, regEx);

		}

		// Return true if text is in valid  format.
		public static bool IsValidPhone(string text)
		{
			if (text.Length == 0)
				return true;
			else
				return Regex.IsMatch(text, @"^((\d{3,5}[/]\d{3,10})([-]\d{3,6})?)|((\d{3,5}[-]\d{3,10})([-]\d{3,6})?)$");
		}



		public static bool CheckValid(string text, TextBoxTyp typ)
		{
			bool isValid = true;

			switch (typ)
			{
				case TextBoxTyp.Text:
					break;

				case TextBoxTyp.Numeric:
					break;

				case TextBoxTyp.Email:
					isValid = IsValidEmail(text);
					break;

				case TextBoxTyp.Url:
					isValid = IsValidUrl(text);
					break;

				case TextBoxTyp.Telefon:
					isValid = IsValidPhone(text);
					break;
			}

			//if (MenueHelper.HasStatusbar && isValid)
			//  if (MenueHelper.Statusbar.Status != "")
			//    MenueHelper.Statusbar.Clear();

			return isValid;
		}

		public static bool IsRuntime
		{
			get
			{
				return (Application.ExecutablePath.ToLower().IndexOf("devenv.exe") > -1);
			}
		}

		public static bool IsExe
		{
			get
			{
				return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Runtime"]);
			}
		}

		// Prüft ob sich Control in der IDE befindet
		public static bool IsIDE
		{
			get
			{
				return Application.StartupPath.EndsWith(@"\Common7\IDE");
			}
		}

		//Prüft ob sich das Control gerade im designmode befindet
		public static bool IsDesignMode(System.Windows.Forms.Control control)
		{
			bool result = false;
			//Solange noch ein Control zum prüfen vorhanden ist
			while (control != null)
			{
				PropertyInfo siteProperty = control.GetType().GetProperty("Site");				//Die Site Eigenschaft des Controls auslesen

				//Falls die .Site Eigenschaft gefunden wurde
				if (siteProperty != null)
				{
					ISite site = siteProperty.GetGetMethod().Invoke(control, new object[0]) as ISite;					//Eigenschaftswert auslesen

					//Falls eine Site Eigenschaft vorhanden ist
					if (site != null)
						//Wenn sich das Control im DesignMode befindet
						if (site.DesignMode)
							result = true;							//Eins der Controls befindet sich noch im Design Mode
				}
				control = control.Parent;				//Parent auslesen, und auch hier die .DesignMode überprüfen
			}

			return result;			//Kein Control befand sich im Designmode  ---> false
		}

		public static void MenuItemRemove(Form form, SysMenuItem item)
		{
			int Handle = 0;
			long Result = 0;

			Handle = GetSystemMenu((int)form.Handle, -1);
			Result = DeleteMenu(Handle, (int)item, MF_BYPOSITION);
		}

		//Verschiebt eine Form (Objekt) ohne Titelleiste anhand des handles
		public static void MoveObject(IntPtr hWnd)
		{
			ReleaseCapture();
			SendMessage(hWnd, WM_NCLBUTTONDOWN, HTCAPTION, 0);
		}

		public static bool IsFormLoaded(string form)
		{
			bool result = false;

			FormCollection x = Application.OpenForms;
			for (int i = 0; i < x.Count; i++)
				if (x[i].Name == form)
					result = true;

			return result;
		}

		public static bool FileExists(string filename)
		{
			FileInfo file = new FileInfo(filename);

			return file.Exists;
		}

		public static void CreateHomeDir()
		{
			//Homeverzeichnis dür die Applikation erzeugen
			DirectoryInfo path = new DirectoryInfo(HomePath);
			if (!path.Exists)
				path.Create();

			//Verzeichnis für Archivierung anlegen falls noch nicht vorhanden
			path = new DirectoryInfo(HomePath + "/Archiv");
			if (!path.Exists)
				path.Create();

			//Verzeichnis für Temp anlegen falls noch nicht vorhanden
			path = new DirectoryInfo(HomePath + "/Temp");
			if (!path.Exists)
				path.Create();
		}

		//// Create and apply the given bitmap region on the supplied control
		//public static void CreateControlRegion(Control control, Bitmap bitmap)
		//{
		//  // Return if control and bitmap are null
		//  if (control == null || bitmap == null)
		//    return;

		//  // Set our control's size to be the same as the bitmap
		//  control.Width = bitmap.Width;
		//  control.Height = bitmap.Height;

		//  // Check if we are dealing with Form here
		//  if (control is FormEx)
		//  {
		//    // Cast to a Form object
		//    FormEx form = (FormEx)control;

		//    // Set our form's size to be a little larger that the bitmap just 
		//    // in case the form's border style is not set to none in the first 
		//    // place
		//    form.Width += 15;
		//    form.Height += 35;

		//    // No border
		//    form.FormBorderStyle = FormBorderStyle.None;

		//    // Set bitmap as the background image
		//    form.BackgroundImage = bitmap;

		//    // Calculate the graphics path based on the bitmap supplied
		//    GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);

		//    // Apply new region
		//    form.Region = new Region(graphicsPath);
		//  }

		//  // Check if we are dealing with Button here
		//  else if (control is ButtonEx)
		//  {
		//    // Cast to a button object
		//    ButtonEx button = (ButtonEx)control;

		//    // Do not show button text
		//    button.Text = "";

		//    // Change cursor to hand when over button
		//    button.Cursor = Cursors.Hand;

		//    // Set background image of button
		//    button.BackgroundImage = bitmap;

		//    // Calculate the graphics path based on the bitmap supplied
		//    GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);

		//    // Apply new region
		//    button.Region = new Region(graphicsPath);
		//  }

		//  // Check if we are dealing with Button here
		//  else if (control is GroupBoxEx)
		//  {
		//    GroupBoxEx groupBox = (GroupBoxEx)control;				// Cast to a button object
		//    groupBox.BackgroundImage = bitmap;				// Set background image of control

		//    // Calculate the graphics path based on the bitmap supplied
		//    GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);

		//    groupBox.Region = new Region(graphicsPath);				// Apply new region
		//  }
		//}

		// Calculate the graphics path that representing the figure in the bitmap 
		// excluding the transparent color which is the top left pixel.
		private static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap)
		{
			GraphicsPath graphicsPath = new GraphicsPath();			// Create GraphicsPath for our bitmap calculation
			Color colorTransparent = bitmap.GetPixel(0, 0);			// Use the top left pixel as our transparent color

			// This is to store the column value where an opaque pixel is first found.
			// This value will determine where we start scanning for trailing opaque pixels.
			int colOpaquePixel = 0;

			// Go through all rows (Y axis)
			for (int row = 0; row < bitmap.Height; row++)
			{
				// Reset value
				colOpaquePixel = 0;

				// Go through all columns (X axis)
				for (int col = 0; col < bitmap.Width; col++)
				{
					// If this is an opaque pixel, mark it and search 
					// for anymore trailing behind
					if (bitmap.GetPixel(col, row) != colorTransparent)
					{
						// Opaque pixel found, mark current position
						colOpaquePixel = col;

						// Create another variable to set the current pixel position
						int colNext = col;

						// Starting from current found opaque pixel, search for 
						// anymore opaque pixels trailing behind, until a transparent
						// pixel is found or minimum width is reached
						for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
							if (bitmap.GetPixel(colNext, row) == colorTransparent)
								break;

						// Form a rectangle for line of opaque pixels found and add it to our graphics path
						graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));

						// No need to scan the line of opaque pixels just found
						col = colNext;
					}
				}
			}

			// Return calculated graphics path
			return graphicsPath;
		}

		/// <summary>
		/// Checks if a number is even
		/// </summary>
		/// <param name="number">the number</param>
		/// <returns>true = even</returns>
		public static bool IsEven(int number)
		{
			return (number % 2 == 0);
		}

		////Run only one instance from you program
		//private void XXXXX_Load(object sender, EventArgs e)
		//{
		//  string m_UniqueIdentifier;
		//  string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).CodeBase;
		//  m_UniqueIdentifier = assemblyName.Replace("\\", "_");

		//  m_Mutex = new Mutex(false, m_UniqueIdentifier);
		//  if (m_Mutex.WaitOne(1, true))
		//  {
		//  }
		//  else
		//  {
		//    //Not the first instance!!!        
		//    m_Mutex.Close();
		//    m_Mutex = null;
		//    MessageBoxAdv.Show("Start only one Instance", "Allways runnning");
		//    this.Close();
		//  }
		//}

	// ------------------------------------------------------


		/// <summary>
		/// Zentriert die Überschrift der Form indem sie mit Leerzeichen aufgefüllt wird.
		/// </summary>
		/// <param name="formText">Die Ursprungsüberschrift (ohne führende Leerzeichen)</param>
		/// <returns>Die modifizierte Überschrift (mit führenden Leerzeichen)</returns>
		//private String zentriereFormUeberschrift(String formText)
		//{
		//  // erstelle ein temporäres Label zum ...
		//  Label lblTemp = new System.Windows.Forms.Label();
		//  lblTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		//  // ... berechnen der Breite eines Leerzeichens.
		//  labelTemp.Text = "";
		//  int einLeerzeichen = labelTemp.Width;
		//  labelTemp.Text = " ";
		//  einLeerzeichen = labelTemp.Width;
		//  labelTemp.Text = "  ";
		//  int zweiLeerzeichen = labelTemp.Width;
		//  einLeerzeichen = zweiLeerzeichen - einLeerzeichen;

		//  //Differenz/2 --> in Leerzeichen umrechnen und dem Text hintenanstellen.
		//  int anzLeerzeichen = (this.Width - labelTemp.Width) / 2 / einLeerzeichen; // '/ einLeerzeichen' weil n Pixel = 1 Leerzeichen
		//  labelTemp.Visible = false;

		//  String leerzeichen = "";
		//  for (int i = 0; i < anzLeerzeichen; i++)
		//    leerzeichen += " ";

		//  return leerzeichen + formText;
		//}

		public static ICollection SortedHashTable(Hashtable ht)
		{
			ArrayList sorter = new ArrayList();
			sorter.AddRange(ht);
			sorter.Sort();
			return sorter;
		}

		public static bool DriveExists(string drive)
		{
			bool result = false;
			try
			{
				System.IO.DriveInfo Drive = new System.IO.DriveInfo(drive);
				result = Drive.IsReady;
			}
			catch
			{
			}

			return result;
		}

		/// <summary>
		/// Setzt ein bestimmtes Bit in einem Byte.
		/// </summary>
		/// <param name="b">Byte, welches bearbeitet werden soll.</param>
		/// <param name="BitNumber">Das zu setzende Bit (0 bis 7).</param>
		/// <returns>Ergebnis - Byte</returns>
		public static byte SetBit(byte b, int BitNumber)
		{
			//Kleine Fehlerbehandlung
			if (BitNumber < 8)
			{
				return (byte)(b | (byte)(0x01 << BitNumber));
			}
			else
			{
				throw new InvalidOperationException(
				"Der Wert für BitNumber (int) war zu gross! (BitNumber = (min)0 - (max)7)"
				);
			}
		}

		/// <summary>
		/// Prüft, ob ein angegebenes Bit im Byte gesetzt ist.
		/// </summary>
		/// <param name="b">Byte, welches getestet werden soll.</param>
		/// <param name="BitNumber">Das zu prüfende Bit (0 bis 7).</param>
		/// <returns>gesetzt=true, nicht gesetzt=false</returns>
		public static bool CheckBitSet(byte b, int BitNumber)
		{
			//Kleine Fehlerbehandlung
			if (BitNumber < 8)
			{
				if ((byte)(b & (byte)(0x01 << BitNumber)) != (byte)0x00)
				{
					return true;
				}
				return false;
			}
			else
			{
				throw new InvalidOperationException(
				"Der Wert für BitNumber (int) war zu gross! (BitNumber = (min)0 - (max)7)"
				);
			}
		}

		/// <summary>
		/// Get a unique filename.
		/// </summary>
		/// <param name="directory">The directory.</param>
		/// <param name="filename">The filename.</param>
		/// <returns>a unique filename</returns>
		public static string GetUniqueFilename(string directory, string filename)
		{
			if (!File.Exists(directory + filename))
				return filename;
			else
			{
				int counter = 0;
				FileInfo fileInfo = new FileInfo(filename);
				while (File.Exists(directory + filename))
				{
					counter++;
					filename = fileInfo.Name.Replace(fileInfo.Extension, String.Empty) + counter + fileInfo.Extension;
				}
				return filename;
			}
		}

		public static string TempPath
		{
			get
			{
				string result = System.IO.Path.GetTempPath();

				if (System.IO.File.Exists(Application.StartupPath +"\\database.config"))
				{
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.Load(Application.StartupPath + "\\database.config");
					XmlNode datapath = xmlDoc.SelectSingleNode("Settings").SelectSingleNode("Datapath");

					if (datapath != null)
						result = datapath.InnerText;
				}

				return result;
			}
		}

	}


}
