using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Drawing.Imaging;
using SAN.Screen;
using System.Diagnostics;
using System.IO;

namespace SAN.Exception
{
	/// <summary>
	/// Summary description for frmException.
	/// </summary>
	public class frmException : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdOK;
		private TabControl tabControlEx1;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private RichTextBox txtError;
		private Button btnEnd;
		private System.ComponentModel.Container components = null;
		private TabPage tabPage3;
		private RichTextBox txtStacktrace;
		private Label lblError;
		private ScreenCapture hardcopy = new ScreenCapture();

		public frmException(System.Threading.ThreadExceptionEventArgs e)
		{
			InitializeComponent();

			Logger1.Log(Logger1.LogLevel.Error, GetCompleteMessage(e.Exception));
			hardcopy.CaptureScreenToFile(Logger1.Filename.Replace(".log", ".jpg"), ImageFormat.Jpeg);
			//hardcopy.CaptureWindowToFile(MenueHelper.MDIForm.Handle, Logger.Filename.Replace(".log", ".jpg"), ImageFormat.Jpeg);

			lblError.Text=e.Exception.ToString();
			txtStacktrace.Text=e.Exception.StackTrace;
			try
			{
				txtError.Text = e.Exception.InnerException.ToString();
			}
			catch{}

			//OleDBConnection.Rollback();
		}

		public frmException()
		{
			InitializeComponent();

			Logger1.Log(Logger1.LogLevel.Error, "Test");
			hardcopy.CaptureScreenToFile(Logger1.Filename.Replace(".log", ".jpg"), ImageFormat.Jpeg);
			//hardcopy.CaptureWindowToFile(MenueHelper.MDIForm.Handle, Logger.Filename.Replace(".log", ".jpg"), ImageFormat.Jpeg);

			lblError.Text = "Test";
			txtStacktrace.Text ="Test";
			try
			{
				txtError.Text = "Test";
			}
			catch
			{
			}

			//OleDBConnection.Rollback();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdOK = new System.Windows.Forms.Button();
			this.btnEnd = new Button();
			this.tabControlEx1 = new TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lblError = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtError = new System.Windows.Forms.RichTextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.txtStacktrace = new System.Windows.Forms.RichTextBox();
			this.tabControlEx1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Enabled = false;
			this.cmdOK.Location = new System.Drawing.Point(8, 512);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(104, 24);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// btnEnd
			// 
			this.btnEnd.BackColor = System.Drawing.SystemColors.Control;
			this.btnEnd.ForeColor = System.Drawing.Color.Black;
			this.btnEnd.Location = new System.Drawing.Point(118, 514);
			this.btnEnd.Name = "btnEnd";
			this.btnEnd.Size = new System.Drawing.Size(114, 22);
			this.btnEnd.TabIndex = 4;
			this.btnEnd.Text = "Programm Beenden";
			this.btnEnd.UseVisualStyleBackColor = true;
			this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
			// 
			// tabControlEx1
			// 
			this.tabControlEx1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControlEx1.Controls.Add(this.tabPage1);
			this.tabControlEx1.Controls.Add(this.tabPage2);
			this.tabControlEx1.Controls.Add(this.tabPage3);
			this.tabControlEx1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabControlEx1.Location = new System.Drawing.Point(3, 4);
			this.tabControlEx1.Name = "tabControlEx1";
			this.tabControlEx1.SelectedIndex = 0;
			this.tabControlEx1.Size = new System.Drawing.Size(1025, 502);
			this.tabControlEx1.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.tabPage1.Controls.Add(this.lblError);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1017, 476);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Fehlermeldung";
			// 
			// lblError
			// 
			this.lblError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblError.Location = new System.Drawing.Point(3, 3);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(1011, 470);
			this.lblError.TabIndex = 1;
			this.lblError.Text = "Error";
			this.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.tabPage2.Controls.Add(this.txtError);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1017, 476);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "ErrorText";
			// 
			// txtError
			// 
			this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtError.Location = new System.Drawing.Point(3, 3);
			this.txtError.Name = "txtError";
			this.txtError.Size = new System.Drawing.Size(1011, 470);
			this.txtError.TabIndex = 3;
			this.txtError.Text = "";
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.tabPage3.Controls.Add(this.txtStacktrace);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(1017, 476);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Stacktrace";
			// 
			// txtStacktrace
			// 
			this.txtStacktrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtStacktrace.Location = new System.Drawing.Point(0, 0);
			this.txtStacktrace.Name = "txtStacktrace";
			this.txtStacktrace.Size = new System.Drawing.Size(1017, 476);
			this.txtStacktrace.TabIndex = 3;
			this.txtStacktrace.Text = "";
			// 
			// frmException
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1040, 542);
			this.Controls.Add(this.btnEnd);
			this.Controls.Add(this.tabControlEx1);
			this.Controls.Add(this.cmdOK);
			this.Name = "frmException";
			this.ShowInTaskbar = false;
			this.Text = "frmException";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmException_FormClosing);
			this.tabControlEx1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			//MenueHelper.BusyOff();
			this.Close();
		}

		private void btnEnd_Click(object sender, EventArgs e)
		{
			Application.Exit();
			//MenueHelper.MDIForm.Close();
		}

		private string GetCompleteMessage(System.Exception exception)
		{
			System.Exception x = exception.InnerException;
			System.Text.StringBuilder msg = new StringBuilder(exception.Message);
			while (x != null)
			{
				msg.AppendFormat("\r\n\r\n{0}", x.Message);
				x = x.InnerException;
			}
			msg.Append("\r\n----Stacktrace----\r\n");
			msg.Append(exception.StackTrace);
			return msg.ToString();
		}

		private void frmException_FormClosing(object sender, FormClosingEventArgs e)
		{
			Logger1.CloseLogfile();

			if (File.Exists("Email.Exe"))
			{
				// Email Programm aufrufen und Email senden
				string args = Logger1.From + " " + Application.ProductName + " " + Logger1.Filename + " " + Logger1.Port + " " + Logger1.Host;

				Process process = new Process();
				process.StartInfo.FileName = "Email.Exe";
				process.StartInfo.Arguments = args;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.Start();
				//process.WaitForExit();
				//string result = process.StandardOutput.ReadToEnd();

				//System.Diagnostics.Process.Start("Email.Exe", args);
			}
		}
	}
}
