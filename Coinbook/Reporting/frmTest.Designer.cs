using System;
namespace Coinbook
{
    partial class frmTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest));
            this.labelEx1 = new System.Windows.Forms.Label();
            this.cboNationen = new SAN.Control.ComboBoxEx();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnPDF = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnCSV = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdAnzeige = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.bgwCalculate = new SAN.Control.BackgroundWorkerEx();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAnzeige)).BeginInit();
            this.SuspendLayout();
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new System.Drawing.Point(9, 6);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(38, 13);
            this.labelEx1.TabIndex = 6;
            this.labelEx1.Text = "Nation";
            // 
            // cboNationen
            // 
            this.cboNationen.BackColor = System.Drawing.Color.White;
            this.cboNationen.Column = 0;
            this.cboNationen.ColumnsToDisplay = "";
            this.cboNationen.ColumnType = SAN.Control.ComboBoxEx.ColType.SingleColumn;
            this.cboNationen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNationen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboNationen.FormattingEnabled = true;
            this.cboNationen.GridLinesMultiColumn = false;
            this.cboNationen.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboNationen.ID = ((long)(-1));
            this.cboNationen.IDObject = -1;
            this.cboNationen.IDString = "";
            this.cboNationen.IsPflichtfeld = false;
            this.cboNationen.Location = new System.Drawing.Point(53, 3);
            this.cboNationen.MaxDropDownItems = 10;
            this.cboNationen.Name = "cboNationen";
            this.cboNationen.ReadOnly = false;
            this.cboNationen.Row = 0;
            this.cboNationen.ShowClipBoard = true;
            this.cboNationen.Size = new System.Drawing.Size(191, 21);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cboNationen);
            this.splitContainer1.Panel1.Controls.Add(this.labelEx1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdAnzeige);
            this.splitContainer1.Size = new System.Drawing.Size(1180, 512);
            this.splitContainer1.SplitterDistance = 27;
            this.splitContainer1.TabIndex = 8;
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
            this.grdAnzeige.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.grdAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAnzeige.Location = new System.Drawing.Point(0, 0);
            this.grdAnzeige.Name = "grdAnzeige";
            this.grdAnzeige.NestedTableGroupOptions.ShowAddNewRecordBeforeDetails = false;
            this.grdAnzeige.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.grdAnzeige.Size = new System.Drawing.Size(1180, 481);
            this.grdAnzeige.TabIndex = 8;
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
            // bgwCalculate
            // 
            this.bgwCalculate.Max = 0;
            this.bgwCalculate.Tag = null;
            this.bgwCalculate.Text = null;
            this.bgwCalculate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCalculate_DoWork);
            this.bgwCalculate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCalculate_RunWorkerCompleted);
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1180, 537);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTest";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reportauswahl";
            this.Shown += new System.EventHandler(this.frmReporting_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAnzeige)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
				private System.Windows.Forms.Label labelEx1;
				private SAN.Control.ComboBoxEx cboNationen;
				private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClose;
				private System.Windows.Forms.ToolStripButton btnPrint;
				private System.Windows.Forms.ToolStripButton btnPDF;
        private System.Windows.Forms.ToolStripButton btnExcel;
				private System.Windows.Forms.SplitContainer splitContainer1;
				private System.Windows.Forms.SaveFileDialog dlgSave;
				private System.Windows.Forms.ToolStripButton btnCSV;
				private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
				private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl grdAnzeige;
        private SAN.Control.BackgroundWorkerEx bgwCalculate;
    }
}