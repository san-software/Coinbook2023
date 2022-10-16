namespace Coinbook
{
    partial class frmPicture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPicture));
            this.lblBilderWahl = new System.Windows.Forms.Label();
            this.lstPicture = new System.Windows.Forms.ListBox();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnDeleteImage = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.chkAnzeige = new System.Windows.Forms.CheckBox();
            this.btnGetImage = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBilderWahl
            // 
            this.lblBilderWahl.AutoSize = true;
            this.lblBilderWahl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBilderWahl.Location = new System.Drawing.Point(9, 9);
            this.lblBilderWahl.Name = "lblBilderWahl";
            this.lblBilderWahl.Size = new System.Drawing.Size(83, 13);
            this.lblBilderWahl.TabIndex = 1;
            this.lblBilderWahl.Text = "Installierte Bilder";
            // 
            // lstPicture
            // 
            this.lstPicture.BackColor = System.Drawing.Color.Gainsboro;
            this.lstPicture.FormattingEnabled = true;
            this.lstPicture.Location = new System.Drawing.Point(12, 25);
            this.lstPicture.Name = "lstPicture";
            this.lstPicture.Size = new System.Drawing.Size(336, 225);
            this.lstPicture.Sorted = true;
            this.lstPicture.TabIndex = 3;
            this.lstPicture.SelectedIndexChanged += new System.EventHandler(this.lstPicture_SelectedIndexChanged);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = ".jpg|*.jpg|.bmp|*.bmp|.gif|*.gif|Alle|*.*";
            this.dlgOpenFile.ShowReadOnly = true;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadImage.Location = new System.Drawing.Point(207, 275);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(164, 23);
            this.btnLoadImage.TabIndex = 7;
            this.btnLoadImage.Text = "Bild einfügen";
            this.btnLoadImage.UseVisualStyleBackColor = false;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteImage.Location = new System.Drawing.Point(377, 275);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(137, 23);
            this.btnDeleteImage.TabIndex = 8;
            this.btnDeleteImage.Text = "Bild löschen";
            this.btnDeleteImage.UseVisualStyleBackColor = false;
            this.btnDeleteImage.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.Color.Gainsboro;
            this.picBox.Location = new System.Drawing.Point(362, 25);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(354, 240);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 9;
            this.picBox.TabStop = false;
            // 
            // chkAnzeige
            // 
            this.chkAnzeige.AutoSize = true;
            this.chkAnzeige.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAnzeige.Location = new System.Drawing.Point(12, 281);
            this.chkAnzeige.Name = "chkAnzeige";
            this.chkAnzeige.Size = new System.Drawing.Size(146, 17);
            this.chkAnzeige.TabIndex = 10;
            this.chkAnzeige.Text = "Originalbild nicht anzeigen";
            this.chkAnzeige.UseVisualStyleBackColor = true;
            this.chkAnzeige.Click += new System.EventHandler(this.chkAnzeige_Click);
            // 
            // btnGetImage
            // 
            this.btnGetImage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnGetImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetImage.Location = new System.Drawing.Point(520, 275);
            this.btnGetImage.Name = "btnGetImage";
            this.btnGetImage.Size = new System.Drawing.Size(164, 23);
            this.btnGetImage.TabIndex = 11;
            this.btnGetImage.Text = "Bild übernehmen";
            this.btnGetImage.UseVisualStyleBackColor = false;
            this.btnGetImage.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(362, 314);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(164, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(728, 355);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGetImage);
            this.Controls.Add(this.chkAnzeige);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.btnDeleteImage);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.lstPicture);
            this.Controls.Add(this.lblBilderWahl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPicture";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Eigene Bilder importieren";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBilderWahl;
        private System.Windows.Forms.ListBox lstPicture;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnDeleteImage;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.CheckBox chkAnzeige;
				private System.Windows.Forms.Button btnGetImage;
				private System.Windows.Forms.Button btnCancel;
    }
}