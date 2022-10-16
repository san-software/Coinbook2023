using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coinbook.Enumerations;

namespace Coinbook.Model
{
	public class Beschreibung
	{
		public const string Table = "tblTexte";
        
        [IgnoreID]
		public string Guid { get; set; }

        [IgnoreID]
        public int NationID { get; set; }

        [IgnoreID]
        public enmTexte Typ { get; set; }

        [Ignore]
        public string Sprache { get; set; }
		public string Text { get; set; }
	}
}
