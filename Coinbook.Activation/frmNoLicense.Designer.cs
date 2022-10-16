using System;
namespace Coinbook.Activation
{
	partial class frmNoLicense
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNoLicense));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.optPC1 = new System.Windows.Forms.RadioButton();
			this.optBuy = new System.Windows.Forms.RadioButton();
			this.optPC2 = new System.Windows.Forms.RadioButton();
			this.optActivate = new System.Windows.Forms.RadioButton();
			this.optPC3 = new System.Windows.Forms.RadioButton();
			this.txtAnzeige = new SAN.Control.TextBoxEx();
			this.btnClose = new SAN.Control.ButtonEx();
			this.lblGrund = new System.Windows.Forms.Label();
			this.btnWork = new System.Windows.Forms.Button();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.txtBegründung = new System.Windows.Forms.TextBox();
			this.lblKommentar = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 313F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 413F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtAnzeige, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnClose, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.lblGrund, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnWork, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(726, 511);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.optPC1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.optBuy, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.optPC2, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.optActivate, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.optPC3, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 258);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 6;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(307, 219);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// optPC1
			// 
			this.optPC1.AutoSize = true;
			this.optPC1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optPC1.Location = new System.Drawing.Point(3, 3);
			this.optPC1.Name = "optPC1";
			this.optPC1.Size = new System.Drawing.Size(301, 24);
			this.optPC1.TabIndex = 0;
			this.optPC1.TabStop = true;
			this.optPC1.Text = "1. Neuer PC";
			this.optPC1.UseVisualStyleBackColor = true;
			this.optPC1.CheckedChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// optBuy
			// 
			this.optBuy.AutoSize = true;
			this.optBuy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optBuy.Location = new System.Drawing.Point(3, 123);
			this.optBuy.Name = "optBuy";
			this.optBuy.Size = new System.Drawing.Size(301, 24);
			this.optBuy.TabIndex = 4;
			this.optBuy.TabStop = true;
			this.optBuy.Text = "Coinbook kaufen";
			this.optBuy.UseVisualStyleBackColor = true;
			this.optBuy.CheckedChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// optPC2
			// 
			this.optPC2.AutoSize = true;
			this.optPC2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optPC2.Location = new System.Drawing.Point(3, 33);
			this.optPC2.Name = "optPC2";
			this.optPC2.Size = new System.Drawing.Size(301, 24);
			this.optPC2.TabIndex = 3;
			this.optPC2.TabStop = true;
			this.optPC2.Text = "Installation auf einem 2. PC, Notebook oder Tablet";
			this.optPC2.UseVisualStyleBackColor = true;
			this.optPC2.CheckedChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// optActivate
			// 
			this.optActivate.AutoSize = true;
			this.optActivate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optActivate.Location = new System.Drawing.Point(3, 93);
			this.optActivate.Name = "optActivate";
			this.optActivate.Size = new System.Drawing.Size(301, 24);
			this.optActivate.TabIndex = 1;
			this.optActivate.TabStop = true;
			this.optActivate.Text = "Erneute Aktivierung beantragen";
			this.optActivate.UseVisualStyleBackColor = true;
			this.optActivate.CheckedChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// optPC3
			// 
			this.optPC3.AutoSize = true;
			this.optPC3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optPC3.Location = new System.Drawing.Point(3, 63);
			this.optPC3.Name = "optPC3";
			this.optPC3.Size = new System.Drawing.Size(301, 24);
			this.optPC3.TabIndex = 2;
			this.optPC3.TabStop = true;
			this.optPC3.Text = "Installation auf einem 3. PC, Notebook oder Tablet";
			this.optPC3.UseVisualStyleBackColor = true;
			this.optPC3.CheckedChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// txtAnzeige
			// 
			this.txtAnzeige.AcceptsReturn = true;
			this.txtAnzeige.AcceptsTab = true;
			this.txtAnzeige.BackColor = System.Drawing.Color.White;
			this.txtAnzeige.Column = 0;
			this.tableLayoutPanel1.SetColumnSpan(this.txtAnzeige, 2);
			this.txtAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtAnzeige.Enter2Tab = true;
			this.txtAnzeige.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtAnzeige.IsPflichtfeld = false;
			this.txtAnzeige.Location = new System.Drawing.Point(3, 3);
			this.txtAnzeige.Multiline = true;
			this.txtAnzeige.NachkommaStellen = ((short)(0));
			this.txtAnzeige.Name = "txtAnzeige";
			this.txtAnzeige.NumberFormat = null;
			this.txtAnzeige.OldText = String.Empty;
			this.txtAnzeige.ReadOnly = true;
			this.txtAnzeige.RegularExpression = String.Empty;
			this.txtAnzeige.Row = 0;
			this.txtAnzeige.ShowClipBoard = true;
			this.txtAnzeige.Size = new System.Drawing.Size(720, 219);
			this.txtAnzeige.TabIndex = 0;
			this.txtAnzeige.Text = resources.GetString("txtAnzeige.Text");
			this.txtAnzeige.Translation = String.Empty;
			this.txtAnzeige.Typ = SAN.Control.TextBoxTyp.Text;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnClose.ForeColor = System.Drawing.Color.Black;
			this.btnClose.ImageStretch = false;
			this.btnClose.Location = new System.Drawing.Point(3, 483);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(116, 22);
			this.btnClose.Status = SAN.Control.ButtonStatus.Nothing;
			this.btnClose.Stretched = false;
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Abbrechen";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblGrund
			// 
			this.lblGrund.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.lblGrund, 2);
			this.lblGrund.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblGrund.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGrund.Location = new System.Drawing.Point(3, 225);
			this.lblGrund.Name = "lblGrund";
			this.lblGrund.Size = new System.Drawing.Size(720, 30);
			this.lblGrund.TabIndex = 5;
			this.lblGrund.Text = "Bitte wählen Sie den Grund für eine neue Aktivierung";
			this.lblGrund.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnWork
			// 
			this.btnWork.Location = new System.Drawing.Point(316, 483);
			this.btnWork.Name = "btnWork";
			this.btnWork.Size = new System.Drawing.Size(407, 25);
			this.btnWork.TabIndex = 6;
			this.btnWork.Text = "Bestellung ausführen";
			this.btnWork.UseVisualStyleBackColor = true;
			this.btnWork.Click += new System.EventHandler(this.btnWork_Click);
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.Controls.Add(this.txtBegründung, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.lblKommentar, 0, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(316, 258);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(407, 219);
			this.tableLayoutPanel4.TabIndex = 7;
			// 
			// txtBegründung
			// 
			this.txtBegründung.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBegründung.Location = new System.Drawing.Point(3, 33);
			this.txtBegründung.Multiline = true;
			this.txtBegründung.Name = "txtBegründung";
			this.txtBegründung.Size = new System.Drawing.Size(401, 183);
			this.txtBegründung.TabIndex = 3;
			this.txtBegründung.TextChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// lblKommentar
			// 
			this.lblKommentar.AutoSize = true;
			this.lblKommentar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblKommentar.Location = new System.Drawing.Point(3, 0);
			this.lblKommentar.Name = "lblKommentar";
			this.lblKommentar.Size = new System.Drawing.Size(401, 30);
			this.lblKommentar.TabIndex = 4;
			this.lblKommentar.Text = "Begründung / Kommentar";
			this.lblKommentar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// frmNoLicense
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(726, 511);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmNoLicense";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "frmNoLicense";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SAN.Control.TextBoxEx txtAnzeige;
		private SAN.Control.ButtonEx btnClose;
		private System.Windows.Forms.RadioButton optBuy;
		private System.Windows.Forms.RadioButton optPC2;
		private System.Windows.Forms.RadioButton optPC3;
		private System.Windows.Forms.RadioButton optActivate;
		private System.Windows.Forms.RadioButton optPC1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TextBox txtBegründung;
		private System.Windows.Forms.Label lblKommentar;
		private System.Windows.Forms.Label lblGrund;
		private System.Windows.Forms.Button btnWork;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
	}
}