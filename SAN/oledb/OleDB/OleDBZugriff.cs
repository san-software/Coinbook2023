using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;
using SAN.Converter;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing;

namespace OleDB
{
	/// <summary>
	/// Summary description for OleDBZugriff.
	/// </summary>
	public class OleDBZugriff
	{
		protected DataRowCollection tableSchema;
		//protected string Tabelle;
		protected DataTable aktRecordset;
		protected DataRow aktRecord;
		protected int columnCount = 0;

		protected DataSet liste;
		protected int datenbank = 0;		// Datenbanknummer, default = 0
		protected string lockMeldung = "";
		protected string lockMeldungShort = "";

		protected ArrayList index = new ArrayList();

		private DBConvert dbConvert = new DBConvert();
		private DBBinding dbBinding;

		public OleDBZugriff()
		{
			datenbank = 0;
		}

		public OleDBZugriff(int datenbank)
		{
			//if (datenbank >= 0 && datenbank < ((DBConnection)OleDBConnection.Connections[datenbank]).DBAnzahl)
				this.datenbank = datenbank;
		}

		public void Init()
		{
			getTableSchema();
		}

		public void Init(string id)
		{
			index.Add(id);
			getTableSchema();
			getDummyRecord(id);
		}

		public void Init(string id1, string id2)
		{
			index.Add(id1);
			index.Add(id2);
			getTableSchema();
			getDummyRecord();
		}

		#region Properties

		public DataTable Liste
		{
			get
			{
				return liste.Tables[0];
			}
		}

		public DataSet DataSetListe
		{
			get
			{
				return liste;
			}
		}

		public string Key { get; set; }

		public string Tabelle {get;set;}

		#endregion

	#region Einzelwerte ohne Transaction
		public object GetObject(string cmdString)
		{
			return ((DBConnection)OleDBConnection.Connections[datenbank]).GetObject(cmdString);
		}

		#region Max
		public object Max(string feldName)
		{
			return GetObject("Select Max(" + feldName + ") from " + Tabelle);
		}

		public object Max(string feldName, string where)
		{
			return GetObject("Select Max(" + feldName + ") from " + Tabelle + " WHERE " + where);
		}

		public object Max(string Tabelle, string feldName, string where)
		{
			if (where != "")
				return GetObject("Select Max(" + feldName + ") from " + Tabelle + " WHERE " + where);
			else
				return GetObject("Select Max(" + feldName + ") from " + Tabelle);
		}
		#endregion

		#region Min
		public object Min(string feldName)
		{
			return GetObject("Select Min(" + feldName + ") from " + Tabelle);
		}

		public object Min(string feldName, string where)
		{
			return GetObject("Select Min(" + feldName + ") from " + Tabelle + " WHERE " + where);
		}

		public object Min(string Tabelle, string feldName, string where)
		{
			if (where != "")
				return GetObject("Select Min(" + feldName + ") from " + Tabelle + " WHERE " + where);
			else
				return GetObject("Select Min(" + feldName + ") from " + Tabelle);
		}
		#endregion

		#region Match
		public bool Match(long id)
		{
			return match(id.ToString());
		}

		public bool Match(string id)
		{
			return match("'" + id + "'");
		}

		private bool match(string id)
		{
			Int32 anzahl = (Int32)GetObject("Select COUNT(*) from " + Tabelle + " WHERE " + index[0].ToString() + "=" + id);
			return (anzahl == 1);
		}

		public bool Match(string Tabelle, string filter)
		{
			Int32 anzahl = (Int32)GetObject("Select COUNT(*) from " + Tabelle + " WHERE " + filter);
			return (anzahl == 1);
		}
		#endregion

		#region Count
		public long Count(string feldName, string where)
		{
			return ConvertEx.ToInt64(GetObject("Select Count(" + feldName + ") from " + Tabelle + " WHERE " + where));
		}

		public long Count(string where)
		{
			return ConvertEx.ToInt64(GetObject(where));
		}

		public long Count(string table, string feldName, string where)
		{
			return ConvertEx.ToInt64(GetObject("Select Count(" + feldName + ") from " + table + " WHERE " + where));
		}
		#endregion

		#region Summe
		public long Summe(string feldName, string where)
		{
			return ConvertEx.ToInt64(GetObject("Select SUM(" + feldName + ") from " + Tabelle + " WHERE " + where));
		}

		public long Summe(string feldName)
		{
			return ConvertEx.ToInt64(GetObject("Select SUM(" + feldName + ") from " + Tabelle));
		}

