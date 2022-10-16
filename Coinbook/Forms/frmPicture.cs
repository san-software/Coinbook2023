using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Coinbook.Model;
using Coinbook.Helper;
using Coinbook.EventHandlers;

namespace Coinbook
{
    public partial class frmPicture : Form
    {
        List<string> ownPictures = new List<string>();
        EigeneBilder bild;
        public event PictureEventHandler ChangePicture;

        public frmPicture()
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);

            btnGetImage.Enabled = false;
            btnDeleteImage.Enabled = false;

            base.ControlBox = true;
            base.MinimizeBox = true;
            base.MaximizeBox = true;
        }

        public string Picture { get; set; }
        public bool Anzeige { get; set; }
        public string Guid { get; set; }

        public new void ShowDialog()
        {
            bild = DatabaseHelper.LiteDatabase.GetOwnPicture(Guid);
            Anzeige = bild.ShowPicture;
            Picture = bild.DateiName;

            int id = -1;

            ownPictures = DatabaseHelper.LiteDatabase.ReadOwnPictures();
            foreach (string item in ownPictures)
                lstPicture.Items.Add(item);

            if (Picture != String.Empty)
            {
                id = lstPicture.FindString(Picture, 0);
                if (id == -1)
                    id = 0;
                else if (id > lstPicture.Items.Count - 1)
                    id = -1;
            }

            if (lstPicture.Items.Count != 0)
                lstPicture.SelectedIndex = id;

            chkAnzeige.Checked = Anzeige;

            base.ShowDialog();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                btnLoadImage.Enabled = false;
                btnDeleteImage.Enabled = false;
                btnGetImage.Enabled = false;

                int anzahl = lstPicture.Items.Count;
                //int id = Database.Database.Instance.GetSequence("tblEigeneBilder", "id");

                ownPictures.Add(dlgOpenFile.FileName);
                lstPicture.Items.Add(dlgOpenFile.FileName);

                Cursor = Cursors.Default;
                btnLoadImage.Enabled = true;
                btnDeleteImage.Enabled = true;
                btnGetImage.Enabled = true;

                lstPicture.SelectedIndex = lstPicture.Items.IndexOf(dlgOpenFile.FileName);
            }
        }

        private void lstPicture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPicture.SelectedIndex != -1)
            {
                string file = lstPicture.Items[lstPicture.SelectedIndex].ToString();
                if (File.Exists(file))
                {
                    Bitmap image = new Bitmap(file);
                    picBox.Image = image;
                }
                else
                    picBox.Image = null;

                btnGetImage.Enabled = true;
                btnDeleteImage.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            btnLoadImage.Enabled = false;
            btnDeleteImage.Enabled = false;
            btnGetImage.Enabled = false;

            int anzahl = lstPicture.Items.Count;
            int index = lstPicture.SelectedIndex;

            bild=DatabaseHelper.LiteDatabase.DeleteOwnPicture(lstPicture.SelectedItem.ToString());

            picBox.Image = null;

            lstPicture.Items.RemoveAt(lstPicture.SelectedIndex);

            if (ChangePicture != null)
                ChangePicture(this, new PictureEventArgs(bild, PictureAction.Delete));

            Picture = "";

            btnLoadImage.Enabled = true;
            btnGetImage.Enabled = true;
            btnDeleteImage.Enabled = false;

            Cursor = Cursors.Default;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Picture=lstPicture.SelectedItem.ToString();

            if (bild == null)
                bild = new EigeneBilder();

            bild.DateiName=lstPicture.SelectedItem.ToString();
            bild.Guid = Guid;
            bild.ShowPicture = Anzeige;

            PictureAction action = (bild.ID == 0 ? PictureAction.Insert : PictureAction.Replace);

            DatabaseHelper.LiteDatabase.SaveOwnPicture(bild);

            if (ChangePicture != null)
                ChangePicture(this, new PictureEventArgs(bild,action));

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void chkAnzeige_Click(object sender, EventArgs e)
        {
            Anzeige = chkAnzeige.Checked;
        }

    }
}
