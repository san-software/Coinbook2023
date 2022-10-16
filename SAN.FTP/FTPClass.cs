using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using FluentFTP;
using System.Net;
using System.Xml.Serialization;
using System.Text;

namespace SAN.FTP
{
	public class FTPClass :IDisposable
	{
		FtpClient client;

		public bool Connect(string host, string user, string passwort, bool showErrorText = true)
		{
			bool result = true;

			client = new FtpClient(host);    // create an FTP client

            // if you don't specify login credentials, we use the "anonymous" user account
            client.Credentials = new NetworkCredential(user, passwort);

            // begin connecting to the server

            try
			{
				client.Connect();
			}
			catch
			{
                if (showErrorText)
                {
                    string text = "Es konnte leider keine Internetverbindung hergestellt werden." + Environment.NewLine + Environment.NewLine
                        + "Die gewünschte Funktion kann ohne funktionerendes Internet leider nicht ausgeführt werden." + Environment.NewLine + Environment.NewLine
                        + "Bitte überprüfen Sie, ob Ihr PC überhaupt einen Internetzugang hat." + Environment.NewLine
                        + "Überprüfen Sie Ihren Router und die Kabelverbindungen." + Environment.NewLine
                        + "Prüfen Sie Ihre Internet-Einstellungen am PC." + Environment.NewLine + Environment.NewLine
                        + "Versuchen Sie es anschließend erneut." + Environment.NewLine + Environment.NewLine
                        + "Wenn Sie trotz richtiger Funktion Ihres Internetzugangs keinen Erfolg haben, kontaktieren Sie bitte unseren Support.";

                    MessageBox.Show(text, "Fehlerhafter Internetzugang", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

				result = false;
			}

			return result;
		}

		public bool Upload(string file, string destination)
		{
			bool fileExists = false;
			bool result = false;

			if (client.FileExists(destination))
			{
				var x = client.GetObjectInfo(destination);
				fileExists = (x.Modified >= File.GetLastWriteTime(file));
			}

			if (!fileExists)
			{
				client.UploadFile(file, destination);
				result = true;
			}

			return result;
		}

		public bool FileExists(string file)
		{
			return client.FileExists(file);

		}

		public void Disconnect()
		{
			client.Disconnect();			// disconnect! good bye!
		}

		/// <summary>
		/// Download komplette datei
		/// </summary>
		/// <param name="file">Pfad im Web</param>
		/// <param name="target">Pfad auf dem PC</param>
		/// <param name="modifyDate">
		/// Wenn modifyDate ist null dann immer herunterladen
		/// Wenn Datum vorhanden, dann nur herunterladen, wenn die Datei im Web jünger ist als die auf der Platte</param>
		/// <returns></returns>
		public enmFTPFile Download(string file, string target, DateTime? modifyDate = null)
		{
			enmFTPFile result = enmFTPFile.FileNotExits;
			DateTime datum;

			if (client.FileExists(file))
			{
				try
				{
					if (modifyDate != null)
					{
						datum = client.GetModifiedTime(file);
						if (datum <= modifyDate)
							result = enmFTPFile.FileDateLowerAndEqual;
					}

				if (result == enmFTPFile.FileNotExits)
				{
						datum = client.GetModifiedTime(file);
						client.DownloadFile(target, file, FtpLocalExists.Overwrite, FtpVerify.Delete & FtpVerify.Retry);
						File.SetCreationTime(target, datum);
						File.SetLastWriteTime(target, datum);
						File.SetLastAccessTime(target, datum);
					result = enmFTPFile.FileDownloadOK;
					}
				}
				catch
				{
					result = enmFTPFile.FileDownloadError;
				}
			}

			return result;
		}

		/// <summary>
		/// download des Inhaltes einer Datei als string
		/// </summary>
		/// <param name="file">Datei incl Pfad im web</param>
		/// <returns>Inhalt der Textdatei</returns>
		public string DownloadString(string file)
		{
			MemoryStream stream = new MemoryStream();
			string result = "";

			if (client.FileExists(file))
			{
				try
				{
					client.Download(stream, file);

					stream.Position = 0;
					var sr = new StreamReader(stream);
					result = sr.ReadToEnd();
				}
				catch
				{
					result = "";
				}
			}

			return result;
		}

		/// <summary>
		/// Datei als Bytefeld downloaden
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public byte[] DownloadBlob(string file)
		{
			MemoryStream stream = new MemoryStream();
			byte[] result = null;

			if (client.FileExists(file))
			{
				try
				{
					long l = client.GetFileSize(file);
					result = new byte[l];
					client.Download(out result, file);
				}
				catch
				{
					result = null;
				}
			}

			return result;
		}

        public List<string> Files()
        {
            List<string> files = new List<string>();

            FtpListItem[] fileList = client.GetListing();

            if (fileList.Length != 0)
            {
                foreach (FtpListItem item in fileList)
                {
                    files.Add(item.FullName);
                }
            }

            return files;
        }

        public List<string> Files(string path, string filter)
		{
			List<string> result = new List<string>();

			FtpListItem[] files = client.GetListing(path);

			if (filter == ".*")
			{
				foreach (FtpListItem item in files)
					result.Add(item.Name);
			}
			else
			{
				foreach (FtpListItem item in files)
					if (Path.GetExtension(item.Name) == filter)
						result.Add(item.Name);
			}

			return result;
		}

		/// <summary>
		/// Setzt das Verzeichnis, aus dem gelsen werden soll.
		/// </summary>
		/// <param name="path"></param>
		public void SetWorkingDirectory(string path)
		{
			client.SetWorkingDirectory(path);
		}

		/// <summary>
		/// Datei im Web löschen
		/// </summary>
		/// <param name="path">Pafd und Name der datei</param>
		public void DeleteFile(string path)
		{
			client.DeleteFile(path);
		}

		public void CreateDirectory(string path)
		{
			if (!client.DirectoryExists(path))
				client.CreateDirectory(path, true);
		}

		/// <summary>
		/// Download komplette datei
		/// </summary>
		/// <param name="file">Pfad im Web</param>
		/// <param name="target">Pfad auf dem PC</param>
		/// <param name="modifyDate">
		/// Wenn modifyDate ist null dann immer herunterladen
		/// Wenn Datum vorhanden, dann nur herunterladen, wenn die Datei im Web jünger ist als die auf der Platte</param>
		/// <returns></returns>
		public enmFTPFile Downloadx(string file, string target, DateTime? modifyDate = null)
		{
			enmFTPFile result = enmFTPFile.FileNotExits;
			DateTime datum;

			if (client.FileExists(file))
			{
				try
				{
					if (modifyDate != null)
					{
						datum = client.GetModifiedTime(file);
						if (datum <= modifyDate)
							result = enmFTPFile.FileDateLowerAndEqual;
					}

					if (result == enmFTPFile.FileNotExits)
					{
						datum = client.GetModifiedTime(file);
						client.DownloadFileAsync(target, file, FtpLocalExists.Overwrite, FtpVerify.Delete & FtpVerify.Retry);
						File.SetCreationTime(target, datum);
						File.SetLastWriteTime(target, datum);
						File.SetLastAccessTime(target, datum);
						result = enmFTPFile.FileDownloadOK;
					}
				}
				catch
				{
					result = enmFTPFile.FileDownloadError;
				}
			}

			return result;
		}

		public long GetFilesize(string path)
		{
			return client.GetFileSize(path);
		}

		public DateTime GetModifiedTime(string path)
		{
			return client.GetModifiedTime(path);
		}

        public FTPModel FTPParameter
        {
			get
			{
				FTPModel ftp = null;

				XmlSerializer serializer = new XmlSerializer(typeof(FTPModel));
				var file = "ftpModule.config";
				var x = File.Exists(file);

				using (Stream reader = new FileStream(file, FileMode.Open))
					ftp = (FTPModel)serializer.Deserialize(reader);

				ftp.Passwort = Decrypt(ftp.Passwort);
				ftp.TransferPasswort = "magixx-1";

				return ftp;
			}
        }

		private string Decrypt(string text)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(text));
		}

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }

	public enum enmFTPFile
	{
		FileNotExits,
		FileDateLowerAndEqual,
		FileDownloadOK,
		FileDownloadError
	}
}
