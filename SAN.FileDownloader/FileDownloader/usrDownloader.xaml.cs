//**************************************************************//
// FileDownloader Demo                                          //
// By De Dauw Jeroen - jeroendedauw@gmail.com                   //
//**************************************************************//
// Copyright 2009 - BN+ Discussions                             //
// http://code.bn2vs.com                                        //
//**************************************************************//

// This code is avaible at
// > BN+ Discussions: http://code.bn2vs.com/viewtopic.php?t=153
// > The Code Project: http://www.codeproject.com/KB/cs/BackgroundFileDownloader.aspx

// Dutch support can be found here: http://www.helpmij.nl/forum/showthread.php?t=416568

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAN.FileDownloader
{
    /// <summary>
    /// Interaktionslogik für usrDownloader.xaml
    /// </summary>
    public partial class usrDownloader : UserControl
    {
		public event EventHandler Ready;

		// Creating a new instance of a FileDownloader
		private FileDownloader downloader = new FileDownloader();

		private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public usrDownloader()
        {
            InitializeComponent();
			//CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
			//		new ExecutedRoutedEventHandler(delegate (object sender, ExecutedRoutedEventArgs args) { this.Close(); })));

			downloader.StateChanged += new EventHandler(downloader_StateChanged);
			downloader.CalculatingFileSize += new FileDownloader.CalculatingFileSizeEventHandler(downloader_CalculationFileSize);
			downloader.ProgressChanged += new EventHandler(downloader_ProgressChanged);
			downloader.FileDownloadAttempting += new EventHandler(downloader_FileDownloadAttempting);
			downloader.FileDownloadStarted += new EventHandler(downloader_FileDownloadStarted);
			downloader.Completed += new EventHandler(downloader_Completed);
			downloader.CancelRequested += new EventHandler(downloader_CancelRequested);
			downloader.DeletingFilesAfterCancel += new EventHandler(downloader_DeletingFilesAfterCancel);
			downloader.Canceled += new EventHandler(downloader_Canceled);
		}

		public void Add(string source, string target)
		{
			dictionary.Add(source, target);
		}

		public Boolean CalculateTotalProgress { get; set; }
		public Boolean DeleteCompletedFilesAfterCancel { get; set; }

		public void Start()
		{
			downloader.Files.Clear();                // Clear the current list of files (in case it's not the first download)

			downloader.SupportsProgress = CalculateTotalProgress;
			downloader.DeleteCompletedFilesAfterCancel = DeleteCompletedFilesAfterCancel;

			foreach (KeyValuePair<string, string> item in dictionary)
				downloader.Files.Add(new FileDownloader.FileInfo(item.Key, item.Value));

			// Start the downloader
			downloader.Start();
			//}
		}

		private void btnPause_Click(object sender, RoutedEventArgs e)
		{
			// Pause the downloader
			downloader.Pause();
		}

		private void btnResume_Click(object sender, RoutedEventArgs e)
		{
			// Resume the downloader
			downloader.Resume();
		}

		private void btnStop_Click(object sender, RoutedEventArgs e)
		{
			// Stop the downloader
			// Note: This will not be instantantanious - the current requests need to be closed down, and the downloaded files need to be deleted
			downloader.Stop();
			Result = false;
		}

		// This event is fired every time the paused or busy state is changed, and used here to set the controls of the interface
		// This makes it enuivalent to a void handling both downloader.IsBusyChanged and downloader.IsPausedChanged
		private void downloader_StateChanged(object sender, EventArgs e)
		{
			// Setting the buttons
			btnStop.IsEnabled = downloader.CanStop;
		}

		// Show the progress of file size calculation
		// Note that these events will only occur when the total file size is calculated in advance, in other words when the SupportsProgress is set to true
		private void downloader_CalculationFileSize(object sender, Int32 fileNr)
		{
			lblStatus.Content = String.Format("Berechne Dateigrössen - Datei {0} / {1}", fileNr, downloader.Files.Count);

			double p = ((double)fileNr / (double)downloader.Files.Count) * 100;

			pBarFileProgress.Value = p;

			string progress = string.Format("{0:##0.00}", p / 1000000);
			lblFileProgress.Content = String.Format("{0} berechnet", progress);
		}

		// Occurs every time of block of data has been downloaded, and can be used to display the progress with
		// Note that you can also create a timer, and display the progress every certain interval
		// Also note that the progress properties return a size in bytes, which is not really user friendly to display
		//      The FileDownloader class provides static functions to format these byte amounts to a more readible format, either in binary or decimal notation 
		private void downloader_ProgressChanged(object sender, EventArgs e)
		{
			try
			{
				pBarFileProgress.Value = downloader.CurrentFilePercentage();
			}
			catch { }

			string progress = string.Format("{0:##0.00}", downloader.CurrentFileProgress / 1000000);
			string percentage = string.Format("{0:##0.00}", downloader.CurrentFilePercentage());
			string size = string.Format("{0:##0.00}", downloader.CurrentFileSize / 1000000);
			string speed = string.Format("{0:##0.00}", downloader.DownloadSpeed / 1000000);

			lblFileProgress.Content = String.Format("{0} MiB / {1} MiB Dateien heruntergeladen ({2}%)", progress, size, percentage) + String.Format(" - {0} MiB/s", speed);

			if (downloader.SupportsProgress)
			{
				try
				{
					pBarTotalProgress.Value = downloader.TotalPercentage();
				}
				catch { }
				string totalProgress = string.Format("{0:##0.00}", downloader.TotalProgress / 1000000);
				string totalPercentage = string.Format("{0:##0.00}", downloader.TotalPercentage());
				size = string.Format("{0:##0.00}", downloader.TotalSize / 1000000);

				lblTotalProgress.Content = String.Format("{0} MiB / {1} MiB heruntergeladen ({2}%)", totalProgress, size, totalPercentage);
			}
		}

		// This will be shown when the request for the file is made, before the download starts (or fails)
		private void downloader_FileDownloadAttempting(object sender, EventArgs e)
		{
			lblStatus.Content = String.Format("Preparing {0}", downloader.CurrentFile.Path);
		}

		// Display of the file info after the download started
		private void downloader_FileDownloadStarted(object sender, EventArgs e)
		{
			lblStatus.Content = String.Format("{0} wird heruntergeladen {1} / {2} Dateien", System.IO.Path.GetFileName(downloader.CurrentFile.Ziel), downloader.CurrentFileNumber, downloader.TotalFileNumber);
			lblFileSize.Content = String.Format("Dateigröße {0}", FileDownloader.FormatSizeBinary(downloader.CurrentFileSize));
		}

		public Boolean Result { get; set; }
		// Display of a completion message, showing the amount of files that has been downloaded.
		// Note, this does not hold into account any possible failed file downloads
		private void downloader_Completed(object sender, EventArgs e)
		{
			lblStatus.Content = String.Format("Download komplett, {0} Dateien heruntergeladen.", downloader.Files.Count);
			Result = true;

			if (Ready != null)
				Ready(this, null);
		}

		// Show a message that the downloads are being canceled - all files downloaded will be deleted and the current ones will be aborted
		private void downloader_CancelRequested(object sender, EventArgs e)
		{
			lblStatus.Content = "Canceling downloads...";
		}

		// Show a message that the downloads are being canceled - all files downloaded will be deleted and the current ones will be aborted
		private void downloader_DeletingFilesAfterCancel(object sender, EventArgs e)
		{
			lblStatus.Content = "Canceling downloads - deleting files...";
		}

		// Show a message saying the downloads have been canceled
		private void downloader_Canceled(object sender, EventArgs e)
		{
			lblStatus.Content = "Download(s) abgebrochen";
			pBarFileProgress.Value = 0;
			pBarTotalProgress.Value = 0;
			lblFileProgress.Content = "-";
			lblTotalProgress.Content = "-";
			lblFileSize.Content = "-";
		}
	}
}
