using SAN.Control;

namespace Coinbook
{
    partial class usrWährung
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdÜbersicht = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWährung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kurz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaktor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblWährung = new System.Windows.Forms.Label();
            this.cboWährung = new SAN.Control.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.grdÜbersicht)).BeginInit();
            this.SuspendLayout();
            // 
            // grdÜbersicht
            // 
            this.grdÜbersicht.AllowUserToAddRows = false;
            this.grdÜbersicht.AllowUserToDeleteRows = false;
            this.grdÜbersicht.AllowUserToOrderColumns = true;
            this.grdÜbersicht.AllowUserToResizeColumns = false;
            this.grdÜbersicht.AllowUserToResizeRows = false;
            this.grdÜbersicht.BackgroundColor = System.Drawing.Color.LightGray;
            this.grdÜbersicht.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdÜbersicht.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colWährung,
            this.Kurz,
            this.colFaktor});
            this.grdÜbersicht.Location = new System.Drawing.Point(6, 41);
            this.grdÜbersicht.Name = "grdÜbersicht";
            this.grdÜbersicht.RowHeadersVisible = false;
            this.grdÜbersicht.Size = new System.Drawing.Size(358, 277);
            this.grdÜbersicht.TabIndex = 9;
            this.grdÜbersicht.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdÜbersicht_CellValueChanged);
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = false;
            this.colID.Width = 35;
            // 
            // colWährung
            // 
            this.colWährung.DataPropertyName = "Bezeichnung";
            this.colWährung.HeaderText = "Währung";
            this.colWährung.MaxInputLength = 10;
            this.colWährung.Name = "colWährung";
            this.colWährung.ReadOnly = true;
            this.colWährung.Width = 165;
            // 
            // Kurz
            // 
            this.Kurz.DataPropertyName = "Waehrung";
            this.Kurz.HeaderText = "Kurz";
            this.Kurz.Name = "Kurz";
            this.Kurz.Width = 50;
            // 
            // colFaktor
            // 
            this.colFaktor.DataPropertyName = "Faktor";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colFaktor.DefaultCellStyle = dataGridViewCellStyle1;
            this.colFaktor.HeaderText = "Faktor";
            this.colFaktor.Name = "colFaktor";
            this.colFaktor.Width = 70;
            // 
            // lblWährung
            // 
            this.lblWährung.AutoSize = true;
            this.lblWährung.Location = new System.Drawing.Point(8, 20);
            this.lblWährung.Name = "lblWährung";
            this.lblWährung.Size = new System.Drawing.Size(108, 13);
            this.lblWährung.TabIndex = 10;
            this.lblWährung.Text = "Eingestellte Währung";
            // 
            // cboWährung
            // 
            this.cboWährung.BackColor = System.Drawing.Color.White;
            this.cboWährung.Column = 0;
            this.cboWährung.ColumnsToDisplay = "";
            this.cboWährung.ColumnType = SAN.Control.ComboBoxEx.ColType.SingleColumn;
            this.cboWährung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWährung.ForeColor = System.Drawing.Color.Black;
            this.cboWährung.FormattingEnabled = true;
            this.cboWährung.GridLinesMultiColumn = false;
            this.cboWährung.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboWährung.ID = ((long)(-1));
            this.cboWährung.IDObject = -1;
            this.cboWährung.IDString = "";
            this.cboWährung.IsPflichtfeld = false;
            this.cboWährung.Location = new System.Drawing.Point(136, 14);
            this.cboWährung.MaxDropDownItems = 10;
            this.cboWährung.Name = "cboWährung";
            this.cboWährung.ReadOnly = false;
            this.cboWährung.Row = 0;
            this.cboWährung.ShowClipBoard = true;
            this.cboWährung.Size = new System.Drawing.Size(228, 21);
            this.cboWährung.TabIndex = 11;
            this.cboWährung.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboWährung.SelectedIndexChanged += new System.EventHandler(this.cboWährung_SelectedIndexChanged);
            // 
            // usrWährung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.cboWährung);
            this.Controls.Add(this.lblWährung);
            this.Controls.Add(this.grdÜbersicht);
            this.Name = "usrWährung";
            this.Size = new System.Drawing.Size(378, 331);
            ((System.ComponentModel.ISupportInitialize)(this.grdÜbersicht)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdÜbersicht;
        private System.Windows.Forms.Label lblWährung;
        private ComboBoxEx cboWährung;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWährung;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kurz;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaktor;
    }
}
