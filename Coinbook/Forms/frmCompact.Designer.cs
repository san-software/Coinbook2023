namespace Coinbook
{
  partial class frmCompact
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.prgProgress = new System.Windows.Forms.ProgressBar();
      this.bgwWorker = new System.ComponentModel.BackgroundWorker();
      this.SuspendLayout();
      // 
      // prgProgress
      // 
      this.prgProgress.Location = new System.Drawing.Point(12, 12);
      this.prgProgress.Name = "prgProgress";
      this.prgProgress.Size = new System.Drawing.Size(351, 23);
      this.prgProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.prgProgress.TabIndex = 1;
      // 
      // bgwWorker
      // 
      this.bgwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwWorker_DoWork);
      this.bgwWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwWorker_RunWorkerCompleted);
      // 
      // frmCompact
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(373, 54);
      this.ControlBox = false;
      this.Controls.Add(this.prgProgress);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "frmCompact";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      this.TopMost = true;
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar prgProgress;
    private System.ComponentModel.BackgroundWorker bgwWorker;
  }
}

