namespace Coinbook
{
	partial class frmMessage
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
			this.labelEx1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.labelEx2 = new System.Windows.Forms.Label();
			this.bgw = new System.ComponentModel.BackgroundWorker();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelEx1
			// 
			this.labelEx1.BackColor = System.Drawing.Color.Transparent;
			this.labelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelEx1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelEx1.Location = new System.Drawing.Point(3, 0);
			this.labelEx1.Name = "labelEx1";
			this.labelEx1.Size = new System.Drawing.Size(373, 132);
			this.labelEx1.TabIndex = 0;
			this.labelEx1.Text = "Es läuft noch ein Modul-Update";
			this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.labelEx2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelEx1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(379, 232);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// labelEx2
			// 
			this.labelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelEx2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelEx2.Location = new System.Drawing.Point(3, 132);
			this.labelEx2.Name = "labelEx2";
			this.labelEx2.Size = new System.Drawing.Size(373, 100);
			this.labelEx2.TabIndex = 2;
			this.labelEx2.Text = "Bitte warten ...";
			this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// bgw
			// 
			this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
			this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
			// 
			// frmMessage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Moccasin;
			this.ClientSize = new System.Drawing.Size(379, 232);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmMessage";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelEx1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label labelEx2;
		private System.ComponentModel.BackgroundWorker bgw;
	}
}