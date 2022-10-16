using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coinbook.Enumerations;

namespace Coinbook.Model
{
	public class Settings
	{
		public int ID { get; set; }
		public int Top { get; set; }
		public String Activated { get; set; }
		public int Left { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public string Culture { get; set; }
		public string MünzTab { get; set; }
		public string Nachname { get; set; }
		public string Vorname { get; set; }
		public string Strasse { get; set; }
		public string PLZ { get; set; }
		public string Ort { get; set; }
		public string Land { get; set; }
		public string Mail { get; set; }
		public int CurrentCurrency { get; set; }
		public string CurrentWährung { get; set; }
		public decimal CurrentFaktor { get; set; }
		public Boolean Exemplarsammler { get; set; }
		public enmSelectedStyle SelectedStyle { get; set; }
		public int Nation { get; set; }
		public int Ära { get; set; }
		public int Gebiet { get; set; }
		public string UpdatePath { get; set; }
		public enmMünzdetailIndex MünzdetailIndex { get; set; }
		public Boolean LastUsed { get; set; }
		public Boolean Preisvorgabe { get; set; }
		public string Ablage { get; set; }
		public string International { get; set; }
		public enmPrintDestination PrintDestination { get; set; }
		public string ReportFolder { get; set; }
		public string Lizenzkey { get; set; }
		public Boolean Maximized { get; set; }
		public Boolean NatFirst { get; set; }
		public string ColumnWidth { get; set; }
		public string Telefon { get; set; }
		public enmPreise Preise { get; set; }
		public Boolean ModulAutoUpdate { get; set; }
		public Boolean BackupByQuit { get; set; }
		public string Passwort { get; set; }
		public bool KatalognummernAnzeige { get; set; }
		public enmKatalognummern Katalognummern { get; set; }
		public string DatabaseVersion { get; set; }
		public string Programmversion { get; set; }
		public string CloudBackup { get; set; }

	}
}
