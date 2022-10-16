using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using SAN.Converter;
using Coinbook.Model;
using Coinbook.Lokalisierung;
using LiteDB.Database;
using Coinbook.Helper;

namespace Coinbook.Import
{
	public partial class frmImport : Form
	{
		private List<SSammlung> lSSammlung;
		List<SBezug> lSBezug;
		List<SEigeneKat> lSEigen;
		List<SZustaztexte> lSZusatz;
		List<SDoubletten> lSDoubletten;
		private int id;

		private string oldPath = String.Empty;
		private List<Sammlung> sammlung = new List<Sammlung>();
		private Lite database;
		private List<Erhaltungsgrad> Erhaltungsgrade;

		public frmImport(string sprache)
		{
			InitializeComponent();

			database = new Lite();
			string resourcePath = Path.Combine(Application.StartupPath, "Lokalisation", "Coinbook");
			LanguageHelper.CreateLocalization(resourcePath);

			LanguageHelper.Localization.UpdateModul(this);

			Erhaltungsgrade = database.ReadErhaltungsgrade(sprache);
		}

		/// <summary>
		/// Nach CB 6 Installation suchen.
		/// </summary>
		private void butSearch_Click(object sender, EventArgs e)
		{
			if (dlgFolder.ShowDialog() == DialogResult.OK)
			{
				if (dlgFolder.SelectedPath.Length > 0)
				{
					btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "Sammlung.dat"));

					if (btnImport.Enabled)
						btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "Doublett.dat"));