		public long Summe(string table, string feldName, string where)
		{
			return ConvertEx.ToInt64(GetObject("Select SUM(" + feldName + ") from " + table + " WHERE " + where));
		}
		#endregion
		
		#region Value
		public string Value(long id, string feld)
		{
			return Value("Select " + feld + " from " + Tabelle + " WHERE " + index[0].ToString() + "=" + id);
		}

		public string Value(string cmdString)
		{
			object temp;
			string result;

			temp = GetObject(cmdString);

			if (temp == System.DBNull.Value)
				result = "";
			else if (temp == null)
				result = "";
			else
				result = temp.ToString();

			return result;
		}
		#endregion

		public object FindID(string Tabelle, string filter)
		{
			return GetObject("Select ID from " + Tabelle + " WHERE " + filter);
		}

		public bool Exists(string feld, string table)
		{
			long result = 0;
			int pos = feld.IndexOf("=");

			if (pos != 0)
			{
				string feldname = feld.Substring(0, pos).Trim();
				result = ConvertEx.ToInt64(GetObject("select count(" + feldname + ") from " + table + " where " + feld));
			}
			else
				MessageBox.Show("Fehler in der Feldbezeichnung");

			return (result != 0);
		}

    public bool FieldExists(string tableName, string fieldName)
    {
      bool result=false;

      fieldName = fieldName.ToLower();

      string cmd = "Select * from " + tableName + " where id = -1";
      DataTable dt = GetDataTable(cmd); 
   
      for (int i = 0; i< dt.Columns.Count; i++)
        if (dt.Columns[i].ColumnName.ToLower() == fieldName)
        {
          result=true;
          break;
        }

      return result;    
    }

		#region Text
		public string Text(string cmd)
		{
			return Convert.ToString(GetObject(cmd));
		}

		public string Text(string feld, string tabelle, string filter)
		{
			return Convert.ToString(GetObject("Select " + feld + " from " + tabelle + " where " + filter));
		}

		public string Text(string feld, string tabelle)
		{
			return Convert.ToString(GetObject("Select " + feld + " from " + tabelle));
		}
		#endregion
	#endregion

		#region Hilfsroutinen
		public void SetLong(string name, long wert)
		{
			aktRecord[name] = wert;
		}

		public void SetInt(string name, int wert)
		{
			aktRecord[name] = wert;
		}

		public void SetShort(string name, short wert)
		{
			aktRecord[name] = wert;
		}

		public void SetDouble(string name, double wert)
		{
			if (!Double.IsNaN(wert))
				aktRecord[name] = wert;
			else
				aktRecord[name] = DBNull.Value;
		}

		public void SetSingle(string name, Single wert)
		{
			if (!Single.IsNaN(wert))
				aktRecord[name] = wert;
			else
				aktRecord[name] = DBNull.Value;
		}

		public void SetString(string name, string wert)
		{
			try
			{
				aktRecord[name] = wert;
			}
			catch { }
		}

		public void SetDate(string name, DateTime wert)
		{

			aktRecord[name] = wert;
		}

		public void SetDate2(string name, object wert)
		{
			DateTime result;

			if (wert == null || wert == System.DBNull.Value)
				aktRecord[name] = System.DBNull.Value;
			else if (DateTime.TryParse(wert.ToString(), out result))
				aktRecord[name] = result;
			else
				aktRecord[name] = System.DBNull.Value;

		}

		public void SetNull(string name, bool wert)
		{
			aktRecord[name] = System.DBNull.Value;
		}

		public void SetBool(string name, bool wert)
		{
			aktRecord[name] = wert;
		}

		public string GetString(string name)
		{
			return aktRecord[name].ToString();
		}

		public short GetShort(string name)
		{
			return Convert.ToInt16(aktRecord[name]);
		}

		public double GetDouble(string name)
		{
			return ConvertEx.ToDouble(aktRecord[name]);
		}

		public Single GetSingle(string name)
		{
			return ConvertEx.ToSingle(aktRecord[name]);
		}

		public long GetLong(string name)
		{
			if (aktRecord[name] == DBNull.Value)
				return 0;
			else
				return Convert.ToInt64(aktRecord[name]);
		}

		public int GetInt(string name)
		{
			if (aktRecord[name] == DBNull.Value)
				return 0;
			else
				return Convert.ToInt32(aktRecord[name]);
		}

		public long GetID(string name)
		{
			if (aktRecord[name] == DBNull.Value)
				return -1;
			else
				return Convert.ToInt64(aktRecord[name]);
		}

