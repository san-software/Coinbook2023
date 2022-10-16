using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Coinbook.Lokalisierung;

namespace Coinbook
{
    public partial class frmCompact : Form
    {
        public frmCompact(string sourceFile)
        {
            InitializeComponent();
            LanguageHelper.Localization.UpdateModul(this);

            //MessageBox.Show(sourceFile);

            //Text = "Aktualisiere Datenbank";

            bgwWorker.RunWorkerAsync(sourceFile);
        }

        private void bgwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string sourceFile = e.Argument.ToString();

            string destinationFile = Path.Combine(Path.GetDirectoryName(sourceFile), Path.GetFileNameWithoutExtension(sourceFile)) + ".tmp";
            string source = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourceFile + ";Jet OLEDB:Engine Type=5;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";
            string destination = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationFile + ";Jet OLEDB:Engine Type=5;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";


            dynamic JROEng = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("JRO.JetEngine"));

            try
            {
                JROEng.CompactDatabase(source, destination);

                File.Delete(sourceFile);
                File.Copy(destinationFile, sourceFile);
                File.Delete(destinationFile);
            }
            catch { }
        }

        private void bgwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }

}