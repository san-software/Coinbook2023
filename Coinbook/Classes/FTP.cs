using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Coinbook
{
	class FTP
	{
		private FtpWebRequest ftpRequest = null;
		private FtpWebResponse ftpResponse = null;
		private Stream ftpStream = null;
		private int bufferSize = 2048;

    public event FTPEventHandler FTPProzess;

    public string Host { get; set; }
    public string User { get; set; }
    public string Passwort { get; set; }

		/* Download File */
		public void download(string remoteFile, string localFile)
		{
      //string x = getFileSize(remoteFile);

			try
			{
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + remoteFile);				/* Create an FTP Request */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);				/* Log in to the FTP Server with the User Name and Password Provided */
				
        /* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;

				ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;				                  /* Specify the Type of FTP Request */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();				                  /* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();          				                  /* Get the FTP Server's Response Stream */
				FileStream localFileStream = new FileStream(localFile, FileMode.Create);				/* Open a File Stream to Write the Downloaded File */
				byte[] byteBuffer = new byte[bufferSize];			              	                  /* Buffer for the Downloaded Data */

        long gesendet = 0;
				int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);

        FTPEventArgs args = new FTPEventArgs(100, remoteFile);

				/* Download the File by Writing the Buffered Data Until the Transfer is Complete */
				try
				{
					while (bytesRead > 0)
					{
            args.SendetBytes = gesendet;

            if (FTPProzess != null)
              FTPProzess(this, args);

						localFileStream.Write(byteBuffer, 0, bytesRead);
            gesendet = gesendet + 1;
						bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
					}

          args.SendetBytes = gesendet;

          if (FTPProzess != null)
            FTPProzess(this, args);
				}
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				localFileStream.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return;
		}

		/* Upload File */
		public string upload(string remoteFile, string localFile)
		{
			string result = localFile + " ---> " + Host + "/" + remoteFile;

			try
			{
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + remoteFile);				/* Create an FTP Request */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);				/* Log in to the FTP Server with the User Name and Password Provided */

        /* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;

				ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;				/* Specify the Type of FTP Request */
				ftpStream = ftpRequest.GetRequestStream();				/* Establish Return Communication with the FTP Server */
				FileStream localFileStream = new FileStream(localFile, FileMode.Open);				/* Open a File Stream to Read the File for Upload */
				byte[] byteBuffer = new byte[bufferSize];				/* Buffer for the Downloaded Data */

        FileInfo i = new FileInfo(localFile);
        long gesendet = 0;

        int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);

        FTPEventArgs args = new FTPEventArgs(i.Length, i.Name);

				/* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
				try
				{
					while (bytesSent != 0)
					{
            args.SendetBytes = gesendet;

 		        if (FTPProzess != null)
				      FTPProzess(this, args);
            
            ftpStream.Write(byteBuffer, 0, bytesSent);
            gesendet = gesendet + bytesSent;
            bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
					}

          args.SendetBytes = gesendet;

          if (FTPProzess != null)
            FTPProzess(this, args);

					result = "Kopiert " + result;
				}

				catch (Exception ex) 
				{
					result = "Fehler: " + result;
					Console.WriteLine(ex.ToString()); 
				}
				/* Resource Cleanup */
				localFileStream.Close();
				ftpStream.Close();
				ftpRequest = null;
			}
			catch (Exception ex) 
			{ 
				Console.WriteLine(ex.ToString()); 
			}
			return result;
		}

		/* Delete File */
		public void delete(string deleteFile)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)WebRequest.Create(Host + "/" + deleteFile);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Resource Cleanup */
				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return;
		}

		/* Rename File */
		public void rename(string currentFileNameAndPath, string newFileName)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)WebRequest.Create(Host + "/" + currentFileNameAndPath);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.Rename;
				/* Rename the File */
				ftpRequest.RenameTo = newFileName;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Resource Cleanup */
				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return;
		}


		/* Create a New Directory on the FTP Server */
		public void createDirectory(string newDirectory)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)WebRequest.Create(Host + "/" + newDirectory);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Resource Cleanup */
				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return;
		}

		/* Get the Date/Time a File was Created */
		public string getFileCreatedDateTime(string fileName)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + fileName);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream);
				/* Store the Raw Response */
				string fileInfo = null;
				/* Read the Full Response Stream */
				try { fileInfo = ftpReader.ReadToEnd(); }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return File Created Date Time */
				return fileInfo;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			/* Return an Empty string Array if an Exception Occurs */
			return String.Empty;
		}

		/* Get the Size of a File */
		public string getFileSize(string fileName)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + fileName);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				return ftpResponse.ContentLength.ToString();

				//	/* Establish Return Communication with the FTP Server */
				//	ftpStream = ftpResponse.GetResponseStream();
				//	/* Get the FTP Server's Response Stream */
				//	StreamReader ftpReader = new StreamReader(ftpStream);
				//	/* Store the Raw Response */
				//	string fileInfo = null;
				//	/* Read the Full Response Stream */
				//	try
				//     { 
				//       while (ftpReader.Peek() != -1) 
				//       { 
				//         fileInfo = ftpReader.ReadToEnd(); 
				//       } 
			}
			catch (Exception ex) 
			{ 
				Console.WriteLine(ex.ToString()); 
			}

		//	/* Resource Cleanup */
		//	ftpReader.Close();
		//	ftpStream.Close();
		//	ftpResponse.Close();
		//	ftpRequest = null;
		//	/* Return File Size */
		//	return fileInfo;
		//}
		//		catch (Exception ex) { Console.WriteLine(ex.ToString()); }
		//		/* Return an Empty string Array if an Exception Occurs */
			return String.Empty;
		}

		/* List Directory Contents File/Folder Name Only */
		public string[] directoryListSimple(string directory)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + directory);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream);
				/* Store the Raw Response */
				string directoryRaw = null;
				/* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			/* Return an Empty string Array if an Exception Occurs */
			return new string[] { String.Empty };
		}

		/* List Directory Contents in Detail (Name, Size, Created, etc.) */
		public string[] directoryListDetailed(string directory)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + directory);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(User, Passwort);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream);
				/* Store the Raw Response */
				string directoryRaw = null;
				/* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			/* Return an Empty string Array if an Exception Occurs */
			return new string[] { String.Empty };
		}

    private string fileSize(string fileName)
    {
      try
      {
        FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + fileName);        ///* Create an FTP Request */
        ftpRequest.Credentials = new NetworkCredential(User, Passwort);       ///* Log in to the FTP Server with the User Name and Password Provided */
                                                                              ///
        /* When in doubt, use these options */
        ftpRequest.UseBinary = true;
        ftpRequest.UsePassive = true;
        ftpRequest.KeepAlive = true;

        ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;                        /* Specify the Type of FTP Request */
        FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();        /* Establish Return Communication with the FTP Server */

        string result = ftpRequest.ContentLength.ToString();

        ftpRequest=null;
        ftpResponse=null;

        return result;
      }
      catch (Exception ex) { Console.WriteLine(ex.ToString()); }
      /* Return an Empty string Array if an Exception Occurs */
      return String.Empty;
    }
	}

  public delegate void FTPEventHandler(object sender, FTPEventArgs args);

  public class FTPEventArgs : EventArgs
  {
    private long bytesToSend;
    private string file;

    #region Constructors

    /// <summary>
    /// Creates a new instance
    /// </summary>
    /// <param name="column">Column the <see cref="IGridFilter"/> is created for.</param>
    /// <param name="gridFilter">Default <see cref="IGridFilter"/> instance.</param>
    public FTPEventArgs(long bytesToSend, string file)
    {
      this.bytesToSend = bytesToSend;
      this.file = file;
    }

    #endregion

    #region Public Interface
    public long BytesToSend
    {
      get { return bytesToSend; }
    }

    public string File
    {
      get
      {
        return file;
      }
    }

    public long SendetBytes {get;set;}
    #endregion


  }
}
