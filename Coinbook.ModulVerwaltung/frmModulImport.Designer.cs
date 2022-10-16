using System;
namespace Coinbook.Modulverwaltung
{
    partial class frmModulImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModulImport));
            this.bNav = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.bgwImport = new SAN.Control.BackgroundWorkerEx();
            this.progressBar = new SAN.Control.ProgressBarEx();
            this.bgwWebDownload = new System.ComponentModel.BackgroundWorker();
            this.groupBoxEx1 = new SAN.Control.GroupBoxEx();
            this.optDVD = new System.Windows.Forms.RadioButton();
            this.optInternet = new System.Windows.Forms.RadioButton();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCheck = new SAN.Control.ButtonEx();
            this.txtAnzeige = new System.Windows.Forms.Label();
            this.lblGesamt = new System.Windows.Forms.Label();
            this.pgbBar = new SAN.Control.ProgressBarEx();
            this.btnClearDownloads = new SAN.Control.ButtonEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnResetSingleDownload = new SAN.Control.ButtonEx();
            this.grdModule = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Bezeichnung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblAnzeige = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bNav)).BeginInit();
            this.bNav.SuspendLayout();
            this.groupBoxEx1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdModule)).BeginInit();
            this.SuspendLayout();
            // 
            // bNav
            // 
            this.bNav.AddNewItem = null;
            this.bNav.BackColor = System.Drawing.SystemColors.Control;
            this.bNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bNav.CountItem = null;
            this.bNav.DeleteItem = null;
            this.bNav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose});
            this.bNav.Location = new System.Drawing.Point(0, 0);
            this.bNav.MoveFirstItem = null;
            this.bNav.MoveLastItem = null;
            this.bNav.MoveNextItem = null;
            this.bNav.MovePreviousItem = null;
            this.bNav.Name = "bNav";
            this.bNav.Padding = new System.Windows.Forms.Padding(0);
            this.bNav.PositionItem = null;
            this.bNav.Size = new System.Drawing.Size(665, 25);
            this.bNav.Stretch = true;
            this.bNav.TabIndex = 3;
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
            // bgwImport
            // 
            this.bgwImport.Max = 0;
            this.bgwImport.Tag = null;
            this.bgwImport.Text = null;
            this.bgwImport.WorkerReportsProgress = true;
            this.bgwImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwImport_DoWork);
            this.bgwImport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwImport_ProgressChanged);
            this.bgwImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwImport_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.BarColor = System.Drawing.Color.Aqua;
            this.progressBar.BlockDistance = ((byte)(0));
            this.progressBar.BlockWidth = ((byte)(1));
            this.progressBar.BorderColor = System.Drawing.Color.Aqua;
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar, 5);
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBar.Location = new System.Drawing.Point(5, 135);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.progressBar.MaxValue = 100;
            this.progressBar.MinValue = 0;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(647, 30);
            this.progressBar.TabIndex = 9;
            this.progressBar.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressBar.Value = 0;
            // 
            // bgwWebDownload
            // 
            this.bgwWebDownload.WorkerReportsProgress = true;
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.BackgroundGradientColor = System.Drawing.Color.White;
            this.groupBoxEx1.BackgroundGradientMode = SAN.Control.GroupBoxEx.GroupBoxGradientMode.None;
            this.groupBoxEx1.BorderColor = System.Drawing.Color.Black;
            this.groupBoxEx1.BorderThickness = 1F;
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxEx1, 2);
            this.groupBoxEx1.Controls.Add(this.optDVD);
            this.groupBoxEx1.Controls.Add(this.optInternet);
            this.groupBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEx1.GroupBoxBackColor = System.Drawing.Color.White;
            this.groupBoxEx1.GroupImage = null;
            this.groupBoxEx1.GroupTitle = "";
            this.groupBoxEx1.Location = new System.Drawing.Point(3, 3);
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.Padding = new System.Windows.Forms.Padding(20);
            this.groupBoxEx1.PaintGroupBox = false;
            this.groupBoxEx1.RoundCorners = 10;
            this.groupBoxEx1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBoxEx1.ShadowControl = false;
            this.groupBoxEx1.ShadowThickness = 3;
            this.groupBoxEx1.Size = new System.Drawing.Size(192, 69);
            this.groupBoxEx1.TabIndex = 10;
            // 
            // optDVD
            // 
            this.optDVD.AutoSize = true;
            this.optDVD.Location = new System.Drawing.Point(14, 35);
            this.optDVD.Name = "optDVD";
            this.optDVD.Size = new System.Drawing.Size(156, 17);
            this.optDVD.TabIndex = 1;
            this.optDVD.TabStop = true;
            this.optDVD.Text = "Module von CD/DVD laden";
            this.optDVD.UseVisualStyleBackColor = true;
            // 
            // optInternet
            // 
            this.optInternet.AutoSize = true;
            this.optInternet.Checked = true;
            this.optInternet.Location = new System.Drawing.Point(14, 12);
            this.optInternet.Name = "optInternet";
            this.optInternet.Size = new System.Drawing.Size(171, 17);
            this.optInternet.TabIndex = 0;
            this.optInternet.TabStop = true;
            this.optInternet.Text = "Module aus dem Internet laden";
            this.optInternet.UseVisualStyleBackColor = true;
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.SetColumnSpan(this.btnCheck, 3);
            this.btnCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheck.ForeColor = System.Drawing.Color.Black;
            this.btnCheck.ImageStretch = false;
            this.btnCheck.Location = new System.Drawing.Point(3, 78);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(296, 24);
            this.btnCheck.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnCheck.Stretched = false;
            this.btnCheck.TabIndex = 11;
            this.btnCheck.Text = "Importiere neue Module bzw. Modul-Updates";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtAnzeige
            // 
            this.txtAnzeige.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.txtAnzeige, 4);
            this.txtAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnzeige.Location = new System.Drawing.Point(68, 165);
            this.txtAnzeige.Name = "txtAnzeige";
            this.txtAnzeige.Size = new System.Drawing.Size(584, 30);
            this.txtAnzeige.TabIndex = 12;
            this.txtAnzeige.Text = " ";
            this.txtAnzeige.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGesamt
            // 
            this.lblGesamt.AutoSize = true;
            this.lblGesamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGesamt.Location = new System.Drawing.Point(3, 105);
            this.lblGesamt.Name = "lblGesamt";
            this.lblGesamt.Size = new System.Drawing.Size(59, 30);
            this.lblGesamt.TabIndex = 13;
            this.lblGesamt.Text = "Gesamt";
            this.lblGesamt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgbBar
            // 
            this.pgbBar.BarColor = System.Drawing.Color.Aqua;
            this.pgbBar.BlockDistance = ((byte)(0));
            this.pgbBar.BlockWidth = ((byte)(1));
            this.pgbBar.BorderColor = System.Drawing.Color.Aqua;
            this.tableLayoutPanel1.SetColumnSpan(this.pgbBar, 5);
            this.pgbBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgbBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pgbBar.Location = new System.Drawing.Point(5, 195);
            this.pgbBar.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.pgbBar.MaxValue = 100;
            this.pgbBar.MinValue = 0;
            this.pgbBar.Name = "pgbBar";
            this.pgbBar.Size = new System.Drawing.Size(647, 30);
            this.pgbBar.TabIndex = 14;
            this.pgbBar.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pgbBar.Value = 0;
            // 
            // btnClearDownloads
            // 
            this.btnClearDownloads.BackColor = System.Drawing.SystemColors.Control;
            this.btnClearDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearDownloads.ForeColor = System.Drawing.Color.Black;
            this.btnClearDownloads.ImageStretch = false;
            this.btnClearDownloads.Location = new System.Drawing.Point(305, 78);
            this.btnClearDownloads.Name = "btnClearDownloads";
            this.btnClearDownloads.Size = new System.Drawing.Size(189, 24);
            this.btnClearDownloads.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnClearDownloads.Stretched = false;
            this.btnClearDownloads.TabIndex = 15;
            this.btnClearDownloads.Text = "Alle Downloads resetten";
            this.toolTip1.SetToolTip(this.btnClearDownloads, "Kennzeichen für heruntergeladene Module werden zurückgesetzt.");
            this.btnClearDownloads.UseVisualStyleBackColor = false;
            this.btnClearDownloads.Click += new System.EventHandler(this.btnClearDownloads_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FloralWhite;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 195F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.pgbBar, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtAnzeige, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblGesamt, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCheck, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnClearDownloads, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnResetSingleDownload, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.grdModule, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblAnzeige, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 4, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(665, 495);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightYellow;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(201, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(451, 75);
            this.label1.TabIndex = 16;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnResetSingleDownload
            // 
            this.btnResetSingleDownload.BackColor = System.Drawing.SystemColors.Control;
            this.btnResetSingleDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResetSingleDownload.ForeColor = System.Drawing.Color.Black;
            this.btnResetSingleDownload.ImageStretch = false;
            this.btnResetSingleDownload.Location = new System.Drawing.Point(500, 78);
            this.btnResetSingleDownload.Name = "btnResetSingleDownload";
            this.btnResetSingleDownload.Size = new System.Drawing.Size(152, 24);
            this.btnResetSingleDownload.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnResetSingleDownload.Stretched = false;
            this.btnResetSingleDownload.TabIndex = 17;
            this.btnResetSingleDownload.Text = "Einzelne Downloads resetten";
            this.btnResetSingleDownload.UseVisualStyleBackColor = false;
            this.btnResetSingleDownload.Click += new System.EventHandler(this.btnResetSingleDownload_Click);
            // 
            // grdModule
            // 
            this.grdModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdModule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.Bezeichnung,
            this.ID,
            this.Key});
            this.tableLayoutPanel1.SetColumnSpan(this.grdModule, 2);
            this.grdModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdModule.Location = new System.Drawing.Point(68, 248);
            this.grdModule.Name = "grdModule";
            this.grdModule.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.grdModule, 3);
            this.grdModule.Size = new System.Drawing.Size(231, 244);
            this.grdModule.TabIndex = 18;
            this.grdModule.Visible = false;
            // 
            // Check
            // 
            this.Check.DataPropertyName = "Check";
            this.Check.HeaderText = "";
            this.Check.Name = "Check";
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Check.Width = 30;
            // 
            // Bezeichnung
            // 
            this.Bezeichnung.DataPropertyName = "Bezeichnung";
            this.Bezeichnung.HeaderText = "Bezeichnung";
            this.Bezeichnung.Name = "Bezeichnung";
            this.Bezeichnung.ReadOnly = true;
            this.Bezeichnung.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Bezeichnung.Width = 190;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Key
            // 
            this.Key.DataPropertyName = "Key";
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            this.Key.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipTitle = "Modulimport  resetten";
            // 
            // lblAnzeige
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lblAnzeige, 2);
            this.lblAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnzeige.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnzeige.Location = new System.Drawing.Point(305, 245);
            this.lblAnzeige.Name = "lblAnzeige";
            this.lblAnzeige.Size = new System.Drawing.Size(347, 54);
            this.lblAnzeige.TabIndex = 19;
            this.lblAnzeige.Text = "Alle Module wurden schon heruntergeladen";
            this.lblAnzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAnzeige.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(500, 302);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(152, 34);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            // 
            // frmModulImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(665, 520);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.bNav);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmModulImport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modulimport";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.bNav)).EndInit();
            this.bNav.ResumeLayout(false);
            this.bNav.PerformLayout();
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdModule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator bNav;
        private System.Windows.Forms.ToolStripButton btnClose;
        private SAN.Control.BackgroundWorkerEx bgwImport;
        private SAN.Control.ProgressBarEx progressBar;
        private System.ComponentModel.BackgroundWorker bgwWebDownload;
        private SAN.Control.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.RadioButton optDVD;
        private System.Windows.Forms.RadioButton optInternet;
        private System.Windows.Forms.FolderBrowserDialog dlgFolder;
        private SAN.Control.ButtonEx btnCheck;
        private System.Windows.Forms.Label txtAnzeige;
        private System.Windows.Forms.Label lblGesamt;
        private SAN.Control.ProgressBarEx pgbBar;
        private SAN.Control.ButtonEx btnClearDownloads;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private SAN.Control.ButtonEx btnResetSingleDownload;
        private System.Windows.Forms.DataGridView grdModule;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bezeichnung;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.Label lblAnzeige;
        private System.Windows.Forms.Button btnOK;
    }
}