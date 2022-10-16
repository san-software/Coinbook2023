using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
namespace OleDB
{
	public class DBSyncronize
	{
		private string dropBoxPath = "";
		private string dropBoxUser = "";
		private Boolean useDropBox = false;
		private String[] users;
    private FileSystemWatcher watcher;

		public void InitSyncronize()
		{
			if (File.Exists("cloud.config"))
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load("cloud.config");

				XmlNode settings = xmlDoc.SelectSingleNode("config");
				XmlNode dropbox = settings.SelectSingleNode("Dropbox");
				if (dropbox != null)
				{
					dropBoxPath = dropbox.Attributes["Path"].Value.ToString();
					dropBoxUser = dropbox.Attributes["User"].Value.ToString();
					useDropBox = Convert.ToBoolean(dropbox.Attributes["UseDropBox"].Value);

          if (Directory.Exists(dropBoxPath))
          {
            watcher = new FileSystemWatcher();

            users = Directory.GetDirectories(dropBoxPath);

            watcher.Path = dropBoxPath + @"\" + dropBoxUser;
            watcher.Filter = "*.dat";

            watcher.EnableRaisingEvents = true;

            watcher.Created += new FileSystemEventHandler(OnChanged);
          }
				}
			}
		}

		public void SyncronizeWrite(string cmd)
		{
			if (useDropBox)
			{
				DateTime time = DateTime.Now;
				string filename = time.Year.ToString() + time.Month.ToString("00") + time.Day.ToString("00") + time.Hour.ToString("00")
					+ time.Minute.ToString("00") + time.Second.ToString("00") + time.Millisecond.ToString("0000") + "-" + dropBoxUser + ".dat";

				for (int i = 0; i < users.Length; i++)
					if (users[i] != dropBoxPath + @"\" + dropBoxUser)
						System.IO.File.WriteAllText(users[i] + @"\" + filename, cmd);
			}
		}

		private void OnChanged(object source, FileSystemEventArgs e)
		{
			StreamReader file = new System.IO.StreamReader(e.FullPath);
			string cmd = file.ReadLine();
			file.Close();

			OleDBZugriff z = new OleDBZugriff();
			z.ExecuteSync(cmd);

			File.Delete(e.FullPath);
		}

		public void SyncronizeRead()
		{
			if (useDropBox)
			{
				OleDBZugriff z = new OleDBZugriff();

				string[] files = Directory.GetFiles(dropBoxPath + @"\" + dropBoxUser);

				for (int i = 0; i < files.Length; i++)
				{
					StreamReader file =	new System.IO.StreamReader(files[i]);
					string cmd = file.ReadLine();
					file.Close();

					z.ExecuteSync(cmd);

					File.Delete(files[i]);
				}
			}
		}

	}
}
