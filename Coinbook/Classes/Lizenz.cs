using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using OleDB;
using System.IO;
using SAN.Converter;
using Rhino.Licensing;
using System.Windows.Forms;
using System.Threading;
using Coinbook.Enumerations;
using Coinbook.Model;
using Coinbook.Lokalisierung;
using Syncfusion.Windows.Forms;
using Splash;
using Coinbook.Helper;
using Coinbook.Activation;

namespace Coinbook
{
	class Lizenz : IDisposable
	{
		//public Lizenz()
		//{
		//  String cmd = String.Empty;

		//  OleDBZugriff Database.Database.Instance = new OleDBZugriff();
		//  Database.Database.Instance.Tabelle="Lizenz";

		//  Database.Database.Instance.Execute("Delete * from tblSettings2");

		//  //string[] files= Directory.GetFiles(OleDBConnection.Datapath,"*.liz", SearchOption.TopDirectoryOnly);

		//  //  cmd = "Select id from tblNation where Bezeichnung ='" + files[i].Substring(0,files[i].Length -4).Substring(files[i].LastIndexOf(@"\")+1).Replace("_"," ").Replace("ae","ä") + "'";
		//  //  string id = Database.Database.Instance.Text(cmd);

		//  //  cmd = "Insert into tblSettings2 (id,Lizenz) values(" + id +",2015)";
		//  //  Database.Database.Instance.Execute(cmd);
		//  //}
		//}

