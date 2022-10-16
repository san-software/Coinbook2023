using System;

namespace Coinbook
{
    partial class frmMünze
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMünze));
            this.lblMuenzTyp = new System.Windows.Forms.Label();
            this.lblErhaltungsgrad = new System.Windows.Forms.Label();
            this.lblAblage = new System.Windows.Forms.Label();
            this.lblKaufdatum = new System.Windows.Forms.Label();
            this.lblKaufort = new System.Windows.Forms.Label();
            this.lblVerkäufer = new System.Windows.Forms.Label();
            this.lblPreis = new System.Windows.Forms.Label();
            this.lblKommentar = new System.Windows.Forms.Label();
            this.lblKatNrEigen = new System.Windows.Forms.Label();
            this.lblWährung = new System.Windows.Forms.Label();
            this.lblStück = new System.Windows.Forms.Label();
            this.chkFehlerhaft = new System.Windows.Forms.CheckBox();
            this.lblGebiet = new System.Windows.Forms.Label();
            this.lblGebietText = new System.Windows.Forms.Label();
            this.lblMünze = new System.Windows.Forms.Label();
            this.lblMünzeText = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.lblWährung1 = new System.Windows.Forms.Label();
            this.lblEigenerPreis = new System.Windows.Forms.Label();
            this.lblWaehrung2 = new System.Windows.Forms.Label();
            this.lblKatalogpreis = new System.Windows.Forms.Label();
            this.txtKatalogpreis = new SAN.Control.TextBoxEx();
            this.txtEigenerPreis = new SAN.Control.TextBoxEx();
            this.txtKaufpreis = new SAN.Control.TextBoxEx();
            this.txtFehlerNotiz = new SAN.Control.TextBoxEx();
            this.txtAnzahl = new SAN.Control.TextBoxEx();
            this.txtKommentar = new SAN.Control.TextBoxEx();
            this.txtKatNrEigen = new SAN.Control.TextBoxEx();
            this.txtVerkäufer = new SAN.Control.TextBoxEx();
            this.txtKaufort = new SAN.Control.TextBoxEx();
            this.txtAblage = new SAN.Control.TextBoxEx();
            this.cboErhaltungsgrad = new SAN.Control.ComboBoxEx();
            this.txtKaufdatum = new SAN.Control.TextBoxEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkDoublette = new SAN.Control.CheckBoxEx();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMuenzTyp
            // 
            this.lblMuenzTyp.AutoSize = true;
            this.lblMuenzTyp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMuenzTyp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMuenzTyp.Location = new System.Drawing.Point(3, 65);
            this.lblMuenzTyp.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblMuenzTyp.Name = "lblMuenzTyp";
            this.lblMuenzTyp.Size = new System.Drawing.Size(94, 20);
            this.lblMuenzTyp.TabIndex = 14;
            this.lblMuenzTyp.Text = "Doublette";
            // 
            // lblErhaltungsgrad
            // 
            this.lblErhaltungsgrad.AutoSize = true;
            this.lblErhaltungsgrad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErhaltungsgrad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErhaltungsgrad.Location = new System.Drawing.Point(3, 115);
            this.lblErhaltungsgrad.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblErhaltungsgrad.Name = "lblErhaltungsgrad";
            this.lblErhaltungsgrad.Size = new System.Drawing.Size(94, 20);
            this.lblErhaltungsgrad.TabIndex = 16;
            this.lblErhaltungsgrad.Text = "Erhaltungsgrad";
            // 
            // lblAblage
            // 
            this.lblAblage.AutoSize = true;
            this.lblAblage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAblage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAblage.Location = new System.Drawing.Point(3, 140);
            this.lblAblage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblAblage.Name = "lblAblage";
            this.lblAblage.Size = new System.Drawing.Size(94, 20);
            this.lblAblage.TabIndex = 17;
            this.lblAblage.Text = "Ablage";
            // 
            // lblKaufdatum
            // 
            this.lblKaufdatum.AutoSize = true;
            this.lblKaufdatum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKaufdatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKaufdatum.Location = new System.Drawing.Point(3, 165);
            this.lblKaufdatum.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblKaufdatum.Name = "lblKaufdatum";
            this.lblKaufdatum.Size = new System.Drawing.Size(94, 20);
            this.lblKaufdatum.TabIndex = 18;
            this.lblKaufdatum.Text = "Wann gekauft";
            // 
            // lblKaufort
            // 
            this.lblKaufort.AutoSize = true;
            this.lblKaufort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKaufort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKaufort.Location = new System.Drawing.Point(3, 190);
            this.lblKaufort.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblKaufort.Name = "lblKaufort";
            this.lblKaufort.Size = new System.Drawing.Size(94, 20);
            this.lblKaufort.TabIndex = 19;
            this.lblKaufort.Text = "Kaufort";
            // 
            // lblVerkäufer
            // 
            this.lblVerkäufer.AutoSize = true;
            this.lblVerkäufer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVerkäufer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerkäufer.Location = new System.Drawing.Point(3, 215);
            this.lblVerkäufer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblVerkäufer.Name = "lblVerkäufer";
            this.lblVerkäufer.Size = new System.Drawing.Size(94, 20);
            this.lblVerkäufer.TabIndex = 20;
            this.lblVerkäufer.Text = "Verkäufer";
            // 
            // lblPreis
            // 
            this.lblPreis.AutoSize = true;
            this.lblPreis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreis.Location = new System.Drawing.Point(3, 240);
            this.lblPreis.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblPreis.Name = "lblPreis";
            this.lblPreis.Size = new System.Drawing.Size(94, 20);
            this.lblPreis.TabIndex = 21;
            this.lblPreis.Text = "Kaufpreis";
            // 
            // lblKommentar
            // 
            this.lblKommentar.AutoSize = true;
            this.lblKommentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKommentar.Location = new System.Drawing.Point(431, 65);
            this.lblKommentar.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblKommentar.Name = "lblKommentar";
            this.lblKommentar.Size = new System.Drawing.Size(60, 13);
            this.lblKommentar.TabIndex = 7;
            this.lblKommentar.Text = "Kommentar";
            // 
            // lblKatNrEigen
            // 
            this.lblKatNrEigen.AutoSize = true;
            this.lblKatNrEigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKatNrEigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKatNrEigen.Location = new System.Drawing.Point(3, 290);
            this.lblKatNrEigen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblKatNrEigen.Name = "lblKatNrEigen";
            this.lblKatNrEigen.Size = new System.Drawing.Size(94, 20);
            this.lblKatNrEigen.TabIndex = 23;
            this.lblKatNrEigen.Text = "Katalognummer";
            // 
            // lblWährung
            // 
            this.lblWährung.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblWährung, 2);
            this.lblWährung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWährung.Location = new System.Drawing.Point(209, 240);
            this.lblWährung.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblWährung.Name = "lblWährung";
            this.lblWährung.Size = new System.Drawing.Size(83, 20);
            this.lblWährung.TabIndex = 18;
            this.lblWährung.Text = "€";
            // 
            // lblStück
            // 
            this.lblStück.AutoSize = true;
            this.lblStück.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStück.Location = new System.Drawing.Point(3, 90);
            this.lblStück.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblStück.Name = "lblStück";
            this.lblStück.Size = new System.Drawing.Size(94, 20);
            this.lblStück.TabIndex = 15;
            this.lblStück.Text = "Anzahl";
            // 
            // chkFehlerhaft
            // 
            this.chkFehlerhaft.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.chkFehlerhaft, 2);
            this.chkFehlerhaft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFehlerhaft.Location = new System.Drawing.Point(431, 188);
            this.chkFehlerhaft.Name = "chkFehlerhaft";
            this.chkFehlerhaft.Size = new System.Drawing.Size(108, 17);
            this.chkFehlerhaft.TabIndex = 12;
            this.chkFehlerhaft.Text = "Münze fehlerhaft:";
            this.chkFehlerhaft.UseVisualStyleBackColor = true;
            this.chkFehlerhaft.CheckedChanged += new System.EventHandler(this.chkFehlerhaft_CheckedChanged);
            // 
            // lblGebiet
            // 
            this.lblGebiet.AutoSize = true;
            this.lblGebiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGebiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGebiet.Location = new System.Drawing.Point(3, 15);
            this.lblGebiet.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblGebiet.Name = "lblGebiet";
            this.lblGebiet.Size = new System.Drawing.Size(94, 20);
            this.lblGebiet.TabIndex = 28;
            this.lblGebiet.Text = "Gebiet";
            // 
            // lblGebietText
            // 
            this.lblGebietText.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblGebietText, 8);
            this.lblGebietText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGebietText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGebietText.Location = new System.Drawing.Point(103, 15);
            this.lblGebietText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblGebietText.Name = "lblGebietText";
            this.lblGebietText.Size = new System.Drawing.Size(632, 20);
            this.lblGebietText.TabIndex = 29;
            this.lblGebietText.Text = "Gebiet";
            // 
            // lblMünze
            // 
            this.lblMünze.AutoSize = true;
            this.lblMünze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMünze.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMünze.Location = new System.Drawing.Point(3, 40);
            this.lblMünze.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblMünze.Name = "lblMünze";
            this.lblMünze.Size = new System.Drawing.Size(94, 20);
            this.lblMünze.TabIndex = 30;
            this.lblMünze.Text = "Münze";
            // 
            // lblMünzeText
            // 
            this.lblMünzeText.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblMünzeText, 8);
            this.lblMünzeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMünzeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMünzeText.Location = new System.Drawing.Point(103, 40);
            this.lblMünzeText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblMünzeText.Name = "lblMünzeText";
            this.lblMünzeText.Size = new System.Drawing.Size(632, 20);
            this.lblMünzeText.TabIndex = 31;
            this.lblMünzeText.Text = "Münze";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(746, 25);
            this.toolStrip1.TabIndex = 34;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "Schliessen";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Speicherm";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblWährung1
            // 
            this.lblWährung1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblWährung1, 2);
            this.lblWährung1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWährung1.Location = new System.Drawing.Point(209, 265);
            this.lblWährung1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblWährung1.Name = "lblWährung1";
            this.lblWährung1.Size = new System.Drawing.Size(83, 20);
            this.lblWährung1.TabIndex = 36;
            this.lblWährung1.Text = "€";
            // 
            // lblEigenerPreis
            // 
            this.lblEigenerPreis.AutoSize = true;
            this.lblEigenerPreis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEigenerPreis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEigenerPreis.Location = new System.Drawing.Point(3, 265);
            this.lblEigenerPreis.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblEigenerPreis.Name = "lblEigenerPreis";
            this.lblEigenerPreis.Size = new System.Drawing.Size(94, 20);
            this.lblEigenerPreis.TabIndex = 22;
            this.lblEigenerPreis.Text = "Eigener Kat. Preis";
            // 
            // lblWaehrung2
            // 
            this.lblWaehrung2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblWaehrung2, 2);
            this.lblWaehrung2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWaehrung2.Location = new System.Drawing.Point(209, 315);
            this.lblWaehrung2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblWaehrung2.Name = "lblWaehrung2";
            this.lblWaehrung2.Size = new System.Drawing.Size(83, 20);
            this.lblWaehrung2.TabIndex = 39;
            this.lblWaehrung2.Text = "€";
            // 
            // lblKatalogpreis
            // 
            this.lblKatalogpreis.AutoSize = true;
            this.lblKatalogpreis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKatalogpreis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKatalogpreis.Location = new System.Drawing.Point(3, 315);
            this.lblKatalogpreis.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblKatalogpreis.Name = "lblKatalogpreis";
            this.lblKatalogpreis.Size = new System.Drawing.Size(94, 20);
            this.lblKatalogpreis.TabIndex = 24;
            this.lblKatalogpreis.Text = "Katalogpreis";
            // 
            // txtKatalogpreis
            // 
            this.txtKatalogpreis.AcceptsReturn = true;
            this.txtKatalogpreis.AcceptsTab = true;
            this.txtKatalogpreis.BackColor = System.Drawing.Color.White;
            this.txtKatalogpreis.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtKatalogpreis, 2);
            this.txtKatalogpreis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKatalogpreis.Enter2Tab = true;
            this.txtKatalogpreis.ForeColor = System.Drawing.Color.Black;
            this.txtKatalogpreis.IsPflichtfeld = false;
            this.txtKatalogpreis.Location = new System.Drawing.Point(103, 313);
            this.txtKatalogpreis.MaxLength = 12;
            this.txtKatalogpreis.NachkommaStellen = ((short)(2));
            this.txtKatalogpreis.Name = "txtKatalogpreis";
            this.txtKatalogpreis.NumberFormat = "";
            this.txtKatalogpreis.OldText = null;
            this.txtKatalogpreis.ReadOnly = true;
            this.txtKatalogpreis.RegularExpression = "";
            this.txtKatalogpreis.Row = 5;
            this.txtKatalogpreis.ShowClipBoard = true;
            this.txtKatalogpreis.Size = new System.Drawing.Size(100, 20);
            this.txtKatalogpreis.TabIndex = 10;
            this.txtKatalogpreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKatalogpreis.Translation = "";
            this.txtKatalogpreis.Typ = SAN.Control.TextBoxTyp.Numeric;
            // 
            // txtEigenerPreis
            // 
            this.txtEigenerPreis.AcceptsReturn = true;
            this.txtEigenerPreis.AcceptsTab = true;
            this.txtEigenerPreis.BackColor = System.Drawing.Color.White;
            this.txtEigenerPreis.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtEigenerPreis, 2);
            this.txtEigenerPreis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEigenerPreis.Enter2Tab = true;
            this.txtEigenerPreis.ForeColor = System.Drawing.Color.Black;
            this.txtEigenerPreis.IsPflichtfeld = false;
            this.txtEigenerPreis.Location = new System.Drawing.Point(103, 263);
            this.txtEigenerPreis.MaxLength = 12;
            this.txtEigenerPreis.NachkommaStellen = ((short)(2));
            this.txtEigenerPreis.Name = "txtEigenerPreis";
            this.txtEigenerPreis.NumberFormat = "";
            this.txtEigenerPreis.OldText = null;
            this.txtEigenerPreis.RegularExpression = "";
            this.txtEigenerPreis.Row = 11;
            this.txtEigenerPreis.ShowClipBoard = true;
            this.txtEigenerPreis.Size = new System.Drawing.Size(100, 20);
            this.txtEigenerPreis.TabIndex = 8;
            this.txtEigenerPreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEigenerPreis.Translation = "";
            this.txtEigenerPreis.Typ = SAN.Control.TextBoxTyp.Numeric;
            this.txtEigenerPreis.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtKaufpreis
            // 
            this.txtKaufpreis.AcceptsReturn = true;
            this.txtKaufpreis.AcceptsTab = true;
            this.txtKaufpreis.BackColor = System.Drawing.Color.White;
            this.txtKaufpreis.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtKaufpreis, 2);
            this.txtKaufpreis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKaufpreis.Enter2Tab = true;
            this.txtKaufpreis.ForeColor = System.Drawing.Color.Black;
            this.txtKaufpreis.IsPflichtfeld = false;
            this.txtKaufpreis.Location = new System.Drawing.Point(103, 238);
            this.txtKaufpreis.MaxLength = 12;
            this.txtKaufpreis.NachkommaStellen = ((short)(2));
            this.txtKaufpreis.Name = "txtKaufpreis";
            this.txtKaufpreis.NumberFormat = "";
            this.txtKaufpreis.OldText = null;
            this.txtKaufpreis.RegularExpression = "";
            this.txtKaufpreis.Row = 10;
            this.txtKaufpreis.ShowClipBoard = true;
            this.txtKaufpreis.Size = new System.Drawing.Size(100, 20);
            this.txtKaufpreis.TabIndex = 7;
            this.txtKaufpreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKaufpreis.Translation = "";
            this.txtKaufpreis.Typ = SAN.Control.TextBoxTyp.Numeric;
            this.txtKaufpreis.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtFehlerNotiz
            // 
            this.txtFehlerNotiz.AcceptsReturn = true;
            this.txtFehlerNotiz.AcceptsTab = true;
            this.txtFehlerNotiz.BackColor = System.Drawing.Color.White;
            this.txtFehlerNotiz.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtFehlerNotiz, 2);
            this.txtFehlerNotiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFehlerNotiz.Enabled = false;
            this.txtFehlerNotiz.Enter2Tab = true;
            this.txtFehlerNotiz.ForeColor = System.Drawing.Color.Black;
            this.txtFehlerNotiz.IsPflichtfeld = false;
            this.txtFehlerNotiz.Location = new System.Drawing.Point(431, 213);
            this.txtFehlerNotiz.Multiline = true;
            this.txtFehlerNotiz.NachkommaStellen = ((short)(0));
            this.txtFehlerNotiz.Name = "txtFehlerNotiz";
            this.txtFehlerNotiz.NumberFormat = null;
            this.txtFehlerNotiz.OldText = null;
            this.txtFehlerNotiz.RegularExpression = "";
            this.txtFehlerNotiz.Row = 9;
            this.tableLayoutPanel1.SetRowSpan(this.txtFehlerNotiz, 4);
            this.txtFehlerNotiz.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFehlerNotiz.ShowClipBoard = true;
            this.txtFehlerNotiz.Size = new System.Drawing.Size(304, 94);
            this.txtFehlerNotiz.TabIndex = 13;
            this.txtFehlerNotiz.Translation = "";
            this.txtFehlerNotiz.Typ = SAN.Control.TextBoxTyp.Text;
            this.txtFehlerNotiz.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtAnzahl
            // 
            this.txtAnzahl.AcceptsReturn = true;
            this.txtAnzahl.AcceptsTab = true;
            this.txtAnzahl.BackColor = System.Drawing.Color.White;
            this.txtAnzahl.Column = 0;
            this.txtAnzahl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAnzahl.Enter2Tab = true;
            this.txtAnzahl.ForeColor = System.Drawing.Color.Black;
            this.txtAnzahl.IsPflichtfeld = false;
            this.txtAnzahl.Location = new System.Drawing.Point(103, 88);
            this.txtAnzahl.MaxLength = 4;
            this.txtAnzahl.NachkommaStellen = ((short)(0));
            this.txtAnzahl.Name = "txtAnzahl";
            this.txtAnzahl.NumberFormat = null;
            this.txtAnzahl.OldText = "";
            this.txtAnzahl.RegularExpression = "";
            this.txtAnzahl.Row = 4;
            this.txtAnzahl.ShowClipBoard = true;
            this.txtAnzahl.Size = new System.Drawing.Size(54, 20);
            this.txtAnzahl.TabIndex = 1;
            this.txtAnzahl.Text = "0";
            this.txtAnzahl.Translation = "";
            this.txtAnzahl.Typ = SAN.Control.TextBoxTyp.Numeric;
            this.txtAnzahl.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtKommentar
            // 
            this.txtKommentar.AcceptsReturn = true;
            this.txtKommentar.AcceptsTab = true;
            this.txtKommentar.BackColor = System.Drawing.Color.White;
            this.txtKommentar.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtKommentar, 2);
            this.txtKommentar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKommentar.Enter2Tab = true;
            this.txtKommentar.ForeColor = System.Drawing.Color.Black;
            this.txtKommentar.IsPflichtfeld = false;
            this.txtKommentar.Location = new System.Drawing.Point(431, 88);
            this.txtKommentar.Multiline = true;
            this.txtKommentar.NachkommaStellen = ((short)(0));
            this.txtKommentar.Name = "txtKommentar";
            this.txtKommentar.NumberFormat = null;
            this.txtKommentar.OldText = null;
            this.txtKommentar.RegularExpression = "";
            this.txtKommentar.Row = 4;
            this.tableLayoutPanel1.SetRowSpan(this.txtKommentar, 4);
            this.txtKommentar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtKommentar.ShowClipBoard = true;
            this.txtKommentar.Size = new System.Drawing.Size(304, 94);
            this.txtKommentar.TabIndex = 11;
            this.txtKommentar.Translation = "";
            this.txtKommentar.Typ = SAN.Control.TextBoxTyp.Text;
            this.txtKommentar.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtKatNrEigen
            // 
            this.txtKatNrEigen.AcceptsReturn = true;
            this.txtKatNrEigen.AcceptsTab = true;
            this.txtKatNrEigen.BackColor = System.Drawing.Color.White;
            this.txtKatNrEigen.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtKatNrEigen, 2);
            this.txtKatNrEigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKatNrEigen.Enter2Tab = true;
            this.txtKatNrEigen.ForeColor = System.Drawing.Color.Black;
            this.txtKatNrEigen.IsPflichtfeld = false;
            this.txtKatNrEigen.Location = new System.Drawing.Point(103, 288);
            this.txtKatNrEigen.NachkommaStellen = ((short)(0));
            this.txtKatNrEigen.Name = "txtKatNrEigen";
            this.txtKatNrEigen.NumberFormat = null;
            this.txtKatNrEigen.OldText = null;
            this.txtKatNrEigen.ReadOnly = true;
            this.txtKatNrEigen.RegularExpression = "";
            this.txtKatNrEigen.Row = 12;
            this.txtKatNrEigen.ShowClipBoard = true;
            this.txtKatNrEigen.Size = new System.Drawing.Size(100, 20);
            this.txtKatNrEigen.TabIndex = 9;
            this.txtKatNrEigen.Translation = "";
            this.txtKatNrEigen.Typ = SAN.Control.TextBoxTyp.Text;
            this.txtKatNrEigen.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtVerkäufer
            // 
            this.txtVerkäufer.AcceptsReturn = true;
            this.txtVerkäufer.AcceptsTab = true;
            this.txtVerkäufer.BackColor = System.Drawing.Color.White;
            this.txtVerkäufer.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtVerkäufer, 5);
            this.txtVerkäufer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVerkäufer.Enter2Tab = true;
            this.txtVerkäufer.ForeColor = System.Drawing.Color.Black;
            this.txtVerkäufer.IsPflichtfeld = false;
            this.txtVerkäufer.Location = new System.Drawing.Point(103, 213);
            this.txtVerkäufer.MaxLength = 200;
            this.txtVerkäufer.NachkommaStellen = ((short)(0));
            this.txtVerkäufer.Name = "txtVerkäufer";
            this.txtVerkäufer.NumberFormat = null;
            this.txtVerkäufer.OldText = null;
            this.txtVerkäufer.RegularExpression = "";
            this.txtVerkäufer.Row = 9;
            this.txtVerkäufer.ShowClipBoard = true;
            this.txtVerkäufer.Size = new System.Drawing.Size(302, 20);
            this.txtVerkäufer.TabIndex = 6;
            this.txtVerkäufer.Translation = "";
            this.txtVerkäufer.Typ = SAN.Control.TextBoxTyp.Text;
            this.txtVerkäufer.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtKaufort
            // 
            this.txtKaufort.AcceptsReturn = true;
            this.txtKaufort.AcceptsTab = true;
            this.txtKaufort.BackColor = System.Drawing.Color.White;
            this.txtKaufort.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtKaufort, 5);
            this.txtKaufort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKaufort.Enter2Tab = true;
            this.txtKaufort.ForeColor = System.Drawing.Color.Black;
            this.txtKaufort.IsPflichtfeld = false;
            this.txtKaufort.Location = new System.Drawing.Point(103, 188);
            this.txtKaufort.MaxLength = 200;
            this.txtKaufort.NachkommaStellen = ((short)(0));
            this.txtKaufort.Name = "txtKaufort";
            this.txtKaufort.NumberFormat = null;
            this.txtKaufort.OldText = null;
            this.txtKaufort.RegularExpression = "";
            this.txtKaufort.Row = 8;
            this.txtKaufort.ShowClipBoard = true;
            this.txtKaufort.Size = new System.Drawing.Size(302, 20);
            this.txtKaufort.TabIndex = 5;
            this.txtKaufort.Translation = "";
            this.txtKaufort.Typ = SAN.Control.TextBoxTyp.Text;
            this.txtKaufort.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtAblage
            // 
            this.txtAblage.AcceptsReturn = true;
            this.txtAblage.AcceptsTab = true;
            this.txtAblage.BackColor = System.Drawing.Color.White;
            this.txtAblage.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtAblage, 5);
            this.txtAblage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAblage.Enter2Tab = true;
            this.txtAblage.ForeColor = System.Drawing.Color.Black;
            this.txtAblage.IsPflichtfeld = false;
            this.txtAblage.Location = new System.Drawing.Point(103, 138);
            this.txtAblage.MaxLength = 200;
            this.txtAblage.NachkommaStellen = ((short)(0));
            this.txtAblage.Name = "txtAblage";
            this.txtAblage.NumberFormat = null;
            this.txtAblage.OldText = null;
            this.txtAblage.RegularExpression = "";
            this.txtAblage.Row = 6;
            this.txtAblage.ShowClipBoard = true;
            this.txtAblage.Size = new System.Drawing.Size(302, 20);
            this.txtAblage.TabIndex = 3;
            this.txtAblage.Translation = "";
            this.txtAblage.Typ = SAN.Control.TextBoxTyp.Text;
            this.txtAblage.TextChanged += new System.EventHandler(this.TextChanged);
            // 
            // cboErhaltungsgrad
            // 
            this.cboErhaltungsgrad.BackColor = System.Drawing.Color.White;
            this.cboErhaltungsgrad.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.cboErhaltungsgrad, 2);
            this.cboErhaltungsgrad.ColumnsToDisplay = "";
            this.cboErhaltungsgrad.ColumnType = SAN.Control.ComboBoxEx.ColType.SingleColumn;
            this.cboErhaltungsgrad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboErhaltungsgrad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboErhaltungsgrad.ForeColor = System.Drawing.Color.Black;
            this.cboErhaltungsgrad.FormattingEnabled = true;
            this.cboErhaltungsgrad.GridLinesMultiColumn = false;
            this.cboErhaltungsgrad.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboErhaltungsgrad.ID = ((long)(-1));
            this.cboErhaltungsgrad.IDObject = -1;
            this.cboErhaltungsgrad.IDString = "";
            this.cboErhaltungsgrad.IsPflichtfeld = false;
            this.cboErhaltungsgrad.Location = new System.Drawing.Point(103, 113);
            this.cboErhaltungsgrad.MaxDropDownItems = 10;
            this.cboErhaltungsgrad.Name = "cboErhaltungsgrad";
            this.cboErhaltungsgrad.ReadOnly = false;
            this.cboErhaltungsgrad.Row = 0;
            this.cboErhaltungsgrad.ShowClipBoard = true;
            this.cboErhaltungsgrad.Size = new System.Drawing.Size(100, 21);
            this.cboErhaltungsgrad.TabIndex = 2;
            this.cboErhaltungsgrad.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboErhaltungsgrad.SelectedIndexChanged += new System.EventHandler(this.TextChanged);
            // 
            // txtKaufdatum
            // 
            this.txtKaufdatum.AcceptsReturn = true;
            this.txtKaufdatum.AcceptsTab = true;
            this.txtKaufdatum.BackColor = System.Drawing.Color.White;
            this.txtKaufdatum.Column = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.txtKaufdatum, 2);
            this.txtKaufdatum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKaufdatum.Enter2Tab = true;
            this.txtKaufdatum.ForeColor = System.Drawing.Color.Black;
            this.txtKaufdatum.IsPflichtfeld = false;
            this.txtKaufdatum.Location = new System.Drawing.Point(103, 163);
            this.txtKaufdatum.MaxLength = 12;
            this.txtKaufdatum.NachkommaStellen = ((short)(2));
            this.txtKaufdatum.Name = "txtKaufdatum";
            this.txtKaufdatum.NumberFormat = "";
            this.txtKaufdatum.OldText = null;
            this.txtKaufdatum.RegularExpression = "";
            this.txtKaufdatum.Row = 7;
            this.txtKaufdatum.ShowClipBoard = true;
            this.txtKaufdatum.Size = new System.Drawing.Size(100, 20);
            this.txtKaufdatum.TabIndex = 4;
            this.txtKaufdatum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKaufdatum.Translation = "";
            this.txtKaufdatum.Typ = SAN.Control.TextBoxTyp.Text;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Controls.Add(this.lblGebiet, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkFehlerhaft, 7, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtKaufdatum, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblMünze, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblKommentar, 7, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblMuenzTyp, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtEigenerPreis, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblErhaltungsgrad, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblAblage, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblKaufdatum, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtKaufpreis, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblKaufort, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblEigenerPreis, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtKatNrEigen, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.lblVerkäufer, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblMünzeText, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtAnzahl, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblPreis, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtVerkäufer, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblGebietText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtKaufort, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblKatNrEigen, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.cboErhaltungsgrad, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtAblage, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblWährung, 3, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblWährung1, 3, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtFehlerNotiz, 7, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtKommentar, 7, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblStück, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblKatalogpreis, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtKatalogpreis, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.lblWaehrung2, 3, 13);
            this.tableLayoutPanel1.Controls.Add(this.chkDoublette, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(746, 370);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // chkDoublette
            // 
            this.chkDoublette.AutoSize = true;
            this.chkDoublette.BackColorCheck = System.Drawing.Color.White;
            this.chkDoublette.Changed = false;
            this.chkDoublette.Column = 0;
            this.chkDoublette.ForeColorCheck = System.Drawing.Color.Black;
            this.chkDoublette.Location = new System.Drawing.Point(103, 66);
            this.chkDoublette.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.chkDoublette.Name = "chkDoublette";
            this.chkDoublette.ReadOnly = false;
            this.chkDoublette.Row = 0;
            this.chkDoublette.Size = new System.Drawing.Size(15, 14);
            this.chkDoublette.TabIndex = 0;
            this.chkDoublette.TypChecked = System.Windows.Forms.MenuGlyph.Checkmark;
            this.chkDoublette.TypIndeterminate = System.Windows.Forms.MenuGlyph.Bullet;
            this.chkDoublette.UseVisualStyleBackColor = true;
            // 
            // frmMünze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(746, 395);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMünze";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Münze hinzufügen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMünze_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMuenzTyp;
        private System.Windows.Forms.Label lblErhaltungsgrad;
        private System.Windows.Forms.Label lblAblage;
        private System.Windows.Forms.Label lblKaufdatum;
        private System.Windows.Forms.Label lblKaufort;
        private System.Windows.Forms.Label lblVerkäufer;
        private System.Windows.Forms.Label lblPreis;
        private System.Windows.Forms.Label lblKommentar;
        private System.Windows.Forms.Label lblKatNrEigen;
        private SAN.Control.TextBoxEx txtAblage;
        private SAN.Control.TextBoxEx txtKaufort;
        private SAN.Control.TextBoxEx txtVerkäufer;
        private SAN.Control.TextBoxEx txtKatNrEigen;
        private SAN.Control.TextBoxEx txtKommentar;
        private System.Windows.Forms.Label lblWährung;
        private System.Windows.Forms.Label lblStück;
        private SAN.Control.TextBoxEx txtAnzahl;
        private SAN.Control.TextBoxEx txtFehlerNotiz;
        private System.Windows.Forms.CheckBox chkFehlerhaft;
        private System.Windows.Forms.Label lblGebiet;
        private System.Windows.Forms.Label lblGebietText;
        private System.Windows.Forms.Label lblMünze;
        private System.Windows.Forms.Label lblMünzeText;
        private SAN.Control.TextBoxEx txtKaufpreis;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripButton btnSave;
        private SAN.Control.TextBoxEx txtEigenerPreis;
        private System.Windows.Forms.Label lblWährung1;
        private System.Windows.Forms.Label lblEigenerPreis;
        private SAN.Control.TextBoxEx txtKatalogpreis;
        private System.Windows.Forms.Label lblWaehrung2;
        private System.Windows.Forms.Label lblKatalogpreis;
        private SAN.Control.TextBoxEx txtKaufdatum;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SAN.Control.CheckBoxEx chkDoublette;
        private SAN.Control.ComboBoxEx cboErhaltungsgrad;
    }
}