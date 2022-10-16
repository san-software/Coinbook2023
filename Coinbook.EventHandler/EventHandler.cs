using Coinbook.Enumerations;
using Coinbook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.EventHandlers
{
	/// <summary>
	/// Delegate 
	/// </summary>

	#region CoinEvenhandler
	public delegate void CoinEventHandler(object sender, CoinEventArgs args);

	public class CoinEventArgs : EventArgs
	{
		public CoinEventArgs(int index, Sammlung coin, int anzahl)
		{
			Coin = coin;
			Anzahl = anzahl;
			Index = index;
		}

		public Sammlung Coin { get; private set; }
		public int Anzahl { get; private set; }
		public int Index { get; private set; }
	}
	#endregion

	#region PictureEventhandler
	public delegate void PictureEventHandler(object sender, PictureEventArgs args);

	public class PictureEventArgs : EventArgs
	{
		public PictureEventArgs(EigeneBilder bild, PictureAction action)
		{
			Bild = bild;
			Action = action;
		}

		public EigeneBilder Bild {get; private set; }
		public PictureAction Action { get; private set; }
	}

	public enum PictureAction
	{
		Insert,
		Replace,
		Delete
	}
	#endregion

	#region KatalognummerEventHandler
	public delegate void KatalognummerEventHandler(object sender, KatalognummerEventArgs args);

	public class KatalognummerEventArgs : EventArgs
	{
		public KatalognummerEventArgs(enmKatalogAction action, string original, string neu)
		{
			Original = original;
			New = neu;
			Action = action;
		}

		public enmKatalogAction Action { get; private set; }
		public string Original { get; private set; }
		public string New { get; set; }
	}
	#endregion

	#region PreisEventHandler
	public delegate void PreisEventHandler(object sender, PreisEventArgs args);

	public class PreisEventArgs : EventArgs
	{
		public PreisEventArgs(string guid, decimal s, decimal sp, decimal ss, decimal ssp, decimal vz, decimal vzp, decimal stn, decimal sth, decimal pp)
		{
			Guid = guid;
			SPreis = s;
			SPPreis = sp;
			SSPreis = ss;
			SSPPreis = ssp;
			VZPreis = vz;
			VZPPreis = vzp;
			STNPreis = stn;
			STHPreis = sth;
			PPPreis = pp;
	}

		public string Guid { get; private set; }
		public decimal SPreis { get; private set; }
		public decimal SPPreis { get; private set; }
		public decimal SSPreis { get; private set; }
		public decimal SSPPreis { get; private set; }
		public decimal VZPreis { get; private set; }
		public decimal VZPPreis { get; private set; }
		public decimal STNPreis { get; private set; }
		public decimal STHPreis { get; private set; }
		public decimal PPPreis { get; private set; }
	}
	#endregion

	#region PreisStyleEventHandler
	public delegate void PreisStyleEventHandler(object sender, PreisStyleEventArgs args);

	public class PreisStyleEventArgs : EventArgs
	{
		public PreisStyleEventArgs(enmPreise preisStyle)
		{
			PreisStyle = preisStyle;
		}

		public enmPreise PreisStyle { get; private set; }
	}
	#endregion

	#region MünzdetailsEvenhandler
	public delegate void MünzdetailsEventHandler(object sender, MünzdetailsEventArgs args);

	public class MünzdetailsEventArgs : EventArgs
	{
		public MünzdetailsEventArgs()
		{ }

		public MünzdetailsEventArgs(int index, string guid, string nation, string aera, string gebiet)
		{
			Nation = nation;
			Ära = aera;
			Gebiet = gebiet;
			Guid = guid;
			Index = index;
		}

		public string Nation { get; set; }
		public string Ära { get; set; }
		public string Gebiet { get; set; }
		public string Guid { get; set; }
		public int Index { get; set; }
	}
	#endregion
}

