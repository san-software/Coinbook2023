using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
    public class Texte
    {
        [Ignore]
        public string ID { get; set; }
        public string GUID { get; set; }
        public string Kommentar { get; set; }
        public string Rand { get; set; }
        public string Ausgabeanlass { get; set; }
        public string Material { get; set; }
        public string Legierung { get; set; }
        public string Typ { get; set; }
        public string Referenz { get; set; }
        public string Aversbeschreibung { get; set; }
        public string Besonderheit { get; set; }
        public string Reversbeschreibung { get; set; }
        public string AehnlicheMotive { get; set; }
        public string AversEntwurf { get; set; }
        public string ReversEntwurf { get; set; }
        public string Form { get; set; }
        public string Orientation { get; set; }
        public string Praegeort { get; set; }
        public string Motiv { get; set; }
        public int NationID { get; set; }
        public int AeraID { get; set; }
        public int RegionID { get; set; }

    }
}