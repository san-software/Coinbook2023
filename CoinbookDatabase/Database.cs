using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Coinbook.Model;
using SAN.Converter;
using System.Data;
using Coinbook.Enumerations;
using System.IO;
using SAN.Control;
using System.Data.OleDb;

namespace Coinbook.Database
{
    public sealed class Database
    {
        private static Database instance = null;
        private static readonly object padlock = new object();
        private int openConnections = 0;
        //public event ProgressChangedExEventHandler ProgressExChanged;

        private Database()
        {
        }

        public static Database Instance
        {
            get
            {
                //lock (padlock)
                //{
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
                //}
            }
        }

        public string DatabaseName { get; set; }
       
        public string ConnectionString
        {
            get
            {
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\ProgramData\\Coinbook\\coinbook.mdb;User ID=admin;Jet OLEDB:Database Password=7d8a431ef18dk;";
               
                return connectionString;
            }
        }

        OleDbConnection connection;
        public OleDbConnection Connection
        {
            get
            {
                if (connection == null)
                    connection = new OleDbConnection(ConnectionString);

                return connection;
            }
        }

        public bool HasChanges { get; private set; }

        public void ClearConnection()
        {
            if (connection != null)
            {
                closeConnetion();
                connection = null;
            }
        }
        public void Execute(string sql, bool dontOpenConnection = false)
        {
                openConnection();

            OleDbCommand myCommand = new OleDbCommand(sql, Connection);
            myCommand.ExecuteNonQuery();

            HasChanges = true;

                closeConnetion();
        }

        public object ExecuteScalar(string sql)
        {
            object result = null;

            openConnection();

            OleDbCommand myCommand = new OleDbCommand(sql, Connection);
            result = myCommand.ExecuteScalar();

            closeConnetion();

            return result;
        }

        /// <summary>
        /// Einfügen eines Datensatzes in eine Tabelle
        /// Wenn die ID = -1 oder String.Empty dann wird eine neue ID ermittelt
        /// </summary>
        /// <param name="item">Einzufügendes Objekt</param>
        /// <returns>die ID des Datensatzes</returns>
        public int Insert(object item, string table = "")
        {
            Type t = item.GetType();

            if (table == string.Empty)
            {
                FieldInfo fieldInfo = t.GetField("Table");
                table = fieldInfo.GetValue(item).ToString();
            }

            PropertyInfo[] props = t.GetProperties();

            string s1 = string.Empty;
            string s2 = string.Empty;
            List<OleDbParameter> parameter = new List<OleDbParameter>();

            foreach (PropertyInfo p in props)
            {
                Attribute att = p.GetCustomAttribute(typeof(IgnoreAttribute));
                if (att != null)
                    continue;

                s1 = string.Concat(s1, "[" +p.Name, "],");
                s2 = string.Concat(s2, "@", p.Name, ",");

                var value = item.GetType().GetProperty(p.Name).GetValue(item, null);
                OleDbParameter fbp = null;

                switch (p.PropertyType.Name)
                {
                    case "Int32":
                        fbp = new OleDbParameter(p.Name, OleDbType.Integer);
                        break;

                   case "String":
                         fbp = new OleDbParameter(p.Name, OleDbType.VarChar);
                        break;

                    case "Boolean":
                        fbp = new OleDbParameter(p.Name, OleDbType.Boolean);
                        break;

                    case "Decimal":
                        fbp = new OleDbParameter(p.Name, OleDbType.Decimal);
                        break;

                    case "DateTime":
                        fbp = new OleDbParameter(p.Name, OleDbType.Date);
                        break;

                    case "enmTexte":
                        fbp = new OleDbParameter(p.Name, OleDbType.VarChar);
                        break;

                    default:
                        break;
                }

                if (fbp != null)
                {
                    fbp.Value = value;
                    parameter.Add(fbp);
                }
            }

            string sql = string.Format(string.Concat("Insert into {0} (", s1.TrimEnd(','), ") values(", s2.TrimEnd(','), ")"), table);

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                //foreach (OleDbParameter p in parameter)
                //{
                //    if (p.DbType.ToString() == "Boolean")
                //    {
                //        OleDbParameter x = new OleDbParameter(p.ParameterName, OleDbType.Boolean);
                //        x.Value = (bool)p.Value == true ? 1 : 0;
                //        command.Parameters.AddWithValue(x);
                //    }
                //    else
                //        command.Parameters.AddWithValue(p);
                //}

                openConnection();
                int result = command.ExecuteNonQuery();

                HasChanges = true;

                closeConnetion();
            }

            //for (int i = 0; i< props.Length; i++)
            //{ 
            //	switch (propsi].PropertyType.Name)
            //	{
            //		case "xxx":
            //			break;

            //		default:

            //	string.Concat(sql,)


            //	foreach (PropertyInfo prp in props)
            //{
            //	string name = prp.Name;
            //	var type = prp.PropertyType.Name;
            //	var x= item.GetType().GetProperty(prp.Name).GetValue(item, null);
            //}

            return 0;
        }


        public object Value(string sql)
        {
            object result = null;

            openConnection();

            OleDbCommand myCommand = new OleDbCommand(sql, Connection);
            result = myCommand.ExecuteScalar();

            closeConnetion();

            return result;
        }

        public string Text(string sql)
        {
            string result = string.Empty;

            openConnection();

            OleDbCommand myCommand = new OleDbCommand(sql, Connection);
            object temp = myCommand.ExecuteScalar();
            if (temp != null)
                result = temp.ToString();

            closeConnetion();

            return result;
        }

