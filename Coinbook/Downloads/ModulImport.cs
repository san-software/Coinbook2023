using System;
using System.Xml;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using SAN.FTP;
using SAN.Converter;
using Coinbook.Model;
using SAN.Control;
using Coinbook.Enumerations;
using System.Linq;
using System.Globalization;
using Coinbook.Helper;

namespace Coinbook
{

	public class ModulImport
	{
        private FTPClass ftpClass = new FTPClass();
        private BackgroundWorkerEx bgw;
		private string key;
		private int canceled = 0;
		private string host;
		private string user;
		private string passwort;
		public event ProgressChangedEventHandler ModulProcess;
		public event ProgressChangedEventHandler TableProcess;
		public event ProgressChangedEventHandler ModulReady;
		public event ProgressChangedEventHandler DownloadReady;

        Lizenz lizenz = new Lizenz();
        List<Downloads> lizenzierteModule = new List<Downloads>();

        public ModulImport()
		{
			this.host = ftpClass.FTPParameter.URL;
			this.user = ftpClass.FTPParameter.Admin;
			this.passwort = ftpClass.FTPParameter.Passwort;
		}

		public bool Import()
		{
			bool result = true;

			string[] files = Directory.GetFiles(CoinbookHelper.UpdatePath, "*.*");
			foreach (string file in files)
				File.Delete(file);

            //liste = Helper.LiteDatabase.ReadDownloads();

   			if (ftpClass.Connect(host, user, passwort))
			{
				bgw = new BackgroundWorkerEx();

				bgw.WorkerReportsProgress = true;
				bgw.WorkerSupportsCancellation = true;

				bgw.DoWork += Bgw_DoWork;
				bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;
				bgw.ProgressChanged += Bgw_ProgressChanged;

				bgw.RunWorkerAsync();
			}
			else
			{
				result = false;
			}

			return result;
		}

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            lizenz.LizenzDownload();
            lizenz.ReadModulLizenzen();

            var module = DatabaseHelper.LiteDatabase.ReadModulLizenzen();

            lizenzierteModule.Clear();
            foreach (var m in module)
            {
                if (Convert.ToInt32(m.Jahr.Substring(0, 4)) >= 2017)
                {
                    Downloads item = new Downloads();
                    item.ID = m.id;
                    item.Jahr = m.Jahr;
                    item.Lizenz = m.Lizenz;
                    item.Bezeichnung = m.Lizenz;
                    item.Key = m.Lizenz;
                    lizenzierteModule.Add(item);
                }
            }

            var downloads = DatabaseHelper.LiteDatabase.ReadDownloads();
            foreach (var m in downloads)
            {
                var l = lizenzierteModule.First(x => x.ID == m.ID);
                l.Datum = m.Datum;
                l.Lizenz = m.Jahr;
                l.OldLizenz = m.OldLizenz;
            }

            string host = "www.coinbook.de";
            string user = "ftp12564714-Admin";
            string passwort = "Magixx-1";

            if (!ftpClass.Connect(host, user, passwort))
            {
                //MessageBoxAdv.Show("Keine Verbindung zum Internet");     TODO
                //Close();
                //return;
            }

            for (int i = lizenzierteModule.Count - 1; i >= 0; i--)
            {
                var item = lizenzierteModule[i];
                string file1 = item.Key.Replace(" ", "_");
                file1 = file1.Replace("ä", "ae");
                file1 = file1.Replace("Ö", "Oe");
                file1 = file1 + "-" + item.Jahr + ".zip";

                item.Url = "Downloads/Module/" + item.Jahr + "/" + file1;
                item.Target = Path.Combine(CoinbookHelper.DownloadPath, item.Key + "-" + item.Jahr + ".zip");

                if (item.Jahr.Equals(item.OldLizenz) && !string.IsNullOrEmpty(item.Datum))
                {
                    var newDate = ftpClass.GetModifiedTime(item.Url);
                    if (newDate <= Convert.ToDateTime(item.Datum))
                        lizenzierteModule.Remove(item);
                }
            }

