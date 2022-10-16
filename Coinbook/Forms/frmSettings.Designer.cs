using Coinbook.EventHandlers;

namespace Coinbook
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAllgemein = new System.Windows.Forms.TabPage();
            this.tabInternational = new System.Windows.Forms.TabPage();
            this.tabUpdates = new System.Windows.Forms.TabPage();
            this.tabPersonalisierung = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.ctlAllgemein = new Coinbook.usrAllgemein();
            this.ctlInternational = new Coinbook.usrInternational();
            this.ctlUpdates = new Coinbook.usrUpdates();
            this.ctlEigneEinstellungen = new Coinbook.usrEigeneEinst();
            this.tabControl1.SuspendLayout();
            this.tabAllgemein.SuspendLayout();
            this.tabInternational.SuspendLayout();
            this.tabUpdates.SuspendLayout();
            this.tabPersonalisierung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAllgemein);
            this.tabControl1.Controls.Add(this.tabInternational);
            this.tabControl1.Controls.Add(this.tabUpdates);
            this.tabControl1.Controls.Add(this.tabPersonalisierung);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(387, 433);
            this.tabControl1.TabIndex = 1;
            // 
            // tabAllgemein
            // 
            this.tabAllgemein.BackColor = System.Drawing.Color.LightGray;
            this.tabAllgemein.Controls.Add(this.ctlAllgemein);
            this.tabAllgemein.Location = new System.Drawing.Point(4, 22);
            this.tabAllgemein.Name = "tabAllgemein";
            this.tabAllgemein.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllgemein.Size = new System.Drawing.Size(379, 407);
            this.tabAllgemein.TabIndex = 0;
            this.tabAllgemein.Text = "Allgemein";
            // 
            // tabInternational
            // 
            this.tabInternational.BackColor = System.Drawing.Color.LightGray;
            this.tabInternational.Controls.Add(this.ctlInternational);
            this.tabInternational.Location = new System.Drawing.Point(4, 22);
            this.tabInternational.Name = "tabInternational";
            this.tabInternational.Size = new System.Drawing.Size(379, 407);
            this.tabInternational.TabIndex = 3;
            this.tabInternational.Text = "Voreinstellungen";
            // 
            // tabUpdates
            // 
            this.tabUpdates.Controls.Add(this.ctlUpdates);
            this.tabUpdates.Location = new System.Drawing.Point(4, 22);
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpdates.Size = new System.Drawing.Size(379, 407);
            this.tabUpdates.TabIndex = 5;
            this.tabUpdates.Text = "Updates";
            this.tabUpdates.UseVisualStyleBackColor = true;
            // 
            // tabPersonalisierung
            // 
            this.tabPersonalisierung.BackColor = System.Drawing.Color.LightGray;
            this.tabPersonalisierung.Controls.Add(this.ctlEigneEinstellungen);
            this.tabPersonalisierung.Location = new System.Drawing.Point(4, 22);
            this.tabPersonalisierung.Name = "tabPersonalisierung";
            this.tabPersonalisierung.Size = new System.Drawing.Size(379, 407);
            this.tabPersonalisierung.TabIndex = 4;
            this.tabPersonalisierung.Text = "Personalisierung";
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
            this.btnSave.Text = "Speichern";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnSave});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(409, 25);
            this.bindingNavigator1.TabIndex = 2;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // ctlAllgemein
            // 
            this.ctlAllgemein.BackColor = System.Drawing.Color.LightGray;
            this.ctlAllgemein.Location = new System.Drawing.Point(5, 5);
            this.ctlAllgemein.Margin = new System.Windows.Forms.Padding(4);
            this.ctlAllgemein.Name = "ctlAllgemein";
            this.ctlAllgemein.Size = new System.Drawing.Size(374, 401);
            this.ctlAllgemein.TabIndex = 0;
            this.ctlAllgemein.Changed += new System.EventHandler(this.Changed);
            this.ctlAllgemein.PreisStyleChanged += new PreisStyleEventHandler(this.ctlAllgemein_PreisStyleChanged);
            // 
            // ctlInternational
            // 
            this.ctlInternational.BackColor = System.Drawing.Color.LightGray;
            this.ctlInternational.Location = new System.Drawing.Point(5, 5);
            this.ctlInternational.Margin = new System.Windows.Forms.Padding(4);
            this.ctlInternational.Name = "ctlInternational";
            this.ctlInternational.Size = new System.Drawing.Size(378, 379);
            this.ctlInternational.TabIndex = 0;
            this.ctlInternational.Changed += new System.EventHandler(this.Changed);
            this.ctlInternational.LanguageChanged += new System.EventHandler(this.ctlInternational_LanguageChanged);
            this.ctlInternational.ErhaltungChanged += new System.EventHandler(this.ctlInternational_ErhaltungChanged);
            // 
            // ctlUpdates
            // 
            this.ctlUpdates.BackColor = System.Drawing.Color.LightGray;
            this.ctlUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlUpdates.Location = new System.Drawing.Point(3, 3);
            this.ctlUpdates.Name = "ctlUpdates";
            this.ctlUpdates.Size = new System.Drawing.Size(373, 401);
            this.ctlUpdates.TabIndex = 0;
            // 
            // ctlEigneEinstellungen
            // 
            this.ctlEigneEinstellungen.BackColor = System.Drawing.Color.LightGray;
            this.ctlEigneEinstellungen.Location = new System.Drawing.Point(1, 3);
            this.ctlEigneEinstellungen.Name = "ctlEigneEinstellungen";
            this.ctlEigneEinstellungen.Size = new System.Drawing.Size(375, 400);
            this.ctlEigneEinstellungen.TabIndex = 0;
            this.ctlEigneEinstellungen.Changed += new System.EventHandler(this.Changed);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(409, 465);
            this.ControlBox = false;
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Einstellungen";
            this.tabControl1.ResumeLayout(false);
            this.tabAllgemein.ResumeLayout(false);
            this.tabInternational.ResumeLayout(false);
            this.tabUpdates.ResumeLayout(false);
            this.tabPersonalisierung.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

				private System.Windows.Forms.TabControl tabControl1;
				private System.Windows.Forms.TabPage tabAllgemein;
        private usrAllgemein ctlAllgemein;
				private System.Windows.Forms.TabPage tabInternational;
				private System.Windows.Forms.TabPage tabPersonalisierung;
				private usrInternational ctlInternational;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private usrEigeneEinst ctlEigneEinstellungen;
		private System.Windows.Forms.TabPage tabUpdates;
		private usrUpdates ctlUpdates;
	}
}