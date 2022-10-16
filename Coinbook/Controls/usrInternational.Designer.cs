using System;
namespace Coinbook
{
    partial class usrInternational
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
            this.grpErhaltungsgrade = new System.Windows.Forms.GroupBox();
            this.cboSprache = new SAN.Control.ComboBoxEx();
            this.lblSprache = new System.Windows.Forms.Label();
            this.lblErhaltungsgrade = new System.Windows.Forms.Label();
            this.cboErhaltungsgrad = new System.Windows.Forms.ComboBox();
            this.grpDefaultPath = new System.Windows.Forms.GroupBox();
            this.chkBackupBeiQuit = new SAN.Control.CheckBoxEx();
            this.lblPfad = new System.Windows.Forms.Label();
            this.btnBackup = new SAN.Control.ButtonEx();
            this.txtPath = new SAN.Control.TextBoxEx();
            this.dlgPath = new System.Windows.Forms.FolderBrowserDialog();
            this.grpAddMuenze = new System.Windows.Forms.GroupBox();
            this.chkKaufpreis = new System.Windows.Forms.CheckBox();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.optBildTab = new System.Windows.Forms.RadioButton();
            this.optSammlungTab = new System.Windows.Forms.RadioButton();
            this.optAusgabeTab = new System.Windows.Forms.RadioButton();
            this.optBeschreibungTab = new System.Windows.Forms.RadioButton();
            this.optMünzdetailTab = new System.Windows.Forms.RadioButton();
            this.optLetzBen = new System.Windows.Forms.RadioButton();
            this.grpErhaltungsgrade.SuspendLayout();
            this.grpDefaultPath.SuspendLayout();
            this.grpAddMuenze.SuspendLayout();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpErhaltungsgrade
            // 
            this.grpErhaltungsgrade.Controls.Add(this.cboSprache);
            this.grpErhaltungsgrade.Controls.Add(this.lblSprache);
            this.grpErhaltungsgrade.Controls.Add(this.lblErhaltungsgrade);
            this.grpErhaltungsgrade.Controls.Add(this.cboErhaltungsgrad);
            this.grpErhaltungsgrade.Location = new System.Drawing.Point(8, 3);
            this.grpErhaltungsgrade.Name = "grpErhaltungsgrade";
            this.grpErhaltungsgrade.Size = new System.Drawing.Size(359, 128);
            this.grpErhaltungsgrade.TabIndex = 2;
            this.grpErhaltungsgrade.TabStop = false;
            this.grpErhaltungsgrade.Text = "Spracheinstellungen";
            // 
            // cboSprache
            // 
            this.cboSprache.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cboSprache.Column = 0;
            this.cboSprache.ColumnsToDisplay = "";
            this.cboSprache.ColumnType = SAN.Control.ComboBoxEx.ColType.SingleColumn;
            this.cboSprache.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSprache.ForeColor = System.Drawing.Color.Black;
            this.cboSprache.FormattingEnabled = true;
            this.cboSprache.GridLinesMultiColumn = false;
            this.cboSprache.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboSprache.ID = ((long)(-1));
            this.cboSprache.IDObject = -1;
            this.cboSprache.IDString = "";
            this.cboSprache.IsPflichtfeld = false;
            this.cboSprache.Location = new System.Drawing.Point(59, 79);
            this.cboSprache.MaxDropDownItems = 10;
            this.cboSprache.Name = "cboSprache";
            this.cboSprache.ReadOnly = false;
            this.cboSprache.Row = 0;
            this.cboSprache.ShowClipBoard = true;
            this.cboSprache.Size = new System.Drawing.Size(176, 21);
            this.cboSprache.TabIndex = 15;
            this.cboSprache.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboSprache.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboSprache_DrawItem);
            this.cboSprache.SelectedIndexChanged += new System.EventHandler(this.cboSprache_SelectedIndexChanged);
            // 
            // lblSprache
            // 
            this.lblSprache.AutoSize = true;
            this.lblSprache.Location = new System.Drawing.Point(6, 82);
            this.lblSprache.Name = "lblSprache";
            this.lblSprache.Size = new System.Drawing.Size(47, 13);
            this.lblSprache.TabIndex = 14;
            this.lblSprache.Text = "Sprache";
            // 
            // lblErhaltungsgrade
            // 
            this.lblErhaltungsgrade.AutoSize = true;
            this.lblErhaltungsgrade.Location = new System.Drawing.Point(6, 28);
            this.lblErhaltungsgrade.Name = "lblErhaltungsgrade";
            this.lblErhaltungsgrade.Size = new System.Drawing.Size(151, 13);
            this.lblErhaltungsgrade.TabIndex = 13;
            this.lblErhaltungsgrade.Text = "Internationale Erhaltungsgrade";
            // 
            // cboErhaltungsgrad
            // 
            this.cboErhaltungsgrad.FormattingEnabled = true;
            this.cboErhaltungsgrad.Location = new System.Drawing.Point(6, 44);
            this.cboErhaltungsgrad.Name = "cboErhaltungsgrad";
            this.cboErhaltungsgrad.Size = new System.Drawing.Size(347, 21);
            this.cboErhaltungsgrad.TabIndex = 12;
            this.cboErhaltungsgrad.SelectedIndexChanged += new System.EventHandler(this.cboErhaltungsgrad_SelectedIndexChanged);
            // 
            // grpDefaultPath
            // 
            this.grpDefaultPath.Controls.Add(this.chkBackupBeiQuit);
            this.grpDefaultPath.Controls.Add(this.lblPfad);
            this.grpDefaultPath.Controls.Add(this.btnBackup);
            this.grpDefaultPath.Controls.Add(this.txtPath);
            this.grpDefaultPath.Location = new System.Drawing.Point(8, 236);
            this.grpDefaultPath.Name = "grpDefaultPath";
            this.grpDefaultPath.Size = new System.Drawing.Size(359, 74);
            this.grpDefaultPath.TabIndex = 3;
            this.grpDefaultPath.TabStop = false;
            this.grpDefaultPath.Text = "Voreinstellung für die Datensicherung";
            // 
            // chkBackupBeiQuit
            // 
            this.chkBackupBeiQuit.AutoSize = true;
            this.chkBackupBeiQuit.BackColorCheck = System.Drawing.Color.LightBlue;
            this.chkBackupBeiQuit.Changed = false;
            this.chkBackupBeiQuit.Column = 0;
            this.chkBackupBeiQuit.ForeColorCheck = System.Drawing.Color.Black;
            this.chkBackupBeiQuit.Location = new System.Drawing.Point(9, 50);
            this.chkBackupBeiQuit.Name = "chkBackupBeiQuit";
            this.chkBackupBeiQuit.ReadOnly = false;
            this.chkBackupBeiQuit.Row = 0;
            this.chkBackupBeiQuit.Size = new System.Drawing.Size(194, 17);
            this.chkBackupBeiQuit.TabIndex = 3;
            this.chkBackupBeiQuit.Text = "Sicherung bei Beenden nachfragen";
            this.chkBackupBeiQuit.TypChecked = System.Windows.Forms.MenuGlyph.Checkmark;
            this.chkBackupBeiQuit.TypIndeterminate = System.Windows.Forms.MenuGlyph.Bullet;
            this.chkBackupBeiQuit.UseVisualStyleBackColor = true;
            this.chkBackupBeiQuit.CheckedChanged += new System.EventHandler(this.chkBackupBeiQuit_CheckedChanged);
            // 
            // lblPfad
            // 
            this.lblPfad.AutoSize = true;
            this.lblPfad.Location = new System.Drawing.Point(6, 22);
            this.lblPfad.Name = "lblPfad";
            this.lblPfad.Size = new System.Drawing.Size(95, 13);
            this.lblPfad.TabIndex = 2;
            this.lblPfad.Text = "Pfad für Sicherung";
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.SystemColors.Control;
            this.btnBackup.ForeColor = System.Drawing.Color.Black;
            this.btnBackup.ImageStretch = false;
            this.btnBackup.Location = new System.Drawing.Point(323, 19);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(29, 20);
            this.btnBackup.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnBackup.Stretched = false;
            this.btnBackup.TabIndex = 1;
            this.btnBackup.Text = "...";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // txtPath
            // 
            this.txtPath.AcceptsReturn = true;
            this.txtPath.AcceptsTab = true;
            this.txtPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtPath.Column = 0;
            this.txtPath.Enter2Tab = true;
            this.txtPath.ForeColor = System.Drawing.Color.Black;
            this.txtPath.IsPflichtfeld = false;
            this.txtPath.Location = new System.Drawing.Point(108, 19);
            this.txtPath.NachkommaStellen = ((short)(0));
            this.txtPath.Name = "txtPath";
            this.txtPath.NumberFormat = null;
            this.txtPath.OldText = "textBoxEx1";
            this.txtPath.RegularExpression = "";
            this.txtPath.Row = 0;
            this.txtPath.ShowClipBoard = true;
            this.txtPath.Size = new System.Drawing.Size(208, 20);
            this.txtPath.TabIndex = 0;
            this.txtPath.Translation = "";
            this.txtPath.Typ = SAN.Control.TextBoxTyp.Text;
            // 
            // dlgPath
            // 
            this.dlgPath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // grpAddMuenze
            // 
            this.grpAddMuenze.Controls.Add(this.chkKaufpreis);
            this.grpAddMuenze.Location = new System.Drawing.Point(8, 316);
            this.grpAddMuenze.Name = "grpAddMuenze";
            this.grpAddMuenze.Size = new System.Drawing.Size(359, 54);
            this.grpAddMuenze.TabIndex = 4;
            this.grpAddMuenze.TabStop = false;
            this.grpAddMuenze.Text = "Münzen erfassen";
            // 
            // chkKaufpreis
            // 
            this.chkKaufpreis.AutoSize = true;
            this.chkKaufpreis.BackColor = System.Drawing.Color.Transparent;
            this.chkKaufpreis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkKaufpreis.Location = new System.Drawing.Point(9, 13);
            this.chkKaufpreis.Name = "chkKaufpreis";
            this.chkKaufpreis.Size = new System.Drawing.Size(312, 30);
            this.chkKaufpreis.TabIndex = 13;
            this.chkKaufpreis.Text = "Beim Erfassen von Münzen oder Dubletten, Katalogwert als \r\nKaufpreis unterstellen.";
            this.chkKaufpreis.UseVisualStyleBackColor = false;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.optBildTab);
            this.grpDetails.Controls.Add(this.optSammlungTab);
            this.grpDetails.Controls.Add(this.optAusgabeTab);
            this.grpDetails.Controls.Add(this.optBeschreibungTab);
            this.grpDetails.Controls.Add(this.optMünzdetailTab);
            this.grpDetails.Controls.Add(this.optLetzBen);
            this.grpDetails.Location = new System.Drawing.Point(8, 136);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(359, 94);
            this.grpDetails.TabIndex = 5;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Münzdetails Starteinstellungen";
            // 
            // optBildTab
            // 
            this.optBildTab.AutoSize = true;
            this.optBildTab.BackColor = System.Drawing.Color.Transparent;
            this.optBildTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optBildTab.Location = new System.Drawing.Point(172, 65);
            this.optBildTab.Name = "optBildTab";
            this.optBildTab.Size = new System.Drawing.Size(41, 17);
            this.optBildTab.TabIndex = 7;
            this.optBildTab.Text = "Bild";
            this.optBildTab.UseVisualStyleBackColor = false;
            this.optBildTab.CheckedChanged += new System.EventHandler(this.GetMuenzDetailState);
            // 
            // optSammlungTab
            // 
            this.optSammlungTab.AutoSize = true;
            this.optSammlungTab.BackColor = System.Drawing.Color.Transparent;
            this.optSammlungTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optSammlungTab.Location = new System.Drawing.Point(6, 65);
            this.optSammlungTab.Name = "optSammlungTab";
            this.optSammlungTab.Size = new System.Drawing.Size(143, 17);
            this.optSammlungTab.TabIndex = 6;
            this.optSammlungTab.Text = "Sammlung und Dubletten";
            this.optSammlungTab.UseVisualStyleBackColor = false;
            this.optSammlungTab.CheckedChanged += new System.EventHandler(this.GetMuenzDetailState);
            // 
            // optAusgabeTab
            // 
            this.optAusgabeTab.AutoSize = true;
            this.optAusgabeTab.BackColor = System.Drawing.Color.Transparent;
            this.optAusgabeTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optAusgabeTab.Location = new System.Drawing.Point(172, 42);
            this.optAusgabeTab.Name = "optAusgabeTab";
            this.optAusgabeTab.Size = new System.Drawing.Size(160, 17);
            this.optAusgabeTab.TabIndex = 5;
            this.optAusgabeTab.Text = "Ausgabeanlass / Kommentar";
            this.optAusgabeTab.UseVisualStyleBackColor = false;
            this.optAusgabeTab.CheckedChanged += new System.EventHandler(this.GetMuenzDetailState);
            // 
            // optBeschreibungTab
            // 
            this.optBeschreibungTab.AutoSize = true;
            this.optBeschreibungTab.BackColor = System.Drawing.Color.Transparent;
            this.optBeschreibungTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optBeschreibungTab.Location = new System.Drawing.Point(6, 42);
            this.optBeschreibungTab.Name = "optBeschreibungTab";
            this.optBeschreibungTab.Size = new System.Drawing.Size(136, 17);
            this.optBeschreibungTab.TabIndex = 4;
            this.optBeschreibungTab.Text = "Beschreibung / Entwurf";
            this.optBeschreibungTab.UseVisualStyleBackColor = false;
            this.optBeschreibungTab.CheckedChanged += new System.EventHandler(this.GetMuenzDetailState);
            // 
            // optMünzdetailTab
            // 
            this.optMünzdetailTab.AutoSize = true;
            this.optMünzdetailTab.BackColor = System.Drawing.Color.Transparent;
            this.optMünzdetailTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optMünzdetailTab.Location = new System.Drawing.Point(172, 19);
            this.optMünzdetailTab.Name = "optMünzdetailTab";
            this.optMünzdetailTab.Size = new System.Drawing.Size(80, 17);
            this.optMünzdetailTab.TabIndex = 3;
            this.optMünzdetailTab.Text = "Münzdetails";
            this.optMünzdetailTab.UseVisualStyleBackColor = false;
            this.optMünzdetailTab.CheckedChanged += new System.EventHandler(this.GetMuenzDetailState);
            // 
            // optLetzBen
            // 
            this.optLetzBen.AutoSize = true;
            this.optLetzBen.BackColor = System.Drawing.Color.Transparent;
            this.optLetzBen.Checked = true;
            this.optLetzBen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optLetzBen.Location = new System.Drawing.Point(6, 19);
            this.optLetzBen.Name = "optLetzBen";
            this.optLetzBen.Size = new System.Drawing.Size(97, 17);
            this.optLetzBen.TabIndex = 2;
            this.optLetzBen.TabStop = true;
            this.optLetzBen.Text = "Zuletzt benutze";
            this.optLetzBen.UseVisualStyleBackColor = false;
            this.optLetzBen.CheckedChanged += new System.EventHandler(this.GetMuenzDetailState);
            // 
            // usrInternational
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.grpAddMuenze);
            this.Controls.Add(this.grpDefaultPath);
            this.Controls.Add(this.grpErhaltungsgrade);
            this.Name = "usrInternational";
            this.Size = new System.Drawing.Size(378, 413);
            this.grpErhaltungsgrade.ResumeLayout(false);
            this.grpErhaltungsgrade.PerformLayout();
            this.grpDefaultPath.ResumeLayout(false);
            this.grpDefaultPath.PerformLayout();
            this.grpAddMuenze.ResumeLayout(false);
            this.grpAddMuenze.PerformLayout();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpErhaltungsgrade;
				private System.Windows.Forms.ComboBox cboErhaltungsgrad;
        private System.Windows.Forms.GroupBox grpDefaultPath;
        private SAN.Control.ButtonEx btnBackup;
        private SAN.Control.TextBoxEx txtPath;
        private System.Windows.Forms.FolderBrowserDialog dlgPath;
        private SAN.Control.CheckBoxEx chkBackupBeiQuit;
        private System.Windows.Forms.Label lblPfad;
        private System.Windows.Forms.GroupBox grpAddMuenze;
        private System.Windows.Forms.Label lblSprache;
        private System.Windows.Forms.Label lblErhaltungsgrade;
        private SAN.Control.ComboBoxEx cboSprache;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.CheckBox chkKaufpreis;
        private System.Windows.Forms.RadioButton optBildTab;
        private System.Windows.Forms.RadioButton optSammlungTab;
        private System.Windows.Forms.RadioButton optAusgabeTab;
        private System.Windows.Forms.RadioButton optBeschreibungTab;
        private System.Windows.Forms.RadioButton optMünzdetailTab;
        private System.Windows.Forms.RadioButton optLetzBen;
    }
}
