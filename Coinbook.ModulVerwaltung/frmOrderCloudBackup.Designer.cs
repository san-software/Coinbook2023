
namespace Coinbook.Modulverwaltung
{
    partial class frmOrderCloudBackup
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
            Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings messageBoxSettings1 = new Syncfusion.Windows.Forms.PdfViewer.MessageBoxSettings();
            Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings1 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderCloudBackup));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings1 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdOrder = new Syncfusion.Windows.Forms.Grid.GridControl();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAbo1 = new System.Windows.Forms.Label();
            this.lblAbo2 = new System.Windows.Forms.Label();
            this.lblAbo3 = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.txtBank = new System.Windows.Forms.TextBox();
            this.txtBIC = new System.Windows.Forms.TextBox();
            this.txtIBAN = new System.Windows.Forms.TextBox();
            this.lblBIC = new System.Windows.Forms.Label();
            this.lblIBAN = new System.Windows.Forms.Label();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pdfViewer = new Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel1.Controls.Add(this.grdOrder, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAbo1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAbo2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblAbo3, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblBank, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtBank, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtBIC, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtIBAN, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblBIC, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblIBAN, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.btnOrder, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.pdfViewer, 2, 10);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(976, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdOrder
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.grdOrder, 2);
            this.grdOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOrder.Location = new System.Drawing.Point(380, 3);
            this.grdOrder.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.grdOrder.Name = "grdOrder";
            this.tableLayoutPanel1.SetRowSpan(this.grdOrder, 13);
            this.grdOrder.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeIntoCode;
            this.grdOrder.Size = new System.Drawing.Size(586, 444);
            this.grdOrder.SmartSizeBox = false;
            this.grdOrder.TabIndex = 12;
            this.grdOrder.UseRightToLeftCompatibleTextBox = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Abonnement CloudBackup";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAbo1
            // 
            this.lblAbo1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAbo1, 3);
            this.lblAbo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAbo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbo1.Location = new System.Drawing.Point(3, 50);
            this.lblAbo1.Name = "lblAbo1";
            this.lblAbo1.Size = new System.Drawing.Size(364, 16);
            this.lblAbo1.TabIndex = 1;
            this.lblAbo1.Text = "label2";
            // 
            // lblAbo2
            // 
            this.lblAbo2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAbo2, 3);
            this.lblAbo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAbo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbo2.Location = new System.Drawing.Point(3, 86);
            this.lblAbo2.Name = "lblAbo2";
            this.lblAbo2.Size = new System.Drawing.Size(364, 16);
            this.lblAbo2.TabIndex = 2;
            this.lblAbo2.Text = "label2";
            // 
            // lblAbo3
            // 
            this.lblAbo3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAbo3, 3);
            this.lblAbo3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAbo3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbo3.Location = new System.Drawing.Point(3, 122);
            this.lblAbo3.Name = "lblAbo3";
            this.lblAbo3.Size = new System.Drawing.Size(364, 16);
            this.lblAbo3.TabIndex = 3;
            this.lblAbo3.Text = "label2";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBank.Location = new System.Drawing.Point(3, 158);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(94, 30);
            this.lblBank.TabIndex = 4;
            this.lblBank.Text = "Kreditinstitut";
            this.lblBank.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBank
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtBank, 2);
            this.txtBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBank.Location = new System.Drawing.Point(103, 161);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(264, 20);
            this.txtBank.TabIndex = 7;
            // 
            // txtBIC
            // 
            this.txtBIC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBIC.Location = new System.Drawing.Point(103, 191);
            this.txtBIC.MaxLength = 11;
            this.txtBIC.Name = "txtBIC";
            this.txtBIC.Size = new System.Drawing.Size(122, 20);
            this.txtBIC.TabIndex = 8;
            // 
            // txtIBAN
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtIBAN, 2);
            this.txtIBAN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIBAN.Location = new System.Drawing.Point(103, 221);
            this.txtIBAN.MaxLength = 34;
            this.txtIBAN.Name = "txtIBAN";
            this.txtIBAN.Size = new System.Drawing.Size(264, 20);
            this.txtIBAN.TabIndex = 9;
            // 
            // lblBIC
            // 
            this.lblBIC.AutoSize = true;
            this.lblBIC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBIC.Location = new System.Drawing.Point(3, 188);
            this.lblBIC.Name = "lblBIC";
            this.lblBIC.Size = new System.Drawing.Size(94, 30);
            this.lblBIC.TabIndex = 5;
            this.lblBIC.Text = "BIC";
            this.lblBIC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIBAN
            // 
            this.lblIBAN.AutoSize = true;
            this.lblIBAN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIBAN.Location = new System.Drawing.Point(3, 218);
            this.lblIBAN.Name = "lblIBAN";
            this.lblIBAN.Size = new System.Drawing.Size(94, 30);
            this.lblIBAN.TabIndex = 6;
            this.lblIBAN.Text = "IBAN";
            this.lblIBAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOrder
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnOrder, 2);
            this.btnOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOrder.Location = new System.Drawing.Point(103, 411);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(264, 26);
            this.btnOrder.TabIndex = 10;
            this.btnOrder.Text = "Drucken und Bestellen";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(3, 411);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 26);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Abbruch";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pdfViewer
            // 
            this.pdfViewer.CursorMode = Syncfusion.Windows.Forms.PdfViewer.PdfViewerCursorMode.SelectTool;
            this.pdfViewer.EnableContextMenu = true;
            this.pdfViewer.EnableNotificationBar = true;
            this.pdfViewer.HorizontalScrollOffset = 0;
            this.pdfViewer.IsBookmarkEnabled = true;
            this.pdfViewer.IsTextSearchEnabled = true;
            this.pdfViewer.IsTextSelectionEnabled = true;
            this.pdfViewer.Location = new System.Drawing.Point(231, 251);
            messageBoxSettings1.EnableNotification = true;
            this.pdfViewer.MessageBoxSettings = messageBoxSettings1;
            this.pdfViewer.MinimumZoomPercentage = 50;
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings1.PrintLocation")));
            pdfViewerPrinterSettings1.ShowPrintStatusDialog = true;
            this.pdfViewer.PrinterSettings = pdfViewerPrinterSettings1;
            this.pdfViewer.ReferencePath = null;
            this.pdfViewer.ScrollDisplacementValue = 0;
            this.pdfViewer.ShowHorizontalScrollBar = true;
            this.pdfViewer.ShowToolBar = true;
            this.pdfViewer.ShowVerticalScrollBar = true;
            this.pdfViewer.Size = new System.Drawing.Size(85, 45);
            this.pdfViewer.SpaceBetweenPages = 8;
            this.pdfViewer.TabIndex = 13;
            this.pdfViewer.Text = "pdfViewerControl1";
            textSearchSettings1.CurrentInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(64)))));
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pdfViewer.TextSearchSettings = textSearchSettings1;
            this.pdfViewer.ThemeName = "Default";
            this.pdfViewer.VerticalScrollOffset = 0;
            this.pdfViewer.Visible = false;
            this.pdfViewer.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            this.pdfViewer.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
            // 
            // frmOrderCloudBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmOrderCloudBackup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOrderCloudBackup";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAbo1;
        private System.Windows.Forms.Label lblAbo2;
        private System.Windows.Forms.Label lblAbo3;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.TextBox txtBank;
        private System.Windows.Forms.TextBox txtBIC;
        private System.Windows.Forms.TextBox txtIBAN;
        private System.Windows.Forms.Label lblBIC;
        private System.Windows.Forms.Label lblIBAN;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnCancel;
        private Syncfusion.Windows.Forms.Grid.GridControl grdOrder;
        private Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl pdfViewer;
    }
}