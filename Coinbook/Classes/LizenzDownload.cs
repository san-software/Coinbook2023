using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
//using Microsoft.Win32;

namespace Coinbook
{
  /// <summary>
  ///     Main class that lets you auto update applications by setting some static fields and executing its Start method.
  /// </summary>
  public class LizenzVerwaltung
  {

    private WebClient webClient;

    public string DataPath { get; set; }
    public string Email { get; set; }
    public DateTime FileDate { get; set; }
    public string URL { get; set; }
    public string Lizenzdatei { get; set; }

    /// <summary>
    ///     Start checking for new version of application and display dialog to the user if update is available.
    /// </summary>
    /// <param name="appCast">URL of the xml file that contains information about latest version of the application.</param>
    public bool LoadLizenz
    {
      get
      {
        bool result = false;

        //string url = "http://www.Coinbook.de/Downloads/Personalisierung/" + Email;

        if (FileExists)
        {
          if (File.Exists(Lizenzdatei))
          {
            FileInfo info = new FileInfo(Lizenzdatei);
            if (info.CreationTime < FileDate)
              File.Delete(Lizenzdatei);
          }

          if (!File.Exists(Lizenzdatei))
          {
            webClient = new WebClient();

            Uri uri = new Uri(URL);

            webClient.DownloadFile(uri, Lizenzdatei);

            FileInfo info = new FileInfo(Lizenzdatei);
            info.CreationTime = FileDate;

            result = true;
          }
          else
            result = false;
        }
        return result;
      }
    }

    public bool FileExists
    {
      get
      {
        bool result = true;

        try
        {
          System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
          System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
          if (response.StatusCode != HttpStatusCode.OK)
            result = false;

          if (result)
            FileDate = response.LastModified;
          else
            FileDate = Convert.ToDateTime("01.01.1900");

          response.Close();
          request = null;
        }
        catch
        { 
          result = false; 
        }

        return result;
      }

    }
  }
}