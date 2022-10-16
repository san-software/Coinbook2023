
namespace SAN.Logging
{
    partial class frmLogSettings
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAll = new System.Windows.Forms.Label();
            this.lblDebug = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.chkInfo = new System.Windows.Forms.CheckBox();
            this.chkWarning = new System.Windows.Forms.CheckBox();
            this.txtLogfile = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.Controls.Add(this.lblAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDebug, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblInfo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblWarning, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblFile, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.chkAll, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkDebug, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkInfo, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkWarning, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtLogfile, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 158);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblAll
            // 
            this.lblAll.AutoSize = true;
            this.lblAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAll.Location = new System.Drawing.Point(3, 0);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(64, 25);
            this.lblAll.TabIndex = 0;
            this.lblAll.Text = "All";
            this.lblAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDebug.Location = new System.Drawing.Point(3, 25);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(64, 25);
            this.lblDebug.TabIndex = 1;
            this.lblDebug.Text = "Debug";
            this.lblDebug.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Location = new System.Drawing.Point(3, 50);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(64, 25);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Info";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWarning.Location = new System.Drawing.Point(3, 75);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(64, 25);
            this.lblWarning.TabIndex = 3;
            this.lblWarning.Text = "Warning";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFile.Location = new System.Drawing.Point(3, 125);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(64, 25);
            this.lblFile.TabIndex = 5;
            this.lblFile.Text = "Logfile";
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAll.Location = new System.Drawing.Point(73, 7);
            this.chkAll.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(14, 15);
            this.chkAll.TabIndex = 6;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDebug.Location = new System.Drawing.Point(73, 32);
            this.chkDebug.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(14, 15);
            this.chkDebug.TabIndex = 7;
            this.chkDebug.UseVisualStyleBackColor = true;
            this.chkDebug.CheckedChanged += new System.EventHandler(this.chkDebug_CheckedChanged);
            // 
            // chkInfo
            // 
            this.chkInfo.AutoSize = true;
            this.chkInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkInfo.Location = new System.Drawing.Point(73, 57);
            this.chkInfo.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.chkInfo.Name = "chkInfo";
            this.chkInfo.Size = new System.Drawing.Size(14, 15);
            this.chkInfo.TabIndex = 8;
            this.chkInfo.UseVisualStyleBackColor = true;
            this.chkInfo.CheckedChanged += new System.EventHandler(this.chkInfo_CheckedChanged);
            // 
            // chkWarning
            // 
            this.chkWarning.AutoSize = true;
            this.chkWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkWarning.Location = new System.Drawing.Point(73, 82);
            this.chkWarning.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.chkWarning.Name = "chkWarning";
            this.chkWarning.Size = new System.Drawing.Size(14, 15);
            this.chkWarning.TabIndex = 9;
            this.chkWarning.UseVisualStyleBackColor = true;
            this.chkWarning.CheckedChanged += new System.EventHandler(this.chkWarning_CheckedChanged);
            // 
            // txtLogfile
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtLogfile, 2);
            this.txtLogfile.Location = new System.Drawing.Point(73, 128);
            this.txtLogfile.Name = "txtLogfile";
            this.txtLogfile.Size = new System.Drawing.Size(244, 20);
            this.txtLogfile.TabIndex = 10;
            this.txtLogfile.TextChanged += new System.EventHandler(this.txtLogfile_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(323, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 19);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(323, 28);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 19);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Abbruch";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmLogSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 158);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLogSettings";
            this.Text = "frmLogSettings";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblAll;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.CheckBox chkInfo;
        private System.Windows.Forms.CheckBox chkWarning;
        private System.Windows.Forms.TextBox txtLogfile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}