		public void ReadModulLizenzen()
		{
			Boolean result = true;
			string text = String.Empty;
			string file = String.Empty;
			string cmd = String.Empty;
			int id = 0;
			string f = "yyy";

			try
			{
				Restart = false;

				DatabaseHelper.LiteDatabase.ClearCollection("tblSettings2");

				//prüfen ob public key vorhanden ist
				file = Path.Combine(CoinbookHelper.DataPath, "publicKey.xml");
                f = file;
                if (!File.Exists(file))
				{
					text = LanguageHelper.Localization.GetTranslation("Lizenz", "msgNoFile");

					SplashScreen.CloseForm();
					MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					Application.Exit();
				}

                var publicKey = File.ReadAllText(file);

				//Lizenz suchen und falls vorhanden runterladen
				file = Path.Combine(CoinbookHelper.DataPath, "modul.lic");
				f = file;
                if (!File.Exists(file))
				{
					LizenzVerwaltung l = new LizenzVerwaltung();
					l.Email = "san-software@gmx.de".ToLower();
					l.DataPath = CoinbookHelper.DataPath;
					if (!l.LoadLizenz)
					{
						text = LanguageHelper.Localization.GetTranslation("Lizenz", "msgNoFile");

						SplashScreen.CloseForm();
						MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
						Application.Exit();
					}
					else
					{
						text = LanguageHelper.Localization.GetTranslation("Lizenz", "msgInstalled");

						SplashScreen.CloseForm();
						MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						Application.Restart();
						return;
					}
				}

                LicenseValidator licenseValidator = new LicenseValidator(publicKey, file);
				try
				{
					licenseValidator.AssertValidLicense();
				}
				catch
				{
					if (Environment.UserName != "san-software" && Environment.UserName != "san-s")
					{
						text = LanguageHelper.Localization.GetTranslation("Lizenz", "msgNoFile");

						//SplashScreen.CloseForm();
						CoinbookHelper.StartProgram("Coinbook.Activate", enmPrograms.NoLicense.ToString());


						Restart = true;
						Application.Exit();
						return;
					}
				}

                if (licenseValidator.Name != Application.ProductName)
				{
					text = LanguageHelper.Localization.GetTranslation("Lizenz", "msgNoFile");

					SplashScreen.CloseForm();
					MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					Application.Exit();
					return;
				}

				f = CoinbookHelper.Settings.Lizenzkey;

                CoinbookHelper.Settings.Lizenzkey = licenseValidator.UserId.ToString();
				List<LizenzSettings> settings = new List<LizenzSettings>();

				var nations = DatabaseHelper.LiteDatabase.ReadNationen(true);

                foreach (KeyValuePair<string, string> pair in licenseValidator.LicenseAttributes)
				{
                    switch (pair.Key)
					{
						case "Vorname":
							CoinbookHelper.Settings.Vorname = pair.Value;
							break;

						case "Nachname":
							CoinbookHelper.Settings.Nachname = pair.Value;
							break;

						case "Strasse":
							CoinbookHelper.Settings.Strasse = pair.Value;
							break;

						case "PLZ":
							CoinbookHelper.Settings.PLZ = pair.Value;
							break;

						case "Ort":
							CoinbookHelper.Settings.Ort = pair.Value;
							break;

						case "EMail":
							CoinbookHelper.Settings.Mail = pair.Value;
							break;

						case "id":
							CoinbookHelper.Settings.Lizenzkey = pair.Value;
							break;

						case "Version":
							break;

						case "Land":
							break;

						case "Serial":
							string[] serial = pair.Value.Split(new char[] { ',' });
							bool serialFound = false;
							for (int i = 0; i < serial.Length; i++)
								if (serial[i] == CoinbookHelper.CPUID || serial[i] == "x")
									serialFound = true;
                            //serialFound = true;
                            MessageBox.Show("Serial");

                            if (!serialFound)
							{
								if (Environment.UserName != "san-software" && Environment.UserName != "san-s")
								{
                                    SplashScreen.CloseForm();

                                    MessageBox.Show("Serial 3");

                                    frmNoLicense form = new frmNoLicense();
                                    form.Value = pair.Value;
                                    form.Art = enmAktivierungsArt.Wrong;
                                    form.ShowDialog();


                                    //CoinbookHelper.StartProgram("Coinbook.Activate", enmPrograms.NoLicense.ToString() + " " + enmAktivierungsArt.Wrong.ToString()); // +  " " + pair.Value;
									Environment.Exit(1);
								}
							}
							break;

						case "Abo":
							CoinbookHelper.Settings.CloudBackup = pair.Value;
							CoinbookHelper.Abo = pair.Value;
							break;

						default:
							LizenzSettings item = new LizenzSettings();

							string sql = pair.Key.Replace("_", " ");
							if (sql == "EU 2 Euro Modul")
								sql = "EU 2 Euro-Modul";

							var nation1 = nations.FirstOrDefault(n => n.Key == sql);

							if (nation1 != null)
							{
								item.ID = nation1.ID;
								item.Nation = nation1.Bezeichnung;
								item.Jahr = pair.Value;
								item.Key = nation1.Key;

								settings.Add(item);
								id++;
							}
							break;
					}

					if (!result)
						break;
				}

                MessageBox.Show("ReadModulLizenzen 3");

                f = "//Speichere  Settings2";
                //Speichere  Settings2
                List<Settings2> liste = new List<Settings2>();
				foreach (LizenzSettings item in settings)
				{
					Settings2 s = new Settings2();
					s.id = item.ID;
					s.Lizenz = item.Nation;
					s.Jahr = item.Jahr;
					s.Key = item.Key;

					liste.Add(s);
				}
				DatabaseHelper.LiteDatabase.BulkInsertSettings2(liste);

				//Update tblNation
				//foreach (LizenzSettings item in settings)
				//{
				//	var nation = Helper.LiteDatabase.GetNation(item.ID);
				//	nation.InUse = true;
				//	Helper.LiteDatabase.UpdateNation(nation);
				//}

				Activated = enmAktivierung.aktiviert;

                MessageBox.Show("ReadModulLizenzen 4");

                DatabaseHelper.LiteDatabase.UpdateSettings(CoinbookHelper.Settings);

				//if (Environment.UserName.ToLower() != "san-software")
				//{
				DateTime expiration = licenseValidator.ExpirationDate.Date;
				int tage = (expiration - DateTime.Now).Days;

				if (tage <= 0)
				{
					CoinbookHelper.StartProgram("Coinbook.Activate", enmPrograms.NoLicense.ToString() + " " + enmAktivierungsArt.Expired.ToString());
				}
				else if (tage <= 30)
				{
					if (CoinbookHelper.Settings.Activated == String.Empty)
					{
						text = LanguageHelper.Localization.GetTranslation("Lizenz", "msgAktivieren2").Replace("{0}", expiration.ToString()).Replace("{1}", tage.ToString());

						if (MessageBoxAdv.Show(text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
						{
							CoinbookHelper.StartProgram("Coinbook.Activate", enmPrograms.NoLicense.ToString() + " " + enmAktivierungsArt.Initial.ToString());
						}
						Activated = enmAktivierung.warten;
					}
					else
					{
						//text = "Sehr geehrter Anwender," + Environment.NewLine + Environment.NewLine + "Ihre Lizenz ist für Coinbook wurde noch nicht aktiviert." + Environment.NewLine + Environment.NewLine
						//  + "Das Programm wartet noch auf die Aktivierungs-Bestätigung vom Coinbook-Verlag." + Environment.NewLine + Environment.NewLine
						//  + "Während dieser Zeit können Sie selbstverständlich Coinbook in vollem Umfang nutzen." + Environment.NewLine + Environment.NewLine
						//  + "Sollten die Aktivierung jedoch nicht bis zum " + expiration + " erfolgen, können Sie ab da mit Coinbook nicht mehr weiterarbeiten, bis Sie die Aktivierung erfolgt ist." + Environment.NewLine + Environment.NewLine
						//  + "Anschliessend an die Aktivierung können Sie Coinbook selbstverständlich in vollem Umfang für eine unbegrenzte Zeit nutzen." + Environment.NewLine + Environment.NewLine
						//  + "Wenn jetzt die Frist kurz vor dem Ablauf ist, sollten Sie sich mit unserem Support in Verbindung setzen.";
						//MessageBoxAdv.Show(text, "Coinbbok", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						Activated = enmAktivierung.warten;
					}
				}

				Thread.Sleep(1000);

				if (!result)
					return;
			}
			catch (SystemException e)
			{
                //MessageBox.Show(e.Message + " " + f + " " + e.InnerException);
				MessageBox.Show(e.Message + " " + f + " " + e.InnerException);
            }
        }

		public enmAktivierung Activated { get; set; }
		public Boolean Restart { get; set; }

		public Dictionary<string, string> Module
		{
			get
			{
				Dictionary<string, string> module = new Dictionary<string, string>();

				string file = CoinbookHelper.DataPath + @"\publicKey.xml";
				//if (!File.Exists(file))
				//  return;
				var publicKey = File.ReadAllText(file);

				file = CoinbookHelper.DataPath + @"\modul.lic";
				//if (!File.Exists(file))
				//  return;

				var licenseValidator = new LicenseValidator(publicKey, file);
				licenseValidator.AssertValidLicense();

				foreach (KeyValuePair<string, string> pair in licenseValidator.LicenseAttributes)
				{
					switch (pair.Key)
					{
						case "Vorname":
						case "Nachname":
						case "Strasse":
						case "PLZ":
						case "Ort":
						case "EMail":
						case "Version":
							break;

						default:
							module.Add(pair.Key, pair.Value);
							break;
					}
				}
				return module;
			}
		}

		public bool LizenzDownload()
		{
			LizenzVerwaltung l = new LizenzVerwaltung();
			l.Email = "san-software@gmx.de".ToLower();
			l.Lizenzdatei = Path.Combine(CoinbookHelper.DataPath, "modul.lic");
			l.URL = "http://www.Coinbook.de/Downloads/Personalisierung/" + CoinbookHelper.Settings.Lizenzkey;
			return l.LoadLizenz;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public string ExpireDate
		{
			get
			{
				string file = Path.Combine(CoinbookHelper.DataPath, "publicKey.xml");
				var publicKey = File.ReadAllText(file);

				file = Path.Combine(CoinbookHelper.DataPath, "modul.lic");

				var licenseValidator = new LicenseValidator(publicKey, file);
				licenseValidator.AssertValidLicense();

				return licenseValidator.ExpirationDate.ToShortDateString();
			}
		}
    }

    public class LizenzSettings
	{
		public int ID { get; set; }
		public string Nation { get; set; }
		public string Jahr { get; set; }
		public string Key { get; set; }
	}
}