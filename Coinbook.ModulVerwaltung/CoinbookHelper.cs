using Coinbook.Enumerations;
using Coinbook.Lokalisierung;
using Coinbook.Model;
//using Coinbook.Models;
using LiteDB;
using SAN.Converter;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Coinbook
{
    public static class CoinbookHelper
    {
        static CoinbookHelper()
        {
            Filter = new Filter { Jahrgang = "", Nominal = "", Waehrung = "" };
            HauptFilter = new HauptFilter { NationID = -1, AeraID = -1, GebietID = -1 };
        }

        public static Color ColorNation = Color.IndianRed;
        public static Color ColorÄra = Color.AliceBlue;
        public static Color ColorGebiet = Color.LightGreen;
        public static Color ColorColumns = Color.Silver;
        public static Color ColorSumme = Color.Gold;
        
        public static string DataPath { get; set; }
        public static string Cultur { get; set; }
        public static int User { get; set; }
        public static CultureInfo Culture { get => new CultureInfo(Settings.Culture); }

        private static FTPModel ftp = null;
        public static FTPModel FTP
        {
            get
            {
                if (ftp == null)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(FTPModel));
                    var file = "ftpModule.config";
                    var x = File.Exists(file);

                    using (Stream reader = new FileStream(file, FileMode.Open))
                        ftp = (FTPModel)serializer.Deserialize(reader);

                    ftp.Passwort = Decrypt(ftp.Passwort);
                    ftp.TransferPasswort = "magixx-1";
                }

                return ftp;
            }
        }

        public static string Encrypt(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        private static string Decrypt(string text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        }

        public static string BackupPath
        {
            get
            {
                return Path.Combine(DataPath, "Backup");
            }
        }

        public static string UpdatePath
        {
            get
            {
                return Path.Combine(DataPath, "Updater");
            }
        }

        public static string DownloadPath
        {
            get
            {
                return Path.Combine(DataPath, "Downloads");
            }
        }

        public static string InfoPath
        {
            get
            {
                return System.IO.Path.Combine(DataPath, "Info");
            }
        }

        public static string Picturepath
        {
            get
            {
                return System.IO.Path.Combine(DataPath, "Bilder");
            }
        }

        public static string Sammlungsanzeige(string text, bool doublette, int menge)
        {
            int s = 0;
            int d = 0;
            string tmp = text;

            if (tmp != String.Empty)
            {
                if (tmp.IndexOf("/") == -1)
                    s = ConvertEx.ToInt32(tmp);
                else
                {
                    s = ConvertEx.ToInt32(tmp.Substring(0, tmp.IndexOf("/")));
                    d = ConvertEx.ToInt32(tmp.Substring(tmp.IndexOf("/") + 1));
                }
            }

            if (!doublette)
                s = s + menge;
            else
                d = d + menge;

            tmp = s.ToString() + "/" + d.ToString();
            if (tmp == "0/0")
                tmp = string.Empty;

            return tmp;

        }

        public static void WriteDataTable(string path, DataTable datatable, char seperator)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                int numberOfColumns = datatable.Columns.Count;

                for (int i = 0; i < numberOfColumns; i++)
                {
                    sw.Write(datatable.Columns[i]);
                    if (i < numberOfColumns - 1)
                        sw.Write(seperator);
                }
                sw.Write(sw.NewLine);

                foreach (DataRow dr in datatable.Rows)
                {
                    for (int i = 0; i < numberOfColumns; i++)
                    {
                        sw.Write(dr[i].ToString());

                        if (i < numberOfColumns - 1)
                            sw.Write(seperator);
                    }
                    sw.Write(sw.NewLine);
                }
            }
        }

        public static void WriteList(string path, List<Report> liste, char seperator)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                Report r = new Report();


                //int numberOfColumns = datatable.Columns.Count;

                //for (int i = 0; i < numberOfColumns; i++)
                //{
                //    sw.Write(datatable.Columns[i]);
                //    if (i < numberOfColumns - 1)
                //        sw.Write(seperator);
                //}
                //sw.Write(sw.NewLine);

                //foreach (Report item in liste)
                //{
                //    for (int i = 0; i < numberOfColumns; i++)
                //    {
                //        sw.Write(dr[i].ToString());

                //        if (i < numberOfColumns - 1)
                //            sw.Write(seperator);
                //    }
                //    sw.Write(sw.NewLine);
                //}
            }
        }

        public static Settings Settings { get; set; }
        public static List<Erhaltungsgrad> Erhaltungsgrade { get; set; }
        public static List<Nation> Nationen { get; set; }
        public static List<Aera> Aeras { get; set; }

        public static List<Nation> GetNations(string all = null)
        {
            var result = Nationen;

            result = result.OrderBy(x => x.Bezeichnung).ToList();

            if (!string.IsNullOrEmpty(all))
            {
                Nation item = new Nation();
                item.ID = 0;
                item.Bezeichnung = all;
                result.Insert(0, item);
            }

            return result;
        }

        public static List<Aera> GetAeras(int nation, string all = null)
        {
            var result = Aeras.Where(s => s.NationID == nation).ToList();

            if (nation == 12 || nation == 32)
                result = result.OrderBy(x => x.Bezeichnung).ToList();
            else
                result = result.OrderBy(x => x.Sortierung).ToList();

            if (!string.IsNullOrEmpty(all))
            {
                Aera item = new Aera();
                item.ID = 0;
                item.Bezeichnung = all;
                item.NationID = nation;
                result.Insert(0, item);
            }

            return result;
        }

        public static List<Gebiet> Regions { get; set; }
        public static List<Gebiet> GetRegions(int aera, string all = null)
        {
            List<Gebiet> result = new List<Gebiet>();

            if (aera > 0)
            {
                result = Regions.OfType<Gebiet>().Where(s => s.AeraID == aera).ToList();
                result = result.OrderBy(x => x.Sortierung).ToList();
            }

            if (!string.IsNullOrEmpty(all))
            {
                Gebiet item = new Gebiet();
                item.ID = 0;
                item.Bezeichnung = all;
                item.NationID = aera;
                result.Insert(0, item);
            }

            return result;
        }

        public static List<KeyValuePair<int, string>> Currencies { private get; set; }

        public static List<string> GetCurrencies(int regionID, string all)
        {
            List<string> result = new List<string>();
            result.Add(all);

            List<KeyValuePair<int, string>> temp = Currencies.OfType<KeyValuePair<int, string>>().Where(s => s.Key == regionID).ToList();
            temp = temp.OrderBy(x => x.Value).ToList();

            foreach (KeyValuePair<int, string> item in temp)
                result.Add(item.Value);

            return result;
        }

        public static List<KeyValuePair<int, string>> Nominale { private get; set; }
        public static List<string> GetNominale(int regionID, string all)
        {
            List<string> result = new List<string>();
            result.Add(all);

            List<KeyValuePair<int, string>> temp = Nominale.OfType<KeyValuePair<int, string>>().Where(s => s.Key == regionID).ToList();
            temp = temp.OrderBy(x => x.Value).ToList();

            foreach (KeyValuePair<int, string> item in temp)
                result.Add(item.Value);

            return result;
        }

        public static List<KeyValuePair<int, string>> Jahre { private get; set; }
        public static List<string> GetJahre(int regionID, string all)
        {
            List<string> result = new List<string>();
            result.Add(all);

            List<KeyValuePair<int, string>> temp = Jahre.OfType<KeyValuePair<int, string>>().Where(s => s.Key == regionID).ToList();
            temp = temp.OrderBy(x => x.Value).ToList();

            foreach (KeyValuePair<int, string> item in temp)
                result.Add(item.Value);

            return result;
        }

        public static BindingList<Katalog3> Muenzkatalog1 { get; set; }
        public static BindingList<Katalog> Muenzkatalog { get; set; }
        public static BindingList<CoinDetail> MünzDetailListe { get; set; }
        public static BindingList<Sammlung> SammlungListe { get; set; }
        public static BindingList<Sammlung> DoublettenListe { get; set; }
        public static List<EigeneBilder> EigeneBilderListe { get; set; }

        public static HauptFilter HauptFilter { get; set; }
        public static Filter Filter { get; set; }
        public static BindingList<Katalog3> MuenzkatalogFiltered
        {
            get
            {
                List<Katalog3> result;
                string alle = Localization.GetTranslation("Keys", "alle");

                if (Filter.Waehrung.Length > 0 && Filter.Waehrung != alle)
                    result = CoinbookHelper.Muenzkatalog1.Where(a => a.Waehrung == Filter.Waehrung).ToList();
                else
                    result = CoinbookHelper.Muenzkatalog1.ToList();

                if (Filter.Nominal.Length > 0 && Filter.Nominal != alle)
                    result = result.Where(a => a.Nominal == Filter.Nominal).ToList();

                if (Filter.Jahrgang.Length > 0 && Filter.Jahrgang != alle)
                    result = result.Where(a => a.Jahrgang == Filter.Jahrgang).ToList();

                return new BindingList<Katalog3>(result);
            }
        }


        //public static void ReadErhaltungsgrade(Database database, string sprache)
        //{
        //    List<Erhaltungsgrad> e = new List<Erhaltungsgrad>();
        //    string cmd = "Select Erhaltungsgrad from tblErhaltungsgrad where Sprache = '" + sprache + "' order by id";
        //    DataTable dt = database.GetDataTable(cmd);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //        e.Add(dt.Rows[i][0].ToString());

        //    Erhaltungsgrade = e;
        //}

        //public static void RepairBestand(OleDBZugriff database)
        //{
        //    string cmd = "Delete * from tblBestand";
        //    database.Execute(cmd);

        //    cmd = "INSERT INTO tblBestand SELECT * FROM ("
        //      + "SELECT Guid, sum(sammlung.s) AS S, sum(sammlung.SP) AS SP, sum(sammlung.SS) AS SS, sum(sammlung.SSP) AS SSP, sum(sammlung.VZ) AS VZ, sum(sammlung.VZP) AS VZP, sum(sammlung.STN) AS STN, "
        //      + "sum(sammlung.STH) AS STH, sum(sammlung.PP) AS PP, sum(sammlung.ds) AS DS, sum(sammlung.dSP) AS DSP, sum(sammlung.dSS) AS DSS, sum(sammlung.dSSP) AS DSSP, sum(sammlung.dVZ) AS DVZ, sum(sammlung.dVZP) AS DVZP, "
        //      + "sum(sammlung.dSTN) AS DSTN, sum(sammlung.dSTH) AS DSTH, sum(sammlung.dPP) AS DPP "
        //      + "FROM ( "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  Count(ID_ErhaltungsGrad) AS S, 0 AS SP, 0 AS SS, 0 AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  "
        //      + "0 AS DS, 0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=1 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, Count(ID_ErhaltungsGrad) AS SP, 0 AS SS, 0 AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP, 0 AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.ID, tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=2 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, Count(ID_ErhaltungsGrad)  AS SS, 0 AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=3 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, Count(ID_ErhaltungsGrad)  AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=4 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, Count(ID_ErhaltungsGrad) AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=5 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT tblKatalog.RepID AS Guid, stblSammlung.ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, Count(stblSammlung.ID_ErhaltungsGrad) AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, stblSammlung.BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.ID, tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=6 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, 0 AS VZP, Count(ID_ErhaltungsGrad) AS STN, 0 AS STH, 0 AS PP,  0 AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=7 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, Count(ID_ErhaltungsGrad)  AS STH, 0 AS PP,  0 AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=8 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0  AS STH, Count(ID_ErhaltungsGrad) AS PP,  0 AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=9 AND stblSammlung.BOOL_Doublette=False "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0 AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  Count(ID_ErhaltungsGrad) AS DS, "
        //      + "0 AS DSP, 0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=1 AND stblSammlung.BOOL_Doublette=true "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0 AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP, 0 AS DS, Count(ID_ErhaltungsGrad) AS DSP, "
        //      + "0 AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=2 AND stblSammlung.BOOL_Doublette=true "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0 AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, 0 AS DSP, "
        //      + "Count(ID_ErhaltungsGrad) AS DSS, 0 AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.ID, tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=3 AND stblSammlung.BOOL_Doublette=true "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0 AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, 0 AS DSP, 0 AS DSS, "
        //      + "Count(ID_ErhaltungsGrad) AS DSSP, 0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=4 AND stblSammlung.BOOL_Doublette=true "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, 0 AS DSP, 0 AS DSS, 0 AS DSSP, "
        //      + "Count(ID_ErhaltungsGrad) AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=5 AND stblSammlung.BOOL_Doublette=true "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, 0 AS DSP, 0 AS DSS, 0 AS DSSP, "
        //      + "0 AS DVZ, Count(ID_ErhaltungsGrad) AS DVZP, 0 AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=6 AND stblSammlung.BOOL_Doublette=true "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, 0 AS DSP, 0 AS DSS, 0 AS DSSP, "
        //      + "0 AS DVZ, 0 AS DVZP, Count(ID_ErhaltungsGrad) AS DSTN, 0 AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=7 AND stblSammlung.BOOL_Doublette=true "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0 AS STH, 0 AS PP,  0 AS DS, 0 AS DSP, 0 AS DSS, 0 AS DSSP, "
        //      + "0 AS DVZ, 0 AS DVZP, 0 AS DSTN, Count(ID_ErhaltungsGrad) AS DSTH, 0 AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=8 AND stblSammlung.BOOL_Doublette=true "

        //      + "union "

        //      + "SELECT RepID AS Guid, ID_ErhaltungsGrad,  0 AS S, 0 AS SP, 0 AS SS, 0  AS SSP, 0 AS VZ, 0 AS VZP, 0 AS STN, 0  AS STH, 0 AS PP,  0 AS DS, 0 AS DSP, 0 AS DSS, 0 AS DSSP, "
        //      + "0 AS DVZ, 0 AS DVZP, 0 AS DSTN, 0 AS DSTH, Count(ID_ErhaltungsGrad) AS dPP, BOOL_Doublette "
        //      + "FROM tblKatalog LEFT JOIN stblSammlung ON tblKatalog.RepID = stblSammlung.ID_Katalog "
        //      + "GROUP BY tblKatalog.RepID, stblSammlung.ID_ErhaltungsGrad, stblSammlung.BOOL_Doublette, tblKatalog.Nation_ID, tblKatalog.Aera_ID, tblKatalog.gebiet_id "
        //      + "HAVING stblSammlung.ID_ErhaltungsGrad=9 AND stblSammlung.BOOL_Doublette=true "

        //      + ")  AS sammlung GROUP BY guid)";

        //    database.Execute(cmd);
        //}

        public static string CPUID
        {
            get
            {
                string cpuid = string.Empty;
                ManagementClass man = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = man.GetInstances();
                foreach (ManagementObject mob in moc)
                {
                    if (cpuid == String.Empty)
                    {
                        // Nimmt vom ersten CPU die ID und bricht dann ab. 
                        cpuid = mob.Properties["processorID"].Value.ToString();
                        break;
                    }
                }
                return cpuid;
            }
        }

        public static void ClearPath(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
                File.Delete(file);
        }

        public static string ConvertFileName(string text)
        {
            return text.Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue").Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("ß", "ss").Replace(" ", "_");
        }

        //public static void Aktivieren()
        //{
        //  string bit = "32";

        //  MailMessage mail = new MailMessage();
        //  mail.From = new MailAddress("san-software@web.de"); //Absender 
        //  //mail.From = new MailAddress(Settings.Mail); //Absender 
        //  //mail.To.Add("san-software@gmx.de"); //Empfänger 
        //  mail.To.Add("Bestellung@Coinbook.de"); //Empfänger 

        //  if (Environment.OSVersion.Platform.ToString().Contains("64"))
        //    bit = "64";

        //  mail.Subject = "Coinbook Aktivierung #" + cpuID + "#" + Settings.Vorname + "#" + Settings.Nachname + "#" + Settings.Mail + "#" + GetWindwosClientVersion + "#" + bit + "#" + Application.ProductVersion;
        //  mail.Body = Settings.Vorname + Environment.NewLine + Settings.Nachname + Environment.NewLine + Settings.Mail + Environment.NewLine + GetWindwosClientVersion + Environment.NewLine + bit + "#" + Application.ProductVersion;

        //  SmtpClient client = new SmtpClient("smtp.web.de", 587); //SMTP Server von Hotmail und Outlook. 

        //  try
        //  {
        //    client.Credentials = new System.Net.NetworkCredential("san-software@web.de", "magixx");//Anmeldedaten für den SMTP Server 
        //    client.EnableSsl = true; //Die meisten Anbieter verlangen eine SSL-Verschlüsselung 
        //    client.Send(mail); //Senden 

        //    Settings.Activated = "angefordert";
        //    Settings.Save();

        //    MessageBoxAdv.Show("Die Aktivierung von Coinbook wurde angefordert." + Environment.NewLine + Environment.NewLine + "Die Aktivierung sollte innerhalb von 2-3 Werktagen erfolgen.");
        //  }
        //  catch (Exception ex)
        //  {
        //    string text = "Die Aktivierung konnte aus folgenden Gründen nicht angefordert werden" + Environment.NewLine + Environment.NewLine
        //      + ex.Message + Environment.NewLine + Environment.NewLine
        //      + "Bitte teilen Sie dem Coinbook-Support dieses Problem und die nachfolgend angezeige Aktivierungsnummer mit." + Environment.NewLine + Environment.NewLine
        //      + "Aktivierungsnummer: " + cpuID + Environment.NewLine + Environment.NewLine
        //      + "Mit Hile dieser Nummer kann die Aktivierung vom Support manuell vorgenommen werden";
        //    MessageBoxAdv.Show(text);
        //  }
        //}

        public static string GetWindwosClientVersion
        {
            get
            {
                string result = "unbekannt";

                int major = System.Environment.OSVersion.Version.Major;
                int minor = System.Environment.OSVersion.Version.Minor;
                int build = System.Environment.OSVersion.Version.Build;


                if (major == 4 && minor == 0 && build == 950)
                    result = "Win95 Release 1";
                else if (major == 4 && minor == 0 && build == 1111)
                    result = "Win95 Release 2";
                else if (major == 4 && minor == 3 && (build == 1212 || build == 1213 || build == 1214))
                    result = "Win95 Release 2.1";
                else if (major == 4 && minor == 10 && build == 1998)
                    result = "Win98";
                else if (major == 4 && minor == 10 && build == 2222)
                    result = "Win98 Second Edition";
                else if (major == 4 && minor == 90)
                    result = "WinMe";
                else if (major == 5 && minor == 0)
                    result = "Win2000";
                else if (major == 5 && minor == 1 && build == 2600)
                    result = "WinXP";
                else if (major == 5 && minor == 2 && build == 2600)
                    result = "WinXP/64";
                else if (major == 6 && minor == 0)
                    result = "Vista";
                else if (major == 6 && minor == 1)
                    result = "Win7";
                else if (major == 6 && minor == 2)
                    result = "Win8";
                else if (major == 6 && minor == 3)
                    result = "Win8.1 Update 1";
                else if (major == 10 && minor == 0)
                    result = "Win10";

                return result;
            }
        }

        public static bool IsNumeric(string text)
        {
            float output;

            text = text.Replace(".", String.Empty).Replace(",", String.Empty);
            return float.TryParse(text, out output);
        }

        public static string Zipfile(string modul, string jahr)
        {
            string zipfile = modul.Replace(" ", "_") + "-" + jahr + ".zip";
            zipfile = zipfile.Replace(" ", "_");
            zipfile = zipfile.Replace("ä", "ae");
            zipfile = zipfile.Replace("Ö", "Oe");

            return zipfile;
        }

        //public static void Savepreise(Nationen database, bool doublette)
        //{
        //    string cmd = String.Empty;
        //    database.OpenTransaction();
        //    database.Execute("Delete * from tmpPreise");

        //    switch (Settings.Preise)
        //    {
        //        case enmPreise.Katalogpreise:          //Preise umschaufeln Katalogpreise
        //            cmd = "INSERT INTO tmpPreise ( ID, Guid, Nation_ID, Aera_ID, Gebiet_ID, Währung, Nominal, Jahrgang, Münzzeichen, "
        //                + "SPreis, SPPreis, SSPreis, SSPPreis, VZPreis, VZPPreis, STNPreis, STHPreis, PPPreis ) "
        //                + "SELECT ID, RepID, Nation_ID, Aera_ID, Gebiet_ID, Währung, Nominal, Jahrgang, Münzzeichen, "
        //                + "Erh_S_Preis, Erh_SP_Preis, Erh_SS_Preis, Erh_SSP_Preis, Erh_VZ_Preis, Erh_VZP_Preis, Erh_STN_Preis, Erh_STH_Preis, Erh_PP_Preis "
        //                + "FROM tblKatalog ";

        //            database.Execute(cmd);
        //            break;

        //        case enmPreise.EigenePreise:           //Preise umschaufeln Eigene Preise
        //            cmd = "INSERT INTO tmpPreise ( ID, Guid, Nation_ID, Aera_ID, Gebiet_ID, Währung, Nominal, Jahrgang, Münzzeichen, "
        //                + "SPreis, SPPreis, SSPreis, SSPPreis, VZPreis, VZPPreis, STNPreis, STHPreis, PPPreis ) "
        //                + "SELECT ID, RepID, Nation_ID, Aera_ID, Gebiet_ID, Währung, Nominal, Jahrgang, Münzzeichen, "
        //                + "Erh_S_Preis, Erh_SP_Preis, Erh_SS_Preis, Erh_SSP_Preis, Erh_VZ_Preis, Erh_VZP_Preis, Erh_STN_Preis, Erh_STH_Preis, Erh_PP_Preis "
        //                + "FROM tblKatalog ";

        //            database.Execute(cmd);

        //            DataTable dt1 = database.GetDataTable("Select * from stblPreise");

        //            for (int i = 0; i < dt1.Rows.Count; i++)
        //            {
        //                DataRow r = dt1.Rows[i];

        //                cmd = String.Empty;
        //                if (ConvertEx.ToDouble0(r["Spreis"]) != 0)
        //                    cmd = cmd + "Spreis=" + r["Spreis"].ToString().Replace(",", ".") + ",";

        //                if (ConvertEx.ToDouble0(r["SPPreis"]) != 0)
        //                    cmd = cmd + "SPPreis=" + r["SPPreis"].ToString().Replace(",", ".") + ",";

        //                if (ConvertEx.ToDouble0(r["SSPreis"]) != 0)
        //                    cmd = cmd + "SSPreis=" + r["SSPreis"].ToString().Replace(",", ".") + ",";

        //                if (ConvertEx.ToDouble0(r["SSPPreis"]) != 0)
        //                    cmd = cmd + "SSPPreis=" + r["SSPPreis"].ToString().Replace(",", ".") + ",";

        //                if (ConvertEx.ToDouble0(r["VZPreis"]) != 0)
        //                    cmd = cmd + "VZPreis=" + r["VZPreis"].ToString().Replace(",", ".") + ",";

        //                if (ConvertEx.ToDouble0(r["VZPPreis"]) != 0)
        //                    cmd = cmd + "VZPPreis=" + r["VZPPreis"].ToString().Replace(",", ".") + ",";

        //                if (ConvertEx.ToDouble0(r["STNPreis"]) != 0)
        //                    cmd = cmd + "STNPreis=" + r["STNPreis"].ToString().Replace(",", ".") + ",";

        //                if (ConvertEx.ToDouble0(r["STHPreis"]) != 0)
        //                    cmd = cmd + "STHPreis=" + r["STHPreis"].ToString().Replace(",", ".") + ",";

        //                if (ConvertEx.ToDouble0(r["PPPreis"]) != 0)
        //                    cmd = cmd + "PPPreis=" + r["PPPreis"].ToString().Replace(",", ".") + ",";

        //                if (cmd != String.Empty)
        //                    cmd = "Update tmpPreise set " + cmd.Substring(0, cmd.Length - 1) + " where Guid='" + dt1.Rows[i]["guid"].ToString() + "'";
        //                database.Execute(cmd);
        //            }
        //            break;

        //        case enmPreise.Kaufpreise:
        //            string[] e = new string[] { String.Empty, "S", "SP", "SS", "SSP", "VZ", "VZP", "STN", "STH", "PP" };

        //            cmd = "INSERT INTO tmpPreise ( ID, Guid, Nation_ID, Aera_ID, Gebiet_ID, Währung, Nominal, Jahrgang, Münzzeichen, "
        //                + "SPreis, SPPreis, SSPreis, SSPPreis, VZPreis, VZPPreis, STNPreis, STHPreis, PPPreis ) "
        //                + "SELECT ID, RepID, Nation_ID, Aera_ID, Gebiet_ID, Währung, Nominal, Jahrgang, Münzzeichen, "
        //                + "0, 0, 0, 0, 0, 0, 0, 0, 0 "
        //                + "FROM tblKatalog ";
        //            database.Execute(cmd);

        //            cmd = "SELECT ID_Katalog, ID_ErhaltungsGrad, Sum(Preis) AS Kaufpreis "
        //                + "FROM stblSammlung WHERE Preis <> 0 and BOOL_Doublette=" + doublette.ToString() + " "
        //                + "GROUP BY ID_Katalog, ID_ErhaltungsGrad";
        //            DataTable dt = database.GetDataTable(cmd);

        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                cmd = "Update tmpPreise set " + e[Convert.ToInt32(dt.Rows[i]["ID_ErhaltungsGrad"])]
        //                    + "Preis=" + dt.Rows[i]["Kaufpreis"].ToString().Replace(",", ".")
        //                    + " where Guid='" + dt.Rows[i]["ID_Katalog"].ToString() + "'";
        //                database.Execute(cmd);
        //            }
        //            break;
        //    }
        //    database.Commit();
        //}



        //public static void CompactDataBase()
        //{
        //    OleDBConnection.CloseAllConnections();

        //    string sourceFile = OleDBConnection.File;

        //    string destinationFile = Path.Combine(Path.GetDirectoryName(sourceFile), Path.GetFileNameWithoutExtension(sourceFile)) + ".tmp";
        //    string source = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourceFile + ";Jet OLEDB:Engine Type=5;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";
        //    string destination = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationFile + ";Jet OLEDB:Engine Type=5;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";

        //    dynamic JROEng = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("JRO.JetEngine"));

        //    try
        //    {
        //        JROEng.CompactDatabase(source, destination);

        //        File.Delete(sourceFile);
        //        File.Copy(destinationFile, sourceFile);
        //        File.Delete(destinationFile);
        //    }
        //    catch (SystemException ex)
        //    {
        //    }

        //    DBConnect result = OleDBConnection.Init;
        //}

        //public static void AktivierungAnfordern(enmAktivierungsArt aktivierungsart, string grund)
        //{

        //	string bit = "32 bit";
        //	if (Environment.OSVersion.Platform.ToString().Contains("64"))
        //		bit = "64 bit";

        //	XmlDocument document = new XmlDocument();
        //	XmlElement root = document.CreateElement("Element", "Root", String.Empty);
        //	XmlNode aktivierung = document.CreateElement(String.Empty, "Aktivierung", String.Empty);

        //	document.AppendChild(root);
        //	root.AppendChild(aktivierung);

        //	XmlAttribute vorname = document.CreateAttribute("Vorname");
        //	vorname.Value = Settings.Vorname;
        //	aktivierung.Attributes.Append(vorname);

        //	XmlAttribute nachname = document.CreateAttribute("Nachname");
        //	nachname.Value = Settings.Nachname;
        //	aktivierung.Attributes.Append(nachname);

        //	XmlAttribute plz = document.CreateAttribute("PLZ");
        //	plz.Value = Settings.PLZ;
        //	aktivierung.Attributes.Append(plz);

        //	XmlAttribute ort = document.CreateAttribute("Ort");
        //	ort.Value = Settings.Ort;
        //	aktivierung.Attributes.Append(ort);

        //	XmlAttribute land = document.CreateAttribute("Land");
        //	land.Value = Settings.Land;
        //	aktivierung.Attributes.Append(land);

        //	XmlAttribute strasse = document.CreateAttribute("Strasse");
        //	strasse.Value = Settings.Strasse;
        //	aktivierung.Attributes.Append(strasse);

        //	XmlAttribute betriebssystem = document.CreateAttribute("System");
        //	betriebssystem.Value = Helper.GetWindwosClientVersion + "/" + bit;
        //	aktivierung.Attributes.Append(betriebssystem);

        //	XmlAttribute email = document.CreateAttribute("Email");
        //	email.Value = Settings.Mail;
        //	aktivierung.Attributes.Append(email);

        //	XmlAttribute datum = document.CreateAttribute("Datum");
        //	datum.Value = DateTime.Now.ToShortDateString();
        //	aktivierung.Attributes.Append(datum);

        //	XmlAttribute lizenzkey = document.CreateAttribute("Lizenzkey");
        //	lizenzkey.Value = Settings.Lizenzkey;
        //	aktivierung.Attributes.Append(lizenzkey);

        //	XmlAttribute bemerkung = document.CreateAttribute("Bemerkung");
        //	bemerkung.Value = grund;
        //	aktivierung.Attributes.Append(bemerkung);

        //	XmlAttribute version = document.CreateAttribute("Version");
        //	version.Value = Application.ProductVersion;
        //	aktivierung.Attributes.Append(version);

        //	XmlAttribute serial = document.CreateAttribute("Serial");
        //	serial.Value = cpuID;
        //	aktivierung.Attributes.Append(serial);

        //	XmlAttribute art = document.CreateAttribute("Aktivierungsart");
        //	art.Value = aktivierungsart.ToString();
        //	aktivierung.Attributes.Append(art);

        //	XmlAttribute vorgang = document.CreateAttribute("Vorgang");
        //	vorgang.Value = string.Format("{0:yyMMdd}", DateTime.Now) + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
        //	aktivierung.Attributes.Append(vorgang);

        //	string file = Path.Combine(Helper.UpdatePath, "Aktivierung-" + Settings.Lizenzkey + ".xml");

        //	document.Save(file);

        //	FTPClass ftp = new FTPClass();
        //	ftp.Connect("www.coinbook.de", "ftp12564714-Transfer", "magixx-1");
        //	ftp.SetWorkingDirectory("Aktivierung");
        //	ftp.Upload(file, Path.GetFileName(file));
        //	ftp.Disconnect();

        //	File.Delete(file);

        //	Settings.Activated = "angefordert";

        //	MessageBoxAdv.Show("Die Aktivierungsanforderung wurde übermittelt");
        //}

        public static Color ColorHeader1
        {
            get
            {
                return Color.LightSkyBlue;
            }
        }

        public static Color ColorHeader2
        {
            get
            {
                return Color.LightGreen;
            }
        }

        public static Color ColorHeader3
        {
            get
            {
                return Color.LightCoral;
            }
        }

        public static Color ColorAlternateEven
        {
            get
            {
                return Color.Gainsboro;
            }
        }

        public static Color ColorAlternateOdd
        {
            get
            {
                return Color.White;
            }
        }

        public static Color ColorHeader
        {
            get
            {
                return Color.Gainsboro;
            }
        }

        public static Color ColorOwnPrices
        {
            get
            {
                return Color.Yellow;
            }
        }

        public static Color ColorSelection
        {
            get
            {
                return Color.LightSkyBlue;
            }
        }

        public static Color ColorGridlines
        {
            get
            {
                return Color.Black;
            }
        }

        public static Color ColorBlocked
        {
            get
            {
                return Color.Silver;
            }
        }

        public static Color ColorText
        {
            get
            {
                return Color.Black;
            }
        }

        public static void InsertIntoMünzkatalog(Sammlung coin, int anzahl)
        {
            var item = MuenzkatalogFiltered.SingleOrDefault(p => p.GUID == coin.Guid);

            string temp = "";

            switch (coin.Erhaltung)
            {
                case 1:
                    temp = item.S;
                    break;
                case 2:
                    temp = item.SP;
                    break;
                case 3:
                    temp = item.SS;
                    break;
                case 4:
                    temp = item.SSP;
                    break;
                case 5:
                    temp = item.VZ;
                    break;
                case 6:
                    temp = item.VZP;
                    break;
                case 7:
                    temp = item.STN;
                    break;
                case 8:
                    temp = item.STH;
                    break;
                case 9:
                    temp = item.PP;
                    break;
            }

            if (string.IsNullOrEmpty(temp))
            {
                if (Settings.SelectedStyle == enmSelectedStyle.SammlungUndDoubletten)
                    temp = "0/0";
                else
                    temp = "0";
            }

            var a = temp.Split(new Char[] { '/' });

            if (string.IsNullOrEmpty(temp))
            {
                if (Settings.SelectedStyle == enmSelectedStyle.SammlungUndDoubletten)
                    temp = "0/0";
                else
                    temp = "0";
            }

            if (!coin.Doublette)
            {
                if (Settings.SelectedStyle == enmSelectedStyle.SammlungUndDoubletten || Settings.SelectedStyle == enmSelectedStyle.Icon)
                    a[0] = (ConvertEx.ToInt32(a[0]) + anzahl).ToString();
            }
            else
            {
                if (Settings.SelectedStyle == enmSelectedStyle.DoublettenOnly)
                    a[0] = (ConvertEx.ToInt32(a[0]) + anzahl).ToString();
                if (Settings.SelectedStyle == enmSelectedStyle.SammlungUndDoubletten)
                    a[1] = (ConvertEx.ToInt32(a[1]) + anzahl).ToString();
            }

            switch (Settings.SelectedStyle)
            {
                case enmSelectedStyle.SammlungUndDoubletten:
                    temp = a[0] + "/" + a[1];
                    break;

                default:
                    temp = a[0];
                    break;
            }

            if (temp == "0/0" || temp == "0")
                temp = "";

            switch (coin.Erhaltung)
            {
                case 1:
                    item.S = temp;
                    break;
                case 2:
                    item.SP = temp;
                    break;
                case 3:
                    item.SS = temp;
                    break;
                case 4:
                    item.SSP = temp;
                    break;
                case 5:
                    item.VZ = temp;
                    break;
                case 6:
                    item.VZP = temp;
                    break;
                case 7:
                    item.STN = temp;
                    break;
                case 8:
                    item.STH = temp;
                    break;
                case 9:
                    item.PP = temp;
                    break;
            }

            Decimal summe = 0;

            if (item.S != string.Empty)
            {
                var b = item.S.Split('/');
                summe += muenzAnzahl(b) * item.SPreis;
            }

            if (item.SP != string.Empty)
            {
                var b = item.SP.Split('/');
                summe += muenzAnzahl(b) * item.SPPreis;
            }

            if (item.SS != string.Empty)
            {
                var b = item.SS.Split('/');
                summe += muenzAnzahl(b) * item.SSPreis;
            }

            if (item.SSP != string.Empty)
            {
                var b = item.SSP.Split('/');
                summe += muenzAnzahl(b) * item.SSPPreis;
            }

            if (item.VZ != string.Empty)
            {
                var b = item.VZ.Split('/');
                summe += muenzAnzahl(b) * item.VZPreis;
            }

            if (item.VZP != string.Empty)
            {
                var b = item.VZP.Split('/');
                summe += muenzAnzahl(b) * item.VZPPreis;
            }

            if (item.STN != string.Empty)
            {
                var b = item.STN.Split('/');
                summe += muenzAnzahl(b) * item.STNPreis;
            }



            item.SummeS = summe != 0 ? string.Format("{0:#,##0.00}", summe* Settings.CurrentFaktor) : string.Empty;

            summe = 0;

            if (item.STH != string.Empty)
            {
                var b = item.STH.Split('/');
                summe += muenzAnzahl(b) * item.STHPreis;
            }

            if (item.PP != string.Empty)
            {
                var b = item.PP.Split('/');
                summe += muenzAnzahl(b) * item.PPPreis;
            }

            item.SummePP = summe != 0 ? string.Format("{0:#,##0.00}", summe* Settings.CurrentFaktor) : string.Empty;
        }

        private static int muenzAnzahl(string[] b)
        {
            int anzahl = ConvertEx.ToInt32(b[0]);
            if (b.Length == 2)
                anzahl += ConvertEx.ToInt32(b[1]);

            return anzahl;
        }

        public static void ImportNation(string file)
        {
            XmlDocument document = new XmlDocument();
            document.Load(file);
            XmlNode root = document.SelectSingleNode("DocumentElement");
            XmlNodeList liste = root.SelectNodes("tblNation");

            DatabaseHelper.LiteDatabase.ClearCollection("tblNation");
            foreach (XmlNode item in liste)
            {
                Nation nation = new Nation();

                nation.ID = ConvertEx.ToInt32(item.SelectSingleNode("ID").InnerText);
                nation.Bezeichnung = item.SelectSingleNode("DE_DE").InnerText.ToString();
                nation.Key = item.SelectSingleNode("Key").InnerText.ToString();

                DatabaseHelper.LiteDatabase.InsertNation(nation);
            }

            File.Delete(file);
        }

        public static BindingList<Sammlung> LoadSammlungsliste(string guid, int index, int typ = -1)
        {
            BindingList<Sammlung> sammlungListe = new BindingList<Sammlung>(DatabaseHelper.LiteDatabase.ReadSammlung(guid, typ));
            for (int i = 0; i < sammlungListe.Count; i++)
            {
                switch (sammlungListe[i].Erhaltung)
                {
                    case 1:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].SPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;

                    case 2:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].SPPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;

                    case 3:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].SSPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;

                    case 4:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].SSPPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;

                    case 5:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].VZPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;

                    case 6:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].VZPPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;

                    case 7:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].STNPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;

                    case 8:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].STHPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;

                    case 9:
                        sammlungListe[i].Katalogpreis = CoinbookHelper.MuenzkatalogFiltered[index].PPPreis * CoinbookHelper.Settings.CurrentFaktor;
                        break;
                }

                sammlungListe[i].Erhaltungsgrad = CoinbookHelper.Erhaltungsgrade[sammlungListe[i].Erhaltung-1].Erhaltung;

            }

            return sammlungListe;
        }



        //public static DataTable GetReportÄras(List<Report> reportListe, int nationID, int aeraID)
        //{
        //    DataTable dt = new DataTable("tblÄra");
        //    dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
        //    dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
        //    dt.Columns.Add(new DataColumn("NationID", typeof(int)));
        //    dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

        //    var aeras = Helper.Aeras;

        //    if (aeraID != 0)
        //    {
        //        var aera = aeras.FirstOrDefault(x => x.ID == aeraID);
        //        var row = dt.NewRow();
        //        row["NationID"] = aera.NationID;
        //        row["Bezeichnung"] = aera.Bezeichnung;
        //        row["AeraID"] = aera.ID;
        //        row["Sortierung"] = aera.Sortierung;
        //        dt.Rows.Add(row);
        //    }
        //    else
        //    {
        //        foreach (var aera in aeras)
        //        {
        //            if (reportListe.FirstOrDefault(x => x.AeraID == aera.ID) != null)
        //            {
        //                var row = dt.NewRow();
        //                row["NationID"] = aera.NationID;
        //                row["Bezeichnung"] = aera.Bezeichnung;
        //                row["AeraID"] = aera.ID;
        //                row["Sortierung"] = aera.Sortierung; dt.Rows.Add(row);
        //            }
        //        }
        //    }

        //    return dt;
        //}

        //public static DataTable GetReportGebiete(List<Report> reportListe, int nationID, int aeraID, int gebietID)
        //{
        //    DataTable dt = new DataTable("tblGebiet");
        //    dt.Columns.Add(new DataColumn("Bezeichnung", typeof(string)));
        //    dt.Columns.Add(new DataColumn("AeraID", typeof(int)));
        //    dt.Columns.Add(new DataColumn("GebietID", typeof(int)));
        //    dt.Columns.Add(new DataColumn("Sortierung", typeof(int)));

        //    var gebiete = Helper.Regions;

        //    if (gebietID != 0)
        //    {
        //        var gebiet = gebiete.FirstOrDefault(x => x.ID == gebietID);
        //        if (reportListe.FirstOrDefault(x => x.NationID == nationID) != null)
        //            dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
        //    }
        //    else
        //    {
        //        if (nationID == 0)
        //            foreach (var gebiet in gebiete)
        //                dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
        //        else if (aeraID == 0)
        //            foreach (var gebiet in gebiete)
        //            {
        //                if (reportListe.FirstOrDefault(x => x.NationID == nationID) != null)
        //                    dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
        //            }
        //        else
        //        {
        //            foreach (var gebiet in gebiete)
        //            {
        //                if (reportListe.FirstOrDefault(x => x.GebietID == gebiet.ID) != null)
        //                    dt.Rows.Add(gebietAddRow(gebiet, dt.NewRow()));
        //            }
        //        }
        //    }

        //    return dt;
        //}

        /// <summary>
        /// Diese Funktion dekomprimiert eine ZIP-Datei.
        /// </summary>
        /// <param name="FileName">Die Datei die dekomprimiert werden soll.</param>
        /// <param name="OutputDir">Das Verzeichnis in dem die Dateien dekomprimiert werden sollen.</param>
        public static void DecompressFile(string FileName, string OutputDir = "", string outputFile = "", string password = "")
        {
            FileStream ZFS = new FileStream(FileName, FileMode.Open);
            ICSharpCode.SharpZipLib.Zip.ZipInputStream ZIN = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(ZFS);

            ICSharpCode.SharpZipLib.Zip.ZipEntry ZipEntry = default(ICSharpCode.SharpZipLib.Zip.ZipEntry);

            byte[] Buffer = new byte[4097];
            int ByteLen = 0;
            FileStream FS = null;

            string InZipDirName = null;
            string InZipFileName = null;
            string TargetFileName = null;

            if (password != string.Empty)
                ZIN.Password = password;

            do
            {
                ZipEntry = ZIN.GetNextEntry();
                if (ZipEntry == null) break;

                InZipDirName = Path.GetDirectoryName(ZipEntry.Name);
                InZipFileName = Path.GetFileName(ZipEntry.Name);

                if (outputFile == InZipFileName || outputFile == string.Empty)
                {
                    if (!Directory.Exists(Path.Combine(OutputDir, InZipDirName)))
                        Directory.CreateDirectory(Path.Combine(CoinbookHelper.DataPath, InZipDirName));

                    if (InZipDirName == String.Empty)
                        TargetFileName = Path.Combine(OutputDir, InZipFileName);
                    else
                        TargetFileName = Path.Combine(Path.Combine(CoinbookHelper.DataPath, InZipDirName), InZipFileName);

                    if (InZipFileName != String.Empty)
                    {
                        FS = new FileStream(TargetFileName, FileMode.Create);
                        do
                        {
                            ByteLen = ZIN.Read(Buffer, 0, Buffer.Length);
                            FS.Write(Buffer, 0, ByteLen);
                        }
                        while (!(ByteLen <= 0));
                        FS.Close();
                    }

                    //break;
                }
            }
            while (true);

            ZIN.Close();
            ZFS.Close();
        }

        public static int GetModulID
        {
            get
            {
                int result = 0;
                string file = Path.Combine(CoinbookHelper.DownloadPath, "modul.xml");
                XmlDocument document = new XmlDocument();

                document.Load(file);

                XmlNode root = document.SelectSingleNode("DocumentElement");
                XmlNode modul = root.SelectSingleNode("Modul");
                result = Convert.ToInt32(modul.SelectSingleNode("id").InnerText);

                File.Delete(file);

                return result;
            }
        }

        public static void Neustart()
        {
            MessageBoxAdv.Show("Coinbook wird neu gestartet", "Coinbook");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Application.ExecutablePath;
            Process.Start(startInfo);
            Process.GetCurrentProcess().Kill();
        }

        public static bool IsActivated {get;set;}
        public static Icon Coin { get; set; }
        public static Icon Hinweis { get; set; }
        public static Icon Lupe { get; set; }
        public static Icon CoinbookIcon { get; set; }
        public static bool Changes { get; set; }
        public static string Abo { get; set; }

        public static void StartProgram(string program, enmPrograms param)
        {
            Process P = new Process();
            P.StartInfo.FileName = program;
            // hier kann z.B. eine Textdatei mit übergeben werden
            P.StartInfo.Arguments = param.ToString();
            P.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            P.StartInfo.CreateNoWindow = false;
            var result = P.Start();
            P.WaitForExit();
        }
    }
    public class JSONDataFormat
    {
        #region Properties

        public string data
        {
            get; set;
        }

        public string parameter
        {
            get; set;
        }

        #endregion Properties
    }

}
