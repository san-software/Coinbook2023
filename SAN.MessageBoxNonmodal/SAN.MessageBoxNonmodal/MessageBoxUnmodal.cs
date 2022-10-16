using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAN.UIControls
{
    public partial class MessageBoxNonmodal : Form
    {
        private int delay = 0;

        /// <summary>
        /// Nicht modale Messagebox wird erstellt
        /// </summary>
        /// <param name="text">Anzuzeigender Text</param>
        /// <param name="caption">Überschrift der Messagebox</param>
        /// <param name="delay">Verzögerung in Sekunden,
        /// > 0 --> wird die Messagebox automatischen nach dieser Zeit geschlossen.
        /// == 0 --> Messagebox wird nur per Messagebox.Close() geschlossen</param>
        public MessageBoxNonmodal(string text, string caption, int delay =0)
        {
            InitializeComponent();

            Text = caption;
            txtAnzeige.Text = text;

            this.delay = delay;
            if (delay != 0)
                timer.Interval = delay * 1000;
        }

        /// <summary>
        /// Anzeige der Messagebox
        /// </summary>
        /// <param name="parent">Parent-Form</param>
        /// <param name="rows">Anzahl der Text-Zeilen, die angezeigt werden sollen</param>
        public void Show(Form parent, int rows)
        {
            this.Size = new Size(Width, rows * 30 + 3 * 25);

            //Left = (parent.Width - Width) / 2;
            //Top = (parent.Height - Height) / 2;

            base.Show(parent);
            Application.DoEvents();

            if (delay != 0)
            {
                timer.Enabled = true;
                timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
