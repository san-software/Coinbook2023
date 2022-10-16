using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDB.Database
{
    public class DumpModel
    {
        public int pageID { get; set; }
        public string pageType { get; set; }
        public long _position { get; set; }
        public string _origin { get; set; }
        public string _version { get; set; }
        public int nextPageID { get; set; }
        public int prevPageID { get; set; }
        public string collection { get; set; }

    }
}