		public DateTime GetDate(string name)
		{
			if (aktRecord[name] == DBNull.Value)
				return Convert.ToDateTime(null);
			else
				return Convert.ToDateTime(aktRecord[name]);
		}

		public object GetDate2(string name)
		{
			if (aktRecord[name] == DBNull.Value)
				return null;
			else
				return Convert.ToDateTime(aktRecord[name]);
		}

		public bool GetIsNull(string name)
		{
			return (aktRecord[name] == DBNull.Value);
		}

		public bool GetBool(string name)
		{
			if (aktRecord[name] == DBNull.Value)
				return false;
			else
				return ConvertEx.ToBoolean(aktRecord[name]);
		}

		private string getDBString(object counter)
		{
			string result = "";
			string index;

			if (counter.GetType().FullName == "System.Int32")
				index = aktRecordset.Columns[Convert.ToInt32(counter)].ColumnName;
			else
				index = counter.ToString();

			switch (aktRecordset.Columns[index].DataType.ToString())
			{
				case "System.String":
					result = dbConvert.DBString(aktRecord[index]);
					break;

				case "System.Int16":
				case "System.Int32":
				case "System.Int64":
				case "System.Decimal":
				case "System.Double":
				case "System.Single":
					result = dbConvert.DBNumber(aktRecord[index]).Replace(",", ".");
					break;

				case "System.DateTime":
					result = dbConvert.DBDateTime(DatenbankTyp, aktRecord[index]);
					break;

				case "System.Boolean":
					result = dbConvert.DBBool(DatenbankTyp, aktRecord[index]);
					break;

				default:
					result = dbConvert.DBString(aktRecord[index]);
					break;
				//				case (int)OleDbType.BigInt:
				//				case (int)OleDbType.Integer:
				//				case (int)OleDbType.SmallInt:
				//				case (int)OleDbType.TinyInt:
				//					cmdString = cmdString + DBNumber(tabellenStruktur[counter].m_FeldValueLong) + ",";
				//					break;
				//				case (int)OleDbType.Currency:
				//				case (int)OleDbType.Numeric:
				//				case (int)OleDbType.Single:
				//					cmdString = cmdString + DBNumber(tabellenStruktur[counter].m_FeldValueDouble) + ",";
				//					break;
			}
			return result;
		}

		public int Length(int col)
		{
			return Length(aktRecordset.Columns[col].ColumnName);
		}

		public int Length(string name)
		{
			int result = 0;
			string datatype ="";
			int columnsize = 0;

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLite:
					for (int row = 0; row < tableSchema.Count; row++)
					{
						if (name.ToLower() == tableSchema[row]["ColumnName"].ToString().ToLower())
						{
							datatype = tableSchema[row]["DataType"].ToString();
							columnsize = Convert.ToInt32(tableSchema[row]["ColumnSize"]);
							break;
						}
					}

					switch (datatype)
					{
						case "System.Int32":
						case "System.Int16":
							result = 6;
							break;
						case "System.DateTime":
							result = 10;
							break;
						case "System.Boolean":
							result = 1;
							break;
						default:
							result = columnsize;
							break;
					}
					break;

				default:
					switch (aktRecordset.Columns[name].DataType.ToString())
					{
						case "System.Int32":
						case "System.Int16":
							result = 6;
							break;
						case "System.DateTime":
							result = 10;
							break;
						case "System.Boolean":
							result = 1;
							break;
						default:
							result = 0;
							for (int row = 0; row < tableSchema.Count; row++)
							{
								if (name.ToLower() == tableSchema[row]["ColumnName"].ToString().ToLower())
								{
									result = Convert.ToInt32(tableSchema[row]["ColumnSize"]);
									break;
								}
							}
							break;
					}
					break;
			}
			return result;
		}

		public void AddDataRelation(DataSet dataSet, string name, int table1, string col1, int table2, string col2)
		{
			DataRelation rel;
			rel = new DataRelation(name, dataSet.Tables[table1].Columns[col1], dataSet.Tables[table2].Columns[col2]);
			dataSet.Relations.Add(rel);
		}

		#endregion

		#region Datasets und Tables ohne Transaction

		public DataSet GetDataset()
		{
			string cmdString = "Select * from " + Tabelle;
			return ((DBConnection)OleDBConnection.Connections[datenbank]).GetDataset(cmdString, Tabelle);
		}

		public DataSet GetDataset(string cmdString)
		{
			return ((DBConnection)OleDBConnection.Connections[datenbank]).GetDataset(cmdString, Tabelle);
		}

