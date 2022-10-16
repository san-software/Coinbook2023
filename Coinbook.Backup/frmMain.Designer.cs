namespace Coinbook.Backup
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBackup1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloud = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloudBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloudRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuBackup1,
            this.mnuCloud});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClose});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(46, 20);
            this.mnuFile.Text = "Datei";
            // 
            // mnuClose
            // 
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(180, 22);
            this.mnuClose.Text = "Beenden";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // mnuBackup1
            // 
            this.mnuBackup1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBackup,
            this.mnuRestore});
            this.mnuBackup1.Name = "mnuBackup1";
            this.mnuBackup1.Size = new System.Drawing.Size(102, 20);
            this.mnuBackup1.Text = "Datensicherung";
            // 
            // mnuCloud
            // 
            this.mnuCloud.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.mnuCloudBackup,
            this.mnuCloudRestore});
            this.mnuCloud.Name = "mnuCloud";
            this.mnuCloud.Size = new System.Drawing.Size(90, 20);
            this.mnuCloud.Text = "CloudBackup";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(218, 22);
            this.mnuAbout.Text = "Über CloudBackup";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuCloudBackup
            // 
            this.mnuCloudBackup.Name = "mnuCloudBackup";
            this.mnuCloudBackup.Size = new System.Drawing.Size(218, 22);
            this.mnuCloudBackup.Text = "In Cloud sichern";
            this.mnuCloudBackup.Click += new System.EventHandler(this.mnuCloudBackup_Click);
            // 
            // mnuCloudRestore
            // 
            this.mnuCloudRestore.Name = "mnuCloudRestore";
            this.mnuCloudRestore.Size = new System.Drawing.Size(218, 22);
            this.mnuCloudRestore.Text = "Aus Cloud wiederherstellen";
            this.mnuCloudRestore.Click += new System.EventHandler(this.mnuCloudRestore_Click);
            // 
            // mnuBackup
            // 
            this.mnuBackup.Name = "mnuBackup";
            this.mnuBackup.Size = new System.Drawing.Size(196, 22);
            this.mnuBackup.Text = "Datensicherung";
            this.mnuBackup.Click += new System.EventHandler(this.mnuBackup_Click);
            // 
            // mnuRestore
            // 
            this.mnuRestore.Name = "mnuRestore";
            this.mnuRestore.Size = new System.Drawing.Size(196, 22);
            this.mnuRestore.Text = "Daten Wiederherstellen";
            this.mnuRestore.Click += new System.EventHandler(this.mnuRestore_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datensicherung";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem mnuBackup1;
        private System.Windows.Forms.ToolStripMenuItem mnuCloud;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuCloudBackup;
        private System.Windows.Forms.ToolStripMenuItem mnuCloudRestore;
        private System.Windows.Forms.ToolStripMenuItem mnuBackup;
        private System.Windows.Forms.ToolStripMenuItem mnuRestore;
    }
}