					if (btnImport.Enabled)
						btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "PZus.dat"));

					if (btnImport.Enabled)
						btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "CBwert.dat"));

					if (btnImport.Enabled)
						btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "EKat.dat"));

					if (btnImport.Enabled)
						btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "AukDat.dat"));

					if (btnImport.Enabled)
						btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "AukVerw.dat"));

					if (btnImport.Enabled)
						btnImport.Enabled = File.Exists(Path.Combine(dlgFolder.SelectedPath, "CBKat.dat"));
				}
			}
		}

		/// <summary>
		/// Import starten.
		/// </summary>
		private void butImport_Click(object sender, EventArgs e)
		{
			btnClose.Enabled = false;
			btnImport.Enabled = false;
			btnSearch.Enabled = false;

			oldPath = dlgFolder.SelectedPath;

			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgLoad");

			lblAnzeige.Visible = true;
			pgbProgress.Visible = true;

			bgwWorker.RunWorkerAsync();

		}

		private void bgwWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			ladeSammlung();
		}

		private void bgwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			database.ClearCollection("stblSammlung", "Sammlung");

			pgbProgress.Maximum = 100;
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgWorkCoin").Replace("{0}", "0").Replace("{1}", lSSammlung.Count.ToString());

			bgwBearbeiten.RunWorkerAsync();
		}

		private void bgwBearbeiten_DoWork(object sender, DoWorkEventArgs e)
		{
			bearbeiteMünzen();
		}

		private void bgwBearbeiten_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			pgbProgress.Value = e.ProgressPercentage;
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgWorkCoin").Replace("{0}", e.UserState.ToString()).Replace("{1}", lSSammlung.Count.ToString());
		}

		private void bgwBearbeiten_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgSaveCoin").Replace("{0}", "0").Replace("{1}", lSSammlung.Count.ToString());
			pgbProgress.Value = 0;

			bgwSaveSammlung.RunWorkerAsync();
		}

		private void bgwSaveSammlung_DoWork(object sender, DoWorkEventArgs e)
		{
			saveSammlung();
		}

		private void bgwSaveSammlung_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			pgbProgress.Value = e.ProgressPercentage;
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgSaveCoin").Replace("{0}", e.UserState.ToString()).Replace("{1}", lSSammlung.Count.ToString());
		}

		private void bgwSaveSammlung_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgWorkDoublette").Replace("{0}", "0").Replace("{1}", lSSammlung.Count.ToString());
			pgbProgress.Value = 0;

			bgwSaveDoublette.RunWorkerAsync();
		}

		private void bgwSaveDoublette_DoWork(object sender, DoWorkEventArgs e)
		{
			saveDoubletten();
		}

		private void bgwSaveDoublette_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			pgbProgress.Value = e.ProgressPercentage;
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgWorkDoublette").Replace("{0}", e.UserState.ToString()).Replace("{1}", lSSammlung.Count.ToString());
		}

		private void bgwSaveDoublette_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//Helper.RepairBestand(database);			TODO

			pgbProgress.Visible = true;
			lblAnzeige.Text = LanguageHelper.Localization.GetTranslation(Name, "msgOK");

			btnClose.Enabled = true;
		}

		/** User-Dateien lesen und Listen erstellen */
		private void ladeSammlung()
		{
			lSBezug = Bezug();
			lSSammlung = Sammlung();
			lSEigen = EigeneKatalognummer();
			lSZusatz = Zusatz();
			lSDoubletten = Doubletten();
		}

		/// <summary>
		/// Import starten.
		/// </summary>
		/// <param name="objSelectedFolder">object : Ausgewählter Speicherort der CB2006 Installation.</param>
		private void bearbeiteMünzen()
		{
			int iLocalIndex = 0;

			for (int counter = 0; counter < lSSammlung.Count; counter++)
			{
				int percent = ConvertEx.ToInt32(ConvertEx.ToDouble0(counter) / ConvertEx.ToDouble0(lSSammlung.Count) * 100);

				bgwBearbeiten.ReportProgress(percent, counter + 1);

				SSammlung s = lSSammlung[counter];
				SDoubletten d = lSDoubletten[counter];

				SZustaztexte sZ;
				try
				{
					sZ = lSZusatz[s.NzPZus - 1];
				}
				catch
				{
					sZ = new SZustaztexte();

					sZ.Ablage = String.Empty;
					sZ.Fehlerhaft = false;
					sZ.Fehlernotiz = String.Empty;
					sZ.Wo = String.Empty;
					sZ.Wem = String.Empty;
					sZ.Preis = String.Empty;
					sZ.Wann = String.Empty;
				}

				iLocalIndex = getRelIndex(counter + 1, lSBezug);

				s.ID_Katalog = getGuid(iLocalIndex);
				s.KatNrEigen = GetKatalogNr(iLocalIndex, lSEigen).Trim();

				s.Ablage = sZ.Ablage.Replace("\0", String.Empty).Trim();
				s.Fehlerhaft = sZ.Fehlerhaft;
				s.Fehlernotiz = sZ.Fehlernotiz.Trim();
				s.Kaufort = sZ.Wo.Trim();
				s.Verkäufer = sZ.Wem.Trim();
				s.Preis = sZ.Preis.Trim();
				s.Kaufdatum = sZ.Wann;

				//if (!string.IsNullOrEmpty(sZ.Wann))
				//  if (!DateTime.TryParse(sZ.Wann, out datum))
				//    s.Kaufdatum = datum.ToShortDateString();

				lSSammlung[counter] = s;

				d.ID_Katalog = getGuid(iLocalIndex);
				d.KatNrEigen = GetKatalogNr(iLocalIndex, lSEigen).Trim();

				d.Ablage = sZ.Ablage.Replace("\0", String.Empty).Trim();
				d.Fehlerhaft = sZ.Fehlerhaft;
				d.Fehlernotiz = sZ.Fehlernotiz.Trim();
				d.Kaufort = sZ.Wo.Trim();
				d.Verkäufer = sZ.Wem.Trim();
				d.Preis = sZ.Preis.Trim();
				d.Kaufdatum = sZ.Wann;

				//if (!string.IsNullOrEmpty(sZ.Wann))
				//  if (!DateTime.TryParse(sZ.Wann, out datum))
				//    d.Kaufdatum = datum.ToShortDateString();

				lSDoubletten[counter] = d;
			}
		}

		private int getRelIndex(int iIndex, List<SBezug> lSBezug)
		{
			String guid = String.Empty;

			foreach (SBezug s in lSBezug)
			{
				if (s.iIndex == iIndex)
				{
					guid = s._Guid;
					break;
				}
			}

			int iRet = 0; // database.GetCoinFromGuid(guid).ID;

			//int iRet = ConvertEx.ToInt32(database.Value("Select id from tblKatalog where RepID = '" + guid.Replace("{", String.Empty).Replace("}", String.Empty).Trim() + "'"));

			return iRet;
		}


		private string getGuid(int id)
		{
			string temp = database.GetCoinFromID(id,CoinbookHelper.ModulKey).GUID;

			//string temp = database.Text("Select RepID from tblKatalog where id =" + id.ToString());

			return temp;
		}

		//private void thrClose(String StrAbortText)
		//{
		//  EnableButtons();
		//  fStat.Close();

		//  if (thrImport != null)
		//  {
		//    if (thrImport.IsAlive)
		//      thrImport.Abort();
		//  }

		//  if (StrAbortText.Length > 0)
		//    MessageBox.Show(StrAbortText);
		//}

		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		public struct SSammlung
		{
			public int S;
			public int SS;
			public int VZ;
			public int STN;
			public int STH;
			public int PP;
			public int NzPZus;
			public int NzPZusPP;
			public decimal SpS;
			public decimal SpSS;
			public decimal SpZ;
			public decimal SpSTN;
			public decimal SpSTH;
			public decimal SpPP;
			public int Splus;
			public int SSplus;
			public int VZplus;
			public decimal PSplus;
			public decimal PSSplus;
			public decimal PVZplus;
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 5)]
			public String Mzz;
			public bool bDruckbar;
			public double Datum;
			public string ID_Katalog;
			public string Ablage;
			public string Fehlernotiz;
			public bool Fehlerhaft;
			public string KatNrEigen;
			public string Kaufdatum;
			public string Kaufort;
			public string Verkäufer;
			public string Preis;

			public int Size
			{
				//gibt Die Länge der Struktur zurück
				get
				{
					return sizeof(int) * 11
					 + sizeof(decimal) * 9
					 + sizeof(bool)
					 + sizeof(double)
					 + Mzz.Length;
				}
			}
		}

		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		public struct SDoubletten
		{
			public int S;
			public int SS;
			public int VZ;
			public int STN;
			public int STH;
			public int PP;
			public int Splus;
			public int SSplus;
			public int VZplus;

			public string ID_Katalog;
			public string Ablage;
			public string Fehlernotiz;
			public bool Fehlerhaft;
			public string KatNrEigen;
			public string Kaufdatum;
			public string Kaufort;
			public string Verkäufer;
			public string Preis;

			public int Size
			{
				//gibt Die Länge der Struktur zurück
				get
				{
					return sizeof(int) * 9;
				}
			}
		}

		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		public struct SZustaztexte
		{
			public int iStatus;
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 10)]
			public String Wann;
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 30)]
			public String Wo;
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 30)]
			public String Wem;
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 10)]
			public String Preis;
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 50)]
			public String Ablage;
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 256)]
			public String Notiezen;
			public bool Fehlerfrei;
			public bool Fehlerhaft;
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 256)]
			public String Fehlernotiz;

			public int Size
			{
				//gibt Die Länge der Struktur zurück
				get
				{
					return sizeof(int)
						+ sizeof(bool) * 2
						+ Wann.Length
						+ Wo.Length
						+ Wem.Length
						+ Preis.Length
						+ Ablage.Length
						+ Notiezen.Length
						+ Fehlernotiz.Length;
				}
			}
		}

		public struct SBezug
		{
			public String _Guid;
			public int iIndex;
		}

		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		public struct SEigeneKat
		{
			[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 5)]
			public String Nmr;
			public int iIndex;

			public int Size
			{
				//gibt Die Länge der Struktur zurück
				get
				{
					return sizeof(int)
						+ Nmr.Length;
				}
			}
		}

		/*
		 *  Sammlung auslesen
		 */
		private List<SSammlung> Sammlung()
		{
			long lLength = 0;

			List<SSammlung> sammlung = new List<SSammlung>();

			FileStream fsSammlung = new FileStream(oldPath + @"\Sammlung.dat", FileMode.Open);
			BinaryReader brSammlung = new BinaryReader(fsSammlung);

			if (brSammlung.BaseStream.Length > 0)
			{
				do
				{
					try
					{
						SSammlung s = new SSammlung();

						s.S = brSammlung.ReadInt16();
						s.SS = brSammlung.ReadInt16();
						s.VZ = brSammlung.ReadInt16();
						s.STN = brSammlung.ReadInt16();
						s.STH = brSammlung.ReadInt16();
						s.PP = brSammlung.ReadInt16();

						s.NzPZus = brSammlung.ReadInt16();
						s.NzPZusPP = brSammlung.ReadInt16();

						s.SpS = decimal.FromOACurrency(brSammlung.ReadInt64());
						s.SpSS = decimal.FromOACurrency(brSammlung.ReadInt64());
						s.SpZ = decimal.FromOACurrency(brSammlung.ReadInt64());
						s.SpSTN = decimal.FromOACurrency(brSammlung.ReadInt64());
						s.SpSTH = decimal.FromOACurrency(brSammlung.ReadInt64());
						s.SpPP = decimal.FromOACurrency(brSammlung.ReadInt64());

						s.Splus = brSammlung.ReadInt16();
						s.SSplus = brSammlung.ReadInt16();
						s.VZplus = brSammlung.ReadInt16();

						s.PSplus = decimal.FromOACurrency(brSammlung.ReadInt64());
						s.PSSplus = decimal.FromOACurrency(brSammlung.ReadInt64());
						s.PVZplus = decimal.FromOACurrency(brSammlung.ReadInt64());

						foreach (char c in brSammlung.ReadChars(5))
							s.Mzz += c.ToString();

						s.Mzz = s.Mzz.Trim();

						s.bDruckbar = brSammlung.ReadBoolean();

						s.Datum = brSammlung.ReadDouble();
						bool bEx = brSammlung.ReadBoolean();

						sammlung.Add(s);

						lLength += s.Size + sizeof(bool);
					}
					catch
					{
						break;
					}

				} while (true);
			}

			brSammlung.Close();
			fsSammlung.Close();

			return sammlung;
		}

		/*
						*   Bezugsdatei laden
						*/
		private List<SBezug> Bezug()
		{
			List<SBezug> bezug = new List<SBezug>();

			FileStream fsBezug = new FileStream(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Bezug.bin"), FileMode.Open);
			BinaryReader brBezug = new BinaryReader(fsBezug);

			int iMax = brBezug.ReadInt32();
			int iZaehler = 0;
			do
			{
				try
				{
					SBezug s;
					s._Guid = brBezug.ReadString().Trim().Replace("}", String.Empty).Replace("{", String.Empty);
					s.iIndex = brBezug.ReadInt32();
					bezug.Add(s);
				}
				catch
				{
					break;
				}
			} while (iZaehler < iMax);

			brBezug.Close();
			fsBezug.Close();

			return bezug;
		}

		/*
			 *  Doubletten auslesen
			 */
		private List<SDoubletten> Doubletten()
		{

			long lLength = 0;
			List<SDoubletten> doubletten = new List<SDoubletten>();

			FileStream fsDoubletten = new FileStream(oldPath + @"\Doublett.dat", FileMode.Open);
			BinaryReader brDoubletten = new BinaryReader(fsDoubletten);

			if (brDoubletten.BaseStream.Length > 0)
			{
				do
				{
					try
					{

						SDoubletten s = new SDoubletten();

						s.S = brDoubletten.ReadInt16();
						s.SS = brDoubletten.ReadInt16();
						s.VZ = brDoubletten.ReadInt16();
						s.STN = brDoubletten.ReadInt16();
						s.STH = brDoubletten.ReadInt16();
						s.PP = brDoubletten.ReadInt16();

						s.Splus = brDoubletten.ReadInt16();
						s.SSplus = brDoubletten.ReadInt16();
						s.VZplus = brDoubletten.ReadInt16();

						doubletten.Add(s);
						lLength += s.Size;
					}
					catch
					{
						break;
					}

				} while (true);
			}
			brDoubletten.Close();
			fsDoubletten.Close();

			return doubletten;
		}

		/*
		*  Eigene Katalognummern auslesen
		*/
		private List<SEigeneKat> EigeneKatalognummer()
		{
			long lLength = 0;

			List<SEigeneKat> eigen = new List<SEigeneKat>();

			FileStream fsEigen = new FileStream(oldPath + @"\EKat.dat", FileMode.Open);
			BinaryReader brEigen = new BinaryReader(fsEigen);

			if (brEigen.BaseStream.Length > 0)
			{
				do
				{
					try
					{

						SEigeneKat s;

						s.Nmr = String.Empty;
						foreach (char c in brEigen.ReadChars(5))
							s.Nmr += c.ToString();

						s.iIndex = brEigen.ReadInt16();

						eigen.Add(s);
						lLength += s.Size;
					}
					catch
					{
						break;
					}

				} while (true);
			}
			brEigen.Close();
			fsEigen.Close();

			return eigen;
		}

		/*
		*  Zustatztexte auslesen
		*/
		private List<SZustaztexte> Zusatz()
		{
			long lLength = 0;
			List<SZustaztexte> zusatz = new List<SZustaztexte>();

			FileStream fsZusatz = new FileStream(oldPath + @"\PZus.dat", FileMode.Open);
			BinaryReader brZusatz = new BinaryReader(fsZusatz);

			lLength = 0;
			if (brZusatz.BaseStream.Length > 0)
			{
				do
				{
					try
					{
						SZustaztexte s;

						s.iStatus = brZusatz.ReadInt16();

						s.Wann = String.Empty;
						foreach (char c in brZusatz.ReadChars(10))
							s.Wann += c.ToString();

						s.Wo = String.Empty;
						foreach (char c in brZusatz.ReadChars(30))
							s.Wo += c.ToString();

						s.Wem = String.Empty;
						foreach (char c in brZusatz.ReadChars(30))
							s.Wem += c.ToString();

						s.Preis = String.Empty;
						foreach (char c in brZusatz.ReadChars(10))
							s.Preis += c.ToString();

						s.Ablage = String.Empty;
						foreach (char c in brZusatz.ReadChars(50))
							s.Ablage += c.ToString();

						s.Notiezen = String.Empty;
						foreach (char c in brZusatz.ReadChars(256))
							s.Notiezen += c.ToString();


						bool bI = brZusatz.ReadBoolean();

						s.Fehlerfrei = brZusatz.ReadBoolean();

						bool bI1 = brZusatz.ReadBoolean();

						s.Fehlerhaft = brZusatz.ReadBoolean();

						s.Fehlernotiz = String.Empty;
						foreach (char c in brZusatz.ReadChars(256))
							s.Fehlernotiz += c.ToString();

						zusatz.Add(s);
						lLength += s.Size + sizeof(bool) * 2;
					}
					catch
					{
						break;
					}

				} while (true);
			}
			brZusatz.Close();
			fsZusatz.Close();

			return zusatz;
		}

		private void saveSammlung()
		{
			database.ClearCollection("stblSammlung","Sammlung");

			id = 0;

			for (int counter = 0; counter < lSSammlung.Count; counter++)
			{
				int percent = ConvertEx.ToInt32(ConvertEx.ToDouble0(counter) / ConvertEx.ToDouble0(lSSammlung.Count) * 100);

				bgwSaveSammlung.ReportProgress(percent, counter + 1);

				SSammlung s = lSSammlung[counter];

				//Sammlungsgrad S
				for (int i = 1; i <= s.S; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 1));
				}

				//Sammlungsgrad S+
				for (int i = 1; i <= s.Splus; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 2));
				}

				//Sammlungsgrad SS
				for (int i = 1; i <= s.SS; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 3));
				}

				//Sammlungsgrad SS+
				for (int i = 1; i <= s.SSplus; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 4));
				}

				//Sammlungsgrad VZ
				for (int i = 1; i <= s.VZ; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 5));
				}

				//Sammlungsgrad VZ+
				for (int i = 1; i <= s.VZplus; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 6));
				}

				//Sammlungsgrad STN
				for (int i = 1; i <= s.STN; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 7));
				}

				//Sammlungsgrad STH
				for (int i = 1; i <= s.STH; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 8));
				}

				//Sammlungsgrad PP
				for (int i = 1; i <= s.PP; i++)
				{
					id++;
					sammlung.Add(CreateSammlungsItem(s, id, 9));
				}
			}

			database.BulkInsertSammlung(sammlung);
		}

		private Sammlung CreateSammlungsItem(SSammlung s, int id, int erhaltung)
		{
			decimal preis = ConvertEx.ToDecimal(s.Preis);
			string kaufdatum = s.Kaufdatum;
			if (kaufdatum != String.Empty)
				kaufdatum = ConvertEx.ToDBDate("MSAccess", s.Kaufdatum);
			else
				kaufdatum = "null";

			Sammlung sItem = new Sammlung();
			sItem.ID = id;
			sItem.Guid = s.ID_Katalog;
			sItem.Ablage = s.Ablage;
			sItem.Kaufort = s.Kaufort;
			sItem.Verkaeufer = s.Verkäufer;
			sItem.Kaufpreis = preis;
			sItem.KatNrEigen = s.KatNrEigen;
			sItem.FehlerText = s.Fehlernotiz;
			sItem.Fehlerhaft = s.Fehlerhaft;
			sItem.Kaufdatum = kaufdatum;
			sItem.Erhaltungsgrad = Erhaltungsgrade[erhaltung - 1].Bezeichnung;
			sItem.Erhaltung = erhaltung;

			sItem.Doublette = false;
			sItem.EigenerPreis = 0;
			sItem.Kommentar = string.Empty;
			sItem.KatNr = database.GetCoinFromGuid(sItem.Guid, CoinbookHelper.ModulKey).KatNr;

			return sItem;
		}

		private Sammlung CreateDoublettenItem(SDoubletten s, int id, int erhaltung)
		{
			decimal preis = ConvertEx.ToDecimal(s.Preis);
			string kaufdatum = s.Kaufdatum;
			if (kaufdatum != String.Empty)
				kaufdatum = ConvertEx.ToDBDate("MSAccess", s.Kaufdatum);
			else
				kaufdatum = "null";

			Sammlung sItem = new Sammlung();
			sItem.ID = id;
			sItem.Guid = s.ID_Katalog;
			sItem.Ablage = s.Ablage;
			sItem.Kaufort = s.Kaufort;
			sItem.Verkaeufer = s.Verkäufer;
			sItem.Kaufpreis = preis;
			sItem.KatNrEigen = s.KatNrEigen;
			sItem.FehlerText = s.Fehlernotiz;
			sItem.Fehlerhaft = s.Fehlerhaft;
			sItem.Kaufdatum = kaufdatum;
			sItem.Erhaltungsgrad = Erhaltungsgrade[erhaltung - 1].Bezeichnung;
			sItem.Erhaltung = erhaltung;

			sItem.Doublette = true;
			sItem.EigenerPreis = 0;
			sItem.Kommentar = string.Empty;
			sItem.KatNr = database.GetCoinFromGuid(sItem.Guid,CoinbookHelper.ModulKey).KatNr;

			return sItem;
		}

		private void saveDoubletten()
		{
			for (int counter = 0; counter < lSDoubletten.Count; counter++)
			{
				int percent = ConvertEx.ToInt32(ConvertEx.ToDouble0(counter) / ConvertEx.ToDouble0(lSDoubletten.Count) * 100);

				bgwSaveDoublette.ReportProgress(percent, counter + 1);

				SDoubletten s = lSDoubletten[counter];

				double preis = ConvertEx.ToDouble0(s.Preis);
				string kaufdatum = s.Kaufdatum;
				if (kaufdatum != String.Empty)
					kaufdatum = ConvertEx.ToDBDate("MSAccess", s.Kaufdatum);
				else
					kaufdatum = "null";

				//Sammlungsgrad S
				for (int i = 1; i <= s.S; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 1));
				}

				//Sammlungsgrad S+
				for (int i = 1; i <= s.Splus; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 2));
				}

				//Sammlungsgrad SS
				for (int i = 1; i <= s.SS; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 3));
				}

				//Sammlungsgrad SS+
				for (int i = 1; i <= s.SSplus; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 4));
				}

				//Sammlungsgrad VZ
				for (int i = 1; i <= s.VZ; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 5));
				}

				//Sammlungsgrad VZ+
				for (int i = 1; i <= s.VZplus; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 6));
				}

				//Sammlungsgrad STN
				for (int i = 1; i <= s.STN; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 7));
				}

				//Sammlungsgrad STH
				for (int i = 1; i <= s.STH; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 8));
				}

				//Sammlungsgrad PP
				for (int i = 1; i <= s.PP; i++)
				{
					id++;
					sammlung.Add(CreateDoublettenItem(s, id, 9));
				}
			}
			database.BulkInsertSammlung(sammlung);
		}

		private string GetKatalogNr(int iIndex, List<SEigeneKat> lSEigen)
		{
			//bool bExists = false;
			//int i = 0;
			//foreach (SEigeneKat s in lSEigen)
			//{
			//  if (s.iIndex == iIndex)
			//  {
			//    DataRow[] dr = this.fromOleDb.Database.Tables["tblEigeneKatNr"].Select(this.fromOleDb.Database.Tables["tblEigeneKatNr"].Columns[1].ColumnName
			//      + " = '" + s.Nmr + "'");

			//    if (dr.Length > 0)
			//    {
			//      try
			//      {
			//        i = Convert.ToInt32(dr[0][0]);
			//        bExists = true;
			//      }
			//      catch { }
			//    }
			//    if (!bExists)
			//    {
			//      object[] objInsert = new object[2];
			//      objInsert[1] = s.Nmr;
			//      this.fromOleDb.Database.Tables["tblEigeneKatNr"].Rows.Add(objInsert);
			//      i = this.fromOleDb.Database.Tables["tblEigeneKatNr"].Rows.Count;
			//    }
			//  }
			//}

			return String.Empty;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