		public DataRow GetDataRow(string cmdString)
		{
			return ((DBConnection)OleDBConnection.Connections[datenbank]).GetDataRow(cmdString);
		}

    public void WriteDataRow(DataRow row, bool isNew)
    {
      ((DBConnection)OleDBConnection.Connections[datenbank]).WriteDataRow(row,isNew);
    }

		public DataTable GetDataTable(string cmdString)
		{
			return ((DBConnection)OleDBConnection.Connections[datenbank]).GetDataTable(cmdString);
		}

		public DataTable GetDataTable()
		{
			return aktRecordset;
		}

		public bool GetRecord(long id)
		{
			return getRecord(id.ToString());
		}

		public bool GetRecord(string id)
		{
			if (id.IndexOf("=") == -1)
				return getRecord("'" + id + "'");
			else
				return getRecord(id);
		}

		public bool GetRecord(string field,string id)
		{
			string cmdString;
			bool result;

			cmdString = "Select * from " + Tabelle + " WHERE " + field + "='" + id + "'";

			aktRecordset = GetDataTable(cmdString);
			aktRecordset.AcceptChanges();
			result = (aktRecordset.Rows.Count != 0);
			if (!result)
			{
				aktRecord = aktRecordset.NewRow();
				for (int i = 0; i < aktRecordset.Columns.Count; i++)
				{
					if (aktRecordset.Columns[i].ColumnName.ToLower() != field.ToLower())
					{
						switch (aktRecordset.Columns[i].DataType.ToString())
						{
							case "System.Int32":
							case "System.Int16":
							case "System.Int64":
							case "System.Single":
							case "System.Double":
							case "System.Decimal":
								aktRecord[i] = 0;
								break;

							case "System.String":
								aktRecord[i] = String.Empty;
								break;
						}
					}
				}
			}
			else
				aktRecord = aktRecordset.Rows[0];

			return result;
		}

		private void getDummyRecord()
		{
			if (tableSchema[0]["DataType"].ToString() == "System.String")
				getRecord("''");
			else
				getRecord("-1");
		}

		private void getDummyRecord(string field)
		{
      try
      {
        for (int i = 0; i < tableSchema.Count; i++)
        {
          if (tableSchema[i]["ColumnName"].ToString().ToLower() == field.ToLower())
          {
            if (tableSchema[i]["DataType"].ToString() == "System.String")
              getRecord("''");
            else
              getRecord("-1");
          }
        }
      }
      catch { }
		}

		private bool getRecord(string id)
		{
			string cmdString;
			bool result;
			
			int pos = id.IndexOf("=");

			if (pos == -1)
				cmdString = "Select * from " + Tabelle + " WHERE " + index[0].ToString() + "=" + id;
			else
				cmdString = "Select * from " + Tabelle + " WHERE " + id;

			aktRecordset = GetDataTable(cmdString);
			aktRecordset.AcceptChanges();
			result = (aktRecordset.Rows.Count != 0);
			if (!result)
				aktRecord = aktRecordset.NewRow();
			else
				aktRecord = aktRecordset.Rows[0];

			return result;
		}

		public bool FindRecord(string where)
		{
			string cmdString;
			bool result;

			cmdString = "Select * from " + Tabelle + " WHERE " + where;

			aktRecordset = GetDataTable(cmdString);
			aktRecordset.AcceptChanges();
			result = (aktRecordset.Rows.Count != 0);
			if (!result)
				aktRecord = aktRecordset.NewRow();
			else
				aktRecord = aktRecordset.Rows[0];

			return result;
		}

		public bool GetFirstRecord(string id)
		{
			return getRecord(id);
		}

		public bool GetRecord()
		{
			string cmdString;
			bool result;

			cmdString = "Select * from " + Tabelle;

			aktRecordset = GetDataTable(cmdString);
			aktRecordset.AcceptChanges();
			result = (aktRecordset.Rows.Count != 0);
			if (!result)
				aktRecord = aktRecordset.NewRow();
			else
				aktRecord = aktRecordset.Rows[0];

			return result;
		}

		public string[] getFeld(string feld, string tabelle, string filter)
		{
			DataSet ds = GetDataset("Select Distinct " + feld + " from " + tabelle + " where " + filter);
			int count = ds.Tables[0].Rows.Count;
			string[] result = new string[count];

			for (int counter = 0; counter < ds.Tables[0].Rows.Count; counter++)
				result[counter] = ds.Tables[0].Rows[counter][0].ToString();

			return result;
		}

