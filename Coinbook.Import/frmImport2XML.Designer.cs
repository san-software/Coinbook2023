using System;

namespace Coinbook.Import
{
    partial class frmImport2XML
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport2XML));
            this.lblStd1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.pgbProgress = new System.Windows.Forms.ProgressBar();
            this.lblAnzeige = new System.Windows.Forms.Label();
            this.bgwWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStd1
            // 
            this.lblStd1.AutoSize = true;
            this.lblStd1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStd1.Location = new System.Drawing.Point(12, 35);
            this.lblStd1.Name = "lblStd1";
            this.lblStd1.Size = new System.Drawing.Size(263, 26);
            this.lblStd1.TabIndex = 0;
            this.lblStd1.Text = "Über diese Funktion können Sie ihre Sammlungsdaten\r\naus Coinbook 2.x  importieren" +
    ".";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(15, 77);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(263, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Coinbook 2.x Intallationsordner suchen";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.Gainsboro;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Location = new System.Drawing.Point(80, 106);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(130, 23);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Importieren";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.butImport_Click);
            // 
            // pgbProgress
            // 
            this.pgbProgress.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pgbProgress.Location = new System.Drawing.Point(12, 148);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(266, 23);
            this.pgbProgress.TabIndex = 4;
            this.pgbProgress.Visible = false;
            // 
            // lblAnzeige
            // 
            this.lblAnzeige.Location = new System.Drawing.Point(12, 174);
            this.lblAnzeige.Name = "lblAnzeige";
            this.lblAnzeige.Size = new System.Drawing.Size(266, 13);
            this.lblAnzeige.TabIndex = 5;
            this.lblAnzeige.Visible = false;
            // 
            // bgwWorker
            // 
            this.bgwWorker.WorkerReportsProgress = true;
            this.bgwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWorker_DoWork);
            this.bgwWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwWorker_ProgressChanged);
            this.bgwWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWorker_RunWorkerCompleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(292, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "Schliessen";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmImport2XML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(292, 213);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lblAnzeige);
            this.Controls.Add(this.pgbProgress);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblStd1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmImport2XML";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import von Coinbook  2.x per XML";
            this.TopMost = true;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStd1;
        private System.Windows.Forms.Button btnSearch;
				private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.FolderBrowserDialog dlgFolder;
				private System.Windows.Forms.ProgressBar pgbProgress;
				private System.Windows.Forms.Label lblAnzeige;
        private System.ComponentModel.BackgroundWorker bgwWorker;
				private System.Windows.Forms.ToolStrip toolStrip1;
				private System.Windows.Forms.ToolStripButton btnClose;
    }
}