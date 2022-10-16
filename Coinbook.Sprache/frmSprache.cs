using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Coinbook.Database;
using System.Windows.Forms;
using Lokalisierung;

namespace Coinbook.Sprache
{
    public partial class frmSprache : Form
    {
        string language;

        public frmSprache()
        {
            InitializeComponent();

            cboSprache.DataSource = Localization.Languages;
            cboSprache.DisplayMember = "Sprache";
            cboSprache.ValueMember = "Key";
            for (int i = 0; i < cboSprache.Items.Count; i++)
                if (((ComboStruktur)cboSprache.Items[i]).Key == Localization.Language)
                    cboSprache.SelectedIndex = i;
        }

        public string Sprache { get; set; }

        private void cboSprache_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();         // Draw the default background

            ComboStruktur item = (ComboStruktur)cboSprache.Items[e.Index];

            // Get the bounds for the first column
            Rectangle r1 = e.Bounds;
            r1.Width /= 2;

            // Draw the colored 16 x 16 square
            Rectangle r = new Rectangle();
            r.X = e.Bounds.Left;
            r.Y = e.Bounds.Top;
            r.Height = e.Bounds.Height;
            r.Width = 32;
            e.Graphics.DrawImage(item.Flagge, r);

            // Get the bounds for the second column
            Rectangle r2 = e.Bounds;
            r2.X = 40;
            r2.Width = e.Bounds.Width - 40;

            // Draw the language on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(item.Sprache, e.Font, sb, r2);
        }

        private void cboSprache_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboStruktur item = (ComboStruktur)cboSprache.Items[cboSprache.SelectedIndex];

            Localization.ChangeLanguage(item.Key);
            Localization.UpdateModul(this);

            if (cboSprache.SelectedIndex == 0)
                language = "de-DE";
            else
                language = "en-US";

            Database.Instance.Execute("Update tblSettings set wert = '" + language + "' where id = 'Culture'");

            Sprache = language.Substring(0, 2).ToUpper();

            //Hide();
        }

        private void btnWeiter_Click(object sender, EventArgs e)
        {
            cboSprache_SelectedIndexChanged(null, null);
        }

    }
}
