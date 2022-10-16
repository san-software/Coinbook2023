using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class EigeneBilder
	{
		public const string Table = "tblEigeneBilder";
		public int ID { get; set; }
		public string Guid { get; set; }
		public string DateiName { get; set; }
		public bool ShowPicture { get; set; }
	}
}
