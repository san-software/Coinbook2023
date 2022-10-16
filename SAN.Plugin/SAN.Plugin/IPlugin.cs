
using System.Windows.Forms;

namespace SAN.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        void Initialize(string lizenzInfo);
        void Run(string text = null);
        ToolStripMenuItem MenuEntry { get; }
        object Host { get; set; }
        bool HasMenu { get; }
        bool HasLicense { get; }
        string BackupPath { get; set; }
        string UpdatePath { get; set; }
        string Lizenz { get; set; }
        string Programm { get; set; }
        string DataPath { get; set; }

        void Dispose();

    }

    public interface IPluginHost
    {
        void Feedback(string Feedback, IPlugin Plugin);
    }
}
