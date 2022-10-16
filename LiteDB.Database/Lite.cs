using Coinbook.Enumerations;
using Coinbook.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Environment;

namespace LiteDB.Database
{
    public delegate void LiteEventHandler(object sender, LiteEventArgs args);

    /// </summary>
    public class LiteEventArgs : EventArgs
    {
        #region Constructors
        public LiteEventArgs(int counter, int max)
        {
            Counter = counter;
            Max = max;
        }
        #endregion

        #region Public Interface
        public int Counter { get; set; }
        public int Max { get; set; }
        #endregion
    }
    public class Lite : IDisposable
    {
        private LiteDatabase db = null;

        //public event LiteEventHandler ReportProgress;
        private string logFile = string.Empty;
        DatabaseConfig config = null;

        public Lite()
        {
            Initialize();
        }

        public void Initialize()
        {
            config = null;

            XmlSerializer serializer = new XmlSerializer(typeof(DatabaseConfig));
            var file = "database.config";
            var x = File.Exists(file);

            using (Stream reader = new FileStream(file, FileMode.Open))
                config = (DatabaseConfig)serializer.Deserialize(reader);

            DataPath = config.Datapath;
            if (DataPath.StartsWith("CommonApplicationData"))
                DataPath = DataPath.Replace("CommonApplicationData", GetFolderPath(SpecialFolder.CommonApplicationData));

            //connectionstring = $"Filename={Path.Combine(DataPath, config.DataSource)}";

            //logFile = Path.Combine(DataPath, Path.GetFileNameWithoutExtension(config.DataSource) +"-log" + Path.GetExtension(config.DataSource));

            //ftp.Passwort = Decrypt(ftp.Passwort);
            //ftp.TransferPasswort = "magixx-1";
        }

        public string DataPath { get; set; }

        //private string Decrypt(string text)
        //{
        //    return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        //}

        //Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }

        public string Connectionstring(string database = null)
        {
            string connectionstring;

            if (string.IsNullOrEmpty(database))
            {
                connectionstring = $"Filename={Path.Combine(DataPath, config.DataSource)}";
                logFile = Path.Combine(DataPath, Path.GetFileNameWithoutExtension(config.DataSource) + "-log" + Path.GetExtension(config.DataSource));
            }
            else
            {
                if (database == "Sammlung")
                    connectionstring = $"{Path.Combine(DataPath, Path.GetFileNameWithoutExtension(config.DataSource))}-{database}{Path.GetExtension(config.DataSource)}";
                else
                    connectionstring = Path.Combine(DataPath, "Katalog", $"{database}{Path.GetExtension(config.DataSource)}");

                logFile = Path.Combine(DataPath, "katalog", $"{database}-log{Path.GetExtension(config.DataSource)}");
            }

            return connectionstring;
        }

        public string ConnectionstringAllgemein(string database)
        {
            string file = Path.Combine(database, "allgemein.db");
            string connectionstring;

            connectionstring = $"Filename={file}";
            logFile = Path.GetFileNameWithoutExtension(file) + "-log"; // + Path.GetExtension(config.DataSource));
            
            return connectionstring;
        }

        public string ConnectionstringDaten
        {
            get => @"Filename=C:\ProgramData\Coinbook\Coinbook{0}.db";
        }

        public void Close()
        {

        }

        public void InsertNation(Nation nation)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Nation>("tblNation");

                if (collection.Exists(Query.EQ("_ID", nation.ID)))
                    collection.Update(nation);
                else
                    collection.Insert(nation);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void InsertAera(Aera aera, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Aera>("tblAera");
                if (collection.Exists(Query.EQ("_id", aera.ID)))
                    collection.Update(aera);
                else
                    collection.Insert(aera);

                collection.EnsureIndex(x => x.ID);

            }
        }

