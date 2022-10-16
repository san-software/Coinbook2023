namespace Coinbook.Konvert
{
  partial class frmKonvert
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
            this.bgwVersion26 = new System.ComponentModel.BackgroundWorker();
            this.pgbProgress = new System.Windows.Forms.ProgressBar();
            this.lblText = new System.Windows.Forms.Label();
            this.lblEnde = new System.Windows.Forms.Label();
            this.txtAnzeige = new System.Windows.Forms.TextBox();
            this.pgbProgress2 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bgwVersion26
            // 
            this.bgwVersion26.WorkerReportsProgress = true;
            this.bgwVersion26.WorkerSupportsCancellation = true;
            this.bgwVersion26.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwVersion26_DoWork);
            this.bgwVersion26.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwVersion26_ProgressChanged);
            this.bgwVersion26.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwVersion26_RunWorkerCompleted);
            // 
            // pgbProgress
            // 
            this.pgbProgress.Location = new System.Drawing.Point(24, 189);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(871, 23);
            this.pgbProgress.Step = 1;
            this.pgbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbProgress.TabIndex = 0;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(21, 157);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(41, 13);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "label1";
            // 
            // lblEnde
            // 
            this.lblEnde.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnde.Location = new System.Drawing.Point(20, 252);
            this.lblEnde.Name = "lblEnde";
            this.lblEnde.Size = new System.Drawing.Size(871, 28);
            this.lblEnde.TabIndex = 3;
            this.lblEnde.Text = "Die Konvertierung ist beendet";
            this.lblEnde.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEnde.Visible = false;
            // 
            // txtAnzeige
            // 
            this.txtAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnzeige.Location = new System.Drawing.Point(24, 12);
            this.txtAnzeige.Multiline = true;
            this.txtAnzeige.Name = "txtAnzeige";
            this.txtAnzeige.Size = new System.Drawing.Size(871, 131);
            this.txtAnzeige.TabIndex = 4;
            this.txtAnzeige.TabStop = false;
            this.txtAnzeige.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pgbProgress2
            // 
            this.pgbProgress2.Location = new System.Drawing.Point(24, 218);
            this.pgbProgress2.Name = "pgbProgress2";
            this.pgbProgress2.Size = new System.Drawing.Size(871, 23);
            this.pgbProgress2.Step = 1;
            this.pgbProgress2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbProgress2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(767, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmKonvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 289);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pgbProgress2);
            this.Controls.Add(this.txtAnzeige);
            this.Controls.Add(this.lblEnde);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.pgbProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmKonvert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convert from 3.0.1 to 3.0.2";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.ComponentModel.BackgroundWorker bgwVersion26;
    private System.Windows.Forms.ProgressBar pgbProgress;
    private System.Windows.Forms.Label lblText;
    private System.Windows.Forms.Label lblEnde;
    private System.Windows.Forms.TextBox txtAnzeige;
		private System.Windows.Forms.ProgressBar pgbProgress2;
        private System.Windows.Forms.Button button1;
    }
}