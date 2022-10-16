using System;
namespace Coinbook
{
  partial class frmLizenz
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
            this.txtLizenz = new SAN.Control.TextBoxEx();
            this.lblLizenztext = new System.Windows.Forms.Label();
            this.btnOK = new SAN.Control.ButtonEx();
            this.btnWeiter = new SAN.Control.ButtonEx();
            this.pgbBar = new System.Windows.Forms.ProgressBar();
            this.lblAnzeige = new System.Windows.Forms.Label();
            this.btnCancel = new SAN.Control.ButtonEx();
            this.SuspendLayout();
            // 
            // txtLizenz
            // 
            this.txtLizenz.AcceptsReturn = true;
            this.txtLizenz.AcceptsTab = true;
            this.txtLizenz.BackColor = System.Drawing.Color.White;
            this.txtLizenz.Column = 0;
            this.txtLizenz.Enter2Tab = true;
            this.txtLizenz.ForeColor = System.Drawing.Color.Black;
            this.txtLizenz.IsPflichtfeld = false;
            this.txtLizenz.Location = new System.Drawing.Point(24, 54);
            this.txtLizenz.MaxLength = 36;
            this.txtLizenz.NachkommaStellen = ((short)(0));
            this.txtLizenz.Name = "txtLizenz";
            this.txtLizenz.NumberFormat = null;
            this.txtLizenz.OldText = "textBoxEx1";
            this.txtLizenz.RegularExpression = "";
            this.txtLizenz.Row = 0;
            this.txtLizenz.ShowClipBoard = true;
            this.txtLizenz.Size = new System.Drawing.Size(277, 20);
            this.txtLizenz.TabIndex = 1;
            this.txtLizenz.Translation = "";
            this.txtLizenz.Typ = SAN.Control.TextBoxTyp.Text;
            this.txtLizenz.TextChanged += new System.EventHandler(this.txtLizenz_TextChanged);
            // 
            // lblLizenztext
            // 
            this.lblLizenztext.Location = new System.Drawing.Point(21, 21);
            this.lblLizenztext.Name = "lblLizenztext";
            this.lblLizenztext.Size = new System.Drawing.Size(280, 30);
            this.lblLizenztext.TabIndex = 2;
            this.lblLizenztext.Text = "Bitte geben Sie hier Ihre Lizenznummer für Coinbook ein. ";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.ImageStretch = false;
            this.btnOK.Location = new System.Drawing.Point(101, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(132, 23);
            this.btnOK.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnOK.Stretched = false;
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Lizenz installieren";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnWeiter
            // 
            this.btnWeiter.BackColor = System.Drawing.SystemColors.Control;
            this.btnWeiter.Enabled = false;
            this.btnWeiter.ForeColor = System.Drawing.Color.Black;
            this.btnWeiter.ImageStretch = false;
            this.btnWeiter.Location = new System.Drawing.Point(101, 188);
            this.btnWeiter.Name = "btnWeiter";
            this.btnWeiter.Size = new System.Drawing.Size(132, 25);
            this.btnWeiter.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnWeiter.Stretched = false;
            this.btnWeiter.TabIndex = 4;
            this.btnWeiter.Text = "Weiter";
            this.btnWeiter.UseVisualStyleBackColor = false;
            this.btnWeiter.Click += new System.EventHandler(this.btnWeiter_Click);
            // 
            // pgbBar
            // 
            this.pgbBar.Location = new System.Drawing.Point(24, 80);
            this.pgbBar.Name = "pgbBar";
            this.pgbBar.Size = new System.Drawing.Size(277, 24);
            this.pgbBar.TabIndex = 5;
            // 
            // lblAnzeige
            // 
            this.lblAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnzeige.Location = new System.Drawing.Point(24, 145);
            this.lblAnzeige.Name = "lblAnzeige";
            this.lblAnzeige.Size = new System.Drawing.Size(277, 30);
            this.lblAnzeige.TabIndex = 6;
            this.lblAnzeige.Text = "Keine Lizenz gefunden";
            this.lblAnzeige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageStretch = false;
            this.btnCancel.Location = new System.Drawing.Point(101, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(132, 25);
            this.btnCancel.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnCancel.Stretched = false;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Abbruch";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmLizenz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(329, 256);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblAnzeige);
            this.Controls.Add(this.pgbBar);
            this.Controls.Add(this.btnWeiter);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblLizenztext);
            this.Controls.Add(this.txtLizenz);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(345, 294);
            this.MinimumSize = new System.Drawing.Size(345, 294);
            this.Name = "frmLizenz";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coinbook lizenzieren";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private SAN.Control.TextBoxEx txtLizenz;
    private System.Windows.Forms.Label lblLizenztext;
    private SAN.Control.ButtonEx btnOK;
    private SAN.Control.ButtonEx btnWeiter;
    private System.Windows.Forms.ProgressBar pgbBar;
    private System.Windows.Forms.Label lblAnzeige;
    private SAN.Control.ButtonEx btnCancel;
  }
}