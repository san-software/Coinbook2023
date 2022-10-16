namespace Global
{
	partial class StatusBarEx
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panRecords = new System.Windows.Forms.Panel();
			this.lblRecords = new System.Windows.Forms.Label();
			this.panStatus = new System.Windows.Forms.Panel();
			this.lblStatus = new System.Windows.Forms.Label();
			this.panRolle = new System.Windows.Forms.Panel();
			this.panDate = new System.Windows.Forms.Panel();
			this.lblDate = new Global.LabelEx();
			this.lblRolle = new Global.LabelEx();
			this.progressbar = new Global.ProgressBarEx();
			this.panRecords.SuspendLayout();
			this.panStatus.SuspendLayout();
			this.panRolle.SuspendLayout();
			this.panDate.SuspendLayout();
			this.SuspendLayout();
			// 
			// panRecords
			// 
			this.panRecords.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panRecords.Controls.Add(this.lblRecords);
			this.panRecords.Location = new System.Drawing.Point(133, 4);
			this.panRecords.Name = "panRecords";
			this.panRecords.Size = new System.Drawing.Size(124, 19);
			this.panRecords.TabIndex = 6;
			// 
			// lblRecords
			// 
			this.lblRecords.AutoSize = true;
			this.lblRecords.Location = new System.Drawing.Point(38, 2);
			this.lblRecords.Name = "lblRecords";
			this.lblRecords.Size = new System.Drawing.Size(35, 13);
			this.lblRecords.TabIndex = 1;
			this.lblRecords.Text = "label1";
			// 
			// panStatus
			// 
			this.panStatus.BackColor = System.Drawing.SystemColors.Control;
			this.panStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panStatus.Controls.Add(this.progressbar);
			this.panStatus.Controls.Add(this.lblStatus);
			this.panStatus.Location = new System.Drawing.Point(3, 0);
			this.panStatus.Name = "panStatus";
			this.panStatus.Size = new System.Drawing.Size(124, 23);
			this.panStatus.TabIndex = 7;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(24, 3);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(35, 13);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Text = "label1";
			// 
			// panRolle
			// 
			this.panRolle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panRolle.Controls.Add(this.lblRolle);
			this.panRolle.Location = new System.Drawing.Point(263, 4);
			this.panRolle.Name = "panRolle";
			this.panRolle.Size = new System.Drawing.Size(124, 19);
			this.panRolle.TabIndex = 8;
			// 
			// panDate
			// 
			this.panDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panDate.Controls.Add(this.lblDate);
			this.panDate.Location = new System.Drawing.Point(393, 4);
			this.panDate.Name = "panDate";
			this.panDate.Size = new System.Drawing.Size(124, 19);
			this.panDate.TabIndex = 9;
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.BackColor = System.Drawing.SystemColors.Control;
			this.lblDate.Location = new System.Drawing.Point(54, 0);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(30, 13);
			this.lblDate.TabIndex = 4;
			this.lblDate.Text = "Date";
			// 
			// lblRolle
			// 
			this.lblRolle.AutoSize = true;
			this.lblRolle.BackColor = System.Drawing.SystemColors.Control;
			this.lblRolle.Location = new System.Drawing.Point(62, 2);
			this.lblRolle.Name = "lblRolle";
			this.lblRolle.Size = new System.Drawing.Size(31, 13);
			this.lblRolle.TabIndex = 3;
			this.lblRolle.Text = "Rolle";
			// 
			// progressbar
			// 
			this.progressbar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(10)))));
			this.progressbar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(240)))), ((int)(((byte)(170)))));
			this.progressbar.Location = new System.Drawing.Point(65, -3);
			this.progressbar.Margin = new System.Windows.Forms.Padding(0);
			this.progressbar.MaxValue = 100;
			this.progressbar.MinValue = 0;
			this.progressbar.Name = "progressbar";
			this.progressbar.Size = new System.Drawing.Size(46, 21);
			this.progressbar.TabIndex = 1;
			this.progressbar.Text = "progressbar";
			this.progressbar.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.progressbar.Value = 50;
			// 
			// StatusBarEx
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.panDate);
			this.Controls.Add(this.panRolle);
			this.Controls.Add(this.panStatus);
			this.Controls.Add(this.panRecords);
			this.Name = "StatusBarEx";
			this.Size = new System.Drawing.Size(1174, 26);
			this.SizeChanged += new System.EventHandler(this.StatusBarEx_SizeChanged);
			this.panRecords.ResumeLayout(false);
			this.panRecords.PerformLayout();
			this.panStatus.ResumeLayout(false);
			this.panStatus.PerformLayout();
			this.panRolle.ResumeLayout(false);
			this.panRolle.PerformLayout();
			this.panDate.ResumeLayout(false);
			this.panDate.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Global.LabelEx lblRolle;
		private Global.LabelEx lblDate;
		private System.Windows.Forms.Panel panRecords;
		private System.Windows.Forms.Panel panStatus;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Panel panRolle;
		private System.Windows.Forms.Panel panDate;
		private System.Windows.Forms.Label lblRecords;
		private ProgressBarEx progressbar;
	}
}