            if (lizenzierteModule.Count == 0)
            {
                //MessageBoxAdv.Show("Alle Module sind schon heruntergeladen");            TODO
                //Close();
            }

            int k = 0;

            lizenzierteModule = lizenzierteModule.OrderBy(x => x.Key).ToList();

            foreach (var item in lizenzierteModule)
            {
                key = item.Lizenz;
                string jahr = item.Jahr;
                string datum = item.Datum;
                string bezeichnung = item.Bezeichnung;
                string oldLizenz = item.OldLizenz;

                int p = Convert.ToInt32(k / (double)lizenzierteModule.Count * 100);
                k++;

                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessModul, "Download von " + Path.GetFileNameWithoutExtension(item.Target)));
                enmFTPFile result = ftpClass.Download(item.Url, item.Target);
                if (result != enmFTPFile.FileDownloadOK)
                {
                    File.Delete(item.Target);
                    continue;
                }

                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessModul, "Importiere " + Path.GetFileNameWithoutExtension(item.Target)));

                canceled++;

                if (!bgw.CancellationPending)
                {
                    ArchivHelper.DecompressFile(item.Target, CoinbookHelper.UpdatePath, "", "magixx-1-xxigam");

                    import();

                    importPictures();
                    File.Delete(item.Target);

                    item.Datum = DateTime.Now.ToShortDateString();
                    item.OldLizenz = item.Jahr;

                    DatabaseHelper.LiteDatabase.SaveDownloads(item);

                    bgw.ReportProgress(p, new xxxx(ModulImportEnum.ReadyModul, "Modul " + item.Key + " wurde importiert"));
                }
                else
                {
                    CoinbookHelper.ClearPath(CoinbookHelper.UpdatePath);
                    CoinbookHelper.ClearPath(CoinbookHelper.DownloadPath);
                    break;
                }
            }
        }

		private void Bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			xxxx action = (xxxx)e.UserState;

			switch (action.Typ)
			{
				case ModulImportEnum.ProcessModul:
					if (ModulProcess != null)
					{
						ProgressChangedEventArgs ee = new ProgressChangedEventArgs(e.ProgressPercentage, action.Text);
						ModulProcess(this, ee);
					}
					break;

				case ModulImportEnum.ProcessTable:
					if (TableProcess != null)
					{
						ProgressChangedEventArgs ee = new ProgressChangedEventArgs(e.ProgressPercentage, action.Text);
						TableProcess(this, ee);
					}
					break;

				case ModulImportEnum.ReadyModul:
					if (ModulReady != null)
					{
						ProgressChangedEventArgs ee = new ProgressChangedEventArgs(e.ProgressPercentage, action.Text);
						ModulReady(this, ee);
					}
					break;

				case ModulImportEnum.ProcessPicture:
					if (TableProcess != null)
					{
						ProgressChangedEventArgs ee = new ProgressChangedEventArgs(e.ProgressPercentage, action.Text);
						TableProcess(this, ee);
					}
					break;
			}
		}

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string text;
            ProgressChangedEventArgs ee;

            if (bgw.CancellationPending)
            {
                text = "Das automatische Modul-Update wurde abgebrochen." + Environment.NewLine
                        + "Beim nächsten Start des Programms werden die restlichen noch nicht geladenen Module weiter herunter geladen";
                ee = new ProgressChangedEventArgs(0, text);
                DownloadReady(this, ee);
            }
            else if (canceled > 0)
            {
                text = "Alle erworbenen Module wurden aktualisiert";
                ee = new ProgressChangedEventArgs(0, text);
                DownloadReady(this, ee);
            }
        }

        private void import()
		{
            int modul = CoinbookHelper.GetModulID;

            string[] files = Directory.GetFiles(CoinbookHelper.UpdatePath,"*.xml");

            foreach (string file in files)
            {
                if (file.Contains("tblAera"))
                    importAera(Path.GetFileName(file), modul);

                if (file.Contains("tblGebiet"))
                    importRegion(Path.GetFileName(file), modul);

                if (file.Contains("tblModule"))
                    importModule(Path.GetFileName(file), modul);

                if (file.Contains("tblPrägeanstalt"))
                    importPraegeanstalt(Path.GetFileName(file), modul);

                if (file.Contains("tblCB_DB"))
                    importKatalog(Path.GetFileName(file), modul);

                if (file.Contains("tblTexte"))
                {
                    importTexte(Path.GetFileName(file), modul, "DE");
                    importTexte(Path.GetFileName(file), modul, "EN");
                    File.Delete(file);
                }
            }

            //Helper.LiteDatabase.SaveDownloads(modul, lizenz, newDate);         TODO
        }

		//private void decompressPictures()
		//{
		//	string[] files = Directory.GetFiles(Helper.UpdatePath, "*.zip");

		//	foreach (string file in files)
		//	{
		//		Helper.DecompressFile(file, Helper.UpdatePath,"", "magixx-1-xxigam");

		//		string[] pictures = Directory.GetFiles(Helper.UpdatePath, "*.jpg");

		//		double k = 0;
		//		bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Bilder"));

		//		foreach (string picture in pictures)
		//		{
		//			k++;
		//			int p = Convert.ToInt32(k / (double)pictures.Length * 100);
		//			bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessPicture, "Bilder"));

		//			try
		//			{
		//				if (File.Exists(Path.Combine(Helper.Picturepath, Path.GetFileName(picture))))
		//					File.Delete(Path.Combine(Helper.Picturepath, Path.GetFileName(picture)));
		//				File.Move(picture, Path.Combine(Helper.Picturepath, Path.GetFileName(picture)));
		//			}
		//			catch { }
		//		}

		//		File.Delete(file);
		//	}
		//}

		public bool IsRunning
		{
			get
			{
				bool result = false;
				if (bgw != null)
					result = bgw.IsBusy;

				return result;
			}
		}

		public void CancelUpdate()
		{
			bgw.CancelAsync();
			canceled = 1;
		}

        private void importAera(string file, int nationID)
        {
            List<Aera> aeras = new List<Aera>();

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Lösche Ära"));

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(CoinbookHelper.UpdatePath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblAera");

            int countTable = 0;

            foreach (XmlNode node in nodes)
            {
                countTable++;
                int p = Convert.ToInt32(countTable / (double)nodes.Count * 100);
                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessTable, "tblAera"));

                Aera aera = new Aera();
                aera.ID = ConvertEx.ToInt32(node.SelectSingleNode("ID").InnerText);
                aera.Bezeichnung = node.SelectSingleNode("DE_DE").InnerText;
                aera.NationID = ConvertEx.ToInt32(node.SelectSingleNode("NAT").InnerText);
                aera.Sortierung = ConvertEx.ToInt32(node.SelectSingleNode("Sortierung").InnerText);

                aeras.Add(aera);
            }

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Speichere Ära"));
            DatabaseHelper.LiteDatabase.BulkUpsertAera(aeras,CoinbookHelper.ModulKey);

            File.Delete(Path.Combine(CoinbookHelper.UpdatePath, file));
        }

        private void importRegion(string file, int nationID)
        {
            List<Gebiet> gebiete = new List<Gebiet>();

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(CoinbookHelper.UpdatePath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblGebiet");

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Lösche Gebiete"));

            double k = 0;

            foreach (XmlNode node in nodes)
            {
                k++;
                int p = Convert.ToInt32(k / (double)nodes.Count * 100);
                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessTable, "Lade Gebiete"));

                Gebiet region = new Gebiet();
                region.ID = ConvertEx.ToInt32(node.SelectSingleNode("ID").InnerText);
                region.Bezeichnung = node.SelectSingleNode("DE_DE").InnerText;
                region.NationID = ConvertEx.ToInt32(node.SelectSingleNode("NAT").InnerText);
                region.Sortierung = ConvertEx.ToInt32(node.SelectSingleNode("Sortierung").InnerText);
                region.AeraID = ConvertEx.ToInt32(node.SelectSingleNode("Aera").InnerText);

                gebiete.Add(region);
            }

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Speichere Gebiete"));
            DatabaseHelper.LiteDatabase.BulkUpsertRegion(gebiete, CoinbookHelper.ModulKey);

            File.Delete(Path.Combine(CoinbookHelper.UpdatePath, file));
        }

        private void importKatalog(string file, int nationID)
        {
            List<Katalog2> liste = new List<Katalog2>();
            List<MünzDetail> details = new List<MünzDetail>();
            List<Texte> texte = new List<Texte>();

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(CoinbookHelper.UpdatePath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblCB_DB");

            double k = 0;
            foreach (XmlNode node in nodes)
            {
                k++;
                int p = Convert.ToInt32(k / (double)nodes.Count * 100);
                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessTable, "Lade Katalog"));

                Katalog2 katalog = new Katalog2();
                MünzDetail detail = new MünzDetail();
                Texte text = new Texte();

                katalog.NationID = ConvertEx.ToInt32(node.SelectSingleNode("Nation_ID").InnerText);
                katalog.AeraID = ConvertEx.ToInt32(node.SelectSingleNode("Aera_ID").InnerText);
                katalog.RegionID = ConvertEx.ToInt32(node.SelectSingleNode("Gebiet_ID").InnerText);

                if (node.SelectSingleNode("Jahrgang") != null)
                    katalog.Jahrgang = node.SelectSingleNode("Jahrgang").InnerText;
                else
                    katalog.Jahrgang = string.Empty;

                if (node.SelectSingleNode("KatNr") != null)
                    katalog.KatNr = node.SelectSingleNode("KatNr").InnerText;
                else
                    katalog.KatNr = string.Empty;

                katalog.SPreis = decimal.Parse(node.SelectSingleNode("Erh_S_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.SPPreis = decimal.Parse(node.SelectSingleNode("Erh_SP_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.SSPreis = decimal.Parse(node.SelectSingleNode("Erh_SS_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.SSPPreis = decimal.Parse(node.SelectSingleNode("Erh_SSP_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.VZPreis = decimal.Parse(node.SelectSingleNode("Erh_VZ_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.VZPPreis = decimal.Parse(node.SelectSingleNode("Erh_VZP_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.STNPreis = decimal.Parse(node.SelectSingleNode("Erh_STN_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.STHPreis = decimal.Parse(node.SelectSingleNode("Erh_STH_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.PPPreis = decimal.Parse(node.SelectSingleNode("Erh_PP_Preis").InnerText, CultureInfo.InvariantCulture);
                katalog.Auflage = node.SelectSingleNode("Auflage").InnerText;
                katalog.AuflageSTH = node.SelectSingleNode("AuflageSTH").InnerText;
                katalog.AuflagePP = node.SelectSingleNode("AuflagePP").InnerText;
                katalog.GUID = node.SelectSingleNode("RepID").InnerText;
                katalog.ID = node.SelectSingleNode("RepID").InnerText;

                if (node.SelectSingleNode("Münzzeichen") != null)
                    katalog.Muenzzeichen = node.SelectSingleNode("Münzzeichen").InnerText;
                else
                    katalog.Muenzzeichen = string.Empty;

                if (node.SelectSingleNode("Nominal") != null)
                    katalog.Nominal = node.SelectSingleNode("Nominal").InnerText;
                else
                    katalog.Nominal = string.Empty;

                if (node.SelectSingleNode("Währung") != null)
                    katalog.Waehrung = node.SelectSingleNode("Währung").InnerText;
                else
                    katalog.Waehrung = string.Empty;

                if (node.SelectSingleNode("Motiv") != null)
                    katalog.Motiv = node.SelectSingleNode("Motiv").InnerText;
                else
                    katalog.Motiv = "";

                if (node.SelectSingleNode("Picture") != null)
                    katalog.Picture = node.SelectSingleNode("Picture").InnerText;
                else
                    katalog.Picture = "";

                if (node.SelectSingleNode("LPS") != null)
                    katalog.LPS = ConvertEx.ToBoolean(node.SelectSingleNode("LPS").InnerText);
                else
                    katalog.LPS = false;

                if (node.SelectSingleNode("LPSP") != null)
                    katalog.LPSP = ConvertEx.ToBoolean(node.SelectSingleNode("LPSP").InnerText);
                else
                    katalog.LPSP = false;

                if (node.SelectSingleNode("LPSS") != null)
                    katalog.LPSS = ConvertEx.ToBoolean(node.SelectSingleNode("LPSS").InnerText);
                else
                    katalog.LPSS = false;

                if (node.SelectSingleNode("LPSSP") != null)
                    katalog.LPSSP = ConvertEx.ToBoolean(node.SelectSingleNode("LPSSP").InnerText);
                else
                    katalog.LPSSP = false;

                if (node.SelectSingleNode("LPVZ") != null)
                    katalog.LPVZ = ConvertEx.ToBoolean(node.SelectSingleNode("LPVZ").InnerText);
                else
                    katalog.LPVZ = false;

                if (node.SelectSingleNode("LPVZP") != null)
                    katalog.LPVZP = ConvertEx.ToBoolean(node.SelectSingleNode("LPVZP").InnerText);
                else
                    katalog.LPVZP = false;

                if (node.SelectSingleNode("LPSTN") != null)
                    katalog.LPSTN = ConvertEx.ToBoolean(node.SelectSingleNode("LPSTN").InnerText);
                else
                    katalog.LPSTN = false;

                if (node.SelectSingleNode("LPSTH") != null)
                    katalog.LPSTH = ConvertEx.ToBoolean(node.SelectSingleNode("LPSTH").InnerText);
                else
                    katalog.LPSTH = false;

                if (node.SelectSingleNode("LPPP") != null)
                    katalog.LPPP = ConvertEx.ToBoolean(node.SelectSingleNode("LPPP").InnerText);
                else
                    katalog.LPPP = false;

                //------------------------
                detail.Gewicht = decimal.Parse(node.SelectSingleNode("Gewicht").InnerText, CultureInfo.InvariantCulture);
                detail.Durchmesser = node.SelectSingleNode("Durchmesser").InnerText;
                detail.Dicke = decimal.Parse(node.SelectSingleNode("Dicke").InnerText, CultureInfo.InvariantCulture);
                detail.NationID = ConvertEx.ToInt32(node.SelectSingleNode("Nation_ID").InnerText);
                detail.GUID = node.SelectSingleNode("RepID").InnerText;
                detail.ID = node.SelectSingleNode("RepID").InnerText;
                detail.AeraID = ConvertEx.ToInt32(node.SelectSingleNode("Aera_ID").InnerText);
                detail.GebietID = ConvertEx.ToInt32(node.SelectSingleNode("Gebiet_ID").InnerText);

                if (node.SelectSingleNode("AusserKurs") != null)
                    detail.AusserKurs = node.SelectSingleNode("AusserKurs").InnerText;
                else
                    detail.AusserKurs = string.Empty;

                if (node.SelectSingleNode("InKurs") != null)
                    detail.InKurs = node.SelectSingleNode("InKurs").InnerText;
                else
                    detail.InKurs = string.Empty;

                if (node.SelectSingleNode("geprägt") != null)
                    detail.Gepraegt = node.SelectSingleNode("geprägt").InnerText;
                else
                    detail.Gepraegt = string.Empty;

                if (node.SelectSingleNode("Material") != null)
                    detail.Material = node.SelectSingleNode("Material").InnerText;
                else
                    detail.Material = "";

                if (node.SelectSingleNode("Legierung") != null)
                    detail.Legierung = node.SelectSingleNode("Legierung").InnerText;
                else
                    detail.Legierung = "";

                if (node.SelectSingleNode("Form") != null)
                    detail.Form = node.SelectSingleNode("Form").InnerText;
                else
                    detail.Form = "";

                if (node.SelectSingleNode("Orientation") != null)
                    detail.Orientation = node.SelectSingleNode("Orientation").InnerText;
                else
                    detail.Orientation = "";

                if (node.SelectSingleNode("Referenz") != null)
                    detail.Referenz = node.SelectSingleNode("Referenz").InnerText;
                else
                    detail.Referenz = "";

                if (node.SelectSingleNode("LPStandS") != null)
                    detail.LPStandS = node.SelectSingleNode("LPStandS").InnerText;
                else
                    detail.LPStandS = "";

                if (node.SelectSingleNode("LPStandSP") != null)
                    detail.LPStandSP = node.SelectSingleNode("LPStandSP").InnerText;
                else
                    detail.LPStandSP = "";

                if (node.SelectSingleNode("LPStandSS") != null)
                    detail.LPStandSS = node.SelectSingleNode("LPStandSS").InnerText;
                else
                    detail.LPStandSS = "";

                if (node.SelectSingleNode("LPStandSSP") != null)
                    detail.LPStandSSP = node.SelectSingleNode("LPStandSSP").InnerText;
                else
                    detail.LPStandSSP = "";

                if (node.SelectSingleNode("LPStandVZ") != null)
                    detail.LPStandVZ = node.SelectSingleNode("LPStandVZ").InnerText;
                else
                    detail.LPStandVZ = "";

                if (node.SelectSingleNode("LPStandVZP") != null)
                    detail.LPStandVZP = node.SelectSingleNode("LPStandVZP").InnerText;
                else
                    detail.LPStandVZP = "";

                if (node.SelectSingleNode("LPStandSTN") != null)
                    detail.LPStandSTN = node.SelectSingleNode("LPStandSTN").InnerText;
                else
                    detail.LPStandSTN = "";

                if (node.SelectSingleNode("LPStandSTH") != null)
                    detail.LPStandSTH = node.SelectSingleNode("LPStandSTH").InnerText;
                else
                    detail.LPStandSTH = "";

                if (node.SelectSingleNode("LPStandPP") != null)
                    detail.LPStandPP = node.SelectSingleNode("LPStandPP").InnerText;
                else
                    detail.LPStandPP = "";

                if (node.SelectSingleNode("AusserkursFlag") != null)
                    detail.AusserKursBool = ConvertEx.ToBoolean(node.SelectSingleNode("AusserkursFlag").InnerText);
                else
                    detail.AusserKursBool = false;

                if (node.SelectSingleNode("BearbeitungsDatum") != null)
                    detail.Bearbeitungsdatum = node.SelectSingleNode("BearbeitungsDatum").InnerText;
                else
                    detail.Bearbeitungsdatum = "";

                //----------------------------
                text.ID = node.SelectSingleNode("RepID").InnerText;
                text.GUID = node.SelectSingleNode("RepID").InnerText;
                text.NationID = ConvertEx.ToInt32(node.SelectSingleNode("Nation_ID").InnerText);

                if (node.SelectSingleNode("Aversbeschreibung") != null)
                    text.Aversbeschreibung = node.SelectSingleNode("Aversbeschreibung").InnerText;
                else
                    text.Aversbeschreibung = string.Empty;

                if (node.SelectSingleNode("Besonderheit") != null)
                    text.Besonderheit = node.SelectSingleNode("Besonderheit").InnerText;
                else
                    text.Besonderheit = string.Empty;

                if (node.SelectSingleNode("Reversbeschreibung") != null)
                    text.Reversbeschreibung = node.SelectSingleNode("Reversbeschreibung").InnerText;
                else
                    text.Reversbeschreibung = string.Empty;

                if (node.SelectSingleNode("Kommentar") != null)
                    text.Kommentar = node.SelectSingleNode("Kommentar").InnerText;
                else
                    text.Kommentar = string.Empty;

                if (node.SelectSingleNode("Rand") != null)
                    text.Rand = node.SelectSingleNode("Rand").InnerText;
                else
                    text.Rand = "";

                if (node.SelectSingleNode("Ausgabeanlass") != null)
                    text.Ausgabeanlass = node.SelectSingleNode("Ausgabeanlass").InnerText;
                else
                    text.Ausgabeanlass = "";

                if (node.SelectSingleNode("ÄhnlicheMotive") != null)
                    text.AehnlicheMotive = node.SelectSingleNode("ÄhnlicheMotive").InnerText;
                else
                    text.AehnlicheMotive = "";

                if (node.SelectSingleNode("AversEntwurf") != null)
                    text.AversEntwurf = node.SelectSingleNode("AversEntwurf").InnerText;
                else
                    text.AversEntwurf = "";

                if (node.SelectSingleNode("ReversEntwurf") != null)
                    text.ReversEntwurf = node.SelectSingleNode("ReversEntwurf").InnerText;
                else
                    text.ReversEntwurf = "";

                if (node.SelectSingleNode("Typ") != null)
                    text.Typ = node.SelectSingleNode("Typ").InnerText;
                else
                    text.Typ = "";

                if (node.SelectSingleNode("Prägeort") != null)
                    text.Praegeort = node.SelectSingleNode("Prägeort").InnerText;
                else
                    text.Praegeort = "";

                liste.Add(katalog);
                details.Add(detail);
                texte.Add(text);
            }

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Speichere Katalog"));
            DatabaseHelper.LiteDatabase.UpsertKatalog(liste, CoinbookHelper.ModulKey);
            DatabaseHelper.LiteDatabase.BulkUpsertDetails(details, CoinbookHelper.ModulKey);
            DatabaseHelper.LiteDatabase.BulkUpsertTexteDE(texte, CoinbookHelper.ModulKey);

            File.Delete(Path.Combine(CoinbookHelper.UpdatePath, file));
        }

        private void importModule(string file, int nationID)
        {
            List<Modul> module = new List<Modul>();

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(CoinbookHelper.UpdatePath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblModule");

            double k = 0;

            foreach (XmlNode node in nodes)
            {
                k++;
                int p = Convert.ToInt32(k / (double)nodes.Count * 100);
                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessTable, "Lade Modultexte"));

                Modul item = new Modul();
                item.ModulID = ConvertEx.ToInt32(node.SelectSingleNode("id").InnerText);
                item.Typ = node.SelectSingleNode("typ").InnerText;
                item.Sprache = node.SelectSingleNode("sprache").InnerText;
                item.Text = node.SelectSingleNode("text").InnerText;
                item.NationID = ConvertEx.ToInt32(node.SelectSingleNode("Nation").InnerText);

                module.Add(item);
            }

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Speichere Modultexte"));
            DatabaseHelper.LiteDatabase.BulkUpsertModule(module, CoinbookHelper.ModulKey);

            File.Delete(Path.Combine(CoinbookHelper.UpdatePath, file));
        }

        private void importPraegeanstalt(string file, int nationID)
        {
            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(CoinbookHelper.UpdatePath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblPrägeanstalt");

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Lösche Prägeanstalten"));

            double k = 0;
            foreach (XmlNode node in nodes)
            {
                k++;
                int p = Convert.ToInt32(k / (double)nodes.Count * 100);
                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessTable, "Lade Prägeanstalten"));

                Praegeanstalt item = new Praegeanstalt();
                item.ID = ConvertEx.ToInt32(node.SelectSingleNode("id").InnerText);
                item.Nation = ConvertEx.ToInt32(node.SelectSingleNode("Nation").InnerText);
                item.Muenzzeichen = node.SelectSingleNode("Münzzeichen").InnerText;
                item.Ort = node.SelectSingleNode("Ort").InnerText;
                item.Adresse = node.SelectSingleNode("Adresse").InnerText;
                item.Email = node.SelectSingleNode("Email").InnerText;
                item.Homepage = node.SelectSingleNode("Homepage").InnerText;

                if (node.SelectSingleNode("Bemerkung") != null)
                    item.Bemerkung = node.SelectSingleNode("Bemerkung").InnerText;

                if (node.SelectSingleNode("Caption") != null)
                    item.Land = node.SelectSingleNode("Land").InnerText;

                if (node.SelectSingleNode("Land") != null)
                    item.Land = node.SelectSingleNode("Land").InnerText;

                if (node.SelectSingleNode("Zeit") != null)
                    item.Zeit = node.SelectSingleNode("Zeit").InnerText;

                bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Speichere Prägeanstalten"));
                DatabaseHelper.LiteDatabase.SavePraegeanstalt(item, CoinbookHelper.ModulKey);

                File.Delete(Path.Combine(CoinbookHelper.UpdatePath, file));
            }
        }

        private void importTexte(string file, int nationID, string sprache)
        {
            Dictionary<string, Texte> texte = new Dictionary<string, Texte>();
            Texte item;

            XmlDocument document = new XmlDocument();
            document.Load(Path.Combine(CoinbookHelper.UpdatePath, file));

            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList nodes = root.SelectNodes("tblTexte");

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Lade Texte " + sprache));

            double k = 0;

            foreach (XmlNode node in nodes)
            {
                k++;
                int p = Convert.ToInt32(k / (double)nodes.Count * 100);
                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessTable, "Lade Texte " + sprache));

                if (node.SelectSingleNode("sprache").InnerText == sprache)
                {
                    string guid = node.SelectSingleNode("guid").InnerText;

                    if (texte.ContainsKey(guid))
                        item = texte[guid];
                    else
                    {
                        item = new Texte { GUID = guid, NationID = nationID, ID = guid };
                        texte.Add(guid, item);
                    }

                    switch (node.SelectSingleNode("typ").InnerText.Replace("Ä", "Ae").Replace("ä", "ae").Replace("_", ""))
                    {
                        case "Aversbeschreibung":
                            item.Aversbeschreibung = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Besonderheit":
                            item.Besonderheit = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Reversbeschreibung":
                            item.Reversbeschreibung = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Kommentar":
                            item.Kommentar = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Rand":
                            item.Rand = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Ausgabeanlass":
                            item.Ausgabeanlass = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Material":
                            item.Material = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Legierung":
                            item.Legierung = node.SelectSingleNode("text").InnerText;
                            break;

                        case "AehnlicheMotive":
                            item.AehnlicheMotive = node.SelectSingleNode("text").InnerText;
                            break;

                        case "AversEntwurf":
                            item.AversEntwurf = node.SelectSingleNode("text").InnerText;
                            break;

                        case "ReversEntwurf":
                            item.ReversEntwurf = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Form":
                            item.Form = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Orientation":
                            item.Orientation = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Typ":
                            item.Typ = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Referenz":
                            item.Referenz = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Praegeort":
                            item.Praegeort = node.SelectSingleNode("text").InnerText;
                            break;

                        case "Motiv":
                            item.Motiv = node.SelectSingleNode("text").InnerText;
                            break;
                    }
                }
            }

            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Speichere Texte " + sprache));
            if (sprache == "DE")
            {
                DatabaseHelper.LiteDatabase.BulkUpsertTexteDE(texte.Values.ToList(), CoinbookHelper.ModulKey);
            }
            else
            {
                DatabaseHelper.LiteDatabase.BulkUpsertTexteEN(texte.Values.ToList(), CoinbookHelper.ModulKey);
            }
        }

        private void importPictures()
        {
            bgw.ReportProgress(0, new xxxx(ModulImportEnum.ProcessTable, "Entpacke Bilder"));

            var files = Directory.GetFiles(CoinbookHelper.UpdatePath, "*.zip");
            string file =files[0];

            ArchivHelper.DecompressFile(file, CoinbookHelper.UpdatePath,"","magixx-1-xxigam");
            File.Delete(file);

            files = Directory.GetFiles(CoinbookHelper.UpdatePath, "*.*");

            for (int i = 0; i < files.Length; i++)
            {
                int p = Convert.ToInt32(i / (double)files.Length * 100);
                bgw.ReportProgress(p, new xxxx(ModulImportEnum.ProcessPicture, "Kopiere Bilder"));

                try
                {
                    if (File.Exists(Path.Combine(CoinbookHelper.Picturepath, Path.GetFileName(files[i]))))
                        File.Delete(Path.Combine(CoinbookHelper.Picturepath, Path.GetFileName(files[i])));

                File.Move(files[i], Path.Combine(CoinbookHelper.Picturepath, Path.GetFileName(files[i])));
                }
                catch { }
            }
        }
    }

	public class xxxx
	{
		public xxxx(ModulImportEnum typ, string text)
		{
			Typ = typ;
			Text = text;
		}

		public ModulImportEnum Typ { get; set; }
		public string Text { get; set; }
	}
}
