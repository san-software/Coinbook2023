using System;
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

            chkDebug.Checked = LogHelper.LogSettings.Debug;
            chkInfo.Checked = LogHelper.LogSettings.Info;
            chkWarning.Checked = LogHelper.LogSettings.Warn;
            chkAll.Checked = LogHelper.LogSettings.All;
            txtLogfile.Text = LogHelper.LogSettings.LogFile;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var logSettings = LogHelper.LogSettings;

            logSettings.Debug = chkDebug.Checked;
            logSettings.Info = chkInfo.Checked;
            logSettings.Warn = chkWarning.Checked;
            logSettings.All = chkAll.Checked;
            logSettings.LogFile = txtLogfile.Text;

            LogHelper.LogSettings = logSettings;

            XmlSerializer serializer = new XmlSerializer(typeof(LogModel));

            var file = "LogSettings.config";
            XmlTextWriter xmlWriter = new XmlTextWriter(file, System.Text.Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            serializer.Serialize(xmlWriter, logSettings);
            xmlWriter.Close();
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


    }
}
