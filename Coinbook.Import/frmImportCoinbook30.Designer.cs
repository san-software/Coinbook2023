namespace Coinbook.Import
{
    partial class frmImportCoinbook30
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
            this.labelEx1 = new System.Windows.Forms.Label();
            this.pgbGesamt = new SAN.Control.ProgressBarEx();
            this.labelEx2 = new System.Windows.Forms.Label();
            this.pgbTabelle = new SAN.Control.ProgressBarEx();
            this.btnImport = new SAN.Control.ButtonEx();
            this.btnCancel = new SAN.Control.ButtonEx();
            this.progressBarAdv1 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labelEx1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pgbGesamt, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelEx2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.pgbTabelle, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnImport, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.progressBarAdv1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(655, 208);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEx1.Location = new System.Drawing.Point(3, 30);
            this.labelEx1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(94, 20);
            this.labelEx1.TabIndex = 0;
            this.labelEx1.Text = "Gesamt";
            // 
            // pgbGesamt
            // 
            this.pgbGesamt.BarColor = System.Drawing.Color.Aqua;
            this.pgbGesamt.BlockDistance = ((byte)(0));
            this.pgbGesamt.BlockWidth = ((byte)(1));
            this.pgbGesamt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(240)))), ((int)(((byte)(170)))));
            this.tableLayoutPanel1.SetColumnSpan(this.pgbGesamt, 4);
            this.pgbGesamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgbGesamt.Location = new System.Drawing.Point(1, 51);
            this.pgbGesamt.Margin = new System.Windows.Forms.Padding(1);
            this.pgbGesamt.MaxValue = 100;
            this.pgbGesamt.MinValue = 0;
            this.pgbGesamt.Name = "pgbGesamt";
            this.pgbGesamt.Size = new System.Drawing.Size(648, 23);
            this.pgbGesamt.Style = SAN.Control.ProgressBarEx.ProgressbarStyleEx.Marquee;
            this.pgbGesamt.TabIndex = 2;
            this.pgbGesamt.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pgbGesamt.TextShadow = false;
            this.pgbGesamt.Value = 0;
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEx2.Location = new System.Drawing.Point(3, 105);
            this.labelEx2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new System.Drawing.Size(94, 20);
            this.labelEx2.TabIndex = 1;
            this.labelEx2.Text = "Tabelle";
            this.labelEx2.Visible = false;
            // 
            // pgbTabelle
            // 
            this.pgbTabelle.BarColor = System.Drawing.Color.Aqua;
            this.pgbTabelle.BlockDistance = ((byte)(0));
            this.pgbTabelle.BlockWidth = ((byte)(1));
            this.pgbTabelle.BorderColor = System.Drawing.Color.Aqua;
            this.tableLayoutPanel1.SetColumnSpan(this.pgbTabelle, 4);
            this.pgbTabelle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgbTabelle.Location = new System.Drawing.Point(1, 126);
            this.pgbTabelle.Margin = new System.Windows.Forms.Padding(1);
            this.pgbTabelle.MaxValue = 100;
            this.pgbTabelle.MinValue = 0;
            this.pgbTabelle.Name = "pgbTabelle";
            this.pgbTabelle.Size = new System.Drawing.Size(648, 23);
            this.pgbTabelle.TabIndex = 3;
            this.pgbTabelle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pgbTabelle.TextShadow = false;
            this.pgbTabelle.Value = 0;
            this.pgbTabelle.Visible = false;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.SystemColors.Control;
            this.btnImport.ForeColor = System.Drawing.Color.Black;
            this.btnImport.ImageStretch = false;
            this.btnImport.Location = new System.Drawing.Point(553, 178);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 22);
            this.btnImport.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnImport.Stretched = false;
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Importiere";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Visible = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageStretch = false;
            this.btnCancel.Location = new System.Drawing.Point(453, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 23);
            this.btnCancel.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnCancel.Stretched = false;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Abbruch";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressBarAdv1
            // 
            this.progressBarAdv1.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv1.BackSegments = false;
            this.progressBarAdv1.CustomText = null;
            this.progressBarAdv1.CustomWaitingRender = false;
            this.progressBarAdv1.ForeColor = System.Drawing.Color.Cyan;
            this.progressBarAdv1.ForegroundImage = null;
            this.progressBarAdv1.Location = new System.Drawing.Point(103, 3);
            this.progressBarAdv1.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv1.Name = "progressBarAdv1";
            this.progressBarAdv1.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.Gradient;
            this.progressBarAdv1.SegmentWidth = 1;
            this.progressBarAdv1.ShowProgressImage = true;
            this.progressBarAdv1.Size = new System.Drawing.Size(344, 19);
            this.progressBarAdv1.Step = 1;
            this.progressBarAdv1.TabIndex = 6;
            this.progressBarAdv1.Text = "progressBarAdv1";
            this.progressBarAdv1.ThemeName = "Gradient";
            this.progressBarAdv1.WaitingGradientWidth = 400;
            // 
            // frmImportCoinbook30
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 208);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmImportCoinbook30";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import von Coinbook 3.0";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.frmImportCoinbook30_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelEx1;
        private System.Windows.Forms.Label labelEx2;
        private SAN.Control.ProgressBarEx pgbGesamt;
        private SAN.Control.ProgressBarEx pgbTabelle;
        private SAN.Control.ButtonEx btnImport;
        private SAN.Control.ButtonEx btnCancel;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBarAdv1;
    }
}