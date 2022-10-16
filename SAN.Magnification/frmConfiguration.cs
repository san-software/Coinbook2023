using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAN.Magnifier
{
    public partial class frmMagnifierConfiguration : Form
    {
        public frmMagnifierConfiguration()
        {
            InitializeComponent();
            
            InitConfigutationSettings();
        }

        private void InitConfigutationSettings()
        {
            tb_ZoomFactor.Maximum = (int)Configuration.ZOOM_FACTOR_MAX;
            tb_ZoomFactor.Minimum = (int)Configuration.ZOOM_FACTOR_MIN;
            tb_ZoomFactor.Value = (int)MagnifierHelper.Config.ZoomFactor;

            tb_Width.Maximum = 3000;
            tb_Width.Minimum = 100;
            tb_Width.Value = MagnifierHelper.Config.MagnifierWidth;

            tb_Height.Maximum = 3000;
            tb_Height.Minimum = 100;
            tb_Height.Value = MagnifierHelper.Config.MagnifierHeight;

            lbl_ZoomFactor.Text = MagnifierHelper.Config.ZoomFactor.ToString();
            lbl_Width.Text = MagnifierHelper.Config.MagnifierWidth.ToString();
            lbl_Height.Text = MagnifierHelper.Config.MagnifierHeight.ToString();


            //--- Init Boolean Settings ---
            cb_RememberLastPoint.Checked = MagnifierHelper.Config.RememberLastPoint;
            cb_ReturnToOrigin.Checked = MagnifierHelper.Config.ReturnToOrigin;

            ShowInTaskbar = false;
        }

        private void tb_ZoomFactor_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            MagnifierHelper.Config.ZoomFactor = tb.Value;
            lbl_ZoomFactor.Text = MagnifierHelper.Config.ZoomFactor.ToString();
        }

        private void tb_Width_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            MagnifierHelper.Config.MagnifierWidth = tb.Value;
            lbl_Width.Text = MagnifierHelper.Config.MagnifierWidth.ToString();

            if (cb_Symmetry.Checked)
            {
                tb_Height.Value = tb.Value;
                MagnifierHelper.Config.MagnifierHeight = tb.Value;
                lbl_Height.Text = MagnifierHelper.Config.MagnifierHeight.ToString();
            }
        }

        private void tb_Height_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            MagnifierHelper.Config.MagnifierHeight = tb.Value;
            lbl_Height.Text = MagnifierHelper.Config.MagnifierHeight.ToString();
        }

        private void cb_Symmetry_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                // Symmetric: Don't enable
                tb_Height.Enabled = false;
            }
            else
            {
                // Non-symmetric: Allow height to be controlled independently.
                tb_Height.Enabled = true;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
						MagnifierHelper.SaveConfiguration();
            Close();
        }
        
        private void cb_RememberLastPoint_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            MagnifierHelper.Config.RememberLastPoint = cb.Checked;
        }

        private void cb_ReturnToOrigin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            MagnifierHelper.Config.ReturnToOrigin = cb.Checked;
        }

	}
}