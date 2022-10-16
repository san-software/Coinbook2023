namespace OleDB
{
	using System;
	using System.Text;
	using System.Data;
	using System.Data.OleDb;
	using System.Configuration;
	using System.Xml;
	using System.Windows.Forms;
	using System.IO;
	using System.Runtime.Serialization.Formatters.Binary;
	using System.Collections;
	using System.Data.SqlClient;

	/// <summary>
	/// Summary description for OleDBZugriff.
	/// </summary>
	public class DBConnection
	{
		#region Connections

		internal virtual void CloseConnection()
		{
		}

		private void OnInfoMessage(object sender, OleDbInfoMessageEventArgs e)
		{
			throw new Exception(e.Message);
		}

		public string Schema { get; set; }
		public string Host { get; set; }
		public string Provider { get; set; }
		public string DataSource { get; set; }
		public string User { get; set; }
		public string Datapath { get; set; }
		public string DataBase { get; set; }
		public string Password { get; set; }
		public Databases DatenbankTyp { get; set; }
		public string ConnectionString { get; set; }
		public int DBAnzahl { get; set; }

		#endregion

		#region Transaction

		public virtual void Rollback()
		{
		}

		public virtual void OpenTransaction()
		{
		}

		public virtual void Commit()
		{
		}

		#endregion

		//private void throwError(int datenbank)
		//{

		//  //throw new ArgumentException("The parameter was invalid");

		//  string text;

		//  text = "Die gewählte Datenbank existiert nicht.\n\nDie Angewählte Datenbank Nummer ist " + datenbank.ToString()
		//    + " Für die Applikation sind nur " + DBAnzahl.ToString() + " Datenbanken initialisiert";

		//  throw new ArgumentOutOfRangeException("datenbank", null, text);
		//}

		internal string addSchema(string text)
		{
			if (DatenbankTyp != Databases.MSAccess)
			{
				text = text.Replace("&", "+");
				text = text.Replace("true", "1");
				text = text.Replace("false", "0");
				text = text.Replace("True", "1");
				text = text.Replace("False", "0");
			}

			if (Schema != "" && Schema != null)
			{
				text = text.Replace("tbl", Schema + ".tbl");
				text = text.Replace(" view", " " + Schema + ".view");
			}
			return text;
		}

		internal virtual void saveBlob(string cmdString, byte[] bytefeld)
		{
		}

		internal virtual byte[] getBlob(string cmdString, long bytes)
		{
			return null;
		}

		internal virtual void SaveImage(string cmdString, long id, byte[] bytes)
		{
		}

		internal virtual void DeleteBlob(string cmdString)
		{
		}

		internal virtual DataRow GetDataRow(string cmdString)
		{
			return null;
		}

		internal virtual DataTable GetDataTable(string cmdString)
		{
			return null;
		}

		internal virtual DataSet GetDataset(string cmdString, string tableName)
		{
			return null;
		}

    internal virtual void WriteDataRow(DataRow row, bool isNew)
    {
    }

		//internal virtual DataRowCollection GetTableSchema(int datenbank, string cmdString)
		//{
		//  DataRowCollection result = null;
		//    switch (DatenbankTyp)
		//    {
		//      case Databases.SQLServer:
		//        SqlCommand cmdWithoutTransaction = new SqlCommand(cmdString, this.ConnectionWithoutTransaction);
		//        using (SqlDataReader dr = cmdWithoutTransaction.ExecuteReader(CommandBehavior.SchemaOnly))
		//        {
		//          cmdWithoutTransaction.Dispose();
		//          result = dr.GetSchemaTable().Rows;
		//        }
		//        break;

		//case Databases.MSAccess:
		//  ((DBConnectionAccess)dbtest[datenbank]).DataBindingInit1(cmdstring);
		//  break;

		//case Databases.Oracle:
		//  ((DBConnectionOracle)dbtest[datenbank]).DataBindingInit1(cmdstring);
		//  break;

		//case Databases.SQLite:
		//  ((DBConnectionSQLite)dbtest[datenbank]).DataBindingInit1(cmdstring);
		//  break;
		//    }

		//    return result;
		//  }
		//}

		internal virtual void CallProcedure(string cmd, ArrayList parameter)
		{
		}

		internal virtual object CallProcedure(string cmd, ArrayList parameter, string returnParameter)
		{
			return null;
		}

		internal virtual object[] CallProcedure(string cmd, ArrayList parameter, string[] returnParameter)
		{
			return null;
		}

		internal virtual object Find(string cmdString)
		{
			return null;
		}

    internal virtual void MergeDataTables(string cmd, DataTable dtNew)
    {
    }
 
		//public virtual int DataBindingInit(string cmdstring)
		//{
		//  return 0;
		//}

		//public virtual void BindingSave(int index, BindingSource bindingSource)
		//{
		//}

		//public static string DBDateSQL(int datenbank, object wert)
		//{
		//  return convertDB[datenbank].DBDate(wert);
		//}

		//public static string DBDateSQL(object wert)
		//{
		//  return convertDB[0].DBDate(wert);
		//}

#region DummyProperties
		//public virtual DataRowCollection GetTableSchema(string cmdString)
		//{
		//  return null;
		//}

		//public virtual long Sequence(string cmdString)
		//{
		//  return 0;
		//}

		internal virtual object GetObject(string cmdString)
		{
			return null;
		}

#endregion
	}
}
