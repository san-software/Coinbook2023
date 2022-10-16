namespace Coinbook
{
    partial class usrReport
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
            this.lblAusg = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.grPbExportWahl = new System.Windows.Forms.GroupBox();
            this.optCsv = new System.Windows.Forms.RadioButton();
            this.optHtml = new System.Windows.Forms.RadioButton();
            this.optPrint = new System.Windows.Forms.RadioButton();
            this.optLetzBen = new System.Windows.Forms.RadioButton();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.grPbExportWahl.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAusg
            // 
            this.lblAusg.AutoSize = true;
            this.lblAusg.Location = new System.Drawing.Point(6, 12);
            this.lblAusg.Name = "lblAusg";
            this.lblAusg.Size = new System.Drawing.Size(107, 13);
            this.lblAusg.TabIndex = 0;
            this.lblAusg.Text = "Ausgabeverzeichniss";
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.Location = new System.Drawing.Point(9, 28);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(317, 20);
            this.txtPath.TabIndex = 1;
            // 
            // btnPath
            // 
            this.btnPath.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPath.Location = new System.Drawing.Point(332, 28);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(30, 21);
            this.btnPath.TabIndex = 9;
            this.btnPath.Text = "...";
            this.btnPath.UseVisualStyleBackColor = false;
            this.btnPath.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // grPbExportWahl
            // 
            this.grPbExportWahl.Controls.Add(this.optCsv);
            this.grPbExportWahl.Controls.Add(this.optHtml);
            this.grPbExportWahl.Controls.Add(this.optPrint);
            this.grPbExportWahl.Controls.Add(this.optLetzBen);
            this.grPbExportWahl.Location = new System.Drawing.Point(9, 54);
            this.grPbExportWahl.Name = "grPbExportWahl";
            this.grPbExportWahl.Size = new System.Drawing.Size(358, 70);
            this.grPbExportWahl.TabIndex = 10;
            this.grPbExportWahl.TabStop = false;
            this.grPbExportWahl.Text = "Wie wird exportiert";
            // 
            // optCsv
            // 
            this.optCsv.AutoSize = true;
            this.optCsv.BackColor = System.Drawing.Color.Transparent;
            this.optCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optCsv.Location = new System.Drawing.Point(182, 42);
            this.optCsv.Name = "optCsv";
            this.optCsv.Size = new System.Drawing.Size(89, 17);
            this.optCsv.TabIndex = 11;
            this.optCsv.TabStop = true;
            this.optCsv.Text = "CSV für Excel";
            this.optCsv.UseVisualStyleBackColor = false;
            this.optCsv.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // optHtml
            // 
            this.optHtml.AutoSize = true;
            this.optHtml.BackColor = System.Drawing.Color.Transparent;
            this.optHtml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optHtml.Location = new System.Drawing.Point(16, 42);
            this.optHtml.Name = "optHtml";
            this.optHtml.Size = new System.Drawing.Size(108, 17);
            this.optHtml.TabIndex = 10;
            this.optHtml.TabStop = true;
            this.optHtml.Text = "Webseite (HTML)";
            this.optHtml.UseVisualStyleBackColor = false;
            this.optHtml.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // optPrint
            // 
            this.optPrint.AutoSize = true;
            this.optPrint.BackColor = System.Drawing.Color.Transparent;
            this.optPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optPrint.Location = new System.Drawing.Point(182, 19);
            this.optPrint.Name = "optPrint";
            this.optPrint.Size = new System.Drawing.Size(53, 17);
            this.optPrint.TabIndex = 9;
            this.optPrint.TabStop = true;
            this.optPrint.Text = "Druck";
            this.optPrint.UseVisualStyleBackColor = false;
            this.optPrint.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // optLetzBen
            // 
            this.optLetzBen.AutoSize = true;
            this.optLetzBen.BackColor = System.Drawing.Color.Transparent;
            this.optLetzBen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optLetzBen.Location = new System.Drawing.Point(16, 19);
            this.optLetzBen.Name = "optLetzBen";
            this.optLetzBen.Size = new System.Drawing.Size(97, 17);
            this.optLetzBen.TabIndex = 8;
            this.optLetzBen.TabStop = true;
            this.optLetzBen.Text = "Zuletzt benutze";
            this.optLetzBen.UseVisualStyleBackColor = false;
            this.optLetzBen.CheckedChanged += new System.EventHandler(this.optLetzBen_CheckedChanged);
            // 
            // usrReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.grPbExportWahl);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblAusg);
            this.Name = "usrReport";
            this.Size = new System.Drawing.Size(378, 379);
            this.grPbExportWahl.ResumeLayout(false);
            this.grPbExportWahl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAusg;
				private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.GroupBox grPbExportWahl;
        private System.Windows.Forms.RadioButton optCsv;
        private System.Windows.Forms.RadioButton optHtml;
        private System.Windows.Forms.RadioButton optPrint;
        private System.Windows.Forms.RadioButton optLetzBen;
        private System.Windows.Forms.FolderBrowserDialog dlgFolder;

    }
}
