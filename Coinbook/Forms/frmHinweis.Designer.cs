namespace Coinbook
{
    partial class frmHinweis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHinweis));
            this.lblKommentar = new System.Windows.Forms.Label();
            this.lblBesonderheit = new System.Windows.Forms.Label();
            this.txtBesonderheit = new System.Windows.Forms.TextBox();
            this.txtKommentar = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblKommentar
            // 
            this.lblKommentar.AutoSize = true;
            this.lblKommentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKommentar.Location = new System.Drawing.Point(11, 119);
            this.lblKommentar.Name = "lblKommentar";
            this.lblKommentar.Size = new System.Drawing.Size(60, 13);
            this.lblKommentar.TabIndex = 0;
            this.lblKommentar.Text = "Kommentar";
            // 
            // lblBesonderheit
            // 
            this.lblBesonderheit.AutoSize = true;
            this.lblBesonderheit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBesonderheit.Location = new System.Drawing.Point(11, 30);
            this.lblBesonderheit.Name = "lblBesonderheit";
            this.lblBesonderheit.Size = new System.Drawing.Size(69, 13);
            this.lblBesonderheit.TabIndex = 2;
            this.lblBesonderheit.Text = "Besonderheit";
            // 
            // txtBesonderheit
            // 
            this.txtBesonderheit.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBesonderheit.Location = new System.Drawing.Point(11, 46);
            this.txtBesonderheit.Multiline = true;
            this.txtBesonderheit.Name = "txtBesonderheit";
            this.txtBesonderheit.ReadOnly = true;
            this.txtBesonderheit.Size = new System.Drawing.Size(608, 70);
            this.txtBesonderheit.TabIndex = 3;
            this.txtBesonderheit.TabStop = false;
            // 
            // txtKommentar
            // 
            this.txtKommentar.BackColor = System.Drawing.Color.Gainsboro;
            this.txtKommentar.Location = new System.Drawing.Point(11, 135);
            this.txtKommentar.Multiline = true;
            this.txtKommentar.Name = "txtKommentar";
            this.txtKommentar.ReadOnly = true;
            this.txtKommentar.Size = new System.Drawing.Size(608, 70);
            this.txtKommentar.TabIndex = 4;
            this.txtKommentar.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(632, 25);
            this.toolStrip1.TabIndex = 6;
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
            // frmHinweis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(632, 217);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtKommentar);
            this.Controls.Add(this.txtBesonderheit);
            this.Controls.Add(this.lblBesonderheit);
            this.Controls.Add(this.lblKommentar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmHinweis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hinweis";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKommentar;
        private System.Windows.Forms.Label lblBesonderheit;
        private System.Windows.Forms.TextBox txtBesonderheit;
				private System.Windows.Forms.TextBox txtKommentar;
				private System.Windows.Forms.ToolStrip toolStrip1;
				private System.Windows.Forms.ToolStripButton btnClose;
    }
}