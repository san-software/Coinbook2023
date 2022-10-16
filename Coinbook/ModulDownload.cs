using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace Coinbook
{
  public class ModulDownload
  {
    public event ProgressDownloadEventHandler ProgressChanged;

    WebClient webClient = new WebClient();

    public ModulDownload()
    {
      webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
      webClient.DownloadFileCompleted +=new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);

      Ready = false;
    }

    public void DownloadFile(string sourceFile, string targetFile)
    {
      //WebResponse response = null;

      string file1 = sourceFile.Replace(" ", "_");
      file1 = file1.Replace("ä", "ae");
      file1 = file1.Replace("Ö", "Oe");

      string jahr = file1.Substring(file1.IndexOf('-') + 1);
      jahr = jahr.Substring(0, jahr.IndexOf('.'));

      string url = "http://coinbook.de/Downloads/Module/" + jahr + "/" + file1;
      Uri uri = new Uri(url);

      //WebRequest request = WebRequest.Create(uri);
      //request.Method = "HEAD";

      bool fileFound = true;
      //try
      //{
      //  response = request.GetResponse();
      //}
      //catch
      //{
      //  fileFound = false;
      //}

      //if (fileFound && response.ContentLength == 0)
      //{
      //  fileFound = false;
      //}

      if (fileFound)
      {
         webClient.DownloadFileAsync(uri, targetFile);

        //webClient.DownloadFile(uri, targetFile);

         do
         {
           if (Ready)
             break;

           Thread.Sleep(1000);
         } while (true);
          
      }
    }

    private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
      if (ProgressChanged != null)
      {
        ProgressDownloadEventArgs args = new ProgressDownloadEventArgs();
        args.Percent = e.ProgressPercentage;
        args.Text = "Test";
        ProgressChanged(this, args);
      }

      //txtAnzeige.Text = file;
    }

    private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
      Ready = true;
    }

    public Boolean Ready { get; set; }
  }

  public delegate void ProgressDownloadEventHandler(Object sender, ProgressDownloadEventArgs e);

	public class ProgressDownloadEventArgs
	{
		public ProgressDownloadEventArgs()
		{
		}

		public string Text
		{
			set;
			get;
		}

		public int Percent {get;set;}
	}
}
