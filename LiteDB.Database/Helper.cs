using Coinbook.Enumerations;
using Coinbook.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LiteDB.Database
{
    public static class Helper
    {
        internal static List<Bestand> GetOwnPrices(LiteDatabase db, List<Bestand> bestand, int nationID)
        {
            List<Preise> preise;

            var collectionPreise = db.GetCollection<Preise>("tblPreise");

            if (nationID == 0)
                preise = collectionPreise.FindAll().ToList();
            else
                preise = collectionPreise.Find(Query.EQ("NationID", nationID)).ToList();

            for (int i = 0; i < bestand.Count; i++)
            {
                var p = preise.FirstOrDefault(x => x.GUID == bestand[i].Guid);

                if (p != null)
                {
                    if (p.SPreis != 0)
                        bestand[i].PS = p.SPreis;

                    if (p.SPPreis != 0)
                        bestand[i].PSP = p.SPPreis;

                    if (p.SSPreis != 0)
                        bestand[i].PSS = p.SSPreis;

                    if (p.SSPPreis != 0)
                        bestand[i].PSSP = p.SSPPreis;

                    if (p.VZPreis != 0)
                        bestand[i].PVZ = p.VZPreis;

                    if (p.VZPPreis != 0)
                        bestand[i].PVZP = p.VZPPreis;

                    if (p.STNPreis != 0)
                        bestand[i].PSTN = p.STNPreis;

                    if (p.STHPreis != 0)
                        bestand[i].PSTH = p.STHPreis;

                    if (p.PPPreis != 0)
                        bestand[i].PPP = p.PPPreis;
                }
            }

            return bestand;
        }

        public static List<Bestand> GetKatalogPreise(LiteDatabase db, List<Bestand> bestand, int nation, enmPreise settings, decimal faktor)
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

                    switch (Convert.ToInt16(bestand[i].Erhaltung))
                    {
                        case 1:
                            bestand[i].Preis = bestand[i].PS;
                            break;

                        case 2:
                            bestand[i].Preis = bestand[i].PSP;
                            break;

                        case 3:
                            bestand[i].Preis = bestand[i].PSS;
                            break;

                        case 4:
                            bestand[i].Preis = bestand[i].PSSP;
                            break;

                        case 5:
                            bestand[i].Preis = bestand[i].PVZ;
                            break;

                        case 6:
                            bestand[i].Preis = bestand[i].PVZP;
                            break;

                        case 7:
                            bestand[i].Preis = bestand[i].PSTN;
                            break;

                        case 8:
                            bestand[i].Preis = bestand[i].PSTH;
                            break;

                        case 9:
                            bestand[i].Preis = bestand[i].PPP;
                            break;
                    }
                }
            }

            if (settings == enmPreise.EigenePreise)
                bestand = Helper.GetOwnPrices(db, bestand, nation);

            return bestand;
        }

        internal static List<Wertermittlung> BerechnetSummenzeile(List<Wertermittlung> wertermittlung)
        {
            wertermittlung = wertermittlung.Where(a => a.Anzahl != 0).ToList();

            Wertermittlung summe = new Wertermittlung { S = 0, SP = 0, SS = 0, SSP = 0, VZ = 0, VZP = 0, STN = 0, STH = 0, PP = 0, Gesamt = 0, Anzahl = 0, Nation = "Gesamt" };
            foreach (var item in wertermittlung)
            {
                summe.S += item.S;
                summe.SP += item.SP;
                summe.SS += item.SS;
                summe.SSP += item.SSP;
                summe.VZ += item.VZ;
                summe.VZP += item.VZP;
                summe.STN += item.STN;
                summe.STH += item.STH;
                summe.PP += item.PP;
                summe.Anzahl += item.Anzahl;
                summe.Gesamt += item.Gesamt;
            }
            wertermittlung.Add(summe);

            return wertermittlung;
        }

        internal static List<Wertermittlung> WerteberechnungSammlungNation(List<Bestand> bestand, List<Nation> nationen)
        {
            var temp = from b in bestand
                       group b by b.NationID into g
                       select new
                       {
                           Nation = "",
                           S = g.Sum(x => x.S),
                           SP = g.Sum(x => x.SP),
                           SS = g.Sum(x => x.SS),
                           SSP = g.Sum(x => x.SSP),
                           VZ = g.Sum(x => x.VZ),
                           VZP = g.Sum(x => x.VZP),
                           STN = g.Sum(x => x.STN),
                           STH = g.Sum(x => x.STH),
                           PP = g.Sum(x => x.PP),
                           Anzahl = g.Sum(x => x.S)
                               + g.Sum(x => x.SP)
                               + g.Sum(x => x.SS)
                               + g.Sum(x => x.SSP)
                               + g.Sum(x => x.VZ)
                               + g.Sum(x => x.VZP)
                               + g.Sum(x => x.STN)
                               + g.Sum(x => x.STH)
                               + g.Sum(x => x.PP),
                           Farbe=g.Max(x => x.Farbe),
                           Betrag = g.Sum(x => x.Gesamt),
                           NationID = g.Key
                       };

            List<Wertermittlung> wertermittlung = new List<Wertermittlung>();


            foreach (var item in temp)
            {
                if (item.NationID != 0)
                {
                    var nation = nationen.FirstOrDefault(x => x.ID == item.NationID);

                    if (nation != null)
                    {
                        Wertermittlung w = new Wertermittlung();
                        w.S = item.S;
                        w.SP = item.SP;
                        w.SS = item.SS;
                        w.SSP = item.SSP;
                        w.VZ = item.VZ;
                        w.VZP = item.VZP;
                        w.STN = item.STN;
                        w.STH = item.STH;
                        w.PP = item.PP;
                        w.Gesamt = item.Betrag;
                        w.Anzahl = item.Anzahl;
                        w.NationID = item.NationID;
                        w.Nation = nation.Bezeichnung;
                        w.Farbe = item.Farbe;

                        wertermittlung.Add(w);
                    }
                }
            }

            wertermittlung = wertermittlung.OrderBy(x => x.Nation).ToList();

            return wertermittlung;
        }

        internal static List<Wertermittlung> WerteberechnungSammlungAera(List<Bestand> bestand, List<Aera> aeras)
        {
            var temp = from b in bestand
                       group b by b.AeraID into g
                       select new
                       {
                           Nation = "",
                           S = g.Sum(x => x.S),
                           SP = g.Sum(x => x.SP),
                           SS = g.Sum(x => x.SS),
                           SSP = g.Sum(x => x.SSP),
                           VZ = g.Sum(x => x.VZ),
                           VZP = g.Sum(x => x.VZP),
                           STN = g.Sum(x => x.STN),
                           STH = g.Sum(x => x.STH),
                           PP = g.Sum(x => x.PP),
                           Anzahl = g.Sum(x => x.S)
                               + g.Sum(x => x.SP)
                               + g.Sum(x => x.SS)
                               + g.Sum(x => x.SSP)
                               + g.Sum(x => x.VZ)
                               + g.Sum(x => x.VZP)
                               + g.Sum(x => x.STN)
                               + g.Sum(x => x.STH)
                               + g.Sum(x => x.PP),
                           Farbe=g.Max(x => x.Farbe),
                           Betrag = g.Sum(x => x.Gesamt),
                           NationID = g.Key
                       };

            List<Wertermittlung> wertermittlung = new List<Wertermittlung>();


            foreach (var item in temp)
            {
                if (item.NationID != 0)
                {
                    Wertermittlung w = new Wertermittlung();
                    w.S = item.S;
                    w.SP = item.SP;
                    w.SS = item.SS;
                    w.SSP = item.SSP;
                    w.VZ = item.VZ;
                    w.VZP = item.VZP;
                    w.STN = item.STN;
                    w.STH = item.STH;
                    w.PP = item.PP;
                    w.Gesamt = item.Betrag;
                    w.Anzahl = item.Anzahl;
                    w.Nation = item.NationID.ToString();
                    w.Nation = aeras.FirstOrDefault(x => x.ID == item.NationID).Bezeichnung;
                    w.NationID = item.NationID;
                    w.Farbe = item.Farbe;

                    wertermittlung.Add(w);
                }
            }

            wertermittlung = wertermittlung.OrderBy(x => x.Nation).ToList();

            return wertermittlung;
        }

        internal static List<Wertermittlung> WerteberechnungDoublettenNation(List<Bestand> bestand, List<Nation> nationen)
        {
            var temp = from b in bestand
                       group b by b.NationID into g
                       select new
                       {
                           Nation = "",
                           S = g.Sum(x => x.DS),
                           SP = g.Sum(x => x.DSP),
                           SS = g.Sum(x => x.DSS),
                           SSP = g.Sum(x => x.DSSP),
                           VZ = g.Sum(x => x.DVZ),
                           VZP = g.Sum(x => x.DVZP),
                           STN = g.Sum(x => x.DSTN),
                           STH = g.Sum(x => x.DSTH),
                           PP = g.Sum(x => x.DPP),
                           Anzahl = g.Sum(x => x.DS)
                               + g.Sum(x => x.DSP)
                               + g.Sum(x => x.DSS)
                               + g.Sum(x => x.DSSP)
                               + g.Sum(x => x.DVZ)
                               + g.Sum(x => x.DVZP)
                               + g.Sum(x => x.DSTN)
                               + g.Sum(x => x.DSTH)
                               + g.Sum(x => x.DPP),
                           Farbe=g.Max(x => x.Farbe),
                           Betrag = g.Sum(x => x.Gesamt),
                           NationID = g.Key
                       };

            List<Wertermittlung> wertermittlung = new List<Wertermittlung>();


            foreach (var item in temp)
            {
                if (item.NationID != 0)
                {
                    Wertermittlung w = new Wertermittlung();
                    w.S = item.S;
                    w.SP = item.SP;
                    w.SS = item.SS;
                    w.SSP = item.SSP;
                    w.VZ = item.VZ;
                    w.VZP = item.VZP;
                    w.STN = item.STN;
                    w.STH = item.STH;
                    w.PP = item.PP;
                    w.Gesamt = item.Betrag;
                    w.Anzahl = item.Anzahl;
                    w.Nation = nationen.FirstOrDefault(x => x.ID == item.NationID).Bezeichnung;
                    w.NationID = item.NationID;
                    w.Farbe = item.Farbe;

                    wertermittlung.Add(w);
                }
            }

            wertermittlung = wertermittlung.OrderBy(x => x.Nation).ToList();

            return wertermittlung;
        }

        internal static List<Wertermittlung> WerteberechnungDoublettenAera(List<Bestand> bestand, List<Aera> aeras)
        {
            var temp = from b in bestand
                       group b by b.AeraID into g
                       select new
                       {
                           Nation = "",
                           S = g.Sum(x => x.DS),
                           SP = g.Sum(x => x.DSP),
                           SS = g.Sum(x => x.DSS),
                           SSP = g.Sum(x => x.DSSP),
                           VZ = g.Sum(x => x.DVZ),
                           VZP = g.Sum(x => x.DVZP),
                           STN = g.Sum(x => x.DSTN),
                           STH = g.Sum(x => x.DSTH),
                           PP = g.Sum(x => x.DPP),
                           Anzahl = g.Sum(x => x.DS)
                               + g.Sum(x => x.DSP)
                               + g.Sum(x => x.DSS)
                               + g.Sum(x => x.DSSP)
                               + g.Sum(x => x.DVZ)
                               + g.Sum(x => x.DVZP)
                               + g.Sum(x => x.DSTN)
                               + g.Sum(x => x.DSTH)
                               + g.Sum(x => x.DPP),
                           Farbe=g.Max(x => x.Farbe),
                           Betrag = g.Sum(x => x.Gesamt),
                           NationID = g.Key
                       };

            List<Wertermittlung> wertermittlung = new List<Wertermittlung>();


            foreach (var item in temp)
            {
                if (item.NationID != 0)
                {
                    Wertermittlung w = new Wertermittlung();
                    w.S = item.S;
                    w.SP = item.SP;
                    w.SS = item.SS;
                    w.SSP = item.SSP;
                    w.VZ = item.VZ;
                    w.VZP = item.VZP;
                    w.STN = item.STN;
                    w.STH = item.STH;
                    w.PP = item.PP;
                    w.Gesamt = item.Betrag;
                    w.Anzahl = item.Anzahl;
                    w.Nation = aeras.FirstOrDefault(x => x.ID == item.NationID).Bezeichnung;
                    w.NationID = item.NationID;
                    w.Farbe = item.Farbe;

                    wertermittlung.Add(w);
                }
            }

            wertermittlung = wertermittlung.OrderBy(x => x.Nation).ToList();

            return wertermittlung;
        }

        internal static List<Bestand> GetKaufpreise(LiteDatabase db, List<Bestand> bestand, bool doublette)
        {
            var collectionSammlung = db.GetCollection<Sammlung>("tblSammlung");
            var kaufpreise = collectionSammlung.Find(x => x.Doublette == doublette).ToList();

            for (int i = 0; i < bestand.Count; i++)
            {
                var preise = kaufpreise.Where(a => a.Guid == bestand[i].Guid).ToList();

                foreach (var item in preise)
                    bestand[i].Gesamt += item.Kaufpreis;
            }

            return bestand;
        }

        internal static List<Wertermittlung> Werteberechnung(List<Bestand> bestand, int nation, List<Nation> nationen, List<Aera> aeras, bool doublette)
        {
            List<Wertermittlung> wertermittlung;

            if (nation == 0)
                wertermittlung = doublette ? Helper.WerteberechnungDoublettenNation(bestand, nationen) : Helper.WerteberechnungSammlungNation(bestand, nationen);
            else
                wertermittlung = doublette ? Helper.WerteberechnungDoublettenAera(bestand, aeras) : Helper.WerteberechnungSammlungAera(bestand, aeras);

            wertermittlung = BerechnetSummenzeile(wertermittlung);

            return wertermittlung;
        }

        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            try
            {
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "Settings";
                xRoot.IsNullable = true;

                StreamReader xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                var x = ex;
                //ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now);
            }
            return returnObject;
        }

    }
}
