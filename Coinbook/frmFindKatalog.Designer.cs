namespace Coinbook
{
    partial class frmFindKatalog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFindKatalog));
			this.grdKatalog = new System.Windows.Forms.DataGridView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.Schliessen = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.grdKatalog)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grdKatalog
			// 
			this.grdKatalog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdKatalog.Location = new System.Drawing.Point(0, 35);
			this.grdKatalog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grdKatalog.Name = "grdKatalog";
			this.grdKatalog.RowHeadersVisible = false;
			this.grdKatalog.Size = new System.Drawing.Size(228, 285);
			this.grdKatalog.TabIndex = 0;
			this.grdKatalog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdKatalog_CellDoubleClick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Schliessen});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(228, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// Schliessen
			// 
			this.Schliessen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Schliessen.Image = ((System.Drawing.Image)(resources.GetObject("Schliessen.Image")));
			this.Schliessen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Schliessen.Name = "Schliessen";
			this.Schliessen.Size = new System.Drawing.Size(23, 22);
			this.Schliessen.Text = "btnClose";
			this.Schliessen.Click += new System.EventHandler(this.Schliessen_Click);
			// 
			// frmFindKatalog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(228, 321);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.grdKatalog);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmFindKatalog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Eigene Katalognummer suchen";
			((System.ComponentModel.ISupportInitialize)(this.grdKatalog)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

				private System.Windows.Forms.DataGridView grdKatalog;
				private System.Windows.Forms.ToolStrip toolStrip1;
				private System.Windows.Forms.ToolStripButton Schliessen;

		}
}