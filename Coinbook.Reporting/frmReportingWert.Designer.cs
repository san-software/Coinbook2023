using System;
namespace Coinbook
{
	partial class frmReportingWert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportingWert));
            this.labelEx1 = new SAN.UI.Controls.LabelEx();
            this.cboNationen = new SAN.UI.Controls.ComboBoxEx();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnPDF = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnCSV = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.bgwCalculate = new SAN.UI.Controls.BackgroundWorkerEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdAnzeige = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.progressBar = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.lblAnzeige = new SAN.UI.Controls.LabelEx();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAnzeige)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar)).BeginInit();
            this.SuspendLayout();
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEx1.Location = new System.Drawing.Point(3, 0);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(94, 30);
            this.labelEx1.TabIndex = 6;
            this.labelEx1.Text = "Nation";
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboNationen
            // 
            this.cboNationen.BackColor = System.Drawing.Color.White;
            this.cboNationen.Column = 0;
            this.cboNationen.ColumnsToDisplay = "";
            this.cboNationen.ColumnType = SAN.UI.Controls.ComboBoxEx.ColType.SingleColumn;
            this.cboNationen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNationen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNationen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboNationen.FormattingEnabled = true;
            this.cboNationen.GridLinesMultiColumn = false;
            this.cboNationen.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboNationen.ID = ((long)(-1));
            this.cboNationen.IDObject = -1;
            this.cboNationen.IDString = "";
            this.cboNationen.IsPflichtfeld = false;
            this.cboNationen.Location = new System.Drawing.Point(103, 3);
            this.cboNationen.MaxDropDownItems = 10;
            this.cboNationen.Name = "cboNationen";
            this.cboNationen.ReadOnly = false;
            this.cboNationen.Row = 0;
            this.cboNationen.ShowClipBoard = true;
            this.cboNationen.Size = new System.Drawing.Size(197, 21);
            this.cboNationen.TabIndex = 3;
            this.cboNationen.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboNationen.SelectedIndexChanged += new System.EventHandler(this.cboNation_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnPrint,
            this.btnPDF,
            this.btnExcel,
            this.btnCSV,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(888, 25);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 677F));
            this.tableLayoutPanel1.Controls.Add(this.grdAnzeige, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblAnzeige, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboNationen, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelEx1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(888, 554);
            this.tableLayoutPanel1.TabIndex = 9;
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
            this.tableLayoutPanel1.SetColumnSpan(this.grdAnzeige, 3);
            this.grdAnzeige.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.grdAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAnzeige.Location = new System.Drawing.Point(3, 33);
            this.grdAnzeige.Name = "grdAnzeige";
            this.grdAnzeige.NestedTableGroupOptions.ShowAddNewRecordBeforeDetails = false;
            this.tableLayoutPanel1.SetRowSpan(this.grdAnzeige, 2);
            this.grdAnzeige.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.grdAnzeige.Size = new System.Drawing.Size(974, 487);
            this.grdAnzeige.TabIndex = 25;
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
            this.grdAnzeige.QueryCellStyleInfo += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableCellStyleInfoEventHandler(this.grdAnzeige_QueryCellStyleInfo);
            // 
            // progressBar
            // 
            this.progressBar.BackgroundStyle = Syncfusion.Windows.Forms.Tools.ProgressBarBackgroundStyles.Office2016Colorful;
            this.progressBar.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBar.BackSegments = false;
            this.progressBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(149)))), ((int)(((byte)(152)))));
            this.progressBar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.progressBar.CustomText = null;
            this.progressBar.CustomWaitingRender = false;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.progressBar.ForegroundImage = null;
            this.progressBar.Location = new System.Drawing.Point(306, 526);
            this.progressBar.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBar.Name = "progressBar";
            this.progressBar.SegmentWidth = 1;
            this.progressBar.Size = new System.Drawing.Size(671, 25);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 24;
            this.progressBar.Text = "progressBar";
            this.progressBar.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
            this.progressBar.ThemeName = "Constant";
            this.progressBar.Value = 0;
            this.progressBar.WaitingGradientWidth = 400;
            // 
            // lblAnzeige
            // 
            this.lblAnzeige.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAnzeige, 2);
            this.lblAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnzeige.Location = new System.Drawing.Point(3, 523);
            this.lblAnzeige.Name = "lblAnzeige";
            this.lblAnzeige.Size = new System.Drawing.Size(297, 31);
            this.lblAnzeige.TabIndex = 23;
            this.lblAnzeige.Text = "labelEx2";
            this.lblAnzeige.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmReportingWert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(888, 579);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmReportingWert";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reportauswahl";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAnzeige)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

				private SAN.UI.Controls.LabelEx labelEx1;
				private SAN.UI.Controls.ComboBoxEx cboNationen;
				private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClose;
				private System.Windows.Forms.ToolStripButton btnPrint;
				private System.Windows.Forms.ToolStripButton btnPDF;
        private System.Windows.Forms.ToolStripButton btnExcel;
				private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.ToolStripButton btnCSV;
				private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private SAN.UI.Controls.BackgroundWorkerEx bgwCalculate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SAN.UI.Controls.LabelEx lblAnzeige;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBar;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl grdAnzeige;
    }
}