        public void InsertRegion(Gebiet gebiet, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Gebiet>("tblGebiet");
                if (collection.Exists(Query.EQ("_id", gebiet.ID)))
                    collection.Update(gebiet);
                else
                    collection.Insert(gebiet);

                collection.EnsureIndex(x => x.ID);

            }
        }

        public void InsertKatalog(Katalog katalog, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Katalog>("tblkatalog");
                if (collection.Exists(Query.EQ("_id", katalog.ID)))
                    collection.Update(katalog);
                else
                    collection.Insert(katalog);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.GUID);
                collection.EnsureIndex(x => x.NationID);
                collection.EnsureIndex(x => x.AeraID);
                collection.EnsureIndex(x => x.GebietID);
                collection.EnsureIndex(x => x.KatNr);
            }
        }

        public void UpdateSettings(Settings settings)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings>("tblSettings");
                collection.Update(settings);
            }
        }

        public void BulkInsertKatalog(List<Katalog2> katalog, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Katalog2>("tblkatalog");
                collection.InsertBulk(katalog);
                collection.EnsureIndex(x => x.GUID);
                collection.EnsureIndex(x => x.NationID);
                collection.EnsureIndex(x => x.AeraID);
                collection.EnsureIndex(x => x.RegionID);
                collection.EnsureIndex(x => x.Jahrgang);
                collection.EnsureIndex(x => x.Waehrung);
                collection.EnsureIndex(x => x.Nominal);
            }
        }

        public void BulkInsertRegion(List<Gebiet> gebiet, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Gebiet>("tblGebiet");
                collection.InsertBulk(gebiet);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkInsertAera(List<Aera> aera, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Aera>("tblAera");
                collection.InsertBulk(aera);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkInsertNation(List<Nation> nation, string sprache = "")
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Nation>($"tblNation{sprache}");
                collection.InsertBulk(nation);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkInsertAuktionen(List<Auktion> auktionen)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                collection.InsertBulk(auktionen);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkInsertPraegeanstalt(List<Praegeanstalt> praegeanstalt, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Praegeanstalt>("tblPraegeanstalt");
                collection.InsertBulk(praegeanstalt);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkInsertTexte(List<Beschreibung> beschreibung, string tabelle, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Beschreibung>(tabelle);
                collection.InsertBulk(beschreibung);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.Typ);
            }
        }

        public void BulkInsertModule(List<Modul> module)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Modul>("tblModule");
                collection.InsertBulk(module);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.NationID);
                collection.EnsureIndex(x => x.ModulID);
            }
        }

        public void BulkUpsertErhaltungsgrade(List<Erhaltungsgrad> erhaltungsgrade)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                db.DropCollection("tblErhaltungsgrad");
                var collection = db.GetCollection<Erhaltungsgrad>("tblErhaltungsgrad");
                collection.InsertBulk(erhaltungsgrade);

                //collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkUpsertCulture(List<Culture> culture)
        {

            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                db.DropCollection("tblCulture");

                var collection = db.GetCollection<Culture>("tblCulture");
                foreach (var item in culture)
                    collection.Insert(item);

                //collection.EnsureIndex(x => x.ID);
            }
        }

        public void InsertSettings(Settings settings)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings>("tblSettings");
                collection.Insert(settings);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkInsertParameter(List<Parameter> parameter)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Parameter>("tblParameter");
                collection.InsertBulk(parameter);
                collection.EnsureIndex(x => x.id);
            }
        }

        public void BulkInsertBestand(List<Bestand> bestand)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                collection.InsertBulk(bestand);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.AeraID);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void BulkInsertEigeneBilder(List<EigeneBilder> eigeneBilder)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                collection.InsertBulk(eigeneBilder);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkInsertEigeneKatNr(List<EigeneKatNr> eigeneKatNr)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                collection.InsertBulk(eigeneKatNr);

                collection.EnsureIndex(x => x.Coinbook);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void BulkInsertPreise(List<Preise> preise)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                collection.InsertBulk(preise);

                collection.EnsureIndex(x => x.GUID);
            }
        }

        public bool CollectionExists(string name, string database = null)
        {
            bool result = false;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<DumpModel>("$dump").Query().Where("pageType = 'Collection'").ToDocuments();

                foreach (var item in collection)
                {
                    if (item["collection"].ToString().Replace("\"", "") == name)
                        result = true;
                }
            }

            return result;
        }

        public void BulkInsertSammlung(List<Sammlung> sammlung)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                collection.InsertBulk(sammlung);

                collection.EnsureIndex(x => x.Guid);
            }
        }

        public void BulkInsertSettings2(List<Settings2> settings2)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings2>("tblSettings2");
                collection.InsertBulk(settings2);

                collection.EnsureIndex(x => x.id);
            }
        }

        public void BulkInsertTexteDE(List<Texte> katalog, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>("tblDetailTexteDE");
                collection.InsertBulk(katalog);

                collection.EnsureIndex(x => x.GUID);
            }
        }

        public void BulkInsertTexteEN(List<Texte> katalog, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>("tblDetailTexteEN");
                collection.InsertBulk(katalog);

                collection.EnsureIndex(x => x.GUID);
            }
        }

        public void BulkInsertDownloads(List<Downloads> katalog)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Downloads>("tblDownloads");
                collection.InsertBulk(katalog);

                collection.EnsureIndex(x => x.ID);

            }
        }

        //public void BulkInsertCurrency(List<WaehrungAuswahl> katalog, string database)
        //{
        //    string connectionstring = Connectionstring(database);

        //    using (var db = new LiteDatabase(connectionstring))
        //    {
        //        var collection = db.GetCollection<WaehrungAuswahl>("tblWaehrung");
        //        collection.InsertBulk(katalog);
        //    }
        //}

        public void BulkInsertDetails(List<MünzDetail> detail, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<MünzDetail>("tblDetails");
                collection.InsertBulk(detail);

                collection.EnsureIndex(x => x.GUID);
            }
        }

        public List<Nation> ReadNationen(bool all = false)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Nation>("tblNation");
                var nations = collection.FindAll().OrderBy(x => x.Bezeichnung).ToList();

                if (!all)
                {
                    var collectionSettings2 = db.GetCollection<Settings2>("tblSettings2");
                    var settings2 = collectionSettings2.FindAll().ToList();
                    List<Nation> result = new List<Nation>();

                    foreach (var item in nations)
                    {
                        if (settings2.Any(x => x.id == item.ID))
                            result.Add(item);
                    }

                    nations = result;
                }
                return nations;
            }
        }

        public List<Nation> ReadNationen(string sprache, string database)
        {
            string connectionstring = Connectionstring(database);

            if (sprache == "DE") sprache = string.Empty;

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Nation>($"tblNation{sprache}");
                var nations = collection.FindAll().ToList();

                return nations;
            }
        }

        public List<Gebiet> ReadRegions(string database, int modul = -1)
        {
            List<Gebiet> regions;
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Gebiet>("tblGebiet");
                regions = collection.FindAll().OrderBy(x => x.Sortierung).ToList();

                if (modul != -1)
                    regions = regions.Where(x => x.AeraID == modul).ToList();
            }
            return regions;
        }

        public List<Aera> ReadAeras(string database, int modul = -1)
        {
            List<Aera> aeras;
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Aera>("tblAera");

                aeras = collection.FindAll().OrderBy(x => x.Sortierung).ToList();

                if (modul != -1)
                    aeras = aeras.Where(x => x.NationID == modul).ToList();
            }

            return aeras;

        }

        public BindingList<Auktion> ReadAuktionen(string guid, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                var auktionen = collection.Find(Query.EQ("Guid", guid)).OrderBy(x => x.Erhaltungsgrad).ToList();
                return new BindingList<Auktion>(auktionen);
            }
        }

        public List<Auktion> ReadAuktionen()
        {
            string connectionstring = Connectionstring("");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                var auktionen = collection.FindAll().ToList();
                return auktionen;
            }
        }

        public List<Erhaltungsgrad> ReadErhaltungsgrade(string sprache = "", string database = "")
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Erhaltungsgrad>("tblErhaltungsgrad");
                collection.EnsureIndex(x => x.Sprache);
                var erhaltungsgrade = collection.FindAll().OrderBy(x => x.ErhaltungsgradID).ToList();

                if (sprache != string.Empty)
                    erhaltungsgrade = erhaltungsgrade.Where(p => p.Sprache == sprache).ToList();

                if (erhaltungsgrade.Count == 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        Erhaltungsgrad e = new Erhaltungsgrad()
                        { Bezeichnung = "Error", Erhaltung = "Error", ErhaltungsgradID = i, ID = i, Land = "Error", Sprache = "de" };
                        erhaltungsgrade.Add(e);
                    }
                }

                return erhaltungsgrade;
            }
        }

        //public List<Erhaltungsgrad> ImportErhaltungsgrade(string database = "")
        //{
        //    string connectionstring = Connectionstring(database);

        //    using (var db = new LiteDatabase(connectionstring))
        //    {
        //        var collection = db.GetCollection<Erhaltungsgrad>("tblErhaltungsgrade");
        //        var erhaltungsgrade = collection.FindAll().ToList();

        //        return erhaltungsgrade;
        //    }
        //}

        public List<EigeneKatNr> ReadKatalogNummern(string database = "Sammlung", int nationID = -1)
        {
            List<EigeneKatNr> katalognummern = new List<EigeneKatNr>();

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                if (nationID != -1)
                    katalognummern = collection.Find(Query.EQ("NationID", nationID)).ToList();
                else
                    katalognummern = collection.FindAll().ToList();

                return katalognummern;
            }
        }

        public BindingList<Katalog3> ReadCoins(HauptFilter filter, enmSelectedStyle style, decimal faktor, enmPreise preistyp, string modul)
        {
            Dictionary<string, Katalog3> dictionary = new Dictionary<string, Katalog3>();
            List<Katalog3> liste = new List<Katalog3>();
            List<Katalog2> katalog;

            string connectionstring = Connectionstring(modul);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collectionKatalog = db.GetCollection<Katalog2>("tblKatalog");
                var collectionKatalog2 = collectionKatalog.Find(Query.EQ("AeraID", filter.AeraID)); //.ToList();
                katalog = collectionKatalog.Find(Query.EQ("regionID", filter.GebietID)).ToList();
            }

            foreach (Katalog2 item in katalog)
            {
                Katalog3 k = new Katalog3();
                k.AeraID = item.AeraID;
                k.Auflage = item.Auflage;
                k.AuflagePP = item.AuflagePP;
                k.AuflageSTH = item.AuflageSTH;
                k.RegionID = item.RegionID;
                k.GUID = item.GUID;
                k.HinweisKZ = item.HinweisKZ;
                k.Jahrgang = item.Jahrgang;
                k.Motiv = item.Motiv;
                k.Muenzzeichen = item.Muenzzeichen;
                k.NationID = item.NationID;
                k.Nominal = item.Nominal;
                k.OwnPicture = item.OwnPicture;
                k.Picture = item.Picture;
                k.Waehrung = item.Waehrung;
                k.OwnPicture = null;
                k.OriginalKatNr = item.KatNr;
                k.KatNr = item.KatNr;
                k.Selected = false;

                k.PPPreis = item.PPPreis;
                k.SPPreis = item.SPPreis;
                k.SPreis = item.SPreis;
                k.SSPPreis = item.SSPPreis;
                k.SSPreis = item.SSPreis;
                k.STHPreis = item.STHPreis;
                k.STNPreis = item.STNPreis;
                k.VZPPreis = item.VZPPreis;
                k.VZPreis = item.VZPreis;

                if (preistyp == enmPreise.EigenePreise)
                {
                    var p = ReadEigenePreise(item.GUID);

                    if (p[0].Preis != 0)
                    {
                        k.SPreis = p[0].Preis;
                        k.Farbe = "1";
                    }

                    if (p[1].Preis != 0)
                    {
                        k.SPPreis = p[1].Preis;
                        k.Farbe = "1";
                    }

                    if (p[2].Preis != 0)
                    {
                        k.SSPreis = p[2].Preis;
                        k.Farbe = "1";
                    }

                    if (p[3].Preis != 0)
                    {
                        k.SSPPreis = p[3].Preis;
                        k.Farbe = "1";
                    }

                    if (p[4].Preis != 0)
                    {
                        k.VZPreis = p[4].Preis;
                        k.Farbe = "1";
                    }

                    if (p[5].Preis != 0)
                    {
                        k.VZPPreis = p[5].Preis;
                        k.Farbe = "1";
                    }

                    if (p[6].Preis != 0)
                    {
                        k.STNPreis = p[6].Preis;
                        k.Farbe = "1";
                    }

                    if (p[7].Preis != 0)
                    {
                        k.STHPreis = p[7].Preis;
                        k.Farbe = "1";
                    }

                    if (p[8].Preis != 0)
                    {
                        k.PPPreis = p[8].Preis;
                    }
                }

                liste.Add(k);
            }

            connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection1 = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                if (collection1.Count() > 0)
                {
                    for (int i = 0; i < liste.Count; i++)
                    {
                        var picture = collection1.FindOne(Query.EQ("Guid", liste[i].GUID));
                        if (picture != null)
                            liste[i].OwnPicture = picture.DateiName;
                    }
                }

                //if (!string.IsNullOrEmpty(filter.Waehrung))
                //    liste = liste.Where(p => p.Waehrung == filter.Waehrung).ToList();

                //if (!string.IsNullOrEmpty(filter.Nominal))
                //    liste = liste.Where(p => p.Nominal == filter.Nominal).ToList();

                //if (!string.IsNullOrEmpty(filter.Jahrgang))
                //    liste = liste.Where(p => p.Jahrgang == filter.Jahrgang).ToList();

                dictionary.Clear();
                foreach (Katalog3 item in liste)
                    if (!dictionary.ContainsKey(item.GUID))
                        dictionary.Add(item.GUID, item);

                var col = db.GetCollection<Bestand>("tblBestand");
                var bestand = col.Find(Query.EQ("GebietID", filter.GebietID)).ToList();
                Decimal summe = 0;

                switch (style)
                {
                    case enmSelectedStyle.SammlungUndDoubletten:
                        foreach (Bestand item in bestand)
                        {
                            if (dictionary.ContainsKey(item.Guid))
                            {
                                summe = 0;
                                dictionary[item.Guid].S = (item.S + item.DS) == 0 ? string.Empty : string.Format("{0}/{1}", item.S, item.DS);
                                dictionary[item.Guid].SP = (item.SP + item.DSP) == 0 ? string.Empty : string.Format("{0}/{1}", item.SP, item.DSP);
                                dictionary[item.Guid].SS = (item.SS + item.DSS) == 0 ? string.Empty : string.Format("{0}/{1}", item.SS, item.DSS);
                                dictionary[item.Guid].SSP = (item.SSP + item.DSSP) == 0 ? string.Empty : string.Format("{0}/{1}", item.SSP, item.DSSP);
                                dictionary[item.Guid].VZ = (item.VZ + item.DVZ) == 0 ? string.Empty : string.Format("{0}/{1}", item.VZ, item.DVZ);
                                dictionary[item.Guid].VZP = (item.VZP + item.DVZP) == 0 ? string.Empty : string.Format("{0}/{1}", item.VZP, item.DVZP);
                                dictionary[item.Guid].STN = (item.STN + item.DSTN) == 0 ? string.Empty : string.Format("{0}/{1}", item.STN, item.DSTN);
                                dictionary[item.Guid].STH = (item.STH + item.DSTH) == 0 ? string.Empty : string.Format("{0}/{1}", item.STH, item.DSTH);
                                dictionary[item.Guid].PP = (item.PP + item.DPP) == 0 ? string.Empty : string.Format("{0}/{1}", item.PP, item.DPP);

                                summe += (item.S + item.DS) * dictionary[item.Guid].SPreis;
                                summe += (item.SP + item.DSP) * dictionary[item.Guid].SPPreis;
                                summe += (item.SS + item.DSS) * dictionary[item.Guid].SSPreis;
                                summe += (item.SSP + item.DSSP) * dictionary[item.Guid].SSPPreis;
                                summe += (item.VZ + item.DVZ) * dictionary[item.Guid].VZPreis;
                                summe += (item.VZP + item.DVZP) * dictionary[item.Guid].VZPPreis;
                                summe += (item.STN + item.DSTN) * dictionary[item.Guid].STNPreis;
                                dictionary[item.Guid].SummeS = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;

                                summe = 0;
                                summe += (item.STH + item.DSTH) * dictionary[item.Guid].STHPreis;
                                summe += (item.PP + item.DPP) * dictionary[item.Guid].PPPreis;
                                dictionary[item.Guid].SummePP = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;
                            }
                        }
                        break;

                    case enmSelectedStyle.DoublettenOnly:
                        foreach (Bestand item in bestand)
                        {
                            if (dictionary.ContainsKey(item.Guid))
                            {
                                summe = 0;
                                dictionary[item.Guid].S = item.DS == 0 ? string.Empty : item.DS.ToString();
                                dictionary[item.Guid].SP = item.DSP == 0 ? string.Empty : item.DSP.ToString();
                                dictionary[item.Guid].SS = item.DSS == 0 ? string.Empty : item.DSS.ToString();
                                dictionary[item.Guid].SSP = item.DSSP == 0 ? string.Empty : item.DSSP.ToString();
                                dictionary[item.Guid].VZ = item.DVZ == 0 ? string.Empty : item.DVZ.ToString();
                                dictionary[item.Guid].VZP = item.DVZP == 0 ? string.Empty : item.DVZP.ToString();
                                dictionary[item.Guid].STN = item.DSTN == 0 ? string.Empty : item.DSTN.ToString();
                                dictionary[item.Guid].STH = item.DSTH == 0 ? string.Empty : item.DSTH.ToString();
                                dictionary[item.Guid].PP = item.DPP == 0 ? string.Empty : item.DPP.ToString();

                                summe += item.DS * dictionary[item.Guid].SPreis;
                                summe += item.DSP * dictionary[item.Guid].SPPreis;
                                summe += item.DSS * dictionary[item.Guid].SSPreis;
                                summe += item.DSSP * dictionary[item.Guid].SSPPreis;
                                summe += item.DVZ * dictionary[item.Guid].VZPreis;
                                summe += item.DVZP * dictionary[item.Guid].VZPPreis;
                                summe += item.DSTN * dictionary[item.Guid].STNPreis;
                                dictionary[item.Guid].SummeS = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;

                                summe = 0;
                                summe += item.DSTH * dictionary[item.Guid].STHPreis;
                                summe += item.DPP * dictionary[item.Guid].PPPreis;
                                dictionary[item.Guid].SummePP = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;
                            }
                        }
                        break;

                    case enmSelectedStyle.Icon:
                    case enmSelectedStyle.SammlungOnly:
                        foreach (Bestand item in bestand)
                        {
                            if (dictionary.ContainsKey(item.Guid))
                            {
                                dictionary[item.Guid].S = item.S == 0 ? string.Empty : item.S.ToString();
                                dictionary[item.Guid].SP = item.SP == 0 ? string.Empty : item.SP.ToString();
                                dictionary[item.Guid].SS = item.SS == 0 ? string.Empty : item.SS.ToString();
                                dictionary[item.Guid].SSP = item.SSP == 0 ? string.Empty : item.SSP.ToString();
                                dictionary[item.Guid].VZ = item.VZ == 0 ? string.Empty : item.VZ.ToString();
                                dictionary[item.Guid].VZP = item.VZP == 0 ? string.Empty : item.VZP.ToString();
                                dictionary[item.Guid].STN = item.STN == 0 ? string.Empty : item.STN.ToString();
                                dictionary[item.Guid].STH = item.STH == 0 ? string.Empty : item.STH.ToString();
                                dictionary[item.Guid].PP = item.PP == 0 ? string.Empty : item.PP.ToString();

                                summe = 0;
                                summe += item.S * dictionary[item.Guid].SPreis;
                                summe += item.SP * dictionary[item.Guid].SPPreis;
                                summe += item.SS * dictionary[item.Guid].SSPreis;
                                summe += item.SSP * dictionary[item.Guid].SSPPreis;
                                summe += item.VZ * dictionary[item.Guid].VZPreis;
                                summe += item.VZP * dictionary[item.Guid].VZPPreis;
                                summe += item.STN * dictionary[item.Guid].STNPreis;
                                dictionary[item.Guid].SummeS = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;

                                summe = 0;
                                summe += item.STH * dictionary[item.Guid].STHPreis;
                                summe += item.PP * dictionary[item.Guid].PPPreis;
                                dictionary[item.Guid].SummePP = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;
                            }
                        }
                        break;
                }
            }
            liste = dictionary.Values.ToList().OrderBy(a => a.KatNr).ThenBy(b => b.Nominal).ThenBy(c => c.Jahrgang).ThenBy(d => d.Muenzzeichen).ToList();

            return new BindingList<Katalog3>(liste);
        }

        public BindingList<EigenerPreis> ReadEigenePreise(string guid, string database = null)
        {
            Preise preise;

            string connectionstring = Connectionstring(database);
            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                preise = collection.FindOne(Query.EQ("Guid", guid));
                if (preise == null)
                    preise = new Preise();
            }

            connectionstring = Connectionstring();
            using (var db = new LiteDatabase(connectionstring))
            {
                var collection2 = db.GetCollection<Erhaltungsgrad>("tblErhaltungsgrad");
                var erhaltung = collection2.Find(Query.EQ("Sprache", "de")).OrderBy(a => a.ID).ToList();                //TODO

                BindingList<EigenerPreis> p = new BindingList<EigenerPreis>();

                p.Add(new EigenerPreis { Erhaltung = erhaltung[0].Erhaltung, Preis = preise.SPreis });
                p.Add(new EigenerPreis { Erhaltung = erhaltung[1].Erhaltung, Preis = preise.SPPreis });
                p.Add(new EigenerPreis { Erhaltung = erhaltung[2].Erhaltung, Preis = preise.SSPreis });
                p.Add(new EigenerPreis { Erhaltung = erhaltung[3].Erhaltung, Preis = preise.SSPPreis });
                p.Add(new EigenerPreis { Erhaltung = erhaltung[4].Erhaltung, Preis = preise.VZPreis });
                p.Add(new EigenerPreis { Erhaltung = erhaltung[5].Erhaltung, Preis = preise.VZPPreis });
                p.Add(new EigenerPreis { Erhaltung = erhaltung[6].Erhaltung, Preis = preise.STNPreis });
                p.Add(new EigenerPreis { Erhaltung = erhaltung[7].Erhaltung, Preis = preise.STHPreis });
                p.Add(new EigenerPreis { Erhaltung = erhaltung[8].Erhaltung, Preis = preise.PPPreis });

                return p;
            }
        }

        public List<Preise> ReadEigenePreise()
        {
            List<Preise> preise;

            string connectionstring = Connectionstring();
            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                preise = collection.FindAll().ToList();
            }

            return preise;
        }

        public Dictionary<int, decimal> ReadKaufpreise(string guid)
        {
            Dictionary<int, List<decimal>> dictionary = new Dictionary<int, List<decimal>>();
            Dictionary<int, decimal> xxx = new Dictionary<int, decimal>();

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                var sammlung = collection.Find(Query.EQ("Guid", guid)).ToList();


                foreach (Sammlung item in sammlung)
                {
                    if (dictionary.ContainsKey(item.Erhaltung))
                        dictionary[item.Erhaltung].Add(item.Kaufpreis);
                    else
                    {
                        List<decimal> liste = new List<decimal>();
                        liste.Add(item.Kaufpreis);
                        dictionary.Add(item.Erhaltung, liste);
                    }
                }

                foreach (KeyValuePair<int, List<decimal>> item in dictionary)
                    xxx.Add(item.Key, item.Value.Average());

            }
            return xxx;
        }

        public List<string> ReadOwnPictures()
        {
            List<string> result = new List<string>();

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                var nations = collection.FindAll().OrderBy(x => x.DateiName);

                foreach (var item in nations)
                    result.Add(item.DateiName);

            }
            return result;

        }

        public List<Sammlung> ReadSammlung(string guid = "", enmDoublette doublette = enmDoublette.Alle)
        {
            List<Sammlung> sammlung;

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");

                if (guid != string.Empty)
                    sammlung = collection.Find(Query.EQ("Guid", guid)).ToList();
                else
                    sammlung = collection.FindAll().ToList();

                switch (doublette)
                {
                    case enmDoublette.Sammlung:
                        sammlung = sammlung.Where(p => !p.Doublette).OrderBy(o => o.Erhaltung).ToList();
                        break;

                    case enmDoublette.Doublette:
                        sammlung = sammlung.Where(p => p.Doublette).OrderBy(o => o.Erhaltung).ToList();
                        break;
                }

                return sammlung;
            }
        }

        public List<Sammlung> ReadSammlung()
        {
            List<Sammlung> sammlung;

            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                sammlung = collection.FindAll().ToList();

                return sammlung;
            }
        }

        public List<Bestand> ReadBestand(string database = null)
        {
            List<Bestand> liste;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                liste = collection.FindAll().ToList();
            }

            return liste;
        }

        public void Drop(string table)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
                db.DropCollection(table);
        }

        public List<SammlungShort> ReadSammlungShort()
        {
            List<SammlungShort> result = new List<SammlungShort>();

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                var sammlung = collection.FindAll().ToList();

                foreach (var item in sammlung)
                    result.Add(new SammlungShort { Guid = item.Guid, Doublette = item.Doublette, Erhaltung = item.Erhaltung });
            }

            return result;
        }

        public List<EigeneBilder> ReadEigeneBilder(string database = "Sammlung")
        {
            List<EigeneBilder> liste;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                liste = collection.FindAll().ToList();
            }

            return liste;
        }


        public List<Settings2> ReadSettings2()
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings2>("tblSettings2");
                var settings2 = collection.FindAll().ToList();

                return settings2;
            }
        }

        //public List<Sammlung> ReadDoubletten(string guid)
        //{
        //    using (var db = new LiteDatabase(Connectionstring))
        //    {
        //        var collection = db.GetCollection<Sammlung>("tblSammlung");
        //        var sammlung = collection.Find(Query.EQ("Guid", guid)).ToList();

        //        sammlung = sammlung.Where(p => p.Doublette == true).OrderBy(o => o.Erhaltung).ToList();

        //        return sammlung;
        //    }
        //}

        public void ClearCollection(string collectionName, string database = null)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection(collectionName);
                collection.DeleteMany("1=1");
            }
        }

        public void InitListen(string database)
        {
            KeyValuePair<int, string> pair;

            Dictionary<string, KeyValuePair<int, string>> currencies = new Dictionary<string, KeyValuePair<int, string>>();
            Dictionary<string, KeyValuePair<int, string>> nominales = new Dictionary<string, KeyValuePair<int, string>>();
            Dictionary<string, KeyValuePair<int, string>> Jahre = new Dictionary<string, KeyValuePair<int, string>>();

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<WaehrungAuswahl>("tblKatalog");
                var currency = collection.FindAll().ToList();

                foreach (WaehrungAuswahl item in currency)
                {
                    string temp = item.GebietID.ToString() + "|" + item.Waehrung;
                    if (!currencies.ContainsKey(temp))
                    {
                        pair = new KeyValuePair<int, string>(item.GebietID, item.Waehrung);
                        currencies.Add(temp, pair);
                    }

                    temp = item.GebietID.ToString() + "|" + item.Nominal;
                    if (!nominales.ContainsKey(temp))
                    {
                        pair = new KeyValuePair<int, string>(item.GebietID, item.Nominal);
                        nominales.Add(temp, pair);
                    }

                    temp = item.GebietID.ToString() + "|" + item.Jahrgang;
                    if (!Jahre.ContainsKey(temp))
                    {
                        pair = new KeyValuePair<int, string>(item.GebietID, item.Jahrgang);
                        Jahre.Add(temp, pair);
                    }
                }

                ReadNominalListe = nominales.Values.ToList();
                ReadJahrgangsListe = Jahre.Values.ToList();
                ReadCurrencyListe = currencies.Values.ToList();
            }
        }

        public List<KeyValuePair<int, string>> ReadCurrencyListe { get; set; }
        public List<KeyValuePair<int, string>> ReadNominalListe { get; set; }
        public List<KeyValuePair<int, string>> ReadJahrgangsListe { get; set; }

        public Settings ReadSettings()
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings>("tblSettings");
                var settings = collection.FindById(1);
                return settings;
            }
        }

        public Texte GetHinweis(string guid, string sprache, string database)
        {
            if (sprache.ToLower() == "de")
                sprache = String.Empty;

            var tableName = string.Format("tblDetailTexte{0}", sprache);

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>(tableName);
                var texte = collection.FindById(guid);

                return texte;
            }
        }

        public MünzDetail GetMuenzDetails(string guid, string modul)
        {
            string connectionstring = Connectionstring(modul);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<MünzDetail>("tblKatalog");
                var texte = collection.FindById(guid);

                return texte;
            }
        }

        public EigeneKatNr GetKatalogNummer(string katalognummer)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                var item = collection.FindOne(Query.EQ(Application.ProductName, katalognummer));

                if (item == null)
                    item = new EigeneKatNr { Coinbook = katalognummer, KatNr = string.Empty, ID = katalognummer };

                return item;
            }
        }

        public Nation GetNation(int id)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Nation>("tblNation");
                var nation = collection.FindOne(Query.EQ("ID", id));

                return nation;
            }
        }

        public Bank GetBank()
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                int id = 1;
                var collection = db.GetCollection<Bank>("tblBank");
                var bank = collection.FindOne(Query.EQ("_id", id));

                return bank;
            }
        }

        public Bestand GetBestand(string guid)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                var bestand = collection.FindOne(Query.EQ("Guid", guid));

                if (bestand == null)
                {
                    bestand = new Bestand();
                    bestand.Guid = guid;
                    bestand.S = 0;
                    bestand.SP = 0;
                    bestand.SS = 0;
                    bestand.SSP = 0;
                    bestand.VZ = 0;
                    bestand.VZP = 0;
                    bestand.STN = 0;
                    bestand.STH = 0;
                    bestand.PP = 0;
                    bestand.DS = 0;
                    bestand.DSP = 0;
                    bestand.DSS = 0;
                    bestand.DSSP = 0;
                    bestand.DVZ = 0;
                    bestand.DVZP = 0;
                    bestand.DSTN = 0;
                    bestand.DSTH = 0;
                    bestand.DPP = 0;
                }

                return bestand;
            }
        }

        public EigeneBilder GetOwnPicture(string guid)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                var picture = collection.FindOne(Query.EQ("Guid", guid));

                if (picture == null)
                {
                    picture = new EigeneBilder();
                    picture.ID = 0;
                    picture.Guid = guid;
                    picture.ShowPicture = false;
                    picture.DateiName = string.Empty;
                }

                return picture;
            }
        }

        public Katalog2 GetCoinFromGuid(string guid, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collectionKatalog = db.GetCollection<Katalog2>("tblKatalog");
                var coin = collectionKatalog.FindOne(Query.EQ("Guid", guid));

                return coin;
            }
        }

        public Katalog2 GetCoinFromID(int id, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collectionKatalog = db.GetCollection<Katalog2>("tblKatalog");
                var coin = collectionKatalog.FindOne(Query.EQ("ID", id));

                return coin;
            }
        }

        //public Beschreibung GetText(string guid, string tabelle, string database)
        //{
        //    Beschreibung beschreibung;

        //    string connectionstring = Connectionstring(database);

        //    using (var db = new LiteDatabase(connectionstring))
        //    {
        //        var collection = db.GetCollection<Beschreibung>(tabelle);
        //        beschreibung = collection.FindOne(Query.EQ("Guid", guid));
        //    }

        //    return beschreibung;
        //}

        public List<Texte> ReadDetailTexte(string language, int modul, string database)
        {
            List<Texte> beschreibung;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>($"tblDetailTexte{language}");
                beschreibung = collection.Find(Query.EQ("NationID", modul)).ToList();
            }

            return beschreibung;
        }

        public List<MünzDetail> ReadMuenzDetails(int modul, string database)
        {
            List<MünzDetail> beschreibung;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<MünzDetail>("tblDetails");
                beschreibung = collection.Find(Query.EQ("NationID", modul)).ToList();
            }

            return beschreibung;
        }

        public List<Texte> ReadDetailTextEN(int modul, string database)
        {
            List<Texte> beschreibung;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>("tblDetailTexteEN");
                beschreibung = collection.Find(Query.EQ("NationID", modul)).ToList();
            }

            return beschreibung;
        }

        public Texte GetDetailTextDE(string guid, string database)
        {
            Texte beschreibung;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>("tblDetailTexteDE");
                beschreibung = collection.FindOne(Query.EQ("Guid", guid));
            }

            return beschreibung;
        }

        public Texte GetDetailTextEN(string guid, string database)
        {
            Texte beschreibung;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>("tblDetailTexteEN");
                beschreibung = collection.FindOne(Query.EQ("Guid", guid));
            }

            return beschreibung;
        }

        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        public Preise GetEigenePreise(string guid)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                var preise = collection.FindOne(Query.EQ("Guid", guid));

                return preise;
            }
        }

        public void SaveSammlung(Sammlung coin)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                collection.Upsert(coin);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void SaveSammlung(List<Sammlung> liste)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void SaveCulture(List<Culture> culture)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Culture>("tblCulture");

                foreach (var item in culture)
                    collection.Upsert(item);
            }
        }

        public void SaveBank(Bank bank)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Bank>("tblBank");
                bank.ID = 1;
                collection.Upsert(bank);
            }
        }

        public void SaveAuktionen(BindingList<Auktion> auktionen)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                foreach (Auktion item in auktionen)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.ID);
            }
        }

        public void SaveEigeneKatNr(List<EigeneKatNr> liste)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                collection.InsertBulk(liste);
            }
        }

        public void SaveBestand(Sammlung coin, int anzahl, int gebietID, int aeraID, int nationID)
        {
            string connectionstring = Connectionstring("Sammlung");
            Bestand bestand = GetBestand(coin.Guid);

            bestand.GebietID = gebietID;
            bestand.AeraID = aeraID;
            bestand.NationID = nationID;
            switch (coin.Erhaltung)
            {
                case 1:
                    if (coin.Doublette)
                        bestand.DS += anzahl;
                    else
                        bestand.S += anzahl;
                    break;


                case 2:
                    if (coin.Doublette)
                        bestand.DSP += anzahl;
                    else
                        bestand.SP += anzahl;
                    break;

                case 3:
                    if (coin.Doublette)
                        bestand.DSS += anzahl;
                    else
                        bestand.SS += anzahl;
                    break;

                case 4:
                    if (coin.Doublette)
                        bestand.DSSP += anzahl;
                    else
                        bestand.SSP += anzahl;
                    break;

                case 5:

                    if (coin.Doublette)
                        bestand.DVZ += anzahl;
                    else
                        bestand.VZ += anzahl;
                    break;

                case 6:
                    if (coin.Doublette)
                        bestand.DVZP += anzahl;
                    else
                        bestand.VZP += anzahl;
                    break;

                case 7:
                    if (coin.Doublette)
                        bestand.DSTN += anzahl;
                    else
                        bestand.STN += anzahl;
                    break;

                case 8:
                    if (coin.Doublette)
                        bestand.DSTH += anzahl;
                    else
                        bestand.STH += anzahl;
                    break;

                case 9:
                    if (coin.Doublette)
                        bestand.DPP += anzahl;
                    else
                        bestand.PP += anzahl;
                    break;
            }

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                collection.Upsert(bestand);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.id);
                collection.EnsureIndex(x => x.NationID);
                collection.EnsureIndex(x => x.AeraID);
                collection.EnsureIndex(x => x.GebietID);
            }
        }

        public Preise SaveOwnPrices(string guid, BindingList<EigenerPreis> p)
        {
            Preise preise;

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                preise = collection.FindOne(Query.EQ("Guid", guid));
                if (preise == null)
                    preise = new Preise();

                preise.GUID = guid;
                preise.SPreis = p[0].Preis;
                preise.SPPreis = p[1].Preis;
                preise.SSPreis = p[2].Preis;
                preise.SSPPreis = p[3].Preis;
                preise.VZPreis = p[4].Preis;
                preise.VZPPreis = p[5].Preis;
                preise.STNPreis = p[6].Preis;
                preise.STHPreis = p[7].Preis;
                preise.PPPreis = p[8].Preis;

                collection.Upsert(preise);

                collection.EnsureIndex(x => x.GUID);
                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.NationID);
            }

            return preise;
        }

        public void SaveKatalogNummer(EigeneKatNr katalog)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                collection.Upsert(katalog);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.KatNr);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public int SaveOwnPicture(EigeneBilder picture)
        {
            int id = picture.ID;

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                collection.Upsert(picture);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.ID);

                if (id == 0)
                    id = collection.Max();
            }
            return id;
        }

        public void SaveEigeneBilder(List<EigeneBilder> liste)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.Guid);
            }
        }


        public void SaveSettings(Settings settings)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings>("tblSettings");
                collection.Upsert(settings);
            }
        }

        public void DeleteAuktion(int id)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                collection.Delete(id);
            }
        }

        public void UpsertTextDE(Texte text, int connectionAction, string database)
        {
            string connectionstring = Connectionstring(database);

            ILiteCollection<Texte> collection = null;
            switch (connectionAction)
            {
                case 0:
                    db = new LiteDatabase(connectionstring);
                    break;

                case 1:
                    collection = db.GetCollection<Texte>("tblDetailTexteDE");
                    collection.Upsert(text);
                    break;

                case 2:
                    collection.EnsureIndex(x => x.ID);
                    collection.EnsureIndex(x => x.GUID);
                    collection.EnsureIndex(x => x.NationID);
                    db.Dispose();
                    break;
            }
        }

        public void UpsertKatalog(Katalog2 katalog, int connectionAction, string database)
        {
            //ILiteCollection<Katalog2> collection = null;
            string connectionstring = Connectionstring(database);

            switch (connectionAction)
            {
                case 0:
                    db = new LiteDatabase(connectionstring);
                    break;

                case 1:
                    {
                        var collection = db.GetCollection<Katalog2>("tblKatalog");
                        collection.Upsert(katalog);
                    }
                    break;

                case 2:
                    {
                        var collection = db.GetCollection<Katalog2>("tblKatalog");
                        //collection.EnsureIndex(x => x.GUID);
                        //collection.EnsureIndex(x => x.NationID);
                        //collection.EnsureIndex(x => x.AeraID);
                        //collection.EnsureIndex(x => x.GebietID);
                        //collection.EnsureIndex(x => x.KatNr);
                    }
                    db.Dispose();
                    break;
            }
        }

        public void BulkUpsertDetails(List<MünzDetail> details, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<MünzDetail>("tblDetails");

                foreach (var item in details)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.GUID);
            }
        }

        public void UpsertKatalog(List<Katalog2> katalog, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Katalog2>("tblKatalog");

                foreach (var item in katalog)
                    collection.Upsert(item);

                //collection.EnsureIndex(x => x.GUID);
                //collection.EnsureIndex(x => x.NationID);
                //collection.EnsureIndex(x => x.AeraID);
                //collection.EnsureIndex(x => x.GebietID);
                //collection.EnsureIndex(x => x.KatNr);
            }
        }

        public void UpsertDetail(MünzDetail detail, int connectionAction, string database)
        {
            string connectionstring = Connectionstring(database);

            switch (connectionAction)
            {
                case 0:
                    db = new LiteDatabase(connectionstring);
                    break;

                case 1:
                    {
                        var collection = db.GetCollection<MünzDetail>("tblDetails");
                        collection.Upsert(detail);
                    }
                    break;

                case 2:
                    {
                        var collection = db.GetCollection<MünzDetail>("tblDetails");
                        collection.EnsureIndex(x => x.ID);
                        collection.EnsureIndex(x => x.GUID);
                    }
                    db.Dispose();
                    break;
            }
        }

        public void UpsertTexteEN(Texte text, int connectionAction, string database)
        {
            string connectionstring = Connectionstring(database);

            switch (connectionAction)
            {
                case 0:
                    db = new LiteDatabase(connectionstring);
                    break;

                case 1:
                    {
                        var collection = db.GetCollection<Texte>("tblDetailTexteEN");
                        collection.Upsert(text);
                    }
                    break;

                case 2:
                    {
                        var collection = db.GetCollection<Texte>("tblDetailTexteEN");
                        collection.EnsureIndex(x => x.ID);
                        collection.EnsureIndex(x => x.GUID);
                        collection.EnsureIndex(x => x.Typ);
                        collection.EnsureIndex(x => x.NationID);
                    }
                    db.Dispose();
                    break;
            }
        }

        public void BulkUpsertTexteDE(List<Texte> texte, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>("tblDetailTexteDE");

                foreach (var item in texte)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.GUID);
                collection.EnsureIndex(x => x.Typ);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void BulkUpsertTexteEN(List<Texte> texte, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>("tblDetailTexteEN");

                foreach (var item in texte)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.GUID);
                collection.EnsureIndex(x => x.Typ);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void DeleteSammlung(int id)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                collection.Delete(id);
            }
        }

        public EigeneBilder DeleteOwnPicture(string name)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                var picture = collection.FindOne(Query.EQ("DateiName", name));
                collection.Delete(picture.ID);
                return picture;
            }
        }

        public bool DeleteKatalogNummer(string id)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                return collection.Delete(id);
            }
        }

        //public void DeleteKatalog(int nationID, string database)
        //{
        //    var del = ReadKatalog(nationID, database);

        //    int max = del.Count;

        //    string connectionstring = Connectionstring(database);

        //    using (var db = new LiteDatabase(connectionstring))
        //    {
        //        var collection = db.GetCollection<Katalog2>("tblKatalog");
        //        var details = db.GetCollection<MünzDetail>("tblDetails");
        //        var texte = db.GetCollection<Texte>("tblDetailTexteDE");
        //        var texteEN = db.GetCollection<Texte>("tblDetailTexteEN");

        //        db.BeginTrans();
        //        foreach (var d in del)
        //        {
        //            collection.Delete(d.GUID);
        //            details.Delete(d.GUID);
        //            texte.Delete(d.GUID);
        //            texteEN.Delete(d.GUID);
        //        }
        //        db.Commit();

        //        //if (ReportProgress != null)
        //        //    ReportProgress(null, new LiteEventArgs(i, max));

        //        //var details = db.GetCollection<MünzDetail>("tblDetails");
        //        //var texte = db.GetCollection<Texte>("tblDetailTexteDE");


        //        //    if (ReportProgress != null)
        //        //        ReportProgress(null, new LiteEventArgs(i, max));

        //        //    collection.Delete(d.GUID);
        //        //    details.Delete(d.GUID);
        //        //    texte.Delete(d.GUID);
        //        //    i++;
        //        //}

        //    }
        //}

        //public void DeleteTexte(int nationID, string database)
        //{
        //    var del = ReadKatalog(database, nationID );

        //    string connectionstring = Connectionstring(database);

        //    using (var db = new LiteDatabase(connectionstring))
        //    {
        //        var texte = db.GetCollection<Texte>("tblDetailTexteEN");

        //        foreach (var d in del)
        //            texte.Delete(d.GUID);
        //    }
        //}

        public void DropCollection(string collection, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
                db.DropCollection(collection);
        }

        public int Count(string table, string database = null)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection(table);
                return collection.Count();
            }
        }

        public List<Status> ReadStatus(string noLicense)
        {
            var nation = ReadNationen(true);
            var settings = ReadSettings2();

            List<Status> status = new List<Status>();

            foreach (var item in nation)
            {
                Status i = new Status();
                i.ID = item.ID;
                i.Nation = item.Bezeichnung;
                i.InUse = false;
                i.Jahr = noLicense;

                status.Add(i);
            }

            foreach (var item in settings)
            {
                var st = status.First(x => x.ID == item.id);
                st.Jahr = item.Jahr;
                st.InUse = true;
            }

            return status;
        }

        public List<Praegeanstalt> ReadPraegestellen(int nation, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Praegeanstalt>("tblPraegeanstalt");
                var praegeanstalt = collection.FindAll().Where(p => p.Nation == nation).OrderBy(x => x.Land).ThenBy(x => x.Muenzzeichen).ToList();

                return praegeanstalt;
            }
        }

        public List<Downloads> ReadDownloads()
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Downloads>("tblDownloads");
                var downloads = collection.FindAll().OrderBy(x => x.Bezeichnung).ToList();
                return downloads;
            }
        }

        public List<KeyValuePair<string, string>> ReadCountries()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Erhaltungsgrad>("tblErhaltungsgrad");
                collection.EnsureIndex(x => x.Sprache);
                var erhaltungsgrade = collection.FindAll().ToList();

                Dictionary<string, Erhaltungsgrad> temp = new Dictionary<string, Erhaltungsgrad>();

                foreach (var item in erhaltungsgrade)
                    if (!temp.ContainsKey(item.Land))
                        temp.Add(item.Land, item);


                foreach (var item in temp)
                {
                    KeyValuePair<string, string> temp2 = new KeyValuePair<string, string>(item.Value.Sprache, item.Value.Land);
                    result.Add(temp2);
                }
            }

            return result;
        }

        public List<Culture> ReadCulture(string database = "")
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Culture>("tblCulture");
                var culture = collection.FindAll().OrderBy(x => x.Bezeichnung).ToList();

                return culture;
            }
        }

        #region Backup
        public void BackupAuktionen(string fileName, string database)
        {
            List<Auktion> auktionen;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                auktionen = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(fileName))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                collection.InsertBulk(auktionen);
            }
        }

        public void BackupBestand(string fileName, string database)
        {
            List<Bestand> liste;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                liste = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(fileName))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                collection.InsertBulk(liste);
            }
        }

        public void BackupEigeneBilder(string fileName, string database)
        {
            List<EigeneBilder> liste;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                liste = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(fileName))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                collection.InsertBulk(liste);
            }
        }

        public void BackupEigeneKatNr(string fileName, string database)
        {
            List<EigeneKatNr> liste;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                liste = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(fileName))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                collection.InsertBulk(liste);
            }
        }

        public void BackupEigenePreise(string fileName, string database)
        {
            List<Preise> liste;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                liste = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(fileName))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                collection.InsertBulk(liste);
            }
        }

        public void BackupSammlung(string fileName, string database)
        {
            List<Sammlung> liste;

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                liste = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(fileName))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                collection.InsertBulk(liste);
            }
        }

        public void BackupSettings(string fileName)
        {
            List<Settings> liste;

            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings>("tblSettings");
                liste = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(fileName))
            {
                var collection = db.GetCollection<Settings>("tblSettings");
                collection.InsertBulk(liste);
            }
        }
        #endregion Backup

        #region Restore
        public void RestoreAuktionen(string filename)
        {
            List<Auktion> auktionen;

            using (var db = new LiteDatabase(filename))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                auktionen = collection.FindAll().ToList();
            }

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Auktion>("tblAuktionen");
                collection.DeleteMany("1=1");
                collection.InsertBulk(auktionen);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.Guid);
            }
        }

        public void RestoreBestand(string filename)
        {
            List<Bestand> liste;

            using (var db = new LiteDatabase(filename))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                liste = collection.FindAll().ToList();
            }

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.AeraID);
                collection.EnsureIndex(x => x.NationID);
                collection.EnsureIndex(x => x.id);
                collection.EnsureIndex(x => x.GebietID);
            }
        }

        public void RestoreEigeneBilder(string filename)
        {
            List<EigeneBilder> liste;
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(filename))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                liste = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneBilder>("tblEigeneBilder");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.Guid);
            }
        }

        public void RestoreEigeneKatNr(string filename)
        {
            List<EigeneKatNr> liste;

            using (var db = new LiteDatabase(filename))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                liste = collection.FindAll().ToList();
            }

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<EigeneKatNr>("tblEigeneKatNr");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);

                collection.EnsureIndex(x => x.Coinbook);
                collection.EnsureIndex(x => x.NationID);
                collection.EnsureIndex(x => x.ID);
            }
        }

        public void RestoreEigenePreise(string filename)
        {
            List<Preise> liste;

            using (var db = new LiteDatabase(filename))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                liste = collection.FindAll().ToList();
            }

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Preise>("tblPreise");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);

                collection.EnsureIndex(x => x.GUID);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void RestoreSammlung(string filename)
        {
            List<Sammlung> liste;

            using (var db = new LiteDatabase(filename))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                liste = collection.FindAll().ToList();
            }

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Sammlung>("tblSammlung");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void RestoreSettings(string filename)
        {
            List<Settings> liste;

            using (var db = new LiteDatabase(filename))
            {
                var collection = db.GetCollection<Settings>("tblSettings");
                liste = collection.FindAll().ToList();
            }

            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings>("tblSettings");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);
            }
        }

        public int GetNationFromKatalogNummer(string katalogNummer, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collectionKatalog = db.GetCollection<Katalog2>("tblKatalog");
                var nationID = collectionKatalog.FindOne(Query.EQ("KatNr", katalogNummer)).NationID;

                return nationID;
            }
        }

        public List<Bestand> GetNationRegionFromCoin(List<Bestand> liste, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collectionKatalog = db.GetCollection<Katalog2>("tblKatalog");

                for (int i = 0; i < liste.Count; i++)
                {
                    var temp = collectionKatalog.FindOne(Query.EQ("Guid", liste[i].Guid));

                    if (temp != null)
                    {
                        liste[i].NationID = temp.NationID;
                        liste[i].GebietID = temp.RegionID;
                    }
                    else
                    {
                        liste[i].NationID = 0;
                        liste[i].GebietID = 0;
                    }
                }
                return liste;
            }
        }
        #endregion Restore

        public List<Fehlliste> Fehllisten(int nation, int aera, int region, enmPreise preisTyp, decimal faktor, List<Nation> nationen)
        {
            List<Bestand> bestand = new List<Bestand>();
            List<Fehlliste> katalog = new List<Fehlliste>();
            List<Preise> preise = new List<Preise>();

            if (nation != 0)
            {
                var n = nationen.FirstOrDefault(x => x.ID == nation);

                string connectionstring = Connectionstring(n.Key);
                using (var db = new LiteDatabase(connectionstring))
                {
                    katalog = db.GetCollection<Fehlliste>("tblKatalog").FindAll().ToList();

                    if (aera != 0)
                        katalog = katalog.Where(a => a.AeraID == aera).ToList();

                    if (region != 0)
                        katalog = katalog.Where(a => a.GebietID == region).ToList();
                }

                bestand = ReadBestand("Sammlung").FindAll(x => x.NationID == nation);

                if (aera != 0)
                    bestand = bestand.Where(a => a.AeraID == aera).ToList();

                if (region != 0)
                    bestand = bestand.Where(a => a.GebietID == region).ToList();

                foreach (var item in bestand)
                {
                    var katalogItem = katalog.FirstOrDefault(x => x.GUID == item.Guid);
                    katalog.Remove(katalogItem);
                }
            }

            if (preisTyp == enmPreise.EigenePreise)
                preise = ReadEigenePreise();

            foreach (var item in katalog)
            {
                item.SPreis = item.SPreis * faktor;
                item.SPPreis = item.SSPreis * faktor;
                item.SSPreis = item.SSPreis * faktor;
                item.SSPPreis = item.SSPPreis * faktor;
                item.STHPreis = item.STHPreis * faktor;
                item.STNPreis = item.STNPreis * faktor;
                item.VZPreis = item.VZPreis * faktor;
                item.VZPPreis = item.VZPPreis * faktor;
                item.PPPreis = item.PPPreis * faktor;

                if (preisTyp == enmPreise.EigenePreise)
                {
                    var preis = preise.FirstOrDefault(x => x.GUID == item.GUID);
                    if (preis != null)
                    {
                        if (preis.SPreis != 0) item.SPreis = preis.SPreis; item.Farbe = item.Farbe | enmColorFlag.S;
                        if (preis.SPPreis != 0) item.SPPreis = preis.SPPreis; item.Farbe = item.Farbe | enmColorFlag.SP;
                        if (preis.SSPreis != 0) item.SSPreis = preis.SSPreis; item.Farbe = item.Farbe | enmColorFlag.SS;
                        if (preis.SSPPreis != 0) item.SSPPreis = preis.SSPPreis; item.Farbe = item.Farbe | enmColorFlag.SSP;
                        if (preis.STHPreis != 0) item.STHPreis = preis.STHPreis; item.Farbe = item.Farbe | enmColorFlag.VZ;
                        if (preis.STNPreis != 0) item.STNPreis = preis.STNPreis; item.Farbe = item.Farbe | enmColorFlag.VZP;
                        if (preis.VZPreis != 0) item.VZPreis = preis.VZPreis; item.Farbe = item.Farbe | enmColorFlag.STN;
                        if (preis.VZPPreis != 0) item.VZPPreis = preis.VZPPreis; item.Farbe = item.Farbe | enmColorFlag.STH;
                        if (preis.PPPreis != 0) item.PPPreis = preis.PPPreis; item.Farbe = item.Farbe | enmColorFlag.PP;
                    }
                }
            }

            return katalog;
        }

        public void BulkUpsertNation(List<Nation> nationen)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))

            {
                var collection = db.GetCollection<Nation>("tblNation");

                foreach (var item in nationen)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.ID);

            }
        }

        public void BulkUpsertAera(List<Aera> aera, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Aera>("tblAera");

                foreach (var item in aera)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkUpsertRegion(List<Gebiet> gebiet, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Gebiet>("tblGebiet");

                foreach (var item in gebiet)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkUpsertModule(List<Modul> module, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Modul>("tblModule");

                foreach (var item in module)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.ModulID);
                collection.EnsureIndex(x => x.NationID);
            }
        }

        public void SavePraegeanstalt(Praegeanstalt praegeanstalt, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Praegeanstalt>("tblPraegeanstalt");
                collection.Upsert(praegeanstalt);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.Nation);
            }
        }

        public void SaveBestand(List<Bestand> liste)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Bestand>("tblBestand");
                collection.DeleteMany("1=1");
                collection.InsertBulk(liste);

                collection.EnsureIndex(x => x.Guid);
                collection.EnsureIndex(x => x.AeraID);
                collection.EnsureIndex(x => x.NationID);
                collection.EnsureIndex(x => x.id);
                collection.EnsureIndex(x => x.GebietID);
            }
        }


        public void SaveDownloads(Downloads download)
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Downloads>("tblDownloads");
                collection.Upsert(download);

                collection.EnsureIndex(x => x.ID);
            }
        }

        public void BulkUpsertKatalog(List<Katalog2> katalog, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Katalog2>("tblkatalog");

                foreach (var item in katalog)
                    collection.Upsert(item);

                collection.EnsureIndex(x => x.ID);
                collection.EnsureIndex(x => x.GUID);
                collection.EnsureIndex(x => x.NationID);
                collection.EnsureIndex(x => x.AeraID);
                collection.EnsureIndex(x => x.RegionID);
                collection.EnsureIndex(x => x.KatNr);

            }
        }

        public List<Settings2> ReadModulLizenzen(string database = "")
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Settings2>("tblSettings2");
                var settings2 = collection.FindAll().ToList();

                return settings2;
            }
        }

        public List<Modul> ReadModulLanguage(string language, string typ, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Modul>("tblModule");
                var module = collection.FindAll().Where(x => x.Typ == typ).Where(p => p.Sprache == language).OrderBy(x => x.NationID).ToList();
                return module;
            }
        }

        public List<Modul> ReadModulLanguage(int nation, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Modul>("tblModule");
                var module = collection.FindAll().Where(x => x.NationID == nation).ToList();
                return module;
            }
        }

        public List<Texte> ReadTexteLanguage(string table, int nationID, enmTexte typ, string database)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Texte>(table);
                var module = collection.Find(Query.EQ("NationID", nationID)).ToList();
                return module;
            }
        }

        public List<Katalog2> ReadKatalog(string database, int aera = -1)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collectionKatalog = db.GetCollection<Katalog2>("tblKatalog");
                var katalog = collectionKatalog.Find(Query.EQ("AeraID", aera)).ToList();

                return katalog;
            }
        }

        public void ClearDownloads()
        {
            string connectionstring = Connectionstring();

            using (var db = new LiteDatabase(connectionstring))
                db.DropCollection("tblDownloads");
        }
        #endregion

       

        public void Flush(string database)
        {
            while (File.Exists(logFile))
            {
                string connectionstring = Connectionstring(database);

                using (var db = new LiteDatabase(connectionstring))
                    db.Checkpoint();
            }
        }

        public List<string> Collections(string database)
        {
            List<string> collections = new List<string>();

            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<DBColumns>("$cols");
                var temp = collection.FindAll().ToList();

                foreach (DBColumns item in temp)
                    if (item.Name.Substring(0, 1) != "$")
                        collections.Add(item.Name);
            }
            return collections;
        }

        public CoinBestand GetCoinBestand(string guid, enmSelectedStyle style, decimal faktor, enmPreise preistyp, string modul)
        {
            string connectionstring = Connectionstring(modul);
            CoinBestand coinBestand = new CoinBestand();

            using (var db = new LiteDatabase(connectionstring))
            {
                var item = db.GetCollection<Katalog2>("tblKatalog").FindOne(Query.EQ("Guid", guid));

                coinBestand.PPPreis = item.PPPreis* faktor;
                coinBestand.SPPreis = item.SPPreis * faktor;
                coinBestand.SPreis = item.SPreis * faktor;
                coinBestand.SSPPreis = item.SSPPreis * faktor;
                coinBestand.SSPreis = item.SSPreis * faktor;
                coinBestand.STHPreis = item.STHPreis * faktor;
                coinBestand.STNPreis = item.STNPreis * faktor;
                coinBestand.VZPPreis = item.VZPPreis * faktor;
                coinBestand.VZPreis = item.VZPreis * faktor;

                if (preistyp == enmPreise.EigenePreise)
                {
                    var p = ReadEigenePreise(item.GUID);

                    if (p[0].Preis != 0)
                    {
                        coinBestand.SPreis = p[0].Preis;
                        coinBestand.Farbe = "1";
                    }

                    if (p[1].Preis != 0)
                    {
                        coinBestand.SPPreis = p[1].Preis;
                        coinBestand.Farbe = "1";
                    }

                    if (p[2].Preis != 0)
                    {
                        coinBestand.SSPreis = p[2].Preis;
                        coinBestand.Farbe = "1";
                    }

                    if (p[3].Preis != 0)
                    {
                        coinBestand.SSPPreis = p[3].Preis;
                        coinBestand.Farbe = "1";
                    }

                    if (p[4].Preis != 0)
                    {
                        coinBestand.VZPreis = p[4].Preis;
                        coinBestand.Farbe = "1";
                    }

                    if (p[5].Preis != 0)
                    {
                        coinBestand.VZPPreis = p[5].Preis;
                        coinBestand.Farbe = "1";
                    }

                    if (p[6].Preis != 0)
                    {
                        coinBestand.STNPreis = p[6].Preis;
                        coinBestand.Farbe = "1";
                    }

                    if (p[7].Preis != 0)
                    {
                        coinBestand.STHPreis = p[7].Preis;
                        coinBestand.Farbe = "1";
                    }

                    if (p[8].Preis != 0)
                    {
                        coinBestand.PPPreis = p[8].Preis;
                    }
                }
            }

            connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var col = db.GetCollection<Bestand>("tblBestand");
                var bestand = col.FindOne(Query.EQ("Guid", guid));
                Decimal summe = 0;

                switch (style)
                {
                    case enmSelectedStyle.SammlungUndDoubletten:
                        summe = 0;
                        coinBestand.S = (bestand.S + bestand.DS) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.S, bestand.DS);
                        coinBestand.SP = (bestand.SP + bestand.DSP) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.SP, bestand.DSP);
                        coinBestand.SS = (bestand.SS + bestand.DSS) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.SS, bestand.DSS);
                        coinBestand.SSP = (bestand.SSP + bestand.DSSP) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.SSP, bestand.DSSP);
                        coinBestand.VZ = (bestand.VZ + bestand.DVZ) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.VZ, bestand.DVZ);
                        coinBestand.VZP = (bestand.VZP + bestand.DVZP) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.VZP, bestand.DVZP);
                        coinBestand.STN = (bestand.STN + bestand.DSTN) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.STN, bestand.DSTN);
                        coinBestand.STH = (bestand.STH + bestand.DSTH) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.STH, bestand.DSTH);
                        coinBestand.PP = (bestand.PP + bestand.DPP) == 0 ? string.Empty : string.Format("{0}/{1}", bestand.PP, bestand.DPP);

                        summe += (bestand.S + bestand.DS) * coinBestand.SPreis;
                        summe += (bestand.SP + bestand.DSP) * coinBestand.SPPreis;
                        summe += (bestand.SS + bestand.DSS) * coinBestand.SSPreis;
                        summe += (bestand.SSP + bestand.DSSP) * coinBestand.SSPPreis;
                        summe += (bestand.VZ + bestand.DVZ) * coinBestand.VZPreis;
                        summe += (bestand.VZP + bestand.DVZP) * coinBestand.VZPPreis;
                        summe += (bestand.STN + bestand.DSTN) * coinBestand.STNPreis;
                        coinBestand.SummeS = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;

                        summe = 0;
                        summe += (bestand.STH + bestand.DSTH) * coinBestand.STHPreis;
                        summe += (bestand.PP + bestand.DPP) * coinBestand.PPPreis;
                        coinBestand.SummePP = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;

                        break;

                    case enmSelectedStyle.DoublettenOnly:
                        summe = 0;
                        coinBestand.S = bestand.DS == 0 ? string.Empty : bestand.DS.ToString();
                        coinBestand.SP = bestand.DSP == 0 ? string.Empty : bestand.DSP.ToString();
                        coinBestand.SS = bestand.DSS == 0 ? string.Empty : bestand.DSS.ToString();
                        coinBestand.SSP = bestand.DSSP == 0 ? string.Empty : bestand.DSSP.ToString();
                        coinBestand.VZ = bestand.DVZ == 0 ? string.Empty : bestand.DVZ.ToString();
                        coinBestand.VZP = bestand.DVZP == 0 ? string.Empty : bestand.DVZP.ToString();
                        coinBestand.STN = bestand.DSTN == 0 ? string.Empty : bestand.DSTN.ToString();
                        coinBestand.STH = bestand.DSTH == 0 ? string.Empty : bestand.DSTH.ToString();
                        coinBestand.PP = bestand.DPP == 0 ? string.Empty : bestand.DPP.ToString();

                        summe += bestand.DS * coinBestand.SPreis;
                        summe += bestand.DSP * coinBestand.SPPreis;
                        summe += bestand.DSS * coinBestand.SSPreis;
                        summe += bestand.DSSP * coinBestand.SSPPreis;
                        summe += bestand.DVZ * coinBestand.VZPreis;
                        summe += bestand.DVZP * coinBestand.VZPPreis;
                        summe += bestand.DSTN * coinBestand.STNPreis;
                        coinBestand.SummeS = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;

                        summe = 0;
                        summe += bestand.DSTH * coinBestand.STHPreis;
                        summe += bestand.DPP * coinBestand.PPPreis;
                        coinBestand.SummePP = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;
                        break;

                    case enmSelectedStyle.Icon:
                    case enmSelectedStyle.SammlungOnly:
                        coinBestand.S = bestand.S == 0 ? string.Empty : bestand.S.ToString();
                        coinBestand.SP = bestand.SP == 0 ? string.Empty : bestand.SP.ToString();
                        coinBestand.SS = bestand.SS == 0 ? string.Empty : bestand.SS.ToString();
                        coinBestand.SSP = bestand.SSP == 0 ? string.Empty : bestand.SSP.ToString();
                        coinBestand.VZ = bestand.VZ == 0 ? string.Empty : bestand.VZ.ToString();
                        coinBestand.VZP = bestand.VZP == 0 ? string.Empty : bestand.VZP.ToString();
                        coinBestand.STN = bestand.STN == 0 ? string.Empty : bestand.STN.ToString();
                        coinBestand.STH = bestand.STH == 0 ? string.Empty : bestand.STH.ToString();
                        coinBestand.PP = bestand.PP == 0 ? string.Empty : bestand.PP.ToString();

                        summe = 0;
                        summe += bestand.S * coinBestand.SPreis;
                        summe += bestand.SP * coinBestand.SPPreis;
                        summe += bestand.SS * coinBestand.SSPreis;
                        summe += bestand.SSP * coinBestand.SSPPreis;
                        summe += bestand.VZ * coinBestand.VZPreis;
                        summe += bestand.VZP * coinBestand.VZPPreis;
                        summe += bestand.STN * coinBestand.STNPreis;
                        coinBestand.SummeS = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;

                        summe = 0;
                        summe += bestand.STH * coinBestand.STHPreis;
                        summe += bestand.PP * coinBestand.PPPreis;
                        coinBestand.SummePP = summe != 0 ? string.Format("{0:#,##0.00}", summe * faktor) : string.Empty;
                        break;
                }
            }

            return coinBestand;
        }

        #region Reporting
        public List<Report> Reporting(enmReportTyp reportTyp, 
                                        int nation, 
                                        int ära, 
                                        int region, 
                                        decimal faktor,
                                        List<Nation> nationen,
                                        enmPreise settings,
                                        string database = null)
        {
            List<Bestand> bestand = null;
            List<Report> reporting = new List<Report>();

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                if (nation == 0)
                    bestand = db.GetCollection<Bestand>("tblBestand").FindAll().ToList();
                else
                {
                    bestand = db.GetCollection<Bestand>("tblBestand").Find(x => x.NationID == nation).ToList();

                    if (ära != 0)
                        bestand = bestand.Where(a => a.AeraID == ära).ToList();

                    if (region != 0)
                        bestand = bestand.Where(a => a.GebietID == region).ToList();
                }
            }

            if (nation == 0)
            {
                foreach (var item in nationen)
                {
                    bestand = GetKatalogPreise(bestand, faktor, nation, item.Key);
                    if (settings == enmPreise.EigenePreise)
                        bestand = GetOwnPrices(bestand, nation);
                }
            }
            else
            {
                bestand = GetKatalogPreise(bestand, faktor, nation, database);
                if (settings == enmPreise.EigenePreise)
                    bestand = GetOwnPrices(bestand, nation);
            }

            foreach (var item in bestand)
            {
                Report r = new Report();
                r.KatNr = item.KatNr;
                r.AeraID = item.AeraID;
                r.GebietID = item.GebietID;
                r.GUID = item.Guid;
                r.Jahrgang = item.Jahrgang;
                r.Muenzz = item.Muenzzeichen;
                r.NationID = item.NationID;
                r.Nominal = item.Nominal;
                r.PPPreis = item.PPP;
                r.SPPreis = item.PSP;
                r.SPreis = item.PS;
                r.SSPPreis = item.PSSP;
                r.SSPreis = item.PSS;
                r.STHPreis = item.PSTH;
                r.STNPreis = item.PSTN;
                r.VZPPreis = item.PVZP;
                r.VZPreis = item.PVZ;
                r.Waehrung = item.Waehrung;

                if (reportTyp == enmReportTyp.ReportSammlung || reportTyp == enmReportTyp.KostenSammlung)
                {
                    r.PP = item.PP;
                    r.S = item.S;
                    r.SP = item.SP;
                    r.SS = item.SS;
                    r.SSP = item.SSP;
                    r.STH = item.STH;
                    r.STN = item.STN;
                    r.VZ = item.VZ;
                    r.VZP = item.VZP;
                    r.Gesamt = item.S * item.PS + item.SP * item.PSP + item.SS * item.PSS + item.SSP * item.PSSP + item.VZ * item.PVZ
                               + item.VZP * item.PVZP + item.STN * item.PSTN + item.STH * item.PSTH + item.PP * item.PPP;
                    r.Farbe = item.Farbe;
                }
                else
                {
                    r.PP = item.DPP;
                    r.S = item.DS;
                    r.SP = item.DSP;
                    r.SS = item.DSS;
                    r.SSP = item.DSSP;
                    r.STH = item.DSTH;
                    r.STN = item.DSTN;
                    r.VZ = item.DVZ;
                    r.VZP = item.DVZP;
                    r.Gesamt = item.DS * item.PS + item.DSP * item.PSP + item.DSS * item.PSS + item.DSSP * item.PSSP + item.DVZ * item.PVZ
                               + item.DVZP * item.PVZP + item.DSTN * item.PSTN + item.DSTH * item.PSTH + item.DPP * item.PPP;
                    r.Farbe = item.Farbe;
                }

                if (r.Gesamt > 0)
                    reporting.Add(r);
            }

            reporting = reporting.OrderBy(a => a.KatNr).ThenBy(b => b.Nominal).ThenBy(c => c.Jahrgang).ThenBy(d => d.Muenzz).ToList();

            return reporting;
        }

        public List<Bestand> Reporting2(enmReportTyp reportTyp, int nation, int ära, int region, enmPreise settings, decimal faktor, List<Nation> nationen, string database)
        {
            List<Bestand> bestand = new List<Bestand>();
            List<Report2> reporting = new List<Report2>();

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                if (nation == 0)
                    bestand = db.GetCollection<Bestand>("tblBestand").FindAll().ToList();
                else
                {
                    bestand = db.GetCollection<Bestand>("tblBestand").Find(x => x.NationID == nation).ToList();

                    if (ära != 0)
                        bestand = bestand.Where(a => a.AeraID == ära).ToList();

                    if (region != 0)
                        bestand = bestand.Where(a => a.GebietID == region).ToList();
                }
            }

            if (nation == 0)
            {
                foreach (var item in nationen)
                {
                    bestand = GetKatalogPreise(bestand, faktor, nation, item.Key);
                    if (settings == enmPreise.EigenePreise)
                        bestand = GetOwnPrices(bestand, nation);
                }
            }
            else
            {
                bestand = GetKatalogPreise(bestand, faktor, nation, database);
                if (settings == enmPreise.EigenePreise)
                    bestand = GetOwnPrices(bestand, nation);
            }

            return bestand;
        }

        public List<Bestand> GetKatalogPreise(List<Bestand> bestand, decimal faktor, int nation, string database = null)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                List<Katalog2> katalogpreise;

                var collectionKatalogPreise = db.GetCollection<Katalog2>("tblKatalog");

                if (nation == 0)
                    katalogpreise = collectionKatalogPreise.FindAll().ToList();
                else
                    katalogpreise = collectionKatalogPreise.Find(Query.EQ("NationID", nation)).ToList();

                for (int i = 0; i < bestand.Count; i++)
                {
                    var p = katalogpreise.FirstOrDefault(x => x.GUID == bestand[i].Guid);

                    if (p != null)
                    {
                        bestand[i].PS = p.SPreis * faktor;
                        bestand[i].PSP = p.SPPreis * faktor;
                        bestand[i].PSS = p.SSPreis * faktor;
                        bestand[i].PSSP = p.SSPPreis * faktor;
                        bestand[i].PVZ = p.VZPreis * faktor;
                        bestand[i].PVZP = p.VZPPreis * faktor;
                        bestand[i].PSTN = p.STNPreis * faktor;
                        bestand[i].PSTH = p.STHPreis * faktor;
                        bestand[i].PPP = p.PPPreis * faktor;
                        bestand[i].Jahrgang = p.Jahrgang;
                        bestand[i].Muenzzeichen = p.Muenzzeichen;
                        bestand[i].Nominal = p.Nominal;
                        bestand[i].Waehrung = p.Waehrung;
                        bestand[i].KatNr = p.KatNr;
                        bestand[i].Motiv = p.Motiv;
                        bestand[i].AeraID = p.AeraID;
                        bestand[i].GebietID = p.RegionID;
                        bestand[i].NationID = p.NationID;
                        bestand[i].Preis = 0;
                    }
                }
            }

            return bestand;
        }

        public List<Bestand> GetOwnPrices(List<Bestand> bestand, int nation)
        {
            List<Preise> preise;

            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {
                var collectionPreise = db.GetCollection<Preise>("tblPreise");

                if (nation == 0)
                    preise = collectionPreise.FindAll().ToList();
                else
                    preise = collectionPreise.Find(Query.EQ("NationID", nation)).ToList();

                for (int i = 0; i < bestand.Count; i++)
                {
                    var p = preise.FirstOrDefault(x => x.GUID == bestand[i].Guid);

                    if (p != null)
                    {
                        if (p.SPreis != 0)
                        {
                            bestand[i].PS = p.SPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.S;
                        }

                        if (p.SPPreis != 0)
                        {
                            bestand[i].PSP = p.SPPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.SP;
                        }

                        if (p.SSPreis != 0)
                        {
                            bestand[i].PSS = p.SSPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.SS;
                        }

                        if (p.SSPPreis != 0)
                        {
                            bestand[i].PSSP = p.SSPPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.SSP;
                        }

                        if (p.VZPreis != 0)
                        {
                            bestand[i].PVZ = p.VZPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.VZ;
                        }

                        if (p.VZPPreis != 0)
                        {
                            bestand[i].PVZP = p.VZPPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.VZP;
                        }

                        if (p.STNPreis != 0)
                        {
                            bestand[i].PSTN = p.STNPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.STN;
                        }

                        if (p.STHPreis != 0)
                        {
                            bestand[i].PSTH = p.STHPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.STH;
                        }

                        if (p.PPPreis != 0)
                        {
                            bestand[i].PPP = p.PPPreis;
                            bestand[i].Farbe = bestand[i].Farbe | enmColorFlag.PP;
                        }
                    }
                }
            }

            return bestand;
        }

        public List<Wertermittlung> ReportingWert(enmReportTyp reportTyp,
                                         int nation,
                                         decimal faktor,
                                         enmPreise settings,
                                         List<Nation> nationen,
                                         List<Aera> aeras, 
                                         string database)
        {
            List<Wertermittlung> wertermittlung = null;

            if (reportTyp == enmReportTyp.KostenSammlung || reportTyp == enmReportTyp.KostenDoubletten)
                settings = enmPreise.Kaufpreise;

            if (settings == enmPreise.Kaufpreise && reportTyp == enmReportTyp.WerteSammlung)
                reportTyp = enmReportTyp.KostenSammlung;

            if (settings == enmPreise.Kaufpreise && reportTyp == enmReportTyp.WerteDoubletten)
                reportTyp = enmReportTyp.KostenDoubletten;

            string connectionstring = Connectionstring("Sammlung");

            List<Bestand> bestand;

            using (var db = new LiteDatabase(connectionstring))
            {
                if (nation == 0)
                    bestand = db.GetCollection<Bestand>("tblBestand").FindAll().ToList();
                else
                    bestand = db.GetCollection<Bestand>("tblBestand").Find(x => x.NationID == nation).ToList();

            }

            switch (reportTyp)
            {
                case enmReportTyp.WerteSammlung:
                    if (nation == 0)
                    {
                        foreach (var item in nationen)
                            bestand = GetKatalogPreise(bestand, faktor, nation, item.Key);
                    }
                    else
                    {
                        bestand = GetKatalogPreise(bestand, faktor, nation, database);
                    }

                    if (settings == enmPreise.EigenePreise)
                        bestand = GetOwnPrices( bestand, nation);

                    for (int i = 0; i < bestand.Count; i++)
                    {
                        bestand[i].Preis = bestand[i].PS * bestand[i].S + bestand[i].PSP * bestand[i].SP + bestand[i].PSS * bestand[i].SS + bestand[i].PSSP * bestand[i].SSP;
                        bestand[i].Preis += bestand[i].PVZ * bestand[i].VZ + bestand[i].PVZP * bestand[i].VZP + bestand[i].PSTN * bestand[i].STN + bestand[i].PSTH * bestand[i].STH;
                        bestand[i].Preis += bestand[i].PPP * bestand[i].PP;
                        bestand[i].Gesamt = bestand[i].Preis;
                    }

                    wertermittlung = Helper.Werteberechnung(bestand, nation, nationen, aeras, false);
                    break;

                case enmReportTyp.WerteDoubletten:
                    if (nation == 0)
                    {
                        foreach (var item in nationen)
                            bestand = GetKatalogPreise(bestand, faktor, nation, item.Key);
                    }
                    else
                    {
                        bestand = GetKatalogPreise(bestand, faktor, nation, database);
                    }

                    if (settings == enmPreise.EigenePreise)
                        bestand = GetOwnPrices( bestand, nation);

                    for (int i = 0; i < bestand.Count; i++)
                    {
                        bestand[i].Preis = bestand[i].PS * bestand[i].DS + bestand[i].PSP * bestand[i].DSP + bestand[i].PSS * bestand[i].DSS + bestand[i].PSSP * bestand[i].DSSP;
                        bestand[i].Preis += bestand[i].PVZ * bestand[i].DVZ + bestand[i].PVZP * bestand[i].DVZP + bestand[i].PSTN * bestand[i].DSTN + bestand[i].PSTH * bestand[i].DSTH;
                        bestand[i].Preis += bestand[i].PPP * bestand[i].DPP;
                        bestand[i].Gesamt = bestand[i].Preis;
                    }

                    wertermittlung = Helper.Werteberechnung(bestand, nation, nationen, aeras, true);
                    break;

                case enmReportTyp.KostenSammlung:
                    bestand = GetKaufpreise(bestand, false);
                    wertermittlung = Helper.Werteberechnung(bestand, nation, nationen, aeras, false);
                    break;

                case enmReportTyp.KostenDoubletten:
                    bestand = GetKaufpreise(bestand, true);
                    wertermittlung = Helper.Werteberechnung(bestand, nation, nationen, aeras, true);
                    break;
            }

            for (int i = 0; i < wertermittlung.Count; i++)
            {
                wertermittlung[i].NationID = nation;
                if (wertermittlung[i].S == 0) wertermittlung[i].S = null;
                if (wertermittlung[i].SP == 0) wertermittlung[i].SP = null;
                if (wertermittlung[i].SS == 0) wertermittlung[i].SS = null;
                if (wertermittlung[i].SSP == 0) wertermittlung[i].SSP = null;
                if (wertermittlung[i].VZ == 0) wertermittlung[i].VZ = null;
                if (wertermittlung[i].VZP == 0) wertermittlung[i].VZP = null;
                if (wertermittlung[i].STN == 0) wertermittlung[i].STN = null;
                if (wertermittlung[i].STH == 0) wertermittlung[i].STH = null;
                if (wertermittlung[i].PP == 0) wertermittlung[i].PP = null;
            }
            return wertermittlung;
        }

        public List<Bestand> GetKaufpreise(List<Bestand> bestand, bool doublette)
        {
            string connectionstring = Connectionstring("Sammlung");

            using (var db = new LiteDatabase(connectionstring))
            {

                var collectionSammlung = db.GetCollection<Sammlung>("tblSammlung");
                var kaufpreise = collectionSammlung.Find(x => x.Doublette == doublette).ToList();

                for (int i = 0; i < bestand.Count; i++)
                {
                    var preise = kaufpreise.Where(a => a.Guid == bestand[i].Guid).ToList();

                    foreach (var item in preise)
                        bestand[i].Gesamt += item.Kaufpreis;
                }
            }

            return bestand;
        }
        #endregion Reporting

        public List<Katalog3> ReadQuickKatalog(string database, int aera = -1, int region =-1)
        {
            string connectionstring = Connectionstring(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collectionKatalog = db.GetCollection<Katalog3>("tblKatalog");
                var katalog = collectionKatalog.FindAll().Where(x => x.AeraID == aera).Where(p => p.RegionID == region)
                        .OrderBy(x => x.KatNr).ThenBy(x => x.Jahrgang).ThenBy(x => x.Muenzzeichen).ToList();

                return katalog;
            }
        }

        public void ImportAllgemein(string database = "")
        {
            List <Erhaltungsgrad> erhaltungsgrade = new List<Erhaltungsgrad>();
            List<Nation> nations = new List<Nation>();
            List<Culture> culture = new List<Culture>();

            string connectionstring = Connectionstring();
            string connectionstringAllgemein = ConnectionstringAllgemein(database);

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Erhaltungsgrad>("tblErhaltungsgrade");
                erhaltungsgrade = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(connectionstringAllgemein))
            {
                var collection = db.GetCollection<Erhaltungsgrad>("tblErhaltungsgrade");
                collection.DeleteAll();
                collection.InsertBulk(erhaltungsgrade);
            }

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Nation>("tblNation");
                nations = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(connectionstringAllgemein))
            {
                var collection = db.GetCollection<Nation>("tblNation");
                collection.DeleteAll();
                collection.InsertBulk(nations);
            }

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Nation>("tblNationEN");
                nations = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(connectionstringAllgemein))
            {
                var collection = db.GetCollection<Nation>("tblNationEN");
                collection.DeleteAll();
                collection.InsertBulk(nations);
            }

            using (var db = new LiteDatabase(connectionstring))
            {
                var collection = db.GetCollection<Culture>("tblCulture");
                culture = collection.FindAll().ToList();
            }

            using (var db = new LiteDatabase(connectionstringAllgemein))
            {
                var collection = db.GetCollection<Culture>("tblCulture");
                collection.DeleteAll();
                collection.InsertBulk(culture);
            }

        }


    }
}