		#endregion

		#region Executes mit Transaction

		public void Insert()
		{
			string cmdString;

			cmdString = "Insert into " + Tabelle + " (";

			for (int counter = 0; counter < aktRecordset.Columns.Count; counter++)
				cmdString = cmdString + "[" + aktRecordset.Columns[counter].ColumnName + "]" + ",";

			cmdString = cmdString.Substring(0, cmdString.Length - 1) + ") Values(";

			for (int counter = 0; counter < aktRecordset.Columns.Count; counter++)
				cmdString = cmdString + getDBString(counter) + ",";

			cmdString = cmdString.Substring(0, cmdString.Length - 1) + ")";
			Execute(cmdString);
		}

		public void Update()
		{
			string cmdString;

			cmdString = "Update " + Tabelle + " set ";

			for (int counter = 0; counter < aktRecordset.Columns.Count; counter++)
				cmdString = cmdString + "[" + aktRecordset.Columns[counter].ColumnName + "]" + "=" + getDBString(counter) + ",";

			cmdString = cmdString.Substring(0, cmdString.Length - 1) + " WHERE ";

			for (int counter2 = 0; counter2 < index.Count; counter2++)
			{
				cmdString = cmdString + index[counter2].ToString() + "=" + getDBString(index[counter2].ToString());
				if (counter2 < index.Count - 1)
					cmdString = cmdString + " AND ";
			}

			Execute(cmdString);
		}

		public void Delete(string feld)
		{
			Execute("Delete FROM " + Tabelle + " WHERE " + feld + "=" + getDBString(feld));
		}

		public void Delete()
		{
			string cmdString;

			cmdString = "Delete FROM " + Tabelle + " WHERE ";

			for (int counter2 = 0; counter2 < index.Count; counter2++)
			{
				cmdString = cmdString + index[counter2].ToString() + "=" + getDBString(index[counter2].ToString());
				if (counter2 < index.Count - 1)
					cmdString = cmdString + " AND ";
			}

			Execute(cmdString);
		}

		public int Execute(string cmdString)
		{
			int result = 0;

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					result = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).Execute(cmdString);
					break;

