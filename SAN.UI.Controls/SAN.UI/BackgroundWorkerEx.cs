/// <summary>
/// Same as BackgroundWorker, but fixes the IsBusy issue.
/// </summary>
 
using System;
using System.ComponentModel;
//using System.Windows.Forms;

namespace SAN.UI.Controls
{
	/// <summary>
	/// Summary description for ComboBoxEx.
	/// </summary>
	public class BackgroundWorkerEx : BackgroundWorker
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>

		/// <summary>    
		/// Occurs when RunWorkerAsync is called.    
		/// </summary>    
		public new event DoWorkEventHandler DoWork;

        public BackgroundWorkerEx()
		{
			base.DoWork += new DoWorkEventHandler(BackgroundWorkerEx_DoWork);			//Provide our own DoWork handler        
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Properties

		[Category("Behavior")]
		public object Tag
		{
			set;
			get;
		}

		public string Text
		{
			get;
			set;
		}

		public int Max
		{
			get;
			set;
		}

		/// <summary>   
		/// true as long as DoWork’s event handler is executing.    
		/// </summary>    
		public new bool IsBusy
		{
			get;
			private set;
		}
		#endregion

		private void BackgroundWorkerEx_DoWork(object sender, DoWorkEventArgs e)
		{        
			//the thread started        
			IsBusy = true;        
			try        
			{            
				DoWork(this, e);  				//call the user handler            
			}        
			finally        
			{            
				IsBusy = false;     				//user handler finished            
			}    
		}
	}

    public class ProgressParameter 
    {
        public ProgressParameter()
        {
            Text = string.Empty;
            Label = string.Empty;
        }

        public int Progress { get; set; }
        public int Max { get; set; }
        public int Command { get; set; }
        public string Text { get; set; }
        public string Label { get; set; }

    }
}