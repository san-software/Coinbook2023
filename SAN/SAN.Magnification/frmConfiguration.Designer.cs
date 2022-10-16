namespace SAN.Magnifier
{
    partial class frmMagnifierConfiguration
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
			this.cb_Symmetry = new System.Windows.Forms.CheckBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.tb_ZoomFactor = new System.Windows.Forms.TrackBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cb_RememberLastPoint = new System.Windows.Forms.CheckBox();
			this.cb_ReturnToOrigin = new System.Windows.Forms.CheckBox();
			this.lbl_ZF = new System.Windows.Forms.Label();
			this.lbl_MW = new System.Windows.Forms.Label();
			this.lbl_MH = new System.Windows.Forms.Label();
			this.tb_Width = new System.Windows.Forms.TrackBar();
			this.tb_Height = new System.Windows.Forms.TrackBar();
			this.lbl_Height = new System.Windows.Forms.Label();
			this.lbl_Width = new System.Windows.Forms.Label();
			this.lbl_ZoomFactor = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.tb_ZoomFactor)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_Width)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_Height)).BeginInit();
			this.SuspendLayout();
			// 
			// cb_Symmetry
			// 
			this.cb_Symmetry.Checked = true;
			this.cb_Symmetry.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_Symmetry.Location = new System.Drawing.Point(15, 59);
			this.cb_Symmetry.Name = "cb_Symmetry";
			this.cb_Symmetry.Size = new System.Drawing.Size(159, 20);
			this.cb_Symmetry.TabIndex = 18;
			this.cb_Symmetry.Text = "Seitenverhältnis beibehalten";
			this.cb_Symmetry.CheckedChanged += new System.EventHandler(this.cb_Symmetry_CheckedChanged);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(467, 152);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(104, 28);
			this.btnClose.TabIndex = 16;
			this.btnClose.Text = "Schliessen";
			this.btnClose.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// tb_ZoomFactor
			// 
			this.tb_ZoomFactor.Location = new System.Drawing.Point(99, 12);
			this.tb_ZoomFactor.Name = "tb_ZoomFactor";
			this.tb_ZoomFactor.Size = new System.Drawing.Size(220, 45);
			this.tb_ZoomFactor.TabIndex = 15;
			this.tb_ZoomFactor.Scroll += new System.EventHandler(this.tb_ZoomFactor_Scroll);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cb_RememberLastPoint);
			this.groupBox1.Controls.Add(this.cb_Symmetry);
			this.groupBox1.Controls.Add(this.cb_ReturnToOrigin);
			this.groupBox1.Location = new System.Drawing.Point(391, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(180, 85);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			// 
			// cb_RememberLastPoint
			// 
			this.cb_RememberLastPoint.Location = new System.Drawing.Point(15, 19);
			this.cb_RememberLastPoint.Name = "cb_RememberLastPoint";
			this.cb_RememberLastPoint.Size = new System.Drawing.Size(148, 16);
			this.cb_RememberLastPoint.TabIndex = 1;
			this.cb_RememberLastPoint.Text = "letzte Position merken";
			this.cb_RememberLastPoint.CheckedChanged += new System.EventHandler(this.cb_RememberLastPoint_CheckedChanged);
			// 
			// cb_ReturnToOrigin
			// 
			this.cb_ReturnToOrigin.Location = new System.Drawing.Point(15, 41);
			this.cb_ReturnToOrigin.Name = "cb_ReturnToOrigin";
			this.cb_ReturnToOrigin.Size = new System.Drawing.Size(148, 16);
			this.cb_ReturnToOrigin.TabIndex = 1;
			this.cb_ReturnToOrigin.Text = "Return To Origin";
			this.cb_ReturnToOrigin.Visible = false;
			this.cb_ReturnToOrigin.CheckedChanged += new System.EventHandler(this.cb_ReturnToOrigin_CheckedChanged);
			// 
			// lbl_ZF
			// 
			this.lbl_ZF.Location = new System.Drawing.Point(7, 20);
			this.lbl_ZF.Name = "lbl_ZF";
			this.lbl_ZF.Size = new System.Drawing.Size(88, 16);
			this.lbl_ZF.TabIndex = 8;
			this.lbl_ZF.Text = "Zoom Faktor";
			// 
			// lbl_MW
			// 
			this.lbl_MW.Location = new System.Drawing.Point(7, 67);
			this.lbl_MW.Name = "lbl_MW";
			this.lbl_MW.Size = new System.Drawing.Size(88, 16);
			this.lbl_MW.TabIndex = 7;
			this.lbl_MW.Text = "Lupe Breite";
			// 
			// lbl_MH
			// 
			this.lbl_MH.Location = new System.Drawing.Point(7, 115);
			this.lbl_MH.Name = "lbl_MH";
			this.lbl_MH.Size = new System.Drawing.Size(88, 16);
			this.lbl_MH.TabIndex = 10;
			this.lbl_MH.Text = "Lupe Höhe";
			// 
			// tb_Width
			// 
			this.tb_Width.Location = new System.Drawing.Point(99, 63);
			this.tb_Width.Name = "tb_Width";
			this.tb_Width.Size = new System.Drawing.Size(220, 45);
			this.tb_Width.TabIndex = 13;
			this.tb_Width.Scroll += new System.EventHandler(this.tb_Width_Scroll);
			// 
			// tb_Height
			// 
			this.tb_Height.Enabled = false;
			this.tb_Height.Location = new System.Drawing.Point(99, 111);
			this.tb_Height.Name = "tb_Height";
			this.tb_Height.Size = new System.Drawing.Size(220, 45);
			this.tb_Height.TabIndex = 12;
			this.tb_Height.Scroll += new System.EventHandler(this.tb_Height_Scroll);
			// 
			// lbl_Height
			// 
			this.lbl_Height.Location = new System.Drawing.Point(323, 119);
			this.lbl_Height.Name = "lbl_Height";
			this.lbl_Height.Size = new System.Drawing.Size(60, 16);
			this.lbl_Height.TabIndex = 20;
			this.lbl_Height.Text = "?";
			// 
			// lbl_Width
			// 
			this.lbl_Width.Location = new System.Drawing.Point(323, 67);
			this.lbl_Width.Name = "lbl_Width";
			this.lbl_Width.Size = new System.Drawing.Size(60, 16);
			this.lbl_Width.TabIndex = 19;
			this.lbl_Width.Text = "?";
			// 
			// lbl_ZoomFactor
			// 
			this.lbl_ZoomFactor.Location = new System.Drawing.Point(323, 20);
			this.lbl_ZoomFactor.Name = "lbl_ZoomFactor";
			this.lbl_ZoomFactor.Size = new System.Drawing.Size(60, 16);
			this.lbl_ZoomFactor.TabIndex = 21;
			this.lbl_ZoomFactor.Text = "?";
			// 
			// frmMagnifierConfiguration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(586, 192);
			this.ControlBox = false;
			this.Controls.Add(this.lbl_ZoomFactor);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tb_ZoomFactor);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lbl_ZF);
			this.Controls.Add(this.lbl_MW);
			this.Controls.Add(this.lbl_MH);
			this.Controls.Add(this.tb_Width);
			this.Controls.Add(this.tb_Height);
			this.Controls.Add(this.lbl_Width);
			this.Controls.Add(this.lbl_Height);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmMagnifierConfiguration";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bildschirmlupe Einstellungen";
			((System.ComponentModel.ISupportInitialize)(this.tb_ZoomFactor)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tb_Width)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tb_Height)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cb_Symmetry;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TrackBar tb_ZoomFactor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_RememberLastPoint;
        private System.Windows.Forms.CheckBox cb_ReturnToOrigin;
        private System.Windows.Forms.Label lbl_ZF;
        private System.Windows.Forms.Label lbl_MW;
        private System.Windows.Forms.Label lbl_MH;
        private System.Windows.Forms.TrackBar tb_Width;
        private System.Windows.Forms.TrackBar tb_Height;
		private System.Windows.Forms.Label lbl_Height;
		private System.Windows.Forms.Label lbl_Width;
		private System.Windows.Forms.Label lbl_ZoomFactor;
	}
}