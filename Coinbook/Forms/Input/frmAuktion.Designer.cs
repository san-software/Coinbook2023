using System;

namespace Coinbook
{
    partial class frmAuktion
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuktion));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdAuktionen = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.lblGebietText = new System.Windows.Forms.Label();
            this.lblMünzeText = new System.Windows.Forms.Label();
            this.colErhaltungsgrad = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuktionator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuktionshaus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdAuktionen)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdAuktionen
            // 
            this.grdAuktionen.AllowUserToAddRows = false;
            this.grdAuktionen.AllowUserToDeleteRows = false;
            this.grdAuktionen.AllowUserToResizeColumns = false;
            this.grdAuktionen.AllowUserToResizeRows = false;
            this.grdAuktionen.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.grdAuktionen.ColumnHeadersHeight = 46;
            this.grdAuktionen.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colErhaltungsgrad,
            this.colDat,
            this.colPreis,
            this.colAuktionator,
            this.colAuktionshaus,
            this.colIndex,
            this.colGuid});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAuktionen.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdAuktionen.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdAuktionen.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdAuktionen.Location = new System.Drawing.Point(12, 122);
            this.grdAuktionen.MultiSelect = false;
            this.grdAuktionen.Name = "grdAuktionen";
            this.grdAuktionen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAuktionen.Size = new System.Drawing.Size(798, 346);
            this.grdAuktionen.StandardTab = true;
            this.grdAuktionen.TabIndex = 0;
            this.grdAuktionen.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAuktionen_CellValueChanged);
            this.grdAuktionen.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdAuktionen_DataError);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnNew,
            this.btnSave,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(822, 25);
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
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "Neuen Datensatz anlegen";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
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
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Löschen";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblGebietText
            // 
            this.lblGebietText.AutoSize = true;
            this.lblGebietText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGebietText.Location = new System.Drawing.Point(14, 33);
            this.lblGebietText.Name = "lblGebietText";
            this.lblGebietText.Size = new System.Drawing.Size(82, 13);
            this.lblGebietText.TabIndex = 7;
            this.lblGebietText.Text = "lblGebietText";
            // 
            // lblMünzeText
            // 
            this.lblMünzeText.AutoSize = true;
            this.lblMünzeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMünzeText.Location = new System.Drawing.Point(14, 57);
            this.lblMünzeText.Name = "lblMünzeText";
            this.lblMünzeText.Size = new System.Drawing.Size(82, 13);
            this.lblMünzeText.TabIndex = 7;
            this.lblMünzeText.Text = "lblMünzeText";
            // 
            // colErhaltungsgrad
            // 
            this.colErhaltungsgrad.DataPropertyName = "Erhaltungsgrad";
            this.colErhaltungsgrad.HeaderText = "Erhaltungs\r\ngrad";
            this.colErhaltungsgrad.Name = "colErhaltungsgrad";
            this.colErhaltungsgrad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colErhaltungsgrad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colErhaltungsgrad.Width = 85;
            // 
            // colDat
            // 
            this.colDat.DataPropertyName = "Datum";
            this.colDat.HeaderText = "Datum";
            this.colDat.MaxInputLength = 10;
            this.colDat.Name = "colDat";
            this.colDat.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDat.Width = 70;
            // 
            // colPreis
            // 
            this.colPreis.DataPropertyName = "Preis";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "###,##0.00";
            this.colPreis.DefaultCellStyle = dataGridViewCellStyle1;
            this.colPreis.HeaderText = "Preis";
            this.colPreis.MaxInputLength = 10;
            this.colPreis.Name = "colPreis";
            this.colPreis.Width = 80;
            // 
            // colAuktionator
            // 
            this.colAuktionator.DataPropertyName = "Auktionator";
            this.colAuktionator.HeaderText = "Auktionator";
            this.colAuktionator.MaxInputLength = 30;
            this.colAuktionator.Name = "colAuktionator";
            this.colAuktionator.Width = 250;
            // 
            // colAuktionshaus
            // 
            this.colAuktionshaus.DataPropertyName = "Auktionshaus";
            this.colAuktionshaus.HeaderText = "Auktionshaus";
            this.colAuktionshaus.MaxInputLength = 30;
            this.colAuktionshaus.Name = "colAuktionshaus";
            this.colAuktionshaus.Width = 250;
            // 
            // colIndex
            // 
            this.colIndex.DataPropertyName = "ID";
            this.colIndex.HeaderText = "Index";
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            this.colIndex.Visible = false;
            this.colIndex.Width = 5;
            // 
            // colGuid
            // 
            this.colGuid.DataPropertyName = "Guid";
            this.colGuid.HeaderText = "Guid";
            this.colGuid.Name = "colGuid";
            this.colGuid.ReadOnly = true;
            this.colGuid.Visible = false;
            // 
            // frmAuktion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(822, 489);
            this.Controls.Add(this.lblMünzeText);
            this.Controls.Add(this.lblGebietText);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grdAuktionen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmAuktion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auktionen";
            ((System.ComponentModel.ISupportInitialize)(this.grdAuktionen)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

				private System.Windows.Forms.DataGridView grdAuktionen;
				private System.Windows.Forms.ToolStrip toolStrip1;
				private System.Windows.Forms.ToolStripButton btnClose;
				private System.Windows.Forms.ToolStripButton btnNew;
				private System.Windows.Forms.Label lblGebietText;
				private System.Windows.Forms.Label lblMünzeText;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.DataGridViewComboBoxColumn colErhaltungsgrad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuktionator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuktionshaus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGuid;
    }
}