using System;

namespace Coinbook
{
	partial class frmFehllisten
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFehllisten));
            this.lblGebiet = new SAN.UI.Controls.LabelEx();
            this.lblÄra = new SAN.UI.Controls.LabelEx();
            this.lblNation = new SAN.UI.Controls.LabelEx();
            this.cboGebiet = new SAN.UI.Controls.ComboBoxEx();
            this.cboÄra = new SAN.UI.Controls.ComboBoxEx();
            this.cboNation = new SAN.UI.Controls.ComboBoxEx();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnPDF = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnCSV = new System.Windows.Forms.ToolStripButton();
            this.btnWork = new SAN.UI.Controls.ButtonEx();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.lblAnzeige = new SAN.UI.Controls.LabelEx();
            this.grdAnzeige = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.bgwCalculate = new SAN.UI.Controls.BackgroundWorkerEx();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAnzeige)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGebiet
            // 
            this.lblGebiet.AutoSize = true;
            this.lblGebiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGebiet.Location = new System.Drawing.Point(482, 0);
            this.lblGebiet.Name = "lblGebiet";
            this.lblGebiet.Size = new System.Drawing.Size(46, 25);
            this.lblGebiet.TabIndex = 8;
            this.lblGebiet.Text = "Gebiet";
            this.lblGebiet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblÄra
            // 
            this.lblÄra.AutoSize = true;
            this.lblÄra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblÄra.Location = new System.Drawing.Point(250, 0);
            this.lblÄra.Name = "lblÄra";
            this.lblÄra.Size = new System.Drawing.Size(27, 25);
            this.lblÄra.TabIndex = 7;
            this.lblÄra.Text = "Ära";
            this.lblÄra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNation
            // 
            this.lblNation.AutoSize = true;
            this.lblNation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNation.Location = new System.Drawing.Point(3, 0);
            this.lblNation.Name = "lblNation";
            this.lblNation.Size = new System.Drawing.Size(42, 25);
            this.lblNation.TabIndex = 6;
            this.lblNation.Text = "Nation";
            this.lblNation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboGebiet
            // 
            this.cboGebiet.BackColor = System.Drawing.Color.White;
            this.cboGebiet.Column = 0;
            this.cboGebiet.ColumnsToDisplay = "";
            this.cboGebiet.ColumnType = SAN.UI.Controls.ComboBoxEx.ColType.SingleColumn;
            this.cboGebiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGebiet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGebiet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboGebiet.FormattingEnabled = true;
            this.cboGebiet.GridLinesMultiColumn = false;
            this.cboGebiet.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboGebiet.ID = ((long)(-1));
            this.cboGebiet.IDObject = -1;
            this.cboGebiet.IDString = "";
            this.cboGebiet.IsPflichtfeld = false;
            this.cboGebiet.Location = new System.Drawing.Point(534, 3);
            this.cboGebiet.MaxDropDownItems = 10;
            this.cboGebiet.Name = "cboGebiet";
            this.cboGebiet.ReadOnly = false;
            this.cboGebiet.Row = 0;
            this.cboGebiet.ShowClipBoard = true;
            this.cboGebiet.Size = new System.Drawing.Size(253, 21);
            this.cboGebiet.TabIndex = 5;
            this.cboGebiet.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboGebiet.SelectedIndexChanged += new System.EventHandler(this.cboGebiet_SelectedIndexChanged);
            // 
            // cboÄra
            // 
            this.cboÄra.BackColor = System.Drawing.Color.White;
            this.cboÄra.Column = 0;
            this.cboÄra.ColumnsToDisplay = "";
            this.cboÄra.ColumnType = SAN.UI.Controls.ComboBoxEx.ColType.SingleColumn;
            this.cboÄra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboÄra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboÄra.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboÄra.FormattingEnabled = true;
            this.cboÄra.GridLinesMultiColumn = false;
            this.cboÄra.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboÄra.ID = ((long)(-1));
            this.cboÄra.IDObject = -1;
            this.cboÄra.IDString = "";
            this.cboÄra.IsPflichtfeld = false;
            this.cboÄra.Location = new System.Drawing.Point(283, 3);
            this.cboÄra.MaxDropDownItems = 10;
            this.cboÄra.Name = "cboÄra";
            this.cboÄra.ReadOnly = false;
            this.cboÄra.Row = 0;
            this.cboÄra.ShowClipBoard = true;
            this.cboÄra.Size = new System.Drawing.Size(193, 21);
            this.cboÄra.TabIndex = 4;
            this.cboÄra.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboÄra.SelectedIndexChanged += new System.EventHandler(this.cboÄra_SelectedIndexChanged);
            // 
            // cboNation
            // 
            this.cboNation.BackColor = System.Drawing.Color.White;
            this.cboNation.Column = 0;
            this.cboNation.ColumnsToDisplay = "";
            this.cboNation.ColumnType = SAN.UI.Controls.ComboBoxEx.ColType.SingleColumn;
            this.cboNation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboNation.FormattingEnabled = true;
            this.cboNation.GridLinesMultiColumn = false;
            this.cboNation.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboNation.ID = ((long)(-1));
            this.cboNation.IDObject = -1;
            this.cboNation.IDString = "";
            this.cboNation.IsPflichtfeld = false;
            this.cboNation.Location = new System.Drawing.Point(51, 3);
            this.cboNation.MaxDropDownItems = 10;
            this.cboNation.Name = "cboNation";
            this.cboNation.ReadOnly = false;
            this.cboNation.Row = 0;
            this.cboNation.ShowClipBoard = true;
            this.cboNation.Size = new System.Drawing.Size(193, 21);
            this.cboNation.TabIndex = 3;
            this.cboNation.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboNation.SelectedIndexChanged += new System.EventHandler(this.cboNation_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnPrint,
            this.btnPDF,
            this.btnExcel,
            this.btnCSV});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1180, 25);
            this.toolStrip1.TabIndex = 5;
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
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.Text = "Drucken";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnPDF.Image")));
            this.btnPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(23, 22);
            this.btnPDF.Text = "Export nach PDF";
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExcel.Text = "Export nach Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnCSV
            // 
            this.btnCSV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCSV.Image = ((System.Drawing.Image)(resources.GetObject("btnCSV.Image")));
            this.btnCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(23, 22);
            this.btnCSV.Text = "Export nach CSV";
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // btnWork
            // 
            this.btnWork.BackColor = System.Drawing.SystemColors.Control;
            this.btnWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWork.ForeColor = System.Drawing.Color.Black;
            this.btnWork.ImageStretch = false;
            this.btnWork.Location = new System.Drawing.Point(818, 3);
            this.btnWork.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(116, 22);
            this.btnWork.Status = SAN.UI.Controls.ButtonStatus.Nothing;
            this.btnWork.Stretched = false;
            this.btnWork.TabIndex = 9;
            this.btnWork.Text = "Anzeigen";
            this.btnWork.UseVisualStyleBackColor = false;
            this.btnWork.Click += new System.EventHandler(this.btnWork_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 259F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblAnzeige, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.grdAnzeige, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnWork, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNation, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboGebiet, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblGebiet, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboNation, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboÄra, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblÄra, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1180, 512);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // progressBar
            // 
            this.progressBar.BackgroundStyle = Syncfusion.Windows.Forms.Tools.ProgressBarBackgroundStyles.Office2016Colorful;
            this.progressBar.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBar.BackSegments = false;
            this.progressBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(149)))), ((int)(((byte)(152)))));
            this.progressBar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar, 6);
            this.progressBar.CustomText = null;
            this.progressBar.CustomWaitingRender = false;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.progressBar.ForegroundImage = null;
            this.progressBar.Location = new System.Drawing.Point(283, 490);
            this.progressBar.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBar.Name = "progressBar";
            this.progressBar.SegmentWidth = 1;
            this.progressBar.Size = new System.Drawing.Size(894, 19);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 18;
            this.progressBar.Text = "progressBar";
            this.progressBar.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
            this.progressBar.ThemeName = "Constant";
            this.progressBar.Value = 0;
            this.progressBar.WaitingGradientWidth = 400;
            // 
            // lblAnzeige
            // 
            this.lblAnzeige.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAnzeige, 3);
            this.lblAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnzeige.Location = new System.Drawing.Point(3, 487);
            this.lblAnzeige.Name = "lblAnzeige";
            this.lblAnzeige.Size = new System.Drawing.Size(274, 25);
            this.lblAnzeige.TabIndex = 17;
            this.lblAnzeige.Text = "labelEx2";
            this.lblAnzeige.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grdAnzeige
            // 
            this.grdAnzeige.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.grdAnzeige.BackColor = System.Drawing.SystemColors.Window;
            this.grdAnzeige.BrowseOnly = true;
            this.grdAnzeige.CacheRecordValues = false;
            this.grdAnzeige.ChildGroupOptions.ShowAddNewRecordBeforeDetails = false;
            this.grdAnzeige.ChildGroupOptions.ShowCaption = false;
            this.grdAnzeige.ChildGroupOptions.ShowSummaries = false;
            this.tableLayoutPanel1.SetColumnSpan(this.grdAnzeige, 10);
            this.grdAnzeige.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.grdAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAnzeige.Location = new System.Drawing.Point(3, 28);
            this.grdAnzeige.Name = "grdAnzeige";
            this.grdAnzeige.NestedTableGroupOptions.ShowAddNewRecordBeforeDetails = false;
            this.tableLayoutPanel1.SetRowSpan(this.grdAnzeige, 2);
            this.grdAnzeige.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.grdAnzeige.Size = new System.Drawing.Size(1174, 456);
            this.grdAnzeige.TabIndex = 10;
            this.grdAnzeige.TableDescriptor.AllowEdit = false;
            this.grdAnzeige.TableDescriptor.AllowNew = false;
            this.grdAnzeige.TableDescriptor.AllowRemove = false;
            this.grdAnzeige.TableDescriptor.ChildGroupOptions.ShowAddNewRecordBeforeDetails = false;
            this.grdAnzeige.TableOptions.ShowRowHeader = false;
            this.grdAnzeige.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            this.grdAnzeige.TopLevelGroupOptions.ShowCaption = false;
            this.grdAnzeige.TopLevelGroupOptions.ShowCaptionSummaryCells = true;
            this.grdAnzeige.TopLevelGroupOptions.ShowGroupFooter = true;
            this.grdAnzeige.UseRightToLeftCompatibleTextBox = true;
            this.grdAnzeige.VersionInfo = "14.4400.0.15";
            // 
            // bgwCalculate
            // 
            this.bgwCalculate.Max = 0;
            this.bgwCalculate.Tag = null;
            this.bgwCalculate.Text = null;
            this.bgwCalculate.WorkerReportsProgress = true;
            this.bgwCalculate.WorkerSupportsCancellation = true;
            this.bgwCalculate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCalculate_DoWork);
            this.bgwCalculate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCalculate_RunWorkerCompleted);
            // 
            // frmFehllisten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1180, 537);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFehllisten";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reportauswahl";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAnzeige)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

				private SAN.UI.Controls.LabelEx lblGebiet;
				private SAN.UI.Controls.LabelEx lblÄra;
				private SAN.UI.Controls.LabelEx lblNation;
				private SAN.UI.Controls.ComboBoxEx cboGebiet;
				private SAN.UI.Controls.ComboBoxEx cboÄra;
				private SAN.UI.Controls.ComboBoxEx cboNation;
				private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClose;
				private System.Windows.Forms.ToolStripButton btnPrint;
				private System.Windows.Forms.ToolStripButton btnPDF;
        private System.Windows.Forms.ToolStripButton btnExcel;
				private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.ToolStripButton btnCSV;
        private SAN.UI.Controls.ButtonEx btnWork;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SAN.UI.Controls.BackgroundWorkerEx bgwCalculate;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl grdAnzeige;
        private SAN.UI.Controls.LabelEx lblAnzeige;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBar;
    }
}