        public List<Erhaltungsgrad> ReadErhaltungsgrade(string language)
        {
            List<Erhaltungsgrad> result = new List<Model.Erhaltungsgrad>();

            string sql = "Select * from tblErhaltungsgrad where Sprache = @language order by id";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("language", language);

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Erhaltungsgrad item = new Model.Erhaltungsgrad();

                    item.ErhaltungsgradID = ConvertEx.ToInt32(reader["ID"]);
                    item.Bezeichnung = reader["Bezeichnung"].ToString();
                    item.Erhaltung = reader["Erhaltung"].ToString();
                    item.Land = reader["Land"].ToString();
                    item.Sprache = reader["Sprache"].ToString();

                    result.Add(item);
                }

            }

            closeConnetion();

            return result;
        }

        //public List<Nation> ReadNationen()
        //{
            



        //    List<Nation> result = new List<Nation>();

        //    string sql = "SELECT Id, Bezeichnung FROM tblNation ORDER BY Bezeichnung";

        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        openConnection();
        //        OleDbDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Nation item = new Nation();
        //            item.ID = ConvertEx.ToInt32(reader["ID"]);
        //            item.Bezeichnung = reader["Bezeichnung"].ToString();

        //            result.Add(item);
        //        }

        //        closeConnetion();
        //    }

        //    return result;
        //}

        public DataRow GetDataRow(string sql, Dictionary<string, object> parameters = null)
        {
            openConnection();

            DataTable dt = new DataTable();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                if (parameters != null)
                    foreach (KeyValuePair<string, object> item in parameters)
                        command.Parameters.AddWithValue(item.Key, item.Value);

                using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                {
                    try
                    {
                        da.Fill(dt);
                    }
                    catch (Exception e)
                    {
                        throw new DataException("\nFehler beim Lesen einer Datatable aus der Datenbank\n" + e.Message + "\n" + sql
                            + "\n\n" + e.Message + "\n\n", e.InnerException);
                    }
                }
            }

            closeConnetion();

            if (dt.Rows.Count != 0)
                return dt.Rows[0];
            else
                return null;
        }

        public DataTable GetDataTable(string sql, Dictionary<string, object> parameters = null)
        {
            openConnection();

            DataTable result = new DataTable();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                if (parameters != null)
                    foreach (KeyValuePair<string, object> item in parameters)
                        command.Parameters.AddWithValue(item.Key, item.Value);

                using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                {
                    try
                    {
                        da.Fill(result);
                    }
                    catch (Exception e)
                    {
                        throw new DataException("\nFehler beim Lesen einer Datatable aus der Datenbank\n" + e.Message + "\n" + sql
                            + "\n\n" + e.Message + "\n\n", e.InnerException);
                    }
                }
            }

            closeConnetion();

            return result;
        }

        //public List<Aera> ReadAera()
        //{
        //    List<Aera> result = new List<Aera>();
        //    string sql = string.Empty;

        //    openConnection();

        //    sql = "SELECT distinct * FROM tblAera where NationID=12 ORDER BY Bezeichnung";
        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        OleDbDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Aera item = new Aera();
        //            item.ID = ConvertEx.ToInt32(reader["ID"]);
        //            item.Bezeichnung = reader["Bezeichnung"].ToString();
        //            item.NationID = 12;

        //            result.Add(item);
        //        }
        //    }

        //    sql = "SELECT distinct * FROM tblAera where NationID=31 ORDER BY Bezeichnung";
        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        OleDbDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Aera item = new Aera();
        //            item.ID = ConvertEx.ToInt32(reader["ID"]);
        //            item.Bezeichnung = reader["Bezeichnung"].ToString();
        //            item.NationID = 31;

        //            result.Add(item);
        //        }
        //    }

        //    sql = "SELECT distinct * FROM tblAera where NationID<>31 and NationID<>12 ORDER BY Sortierung";
        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        OleDbDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Aera item = new Aera();
        //            item.ID = ConvertEx.ToInt32(reader["ID"]);
        //            item.Bezeichnung = reader["Bezeichnung"].ToString();
        //            item.NationID = ConvertEx.ToInt32(reader["NationID"]);

        //            result.Add(item);
        //        }
        //    }

        //    closeConnetion();

        //    return result;
        //}

        //public List<Gebiet> ReadRegions()
        //{
        //    List<Gebiet> result = new List<Gebiet>();
        //    string sql = "Select * from tblGebiet order by aeraID, Sortierung";

        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        openConnection();
        //        OleDbDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Gebiet item = new Gebiet();
        //            item.ID = ConvertEx.ToInt32(reader["ID"]);
        //            item.Bezeichnung = reader["Bezeichnung"].ToString();
        //            item.AeraID = ConvertEx.ToInt32(reader["AeraID"]);

        //            result.Add(item);
        //        }

        //        closeConnetion();
        //    }

        //    return result;
        //}

        public List<string> ReadCurrency(long regionID, string all)
        {
            List<string> result = new List<string>();

            string sql = "SELECT DISTINCT Waehrung FROM tblKatalog Where gebietID = @gebiet";

            result.Add(all);

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("gebiet", regionID);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    result.Add(reader["Waehrung"].ToString());

            }

            closeConnetion();

            return result;
        }

        //public List<string> ReadNominal(long regionID, string all)
        //{
        //    List<string> result = new List<string>();

        //    string sql = "SELECT DISTINCT Nominal FROM tblKatalog Where gebietID = @gebiet";

        //    result.Add(all);

        //    openConnection();

        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        command.Parameters.AddWithValue("gebiet", regionID);
        //        OleDbDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //            result.Add(reader["Nominal"].ToString());
        //    }
        //    closeConnetion();

        //    return result;
        //}

        //public List<string> ReadYear(long regionID, string all)
        //{
        //    List<string> result = new List<string>();

        //    string sql = "SELECT DISTINCT Jahrgang FROM tblKatalog Where gebietID = @gebiet";

        //    result.Add(all);

        //    openConnection();

        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        command.Parameters.AddWithValue("gebiet", regionID);
        //        OleDbDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //            result.Add(reader["Jahrgang"].ToString());
        //    }
        //    closeConnetion();

        //    return result;
        //}

        public List<string> ReadOwnPictureFileNames()
        {
            List<string> result = new List<string>();

            string sql = "Select Distinct DateiName from tblEigeneBilder order by DateiName";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    result.Add(reader["DateiName"].ToString());
            }
            closeConnetion();

            return result;
        }

        public EigeneBilder GetOwnPicture(string guid)
        {
            EigeneBilder result = new EigeneBilder();

            string sql = "Select * from tblEigeneBilder where [Guid] = @guid";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.DateiName = reader["DateiName"].ToString();
                    result.Guid  = reader["Guid"].ToString();
                    result.ID = ConvertEx.ToInt32(reader["ID"]);
                    result.ShowPicture = ConvertEx.ToBoolean(reader["ShowPicture"]);
                }
            }
            closeConnetion();

            return result;
        }

        public Preise GetPrices(string guid)
        {
            Preise result = new Preise();

            string sql = "Select * from tblPreise where [Guid] = @guid";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.GUID  = reader["Guid"].ToString();
                    result.ID = ConvertEx.ToInt32(reader["ID"]);
                    result.SPreis = ConvertEx.ToDecimal(reader["Spreis"]);
                    result.SPPreis = ConvertEx.ToDecimal(reader["SPpreis"]);
                    result.SSPreis = ConvertEx.ToDecimal(reader["SSpreis"]);
                    result.SSPPreis = ConvertEx.ToDecimal(reader["SSPpreis"]);
                    result.VZPreis = ConvertEx.ToDecimal(reader["VZpreis"]);
                    result.VZPPreis = ConvertEx.ToDecimal(reader["VZPpreis"]);
                    result.STNPreis = ConvertEx.ToDecimal(reader["STNpreis"]);
                    result.STHPreis = ConvertEx.ToDecimal(reader["STHpreis"]);
                    result.PPPreis = ConvertEx.ToDecimal(reader["PPpreis"]);
                }
            }
            closeConnetion();

            return result;
        }

        public BindingList<Katalog> ReadKatalog(string sql)
        {
            sql = sql.Replace(".GUID", ".[GUID]").Replace(".Guid", ".[Guid]");

            BindingList<Katalog> result = new BindingList<Model.Katalog>();

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Katalog item = new Model.Katalog();

                    item.ID = ConvertEx.ToInt32(reader["ID"]);
                    //item.AehnlicheMotive = reader["AehnlicheMotive"].ToString();
                    item.AeraID = ConvertEx.ToInt32(reader["AeraID"]);
                    item.Auflage = reader["Auflage"].ToString();
                    item.AuflagePP = reader["AuflagePP"].ToString();
                    item.AuflageSTH = reader["AuflageSTH"].ToString();
                    //item.Ausgabeanlass = reader["Ausgabeanlass"].ToString();
                    //item.AusserKurs = reader["AusserKurs"].ToString();
                    //item.Aversbeschreibung = reader["Aversbeschreibung"].ToString();
                    //item.AversEntwurf = reader["AversEntwurf"].ToString();
                    //item.Besonderheit = reader["Besonderheit"].ToString();
                    item.Dicke = ConvertEx.ToDecimal(reader["Dicke"]);
                    item.Durchmesser = ConvertEx.ToDecimal(reader["Durchmesser"]);
                    //item.Form = reader["Form"].ToString();
                    item.GebietID = ConvertEx.ToInt32(reader["GebietID"]);
                    //item.Gepraegt = reader["gepraegt"].ToString();
                    item.Gewicht = ConvertEx.ToDecimal(reader["Gewicht"]);
                    item.GUID = reader["GUID"].ToString();
                    item.InKurs = reader["InKurs"].ToString();
                    item.Jahrgang = reader["Jahrgang"].ToString();
                    item.KatNr = reader["KatNr"].ToString();
                    item.Kommentar = reader["Kommentar"].ToString();
                    item.Legierung = reader["Legierung"].ToString();
                    item.Material = reader["Material"].ToString();
                    item.Motiv = reader["Motiv"].ToString();
                    item.Muenzzeichen = reader["Muenzzeichen"].ToString();
                    item.NationID = ConvertEx.ToInt32(reader["NationID"]);
                    item.Nominal = reader["Nominal"].ToString();
                    //item.Orientation = reader["Orientation"].ToString();
                    item.Picture = reader["Picture"].ToString();
                    item.Rand = reader["Rand"].ToString();
                    item.Referenz = reader["Referenz"].ToString();
                    //item.Reversbeschreibung = reader["Reversbeschreibung"].ToString();
                    //item.ReversEntwurf = reader["ReversEntwurf"].ToString();
                    item.SPPreis = ConvertEx.ToDecimal(reader["SPPreis"]);
                    item.SPreis = ConvertEx.ToDecimal(reader["SPreis"]);
                    item.SSPPreis = ConvertEx.ToDecimal(reader["SSPPreis"]);
                    item.SSPreis = ConvertEx.ToDecimal(reader["SSPreis"]);
                    item.VZPPreis = ConvertEx.ToDecimal(reader["VZPPreis"]);
                    item.VZPreis = ConvertEx.ToDecimal(reader["VZPreis"]);
                    item.STHPreis = ConvertEx.ToDecimal(reader["STHPreis"]);
                    item.STNPreis = ConvertEx.ToDecimal(reader["STNPreis"]);
                    item.PPPreis = ConvertEx.ToDecimal(reader["PPPreis"]);
                    item.Typ = reader["Typ"].ToString();
                    item.Waehrung = reader["Waehrung"].ToString();
                    item.HinweisKZ = reader["HinweisKZ"].ToString();
                    item.SP = reader["SP"].ToString();
                    item.S = reader["S"].ToString();
                    item.SSP = reader["SSP"].ToString();
                    item.SS = reader["SS"].ToString();
                    item.VZP = reader["VZP"].ToString();
                    item.VZ = reader["VZ"].ToString();
                    item.STH = reader["STH"].ToString();
                    item.STN = reader["STN"].ToString();
                    item.PP = reader["PP"].ToString();
                    item.SummePP = ConvertEx.ToDecimal(reader["SummePP"]) != 0 ? string.Format("{0:###,##0.00}", ConvertEx.ToDecimal(reader["SummePP"])) : string.Empty;
                    item.SummeS = ConvertEx.ToDecimal(reader["SummeS"]) != 0 ? string.Format("{0:###,##0.00}", ConvertEx.ToDecimal(reader["SummeS"])) : string.Empty;
                    item.Farbe = reader["Farbe"].ToString();
                    item.OriginalKatNr = reader["OriginalKatNr"].ToString();

                    result.Add(item);
                }
            }
            closeConnetion();

            return result;
        }

        //public List<MünzDetail> ReadMünzDetail(string guid, string sprache, enmPreise preistyp)
        //{
        //    List<MünzDetail> result = new List<MünzDetail>();

        //    String sql = "SELECT ID, Erhaltung FROM tblErhaltungsgrad where Sprache=@sprache";

        //    openConnection();

        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        command.Parameters.AddWithValue("sprache", sprache);

        //        OleDbDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            MünzDetail item = new MünzDetail();

        //            item.ID = ConvertEx.ToInt32(reader["ID"]);
        //            item.Erhaltungsgrad = reader["Erhaltung"].ToString();
        //            item.KatalogPreis = 0;
        //            item.Sammlung = 0;
        //            item.Doubletten = 0;
        //            item.SammlungGesamt = 0;
        //            item.DoublettenGesamt = 0;
        //            item.Kaufpreis = 0;
        //            item.Farbe = 0;
        //            item.Liebhaberpreis = false;

        //            result.Add(item);
        //        }
        //    }

        //    sql = "Select id, sum(a.Sammlung) as Sammlung, sum(a.Doublette) as Doublette, Sum (a.KaufPreis) as Kaufpreis from("
        //        + "SELECT tblErhaltungsgrad.ID, Count(tblSammlung.id) AS Sammlung, 0 AS Doublette, Sum(tblSammlung.Kaufpreis) AS Kaufpreis, [Guid] "
        //        + "FROM tblErhaltungsgrad INNER JOIN tblSammlung ON tblErhaltungsgrad.ID = tblSammlung.Erhaltung "
        //        + "WHERE tblSammlung.Doublette=False and tblErhaltungsgrad.Sprache='de' "
        //        + "GROUP BY tblErhaltungsgrad.ID, tblSammlung.[GUID] "
        //        + "HAVING tblSammlung.[GUID]=@guid "
        //        + "union "
        //        + "SELECT tblErhaltungsgrad.ID, 0 AS Sammlung, Count(tblSammlung.id) AS Doublette, Sum(tblSammlung.Kaufpreis) AS Kaufpreis, [Guid] "
        //        + "FROM tblErhaltungsgrad INNER JOIN tblSammlung ON tblErhaltungsgrad.ID = tblSammlung.Erhaltung "
        //        + "WHERE tblSammlung.Doublette=true and tblErhaltungsgrad.Sprache='de' "
        //        + "GROUP BY tblErhaltungsgrad.ID, tblSammlung.[GUID] "
        //        + "HAVING tblSammlung.[GUID]=@guid"
        //        + ") as a GROUP BY ID, [Guid] ";

        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        command.Parameters.AddWithValue("guid", guid);

        //        OleDbDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            int id = ConvertEx.ToInt32(reader["ID"]) - 1;
        //            result[id].Doubletten = ConvertEx.ToInt32(reader["Doublette"]);
        //            result[id].Kaufpreis = ConvertEx.ToDecimal(reader["Kaufpreis"]);
        //            result[id].Sammlung = ConvertEx.ToInt32(reader["Sammlung"]);
        //        }
        //    }

        //    sql = sqlKatalogpreise(guid, preistyp);


        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        command.Parameters.AddWithValue("guid", guid);

        //        OleDbDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            int id = ConvertEx.ToInt32(reader["ID"]) - 1;
        //            result[id].KatalogPreis = ConvertEx.ToDecimal(reader["Katalogpreis"]);
        //            result[id].DoublettenGesamt = result[id].Doubletten * result[id].KatalogPreis;
        //            result[id].SammlungGesamt = result[id].Sammlung * result[id].KatalogPreis;
        //            result[id].Farbe = ConvertEx.ToInt32(reader["Farbe"]);
        //            result[id].Liebhaberpreis = ConvertEx.ToBoolean(reader["Liebhaberpreis"]);
        //            result[id].LiebhaberpreisStand = reader["LiebhaberpreisStand"].ToString();
        //        }
        //    }

        //    closeConnetion();

        //    return result;
        //}

        public BindingList<Sammlung> ReadSammlung(string guid, string sprache, enmPreise preistyp, bool doubletten)
        {
            BindingList<Sammlung> result = new BindingList<Sammlung>();

            String sql = sqlSammlung(preistyp);

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);
                command.Parameters.AddWithValue("doublette", doubletten);
                command.Parameters.AddWithValue("sprache", sprache);

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sammlung item = new Sammlung();

                    //item.ID = ConvertEx.ToInt32(reader["ID"]);
                    item.Erhaltungsgrad = reader["Erhaltung"].ToString();
                    item.Katalogpreis = ConvertEx.ToDecimal(reader["Katalogpreis"]);
                    item.Ablage = reader["Ablage"].ToString();
                    item.Kaufdatum = reader["Kaufdatum"].ToString();
                    item.Kaufort = reader["Kaufort"].ToString();
                    item.Kommentar = reader["Kommentar"].ToString();
                    item.Kaufpreis = ConvertEx.ToDecimal(reader["KaufPreis"]);
                    item.Verkaeufer = reader["Verkaeufer"].ToString();
                    item.KatNr = reader["KatNr"].ToString();
                    item.FehlerText = reader["FehlerText"].ToString();
                    item.Farbe = ConvertEx.ToInt32(reader["Farbe"]);
                    item.Doublette = ConvertEx.ToBoolean(reader["Doublette"]);
                    item.Fehlerhaft = ConvertEx.ToBoolean(reader["Fehlerhaft"]);
                    item.Erhaltung = ConvertEx.ToInt32(reader["IDErhaltungsgrad"]);
                    item.Guid = reader["Guid"].ToString();

                    result.Add(item);
                }
            }
            closeConnetion();

            return result;
        }

        public List<Settings2> ReadModulLizenzen()
        {
            List<Settings2> result = new List<Settings2>();

            string sql = "SELECT tblNation.Bezeichnung, tblSettings2.Jahr as Modul FROM tblSettings2 "
                + "INNER JOIN tblNation ON tblSettings2.Lizenz = tblNation.ID where tblSettings2.Jahr <>'' and tblSettings2.Jahr is not null";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Settings2 item = new Settings2();

                    item.Lizenz = reader["Bezeichnung"].ToString();
                    item.Jahr = reader["Modul"].ToString();

                    result.Add(item);
                }
            }
            closeConnetion();

            return result;
        }

        public BindingList<Auktion> ReadAuktionen(string guid)
        {
            BindingList<Auktion> result = new BindingList<Auktion>();

            String sql = "Select * from tblAuktionen where [Guid] =@guid";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Auktion item = new Auktion();

                    item.ID = ConvertEx.ToInt32(reader["ID"]);
                    item.Guid  = reader["Guid"].ToString();
                    item.Erhaltungsgrad = ConvertEx.ToInt32(reader["Erhaltungsgrad"]);
                    item.Datum = reader["Datum"].ToString();
                    item.Preis = ConvertEx.ToDecimal(reader["Preis"]);
                    item.Auktionator = reader["Auktionator"].ToString();
                    item.Auktionshaus = reader["Auktionshaus"].ToString();
                    item.Preis = ConvertEx.ToDecimal(reader["Preis"]);

                    result.Add(item);
                }
            }
            closeConnetion();

            return result;
        }

        public List<KeyValuePair<string, string>> ReadCountries()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            String sql = "SELECT distinct Sprache, Land FROM tblErhaltungsgrad";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    result.Add(new KeyValuePair<string, string>(reader["Sprache"].ToString(), reader["Land"].ToString()));
            }
            closeConnetion();

            return result;
        }

        public List<Currency> ReadCurrencies()
        {
            List<Currency> result = new List<Currency>();

            String sql = "Select * from tblCulture order by ID";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Currency item = new Currency();

                    item.ID = ConvertEx.ToInt32(reader["ID"]);
                    item.Bezeichnung = reader["Bezeichnung"].ToString();
                    item.Kultur = reader["Kultur"].ToString();
                    item.Waehrung = reader["Waehrung"].ToString();
                    item.Faktor = ConvertEx.ToDecimal(reader["Faktor"]);

                    result.Add(item);
                }
            }
            closeConnetion();

            return result;
        }

        public BindingList<EigenerPreis> ReadEigenePreise(string guid, string language)
        {
            BindingList<EigenerPreis> result = new BindingList<EigenerPreis>();

            string sql = "Select * from tblErhaltungsgrad where Sprache = @language order by id";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("language", language);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EigenerPreis item = new EigenerPreis();

                    item.Erhaltung = reader["Erhaltung"].ToString();
                    item.Preis = 0;
                    //item.ID_Kat = guid;

                    result.Add(item);
                }
            }

            sql = "SELECT * FROM tblPreise WHERE [Guid] =@guid";

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //result0].ID = ConvertEx.ToInt32(reader["ID"]);
                    result[0].Preis = ConvertEx.ToDecimal(reader["SPreis"]);
                    result[1].Preis = ConvertEx.ToDecimal(reader["SPPreis"]);
                    result[2].Preis = ConvertEx.ToDecimal(reader["SSPreis"]);
                    result[3].Preis = ConvertEx.ToDecimal(reader["SSPPreis"]);
                    result[4].Preis = ConvertEx.ToDecimal(reader["VZPreis"]);
                    result[5].Preis = ConvertEx.ToDecimal(reader["VZPPreis"]);
                    result[6].Preis = ConvertEx.ToDecimal(reader["STNPreis"]);
                    result[7].Preis = ConvertEx.ToDecimal(reader["STHPreis"]);
                    result[8].Preis = ConvertEx.ToDecimal(reader["PPPreis"]);
                }
            }
            closeConnetion();

            return result;
        }

        public string sqlSammlung(enmPreise preistyp)
        {
            string cmd = String.Empty;

            for (int i = 1; i < 10; i++)
            {
                if (i > 1)
                    cmd = cmd + "union ";

                cmd = cmd + "SELECT tblSammlung.ID, tblErhaltungsgrad.Erhaltung, tblSammlung.Ablage, tblSammlung.Kaufdatum, tblSammlung.Kaufort, tblSammlung.Verkaeufer, tblSammlung.KaufPreis, tblSammlung.Fehlertext, ";

                switch (i)
                {
                    case 1:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.SPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.SPreis is null or tblPreise.SPreis=0,tblKatalog.SPreis,tblPreise.SPreis) as Katalogpreis, iif(tblPreise.SPreis is null or tblPreise.SPreis=0,0,1) as Farbe, ";

                        break;

                    case 2:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.SPPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.SPPreis is null or tblPreise.SPPreis=0,tblKatalog.SPPreis,tblPreise.SPPreis) as Katalogpreis, iif(tblPreise.SPPreis is null or tblPreise.SPPreis=0,0,1) as Farbe, ";
                        break;

                    case 3:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.SSPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.SSPreis is null or tblPreise.SSPreis=0,tblKatalog.SSPreis,tblPreise.SSPreis) as Katalogpreis, iif(tblPreise.SSPreis is null or tblPreise.SSPreis=0,0,1) as Farbe, ";
                        break;

                    case 4:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.SSPPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.SSPPreis is null or tblPreise.SSPPreis=0,tblKatalog.SSPPreis,tblPreise.SSPPreis) as Katalogpreis, iif(tblPreise.SSPPreis is null or tblPreise.SSPPreis=0,0,1) as Farbe, ";
                        break;

                    case 5:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.VZPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.VZPreis is null or tblPreise.VZPreis=0 ,tblKatalog.VZPreis,tblPreise.VZPreis) as Katalogpreis, iif(tblPreise.VZPreis is null or tblPreise.VZPreis=0 ,0,1) as Farbe, ";
                        break;

                    case 6:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.VZPPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.VZPPreis is null or tblPreise.VZPPreis=0,tblKatalog.VZPPreis,tblPreise.VZPPreis) as Katalogpreis, iif(tblPreise.VZPPreis is null or tblPreise.VZPPreis=0,0,1) as Farbe, ";
                        break;

                    case 7:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.STNPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.STNPreis is null or tblPreise.STNPreis=0,tblKatalog.STNPreis,tblPreise.STNPreis) as Katalogpreis, iif(tblPreise.STNPreis is null or tblPreise.STNPreis=0,0,1) as Farbe, ";
                        break;

                    case 8:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.STHPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.STHPreis is null or tblPreise.STHPreis=0,tblKatalog.STHPreis,tblPreise.STHPreis) as Katalogpreis, iif(tblPreise.STHPreis is null or tblPreise.STHPreis=0,0,1) as Farbe, ";
                        break;

                    case 9:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.PPPreis as Katalogpreis, 0 as Farbe, ";
                        else
                            cmd = cmd + "iif(tblPreise.PPPreis is null or tblPreise.PPPreis=0,tblKatalog.PPPreis,tblPreise.PPPreis) as Katalogpreis, iif(tblPreise.PPPreis is null or tblPreise.PPPreis=0,0,1) as Farbe, ";
                        break;
                }

                cmd = cmd + "tblSammlung.Kaufpreis, tblEigeneKatNr.KatNr, tblSammlung.Kommentar, tblErhaltungsgrad.ID as IDErhaltungsgrad, tblSammlung.Doublette, tblSammlung.Fehlerhaft "
                    + "FROM (((tblSammlung LEFT JOIN tblKatalog ON tblSammlung.[Guid] = tblKatalog.[Guid]) "
                    + "LEFT JOIN tblPreise ON tblSammlung.[Guid] = tblPreise.[GUID]) "
                    + "Left Join tblEigeneKatNr on tblKatalog.KatNr = tblEigeneKatNr.Coinbook) "
                    + "INNER JOIN tblErhaltungsgrad ON tblSammlung.Erhaltung = tblErhaltungsgrad.ID "
                    + "where tblSammlung.[Guid] = @guid and Doublette = @doublette and tblErhaltungsgrad.ID=" + i.ToString() + " and tblErhaltungsgrad.Sprache=@sprache ";
            }

            return cmd;
        }

        private string sqlKatalogpreise(string guid, enmPreise preistyp)
        {
            string cmd = String.Empty;

            for (int i = 1; i < 10; i++)
            {
                if (i > 1)
                    cmd = cmd + "union ";

                cmd = cmd + "SELECT Distinct tblKatalog.[Guid], ";

                switch (i)
                {
                    case 1:
                        switch (preistyp)
                        {
                            case enmPreise.Katalogpreise:
                                cmd = cmd + "tblKatalog.SPreis as Katalogpreis, 0 as Farbe, LPS as Liebhaberpreis, LPStandS as LiebhaberpreisStand, ";
                                break;

                            case enmPreise.EigenePreise:
                                cmd = cmd + "iif(tblPreise.SPreis is null or tblPreise.SPreis=0,tblKatalog.SPreis,tblPreise.SPreis) as Katalogpreis, "
                                    + "iif(tblPreise.SPreis is null or tblPreise.SPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                                break;

                            case enmPreise.Kaufpreise:
                                cmd = cmd + "iif(tblPreise.SPreis is null or tblPreise.SPreis=0,tblKatalog.SPreis,tblPreise.SPreis) as Katalogpreis, "
                                    + "iif(tblPreise.SPreis is null or tblPreise.SPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                                break;
                        }
                        break;

                    case 2:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.SPPreis as Katalogpreis, 0 as Farbe, LPSP as Liebhaberpreis, LPStandSP as LiebhaberpreisStand, ";
                        else
                            cmd = cmd + "iif(tblPreise.SPPreis is null or tblPreise.SPPreis=0,tblKatalog.SPPreis,tblPreise.SPPreis) as Katalogpreis, "
                                + "iif(tblPreise.SPPreis is null or tblPreise.SPPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                        break;

                    case 3:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.SSPreis as Katalogpreis, 0 as Farbe, LPSS as Liebhaberpreis, LPStandSS as LiebhaberpreisStand, ";
                        else
                            cmd = cmd + "iif(tblPreise.SSPreis is null or tblPreise.SSPreis=0,tblKatalog.SSPreis,tblPreise.SSPreis) as Katalogpreis, "
                                + "iif(tblPreise.SSPreis is null or tblPreise.SSPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                        break;

                    case 4:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.SSPPreis as Katalogpreis, 0 as Farbe, LPSSP as Liebhaberpreis, LPStandSSP as LiebhaberpreisStand, ";
                        else
                            cmd = cmd + "iif(tblPreise.SSPPreis is null or tblPreise.SSPPreis=0,tblKatalog.SSPPreis,tblPreise.SSPPreis) as Katalogpreis, "
                                + "iif(tblPreise.SSPPreis is null or tblPreise.SSPPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                        break;

                    case 5:
                        if (preistyp == enmPreise.Katalogpreise) 
                            cmd = cmd + "tblKatalog.VZPreis as Katalogpreis, 0 as Farbe, LPVZ as Liebhaberpreis, LPStandVZ as LiebhaberpreisStand, ";
                        else
                            cmd = cmd + "iif(tblPreise.VZPreis is null or tblPreise.VZPreis=0 ,tblKatalog.VZPreis,tblPreise.VZPreis) as Katalogpreis, "
                                + "iif(tblPreise.VZPreis is null or tblPreise.VZPreis=0 ,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                        break;

                    case 6:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.VZPPreis as Katalogpreis, 0 as Farbe, LPVZP as Liebhaberpreis, LPStandVZP as LiebhaberpreisStand, ";
                        else
                            cmd = cmd + "iif(tblPreise.VZPPreis is null or tblPreise.VZPPreis=0,tblKatalog.VZPPreis,tblPreise.VZPPreis) as Katalogpreis, "
                                + "iif(tblPreise.VZPPreis is null or tblPreise.VZPPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                        break;

                    case 7:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.STNPreis as Katalogpreis, 0 as Farbe, LPSTN as Liebhaberpreis, LPStandSTN as LiebhaberpreisStand, ";
                        else
                            cmd = cmd + "iif(tblPreise.STNPreis is null or tblPreise.STNPreis=0,tblKatalog.STNPreis,tblPreise.STNPreis) as Katalogpreis, "
                                + "iif(tblPreise.STNPreis is null or tblPreise.STNPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                        break;

                    case 8:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.STHPreis as Katalogpreis, 0 as Farbe, LPSTH as Liebhaberpreis, LPStandSTH as LiebhaberpreisStand, ";
                        else
                            cmd = cmd + "iif(tblPreise.STHPreis is null or tblPreise.STHPreis=0,tblKatalog.STHPreis,tblPreise.STHPreis) as Katalogpreis, "
                                + "iif(tblPreise.STHPreis is null or tblPreise.STHPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                        break;

                    case 9:
                        if (preistyp == enmPreise.Katalogpreise)
                            cmd = cmd + "tblKatalog.PPPreis as Katalogpreis, 0 as Farbe, LPPP as Liebhaberpreis, LPStandPP as LiebhaberpreisStand, ";
                        else
                            cmd = cmd + "iif(tblPreise.PPPreis is null or tblPreise.PPPreis=0,tblKatalog.PPPreis,tblPreise.PPPreis) as Katalogpreis, "
                                + "iif(tblPreise.PPPreis is null or tblPreise.PPPreis=0,0,1) as Farbe, false as Liebhaberpreis, '' as LiebhaberpreisStand, ";
                        break;
                }

                cmd = cmd + i.ToString() + " as ID FROM tblKatalog Left JOIN tblPreise ON tblKatalog.[Guid] = tblPreise.[GUID] WHERE tblKatalog.[Guid]=@guid ";
            }

            return cmd;
        }



        public int Count(string table, string field)
        {
            int result = 0;

            string sql = string.Format("Select Count({0}) from {1}", field, table);

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                openConnection();

                object temp = command.ExecuteScalar();
                if (temp != null)
                    result = ConvertEx.ToInt32(temp);

                closeConnetion();
            }

            return result;
        }

        public List<ModulStatus> ReadModulStatus(string noLicense)
        {
            string sql = "SELECT tblNation.ID, tblNation.Bezeichnung AS Nation, IIf(tblSettings2.Jahr is null ,false,true) as InUse, "
                + "IIf(tblSettings2.Jahr is null,'" + noLicense + "',tblSettings2.Jahr) AS Jahr "
                + "FROM tblNation LEFT JOIN tblSettings2 ON tblNation.ID = tblSettings2.Lizenz ORDER BY tblNation.Bezeichnung";

            List<ModulStatus> result = new List<ModulStatus>();

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ModulStatus item = new ModulStatus();

                    item.ID = ConvertEx.ToInt32(reader["ID"]);
                    item.Jahr = reader["Jahr"].ToString();
                    item.InUse = ConvertEx.ToBoolean(reader["InUse"]);
                    item.Nation = reader["Nation"].ToString();

                    result.Add(item);
                }
            }
            closeConnetion();

            return result;
        }

        public List<Praegeanstalt> ReadPraegestellen(int nation)
        {
            string sql = "Select Distinct Land, Muenzzeichen, Zeit, Ort,Adresse, Email, Homepage, Bemerkung "
                + "from tblPraegeanstalt where Nation =@nation order by Land, Muenzzeichen";

            List<Praegeanstalt> result = new List<Praegeanstalt>();

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("nation", nation);

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Praegeanstalt item = new Praegeanstalt();

                    item.Muenzzeichen = reader["Muenzzeichen"].ToString();
                    item.Zeit = reader["Zeit"].ToString();
                    item.Ort = reader["Ort"].ToString();
                    item.Adresse = reader["Adresse"].ToString();
                    item.Homepage = reader["Homepage"].ToString();
                    item.Bemerkung = reader["Bemerkung"].ToString();
                    item.Land = reader["Land"].ToString();
                    item.Email = reader["Email"].ToString();

                    result.Add(item);
                }
            }
            closeConnetion();

            return result;
        }

        public EigeneBilder SaveOwnPicture(EigeneBilder ownPicture)
        {
            if (ownPicture.Guid  != String.Empty)
                Update(ownPicture, "[guid] =" + ownPicture.Guid );
            else
            {
                ownPicture.ID = GetSequence("tblEigeneBilder", "id");
                Insert(ownPicture);
            }

            return ownPicture;
        }

        public void SaveAuktionen(BindingList<Auktion> auktionen)
        {
            foreach (Auktion item in auktionen)
            {
                if (item.ID != -1)
                    Update(item, "id=" + item.ID.ToString());
                else
                {
                    item.ID = GetSequence("tblAuktionen", "id");
                    Insert(item);
                }
            }
        }

        public void SaveOwnPrices(string guid, BindingList<EigenerPreis> preise)
        {
            Preise p = new Preise();

            p.GUID  = guid;

            p.SPreis = preise[0].Preis;
            p.SPPreis = preise[1].Preis;
            p.SSPreis = preise[2].Preis;
            p.SSPPreis = preise[3].Preis;
            p.VZPreis = preise[4].Preis;
            p.VZPPreis = preise[5].Preis;
            p.STNPreis = preise[6].Preis;
            p.STHPreis = preise[7].Preis;
            p.PPPreis = preise[8].Preis;

            Preise temp = GetPrices(guid);
            if (temp.ID != 0)
            {
                p.ID = temp.ID;
                Update(p, "id=" + p.ID.ToString());
            }
            else
            {
                p.ID = GetSequence("tblPreise", "id");
                Insert(p);
            }
        }

        public int GetSequence(string table, string field)
        {
            int result = 0;

            string sql = string.Format("Select max({0}) from {1}", field, table);

            object temp = ExecuteScalar(sql);
            if (temp != DBNull.Value)
                result = (int)temp;

            result++;

            return result;
        }

        /// <summary>
        /// Einfügen eines Datensatzes in eine Tabelle
        /// Wenn die ID = -1 oder String.Empty dann wird eine neue ID ermittelt
        /// </summary>
        /// <param name="item">Einzufügendes Objekt</param>
        /// <returns>die ID des Datensatzes</returns>
        public int Update(object item, string where, string table ="")
        {
            int result = -1;
            List<OleDbParameter> parameter = new List<OleDbParameter>();

            Type t = item.GetType();

            if (table == string.Empty)
            {
                FieldInfo fieldInfo = t.GetField("Table");
                table = fieldInfo.GetValue(item).ToString();
            }

            PropertyInfo[] props = t.GetProperties();

            string sql = "Update {0} set ";
            foreach (PropertyInfo p in props)
            {
                Attribute att = p.GetCustomAttribute(typeof(IgnoreAttribute));
                if (att != null)
                    continue;

                att = p.GetCustomAttribute(typeof(IgnoreID));
                if (att != null)
                    continue;

                sql = string.Concat(sql, "[" +p.Name, "]=@", p.Name, ",");

                var value = item.GetType().GetProperty(p.Name).GetValue(item, null);
                OleDbParameter fbp = null;

                switch (p.PropertyType.Name)
                {
                    case "Int32":
                        fbp = new OleDbParameter(p.Name, OleDbType.Integer);
                        break;

                    case "String":
                        fbp = new OleDbParameter(p.Name, OleDbType.VarChar);
                        break;

                    case "Boolean":
                        fbp = new OleDbParameter(p.Name, OleDbType.Boolean);
                        break;

                    case "Decimal":
                        fbp = new OleDbParameter(p.Name, OleDbType.Decimal);
                        break;

                    case "DateTime":
                        fbp = new OleDbParameter(p.Name, OleDbType.Date);
                        break;

                    default:
                        break;
                }

                if (fbp != null)
                {
                  
                        fbp.Value = value;

                    parameter.Add(fbp);
                }
            }

            sql = string.Format(sql, table).TrimEnd(',');
            if (where != string.Empty)
                sql = sql + " where " + where;

            openConnection();
            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                foreach (OleDbParameter pa in parameter)
                {
                    if (pa.OleDbType.ToString() == "Boolean")
                    {
                        OleDbParameter x = new OleDbParameter(pa.ParameterName, OleDbType.Boolean);
                        x.Value = (bool)pa.Value == true ? 1 : 0;
                        command.Parameters.Add(x);
                    }
                    else
                        command.Parameters.Add(pa);
                }

                result = command.ExecuteNonQuery();

                HasChanges = true;

            }
            closeConnetion();

            return result;
        }

        public Sammlung ReadSammlungsmünze(string guid, int erhaltungsgrad)
        {
            Sammlung result = new Sammlung();

            String sql = "Select * from tblSammlung where [Guid] = @guid and erhaltung=@erhaltungsgrad";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);
                command.Parameters.AddWithValue("erhaltungsgrad", erhaltungsgrad);

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //result.ID = ConvertEx.ToInt32(reader["ID"]);
                    result.Erhaltung = ConvertEx.ToInt32(reader["Erhaltung"]);
                    //result.Katalogpreis = ConvertEx.ToDecimal(reader["Katalogpreis"]);
                    result.Ablage = reader["Ablage"].ToString();
                    result.Kaufdatum = reader["Kaufdatum"].ToString();
                    result.Kaufort = reader["Kaufort"].ToString();
                    result.Kommentar = reader["Kommentar"].ToString();
                    result.Kaufpreis = ConvertEx.ToDecimal(reader["Kaufpreis"]);
                    result.Verkaeufer = reader["Verkaeufer"].ToString();
                    result.Guid  = reader["Guid"].ToString();
                    //result.Farbe = ConvertEx.ToInt32(reader["Farbe"]);
                }
            }

            closeConnetion();

            return result;
        }

        public double GetPreis(string guid, int erhaltungsgrad, enmPreise preistyp)
        {
            object value;
            string table = (preistyp == enmPreise.EigenePreise ? "tblPreise" : "tblKatalog");

            String sql = sqlPreis(guid, erhaltungsgrad, table);
            double result = double.NaN;

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);
                value = command.ExecuteScalar();
            }

            table = "tblKatalog";
            sql = sqlPreis(guid, erhaltungsgrad, table);

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);
                value = command.ExecuteScalar();
            }

            if (value != null)
                result = ConvertEx.ToDouble(value);

            closeConnetion();

            return result;
        }

        private string sqlPreis(string guid, int erhaltungsgrad, string table)
        {
            if (erhaltungsgrad < 1)
                erhaltungsgrad = 0;

            string feld = "SPreis,SPreis,SPPreis,SSPreis,SSPPreis,VZPreis,VZPPreis,STNPreis,STHPreis,PPPreis".Split(',')[erhaltungsgrad];

            return string.Format("Select {0} from {1} where [Guid] = @guid", feld, table);
        }

        public void SaveSammlungsmuenze(Sammlung sammlungsmünze)
        {
            //if (sammlungsmünze.ID != -1)                                                  TODO
            //    Update(sammlungsmünze, "[guid] = '" + sammlungsmünze.Guid  + "'");
            //else
            //{
            //    sammlungsmünze.ID = GetSequence("tblSammlung", "id");
            //    Insert(sammlungsmünze);
            //}
        }

        public void SaveCurrencies(List<Currency> currencies)
        {
            foreach (Currency item in currencies)
                Update(item, "id ='" + item.ID.ToString() + "'");
        }

        public void AddBestand(Sammlung coin, int anzahl)
        {
            object value;
            int v = 0;
            string feld = "S,S,SP,SS,SSP,VZ,VZP,STN,STH,PP".Split(',')[coin.Erhaltung];

            if (coin.Doublette)
                feld = "D" + feld;

            string sql = string.Format("Select {0} from tblBestand where [Guid] = @guid", feld);

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", coin.Guid);
                value = command.ExecuteScalar();
            }

            closeConnetion();

            if (value == null)
            {
                Bestand bestand = new Bestand();
                bestand.Guid = coin.Guid;
                Insert(bestand);
                v = anzahl;
            }
            else
                v = (int)value + anzahl;

            sql = string.Format("Update tblBestand set {0} = @anzahl where [Guid] = @guid", feld);

            openConnection();
            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", coin.Guid);
                command.Parameters.AddWithValue("anzahl", v);
                command.ExecuteNonQuery();
                HasChanges = true;
            }

            closeConnetion();

        }

        public void DeleteCoin(Sammlung coin)
        {
            object value;
            int v = 0;
            string feld = "S,S,SP,SS,SSP,VZ,VZP,STN,STH,PP".Split(',')[coin.Erhaltung];

            if (coin.Doublette)
                feld = "D" + feld;

            string sql = string.Format("Select {0} from tblBestand where [Guid] = @guid", feld);

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", coin.Guid );
                value = command.ExecuteScalar();
            }

            if (value != null)
            {
                v = (int)value - 1;

                sql = string.Format("Update tblBestand set {0} = @anzahl where [Guid] = @guid", feld);

                using (OleDbCommand command = new OleDbCommand(sql, Connection))
                {
                    command.Parameters.AddWithValue("guid", coin.Guid);
                    command.Parameters.AddWithValue("anzahl", v);
                    command.ExecuteNonQuery();
            HasChanges = true;
                }
            }

            closeConnetion();
        }

        public void DeleteKatalogNummer(string katalognummer)
        {
            string sql = "Delete from tblEigeneKatNr where Coinbook=@Katalognummer";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("Katalognummer", katalognummer);
                command.ExecuteNonQuery();
            HasChanges = true;
            }

            closeConnetion();
        }

        public string GetKatalogNummer(string katalognummer)
        {
            string result = string.Empty;
            string sql = "Select KatNr from tblEigeneKatNr where Coinbook=@Katalognummer";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("Katalognummer", katalognummer);
                object value = command.ExecuteScalar();
                result = value == null ? string.Empty : value.ToString();
            }

            closeConnetion();

            return result;
        }

        public void SaveKatalogNummer(string katalognummer, string eigeneKatalognummer)
        {
            object value;
            string sql = "Select KatNr from tblEigeneKatNr where Coinbook=@Katalognummer";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("Katalognummer", katalognummer);
                value = command.ExecuteScalar();
            }

            if (value == null)
                sql = "Insert Into tblEigeneKatNr (Coinbook,katNr) values (@Katalognummer,@EigeneKatalognummer)";
            else
                sql = "Update tblEigeneKatNr set katNr=@EigeneKatalognummer where Coinbook=@Katalognummer";

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("Katalognummer", katalognummer);
                command.Parameters.AddWithValue("EigeneKatalognummer", eigeneKatalognummer);
                command.ExecuteNonQuery();
            HasChanges = true;
            }

            closeConnetion();
        }

        public void SavePicture(string guid, string picture, bool anzeige)
        {
            int id = ConvertEx.ToInt32(Value("Select max(id) from tblEigeneBilder")) + 1;

            object value;

            openConnection();

            string sql = "Select max(id) from tblEigeneBilder";
            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                value = command.ExecuteScalar();
                if (value is System.DBNull)
                    id = 1;
                else
                    id = (int)value + 1;
            }

            sql = "Select [Guid] from tblEigeneBilder where [Guid] = @guid";
            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("guid", guid);
                value = command.ExecuteScalar();
            }

            if (value == null)
                sql = "Insert into tblEigeneBilder (id,[Guid],DateiName, ShowPicture) values (@id, @guid, @picture, @anzeige)";
            else
                sql = "Update tblEigeneBilder set DateiName =@picture, ShowPicture =@anzeige where [Guid]=@guid";

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("guid", guid);
                command.Parameters.AddWithValue("picture", picture);
                command.Parameters.AddWithValue("anzeige", anzeige);
                command.ExecuteNonQuery();
            HasChanges = true;
            }

            closeConnetion();
        }

        public void DeleteOwnPicture(string file)
        {
            string sql = "Delete from tblEigeneBilder where DateiName = @file";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("file", file);
                command.ExecuteNonQuery();
            HasChanges = true;
            }

            closeConnetion();
        }

        public string CreateBackup(string table, string path)
        {
            String sql = string.Format("Select * from {0}", table);

            DataTable dt = GetDataTable(sql);
            dt.TableName = table;

            //Helper.BackupPath
            string fileName = Path.Combine(path, table + ".xml");

            dt.WriteXml(fileName);

            return fileName;
        }

        //public void SaveTexte(enmTexte typ, string language, BackgroundWorkerEx bgw)
        //{
        //    int p = 0;
        //    openConnection();

        //    string sql = string.Format("Select guid, Text from tblTexte-{0} where typ=@typ", language);

        //    List<KeyValuePair<string, string>> liste = ReadTexte(language, typ.ToString());

        //    sql = string.Format("update tblKatalog set {0}=@text where guid=@guid", enmTexte.Aversbeschreibung.ToString());

        //    var transaction = OpenTransaction;
        //    p++;
        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        foreach (KeyValuePair<string, string> item in liste)
        //        {
        //            command.Transaction = transaction;
        //            command.Parameters.AddWithValue("guid", item.Key);
        //            command.Parameters.AddWithValue("text", item.Value);
        //            command.ExecuteNonQuery();
            //HasChanges = true;
        //        }

        //        ProgressParameter parameter = new ProgressParameter();
        //        parameter.Command = 3;
        //        parameter.Text = string.Format("Speichere {0}", typ.ToString());
        //        parameter.Max = liste.Count;
        //        bgw.ReportProgress(i, parameter);
        //    }

        //    transaction.Commit();
        //    closeConnetion();
        //}


        public Dictionary<string, string> ReadTexte(string language, int typ)
        {
            Dictionary<string, string> liste = new Dictionary<string, string>();

            string sql = string.Format("Select Distinct [guid], Text from tblTexte_{0} where typ=@typ order by [guid]", language.Substring(0, 2));

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("typ", typ);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    liste.Add(reader["Guid"].ToString(), reader["Text"].ToString());
            }

            closeConnetion();

            return liste;
        }

        public Dictionary<int, string> ReadModulTexte(string language, string typ)
        {
            Dictionary<int, string> liste = new Dictionary<int, string>();

            string sql = string.Format("Select Distinct id, Text from tblModule where typ=@typ order by id", language.Substring(0, 2));

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("typ", typ);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    if (!liste.ContainsKey(ConvertEx.ToInt32(reader["id"])))
                        liste.Add(ConvertEx.ToInt32(reader["id"]), reader["Text"].ToString());
            }

            closeConnetion();

            return liste;
        }

        public int GetTextCount(string sql, int typ)
        {
            int result = 0;

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("typ", typ);
                result = ConvertEx.ToInt32(command.ExecuteScalar());
            }
            closeConnetion();

            return result;
        }

        public void InsertTexte(List<Beschreibung> list, string sprache, BackgroundWorkerEx bgw)
        {
            int i = 0;
            string sql = string.Format("Insert into tblTexte_{0} ([guid],[typ],[Text],NationID) values(@guid,@typ,@text,@nation)", sprache);
            openConnection();
            var transaction = OpenTransaction;

            foreach (Beschreibung item in list)
            {
                i++;
                using (OleDbCommand command = new OleDbCommand(sql, Connection))
                {
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("guid", item.Guid);
                    command.Parameters.AddWithValue("typ", item.Typ);
                    command.Parameters.AddWithValue("text", item.Text);
                    command.Parameters.AddWithValue("nation", item.NationID);
                    command.ExecuteNonQuery();
                    HasChanges = true;

                    ProgressParameter parameter = new ProgressParameter();
                    parameter.Command = 3;
                    parameter.Max = list.Count;
                    parameter.Text = string.Format("Speichere Texte {0}", sprache);

                    if (bgw != null)
                        bgw.ReportProgress(i, parameter);
                }
            }
            transaction.Commit();
            closeConnetion();
        }

        public void InsertKatalog(List<Katalog> list, BackgroundWorkerEx bgw)
        {
            string sql = "insert into tblKatalog (ID,NationID,AeraID,GebietID,Jahrgang,KatNr,SPreis,SPPreis,SSPreis,SSPPreis,VZPreis,VZPPreis,STNPreis,STHPreis,"
                          + "PPPreis,Gewicht,Durchmesser,Dicke,Auflage,AuflageSTH,AuflagePP,[GUID],Muenzzeichen,Nominal,Waehrung,AusserKurs,"
                          + "InKurs,gepraegt,Aversbeschreibung,Besonderheit,Reversbeschreibung,Kommentar,Motiv,Rand,Ausgabeanlass,AehnlicheMotive,Material,Legierung,"
                          + "AversEntwurf,ReversEntwurf,Picture,Typ,Form,Orientation,Referenz,Praegeort,AusserkursBOOL,LPPP,LPS,LPSP,LPSS,LPSSP,LPSTH,LPSTN,LPVZ,LPVZP,"
                          + "LPStandPP,LPStandS,LPStandSP,LPStandSS,LPStandSSP,LPStandSTH,LPStandSTN,LPStandVZ,LPStandVZP, Bearbeitungsdatum) "
                          + "values(@ID,@NationID,@AeraID,@GebietID,@Jahrgang,@KatNr,@SPreis,@SPPreis,@SSPreis,@SSPPreis,@VZPreis,@VZPPreis,@STNPreis,@STHPreis,"
                          + "@PPPreis,@Gewicht,@Durchmesser,@Dicke,@Auflage,@AuflageSTH,@AuflagePP,@GUID,@Muenzzeichen,@Nominal,@Waehrung,@AusserKurs,@InKurs,"
                          + "@gepraegt,@Aversbeschreibung,@Besonderheit,@Reversbeschreibung,@Kommentar,@Motiv,@Rand,@Ausgabeanlass,@AehnlicheMotive,@Material,"
                          + "@Legierung,@AversEntwurf,@ReversEntwurf,@Picture,@Typ,@Form,@Orientation,@Referenz,@Praegeort,@AusserkursBOOL,"
                          + "@LPPP,@LPS,@LPSP,@LPSS,@LPSSP,@LPSTH,@LPSTN,@LPVZ,@LPVZP,@LPStandPP,@LPStandS,@LPStandSP,@LPStandSS,@LPStandSSP,@LPStandSTH,@LPStandSTN,"
                          + "@LPStandVZ,@LPStandVZP, @Bearbeitungsdatum)";

            int i = 0;
            openConnection();

            var transaction = OpenTransaction;
            foreach (Katalog item in list)
            {
                using (OleDbCommand command = new OleDbCommand(sql, Connection))
                {
                    i++;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("ID", item.ID);
                    command.Parameters.AddWithValue("NationID", item.NationID);
                    command.Parameters.AddWithValue("AeraID", item.AeraID);
                    command.Parameters.AddWithValue("GebietID", item.GebietID);
                    command.Parameters.AddWithValue("Jahrgang", item.Jahrgang);
                    command.Parameters.AddWithValue("KatNr", item.KatNr);
                    command.Parameters.AddWithValue("SPreis", item.SPreis);
                    command.Parameters.AddWithValue("SPPreis", item.SPPreis);
                    command.Parameters.AddWithValue("SSPreis", item.SSPreis);
                    command.Parameters.AddWithValue("SSPPreis", item.SSPPreis);
                    command.Parameters.AddWithValue("VZPreis", item.VZPreis);
                    command.Parameters.AddWithValue("VZPPreis", item.VZPPreis);
                    command.Parameters.AddWithValue("STNPreis", item.STNPreis);
                    command.Parameters.AddWithValue("STHPreis", item.STHPreis);
                    command.Parameters.AddWithValue("PPPreis", item.PPPreis);
                    command.Parameters.AddWithValue("Gewicht", item.Gewicht);
                    command.Parameters.AddWithValue("Durchmesser", item.Durchmesser);
                    command.Parameters.AddWithValue("Dicke", item.Dicke);
                    command.Parameters.AddWithValue("Auflage", item.Auflage);
                    command.Parameters.AddWithValue("AuflageSTH", item.AuflageSTH);
                    command.Parameters.AddWithValue("AuflagePP", item.AuflagePP);
                    command.Parameters.AddWithValue("GUID", item.GUID);
                    command.Parameters.AddWithValue("Muenzzeichen", item.Muenzzeichen);
                    command.Parameters.AddWithValue("Nominal", item.Nominal);
                    command.Parameters.AddWithValue("Waehrung", item.Waehrung);
                    command.Parameters.AddWithValue("AusserKurs", item.AusserKurs);
                    command.Parameters.AddWithValue("InKurs", item.InKurs);
                    command.Parameters.AddWithValue("gepraegt", item.Gepraegt);
                    command.Parameters.AddWithValue("Aversbeschreibung", item.Aversbeschreibung);
                    command.Parameters.AddWithValue("Besonderheit", item.Besonderheit);
                    command.Parameters.AddWithValue("Reversbeschreibung", item.Reversbeschreibung);
                    command.Parameters.AddWithValue("Kommentar", item.Kommentar);
                    command.Parameters.AddWithValue("Motiv", item.Motiv);
                    command.Parameters.AddWithValue("Rand", item.Rand);
                    command.Parameters.AddWithValue("Ausgabeanlass", item.Ausgabeanlass);
                    command.Parameters.AddWithValue("AehnlicheMotive", item.AehnlicheMotive);
                    command.Parameters.AddWithValue("Material", item.Material);
                    command.Parameters.AddWithValue("Legierung", item.Legierung);
                    command.Parameters.AddWithValue("AversEntwurf", item.AversEntwurf);
                    command.Parameters.AddWithValue("ReversEntwurf", item.ReversEntwurf);
                    command.Parameters.AddWithValue("Picture", item.Picture);
                    command.Parameters.AddWithValue("Typ", item.Typ);
                    command.Parameters.AddWithValue("Form", item.Form);
                    command.Parameters.AddWithValue("Orientation", item.Orientation);
                    command.Parameters.AddWithValue("Referenz", item.Referenz);
                    command.Parameters.AddWithValue("Praegeort", item.Praegeort);
                    //command.Parameters.AddWithValue("AusserkursBOOL", item.AusserkursBOOL == true ? 1 : 0);
                    command.Parameters.AddWithValue("AusserkursBOOL", item.AusserkursBOOL);
                    //command.Parameters.AddWithValue("LPPP", item.LPPP == true ? 1 : 0);
                    //command.Parameters.AddWithValue("LPS", item.LPS == true ? 1 : 0);
                    //command.Parameters.AddWithValue("LPSP", item.LPSP == true ? 1 : 0);
                    //command.Parameters.AddWithValue("LPSS", item.LPSS == true ? 1 : 0);
                    //command.Parameters.AddWithValue("LPSSP", item.LPSSP == true ? 1 : 0);
                    //command.Parameters.AddWithValue("LPSTH", item.LPSTH == true ? 1 : 0);
                    //command.Parameters.AddWithValue("LPSTN", item.LPSTN == true ? 1 : 0);
                    //command.Parameters.AddWithValue("LPVZ", item.LPVZ == true ? 1 : 0);
                    //command.Parameters.AddWithValue("LPVZP", item.LPVZP == true ? 1 : 0);
                    command.Parameters.AddWithValue("LPPP", item.LPPP);
                    command.Parameters.AddWithValue("LPS", item.LPS);
                    command.Parameters.AddWithValue("LPSP", item.LPSP);
                    command.Parameters.AddWithValue("LPSS", item.LPSS);
                    command.Parameters.AddWithValue("LPSSP", item.LPSSP);
                    command.Parameters.AddWithValue("LPSTH", item.LPSTH);
                    command.Parameters.AddWithValue("LPSTN", item.LPSTN);
                    command.Parameters.AddWithValue("LPVZ", item.LPVZ);
                    command.Parameters.AddWithValue("LPVZP", item.LPVZP);
                    command.Parameters.AddWithValue("LPStandPP", item.LPStandPP);
                    command.Parameters.AddWithValue("LPStandS", item.LPStandS);
                    command.Parameters.AddWithValue("LPStandSP", item.LPStandSP);
                    command.Parameters.AddWithValue("LPStandSS", item.LPStandSS);
                    command.Parameters.AddWithValue("LPStandSSP", item.LPStandSSP);
                    command.Parameters.AddWithValue("LPStandSTH", item.LPStandSTH);
                    command.Parameters.AddWithValue("LPStandSTN", item.LPStandSTN);
                    command.Parameters.AddWithValue("LPStandVZ", item.LPStandVZ);
                    command.Parameters.AddWithValue("LPStandVZP", item.LPStandVZP);


                    if (item.Bearbeitungsdatum.Length > 10)
                        command.Parameters.AddWithValue("Bearbeitungsdatum", Convert.ToDateTime(item.Bearbeitungsdatum.Substring(0, 10)).ToShortDateString());
                    else
                        command.Parameters.AddWithValue("Bearbeitungsdatum", "");

                    command.ExecuteNonQuery();
                    HasChanges = true;

                    ProgressParameter parameter = new ProgressParameter();
                    parameter.Command = 3;
                    parameter.Text = "Speichere Katalog";
                    parameter.Max = list.Count;

                    //ProgressChangedEventArgs e = new ProgressChangedEventArgs( i, parameter);

                    if (bgw != null)
                        bgw.ReportProgress(i, parameter);
                }
            }
            transaction.Commit();
            closeConnetion();
        }

        public void EnableIndex(bool enabled, bool dontOpenConnection = false)
        {
            //List<string> liste = new List<string>();

            //    openConnection();

            //string sql = "SELECT * FROM RDB$INDICES";

            //using (OleDbCommand command = new OleDbCommand(sql, Connection))
            //{
            //    OleDbDataReader reader = command.ExecuteReader();

            //    while (reader.Read())
            //        liste.Add(reader["RDB$INDEX_NAME"].ToString().Trim());
            //}

            //foreach (string index in liste)
            //{
            //    if (index.Substring(0, 3) != "RDB")
            //    {
            //        if (enabled)
            //            sql = string.Format("Alter Index {0} active", index);
            //        else
            //            sql = string.Format("Alter Index {0} Inactive", index);


            //        using (OleDbCommand command = new OleDbCommand(sql, Connection))
            //            command.ExecuteNonQuery();

            //        HasChanges = true;

            //    }
            //}

            //    closeConnetion();
        }

        public OleDbTransaction OpenTransaction
        {
            get
            {
                return connection.BeginTransaction();
            }
        }

        //public MünzDetail GetMuenzDetail(string guid)
        //{
        //    MünzDetail result = new MünzDetail();

        //    openConnection();

        //    string sql = "select * from tblKatalog where [guid]=@guid";
        //    using (OleDbCommand command = new OleDbCommand(sql, Connection))
        //    {
        //        command.Parameters.AddWithValue("guid", guid);

        //        OleDbDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            result.ID = ConvertEx.ToInt32(reader["ID"]);
        //            result.Auflage = reader["Auflage"].ToString();
        //            result.AuflagePP = reader["AuflagePP"].ToString();
        //            result.AuflageSTH = reader["AuflageSTH"].ToString();
        //            result.Dicke = ConvertEx.ToDecimal(reader["Dicke"]);
        //            result.Durchmesser = ConvertEx.ToDecimal(reader["Durchmesser"]);
        //            result.Gepraegt = reader["gepraegt"].ToString();
        //            result.Gewicht = ConvertEx.ToDecimal(reader["Gewicht"]);
        //            result.GUID = reader["GUID"].ToString();
        //            result.InKurs = reader["InKurs"].ToString();
        //            result.Jahrgang = reader["Jahrgang"].ToString();
        //            result.KatNr = reader["KatNr"].ToString();
        //            result.Muenzzeichen = reader["Muenzzeichen"].ToString();
        //            result.Nominal = reader["Nominal"].ToString();
        //            result.Picture = reader["Picture"].ToString();
        //            result.AusserKurs = reader["AusserKurs"].ToString();
        //            result.Waehrung = reader["Waehrung"].ToString();

        //            result.AehnlicheMotive = reader["AehnlicheMotive"].ToString();
        //            result.Ausgabeanlass = reader["Ausgabeanlass"].ToString();
        //            result.Aversbeschreibung = reader["Aversbeschreibung"].ToString();
        //            result.AversEntwurf = reader["AversEntwurf"].ToString();
        //            result.Besonderheit = reader["Besonderheit"].ToString();
        //            result.Form = reader["Form"].ToString();
        //            result.Kommentar = reader["Kommentar"].ToString();
        //            result.Legierung = reader["Legierung"].ToString();
        //            result.Material = reader["Material"].ToString();
        //            result.Motiv = reader["Motiv"].ToString();
        //            result.Orientation = reader["Orientation"].ToString();
        //            result.Rand = reader["Rand"].ToString();
        //            result.Reversbeschreibung = reader["Reversbeschreibung"].ToString();
        //            result.ReversEntwurf = reader["ReversEntwurf"].ToString();

        //            result.AusserKursBool = ConvertEx.ToBoolean(reader["AusserKursBOOL"]);
        //            result.Prägeort = reader["Praegeort"].ToString();
        //            result.Typ = reader["Typ"].ToString();
        //            result.Referenz = reader["Referenz"].ToString();
        //            result.Bearbeitungsdatum = reader["Bearbeitungsdatum"].ToString();    
        //        }
        //    }


        //    //result.Referenz = reader["Referenz"].ToString();
        //    //result.SPPreis = ConvertEx.ToDecimal(reader["SPPreis"]);
        //    //result.SPreis = ConvertEx.ToDecimal(reader["SPreis"]);
        //    //result.SSPPreis = ConvertEx.ToDecimal(reader["SSPPreis"]);
        //    //result.SSPreis = ConvertEx.ToDecimal(reader["SSPreis"]);
        //    //result.VZPPreis = ConvertEx.ToDecimal(reader["VZPPreis"]);
        //    //result.VZPreis = ConvertEx.ToDecimal(reader["VZPreis"]);
        //    //result.STHPreis = ConvertEx.ToDecimal(reader["STHPreis"]);
        //    //result.STNPreis = ConvertEx.ToDecimal(reader["STNPreis"]);
        //    //result.PPPreis = ConvertEx.ToDecimal(reader["PPPreis"]);
        //    //result.Typ = reader["Typ"].ToString();
        //    //result.HinweisKZ = reader["HinweisKZ"].ToString();
        //    //result.SP = reader["SP"].ToString();
        //    //result.S = reader["S"].ToString();
        //    //result.SSP = reader["SSP"].ToString();
        //    //result.SS = reader["SS"].ToString();
        //    //result.VZP = reader["VZP"].ToString();
        //    //result.VZ = reader["VZ"].ToString();
        //    //result.STH = reader["STH"].ToString();
        //    //result.STN = reader["STN"].ToString();
        //    //result.PP = reader["PP"].ToString();
        //    //result.SummePP = ConvertEx.ToDecimal(reader["SummePP"]) != 0 ? string.Format("{0:###,##0.00}", ConvertEx.ToDecimal(reader["SummePP"])) : string.Empty;
        //    //result.SummeS = ConvertEx.ToDecimal(reader["SummeS"]) != 0 ? string.Format("{0:###,##0.00}", ConvertEx.ToDecimal(reader["SummeS"])) : string.Empty;
        //    //result.Farbe = reader["Farbe"].ToString();
        //    //result.OriginalKatNr = reader["OriginalKatNr"].ToString();



        //    //result.AehnlicheMotive = string.Empty;
        //    //result.Ausgabeanlass = string.Empty;
        //    //result.Aversbeschreibung = string.Empty;
        //    //result.AversEntwurf = string.Empty;
        //    //result.Besonderheit = string.Empty;
        //    //result.Form = string.Empty;
        //    //result.Kommentar = string.Empty;
        //    //result.Legierung = string.Empty;
        //    //result.Material = string.Empty;
        //    //result.Motiv = string.Empty;
        //    //result.Orientation = string.Empty;
        //    //result.Rand = string.Empty;
        //    //result.Reversbeschreibung = string.Empty;
        //    //result.ReversEntwurf = string.Empty;

        //    closeConnetion();
        //    return result;
        //}

        public DataTable GetReportingNations(int nation)
        {
            string sql = "SELECT Distinct tblNation.Bezeichnung, tblNation.ID "
                + "FROM (tblNation INNER JOIN tblKatalog ON tblNation.ID = tblKatalog.NationID) INNER JOIN tblBestand ON tblKatalog.[Guid] = tblBestand.[GUID] "
                + "Where ";

            if (nation != 0)
                sql = sql + "tblNation.ID = @nation and ";

            sql = sql + "coalesce(S,0)+coalesce(SP,0)+coalesce(SS,0)+coalesce(SSP,0)+coalesce(VZ,0)+coalesce(VZP,0)+coalesce(STN,0)+coalesce(STH,0)+coalesce(PP,0)>0 "
              + " ORDER BY tblNation.Bezeichnung";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("nation", nation);

            return GetDataTable(sql, parameters);
        }

        public DataTable GetReportingAeras(int nation, int aera)
        {
            string sql = "SELECT Distinct tblAera.Bezeichnung, tblAera.ID, tblAera.NationID, tblAera.Sortierung "
                    + "FROM (tblAera INNER JOIN tblKatalog ON tblAera.ID = tblKatalog.AeraID) INNER JOIN tblBestand ON tblKatalog.[Guid] = tblBestand.[GUID] "
                    + "Where ";

            if (nation != 0)
                sql = sql + "tblAera.NationID = @nation and ";

            if (nation != 0 && aera != 0)
                sql = sql + "tblAera.ID = @aera and ";

            sql = sql + "S+SP+SS+SSP+VZ+VZP+STN+STH+PP>0 ORDER BY tblAera.Sortierung";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("nation", nation);
            parameters.Add("aera", aera);

            return GetDataTable(sql, parameters);
        }

        public DataTable GetReportingGebiete(int nation, int aera, int region)
        {
            string sql = "SELECT Distinct tblGebiet.Bezeichnung, tblGebiet.ID, tblGebiet.AeraID, tblGebiet.Sortierung "
                    + "FROM (tblGebiet INNER JOIN tblKatalog ON tblGebiet.ID = tblKatalog.GebietID) INNER JOIN tblBestand ON tblKatalog.[Guid] = tblBestand.[GUID] "
                    + "Where ";

            if (region == 0)
            {
                if (nation != 0)
                    sql = sql + "tblGebiet.NationID = @nation and ";

                if (aera != 0 && region != 0)
                    sql = sql + "AeraID = @aera and ";

                sql = sql + "S+SP+SS+SSP+VZ+VZP+STN+STH+PP>0 ORDER BY tblGebiet.Sortierung";
            }
            else
                sql = sql + "tblGebiet.id = @region";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("nation", nation);
            parameters.Add("aera", aera);
            parameters.Add("region", region);

            return GetDataTable(sql, parameters);
        }

        public DataTable Reporting(enmReportTyp reportTyp, int nation, int ära, int region, enmPreise preistyp, decimal currentFactor)
        {
            string factor = currentFactor.ToString().Replace(",", ".");
            string sql = "SELECT tblKatalog.NationID, tblKatalog.AeraID, tblKatalog.GebietID, tblNation.Bezeichnung as Nation, tblAera.Bezeichnung as Aera, "
                    + "tblGebiet.Bezeichnung as Gebiet, Waehrung, Nominal, Jahrgang, Muenzzeichen as Muenzz, tblKatalog.[Guid], ";

            switch (reportTyp)
            {
                case enmReportTyp.ReportSammlung:
                    if (preistyp == enmPreise.Kaufpreise)
                        sql = sql + "iif(tblBestand.S<>0, tblBestand.S, null) as S, "
                            + "0 AS SPreis, "
                            + "iif(tblBestand.SP<>0, tblBestand.SP, null) as SP, "
                            + "0 AS SPPreis, "
                            + "iif(tblBestand.SS<>0, tblBestand.SS, null) as SS, "
                            + "0 AS SSPreis, "
                            + "iif(tblBestand.SSP<>0, tblBestand.SSP, null) as SSP, "
                            + "0 AS SSPPreis, "
                            + "iif(tblBestand.VZ<>0, tblBestand.VZ, null) as VZ, "
                            + "0 AS VZPreis, "
                            + "iif(tblBestand.VZP<>0, tblBestand.VZP, null) as VZP, "
                            + "0 AS VZPPreis, "
                            + "iif(tblBestand.STN<>0, tblBestand.STN, null) as STN, "
                            + "0 AS STNPreis, "
                            + "iif(tblBestand.STH<>0, tblBestand.STH, null) as STH, "
                            + "0 AS STHPreis, "
                            + "iif(tblBestand.PP<>0, tblBestand.PP, null) as PP, "
                            + "0 AS PPPreis, "
                            + "0 AS Gesamt ";

                    if (preistyp == enmPreise.Katalogpreise)
                        sql = sql + "coalesce(tblBestand.S,0) as S, "
                            + "tblBestand.S*tblKatalog.SPreis*{0} AS SPreis, "
                            + "coalesce(tblBestand.SP,0) as SP, "
                            + "tblBestand.SP*tblKatalog.SPPreis*{0} AS SPPreis, "
                            + "coalesce(tblBestand.SS,0) as SS, "
                            + "tblBestand.SS*tblKatalog.SSPreis*{0} AS SSPreis, "
                            + "coalesce(tblBestand.SSP,0) as SSP, "
                            + "tblBestand.SSP*tblKatalog.SSPPreis*{0} AS SSPPreis, "
                            + "coalesce(tblBestand.VZ,0) as VZ, "
                            + "tblBestand.VZ*tblKatalog.VZPreis*{0} AS VZPreis, "
                            + "coalesce(tblBestand.VZP,0) as VZP, "
                            + "tblBestand.VZP*tblKatalog.VZPPreis*{0} AS VZPPreis, "
                            + "coalesce(tblBestand.STN,0) as STN, "
                            + "tblBestand.STN*tblKatalog.STNPreis*{0} AS STNPreis, "
                            + "coalesce(tblBestand.STH,0) as STH, "
                            + "tblBestand.STH*tblKatalog.STHPreis*{0} AS STHPreis, "
                            + "coalesce(tblBestand.PP,0) as PP, "
                            + "tblBestand.PP*tblKatalog.PPPreis*{0} AS PPPreis, "
                            + "(tblBestand.S*tblKatalog.SPreis+tblBestand.SP*tblKatalog.SPPreis+tblBestand.SS*tblKatalog.SSPreis"
                            + "+tblBestand.SSP*tblKatalog.SSPPreis+tblBestand.VZ*tblKatalog.VZPreis+tblBestand.VZP*tblKatalog.VZPPreis"
                            + "+tblBestand.STN*tblKatalog.STNPreis+tblBestand.STH*tblKatalog.STHPreis+tblBestand.PP*tblKatalog.PPPreis)"
                            + "*{0} AS Gesamt ";

                    if (preistyp == enmPreise.EigenePreise)
                        sql = sql + "iif(tblBestand.S<>0, tblBestand.S, null) as S, "
                            + "tblBestand.S*iif(tblPreise.SPreis<>0, tblPreise.SPreis,tblKatalog.SPreis)*{0} AS SPreis, "
                            + "iif(tblBestand.SP<>0, tblBestand.SP, null) as SP, "
                            + "tblBestand.SP*iif(tblPreise.SPPreis<>0, tblPreise.SPPreis,tblKatalog.SPPreis)*{0} AS SPPreis, "
                            + "iif(tblBestand.SS<>0, tblBestand.SS, null) as SS, "
                            + "tblBestand.SS*iif(tblPreise.SSPreis<>0, tblPreise.SSPreis,tblKatalog.SSPreis)*{0} AS SSPreis, "
                            + "iif(tblBestand.SSP<>0, tblBestand.SSP, null) as SSP, "
                            + "tblBestand.SSP*iif(tblPreise.SSPPreis<>0, tblPreise.SSPPreis,tblKatalog.SSPPreis)*{0} AS SSPPreis, "
                            + "iif(tblBestand.VZ<>0, tblBestand.VZ, null) as VZ, "
                            + "tblBestand.VZ*iif(tblPreise.VZPreis<>0, tblPreise.VZPreis,tblKatalog.VZPreis)*{0} AS VZPreis, "
                            + "iif(tblBestand.VZP<>0, tblBestand.VZP, null) as VZP, "
                            + "tblBestand.VZP*iif(tblPreise.VZPPreis<>0, tblPreise.VZPPreis,tblKatalog.VZPPreis)*{0} AS VZPPreis, "
                            + "iif(tblBestand.STN<>0, tblBestand.STN, null) as STN, "
                            + "tblBestand.STN*iif(tblPreise.STNPreis<>0, tblPreise.STNPreis,tblKatalog.STNPreis)*{0} AS STNPreis, "
                            + "iif(tblBestand.STH<>0, tblBestand.STH, null) as STH, "
                            + "tblBestand.STH*iif(tblPreise.STHPreis<>0, tblPreise.STHPreis,tblKatalog.STHPreis)*{0} AS STHPreis, "
                            + "iif(tblBestand.PP<>0, tblBestand.PP, null) as PP, "
                            + "tblBestand.PP*iif(tblPreise.PPPreis<>0, tblPreise.PPPreis,tblKatalog.PPPreis)*{0} AS PPPreis, "
                            + "(tblBestand.S*iif(tblPreise.SPreis<>0, tblPreise.SPreis,tblKatalog.SPreis)"
                            + "+tblBestand.SP*iif(tblPreise.SPPreis<>0, tblPreise.SPPreis,tblKatalog.SPPreis)"
                            + "+tblBestand.SS*iif(tblPreise.SSPreis<>0, tblPreise.SSPreis,tblKatalog.SSPreis)"
                            + "+tblBestand.SSP*iif(tblPreise.SSPPreis<>0, tblPreise.SSPPreis,tblKatalog.SSPPreis)"
                            + "+tblBestand.VZ*iif(tblPreise.VZPreis<>0, tblPreise.VZPreis,tblKatalog.VZPreis)"
                            + "+tblBestand.VZP*iif(tblPreise.VZPPreis<>0, tblPreise.VZPPreis,tblKatalog.VZPPreis)"
                            + "+tblBestand.STN*iif(tblPreise.STNPreis<>0, tblPreise.STNPreis,tblKatalog.STNPreis)"
                            + "+tblBestand.STH*iif(tblPreise.STHPreis<>0, tblPreise.STHPreis,tblKatalog.STHPreis)"
                            + "+tblBestand.PP*iif(tblPreise.PPPreis<>0, tblPreise.PPPreis,tblKatalog.PPPreis))"
                            + "*{0} AS Gesamt ";
                    break;

                case enmReportTyp.ReportDoubletten:
                    if (preistyp == enmPreise.Kaufpreise)
                        sql = sql + "iif(tblBestand.DS<>0,tblBestand.DS,null) as S, "
                            + "0 AS SPreis, "
                            + "iif(tblBestand.DSP<>0, tblBestand.DSP, null) as SP, "
                            + "0 AS SPPreis, "
                            + "iif(tblBestand.DSS<>0, tblBestand.DSS, null) as SS, "
                            + "0 AS SSPreis, "
                            + "iif(tblBestand.DSSP<>0, tblBestand.DSSP, null) as SSP, "
                            + "0 AS SSPPreis, "
                            + "iif(tblBestand.DVZ<>0, tblBestand.DVZ, null) as VZ, "
                            + "0 AS VZPreis, "
                            + "iif(tblBestand.DVZP<>0, tblBestand.DVZP, null) as VZP, "
                            + "0 AS VZPPreis, "
                            + "iif(tblBestand.DSTN<>0, tblBestand.DSTN, null) as STN, "
                            + "0 AS STNPreis, "
                            + "iif(tblBestand.DSTH<>0, tblBestand.DSTH, null) as STH, "
                            + "0 AS STHPreis, "
                            + "iif(tblBestand.DPP<>0, tblBestand.DPP, null) as PP, "
                            + "0 AS PPPreis, "
                            + "0 AS Gesamt ";

                    if (preistyp == enmPreise.Katalogpreise)
                        sql = sql + "iif(tblBestand.DS<>0,tblBestand.DS,null) as S, "
                            + "tblBestand.DS*tblKatalog.SPreis*{0} AS SPreis, "
                            + "iif(tblBestand.DSP<>0, tblBestand.DSP, null) as SP, "
                            + "tblBestand.DSP*tblKatalog.SPPreis*{0} AS SPPreis, "
                            + "iif(tblBestand.DSS<>0, tblBestand.DSS, null) as SS, "
                            + "tblBestand.DSS*tblKatalog.SSPreis*{0} AS SSPreis, "
                            + "iif(tblBestand.DSSP<>0, tblBestand.DSSP, null) as SSP, "
                            + "tblBestand.DSSP*tblKatalog.SSPPreis*{0} AS SSPPreis, "
                            + "iif(tblBestand.DVZ<>0, tblBestand.DVZ, null) as VZ, "
                            + "tblBestand.DVZ*tblKatalog.VZPreis*{0} AS VZPreis, "
                            + "iif(tblBestand.DVZP<>0, tblBestand.DVZP, null) as VZP, "
                            + "tblBestand.DVZP*tblKatalog.VZPPreis*{0} AS VZPPreis, "
                            + "iif(tblBestand.DSTN<>0, tblBestand.DSTN, null) as STN, "
                            + "tblBestand.DSTN*tblKatalog.STNPreis*{0} AS STNPreis, "
                            + "iif(tblBestand.DSTH<>0, tblBestand.DSTH, null) as STH, "
                            + "tblBestand.DSTH*tblKatalog.STHPreis*{0} AS STHPreis, "
                            + "iif(tblBestand.DPP<>0, tblBestand.DPP, null) as PP, "
                            + "tblBestand.DPP*tblKatalog.PPPreis*{0} AS PPPreis, "
                            + "(tblBestand.DS*tblKatalog.SPreis+tblBestand.DSP*tblKatalog.SPPreis "
                            + "+tblBestand.DSS*tblKatalog.SSPreis+tblBestand.DSSP*tblKatalog.SSPPreis"
                            + "+tblBestand.DVZ*tblKatalog.VZPreis+tblBestand.DVZP*tblKatalog.VZPPreis"
                            + "+tblBestand.DSTN*tblKatalog.STNPreis+tblBestand.DSTH*tblKatalog.STHPreis+tblBestand.DPP*tblKatalog.PPPreis)"
                            + "*{0} AS Gesamt ";

                    if (preistyp == enmPreise.EigenePreise)
                        sql = sql + "iif(tblBestand.DS<>0, tblBestand.DS, null) as S, "
                            + "tblBestand.DS*iif(tblPreise.SPreis<>0, tblPreise.SPreis,tblKatalog.SPreis)*{0} AS SPreis, "
                            + "iif(tblBestand.DSP<>0, tblBestand.DSP, null) as SP, "
                            + "tblBestand.DSP*iif(tblPreise.SPPreis<>0, tblPreise.SPPreis,tblKatalog.SPPreis)*{0} AS SPPreis, "
                            + "iif(tblBestand.DSS<>0, tblBestand.DSS, null) as SS, "
                            + "tblBestand.DSS*iif(tblPreise.SSPreis<>0, tblPreise.SSPreis,tblKatalog.SSPreis)*{0} AS SSPreis, "
                            + "iif(tblBestand.DSSP<>0, tblBestand.DSSP, null) as SSP, "
                            + "tblBestand.DSSP*iif(tblPreise.SSPPreis<>0, tblPreise.SSPPreis,tblKatalog.SSPPreis)*{0} AS SSPPreis, "
                            + "iif(tblBestand.DVZ<>0, tblBestand.DVZ, null) as VZ, "
                            + "tblBestand.DVZ*iif(tblPreise.VZPreis<>0, tblPreise.VZPreis,tblKatalog.VZPreis)*{0} AS VZPreis, "
                            + "iif(tblBestand.DVZP<>0, tblBestand.DVZP, null) as VZP, "
                            + "tblBestand.DVZP*iif(tblPreise.VZPPreis<>0, tblPreise.VZPPreis,tblKatalog.VZPPreis)*{0} AS VZPPreis, "
                            + "iif(tblBestand.DSTN<>0, tblBestand.DSTN, null) as STN, "
                            + "tblBestand.DSTN*iif(tblPreise.STNPreis<>0, tblPreise.STNPreis,tblKatalog.STNPreis)*{0} AS STNPreis, "
                            + "iif(tblBestand.DSTH<>0, tblBestand.DSTH, null) as STH, "
                            + "tblBestand.DSTH*iif(tblPreise.STHPreis<>0, tblPreise.STHPreis,tblKatalog.STHPreis)*{0} AS STHPreis, "
                            + "iif(tblBestand.DPP<>0, tblBestand.DPP, null) as PP, "
                            + "tblBestand.DPP*iif(tblPreise.PPPreis<>0, tblPreise.PPPreis,tblKatalog.PPPreis)*{0} AS PPPreis, "
                            + "(tblBestand.DS*iif(tblPreise.SPreis<>0, tblPreise.SPreis,tblKatalog.SPreis)"
                            + "+tblBestand.DSP*iif(tblPreise.SPPreis<>0, tblPreise.SPPreis,tblKatalog.SPPreis)"
                            + "+tblBestand.DSS*iif(tblPreise.SSPreis<>0, tblPreise.SSPreis,tblKatalog.SSPreis)"
                            + "+tblBestand.DSSP*iif(tblPreise.SSPPreis<>0, tblPreise.SSPPreis,tblKatalog.SSPPreis)"
                            + "+tblBestand.DVZ*iif(tblPreise.VZPreis<>0, tblPreise.VZPreis,tblKatalog.VZPreis)"
                            + "+tblBestand.DVZP*iif(tblPreise.VZPPreis<>0, tblPreise.VZPPreis,tblKatalog.VZPPreis)"
                            + "+tblBestand.DSTN*iif(tblPreise.STNPreis<>0, tblPreise.STNPreis,tblKatalog.STNPreis)"
                            + "+tblBestand.DSTH*iif(tblPreise.STHPreis<>0, tblPreise.STHPreis,tblKatalog.STHPreis)"
                            + "+tblBestand.DPP*iif(tblPreise.PPPreis<>0, tblPreise.PPPreis,tblKatalog.PPPreis))"
                            + "*{0} AS Gesamt ";
                    break;
            }
            sql = sql + "FROM ((((tblKatalog Inner JOIN tblBestand ON tblKatalog.[Guid] = tblBestand.[GUID]) "
                + "Left JOIN tblPreise ON tblKatalog.[Guid] = tblPreise.[GUID]) "
                + "INNER JOIN tblNation ON tblKatalog.NationID = tblNation.ID) "
                + "INNER JOIN tblAera ON tblKatalog.AeraID = tblAera.ID) "
                + "INNER JOIN tblGebiet ON tblKatalog.GebietID = tblGebiet.ID ";

            if (nation != 0)
                sql = sql + "WHERE tblKatalog.NationID=@nation";

            if (ära != 0)
                sql = sql + " AND tblKatalog.AeraID=@aera";

            if (region != 0)
                sql = sql + " and tblKatalog.GebietID=@region";

            sql = sql + " Order by Waehrung, Nominal, Jahrgang, Muenzzeichen";

            sql = string.Format(sql, factor);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("nation", nation);
            parameters.Add("aera", ära);
            parameters.Add("region", region);

            return GetDataTable(sql, parameters);
            //List<Report> result = new List<Report>();

            //openConnection();

            //using (OleDbCommand command = new OleDbCommand(sql, Connection))
            //{
            //    command.Parameters.AddWithValue("nation", nation);
            //    command.Parameters.AddWithValue("aera", ära);
            //    command.Parameters.AddWithValue("gebiet", gebiet);

            //    OleDbDataReader reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Report item = new Report();
            //        item.NationID = ConvertEx.ToInt32(reader["NationID"]);
            //        item.AeraID = ConvertEx.ToInt32(reader["AeraID"]);
            //        item.GebietID = ConvertEx.ToInt32(reader["GebietID"]);
            //        item.Nation = reader["Nation"].ToString();
            //        item.Aera = reader["Aera"].ToString();
            //        item.Gebiet = reader["Gebiet"].ToString();
            //        item.Waehrung = reader["Waehrung"].ToString();
            //        item.Nominal = reader["Nominal"].ToString();
            //        item.Jahrgang = reader["Jahrgang"].ToString();
            //        item.Muenzz = reader["Muenzz"].ToString();
            //        item. [Guid]  = reader["GUID"].ToString();
            //        item.S = ConvertEx.ToInt32(reader["S"]);
            //        item.SP = ConvertEx.ToInt32(reader["SP"]);
            //        item.SS = ConvertEx.ToInt32(reader["SS"]);
            //        item.SSP = ConvertEx.ToInt32(reader["SSP"]);
            //        item.VZ = ConvertEx.ToInt32(reader["VZ"]);
            //        item.VZP = ConvertEx.ToInt32(reader["VZP"]);
            //        item.STN = ConvertEx.ToInt32(reader["STN"]);
            //        item.STH = ConvertEx.ToInt32(reader["STH"]);
            //        item.PP = ConvertEx.ToInt32(reader["PP"]);
            //        item.SPreis = ConvertEx.ToDecimal(reader["SPreis"]);
            //        item.SPPreis = ConvertEx.ToDecimal(reader["SPPreis"]);
            //        item.SSPreis = ConvertEx.ToDecimal(reader["SSPreis"]);
            //        item.SSPPreis = ConvertEx.ToDecimal(reader["SSPPreis"]);
            //        item.VZPreis = ConvertEx.ToDecimal(reader["VZPreis"]);
            //        item.VZPPreis = ConvertEx.ToDecimal(reader["VZPPreis"]);
            //        item.STNPreis = ConvertEx.ToDecimal(reader["STNPreis"]);
            //        item.STHPreis = ConvertEx.ToDecimal(reader["STHPreis"]);
            //        item.PPPreis = ConvertEx.ToDecimal(reader["PPPreis"]);
            //        item.Gesamt = ConvertEx.ToDecimal(reader["Gesamt"]);
            //        result.Add(item);
            //    }
            //}
            //    closeConnetion();

            //    //DataTable dt = database.GetDataTable(sql);
            //    //dt.TableName = "tblReport";

            //    //if (preistyp ==  enmPreise.Kaufpreise)
            //    //	dt = getReportingKaufpreise(dt, nation, database, false);

            //    //   for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //    //     if (ConvertEx.ToDouble0(dt.Rows[i]["Gesamt"]) == 0)
            //    //       dt.Rows[i].Delete();
            //    //     else
            //    //       for (int c = 11; c < 29; c++)
            //    //         if (ConvertEx.ToDouble0(dt.Rows[i][c]) == 0)
            //    //           dt.Rows[i][c] = DBNull.Value;

            //    //dt.AcceptChanges();
            //    return result;

        }

        public DataTable ReportingWert(enmReportTyp reportTyp, long nation, double currentFactor, enmPreise settings)
        {
            string sql = String.Empty;
            string faktor = currentFactor.ToString().Replace(",", ".");

            if (reportTyp == enmReportTyp.KostenSammlung || reportTyp == enmReportTyp.KostenDoubletten)
                settings = enmPreise.Kaufpreise;

            if (settings == enmPreise.Kaufpreise && reportTyp == enmReportTyp.WerteSammlung)
                reportTyp = enmReportTyp.KostenSammlung;

            if (settings == enmPreise.Kaufpreise && reportTyp == enmReportTyp.WerteDoubletten)
                reportTyp = enmReportTyp.KostenDoubletten;

            if (nation == 0)
                sql = "SELECT tblNation.Bezeichnung as Beschreibung, tblNation.ID as Gebiet, ";
            else
                sql = "SELECT tblAera.Bezeichnung as Beschreibung, tblAera.ID as Gebiet,";

            switch (reportTyp)
            {
                case enmReportTyp.WerteSammlung:
                    sql = sql + "Sum(tblBestand.S) AS S, Sum(tblBestand.SP) AS SP, Sum(tblBestand.SS) AS SS, Sum(tblBestand.SSP) AS SSP, Sum(tblBestand.VZ) AS VZ, "
                        + "Sum(tblBestand.VZP) AS VZP, Sum(tblBestand.STN) AS STN, Sum(tblBestand.STH) AS STH, Sum(tblBestand.PP) AS PP, "
                        + "Sum(tblBestand.S) + Sum(tblBestand.SP) + Sum(tblBestand.SS) + Sum(tblBestand.SSP) + Sum(tblBestand.VZ) +Sum(tblBestand.VZP) + Sum(tblBestand.STN) + Sum(tblBestand.STH) + Sum(tblBestand.PP) AS Anzahl, ";

                    switch (settings)
                    {
                        case enmPreise.Katalogpreise:
                            sql = sql + "(Sum(tblBestand.S * tblKatalog.SPreis) "
                            + "+ Sum(tblBestand.SP  * tblKatalog.SPPreis) "
                            + "+ Sum(tblBestand.SS  * tblKatalog.SSPreis) "
                            + "+ Sum(tblBestand.SSP * tblKatalog.SSPPreis) "
                            + "+ Sum(tblBestand.VZ  * tblKatalog.VZPreis) "
                            + "+ Sum(tblBestand.VZP * tblKatalog.VZPPreis) "
                            + "+ Sum(tblBestand.STN * tblKatalog.STNPreis) "
                            + "+ Sum(tblBestand.STH * tblKatalog.STHPreis) "
                            + "+ Sum(tblBestand.PP  * tblKatalog.PPPreis)) * ";
                            break;

                        case enmPreise.EigenePreise:
                            sql = sql + "(Sum(tblBestand.S * iif(tblPreise.SPreis<>0, tblPreise.SPreis,tblKatalog.SPreis)) "
                            + "+ Sum(tblBestand.SP  * iif(tblPreise.SPreis<>0, tblPreise.SPPreis,tblKatalog.SPPreis)) "
                            + "+ Sum(tblBestand.SS  * iif(tblPreise.SSPreis<>0, tblPreise.SSPreis,tblKatalog.SSPreis)) "
                            + "+ Sum(tblBestand.SSP * iif(tblPreise.SSPPreis<>0, tblPreise.SSPPreis,tblKatalog.SSPPreis)) "
                            + "+ Sum(tblBestand.VZ  * iif(tblPreise.VZPreis<>0, tblPreise.VZPreis,tblKatalog.VZPreis)) "
                            + "+ Sum(tblBestand.VZP * iif(tblPreise.VZPPreis<>0, tblPreise.VZPPreis,tblKatalog.VZPPreis)) "
                            + "+ Sum(tblBestand.STN * iif(tblPreise.STNPreis<>0, tblPreise.STNPreis,tblKatalog.STNPreis)) "
                            + "+ Sum(tblBestand.STH * iif(tblPreise.STHPreis<>0, tblPreise.STHPreis,tblKatalog.STHPreis)) "
                            + "+ Sum(tblBestand.PP  * iif(tblPreise.PPPreis<>0, tblPreise.PPPreis,tblKatalog.PPPreis))) * ";
                            break;

                        case enmPreise.Kaufpreise:
                            sql = sql + "0 * ";
                            break;
                    }

                    sql = sql + faktor + " as Gesamt ";
                    break;

                case enmReportTyp.WerteDoubletten:
                    sql = sql + "Sum(tblBestand.DS) AS S, Sum(tblBestand.DSP) AS SP, Sum(tblBestand.DSS) AS SS, Sum(tblBestand.DSSP) AS SSP, Sum(tblBestand.DVZ) AS VZ, "
                        + "Sum(tblBestand.DVZP) AS VZP, Sum(tblBestand.DSTN) AS STN, Sum(tblBestand.DSTH) AS STH, Sum(tblBestand.DPP) AS PP, "
                        + "Sum(tblBestand.DS) + Sum(tblBestand.DSP) + Sum(tblBestand.DSS) + Sum(tblBestand.DSSP) "
                        + "+ Sum(tblBestand.DVZ) +Sum(tblBestand.DVZP) + Sum(tblBestand.DSTN) + Sum(tblBestand.DSTH) + Sum(tblBestand.PP) AS Anzahl, ";

                    switch (settings)
                    {
                        case enmPreise.Katalogpreise:
                            sql = sql + "(Sum(tblBestand.DS * tblKatalog.SPreis) "
                            + "+ Sum(tblBestand.DSP  * tblKatalog.SPPreis) "
                            + "+ Sum(tblBestand.DSS  * tblKatalog.SSPreis) "
                            + "+ Sum(tblBestand.DSSP * tblKatalog.SSPPreis) "
                            + "+ Sum(tblBestand.DVZ  * tblKatalog.VZPreis) "
                            + "+ Sum(tblBestand.DVZP * tblKatalog.VZPPreis) "
                            + "+ Sum(tblBestand.DSTN * tblKatalog.STNPreis) "
                            + "+ Sum(tblBestand.DSTH * tblKatalog.STHPreis) "
                            + "+ Sum(tblBestand.DPP  * tblKatalog.PPPreis)) * ";
                            break;

                        case enmPreise.EigenePreise:
                            sql = sql + "(Sum(tblBestand.DS * iif(tblPreise.SPreis<>0, tblPreise.SPreis,tblKatalog.SPreis)) "
                            + "+ Sum(tblBestand.DSP  * iif(tblPreise.SPPreis<>0, tblPreise.SPPreis,tblKatalog.SPPreis)) "
                            + "+ Sum(tblBestand.DSS  * iif(tblPreise.SSPreis<>0, tblPreise.SSPreis,tblKatalog.SSPreis)) "
                            + "+ Sum(tblBestand.DSSP * iif(tblPreise.SSPPreis<>0, tblPreise.SSPPreis,tblKatalog.SSPPreis)) "
                            + "+ Sum(tblBestand.DVZ  * iif(tblPreise.VZPreis<>0, tblPreise.VZPreis,tblKatalog.VZPreis)) "
                            + "+ Sum(tblBestand.DVZP * iif(tblPreise.VZPPreis<>0, tblPreise.VZPPreis,tblKatalog.VZPPreis)) "
                            + "+ Sum(tblBestand.DSTN * iif(tblPreise.STNPreis<>0, tblPreise.STNPreis,tblKatalog.STNPreis)) "
                            + "+ Sum(tblBestand.DSTH * iif(tblPreise.STHPreis<>0, tblPreise.STHPreis,tblKatalog.STHPreis)) "
                            + "+ Sum(tblBestand.DPP  * iif(tblPreise.PPPreis<>0, tblPreise.PPPreis,tblKatalog.PPPreis))) * ";
                            break;

                        case enmPreise.Kaufpreise:
                            sql = sql + "0 * ";
                            break;
                    }

                    sql = sql + faktor + " as Gesamt ";
                    break;

                case enmReportTyp.KostenSammlung:
                    sql = sql + "Sum(tblBestand.S) AS S, Sum(tblBestand.SP) AS SP, Sum(tblBestand.SS) AS SS, Sum(tblBestand.SSP) AS SSP, Sum(tblBestand.VZ) AS VZ, "
                        + "Sum(tblBestand.VZP) AS VZP, Sum(tblBestand.STN) AS STN, Sum(tblBestand.STH) AS STH, Sum(tblBestand.PP) AS PP, "
                        + "Sum(tblBestand.S) + Sum(tblBestand.SP) + Sum(tblBestand.SS) + Sum(tblBestand.SSP) + Sum(tblBestand.VZ) "
                        + "+Sum(tblBestand.VZP) + Sum(tblBestand.STN) + Sum(tblBestand.STH) + Sum(tblBestand.PP) AS Anzahl, "
                        + "0 as Gesamt ";
                    break;

                case enmReportTyp.KostenDoubletten:
                    sql = sql + "Sum(tblBestand.DS) AS S, Sum(tblBestand.DSP) AS SP, Sum(tblBestand.DSS) AS SS, Sum(tblBestand.DSSP) AS SSP, Sum(tblBestand.DVZ) AS VZ, "
                        + "Sum(tblBestand.DVZP) AS VZP, Sum(tblBestand.DSTN) AS STN, Sum(tblBestand.DSTH) AS STH, Sum(tblBestand.DPP) AS PP, "
                        + "Sum(tblBestand.DS) + Sum(tblBestand.DSP) + Sum(tblBestand.DSS) + Sum(tblBestand.DSSP) + Sum(tblBestand.DVZ) "
                        + "+Sum(tblBestand.DVZP) + Sum(tblBestand.DSTN) + Sum(tblBestand.DSTH) + Sum(tblBestand.PP) AS Anzahl, "
                      + "0 as Gesamt ";
                    break;
            }

            if (nation == 0)
            {
                sql = sql + "FROM ((((tblNation INNER JOIN tblKatalog ON tblNation.ID = tblKatalog.NationID) "
                    + "INNER JOIN tblAera ON tblAera.ID = tblKatalog.AeraID) "
                    + "INNER JOIN tblBestand ON tblKatalog.[Guid] = tblBestand.[GUID]) "
                    + "INNER JOIN tblSettings2 ON tblKatalog.NationID = tblSettings2.Lizenz) "
                    + "Left Join tblPreise ON tblKatalog.[Guid] = tblPreise.[GUID] "
                    + "GROUP BY tblNation.Bezeichnung, tblNation.ID "
                    + "HAVING (Sum(tblBestand.S)+Sum(tblBestand.SP)+Sum(tblBestand.SS)+Sum(tblBestand.SSP)"
                    + "+Sum(tblBestand.VZ)+Sum(tblBestand.VZP)+Sum(tblBestand.STN)+Sum(tblBestand.STH)+Sum(tblBestand.PP))<>0";
            }
            else
            {
                sql = sql + "FROM (((tblAera INNER JOIN tblKatalog ON tblAera.ID = tblKatalog.AeraID) "
                    + "LEFT JOIN tblPreise ON tblKatalog.[Guid] = tblPreise.[Guid]) "
                    + "LEFT JOIN tblBestand ON tblKatalog.[Guid] = tblBestand.[GUID]) "
                    + "INNER JOIN tblNation ON tblKatalog.NationID = tblNation.ID "
                    + "WHERE tblNation.ID=@nation "
                    + "GROUP BY tblAera.Bezeichnung, tblAera.ID ";
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("nation", nation);

            DataTable dt = GetDataTable(sql, parameters);

            if (reportTyp == enmReportTyp.KostenSammlung)
                dt = getKaufpreise(dt, nation, false);

            if (reportTyp == enmReportTyp.KostenDoubletten)
                dt = getKaufpreise(dt, nation, true);

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
                if (ConvertEx.ToDouble0(dt.Rows[i]["Gesamt"]) == 0)
                    dt.Rows[i].Delete();

            dt.AcceptChanges();

            return dt;
        }

        private DataTable getKaufpreise(DataTable dt, long nation, bool doublette)
        {
            string sql = "SELECT tblSammlung.[Guid], sum(tblSammlung.Kaufpreis) as Preis, tblKatalog.NationID, tblKatalog.AeraID "
                + "FROM tblKatalog INNER JOIN tblSammlung ON tblKatalog.[Guid] = tblSammlung.[Guid] ";

            if (nation != 0)
                sql = sql + "WHERE tblSammlung.Kaufpreis<>0 AND tblKatalog.NationID=@nation and Doublette=@doublette "
                    + "Group by tblKatalog.NationID, tblKatalog.AeraID,tblSammlung.[Guid] ";
            else
                sql = sql + "WHERE tblSammlung.Kaufpreis<>0 and Doublette=@doublette "
                    + "Group by tblKatalog.NationID, tblKatalog.AeraID,tblSammlung.[Guid] ";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("nation", nation);
            parameters.Add("doublette", doublette);

            DataTable d = GetDataTable(sql, parameters);

            string id;
            for (int i = 0; i < d.Rows.Count; i++)
            {
                id = d.Rows[i]["AeraID"].ToString();
                for (int j = 0; j < dt.Rows.Count; j++)
                    if (dt.Rows[j]["Gebiet"].ToString() == id)
                        dt.Rows[j]["Gesamt"] = ConvertEx.ToDouble0(dt.Rows[j]["Gesamt"]) + ConvertEx.ToDouble0(d.Rows[i]["Preis"]);
            }

            return dt;
        }

        public DataTable ReportingFehllisten(int nation, int ära, int region, decimal currentFactor, enmPreise preise)
        {
            string factor = currentFactor.ToString().Replace(",", ".");
            string sql = "SELECT Waehrung, Nominal, Jahrgang, Muenzzeichen as Muenzz, ";

            if (preise == enmPreise.EigenePreise)
                sql = sql + "iif(tblBestand.S is null, iif(tblPreise.SPreis<>0, tblPreise.SPreis,tblKatalog.SPreis)*{0}, null) AS SPreis, "
                  + "iif(tblBestand.SP is null, iif(tblPreise.SPPreis<>0, tblPreise.SPPreis,tblKatalog.SPPreis)*{0}, null) AS SPPreis, "
                  + "iif(tblBestand.SS is null, iif(tblPreise.SSPreis<>0, tblPreise.SSPreis,tblKatalog.SSPreis)*{0}, null) AS SSPreis, "
                  + "iif(tblBestand.SSP is null, iif(tblPreise.SSPPreis<>0, tblPreise.SSPPreis,tblKatalog.SSPPreis)*{0}, null) AS SSPPreis, "
                  + "iif(tblBestand.VZ is null, iif(tblPreise.VZPreis<>0, tblPreise.VZPreis,tblKatalog.VZPreis)*{0}, null) AS VZPreis, "
                  + "iif(tblBestand.VZP is null, iif(tblPreise.VZPPreis<>0, tblPreise.VZPPreis,tblKatalog.VZPPreis)*{0}, null) AS VZPPreis, "
                  + "iif(tblBestand.STN is null, iif(tblPreise.STNPreis<>0, tblPreise.STNPreis,tblKatalog.STNPreis)*{0}, null) AS STNPreis, "
                  + "iif(tblBestand.STH is null, iif(tblPreise.STHPreis<>0, tblPreise.STHPreis,tblKatalog.STHPreis)*{0}, null) AS STHPreis, "
                  + "iif(tblBestand.PP is null, iif(tblPreise.PPPreis<>0, tblPreise.PPPreis,tblKatalog.PPPreis)*{0}, null) AS PPPreis, "
                  + "'' as Bemerkung ";
            else
                sql = sql + "iif(tblBestand.S is null, tblKatalog.SPreis*{0}, null) AS SPreis, "
                  + "iif(tblBestand.SP is null, tblKatalog.SPPreis*{0}, null) AS SPPreis, "
                  + "iif(tblBestand.SS is null, tblKatalog.SSPreis*{0}, null) AS SSPreis, "
                  + "iif(tblBestand.SSP is null, tblKatalog.SSPPreis*{0}, null) AS SSPPreis, "
                  + "iif(tblBestand.VZ is null, tblKatalog.VZPreis*{0}, null) AS VZPreis, "
                  + "iif(tblBestand.VZP is null, tblKatalog.VZPPreis*{0}, null) AS VZPPreis, "
                  + "iif(tblBestand.STN is null, tblKatalog.STNPreis*{0}, null) AS STNPreis, "
                  + "iif(tblBestand.STH is null, tblKatalog.STHPreis*{0}, null) AS STHPreis, "
                  + "iif(tblBestand.PP is null, tblKatalog.PPPreis*{0}, null) AS PPPreis, "
                  + "'' as Bemerkung ";

            sql = sql + "FROM ((((tblKatalog Left JOIN tblBestand ON tblKatalog.[Guid] = tblBestand.[GUID]) "
              + "Left JOIN tblPreise ON tblKatalog.[Guid] = tblPreise.[GUID]) "
              + "INNER JOIN tblNation ON tblKatalog.NationID = tblNation.ID) "
              + "INNER JOIN tblAera ON tblKatalog.AeraID = tblAera.ID) "
              + "INNER JOIN tblGebiet ON tblKatalog.GebietID = tblGebiet.ID ";

            if (nation != 0)
                sql = sql + "WHERE tblKatalog.NationID=@nation";

            if (ära != 0)
                sql = sql + " AND tblKatalog.AeraID=@aera";

            if (region != 0)
                sql = sql + " and tblKatalog.GebietID=@region";

            sql = sql + " Order by Waehrung, Nominal, Jahrgang, Muenzzeichen";

            sql = string.Format(sql, factor);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("nation", nation);
            parameters.Add("aera", ära);
            parameters.Add("region", region);

            return GetDataTable(sql, parameters);
        }

        public void DeleteSammlung(int id)
        {
            string sql = "Delete from tblSammlung where id = @id";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("id", @id);
                command.ExecuteNonQuery();
            HasChanges = true;
            }

            closeConnetion();
        }

        public DataTable Reporting2(enmReportTyp reportTyp, long nation, long ära, long gebiet, decimal faktor)
        {
            string factor = faktor.ToString().Replace(",", ".");
            string sql = string.Empty;

            switch (reportTyp)
            {
                case enmReportTyp.ReportSammlung:
                    sql = "";

                    for (int i = 1; i <= 9; i++)
                        sql = ccc(i, "de", false, sql);
                    break;

                case enmReportTyp.ReportDoubletten:
                    sql = "";

                    for (int i = 1; i <= 9; i++)
                        sql = ccc(i, "de", true, sql);
                    break;
            }

            DataTable dt = GetDataTable(sql);
            dt.TableName = "tblReport";

            //if (Settings.Preise == enumPreise.Kaufpreise)
            //	dt = getReportingKaufpreise(dt, nation, database, false);

            //for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //	if (ConvertEx.ToDouble0(dt.Rows[i]["Gesamt"]) == 0)
            //		dt.Rows[i].Delete();
            //	else
            //		for (int c = 11; c < 29; c++)
            //			if (ConvertEx.ToDouble0(dt.Rows[i][c]) == 0)
            //				dt.Rows[i][c] = DBNull.Value;

            dt.AcceptChanges();

            return dt;
        }

        public string ccc(int i, string sprache, bool duplicate, string cmd)
        {
            //string cmd = string.Empty;

            string field = string.Empty;
            string eigen = string.Empty;

            switch (i)
            {
                case 1:
                    eigen = "SPreis";
                    field = "SPreis";
                    break;

                case 2:
                    eigen = "SPPreis";
                    field = "SPPreis";
                    break;

                case 3:
                    eigen = "SSPreis";
                    field = "SSPreis";
                    break;

                case 4:
                    eigen = "SSPPreis";
                    field = "SSPPreis";
                    break;

                case 5:
                    eigen = "VZPreis";
                    field = "VZPreis";
                    break;

                case 6:
                    eigen = "VZPPreis";
                    field = "VZPPreis";
                    break;

                case 7:
                    eigen = "STNPreis";
                    field = "STNPreis";
                    break;

                case 8:
                    eigen = "STHPreis";
                    field = "STHPreis";
                    break;

                case 9:
                    eigen = "PPPreis";
                    field = "PPPreis";
                    break;
            }

            if (i > 1)
                cmd = cmd + " Union ";

            cmd = cmd + "SELECT NationID, AeraID, GebietID, Waehrung, Nominal, Jahrgang, Muenzzeichen as Muenzz, tblSammlung.Erhaltung, tblErhaltungsgrad.Erhaltung, tblSammlung.[Guid], "
                + "Motiv, Count(Nominal) AS Anzahl, "
                + "iif (tblPreise.{4} is null or tblPreise.{4} = 0, tblKatalog.{2}, tblPreise.{4}) as Preis, "
                + "iif (tblPreise.{4} is null or tblPreise.{4} = 0, tblKatalog.{2}, tblPreise.{4}) * Count(Nominal) AS Gesamt "
                + "FROM ((tblSammlung INNER JOIN tblErhaltungsgrad ON tblSammlung.Erhaltung = tblErhaltungsgrad.id) "
                + "INNER JOIN tblKatalog ON tblSammlung.[Guid] = tblKatalog. [Guid] ) "
                + "LEFT JOIN tblPreise ON tblPreise. [Guid]  = tblKatalog. [Guid]  "
                + "WHERE tblSammlung.Erhaltung = {0} AND tblErhaltungsgrad.Sprache = '{1}' and Doublette={3} "
                + "Group by NationID, AeraID, GebietID, Waehrung, Nominal, Jahrgang, Muenzzeichen, tblSammlung.Erhaltung, tblSammlung. [Guid] , Motiv, "
                + "iif (tblPreise.{4} is null or tblPreise.{4} = 0, tblKatalog.{2}, tblPreise.{4}), tblErhaltungsgrad.Erhaltung ";

            cmd = string.Format(cmd, i, sprache, field, duplicate, eigen);
            return cmd;
        }

        //public DataTable RecordsetÄra(long id, string text)
        //{
        //    string cmd = "Select ID,Bezeichnung,Sortierung from ("
        //        +"SELECT 0 as ID, '" + text + "' as Bezeichnung, 0 as Sortierung FROM tblAera where ID =1 "
        //        + "union "
        //        + "SELECT distinct id, Bezeichnung, Sortierung FROM tblAera where NationID=" + id.ToString() 
        //        + ") ORDER BY Sortierung";

        //    return GetDataTable(cmd);
        //}

        //public DataTable RecordsetGebiet(long id, string text)
        //{
        //    string cmd = "Select ID,Bezeichnung,Sortierung from ("
        //        + "SELECT 0 as ID, '" + text + "' as Bezeichnung, 0 as Sortierung FROM tblGebiet where ID =1 "
        //        + "union "
        //        + "Select id, Bezeichnung, Sortierung from tblGebiet where AeraID =" + id.ToString() 
        //        + ") order by Sortierung";

        //    return GetDataTable(cmd);
        //}

        public DataTable RecordsetAll(string text)
        {
            string cmd = "SELECT 0 as ID, '" + text + "' as Bezeichnung FROM tblNation where ID =1 "
                + "union "
                + "SELECT tblNation.ID, tblNation.Bezeichnung FROM tblNation INNER JOIN tblSettings2 ON tblNation.ID = tblSettings2.Lizenz";

            return GetDataTable(cmd);
        }

        public Boolean Exists(string table, string where)
        {
            string sql = string.Format("Select Count(1) from {0} where {1}", table, where);
            bool result = false;

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                result = ConvertEx.ToInt32(command.ExecuteScalar()) == 0 ? false : true;
            }
            
            closeConnetion();

            return result;
        }

        public void SaveDownloads(int modul, string lizenz, string datum)
        {
            string sql = "";

            if (Exists("tblDownloads", "ID=" + modul.ToString()))
                sql = "Update tblDownloads set Datum=@Datum where id = @id";
            else
                sql = "insert into tblDownloads (id,lizenz,datum) values(@id,@lizenz,@Datum)";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("ID", modul);
                command.Parameters.AddWithValue("Lizenz", lizenz);
                command.Parameters.AddWithValue("Datum", datum);
                
             
                command.ExecuteNonQuery();
            HasChanges = true;
            }

            closeConnetion();
        }

        public List<Downloads> ReadModulDownloads()
        {
            List<Downloads> liste = new List<Downloads>();

            string sql = "Select tblSettings2.Lizenz, Jahr, Bezeichnung, iif(tblDownloads.Lizenz is not null, tblDownloads.Lizenz ,'') as OldLizenz, iif(Datum is not null, Datum,'') as Datum "
                + "from (tblSettings2 Left join tblNation on tblNation.id = tblSettings2.Lizenz) "
                + "left join tblDownloads on tblDownloads.id = tblSettings2.Lizenz";

            openConnection();

            using (OleDbCommand command = new OleDbCommand(sql, Connection))
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Downloads item = new Downloads();

                    item.Bezeichnung = reader["Bezeichnung"].ToString();
                    item.Datum = reader["Datum"].ToString();
                    item.Jahr = reader["Jahr"].ToString();
                    item.Lizenz = reader["Lizenz"].ToString();
                    item.OldLizenz = reader["OldLizenz"].ToString();

                    liste.Add(item);
                }
            }

            closeConnetion();

            return liste;
        }

        private void openConnection()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            openConnections++;
        }

        private void closeConnetion()
        {
            if (Connection.State == ConnectionState.Open && openConnections == 1)
                Connection.Close();

            if (openConnections > 0)
                openConnections--;
        }

        //public void xxx()
        //{
        //    DataTable dt = database.GetDataTable(cmd);
        //    dt.TableName = "tblReport";

        //    if (Settings.Preise == enmPreise.Kaufpreise)
        //        dt = getReportingKaufpreise(dt, nation, database, false);

        //    for (int i = dt.Rows.Count - 1; i >= 0; i--)
        //        if (ConvertEx.ToDouble0(dt.Rowsi]"Gesamt"]) == 0)
        //            dt.Rowsi].Delete();
        //        else
        //            for (int c = 11; c < 29; c++)
        //                if (ConvertEx.ToDouble0(dt.Rowsi]c]) == 0)
        //                    dt.Rowsi]c] = DBNull.Value;

        //    dt.AcceptChanges();

        //    return dt;
        //}
    }
}
