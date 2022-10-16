using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using LiteDB;
using LiteDB.Database;

namespace Coinbook
{
    public static class DatabaseHelper
    {
        static DatabaseHelper()
        {
            LiteDatabase = new Lite();
        }

        public static Lite LiteDatabase { get; set; }

    }
}
