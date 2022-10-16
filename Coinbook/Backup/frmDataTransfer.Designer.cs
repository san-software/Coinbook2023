
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
            this.txtUpload = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.txtDownload = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 276F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Controls.Add(this.optUpload, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.optDownload, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtUpload, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDownload, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnDownload, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnUpload, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(80, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(660, 347);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
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
            // 
            // optDownload
            // 
            this.optDownload.BeforeTouchSize = new System.Drawing.Size(153, 24);
            this.optDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optDownload.Location = new System.Drawing.Point(3, 123);
            this.optDownload.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.optDownload.Name = "optDownload";
            this.optDownload.Size = new System.Drawing.Size(153, 24);
            this.optDownload.TabIndex = 1;
            this.optDownload.Text = "Datenbank herunterladen";
            // 
            // txtUpload
            // 
            this.txtUpload.BeforeTouchSize = new System.Drawing.Size(270, 20);
            this.txtUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUpload.Location = new System.Drawing.Point(162, 33);
            this.txtUpload.Name = "txtUpload";
            this.txtUpload.Size = new System.Drawing.Size(270, 20);
            this.txtUpload.TabIndex = 2;
            this.txtUpload.Text = "textBoxExt1";
            // 
            // txtDownload
            // 
            this.txtDownload.BeforeTouchSize = new System.Drawing.Size(270, 20);
            this.txtDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDownload.Location = new System.Drawing.Point(162, 123);
            this.txtDownload.Name = "txtDownload";
            this.txtDownload.Size = new System.Drawing.Size(270, 20);
            this.txtDownload.TabIndex = 3;
            this.txtDownload.Text = "textBoxExt1";
            // 
            // btnDownload
            // 
            this.btnDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownload.Location = new System.Drawing.Point(438, 123);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(94, 24);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Ausführen";
            this.btnDownload.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpload.Location = new System.Drawing.Point(438, 33);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(94, 24);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Ausführen";
            this.btnUpload.UseVisualStyleBackColor = true;
            // 
            // frmDataTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmDataTransfer";
            this.Text = "frmDataTransfer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDownload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv optUpload;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv optDownload;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtUpload;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtDownload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpload;
    }
}