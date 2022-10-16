namespace Coinbook
{
    partial class frmDBState
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBState));
			this.grdÜbersicht = new System.Windows.Forms.DataGridView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnClose = new System.Windows.Forms.ToolStripButton();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.colNation = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colVerwendet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colLastUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.grdÜbersicht)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grdÜbersicht
			// 
			this.grdÜbersicht.AllowUserToAddRows = false;
			this.grdÜbersicht.AllowUserToDeleteRows = false;
			this.grdÜbersicht.AllowUserToResizeColumns = false;
			this.grdÜbersicht.AllowUserToResizeRows = false;
			this.grdÜbersicht.BackgroundColor = System.Drawing.Color.Gainsboro;
			this.grdÜbersicht.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdÜbersicht.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNation,
            this.colVerwendet,
            this.colLastUpdate,
            this.colID});
			this.grdÜbersicht.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdÜbersicht.GridColor = System.Drawing.SystemColors.ControlDarkDark;
			this.grdÜbersicht.Location = new System.Drawing.Point(3, 3);
			this.grdÜbersicht.Name = "grdÜbersicht";
			this.grdÜbersicht.RowHeadersVisible = false;
			this.grdÜbersicht.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grdÜbersicht.ShowCellErrors = false;
			this.grdÜbersicht.ShowEditingIcon = false;
			this.grdÜbersicht.Size = new System.Drawing.Size(481, 410);
			this.grdÜbersicht.TabIndex = 0;
			this.grdÜbersicht.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdÜbersicht_CellValueChanged);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnSave});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(487, 25);
			this.toolStrip1.TabIndex = 3;
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
			// btnSave
			// 
			this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "Speichern";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.grdÜbersicht, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(487, 416);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// colNation
			// 
			this.colNation.DataPropertyName = "Nation";
			this.colNation.HeaderText = "Nation";
			this.colNation.MinimumWidth = 100;
			this.colNation.Name = "colNation";
			this.colNation.ReadOnly = true;
			this.colNation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colNation.Width = 200;
			// 
			// colVerwendet
			// 
			this.colVerwendet.DataPropertyName = "InUse";
			this.colVerwendet.FalseValue = "false";
			this.colVerwendet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.colVerwendet.HeaderText = "Verwendet";
			this.colVerwendet.Name = "colVerwendet";
			this.colVerwendet.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colVerwendet.TrueValue = "true";
			this.colVerwendet.Width = 80;
			// 
			// colLastUpdate
			// 
			this.colLastUpdate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colLastUpdate.DataPropertyName = "Jahr";
			this.colLastUpdate.HeaderText = "Update";
			this.colLastUpdate.MinimumWidth = 100;
			this.colLastUpdate.Name = "colLastUpdate";
			this.colLastUpdate.ReadOnly = true;
			// 
			// colID
			// 
			this.colID.DataPropertyName = "ID";
			this.colID.HeaderText = "ID";
			this.colID.Name = "colID";
			this.colID.ReadOnly = true;
			this.colID.Visible = false;
			// 
			// frmDBState
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(487, 441);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.toolStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmDBState";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Status der Datenbank";
			((System.ComponentModel.ISupportInitialize)(this.grdÜbersicht)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

				}

        #endregion

				private System.Windows.Forms.DataGridView grdÜbersicht;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnClose;
    private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.DataGridViewTextBoxColumn colNation;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colVerwendet;
		private System.Windows.Forms.DataGridViewTextBoxColumn colLastUpdate;
		private System.Windows.Forms.DataGridViewTextBoxColumn colID;
	}
}