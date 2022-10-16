namespace OleDB
{
	using System;
	using System.Text;
	using System.Data;
	using System.Data.OleDb;
	using System.Configuration;
	using System.Xml;
	using System.Data.SQLite;
	using System.Collections;
	using System.Windows.Forms;

	/// <summary>
	/// Summary description for OleDBZugriff.
	/// </summary>
	public class DBCheck
	{
		private static DBConfig configuration = new DBConfig();

		public static void Init()
		{
			string file = "database.config";

			if (OleDBHelper.ConfigFile != "" && OleDBHelper.ConfigFile != null)
				file = OleDBHelper.ConfigFile;

			if (OleDBHelper.FileExists(file))
				configuration.readConnectionFromXML();
			else
				configuration.readConnectionFromConfig();

			for (int counter = 0; counter < configuration.Anzahl; counter++)
			{
				switch (configuration.DatenbankTyp[counter])
				{
				  case Databases.SQLServer:
						DBConnectionSQLServer connection = new DBConnectionSQLServer();
						connection.Host = configuration.Host[counter];
						connection.Provider = configuration.Provider[counter];
						connection.Schema = configuration.Schema[counter];
						connection.DataSource = configuration.Host[counter];
						connection.User = configuration.User[counter];
						connection.DataBase = configuration.DataBase[counter];
						connection.Password = configuration.Password[counter];
						connection.Datapath = configuration.Datapath;
						connection.DBAnzahl = configuration.Anzahl;
						connection.DatenbankTyp = configuration.DatenbankTyp[counter];
						connection.Connect();
						connection.OpenConnection();
						Connections.Add(connection);
						break;

					case Databases.MSAccess:
						DBConnectionAccess access = new DBConnectionAccess();
						access.Host = configuration.Host[counter];
						access.Provider = configuration.Provider[counter];
						access.Schema = configuration.Schema[counter];
						access.DataSource = configuration.Host[counter];
						access.User = configuration.User[counter];
						access.DataBase = configuration.DataBase[counter];
						access.Password = configuration.Password[counter];
						access.Datapath = configuration.Datapath;
						access.DBAnzahl = configuration.Anzahl;
						access.DatenbankTyp = configuration.DatenbankTyp[counter];
						access.Connect();
						Connections.Add(access);
						break;

					case Databases.Oracle:
						DBConnectionOracle oracle = new DBConnectionOracle();
						oracle.Host = configuration.Host[counter];
						oracle.Provider = configuration.Provider[counter];
						oracle.Schema = configuration.Schema[counter];
						oracle.DataSource = configuration.Host[counter];
						oracle.User = configuration.User[counter];
						oracle.DataBase = configuration.DataBase[counter];
						oracle.Password = configuration.Password[counter];
						oracle.Datapath = configuration.Datapath;
						oracle.DBAnzahl = configuration.Anzahl;
						oracle.DatenbankTyp = configuration.DatenbankTyp[counter];
						oracle.Connect();
						Connections.Add(oracle);
						break;

					case Databases.SQLite:
						DBConnectionSQLite sqlite = new DBConnectionSQLite();
						sqlite.Host = configuration.Host[counter];
						sqlite.Provider = configuration.Provider[counter];
						sqlite.Schema = configuration.Schema[counter];
						sqlite.DataSource = configuration.Host[counter];
						sqlite.User = configuration.User[counter];
						sqlite.DataBase = configuration.DataBase[counter];
						sqlite.Password = configuration.Password[counter];
						sqlite.Datapath = configuration.Datapath;
						sqlite.DBAnzahl = configuration.Anzahl;
						sqlite.DatenbankTyp = configuration.DatenbankTyp[counter];
						sqlite.Connect();
						Connections.Add(sqlite);
						break;
				}
			}
		}

		//public static void Init(string datenbankTyp, string provider, string dataBase,string host)
		//{
		//  string connectionString;

		//  configuration.Anzahl = 1;

		//  string userID = "";
		//  string password = "";

		//  for (int counter = 0; counter < configuration.Anzahl; counter++)
		//  {
		//    switch (datenbankTyp)
		//    {
		//      case Databases.SQLServer:
		//        connectionString= "Provider=" + provider[counter] + ";Data Source=" + host[counter]
		//                         + ";Initial Catalog=" + dataBase[counter]
		//                         + ";Integrated Security=SSPI;Persist Security Info=False";
		//        dbtest.Add(new DBConnection(connectionString, datenbankTyp));
		//        break;

		//      case Databases.MSAccess:
		//        connectionString = "Provider=" + provider[counter] + ";Data Source=" + dataBase[counter]
		//                         + ";User ID=" + userID + ";Password=" + password + ";";
		//        dbtest.Add(new DBConnection(connectionString, datenbankTyp));
		//        break;

		//      case "SQlite":
		//        connectionString = "Data Source=" + dataBase[counter];
		//        dbtest.Add(new DBConnection(connectionString, datenbankTyp));
		//        break;
		//    }
		//  }
		//}

		#region Connections

		private static void OnInfoMessage(object sender, OleDbInfoMessageEventArgs e)
		{
			throw new Exception(e.Message);
		}

		public static void CloseAllConnections()
		{
			for (int counter = 0; counter < Connections.Count; counter++)
				((DBConnection)Connections[counter]).CloseConnection();
		}

		//public DBConnection DBConnection(int datenbank)
		//{
		//  return (DBConnection)dbtest[datenbank];
		//}

		//public DBConnection DBConnection()
		//{
		//  return (DBConnection)dbtest[0];
		//}

		#endregion

		#region Transaction
		internal static OleDbTransaction Transaction(int datenbank)
		{
			if (datenbank >= 0 && datenbank < configuration.Anzahl)
				return transaction[datenbank];
			else
				return null;
			//  TODO Exception werfen
		}

		//public void Rollback(int datenbank)
		//{
		//  DBConnection(datenbank).Rollback();
		//}

		//public void Rollback()
		//{
		//  DBConnection(0).Rollback();
		//}

		//public void OpenTransaction(int datenbank)
		//{
		//  DBConnection(datenbank).OpenTransaction();
		//}

		//public void OpenTransaction()
		//{
		//  DBConnection(0).OpenTransaction();
		//}

		//public void Commit(int datenbank)
		//{
		//  DBConnection(datenbank).Commit();
		//}

		//public void Commit()
		//{
		//  DBConnection(0).Commit();
		//}

		#endregion

		public static string Datapath
		{
			get;
			set;
		}

		//internal object[] CallProcedure(int datenbank, string cmd, ArrayList parameter, string[] returnParameter)
		//{
		//  object[] result = null;
		//  switch (configuration.DatenbankTyp[datenbank])
		//  {
		//    case Databases.SQLServer:
		//      result= ((DBConnectionSQLServer)dbtest[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		//      break;

		//    case Databases.MSAccess:
		//      result= ((DBConnectionAccess)dbtest[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		//      break;

		//    case Databases.Oracle:
		//      result= ((DBConnectionOracle)dbtest[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		//      break;

		//    case Databases.SQLite:
		//      result= ((DBConnectionSQLite)dbtest[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		//      break;
		//  }

		//  return result;
		//}

		//internal object CallProcedure(int datenbank, string cmd, ArrayList parameter, string returnParameter)
		//{
		//  object result = null;
		//  switch (configuration.DatenbankTyp[datenbank])
		//  {
		//    case Databases.SQLServer:
		//      result = ((DBConnectionSQLServer)dbtest[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		//      break;

		//    case Databases.MSAccess:
		//      result = ((DBConnectionAccess)dbtest[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		//      break;

		//    case Databases.Oracle:
		//      result = ((DBConnectionOracle)dbtest[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		//      break;

		//    case Databases.SQLite:
		//      result = ((DBConnectionSQLite)dbtest[datenbank]).CallProcedure(cmd, parameter, returnParameter);
		//      break;
		//  }

		//  return result;
		//}

		//internal void CallProcedure(int datenbank, string cmd, ArrayList parameter)
		//{
		//  switch (configuration.DatenbankTyp[datenbank])
		//  {
		//    case Databases.SQLServer:
		//      ((DBConnectionSQLServer)dbtest[datenbank]).CallProcedure(cmd, parameter);
		//      break;

		//    case Databases.MSAccess:
		//      ((DBConnectionAccess)dbtest[datenbank]).CallProcedure(cmd, parameter);
		//      break;

		//    case Databases.Oracle:
		//      ((DBConnectionOracle)dbtest[datenbank]).CallProcedure(cmd, parameter);
		//      break;

		//    case Databases.SQLite:
		//      ((DBConnectionSQLite)dbtest[datenbank]).CallProcedure(cmd, parameter);
		//      break;
		//  }
		//}

		//internal void DataBindingInit(int datenbank, string cmdstring)
		//{
		//  switch (configuration.DatenbankTyp[datenbank])
		//  {
		//    case Databases.SQLServer:
		//      ((DBConnectionSQLServer)dbtest[datenbank]).DataBindingInit1(cmdstring);
		//      break;

		//    case Databases.MSAccess:
		//      ((DBConnectionAccess)dbtest[datenbank]).DataBindingInit1(cmdstring);
		//      break;

		//    case Databases.Oracle:
		//      ((DBConnectionOracle)dbtest[datenbank]).DataBindingInit1(cmdstring);
		//      break;

		//    case Databases.SQLite:
		//      ((DBConnectionSQLite)dbtest[datenbank]).DataBindingInit1(cmdstring);
		//      break;
		//  }
		//}

		//internal BindingSource DataBinding(int datenbank)
		//{
		//    switch (configuration.DatenbankTyp[datenbank])
		//    {
		//      case Databases.SQLServer:
		//        bindingSource = ((DBConnectionSQLServer)dbtest[datenbank]).DataBinding1;
		//        break;

		//      case Databases.MSAccess:
		//        bindingSource = ((DBConnectionAccess)dbtest[datenbank]).DataBinding1;
		//        break;

		//      case Databases.Oracle:
		//        bindingSource = ((DBConnectionOracle)dbtest[datenbank]).DataBinding1;
		//        break;

		//      case Databases.SQLite:
		//        bindingSource = ((DBConnectionSQLite)dbtest[datenbank]).DataBinding1;
		//        break;
		//    }
		//    return bindingSource;
		//}

		////public BindingSource DataBinding {get; set;}

		//internal void BindingSave(int datenbank)
		//{
		//  switch (configuration.DatenbankTyp[datenbank])
		//  {
		//    case Databases.SQLServer:
		//      ((DBConnectionSQLServer)dbtest[datenbank]).BindingSave1();
		//      break;

		//    case Databases.MSAccess:
		//      ((DBConnectionAccess)dbtest[datenbank]).BindingSave1();
		//      break;

		//    case Databases.Oracle:
		//      ((DBConnectionOracle)dbtest[datenbank]).BindingSave1();
		//      break;

		//    case Databases.SQLite:
		//      ((DBConnectionSQLite)dbtest[datenbank]).BindingSave1();
		//      break;
		//  }
		//}


		//internal void BindingUndo(int datenbank)
		//{
		//  switch (configuration.DatenbankTyp[datenbank])
		//  {
		//    case Databases.SQLServer:
		//      ((DBConnectionSQLServer)dbtest[datenbank]).BindingUndo1();
		//      break;

		//    case Databases.MSAccess:
		//      ((DBConnectionAccess)dbtest[datenbank]).BindingUndo1();
		//      break;

		//    case Databases.Oracle:
		//      ((DBConnectionOracle)dbtest[datenbank]).BindingUndo1();
		//      break;

		//    case Databases.SQLite:
		//      ((DBConnectionSQLite)dbtest[datenbank]).BindingUndo1();
		//      break;
		//  }
		//}

		//public void ResetBindings(int datenbank)
		//{
		//  switch (configuration.DatenbankTyp[datenbank])
		//  {
		//    case Databases.SQLServer:
		//      ((DBConnectionSQLServer)dbtest[datenbank]).ResetBindings1();
		//      break;

		//    case Databases.MSAccess:
		//      ((DBConnectionAccess)dbtest[datenbank]).ResetBindings1();
		//      break;

		//    case Databases.Oracle:
		//      ((DBConnectionOracle)dbtest[datenbank]).ResetBindings1();
		//      break;

		//    case Databases.SQLite:
		//      ((DBConnectionSQLite)dbtest[datenbank]).ResetBindings1();
		//      break;
		//  }
		//}

		//public void NewRowDaten(int datenbank)
		//{
		//  switch (configuration.DatenbankTyp[datenbank])
		//  {
		//    case Databases.SQLServer:
		//      ((DBConnectionSQLServer)dbtest[datenbank]).AddNewRow();
		//      break;

		//    case Databases.MSAccess:
		//      ((DBConnectionAccess)dbtest[datenbank]).AddNewRow();
		//      break;

		//    case Databases.Oracle:
		//      ((DBConnectionOracle)dbtest[datenbank]).AddNewRow();
		//      break;

		//    case Databases.SQLite:
		//      ((DBConnectionSQLite)dbtest[datenbank]).AddNewRow();
		//      break;
		//  }
		//}

	}

}