				case Databases.MSAccess:
					result = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).Execute(cmdString);
					break;

				case Databases.Oracle:
					result = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).Execute(cmdString);
					break;

				//case Databases.SQLite:
				//  result = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).Execute(cmdString);
				//  break;
			}

			OleDBConnection.SyncronizeWrite(cmdString);

			return result;
		}

		public int ExecuteSync(string cmdString)
		{
			int result = 0;

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					result = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).Execute(cmdString);
					break;

				case Databases.MSAccess:
					result = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).Execute(cmdString);
					break;

				case Databases.Oracle:
					result = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).Execute(cmdString);
					break;

				//case Databases.SQLite:
				//  result = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).Execute(cmdString);
				//  break;
			}

			return result;
		}

		public void Update(string feld, string tabelle, string text)
		{
			string cmdString;

			cmdString = "update " + tabelle + " set " + feld + "='" + text + "'";
			Execute(cmdString);
		}

		public void Update(string feld, string tabelle, long text)
		{
			string cmdString;

			cmdString = "update " + tabelle + " set " + feld + "=" + text.ToString();
			Execute(cmdString);
		}

		#endregion

		private void getTableSchema()
		{
			string cmdString;

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					cmdString = "Select * from " + addSchemaToTable(Tabelle);
					tableSchema = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdString);
					break;

				case Databases.MSAccess:
					cmdString = "Select * from " + addSchemaToTable(Tabelle);
					tableSchema = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdString);
					break;

				//case Databases.SQLite:
				//  cmdString = "Select * from sysTableSchema where [Tabelle]='" + Tabelle + "'";
				//  tableSchema = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).GetDataTable(cmdString).Rows;
				//  break;
			}

      if (tableSchema != null)
			  columnCount = tableSchema.Count;
		}

		public DataRowCollection GetTableSchema(string Tabelle)
		{
				string cmdString;
				DataRowCollection result = null;

				switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
				{
					case Databases.SQLServer:
						cmdString = "Select * from " + addSchemaToTable(Tabelle);
						result = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdString);
						break;

					case Databases.MSAccess:
						cmdString = "Select * from " + addSchemaToTable(Tabelle);
						result = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdString);
						break;

					//case Databases.SQLite:
					//  cmdString = "Select * from sysTableSchema where [Tabelle]='" + Tabelle + "'";
					//  result = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdString);
					//  break;
				}

				return result;
		}

		public int ColumnCount
		{
			get
			{
				return columnCount;
			}
		}

		public DataRowCollection TableSchema
		{
			get
			{
				return tableSchema;
			}
		}

		#region Blob
		public long SaveBlob(string Tabelle, long id, byte[] bytefeld)
		{
			string action;

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					id = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).SaveBlob(Tabelle, id, bytefeld);
					break;

				case Databases.MSAccess:
					id = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).SaveBlob(Tabelle, id, bytefeld);
					break;

				case Databases.Oracle:
					id = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).SaveBlob(Tabelle, id, bytefeld);
					break;

				//case Databases.SQLite:
				//  id = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).SaveBlob(Tabelle, id, bytefeld);
				//  break;
			}

			return id;
		}

		public byte[] GetBlob(string Tabelle, long id)
		{
			long bytes = Convert.ToInt64(Value("Select Length from " +Tabelle	 + " where id = " + id.ToString()));

			string cmdString = "Select Objekt from " + addSchemaToTable(Tabelle) + " where id=" + id.ToString();

			return ((DBConnection)OleDBConnection.Connections[datenbank]).getBlob(cmdString, bytes);
		}

		public long SaveImage(string Tabelle, long id, Image image)
		{
			MemoryStream memStream1 = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(memStream1, image);
			byte[] bytefeld = memStream1.GetBuffer();
			memStream1.Close();

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					id = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).SaveBlob(Tabelle, id, bytefeld);
					break;

				case Databases.MSAccess:
					id = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).SaveBlob(Tabelle, id, bytefeld);
					break;

				case Databases.Oracle:
					id = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).SaveBlob(Tabelle, id, bytefeld);
					break;

				//case Databases.SQLite:
				//  id = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).SaveBlob(Tabelle, id, bytefeld);
				//  break;
			}

			return id;
		}

		public Image LoadImage(string Tabelle, long id)
		{
			string cmdString = "Select Length,Objekt from " + addSchemaToTable(Tabelle) + " where id=" + id.ToString();
			byte[] bytefeld = null;

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					bytefeld = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).LoadImage(cmdString);
					break;

				case Databases.MSAccess:
					bytefeld = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).LoadImage(cmdString);
					break;

				case Databases.Oracle:
					bytefeld = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).LoadImage(cmdString);
					break;

				//case Databases.SQLite:
				//  bytefeld = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).LoadImage(cmdString);
				//  break;
			}

			Image image = null;

			if (bytefeld != null)
			{
				MemoryStream memStream2 = new MemoryStream(bytefeld);
				BinaryFormatter formatter = new BinaryFormatter();
				image = (Image)formatter.Deserialize(memStream2);
			}

			return image;
		}

		public void DeleteBlob(string Tabelle, int id)
		{
			string cmdString = "Delete from " + addSchemaToTable(Tabelle) + " where id=" + id.ToString();
			((DBConnection)OleDBConnection.Connections[datenbank]).DeleteBlob(cmdString);
		}

		#endregion

		#region Sequence
		public long Sequence()
		{
			return sequence("Select Max(" + index[0].ToString() + ") from " + addSchemaToTable(Tabelle));
		}

		public long Sequence(string id)
		{
			string cmdString = "Select Max(" + index[0].ToString() + ") from " + addSchemaToTable(Tabelle) + " WHERE " + index[1].ToString() + "='" + id + "'";

			return sequence(cmdString);
		}
		private long sequence(string cmdString)
		{
			long result = -1;

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					result = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).Sequence(cmdString);
					break;

				case Databases.MSAccess:
					result = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).Sequence(cmdString);
					break;

				case Databases.Oracle:
					result = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).Sequence(cmdString);
					break;

				//case Databases.SQLite:
				//  result = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).Sequence(cmdString);
				//  break;
			}
			return result;
		}
		#endregion

		public void AddIndex(string id)
		{
			index.Add(id);
		}

		public object Find(System.Collections.SortedList liste)
		{
			bool first = true;
			string cmdString = "Select " + index[0].ToString() + " from " + addSchemaToTable(Tabelle);
			foreach (string key in liste.Keys)
			{
				if (first)
				{
					cmdString = cmdString + " WHERE " + key + " = '" + liste[key] + "'";
					first = false;
				}
				else
					cmdString = cmdString + " AND " + key + " = '" + liste[key] + "'";
			}

			return ((DBConnection)OleDBConnection.Connections[datenbank]).Find(cmdString);
		}

		public bool Lock(string tabelle, string id)
		{
			string cmdString;
			bool result = false;

			cmdString = "Select * from tblLock WHERE tabelle = '" + tabelle + "' and id = '" + id + "'";

			try
			{
				DataRowCollection rows = GetDataTable(cmdString).Rows;
				if (rows.Count != 0)
				{
					if (rows[0]["username"].ToString() == Environment.UserName && rows[0]["workstation"].ToString() == Environment.MachineName)
					{
						UnLock(tabelle, id);
						rows.RemoveAt(0);
					}
				}

				if (rows.Count == 0)
				{
					cmdString = "Insert into tblLock (Tabelle,id,username,datum, workstation) values('" + tabelle + "','" + id + "','"
						+ Environment.UserName + "'," + dbConvert.DBDateTime(DatenbankTyp, System.DateTime.Now) + ",'" + Environment.MachineName + "')";
					((DBConnection)OleDBConnection.Connections[datenbank]).OpenTransaction();
					Execute(cmdString);
					((DBConnection)OleDBConnection.Connections[datenbank]).Commit();
					result = false;
				}
				else
				{
					lockMeldungShort = "Die gewählten Daten können nicht bearbeitet werden, da sie zur Zeit von einem anderen User benutzt werden.";
					lockMeldung = "Die gewählten Daten können nicht bearbeitet werden, da sie zur Zeit von einem anderen User benutzt werden.\n\n "
						+ "Gesperrt durch: " + rows[0]["username"].ToString()
						+ " am PC " + rows[0]["workstation"].ToString()
						+ " seit " + rows[0]["datum"].ToString();

					MessageBox.Show(lockMeldung, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					result = true;
				}
			}
			catch
			{
				throw;
			}

			return result;
		}

		public void UnLock(string tabelle, string id)
		{
			string cmdString;

			cmdString = "Delete from tblLock WHERE tabelle = '" + tabelle + "' and id = '" + id + "' and username ='" + Environment.UserName + "' and workstation = '" + Environment.MachineName + "'";
			((DBConnection)OleDBConnection.Connections[datenbank]).OpenTransaction();
			Execute(cmdString);
			((DBConnection)OleDBConnection.Connections[datenbank]).Commit();
		}

		public void UnLockAll()
		{
			string cmdString;

			cmdString = "Delete from tblLock WHERE username ='" + Environment.UserName + "' and workstation = '" + Environment.MachineName + "'";
			((DBConnection)OleDBConnection.Connections[datenbank]).OpenTransaction();
			Execute(cmdString);
			((DBConnection)OleDBConnection.Connections[datenbank]).Commit();
		}

		public void OpenTransaction()
		{
			((DBConnection)OleDBConnection.Connections[datenbank]).OpenTransaction();
		}

		public void Commit()
		{
			((DBConnection)OleDBConnection.Connections[datenbank]).Commit();
		}

		private string addSchema(string text)
		{
			string schema = ((DBConnection)OleDBConnection.Connections[datenbank]).Schema;

			//if (dbConnection.DBConnection(datenbank).DatenbankTyp != Databases.MSAccess)
			//{
			//  text = text.Replace("&", "+");
			//  text = text.Replace("true", "1");
			//  text = text.Replace("false", "0");
			//}

			if (schema != "" && schema != null)
			{
				text = text.Replace("tbl", schema + ".tbl");
				text = text.Replace("view", schema + ".view");
			}
			return text;
		}

		private string addSchemaToTable(string text)
		{
			string schema = ((DBConnection)OleDBConnection.Connections[datenbank]).Schema;

			//if (dbConnection.DBConnection(datenbank).DatenbankTyp != Databases.MSAccess)
			//{
			//  text = text.Replace("&", "+");
			//  text = text.Replace("true", "1");
			//  text = text.Replace("false", "0");
			//}

			if (schema != "" && schema != null)
				return schema + "." + text;
			else
				return text;
		}

		public string LockMeldung
		{
			get
			{
				return lockMeldung;
			}
		}

		public string LockMeldungShort
		{
			get
			{
				return lockMeldungShort;
			}
		}

		public Columninfo ColumnInfo(int col)
		{
			Columninfo info = new Columninfo(col, tableSchema);
			return info;
		}

		public Columninfo ColumnInfo(string col)
		{
			Columninfo info = new Columninfo(aktRecord.Table.Columns[col].Ordinal, tableSchema);
			return info;
		}

		public void Put(string feld, object value)
		{
			switch (ColumnInfo(feld).DataType)
			{
				case "System.String":
					SetString(feld, value.ToString());
					break;

				case "System.Int64":
					SetLong(feld, ConvertEx.ToInt64(value));
					break;

				case "System.Int32":
					SetLong(feld, ConvertEx.ToInt64(value));
					break;

				case "System.Int16":
					SetShort(feld, ConvertEx.ToInt16(value));
					break;

				case "System.Boolean":
					SetBool(feld, ConvertEx.ToBoolean(value));
					break;

				case "System.Double":
				case "System.Decimal":
					SetDouble(feld, ConvertEx.ToDouble(value));
					break;

				case "System.Single":
					SetSingle(feld, ConvertEx.ToSingle(value));
					break;

				case "System.DateTime":
					if (value == null || value == DBNull.Value)
						SetNull(feld, true);
					else
						SetDate(feld, Convert.ToDateTime(value));
					break;

			}
		}

		public bool SetRecord(int id)
		{
			int r = -1;
			string feld = index[0].ToString();
			for (int i = 0; i < aktRecordset.Rows.Count; i++)
			{
				if (id == Convert.ToInt32(aktRecordset.Rows[i][feld]))
				{
					r = i;
					break;
				}
			}

			if (r != -1)
			{
				aktRecord = aktRecordset.Rows[r];
				return true;
			}
			else
				return false;
		}

		public bool SetRecord(string id)
		{
			int r = -1;
			string feld = index[0].ToString();
			for (int i = 0; i < aktRecordset.Rows.Count; i++)
			{
				if (id == aktRecordset.Rows[i][feld].ToString())
				{
					r = i;
					break;
				}
			}

			if (r != -1)
			{
				aktRecord = aktRecordset.Rows[r];
				return true;
			}
			else
				return false;
		}

		public Databases DatenbankTyp
		{
			get
			{
				return ((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp;
			}
		}

		public void UpdateAll()
		{
			string cmdString;

			cmdString = "Update " + Tabelle + " set ";

			for (int counter = 0; counter < columnCount; counter++)
				cmdString = cmdString + "[" + aktRecordset.Columns[counter].ColumnName + "]" + "=" + getDBString(counter) + ",";

			cmdString = cmdString.Substring(0, cmdString.Length - 1);

			Execute(cmdString);
		}

		public bool ExistTable(string table)
		{
			bool result = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).ExistTable(table);

			return result;
		}

		public long SessionID
		{
			get
			{
				return ConvertEx.ToInt64(Value("select userenv('SESSIONID') SESSIONID from SYS.Dual"));
			}
		}

		public long NEXTVAL(string sequencename)
		{
			return ConvertEx.ToInt64(Value("Select DLT." + sequencename + ".NEXTVAL VAL from SYS.DUAL"));
		}

		public object[] CallProcedure(string cmd, ArrayList parameter, string[] returnParameter)
		{
			return ((DBConnection)OleDBConnection.Connections[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		}

		public object CallProcedure(string cmd, ArrayList parameter, string returnParameter)
		{
			return ((DBConnection)OleDBConnection.Connections[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		}

		public void CallProcedure(string cmd, ArrayList parameter)
		{
			((DBConnection)OleDBConnection.Connections[datenbank]).CallProcedure(cmd, parameter);
		}

    public void MergeDataTables(string cmd, DataTable dtNew)
    {
      switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
      {
        case Databases.SQLServer:
          ((DBConnection)OleDBConnection.Connections[datenbank]).MergeDataTables(cmd, dtNew);
          break;

        case Databases.MSAccess:
          ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).MergeDataTables(cmd, dtNew);
          break;

        case Databases.Oracle:
         ((DBConnection)OleDBConnection.Connections[datenbank]).MergeDataTables(cmd, dtNew);
          break;

        //case Databases.SQLite:
        //  result = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).Execute(cmdString);
        //  break;
      }

      //OleDBConnection.SyncronizeWrite(cmdString);

    }
#region Binding
		public DBBinding Binding
		{
			get
			{
				if (dbBinding == null)
				{
					if (String.IsNullOrEmpty(Key))
						Key=Tabelle;

					dbBinding = new DBBinding();
					dbBinding.Key = Key;
					dbBinding.Tabelle = Tabelle;
				}
				return dbBinding;
			}
		}
#endregion

    public void CompactAccessDB()
    {
      ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).CompactAccessDB();
    }

    public string ConnectionString
    {
      get
      {
        return ((DBConnection)OleDBConnection.Connections[datenbank]).ConnectionString;
      }
    }

  
	}
}