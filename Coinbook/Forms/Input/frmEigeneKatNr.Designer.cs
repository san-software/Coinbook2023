using System;
namespace Coinbook
{
    partial class frmKatalogNummer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKatalogNummer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.lblKatalognummer = new System.Windows.Forms.Label();
            this.lblCoinbookNummer = new System.Windows.Forms.Label();
            this.txtKatNr = new SAN.Control.TextBoxEx();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnDelete,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(399, 25);
            this.toolStrip1.TabIndex = 2;
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
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Katalognummer löschen";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            // lblKatalognummer
            // 
            this.lblKatalognummer.AutoSize = true;
            this.lblKatalognummer.Location = new System.Drawing.Point(9, 36);
            this.lblKatalognummer.Name = "lblKatalognummer";
            this.lblKatalognummer.Size = new System.Drawing.Size(131, 13);
            this.lblKatalognummer.TabIndex = 3;
            this.lblKatalognummer.Text = "Eigene Katalognummer für";
            // 
            // lblCoinbookNummer
            // 
            this.lblCoinbookNummer.AutoSize = true;
            this.lblCoinbookNummer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoinbookNummer.Location = new System.Drawing.Point(146, 36);
            this.lblCoinbookNummer.Name = "lblCoinbookNummer";
            this.lblCoinbookNummer.Size = new System.Drawing.Size(55, 13);
            this.lblCoinbookNummer.TabIndex = 4;
            this.lblCoinbookNummer.Text = "labelEx2";
            // 
            // txtKatNr
            // 
            this.txtKatNr.AcceptsReturn = true;
            this.txtKatNr.AcceptsTab = true;
            this.txtKatNr.BackColor = System.Drawing.Color.White;
            this.txtKatNr.Column = 0;
            this.txtKatNr.Enter2Tab = true;
            this.txtKatNr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtKatNr.IsPflichtfeld = false;
            this.txtKatNr.Location = new System.Drawing.Point(233, 33);
            this.txtKatNr.MaxLength = 20;
            this.txtKatNr.NachkommaStellen = ((short)(0));
            this.txtKatNr.Name = "txtKatNr";
            this.txtKatNr.NumberFormat = null;
            this.txtKatNr.OldText = "textBoxEx1";
            this.txtKatNr.RegularExpression = "";
            this.txtKatNr.Row = 0;
            this.txtKatNr.ShowClipBoard = true;
            this.txtKatNr.Size = new System.Drawing.Size(142, 20);
            this.txtKatNr.TabIndex = 5;
            this.txtKatNr.Translation = "";
            this.txtKatNr.Typ = SAN.Control.TextBoxTyp.Text;
            this.txtKatNr.TextChanged += new System.EventHandler(this.txtKatNr_TextChanged);
            // 
            // frmKatalogNummer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(399, 74);
            this.ControlBox = false;
            this.Controls.Add(this.txtKatNr);
            this.Controls.Add(this.lblCoinbookNummer);
            this.Controls.Add(this.lblKatalognummer);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmKatalogNummer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Eigene Katalognummer bearbeiten";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnClose;
		private System.Windows.Forms.ToolStripButton btnDelete;
		private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.Label lblKatalognummer;
		private System.Windows.Forms.Label lblCoinbookNummer;
		private SAN.Control.TextBoxEx txtKatNr;
    }
}