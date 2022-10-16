using System;

namespace Coinbook
{
    partial class frmMünzeDelete
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMünzeDelete));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.grdSammlung = new System.Windows.Forms.DataGridView();
            this.lblMünzeText = new System.Windows.Forms.Label();
            this.lblMünze = new System.Windows.Forms.Label();
            this.lblGebietText = new System.Windows.Forms.Label();
            this.lblGebiet = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colErhaltungsgrad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoublette = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAblage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKaufdatum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKaufort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVerkäufer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKommentar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDErhaltung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKatNr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKatalogpreis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFehlerhaft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.toolStrip1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSammlung)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AddNewItem = null;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.CountItem = null;
            this.toolStrip1.DeleteItem = null;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.MoveFirstItem = null;
            this.toolStrip1.MoveLastItem = null;
            this.toolStrip1.MoveNextItem = null;
            this.toolStrip1.MovePreviousItem = null;
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.PositionItem = null;
            this.toolStrip1.Size = new System.Drawing.Size(1031, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 2;
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
            // grdSammlung
            // 
            this.grdSammlung.AllowUserToAddRows = false;
            this.grdSammlung.AllowUserToDeleteRows = false;
            this.grdSammlung.AllowUserToResizeRows = false;
            this.grdSammlung.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSammlung.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdSammlung.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdSammlung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSammlung.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnDel,
            this.colErhaltungsgrad,
            this.colDoublette,
            this.colAblage,
            this.colKaufdatum,
            this.colKaufort,
            this.colVerkäufer,
            this.colKommentar,
            this.colPreis,
            this.colID,
            this.colIDErhaltung,
            this.colKatNr,
            this.colKatalogpreis,
            this.ColFehlerhaft});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSammlung.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdSammlung.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdSammlung.Location = new System.Drawing.Point(0, 88);
            this.grdSammlung.MultiSelect = false;
            this.grdSammlung.Name = "grdSammlung";
            this.grdSammlung.RowHeadersVisible = false;
            this.grdSammlung.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdSammlung.ShowEditingIcon = false;
            this.grdSammlung.Size = new System.Drawing.Size(1030, 385);
            this.grdSammlung.TabIndex = 3;
            this.grdSammlung.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdSammlung_CellContentClick);
            this.grdSammlung.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdSammlung_CellPainting);
            // 
            // lblMünzeText
            // 
            this.lblMünzeText.AutoSize = true;
            this.lblMünzeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMünzeText.Location = new System.Drawing.Point(126, 61);
            this.lblMünzeText.Name = "lblMünzeText";
            this.lblMünzeText.Size = new System.Drawing.Size(44, 13);
            this.lblMünzeText.TabIndex = 35;
            this.lblMünzeText.Text = "Münze";
            // 
            // lblMünze
            // 
            this.lblMünze.AutoSize = true;
            this.lblMünze.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMünze.Location = new System.Drawing.Point(12, 61);
            this.lblMünze.Name = "lblMünze";
            this.lblMünze.Size = new System.Drawing.Size(39, 13);
            this.lblMünze.TabIndex = 34;
            this.lblMünze.Text = "Münze";
            // 
            // lblGebietText
            // 
            this.lblGebietText.AutoSize = true;
            this.lblGebietText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGebietText.Location = new System.Drawing.Point(126, 34);
            this.lblGebietText.Name = "lblGebietText";
            this.lblGebietText.Size = new System.Drawing.Size(44, 13);
            this.lblGebietText.TabIndex = 33;
            this.lblGebietText.Text = "Gebiet";
            // 
            // lblGebiet
            // 
            this.lblGebiet.AutoSize = true;
            this.lblGebiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGebiet.Location = new System.Drawing.Point(12, 34);
            this.lblGebiet.Name = "lblGebiet";
            this.lblGebiet.Size = new System.Drawing.Size(38, 13);
            this.lblGebiet.TabIndex = 32;
            this.lblGebiet.Text = "Gebiet";
            // 
            // btnDel
            // 
            this.btnDel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.HeaderText = "";
            this.btnDel.Name = "btnDel";
            this.btnDel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnDel.Width = 25;
            // 
            // colErhaltungsgrad
            // 
            this.colErhaltungsgrad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colErhaltungsgrad.DataPropertyName = "Erhaltungsgrad";
            this.colErhaltungsgrad.HeaderText = "Erhaltungs-{##}\r\ngrad";
            this.colErhaltungsgrad.Name = "colErhaltungsgrad";
            this.colErhaltungsgrad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colErhaltungsgrad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colErhaltungsgrad.Width = 80;
            // 
            // colDoublette
            // 
            this.colDoublette.DataPropertyName = "Doublette";
            this.colDoublette.HeaderText = "Dou-{##}blette";
            this.colDoublette.Name = "colDoublette";
            this.colDoublette.ReadOnly = true;
            this.colDoublette.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDoublette.Width = 50;
            // 
            // colAblage
            // 
            this.colAblage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colAblage.DataPropertyName = "Ablage";
            this.colAblage.HeaderText = "Ablage";
            this.colAblage.Name = "colAblage";
            this.colAblage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colAblage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colKaufdatum
            // 
            this.colKaufdatum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colKaufdatum.DataPropertyName = "Kaufdatum";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colKaufdatum.DefaultCellStyle = dataGridViewCellStyle2;
            this.colKaufdatum.HeaderText = "Kaufdatum";
            this.colKaufdatum.Name = "colKaufdatum";
            this.colKaufdatum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colKaufdatum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colKaufdatum.Width = 75;
            // 
            // colKaufort
            // 
            this.colKaufort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colKaufort.DataPropertyName = "Kaufort";
            this.colKaufort.HeaderText = "Kaufort";
            this.colKaufort.Name = "colKaufort";
            this.colKaufort.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colKaufort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colKaufort.Width = 118;
            // 
            // colVerkäufer
            // 
            this.colVerkäufer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colVerkäufer.DataPropertyName = "Verkaeufer";
            this.colVerkäufer.HeaderText = "Verkäufer";
            this.colVerkäufer.Name = "colVerkäufer";
            this.colVerkäufer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colVerkäufer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colVerkäufer.Width = 118;
            // 
            // colKommentar
            // 
            this.colKommentar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colKommentar.DataPropertyName = "Kommentar";
            this.colKommentar.HeaderText = "Kommentar";
            this.colKommentar.Name = "colKommentar";
            this.colKommentar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colKommentar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPreis
            // 
            this.colPreis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPreis.DataPropertyName = "KaufPreis";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "###,##0.00";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colPreis.DefaultCellStyle = dataGridViewCellStyle3;
            this.colPreis.HeaderText = "Preis";
            this.colPreis.Name = "colPreis";
            this.colPreis.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colPreis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPreis.ToolTipText = "Münze entfernen";
            this.colPreis.Width = 70;
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "Index";
            this.colID.Name = "colID";
            this.colID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colID.Visible = false;
            // 
            // colIDErhaltung
            // 
            this.colIDErhaltung.DataPropertyName = "Erhaltungsgrad";
            this.colIDErhaltung.HeaderText = "Erhaltung";
            this.colIDErhaltung.Name = "colIDErhaltung";
            this.colIDErhaltung.Visible = false;
            // 
            // colKatNr
            // 
            this.colKatNr.DataPropertyName = "KatNrEigen";
            this.colKatNr.HeaderText = "Eigene Nr.";
            this.colKatNr.Name = "colKatNr";
            // 
            // colKatalogpreis
            // 
            this.colKatalogpreis.DataPropertyName = "Katalogpreis";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "###,##0.00";
            this.colKatalogpreis.DefaultCellStyle = dataGridViewCellStyle4;
            this.colKatalogpreis.HeaderText = "Katalog{##}preis";
            this.colKatalogpreis.Name = "colKatalogpreis";
            this.colKatalogpreis.ReadOnly = true;
            this.colKatalogpreis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colKatalogpreis.Width = 70;
            // 
            // ColFehlerhaft
            // 
            this.ColFehlerhaft.DataPropertyName = "Fehlerhaft";
            this.ColFehlerhaft.HeaderText = "Fehlerhaft";
            this.ColFehlerhaft.Name = "ColFehlerhaft";
            // 
            // frmMünzeDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1031, 475);
            this.ControlBox = false;
            this.Controls.Add(this.lblMünzeText);
            this.Controls.Add(this.lblMünze);
            this.Controls.Add(this.lblGebietText);
            this.Controls.Add(this.lblGebiet);
            this.Controls.Add(this.grdSammlung);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMünzeDelete";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Münze entfernen";
            ((System.ComponentModel.ISupportInitialize)(this.toolStrip1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSammlung)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

				private System.Windows.Forms.BindingNavigator toolStrip1;
				private System.Windows.Forms.ToolStripButton btnClose;
				private System.Windows.Forms.DataGridView grdSammlung;
				private System.Windows.Forms.Label lblMünzeText;
				private System.Windows.Forms.Label lblMünze;
				private System.Windows.Forms.Label lblGebietText;
        private System.Windows.Forms.Label lblGebiet;
        private System.Windows.Forms.DataGridViewButtonColumn btnDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErhaltungsgrad;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDoublette;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAblage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKaufdatum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKaufort;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVerkäufer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKommentar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDErhaltung;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKatNr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKatalogpreis;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFehlerhaft;
    }
}