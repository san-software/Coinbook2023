using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SAN.Logging
{
    public partial class frmLogSettings : Form
    {
        public frmLogSettings()
        {
            InitializeComponent();

            btnSave.Enabled = false;

            chkDebug.Checked = LogHelper.LogSettings.LogDebug;
            chkInfo.Checked = LogHelper.LogSettings.LogInfo;
            chkWarning.Checked = LogHelper.LogSettings.LogWarn;
            chkError.Checked = LogHelper.LogSettings.LogError;
            chkAll.Checked = LogHelper.LogSettings.All;
            chkOn.Checked = LogHelper.LogSettings.On;
            txtLogfile.Text = LogHelper.LogSettings.LogFile;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var logSettings = LogHelper.LogSettings;

            logSettings.LogDebug = chkDebug.Checked;
            logSettings.LogInfo = chkInfo.Checked;
            logSettings.LogWarn = chkWarning.Checked;
            logSettings.All = chkAll.Checked;
            logSettings.LogError = chkError.Checked;
            logSettings.LogFile = txtLogfile.Text;
            logSettings.On = chkOn.Checked;

            LogHelper.LogSettings = logSettings;
            LogHelper.SaveSettings();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                chkDebug.Checked = false;
                chkInfo.Checked = false;
                chkWarning.Checked = false;
                chkError.Checked = false;
            }
            Changed();
        }

        private void chkDebug_CheckedChanged(object sender, EventArgs e)
        {
            if(chkDebug.Checked)
                chkAll.Checked = false;
        
            Changed();
        }

        private void chkInfo_CheckedChanged(object sender, EventArgs e)
        {
            if(chkInfo.Checked)
                chkAll.Checked = false;
        
            Changed();
        }

        private void chkWarning_CheckedChanged(object sender, EventArgs e)
        {
            if(chkWarning.Checked)
                chkAll.Checked = false;
        
            Changed();
        }

        private void txtLogfile_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void Changed()
        {
            btnSave.Enabled = true;
        }

        private void chkError_CheckedChanged(object sender, EventArgs e)
        {
            if (chkError.Checked)
                chkAll.Checked = false;

            Changed();
        }

        private void chkOn_CheckedChanged(object sender, EventArgs e)
        {
            Changed();
        }
    }
}
