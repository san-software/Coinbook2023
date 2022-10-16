using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class CoinDetail
	{
		public int ID { get; set; }
		public string Erhaltungsgrad { get; set; }
		public string Sammlung { get; set; }
		public string Doubletten { get; set; }
		public bool Liebhaberpreis { get; set; }
		public string KatalogPreis { get; set; }
		public string KaufPreis { get; set; }
		public string SammlungGesamt { get; set; }
		public string DoublettenGesamt { get; set; }
		public string LiebhaberPreisStand { get; set; }
		public Color Farbe { get; set; }

	}
}


