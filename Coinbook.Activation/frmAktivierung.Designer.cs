namespace Coinbook.Activation
{
	partial class frmAktivierung
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
            this.btnAktivate = new SAN.Control.ButtonEx();
            this.btnCancel = new SAN.Control.ButtonEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ctlEigneEinstellungen = new usrEigeneEinst();
            this.lblAnzeige = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAktivate
            // 
            this.btnAktivate.BackColor = System.Drawing.SystemColors.Control;
            this.btnAktivate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAktivate.ForeColor = System.Drawing.Color.Black;
            this.btnAktivate.ImageStretch = false;
            this.btnAktivate.Location = new System.Drawing.Point(3, 347);
            this.btnAktivate.Name = "btnAktivate";
            this.btnAktivate.Size = new System.Drawing.Size(194, 24);
            this.btnAktivate.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnAktivate.Stretched = false;
            this.btnAktivate.TabIndex = 1;
            this.btnAktivate.Text = "Aktivierung senden";
            this.btnAktivate.UseVisualStyleBackColor = false;
            this.btnAktivate.Click += new System.EventHandler(this.btnAktivate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageStretch = false;
            this.btnCancel.Location = new System.Drawing.Point(203, 347);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(194, 24);
            this.btnCancel.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnCancel.Stretched = false;
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Abbruch";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Controls.Add(this.ctlEigneEinstellungen, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAktivate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAnzeige, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(822, 374);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // ctlEigneEinstellungen
            // 
            this.ctlEigneEinstellungen.BackColor = System.Drawing.Color.LightGray;
            this.tableLayoutPanel1.SetColumnSpan(this.ctlEigneEinstellungen, 2);
            this.ctlEigneEinstellungen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlEigneEinstellungen.Location = new System.Drawing.Point(3, 3);
            this.ctlEigneEinstellungen.Name = "ctlEigneEinstellungen";
            this.ctlEigneEinstellungen.Size = new System.Drawing.Size(394, 338);
            this.ctlEigneEinstellungen.TabIndex = 0;
            // 
            // lblAnzeige
            // 
            this.lblAnzeige.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAnzeige, 3);
            this.lblAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnzeige.Location = new System.Drawing.Point(403, 0);
            this.lblAnzeige.Name = "lblAnzeige";
            this.lblAnzeige.Size = new System.Drawing.Size(411, 344);
            this.lblAnzeige.TabIndex = 3;
            this.lblAnzeige.Text = "labelEx1";
            this.lblAnzeige.Visible = false;
            // 
            // frmAktivierung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 374);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmAktivierung";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aktivierung";
            this.Shown += new System.EventHandler(this.frmAktivierung_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private usrEigeneEinst ctlEigneEinstellungen;
		private SAN.Control.ButtonEx btnAktivate;
		private SAN.Control.ButtonEx btnCancel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblAnzeige;
	}
}