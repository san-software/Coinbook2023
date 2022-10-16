
using LiteDB.Database;

namespace Coinbook
{
    public static class DatabaseHelper
    {
        static DatabaseHelper()
        {
            LiteDatabase = new Lite();
            LiteDatabase.Initialize();
        }

        public static Lite LiteDatabase { get; set; }

    }
}
