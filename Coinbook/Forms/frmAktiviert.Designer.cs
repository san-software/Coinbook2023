using System;
namespace Coinbook
{
  partial class frmAktiviert
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
			this.btnOK = new System.Windows.Forms.Button();
			this.txtAnzeige = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.SystemColors.Control;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.ForeColor = System.Drawing.Color.Black;
			this.btnOK.Location = new System.Drawing.Point(189, 214);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(143, 38);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtAnzeige
			// 
			this.txtAnzeige.AcceptsReturn = true;
			this.txtAnzeige.AcceptsTab = true;
			this.txtAnzeige.BackColor = System.Drawing.Color.White;
			this.txtAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAnzeige.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtAnzeige.Location = new System.Drawing.Point(22, 12);
			this.txtAnzeige.Multiline = true;
			this.txtAnzeige.Name = "txtAnzeige";
			this.txtAnzeige.ReadOnly = true;
			this.txtAnzeige.Size = new System.Drawing.Size(499, 147);
			this.txtAnzeige.TabIndex = 1;
			this.txtAnzeige.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frmAktiviert
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(533, 283);
			this.ControlBox = false;
			this.Controls.Add(this.txtAnzeige);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAktiviert";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox txtAnzeige;
  }
}