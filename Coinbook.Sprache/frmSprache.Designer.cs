namespace Coinbook.Sprache
{
  partial class frmSprache
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
      this.cboSprache = new System.Windows.Forms.ComboBox();
      this.btnWeiter = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cboSprache
      // 
      this.cboSprache.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cboSprache.FormattingEnabled = true;
      this.cboSprache.Location = new System.Drawing.Point(12, 12);
      this.cboSprache.Name = "cboSprache";
      this.cboSprache.Size = new System.Drawing.Size(252, 21);
      this.cboSprache.TabIndex = 15;
      this.cboSprache.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboSprache_DrawItem);
      this.cboSprache.SelectedIndexChanged += new System.EventHandler(this.cboSprache_SelectedIndexChanged);
      // 
      // btnWeiter
      // 
      this.btnWeiter.Location = new System.Drawing.Point(207, 50);
      this.btnWeiter.Name = "btnWeiter";
      this.btnWeiter.Size = new System.Drawing.Size(60, 27);
      this.btnWeiter.TabIndex = 16;
      this.btnWeiter.Text = "Weiter";
      this.btnWeiter.UseVisualStyleBackColor = true;
      this.btnWeiter.Click += new System.EventHandler(this.btnWeiter_Click);
      // 
      // frmSprache
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(279, 89);
      this.ControlBox = false;
      this.Controls.Add(this.btnWeiter);
      this.Controls.Add(this.cboSprache);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "frmSprache";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Bitte wählen Sie Ihre gewünsche Sprache aus";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cboSprache;
    private System.Windows.Forms.Button btnWeiter;
  }
}