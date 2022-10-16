namespace Coinbook
{
	partial class frmRepair
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRepair));
			this.btnRepair = new SAN.UI.Controls.ButtonEx();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnClose = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnRepair
			// 
			this.btnRepair.BackColor = System.Drawing.SystemColors.Control;
			this.btnRepair.ForeColor = System.Drawing.Color.Black;
			this.btnRepair.ImageStretch = false;
			this.btnRepair.Location = new System.Drawing.Point(41, 46);
			this.btnRepair.Name = "btnRepair";
			this.btnRepair.Size = new System.Drawing.Size(168, 23);
			this.btnRepair.Status = SAN.UI.Controls.ButtonStatus.Nothing;
			this.btnRepair.Stretched = false;
			this.btnRepair.TabIndex = 0;
			this.btnRepair.Text = "Datenbank reorganisieren";
			this.btnRepair.UseVisualStyleBackColor = false;
			this.btnRepair.Click += new System.EventHandler(this.btnRepair_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(255, 25);
			this.toolStrip1.TabIndex = 10;
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
			// frmRepair
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(255, 137);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.btnRepair);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "frmRepair";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Datenbank reorganisieren";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SAN.UI.Controls.ButtonEx btnRepair;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnClose;
	}
}