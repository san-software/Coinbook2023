using System;
namespace Coinbook
{
    partial class usrUpdates
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
            this.dlgPath = new System.Windows.Forms.FolderBrowserDialog();
            this.grpUpdates = new System.Windows.Forms.GroupBox();
            this.chkModulAutoUpdate = new SAN.Control.CheckBoxEx();
            this.grpUpdates.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgPath
            // 
            this.dlgPath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // grpUpdates
            // 
            this.grpUpdates.Controls.Add(this.chkModulAutoUpdate);
            this.grpUpdates.Location = new System.Drawing.Point(18, 17);
            this.grpUpdates.Name = "grpUpdates";
            this.grpUpdates.Size = new System.Drawing.Size(346, 43);
            this.grpUpdates.TabIndex = 4;
            this.grpUpdates.TabStop = false;
            this.grpUpdates.Text = "Updates";
            // 
            // chkModulAutoUpdate
            // 
            this.chkModulAutoUpdate.AutoSize = true;
            this.chkModulAutoUpdate.BackColorCheck = System.Drawing.Color.White;
            this.chkModulAutoUpdate.Changed = false;
            this.chkModulAutoUpdate.Column = 0;
            this.chkModulAutoUpdate.Enabled = false;
            this.chkModulAutoUpdate.ForeColorCheck = System.Drawing.SystemColors.ControlText;
            this.chkModulAutoUpdate.Location = new System.Drawing.Point(9, 19);
            this.chkModulAutoUpdate.Name = "chkModulAutoUpdate";
            this.chkModulAutoUpdate.ReadOnly = false;
            this.chkModulAutoUpdate.Row = 0;
            this.chkModulAutoUpdate.Size = new System.Drawing.Size(333, 17);
            this.chkModulAutoUpdate.TabIndex = 0;
            this.chkModulAutoUpdate.Text = "Automatische Ausführung von Modulupdates beim Programmstart";
            this.chkModulAutoUpdate.TypChecked = System.Windows.Forms.MenuGlyph.Checkmark;
            this.chkModulAutoUpdate.TypIndeterminate = System.Windows.Forms.MenuGlyph.Bullet;
            this.chkModulAutoUpdate.UseVisualStyleBackColor = true;
            this.chkModulAutoUpdate.CheckedChanged += new System.EventHandler(this.chkModulAutoUpdate_CheckedChanged);
            // 
            // usrUpdates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.grpUpdates);
            this.Name = "usrUpdates";
            this.Size = new System.Drawing.Size(378, 413);
            this.grpUpdates.ResumeLayout(false);
            this.grpUpdates.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog dlgPath;
        private System.Windows.Forms.GroupBox grpUpdates;
        private SAN.Control.CheckBoxEx chkModulAutoUpdate;
    }
}
