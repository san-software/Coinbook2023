using System;
namespace Coinbook.Modulverwaltung
{
  partial class frmOrder2
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
			this.grdOrder = new Syncfusion.Windows.Forms.Grid.GridControl();
			this.btnBack = new SAN.Control.ButtonEx();
			this.btnOrder = new SAN.Control.ButtonEx();
			this.btnCancel = new SAN.Control.ButtonEx();
			this.txtAnzeige = new SAN.Control.TextBoxEx();
			this.lblText = new System.Windows.Forms.Label();
			this.txtBemerkung = new SAN.Control.TextBoxEx();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdOrder)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.grdOrder, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnBack, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.btnOrder, 3, 5);
			this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.txtAnzeige, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.lblText, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtBemerkung, 0, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(698, 619);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// grdOrder
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.grdOrder, 4);
			this.grdOrder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdOrder.Location = new System.Drawing.Point(3, 3);
			this.grdOrder.Name = "grdOrder";
			this.grdOrder.SerializeCellsBehavior = Syncfusion.Windows.Forms.Grid.GridSerializeCellsBehavior.SerializeIntoCode;
			this.grdOrder.Size = new System.Drawing.Size(692, 333);
			this.grdOrder.SmartSizeBox = false;
			this.grdOrder.TabIndex = 1;
			this.grdOrder.UseRightToLeftCompatibleTextBox = true;
			// 
			// btnBack
			// 
			this.btnBack.BackColor = System.Drawing.SystemColors.Control;
			this.btnBack.ForeColor = System.Drawing.Color.Black;
			this.btnBack.ImageStretch = false;
			this.btnBack.Location = new System.Drawing.Point(3, 592);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(94, 24);
			this.btnBack.Status = SAN.Control.ButtonStatus.Nothing;
			this.btnBack.Stretched = false;
			this.btnBack.TabIndex = 2;
			this.btnBack.Text = "Zurück";
			this.btnBack.UseVisualStyleBackColor = false;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnOrder
			// 
			this.btnOrder.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.btnOrder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOrder.ForeColor = System.Drawing.Color.Black;
			this.btnOrder.ImageStretch = false;
			this.btnOrder.Location = new System.Drawing.Point(303, 592);
			this.btnOrder.Name = "btnOrder";
			this.btnOrder.Size = new System.Drawing.Size(392, 24);
			this.btnOrder.Status = SAN.Control.ButtonStatus.Nothing;
			this.btnOrder.Stretched = false;
			this.btnOrder.TabIndex = 4;
			this.btnOrder.Text = "Drucken und verbindlich bestellen";
			this.btnOrder.UseVisualStyleBackColor = false;
			this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnCancel.ForeColor = System.Drawing.Color.Black;
			this.btnCancel.ImageStretch = false;
			this.btnCancel.Location = new System.Drawing.Point(103, 592);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(94, 24);
			this.btnCancel.Status = SAN.Control.ButtonStatus.Nothing;
			this.btnCancel.Stretched = false;
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Abbruch";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtAnzeige
			// 
			this.txtAnzeige.AcceptsReturn = true;
			this.txtAnzeige.AcceptsTab = true;
			this.txtAnzeige.BackColor = System.Drawing.Color.White;
			this.txtAnzeige.Column = 0;
			this.tableLayoutPanel1.SetColumnSpan(this.txtAnzeige, 4);
			this.txtAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtAnzeige.Enter2Tab = true;
			this.txtAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAnzeige.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtAnzeige.IsPflichtfeld = false;
			this.txtAnzeige.Location = new System.Drawing.Point(3, 512);
			this.txtAnzeige.Multiline = true;
			this.txtAnzeige.NachkommaStellen = ((short)(0));
			this.txtAnzeige.Name = "txtAnzeige";
			this.txtAnzeige.NumberFormat = null;
			this.txtAnzeige.OldText = "textBoxEx1";
			this.txtAnzeige.ReadOnly = true;
			this.txtAnzeige.RegularExpression = String.Empty;
			this.txtAnzeige.Row = 4;
			this.txtAnzeige.ShowClipBoard = true;
			this.txtAnzeige.Size = new System.Drawing.Size(692, 74);
			this.txtAnzeige.TabIndex = 6;
			this.txtAnzeige.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtAnzeige.Translation = String.Empty;
			this.txtAnzeige.Typ = SAN.Control.TextBoxTyp.Text;
			// 
			// lblText
			// 
			this.lblText.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.lblText, 4);
			this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblText.Location = new System.Drawing.Point(3, 359);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(692, 50);
			this.lblText.TabIndex = 7;
			this.lblText.Text = "lblText";
			// 
			// txtBemerkung
			// 
			this.txtBemerkung.AcceptsReturn = true;
			this.txtBemerkung.AcceptsTab = true;
			this.txtBemerkung.BackColor = System.Drawing.Color.White;
			this.txtBemerkung.Column = 0;
			this.tableLayoutPanel1.SetColumnSpan(this.txtBemerkung, 4);
			this.txtBemerkung.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBemerkung.Enter2Tab = true;
			this.txtBemerkung.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtBemerkung.IsPflichtfeld = false;
			this.txtBemerkung.Location = new System.Drawing.Point(3, 412);
			this.txtBemerkung.Multiline = true;
			this.txtBemerkung.NachkommaStellen = ((short)(0));
			this.txtBemerkung.Name = "txtBemerkung";
			this.txtBemerkung.NumberFormat = null;
			this.txtBemerkung.OldText = "textBoxEx1";
			this.txtBemerkung.RegularExpression = String.Empty;
			this.txtBemerkung.Row = 0;
			this.txtBemerkung.ShowClipBoard = true;
			this.txtBemerkung.Size = new System.Drawing.Size(692, 94);
			this.txtBemerkung.TabIndex = 8;
			this.txtBemerkung.Translation = String.Empty;
			this.txtBemerkung.Typ = SAN.Control.TextBoxTyp.Text;
			// 
			// frmOrder2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(698, 619);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmOrder2";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Bestellung überprüfen";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdOrder)).EndInit();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Syncfusion.Windows.Forms.Grid.GridControl grdOrder;
		private SAN.Control.ButtonEx btnBack;
    private SAN.Control.ButtonEx btnOrder;
    private SAN.Control.ButtonEx btnCancel;
    private SAN.Control.TextBoxEx txtAnzeige;
		private System.Windows.Forms.Label lblText;
		private SAN.Control.TextBoxEx txtBemerkung;
	}
}