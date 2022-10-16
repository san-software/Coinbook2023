namespace Coinbook
{
    partial class usrAllgemein
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpStart = new System.Windows.Forms.GroupBox();
            this.optLastPos = new System.Windows.Forms.RadioButton();
            this.optErsteNat = new System.Windows.Forms.RadioButton();
            this.grpKatalognummern = new System.Windows.Forms.GroupBox();
            this.chkOwnKatalog = new SAN.Control.CheckBoxEx();
            this.grpEigeneKatalognummern = new System.Windows.Forms.GroupBox();
            this.optCoinbookNummern = new System.Windows.Forms.RadioButton();
            this.optEigeneNummern = new System.Windows.Forms.RadioButton();
            this.grpListen = new System.Windows.Forms.GroupBox();
            this.optSammlung = new System.Windows.Forms.RadioButton();
            this.chkExemplar = new SAN.Control.CheckBoxEx();
            this.optIcon = new System.Windows.Forms.RadioButton();
            this.optDoubletten = new System.Windows.Forms.RadioButton();
            this.optStandard = new System.Windows.Forms.RadioButton();
            this.grpPreise = new System.Windows.Forms.GroupBox();
            this.optKaufpreise = new System.Windows.Forms.RadioButton();
            this.optEigenePreise = new System.Windows.Forms.RadioButton();
            this.optKatalogpreise = new System.Windows.Forms.RadioButton();
            this.grpStart.SuspendLayout();
            this.grpKatalognummern.SuspendLayout();
            this.grpEigeneKatalognummern.SuspendLayout();
            this.grpListen.SuspendLayout();
            this.grpPreise.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpStart
            // 
            this.grpStart.Controls.Add(this.optLastPos);
            this.grpStart.Controls.Add(this.optErsteNat);
            this.grpStart.Location = new System.Drawing.Point(8, 5);
            this.grpStart.Name = "grpStart";
            this.grpStart.Size = new System.Drawing.Size(359, 47);
            this.grpStart.TabIndex = 1;
            this.grpStart.TabStop = false;
            this.grpStart.Text = "Starteinstellungen";
            // 
            // optLastPos
            // 
            this.optLastPos.AutoSize = true;
            this.optLastPos.BackColor = System.Drawing.Color.Transparent;
            this.optLastPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optLastPos.Location = new System.Drawing.Point(172, 19);
            this.optLastPos.Name = "optLastPos";
            this.optLastPos.Size = new System.Drawing.Size(130, 17);
            this.optLastPos.TabIndex = 3;
            this.optLastPos.Text = "Mit der letzten Position";
            this.optLastPos.UseVisualStyleBackColor = false;
            this.optLastPos.CheckedChanged += new System.EventHandler(this.GetStEinstState);
            // 
            // optErsteNat
            // 
            this.optErsteNat.AutoSize = true;
            this.optErsteNat.BackColor = System.Drawing.Color.Transparent;
            this.optErsteNat.Checked = true;
            this.optErsteNat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optErsteNat.Location = new System.Drawing.Point(6, 19);
            this.optErsteNat.Name = "optErsteNat";
            this.optErsteNat.Size = new System.Drawing.Size(122, 17);
            this.optErsteNat.TabIndex = 2;
            this.optErsteNat.TabStop = true;
            this.optErsteNat.Text = "Mit der ersten Nation";
            this.optErsteNat.UseVisualStyleBackColor = false;
            this.optErsteNat.CheckedChanged += new System.EventHandler(this.GetStEinstState);
            // 
            // grpKatalognummern
            // 
            this.grpKatalognummern.Controls.Add(this.chkOwnKatalog);
            this.grpKatalognummern.Controls.Add(this.grpEigeneKatalognummern);
            this.grpKatalognummern.Location = new System.Drawing.Point(8, 58);
            this.grpKatalognummern.Name = "grpKatalognummern";
            this.grpKatalognummern.Size = new System.Drawing.Size(359, 132);
            this.grpKatalognummern.TabIndex = 2;
            this.grpKatalognummern.TabStop = false;
            this.grpKatalognummern.Text = "Katalognummern";
            // 
            // chkOwnKatalog
            // 
            this.chkOwnKatalog.AutoSize = true;
            this.chkOwnKatalog.BackColorCheck = System.Drawing.Color.White;
            this.chkOwnKatalog.Changed = false;
            this.chkOwnKatalog.Column = 0;
            this.chkOwnKatalog.ForeColorCheck = System.Drawing.SystemColors.ControlText;
            this.chkOwnKatalog.Location = new System.Drawing.Point(5, 19);
            this.chkOwnKatalog.Name = "chkOwnKatalog";
            this.chkOwnKatalog.ReadOnly = false;
            this.chkOwnKatalog.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkOwnKatalog.Row = 0;
            this.chkOwnKatalog.Size = new System.Drawing.Size(197, 17);
            this.chkOwnKatalog.TabIndex = 3;
            this.chkOwnKatalog.Text = "Eigene Katalognummern verwenden";
            this.chkOwnKatalog.TypChecked = System.Windows.Forms.MenuGlyph.Checkmark;
            this.chkOwnKatalog.TypIndeterminate = System.Windows.Forms.MenuGlyph.Bullet;
            this.chkOwnKatalog.UseVisualStyleBackColor = true;
            this.chkOwnKatalog.CheckedChanged += new System.EventHandler(this.chkOwnKatalog_CheckedChanged);
            // 
            // grpEigeneKatalognummern
            // 
            this.grpEigeneKatalognummern.Controls.Add(this.optCoinbookNummern);
            this.grpEigeneKatalognummern.Controls.Add(this.optEigeneNummern);
            this.grpEigeneKatalognummern.Location = new System.Drawing.Point(6, 42);
            this.grpEigeneKatalognummern.Name = "grpEigeneKatalognummern";
            this.grpEigeneKatalognummern.Size = new System.Drawing.Size(347, 71);
            this.grpEigeneKatalognummern.TabIndex = 2;
            this.grpEigeneKatalognummern.TabStop = false;
            // 
            // optCoinbookNummern
            // 
            this.optCoinbookNummern.AutoSize = true;
            this.optCoinbookNummern.Location = new System.Drawing.Point(6, 19);
            this.optCoinbookNummern.Name = "optCoinbookNummern";
            this.optCoinbookNummern.Size = new System.Drawing.Size(198, 17);
            this.optCoinbookNummern.TabIndex = 0;
            this.optCoinbookNummern.TabStop = true;
            this.optCoinbookNummern.Text = "Coinbook Katalognummern anzeigen";
            this.optCoinbookNummern.UseVisualStyleBackColor = true;
            this.optCoinbookNummern.CheckedChanged += new System.EventHandler(this.GetNumberState);
            // 
            // optEigeneNummern
            // 
            this.optEigeneNummern.AutoSize = true;
            this.optEigeneNummern.Location = new System.Drawing.Point(6, 42);
            this.optEigeneNummern.Name = "optEigeneNummern";
            this.optEigeneNummern.Size = new System.Drawing.Size(186, 17);
            this.optEigeneNummern.TabIndex = 1;
            this.optEigeneNummern.TabStop = true;
            this.optEigeneNummern.Text = "Eigene Katalognummern anzeigen";
            this.optEigeneNummern.UseVisualStyleBackColor = true;
            this.optEigeneNummern.CheckedChanged += new System.EventHandler(this.GetNumberState);
            // 
            // grpListen
            // 
            this.grpListen.Controls.Add(this.optSammlung);
            this.grpListen.Controls.Add(this.chkExemplar);
            this.grpListen.Controls.Add(this.optIcon);
            this.grpListen.Controls.Add(this.optDoubletten);
            this.grpListen.Controls.Add(this.optStandard);
            this.grpListen.Location = new System.Drawing.Point(8, 196);
            this.grpListen.Name = "grpListen";
            this.grpListen.Size = new System.Drawing.Size(359, 99);
            this.grpListen.TabIndex = 7;
            this.grpListen.TabStop = false;
            this.grpListen.Text = "Listeneinstellungen";
            // 
            // optSammlung
            // 
            this.optSammlung.AutoSize = true;
            this.optSammlung.BackColor = System.Drawing.Color.Transparent;
            this.optSammlung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optSammlung.Location = new System.Drawing.Point(200, 42);
            this.optSammlung.Name = "optSammlung";
            this.optSammlung.Size = new System.Drawing.Size(134, 17);
            this.optSammlung.TabIndex = 6;
            this.optSammlung.Text = "Nur Sammlung (Anzahl)";
            this.optSammlung.UseVisualStyleBackColor = false;
            this.optSammlung.CheckedChanged += new System.EventHandler(this.GetCheckStateListe);
            // 
            // chkExemplar
            // 
            this.chkExemplar.AutoSize = true;
            this.chkExemplar.BackColorCheck = System.Drawing.Color.White;
            this.chkExemplar.Changed = false;
            this.chkExemplar.Column = 0;
            this.chkExemplar.ForeColorCheck = System.Drawing.SystemColors.ControlText;
            this.chkExemplar.Location = new System.Drawing.Point(200, 76);
            this.chkExemplar.Name = "chkExemplar";
            this.chkExemplar.ReadOnly = false;
            this.chkExemplar.Row = 0;
            this.chkExemplar.Size = new System.Drawing.Size(107, 17);
            this.chkExemplar.TabIndex = 5;
            this.chkExemplar.Text = "Exemplarsammler";
            this.chkExemplar.TypChecked = System.Windows.Forms.MenuGlyph.Checkmark;
            this.chkExemplar.TypIndeterminate = System.Windows.Forms.MenuGlyph.Bullet;
            this.chkExemplar.UseVisualStyleBackColor = true;
            this.chkExemplar.Visible = false;
            this.chkExemplar.Click += new System.EventHandler(this.chkExemplar_Click);
            // 
            // optIcon
            // 
            this.optIcon.AutoSize = true;
            this.optIcon.BackColor = System.Drawing.Color.Transparent;
            this.optIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optIcon.Location = new System.Drawing.Point(200, 19);
            this.optIcon.Name = "optIcon";
            this.optIcon.Size = new System.Drawing.Size(132, 17);
            this.optIcon.TabIndex = 4;
            this.optIcon.Text = "Sammlung (Münz-Icon)";
            this.optIcon.UseVisualStyleBackColor = false;
            this.optIcon.CheckedChanged += new System.EventHandler(this.GetCheckStateListe);
            // 
            // optDoubletten
            // 
            this.optDoubletten.AutoSize = true;
            this.optDoubletten.BackColor = System.Drawing.Color.Transparent;
            this.optDoubletten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optDoubletten.Location = new System.Drawing.Point(6, 42);
            this.optDoubletten.Name = "optDoubletten";
            this.optDoubletten.Size = new System.Drawing.Size(131, 17);
            this.optDoubletten.TabIndex = 3;
            this.optDoubletten.Text = "Nur Dubletten (Anzahl)";
            this.optDoubletten.UseVisualStyleBackColor = false;
            this.optDoubletten.CheckedChanged += new System.EventHandler(this.GetCheckStateListe);
            // 
            // optStandard
            // 
            this.optStandard.AutoSize = true;
            this.optStandard.BackColor = System.Drawing.Color.Transparent;
            this.optStandard.Checked = true;
            this.optStandard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optStandard.Location = new System.Drawing.Point(6, 19);
            this.optStandard.Name = "optStandard";
            this.optStandard.Size = new System.Drawing.Size(169, 17);
            this.optStandard.TabIndex = 2;
            this.optStandard.TabStop = true;
            this.optStandard.Text = "Sammlung und Dubletten (1/1)";
            this.optStandard.UseVisualStyleBackColor = false;
            this.optStandard.CheckedChanged += new System.EventHandler(this.GetCheckStateListe);
            // 
            // grpPreise
            // 
            this.grpPreise.Controls.Add(this.optKaufpreise);
            this.grpPreise.Controls.Add(this.optEigenePreise);
            this.grpPreise.Controls.Add(this.optKatalogpreise);
            this.grpPreise.Location = new System.Drawing.Point(8, 301);
            this.grpPreise.Name = "grpPreise";
            this.grpPreise.Size = new System.Drawing.Size(359, 91);
            this.grpPreise.TabIndex = 8;
            this.grpPreise.TabStop = false;
            this.grpPreise.Text = "Welche Preise werden angezeigt";
            // 
            // optKaufpreise
            // 
            this.optKaufpreise.AutoSize = true;
            this.optKaufpreise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optKaufpreise.Location = new System.Drawing.Point(6, 65);
            this.optKaufpreise.Name = "optKaufpreise";
            this.optKaufpreise.Size = new System.Drawing.Size(74, 17);
            this.optKaufpreise.TabIndex = 3;
            this.optKaufpreise.Text = "Kaufpreise";
            this.optKaufpreise.UseVisualStyleBackColor = true;
            this.optKaufpreise.CheckedChanged += new System.EventHandler(this.Preise_CheckedChanged);
            // 
            // optEigenePreise
            // 
            this.optEigenePreise.AutoSize = true;
            this.optEigenePreise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optEigenePreise.Location = new System.Drawing.Point(6, 42);
            this.optEigenePreise.Name = "optEigenePreise";
            this.optEigenePreise.Size = new System.Drawing.Size(101, 17);
            this.optEigenePreise.TabIndex = 2;
            this.optEigenePreise.Text = "Eigenene Preise";
            this.optEigenePreise.UseVisualStyleBackColor = true;
            this.optEigenePreise.CheckedChanged += new System.EventHandler(this.Preise_CheckedChanged);
            // 
            // optKatalogpreise
            // 
            this.optKatalogpreise.AutoSize = true;
            this.optKatalogpreise.Checked = true;
            this.optKatalogpreise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optKatalogpreise.Location = new System.Drawing.Point(6, 19);
            this.optKatalogpreise.Name = "optKatalogpreise";
            this.optKatalogpreise.Size = new System.Drawing.Size(88, 17);
            this.optKatalogpreise.TabIndex = 1;
            this.optKatalogpreise.TabStop = true;
            this.optKatalogpreise.Text = "Katalogpreise";
            this.optKatalogpreise.UseVisualStyleBackColor = true;
            this.optKatalogpreise.CheckedChanged += new System.EventHandler(this.Preise_CheckedChanged);
            // 
            // usrAllgemein
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.grpPreise);
            this.Controls.Add(this.grpListen);
            this.Controls.Add(this.grpKatalognummern);
            this.Controls.Add(this.grpStart);
            this.Name = "usrAllgemein";
            this.Size = new System.Drawing.Size(378, 408);
            this.grpStart.ResumeLayout(false);
            this.grpStart.PerformLayout();
            this.grpKatalognummern.ResumeLayout(false);
            this.grpKatalognummern.PerformLayout();
            this.grpEigeneKatalognummern.ResumeLayout(false);
            this.grpEigeneKatalognummern.PerformLayout();
            this.grpListen.ResumeLayout(false);
            this.grpListen.PerformLayout();
            this.grpPreise.ResumeLayout(false);
            this.grpPreise.PerformLayout();
            this.ResumeLayout(false);

				}

        #endregion

        private System.Windows.Forms.GroupBox grpStart;
        private System.Windows.Forms.RadioButton optLastPos;
        private System.Windows.Forms.RadioButton optErsteNat;
        private System.Windows.Forms.GroupBox grpKatalognummern;
        private System.Windows.Forms.GroupBox grpListen;
        private System.Windows.Forms.RadioButton optIcon;
        private System.Windows.Forms.RadioButton optDoubletten;
        private System.Windows.Forms.RadioButton optStandard;
				private SAN.Control.CheckBoxEx chkExemplar;
        private System.Windows.Forms.GroupBox grpPreise;
        private System.Windows.Forms.RadioButton optKaufpreise;
        private System.Windows.Forms.RadioButton optEigenePreise;
        private System.Windows.Forms.RadioButton optKatalogpreise;
        private System.Windows.Forms.RadioButton optSammlung;
        private SAN.Control.CheckBoxEx chkOwnKatalog;
        private System.Windows.Forms.GroupBox grpEigeneKatalognummern;
        private System.Windows.Forms.RadioButton optCoinbookNummern;
        private System.Windows.Forms.RadioButton optEigeneNummern;
    }
}
