using System;
using System.Windows.Forms;

namespace Coinbook
{
	partial class frmPasswort
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelEx1 = new System.Windows.Forms.Label();
            this.txtPasswort = new SAN.Control.TextBoxEx();
            this.btnCancel = new SAN.Control.ButtonEx();
            this.btnOK = new SAN.Control.ButtonEx();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelEx1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPasswort, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(327, 130);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEx1.Location = new System.Drawing.Point(3, 25);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(94, 25);
            this.labelEx1.TabIndex = 0;
            this.labelEx1.Text = "Passwort";
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPasswort
            // 
            this.txtPasswort.AcceptsReturn = true;
            this.txtPasswort.AcceptsTab = true;
            this.txtPasswort.BackColor = System.Drawing.Color.White;
            this.txtPasswort.Column = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.txtPasswort, 2);
            this.txtPasswort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPasswort.Enter2Tab = true;
            this.txtPasswort.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPasswort.IsPflichtfeld = false;
            this.txtPasswort.Location = new System.Drawing.Point(103, 28);
            this.txtPasswort.MaxLength = 30;
            this.txtPasswort.NachkommaStellen = ((short)(0));
            this.txtPasswort.Name = "txtPasswort";
            this.txtPasswort.NumberFormat = null;
            this.txtPasswort.OldText = "textBoxEx1";
            this.txtPasswort.PasswordChar = '*';
            this.txtPasswort.RegularExpression = "";
            this.txtPasswort.Row = 0;
            this.txtPasswort.ShowClipBoard = true;
            this.txtPasswort.Size = new System.Drawing.Size(194, 20);
            this.txtPasswort.TabIndex = 2;
            this.txtPasswort.Translation = "";
            this.txtPasswort.Typ = SAN.Control.TextBoxTyp.Text;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageStretch = false;
            this.btnCancel.Location = new System.Drawing.Point(103, 78);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 19);
            this.btnCancel.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnCancel.Stretched = false;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Abbruch";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.ImageStretch = false;
            this.btnOK.Location = new System.Drawing.Point(203, 78);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 19);
            this.btnOK.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnOK.Stretched = false;
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmPasswort
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(327, 130);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPasswort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = Application.ProductName;
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label labelEx1;
		private SAN.Control.ButtonEx btnCancel;
		private SAN.Control.TextBoxEx txtPasswort;
		private SAN.Control.ButtonEx btnOK;
	}
}