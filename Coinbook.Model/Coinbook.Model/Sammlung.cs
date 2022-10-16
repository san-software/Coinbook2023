using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Sammlung
	{
		public const string Table = "tblSammlung";

		public int ID { get; set; }
		public int Erhaltung { get; set; }
		public bool Doublette { get; set; }
		public string Ablage { get; set; }
		public string Guid { get; set; }
		public string Kaufdatum { get; set; }
		public string Kaufort { get; set; }
		public string Verkaeufer { get; set; }
		public string Kommentar { get; set; }
		public bool Fehlerhaft { get; set; }
		public string FehlerText { get; set; }
		public string KatNrEigen { get; set; }
		public string Picture { get; set; }
		public bool ShowPicture { get; set; }
		public Decimal Kaufpreis { get; set; }
		public int NationID { get; set; }

		[Ignore]
		public Decimal Katalogpreis { get; set; }

		[Ignore]
		public string Erhaltungsgrad { get; set; }

		[Ignore]
		public string KatNr { get; set; }

		[Ignore]
		public int Farbe { get; set; }

		[Ignore]
		public Decimal EigenerPreis { get; set; }


	}
}
