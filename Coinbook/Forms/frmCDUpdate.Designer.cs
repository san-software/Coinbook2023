namespace Coinbook
{
  partial class frmCDUpdate
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
      this.lstDrives = new System.Windows.Forms.ListView();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lstDrives
      // 
      this.lstDrives.Location = new System.Drawing.Point(12, 12);
      this.lstDrives.Name = "lstDrives";
      this.lstDrives.Size = new System.Drawing.Size(170, 198);
      this.lstDrives.TabIndex = 0;
      this.lstDrives.UseCompatibleStateImageBehavior = false;
      this.lstDrives.View = System.Windows.Forms.View.List;
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(390, 82);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(124, 28);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "Auswählen";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(390, 132);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(123, 29);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Abbruch";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // frmCDUpdate
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 550);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.lstDrives);
      this.Name = "frmCDUpdate";
      this.Text = "frmCDUpdate";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lstDrives;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
  }
}