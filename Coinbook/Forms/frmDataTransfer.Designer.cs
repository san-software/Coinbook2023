
namespace Coinbook
{
    partial class frmDataTransfer
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
            this.optUpload = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.optDownload = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btbClose = new System.Windows.Forms.Button();
            this.txtUpload = new System.Windows.Forms.RichTextBox();
            this.txtDownload = new System.Windows.Forms.RichTextBox();
            this.progressBarUpload = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.progressBarDownload = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.backgroundWorkerUpload = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerDownload = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.optUpload, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.optDownload, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnDownload, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnUpload, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btbClose, 2, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtUpload, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDownload, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.progressBarUpload, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.progressBarDownload, 1, 11);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(737, 435);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // optUpload
            // 
            this.optUpload.BeforeTouchSize = new System.Drawing.Size(153, 24);
            this.optUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optUpload.Location = new System.Drawing.Point(3, 33);
            this.optUpload.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.optUpload.Name = "optUpload";
            this.optUpload.Size = new System.Drawing.Size(153, 24);
            this.optUpload.TabIndex = 0;
            this.optUpload.Text = "Datenbank hochladen";
            this.optUpload.CheckChanged += new System.EventHandler(this.optUpload_CheckChanged);
            // 
            // optDownload
            // 
            this.optDownload.BeforeTouchSize = new System.Drawing.Size(153, 24);
            this.optDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optDownload.Location = new System.Drawing.Point(3, 243);
            this.optDownload.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.optDownload.Name = "optDownload";
            this.optDownload.Size = new System.Drawing.Size(153, 24);
            this.optDownload.TabIndex = 1;
            this.optDownload.Text = "Datenbank herunterladen";
            this.optDownload.CheckChanged += new System.EventHandler(this.optDownload_CheckChanged);
            // 
            // btnDownload
            // 
            this.btnDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(614, 243);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(94, 24);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Ausführen";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(614, 33);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(94, 24);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Ausführen";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btbClose
            // 
            this.btbClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btbClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btbClose.Location = new System.Drawing.Point(614, 363);
            this.btbClose.Name = "btbClose";
            this.btbClose.Size = new System.Drawing.Size(94, 24);
            this.btbClose.TabIndex = 7;
            this.btbClose.Text = "Schliessen";
            this.btbClose.UseVisualStyleBackColor = true;
            this.btbClose.Click += new System.EventHandler(this.btbClose_Click);
            // 
            // txtUpload
            // 
            this.txtUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUpload.Location = new System.Drawing.Point(162, 33);
            this.txtUpload.Name = "txtUpload";
            this.txtUpload.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.txtUpload, 5);
            this.txtUpload.Size = new System.Drawing.Size(446, 144);
            this.txtUpload.TabIndex = 8;
            this.txtUpload.Text = "";
            // 
            // txtDownload
            // 
            this.txtDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDownload.Location = new System.Drawing.Point(162, 243);
            this.txtDownload.Name = "txtDownload";
            this.txtDownload.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.txtDownload, 3);
            this.txtDownload.Size = new System.Drawing.Size(446, 84);
            this.txtDownload.TabIndex = 9;
            this.txtDownload.Text = "";
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarUpload.BackSegments = false;
            this.progressBarUpload.CustomText = null;
            this.progressBarUpload.CustomWaitingRender = false;
            this.progressBarUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarUpload.ForegroundImage = null;
            this.progressBarUpload.Location = new System.Drawing.Point(162, 183);
            this.progressBarUpload.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarUpload.SegmentWidth = 1;
            this.progressBarUpload.Size = new System.Drawing.Size(446, 24);
            this.progressBarUpload.TabIndex = 10;
            this.progressBarUpload.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
            this.progressBarUpload.ThemeName = "WaitingGradient";
            this.progressBarUpload.WaitingGradientWidth = 400;
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarDownload.BackSegments = false;
            this.progressBarDownload.CustomText = null;
            this.progressBarDownload.CustomWaitingRender = false;
            this.progressBarDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarDownload.ForegroundImage = null;
            this.progressBarDownload.Location = new System.Drawing.Point(162, 333);
            this.progressBarDownload.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.WaitingGradient;
            this.progressBarDownload.SegmentWidth = 12;
            this.progressBarDownload.Size = new System.Drawing.Size(446, 24);
            this.progressBarDownload.TabIndex = 11;
            this.progressBarDownload.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
            this.progressBarDownload.ThemeName = "WaitingGradient";
            this.progressBarDownload.WaitingGradientWidth = 400;
            // 
            // backgroundWorkerUpload
            // 
            this.backgroundWorkerUpload.WorkerReportsProgress = true;
            this.backgroundWorkerUpload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerUpload_DoWork);
            this.backgroundWorkerUpload.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerUpload_ProgressChanged);
            this.backgroundWorkerUpload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerUpload_RunWorkerCompleted);
            // 
            // backgroundWorkerDownload
            // 
            this.backgroundWorkerDownload.WorkerReportsProgress = true;
            this.backgroundWorkerDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDownload_DoWork);
            this.backgroundWorkerDownload.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerDownload_ProgressChanged);
            this.backgroundWorkerDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDownload_RunWorkerCompleted);
            // 
            // frmDataTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 439);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmDataTransfer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Datentransfer";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.optUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarDownload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv optUpload;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv optDownload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btbClose;
        private System.Windows.Forms.RichTextBox txtUpload;
        private System.Windows.Forms.RichTextBox txtDownload;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBarUpload;
        private System.ComponentModel.BackgroundWorker backgroundWorkerUpload;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBarDownload;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDownload;
    }
}