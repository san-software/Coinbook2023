using Coinbook.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Model
{
	public class Bestand
	{
		public const string Table = "tblBestand";

		public Bestand()
		{
			Farbe = enmColorFlag.None;
		}

		public int id { get; set; }
		public string Guid { get; set; }
		public int S { get; set; }
		public int SP { get; set; }
		public int SS { get; set; }
		public int SSP { get; set; }
		public int VZ { get; set; }
		public int VZP { get; set; }
		public int STN { get; set; }
		public int STH { get; set; }
		public int PP { get; set; }
		public int DS { get; set; }
		public int DSP { get; set; }
		public int DSS { get; set; }
		public int DSSP { get; set; }
		public int DVZ { get; set; }
		public int DVZP { get; set; }
		public int DSTN { get; set; }
		public int DSTH { get; set; }
		public int DPP { get; set; }
		public int NationID { get; set; }
		public int AeraID { get; set; }
		public int GebietID { get; set; }

		[Ignore]
		public decimal PS { get; set; }
		[Ignore]
		public decimal PSP { get; set; }
		[Ignore]
		public decimal PSS { get; set; }
		[Ignore]
		public decimal PSSP { get; set; }
		[Ignore]
		public decimal PVZ { get; set; }
		[Ignore]
		public decimal PVZP { get; set; }
		[Ignore]
		public decimal PSTN { get; set; }
		[Ignore]
		public decimal PSTH { get; set; }
		[Ignore]
		public decimal PPP { get; set; }
		[Ignore]
		public decimal Gesamt { get; set; }
		[Ignore]
		public string Waehrung { get; set; }
		[Ignore]
		public string Nominal { get; set; }
		[Ignore]
		public string Jahrgang { get; set; }
		[Ignore]
		public string Muenzzeichen { get; set; }
		[Ignore]
		public string KatNr { get; set; }
		[Ignore]
		public string Motiv { get; set; }
		[Ignore]
		public int Anzahl { get; set; }
		[Ignore]
		public int Erhaltung { get; set; }
		[Ignore]
		public decimal Preis { get; set; }
		[Ignore]
		public enmColorFlag Farbe { get; set; }
	}
}
