using LiteDB.Database;
using System.Linq;

namespace Coinbook.Sprache
{
    public class Language
    {
        Lite LiteDatabase = new Lite();
        string sprache = string.Empty;

        public void StartChangeLanguage(string sprache)
        {
            frmProgress form = new frmProgress(sprache);
            form.ShowDialog();
        }
    }
}
