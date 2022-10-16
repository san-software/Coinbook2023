using System;

namespace Coinbook
{
    partial class frmEigenePreise
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEigenePreise));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.grdPreise = new System.Windows.Forms.DataGridView();
			this.lblMünzeText = new System.Windows.Forms.Label();
			this.lblMünze = new System.Windows.Forms.Label();
			this.lblGebietText = new System.Windows.Forms.Label();
			this.lblGebiet = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnClose = new System.Windows.Forms.ToolStripButton();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colErhaltungsgrad = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colSammlungID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colPreis = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.grdPreise)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grdPreise
			// 
			this.grdPreise.AllowUserToAddRows = false;
			this.grdPreise.AllowUserToDeleteRows = false;
			this.grdPreise.AllowUserToResizeColumns = false;
			this.grdPreise.AllowUserToResizeRows = false;
			this.grdPreise.BackgroundColor = System.Drawing.Color.Gainsboro;
			this.grdPreise.ColumnHeadersHeight = 46;
			this.grdPreise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.grdPreise.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colErhaltungsgrad,
            this.colSammlungID,
            this.colPreis});
			this.grdPreise.GridColor = System.Drawing.SystemColors.ControlDarkDark;
			this.grdPreise.Location = new System.Drawing.Point(12, 180);
			this.grdPreise.MultiSelect = false;
			this.grdPreise.Name = "grdPreise";
			this.grdPreise.RowHeadersVisible = false;
			this.grdPreise.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grdPreise.ShowEditingIcon = false;
			this.grdPreise.Size = new System.Drawing.Size(308, 249);
			this.grdPreise.TabIndex = 0;
			this.grdPreise.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPreise_CellValueChanged);
			// 
			// lblMünzeText
			// 
			this.lblMünzeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMünzeText.Location = new System.Drawing.Point(56, 97);
			this.lblMünzeText.Name = "lblMünzeText";
			this.lblMünzeText.Size = new System.Drawing.Size(264, 67);
			this.lblMünzeText.TabIndex = 35;
			this.lblMünzeText.Text = "Münze";
			// 
			// lblMünze
			// 
			this.lblMünze.AutoSize = true;
			this.lblMünze.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMünze.Location = new System.Drawing.Point(11, 97);
			this.lblMünze.Name = "lblMünze";
			this.lblMünze.Size = new System.Drawing.Size(39, 13);
			this.lblMünze.TabIndex = 34;
			this.lblMünze.Text = "Münze";
			// 
			// lblGebietText
			// 
			this.lblGebietText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGebietText.Location = new System.Drawing.Point(56, 34);
			this.lblGebietText.Name = "lblGebietText";
			this.lblGebietText.Size = new System.Drawing.Size(264, 63);
			this.lblGebietText.TabIndex = 33;
			this.lblGebietText.Text = "Gebiet";
			// 
			// lblGebiet
			// 
			this.lblGebiet.AutoSize = true;
			this.lblGebiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGebiet.Location = new System.Drawing.Point(12, 34);
			this.lblGebiet.Name = "lblGebiet";
			this.lblGebiet.Size = new System.Drawing.Size(38, 13);
			this.lblGebiet.TabIndex = 32;
			this.lblGebiet.Text = "Gebiet";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnSave});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(332, 25);
			this.toolStrip1.TabIndex = 36;
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// colID
			// 
			this.colID.DataPropertyName = "ID";
			this.colID.HeaderText = "ID";
			this.colID.Name = "colID";
			this.colID.Visible = false;
			// 
			// colErhaltungsgrad
			// 
			this.colErhaltungsgrad.DataPropertyName = "Erhaltung";
			this.colErhaltungsgrad.HeaderText = "Erhaltungs\r\ngrad";
			this.colErhaltungsgrad.Name = "colErhaltungsgrad";
			this.colErhaltungsgrad.ReadOnly = true;
			this.colErhaltungsgrad.Width = 90;
			// 
			// colSammlungID
			// 
			this.colSammlungID.DataPropertyName = "SammlungID";
			this.colSammlungID.HeaderText = "Sammlung";
			this.colSammlungID.Name = "colSammlungID";
			this.colSammlungID.Visible = false;
			this.colSammlungID.Width = 60;
			// 
			// colPreis
			// 
			this.colPreis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colPreis.DataPropertyName = "Preis";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle1.Format = "#####0.00";
			this.colPreis.DefaultCellStyle = dataGridViewCellStyle1;
			this.colPreis.HeaderText = "Preis";
			this.colPreis.MaxInputLength = 10;
			this.colPreis.Name = "colPreis";
			// 
			// frmEigenePreise
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(332, 441);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.lblMünzeText);
			this.Controls.Add(this.lblMünze);
			this.Controls.Add(this.lblGebietText);
			this.Controls.Add(this.lblGebiet);
			this.Controls.Add(this.grdPreise);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEigenePreise";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Eigene Preise bearbeiten";
			((System.ComponentModel.ISupportInitialize)(this.grdPreise)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

				private System.Windows.Forms.DataGridView grdPreise;
				private System.Windows.Forms.Label lblMünzeText;
				private System.Windows.Forms.Label lblMünze;
				private System.Windows.Forms.Label lblGebietText;
				private System.Windows.Forms.Label lblGebiet;
				private System.Windows.Forms.ToolStrip toolStrip1;
				private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.DataGridViewTextBoxColumn colID;
		private System.Windows.Forms.DataGridViewTextBoxColumn colErhaltungsgrad;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSammlungID;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPreis;
	}
}