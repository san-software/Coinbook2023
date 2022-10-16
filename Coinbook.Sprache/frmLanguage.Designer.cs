namespace Coinbook.Sprache
{
    partial class frmProgress
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
            this.lblAnzeige = new System.Windows.Forms.Label();
            this.bgwLanguage = new SAN.Control.BackgroundWorkerEx();
            this.pgbProgress = new SAN.Control.ProgressBarEx();
            this.SuspendLayout();
            // 
            // lblAnzeige
            // 
            this.lblAnzeige.AutoSize = true;
            this.lblAnzeige.Location = new System.Drawing.Point(12, 18);
            this.lblAnzeige.Name = "lblAnzeige";
            this.lblAnzeige.Size = new System.Drawing.Size(47, 13);
            this.lblAnzeige.TabIndex = 1;
            this.lblAnzeige.Text = "labelEx1";
            // 
            // bgwLanguage
            // 
            this.bgwLanguage.Max = 0;
            this.bgwLanguage.Tag = null;
            this.bgwLanguage.Text = null;
            this.bgwLanguage.WorkerReportsProgress = true;
            this.bgwLanguage.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLanguage_DoWork);
            this.bgwLanguage.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwLanguage_ProgressChanged);
            this.bgwLanguage.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLanguage_RunWorkerCompleted);
            // 
            // pgbProgress
            // 
            this.pgbProgress.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pgbProgress.BlockDistance = ((byte)(0));
            this.pgbProgress.BlockWidth = ((byte)(1));
            this.pgbProgress.BorderColor = System.Drawing.Color.White;
            this.pgbProgress.GradientStyle = SAN.Control.ProgressBarEx.GradientMode.None;
            this.pgbProgress.Location = new System.Drawing.Point(15, 43);
            this.pgbProgress.MaxValue = 100;
            this.pgbProgress.MinValue = 0;
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(398, 24);
            this.pgbProgress.TabIndex = 2;
            this.pgbProgress.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pgbProgress.TextShadow = false;
            this.pgbProgress.Value = 50;
            // 
            // frmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 88);
            this.Controls.Add(this.pgbProgress);
            this.Controls.Add(this.lblAnzeige);
            this.Name = "frmProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Umstellen der Sprache";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAnzeige;
        private SAN.Control.BackgroundWorkerEx bgwLanguage;
        private SAN.Control.ProgressBarEx pgbProgress;
    }    
}

