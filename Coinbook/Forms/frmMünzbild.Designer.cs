namespace Coinbook
{
  partial class frmMünzbild
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMünzbild));
			this.picMünze = new System.Windows.Forms.PictureBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnClose = new System.Windows.Forms.ToolStripButton();
			this.lblAnzeige = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblCopyright = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picMünze)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// picMünze
			// 
			this.picMünze.BackColor = System.Drawing.SystemColors.Control;
			this.picMünze.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picMünze.Location = new System.Drawing.Point(33, 33);
			this.picMünze.Name = "picMünze";
			this.picMünze.Size = new System.Drawing.Size(1162, 665);
			this.picMünze.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picMünze.TabIndex = 0;
			this.picMünze.TabStop = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1228, 25);
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
			// lblAnzeige
			// 
			this.lblAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAnzeige.Location = new System.Drawing.Point(33, 0);
			this.lblAnzeige.Name = "lblAnzeige";
			this.lblAnzeige.Size = new System.Drawing.Size(1162, 30);
			this.lblAnzeige.TabIndex = 7;
			this.lblAnzeige.Text = "labelEx1";
			this.lblAnzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Controls.Add(this.picMünze, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblAnzeige, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblCopyright, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1228, 726);
			this.tableLayoutPanel1.TabIndex = 8;
			// 
			// lblCopyright
			// 
			this.lblCopyright.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.lblCopyright, 3);
			this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCopyright.Location = new System.Drawing.Point(3, 701);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(1222, 25);
			this.lblCopyright.TabIndex = 8;
			this.lblCopyright.Text = "Die Copyrights für unsere Bilder finden Sie im Tabreiter Copyright in der Maske \'" +
    "Über das Programm\'";
			this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// frmMünzbild
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1228, 751);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "frmMünzbild";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			((System.ComponentModel.ISupportInitialize)(this.picMünze)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox picMünze;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnClose;
    private System.Windows.Forms.Label lblAnzeige;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblCopyright;
	}
}