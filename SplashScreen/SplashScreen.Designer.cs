using System.Windows.Forms;

namespace Splash
{
	partial class SplashScreen : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lblStatus = new System.Windows.Forms.Label();
			this.pnlStatus = new System.Windows.Forms.Panel();
			this.lblTimeRemaining = new System.Windows.Forms.Label();
			this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.lblVersion = new System.Windows.Forms.Label();
			this.labelEx1 = new System.Windows.Forms.Label();
			this.lblLizenz = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.Location = new System.Drawing.Point(0, 378);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(381, 28);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.lblStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// pnlStatus
			// 
			this.pnlStatus.BackColor = System.Drawing.Color.Transparent;
			this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlStatus.Location = new System.Drawing.Point(0, 409);
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(770, 24);
			this.pnlStatus.TabIndex = 1;
			this.pnlStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// lblTimeRemaining
			// 
			this.lblTimeRemaining.BackColor = System.Drawing.Color.Orange;
			this.lblTimeRemaining.Location = new System.Drawing.Point(462, 27);
			this.lblTimeRemaining.Name = "lblTimeRemaining";
			this.lblTimeRemaining.Size = new System.Drawing.Size(279, 27);
			this.lblTimeRemaining.TabIndex = 2;
			this.lblTimeRemaining.Text = "Time remaining";
			this.lblTimeRemaining.Visible = false;
			this.lblTimeRemaining.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// UpdateTimer
			// 
			this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVersion.Location = new System.Drawing.Point(78, 240);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(231, 26);
			this.lblVersion.TabIndex = 3;
			this.lblVersion.Text = "labelEx1";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelEx1
			// 
			this.labelEx1.BackColor = System.Drawing.Color.Transparent;
			this.labelEx1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelEx1.Location = new System.Drawing.Point(78, 214);
			this.labelEx1.Name = "labelEx1";
			this.labelEx1.Size = new System.Drawing.Size(232, 26);
			this.labelEx1.TabIndex = 4;
			this.labelEx1.Text = Application.ProductName;
			this.labelEx1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblLizenz
			// 
			this.lblLizenz.BackColor = System.Drawing.Color.Transparent;
			this.lblLizenz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLizenz.Location = new System.Drawing.Point(79, 266);
			this.lblLizenz.Name = "lblLizenz";
			this.lblLizenz.Size = new System.Drawing.Size(231, 63);
			this.lblLizenz.TabIndex = 5;
			this.lblLizenz.Text = "labelEx1";
			this.lblLizenz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SplashScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.LightGray;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(770, 433);
			this.Controls.Add(this.lblLizenz);
			this.Controls.Add(this.labelEx1);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.lblTimeRemaining);
			this.Controls.Add(this.pnlStatus);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SplashScreen";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashScreen";
			this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			this.ResumeLayout(false);

		}
		#endregion


		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblTimeRemaining;
		private System.Windows.Forms.Timer UpdateTimer;
		private System.Windows.Forms.Panel pnlStatus;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label labelEx1;
		private System.Windows.Forms.Label lblLizenz;
	}
}