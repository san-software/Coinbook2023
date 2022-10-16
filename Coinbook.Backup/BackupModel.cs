using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Backup
{
    [Serializable()]
    public class BackupModel
    {
        public string Program { get; set; }
        public string DataPath { get; set; }
        public string BackupPath { get; set; }
        public string TargetPath { get; set; }
        public string Language { get; set; }
        public bool AutomaticBackup { get; set; }
        public string[] Files { get; set; }
        public string License { get; set; }
        public bool Cloud { get; set; }
        public string ABO { get; set; }
        public string DownloadPath { get; set; }
    }
}
