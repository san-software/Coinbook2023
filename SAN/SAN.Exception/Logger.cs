using System.IO;
using System.Windows.Forms;
using System;
using NLog;
using NLog.Targets;
using NLog.Config;
using NLog.Win32.Targets;


namespace SAN.Exception
{
	public class Nlogger
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		//private static readonly ILog logger = LogManager.GetLogger("root");
		private static string filename = "";
		private static bool error = false;
		public static string fileDate = "";

		public enum LogLevel
		{
			Debug,
			Info,
			Warning,
			Error,
			Fatal,
			Trace,
		}

		static Nlogger()
		{

			//Bereinigen();

			fileDate = System.DateTime.Now.ToShortDateString() + "-" + System.DateTime.Now.ToLongTimeString();
			fileDate = fileDate.Replace(".", "-").Replace(":", "-");

			filename = Application.LocalUserAppDataPath + "Logfile_" + fileDate + ".log";

			//foreach(IAppender appender in log4net.LogManager.GetRepository().GetAppenders())
			//{
			//	// Look for the appender you want to change. You can check the appender Name if that helpd
			//	if (appender is FileAppender)
			//	{
			//		((FileAppender)appender).File = filename;
			//		((FileAppender)appender).ActivateOptions();
			//	}

			//	if (appender is SmtpAppender)
			//	{
			//		From = ((SmtpAppender)appender).From;
			//		Port =((SmtpAppender) appender).Port;
			//		Host=((SmtpAppender) appender).SmtpHost;
			//		To = ((SmtpAppender) appender).To;
			//	}
			//}

			logger.Info("Programm " + Application.ProductName + " " + Application.ProductVersion + " wurde gestartet");
			logger.Info("User=" + Environment.UserName);
			logger.Info("Workstation=" + Environment.MachineName);
		}

		static void Configuration()
		{
			// Step 1. Create configuration object

			LoggingConfiguration config = new LoggingConfiguration();

			// Step 2. Create targets and add them to the configuration

			ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
			config.AddTarget("console", consoleTarget);

			FileTarget fileTarget = new FileTarget();
			config.AddTarget("file", fileTarget);

			// Step 3. Set target properties

			consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
			fileTarget.FileName = "${basedir}/file.txt";
			fileTarget.Layout = "${message}";

			// Step 4. Define rules

			LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
			config.LoggingRules.Add(rule1);

			LoggingRule rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
			config.LoggingRules.Add(rule2);

			// Step 5. Activate the configuration

			LogManager.Configuration = config;

			// Example usage

			Logger logger = LogManager.GetLogger("Example");
			logger.Trace("trace log message");
			logger.Debug("debug log message");
			logger.Info("info log message");
			logger.Warn("warn log message");
			logger.Error("error log message");
			logger.Fatal("fatal log message");
		}
	}

	public static void Dispose()
		{
			if (error)
				logger.Error("Programm wurde mit Fehlern verlassen");
			else
				logger.Info("Programm wurde regulär verlassen");

			LogManager.Shutdown();

			//if (filename.Length != 0)
			//{
			//  FileInfo file = new FileInfo(filename);
			//  filename = filename.Replace("Logfile","Logfile " + string.Format("{0:yyyy-MM-dd hh-mm}",file.CreationTime));
			//  if (file.Exists)
			//  {
			//    file.CopyTo(filename, true);
			//    file.Delete();
			//  }
			//}

		}

		public static void Log(LogLevel typ, object message)
		{
			switch (typ)
			{
				case LogLevel.Debug:
					logger.Debug(message);
					break;

				case LogLevel.Error:
					logger.Error("Programm " + Application.ProductName + " " + Application.ProductVersion);
					logger.Error("User=" + Environment.UserName);
					logger.Error("Workstation=" + Environment.MachineName);
					logger.Error("");
					logger.Error(message);
					logger.Error("");
					error = true;
					break;

				case LogLevel.Fatal:
					logger.Fatal("Programm " + Application.ProductName + " " + Application.ProductVersion);
					logger.Fatal("User=" + Environment.UserName);
					logger.Fatal("Workstation=" + Environment.MachineName);
					logger.Fatal("");
					logger.Fatal(message);
					logger.Fatal("");
					error = true;
					break;

				case LogLevel.Info:
					logger.Info(message);
					break;

				case LogLevel.Warning:
					logger.Warn(message);
					break;
			}
		}

//		public static void Bereinigen()
//		{
//			//DirectoryInfo directory = new DirectoryInfo(Helper.HomePath);
//			//FileInfo[] info = directory.GetFiles(Application.ProductName + "*.log");
//			//foreach (FileInfo fiTemp in info)
//			//{
//			//  if (fiTemp.LastWriteTime.AddDays(30)< System.DateTime.Now)
//			//    fiTemp.Delete();
//			//}
//		}

//		public static void Email(string email)
//		{
//			//if (email.Length != 0)
//			//{
//			//	foreach(log4net.Appender.IAppender appender in log4net.LogManager.GetRepository().GetAppenders())
//			//	{
//			//		// Look for the appender you want to change. You can check the appender Name if that helpd
//			//		if (appender is log4net.Appender.SmtpAppender)
//			//		{
//			//			log4net.Appender.SmtpAppender smtpAppender = (log4net.Appender.SmtpAppender)appender;
//			//			smtpAppender.From = email;					// Change value of properties
//			//		}
//			//	}
//			//}
//		}

//		public static string Filename
//		{
//			get
//			{
//				return filename;
//			}
//		}

//		public static string FileDate
//		{
//			get
//			{
//				return fileDate;
//			}
//		}
//		public static int Port
//		{
//			get;
//			set;
//		}

//		public static string Host
//		{
//			get;
//			set;
//		}

//		public static string From
//		{
//			get;
//			set;
//		}

//		public static string To
//		{
//			get;
//			set;
//		}

//		public static string Authentication
//		{
//			get; set;
//		}

//		public static void CloseLogfile()
//		{
//			logger.Info("Programm wurde mit Fehler beendet");

//			LogManager.Shutdown();
//		}

//		//public static string HomePath
//		//{
//		//  get
//		//  {
//		//    if (homeDrive == null)
//		//      homeDrive = "";

//		//    if (homeDrive.Length > 0)
//		//    {
//		//      if (homeDrive.Substring(homeDrive.Length - 1, 1) != "/")
//		//        homeDrive = homeDrive + "/";
//		//    }
//		//    else
//		//    {
//		//      if (DriveExists("H"))
//		//        homeDrive = "H:/" + Application.ProductName + "/";
//		//      else
//		//        homeDrive = Path.GetTempPath() + Application.ProductName + "/";
//		//    }

//		//    if (!Directory.Exists(homeDrive))
//		//      Directory.CreateDirectory(homeDrive);

//		//    return homeDrive;
//		//  }
//		//}
//	}
//}

