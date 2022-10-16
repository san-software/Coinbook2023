	using System;
	using System.Text;
	using System.Data;
using System.Data.SqlClient;
using System.Configuration;
	using System.Xml;
	using System.Collections;
	using System.Collections.Generic;
using System.Windows.Forms;
	
	namespace OleDB
{


	/// <summary>
	/// Summary description for OleDBZugriff.
	/// </summary>
	public class DBConnectionSQLServer : DBConnection
	{
		private SqlConnection Connection;
		private SqlConnection ConnectionWithoutTransaction;
		private SqlTransaction transaction;
		private Dictionary<string,SqlDataAdapter> dataAdapter = new Dictionary<string,SqlDataAdapter>();

		public DBConnectionSQLServer()
		{
			DatenbankTyp = Databases.SQLServer;
		}

		#region Connections

		public void Connect()
		{
			ConnectionString = "Data Source=" + Host + ";Initial Catalog=" + DataBase + ";Integrated Security=SSPI;Persist Security Info=True";

			Connection = new SqlConnection(ConnectionString);
			ConnectionWithoutTransaction = new SqlConnection(ConnectionString);
			//Connection.InfoMessage += new SqlInfoMessageEventHandler(OnInfoMessage);
		}

    public Boolean OpenConnection
    {
      get
      {
        Boolean result = true;

        try
        {
          Connection.Open();
          ConnectionWithoutTransaction.Open();
        }
        catch (Exception e)
        {
          MessageBox.Show(e.Message);
          result = false;
        }

        return result;
      }
		}
		#endregion

		#region Transaction

		public override void Rollback()
		{
			if (transaction != null)
				if (transaction.Connection != null)
					transaction.Rollback();
		}

		public override void OpenTransaction()
		{
			if (transaction != null)
				if (transaction.Connection != null)
				{
					do
						System.Threading.Thread.Sleep(100);

					while (transaction.Connection != null);
				}

			transaction = Connection.BeginTransaction();
		}

		public override void Commit()
		{
			if (transaction != null)
				if (transaction.Connection != null)
					transaction.Commit();
		}

		#endregion

		#region Blob
		public long SaveBlob(string tableName, long id, byte[] bytefeld)
		{
			string cmdString;
			SqlCommand cmd;

			if (id < 1)
			{
				cmdString = "Select Max(id) from " + tableName;
				cmd = new SqlCommand(addSchema(cmdString), Connection);
				if (cmd.ExecuteScalar() == DBNull.Value)
					id = 1;
				else
					id = Convert.ToInt64(cmd.ExecuteScalar()) + 1;

				cmdString = "Insert Into " + tableName + " (id,Length,Objekt) values(@id,@Length,@Blob)";
			}
			else
				cmdString = "Update " + tableName + " set Length =@Length, Objekt=@Blob where id = @id";

			cmd = new SqlCommand(addSchema(cmdString), Connection);
			SqlParameter paramID = cmd.Parameters.Add("@id", SqlDbType.Int);
			SqlParameter paramLength = cmd.Parameters.Add("@Length", SqlDbType.BigInt);
			SqlParameter paramBlob = cmd.Parameters.Add("@Blob", SqlDbType.VarBinary);
			paramBlob.Value = bytefeld;
			paramID.Value = id;
			paramLength.Value = bytefeld.Length;
			cmd.ExecuteNonQuery();
			cmd.Dispose();

			return id;
		}

		internal override byte[] getBlob(string cmdString, long bytes)
		{
			SqlDataReader reader;
			long count = 0;
			long startIndex = 0;

			SqlCommand cmd = new SqlCommand(cmdString, ConnectionWithoutTransaction);
			reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
			reader.Read();

			byte[] bytefeld = new byte[bytes];

			if (bytes != 0)
			{
				do
				{
					count = reader.GetBytes(0, startIndex, bytefeld, 0, Convert.ToInt32(bytes));
					if (count == 0)
						break;
					startIndex += count;
				}
				while (true);
			}
			reader.Close();
			reader = null;
			cmd.Dispose();

			return bytefeld;
		}

		internal override void SaveImage(string cmdString, long id, byte[] bytes)
		{
			SqlParameter paramID;
			SqlParameter paramLength;

			SqlCommand cmd = new SqlCommand(addSchema(cmdString), Connection, transaction);
			paramID = cmd.Parameters.Add("@id", SqlDbType.Int);
			paramLength = cmd.Parameters.Add("@Length", SqlDbType.Int);
			paramID.Value = id;
			paramLength.Value = bytes.Length;
			cmd.ExecuteNonQuery();

			cmd.Dispose();
		}

		public byte[] LoadImage(string cmdString)
		{
			long bytes = 0;
			byte[] bytefeld = null;

			SqlCommand cmd = new SqlCommand(cmdString, ConnectionWithoutTransaction);
			SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
			reader.Read();

			if (reader.HasRows)
			{
				if (bytes != 0 && reader["Objekt"] != DBNull.Value)
				{
					bytes = Convert.ToInt64(reader["Length"]);
					bytefeld = (byte[])reader["Objekt"];
				}
				reader.Close();
				reader = null;
				cmd.Dispose();
			}
			return bytefeld;
		}

		internal override void DeleteBlob(string cmdString)
		{
			SqlCommand cmd = new SqlCommand(addSchema(cmdString), Connection, transaction);
			cmd.ExecuteNonQuery();
		}

        #endregion

        public int Execute(string cmdString)
        {
            int rows = 0;

            if (cmdString.Substring(0, 7).ToLower() == "delete ")
                cmdString = cmdString.Replace("*", "");

            cmdString = cmdString.Replace("True", "1").Replace("true", "1").Replace("False", "0").Replace("false", "0");

            try
            {
                SqlCommand cmd = new SqlCommand(addSchema(cmdString), Connection, transaction);
                rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                rows = 0;
                throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmdString) + "\n\n" + e.Message + "\n\n", e.InnerException);
            }

            return rows;
        }

		public long Sequence(string cmdString)
		{
			long result;

			SqlCommand cmd = new SqlCommand(cmdString, Connection, transaction);
			object temp = cmd.ExecuteScalar();
			cmd.Dispose();

			if (temp == System.DBNull.Value)
				result = 1;
			else
				result = Convert.ToInt64(temp) + 1;

			return result;
		}

		internal override DataRow GetDataRow(string cmdString)
		{
			SqlDataAdapter da = new SqlDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
			}
			catch (Exception e)
			{
				throw new DataException("\nFehler beim Lesen einer Datatable aus der Datenbank\n" + e.Message + "\n" + addSchema(cmdString) + "\n\n" + e.Message + "\n\n", e.InnerException);
			}

			da.Dispose();

			if (dt.Rows.Count != 0)
				return dt.Rows[0];
			else
				return null;
		}

		internal override DataTable GetDataTable(string cmdString)
		{
			SqlDataAdapter da = new SqlDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
			}
			catch (Exception e)
			{
				throw new DataException("\nFehler beim Lesen einer Datatable aus der Datenbank\n" + e.Message + "\n" + addSchema(cmdString) + "\n\n" + e.Message + "\n\n", e.InnerException);
			}

			da.Dispose();

			return dt;
		}

		internal override DataSet GetDataset(string cmdString, string tableName)
		{
			SqlDataAdapter da = new SqlDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
			DataSet ds = new DataSet();
			try
			{
				if (tableName != null)
					da.Fill(ds, tableName);
				else
					da.Fill(ds);
			}
			catch (Exception e)
			{
				throw new DataException("\nFehler beim Lesen eines Datasets aus der Datenbank\n"
						+ addSchema(cmdString) + "\n\n" + e.Message + "\n\n", e.InnerException);
			}

			da.Dispose();

			return ds;
		}

		public DataRowCollection GetTableSchema(string cmdString)
		{
      cmdString = cmdString.Replace("true", "1").Replace("True", "1").Replace("false", "0").Replace("False", "0");

			SqlCommand cmdWithoutTransaction = new SqlCommand(cmdString, ConnectionWithoutTransaction);
			using (SqlDataReader dr = cmdWithoutTransaction.ExecuteReader(CommandBehavior.SchemaOnly))
			{
				cmdWithoutTransaction.Dispose();

				return dr.GetSchemaTable().Rows;
			}
		}

		internal override void CallProcedure(string cmd, ArrayList parameter)
		{
			try
			{
				SqlCommand cmdWithoutTransaction = new SqlCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (SqlParameter p in parameter)
					cmdWithoutTransaction.Parameters.Add(p);

				cmdWithoutTransaction.CommandText = cmd;
				cmdWithoutTransaction.ExecuteNonQuery();

				cmdWithoutTransaction.Dispose();
			}
			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmd) + "\n\n" + e.Message + "\n\n", e.InnerException);
			}
		}

		internal override object CallProcedure(string cmd, ArrayList parameter, string returnParameter)
		{
			object result = null;

			try
			{
				SqlCommand cmdWithoutTransaction = new SqlCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (SqlParameter p in parameter)
					cmdWithoutTransaction.Parameters.Add(p);

				cmdWithoutTransaction.CommandText = cmd;
				cmdWithoutTransaction.ExecuteNonQuery();

				result = cmdWithoutTransaction.Parameters[returnParameter].Value;

				cmdWithoutTransaction.Dispose();
			}
			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmd) + "\n\n" + e.Message + "\n\n", e.InnerException);
			}

			return result;
		}

		internal override object[] CallProcedure(string cmd, ArrayList parameter, string[] returnParameter)
		{
			object[] result;
			result = new object[returnParameter.Length];

			try
			{
				SqlCommand cmdWithoutTransaction = new SqlCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (SqlParameter p in parameter)
					cmdWithoutTransaction.Parameters.Add(p);

				cmdWithoutTransaction.CommandText = cmd;
				cmdWithoutTransaction.ExecuteNonQuery();

				for (int i = 0; i < returnParameter.Length; i++)
					result[i] = cmdWithoutTransaction.Parameters[returnParameter[i]].Value;

				cmdWithoutTransaction.Dispose();
			}
			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmd) + "\n\n" + e.Message + "\n\n", e.InnerException);
			}

			return result;
		}

		internal override object Find(string cmdString)
		{
			SqlCommand cmdWithoutTransaction = new SqlCommand(cmdString, ConnectionWithoutTransaction);
			object result = cmdWithoutTransaction.ExecuteScalar();
			cmdWithoutTransaction.Dispose();

			return result;
		}

		internal override object GetObject(string cmdString)
		{
			object result = null;

			try
			{
				SqlCommand cmdWithoutTransaction = new SqlCommand(addSchema(cmdString), ConnectionWithoutTransaction);
				result = cmdWithoutTransaction.ExecuteScalar();
				cmdWithoutTransaction.Dispose();
			}

			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmdString) + "\n\n" + e.Message 
					+ "\n\n", e.InnerException);
			}

			return result;
		}

		internal override void CloseConnection()
		{
			Connection.Close();
			ConnectionWithoutTransaction.Close();
		}

    internal override void WriteDataRow(DataRow row, bool isNew)
    {
      string cmd;
      DataTable dt = row.Table;

      if (!isNew)
      {
        cmd = "Update " + dt.TableName + " set ";
        for (int i = 1; i < row.ItemArray.Length; i++)
        {
          switch (row[i].GetType().ToString())
          {
            case "System.String":
              cmd = cmd + dt.Columns[i].ColumnName + "='" + row[i].ToString().Replace("'","''") + "',";
              break;

            case "System.Int16":
            case "System.Int32":
            case "System.Int64":
              cmd = cmd + dt.Columns[i].ColumnName + "=" + row[i].ToString() + ",";
              break;

            case "System.Single":
            case "System.Double":
              cmd = cmd + dt.Columns[i].ColumnName + "=" + row[i].ToString().Replace(",", ".") + ",";
              break;

            case "System.Boolean":
              cmd = cmd + dt.Columns[i].ColumnName + "=" + row[i].ToString() + ",";
              break;

            case "System.DBNull":
              cmd = cmd + dt.Columns[i].ColumnName + "=null,";
              break;

            default:
              throw new Exception();
              break;
          }
        }

        cmd = cmd.Substring(0, cmd.Length - 1) + " where " + dt.Columns[0].ColumnName + "=" + row[0].ToString();
      }
      else
      {
        cmd = "Insert into " + dt.TableName + " (";
        for (int i = 0; i < row.ItemArray.Length; i++)
          cmd = cmd + dt.Columns[i].ColumnName + ",";

        cmd = cmd.Substring(0, cmd.Length - 1) + ") Values (";

        for (int i = 0; i < row.ItemArray.Length; i++)
        {
          switch (row[i].GetType().ToString())
          {
            case "System.String":
              cmd = cmd + "'" + row[i].ToString().Replace("'","''") + "',";
              break;

            case "System.Int16":
            case "System.Int32":
            case "System.Int64":
              cmd = cmd + row[i].ToString() + ",";
              break;

            case "System.Single":
            case "System.Double":
              cmd = cmd + row[i].ToString().Replace(",",".") + ",";
              break;

            case "System.Boolean":
              cmd = cmd + row[i].ToString() + ",";
              break;

            case "System.DBNull":
              cmd = cmd + "null,";
              break;

            default:
              throw new Exception();
              break;
          }
        }

        cmd = cmd.Substring(0, cmd.Length - 1) + ")";
      }

      Execute(cmd);
    }

		public BindingSource DataBindingInit(string cmdstring, string tabelle)
		{
      cmdstring = cmdstring.Replace("true", "1").Replace("True", "1").Replace("false", "0").Replace("False", "0");

			BindingSource DataBinding = new BindingSource();

			DataTable table = new DataTable();

			// Create a new data adapter based on the specified query.
			SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdstring, Connection);			

			// Create a command builder to generate SQL update, insert, and delete commands based on selectCommand.
			//These are used to update the database.
			SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

			table.Locale = System.Globalization.CultureInfo.InvariantCulture;// Populate a new data table and bind it to the BindingSource.

			dataAdapter.Fill(table);

			if (!this.dataAdapter.ContainsKey(tabelle))
				this.dataAdapter.Add(tabelle, dataAdapter);
			else
				this.dataAdapter[tabelle] = dataAdapter;

			DataBinding.DataSource = table;

			return DataBinding;
		}

		public void BindingClose(string tabelle)
		{
			this.dataAdapter.Remove(tabelle);
		}

		//public override BindingSource DataBinding { get; set; }

		public void BindingSave(string tabelle, BindingSource bindingSource)
		{
			int count = dataAdapter[tabelle].Update((DataTable)bindingSource.DataSource);
		}

		public bool ExistTable(string table)
		{
			bool result = false;

			string command = $"select * from sys.tables";
			//using (SqlConnection con = new SqlConnection(Connection))
			using (SqlCommand com = new SqlCommand(command, Connection))
			{
				SqlDataReader reader = com.ExecuteReader();
				while (reader.Read())
				{
					if (reader.GetString(0).ToLower() == table.ToLower())
						result= true;
				}
				reader.Close();
			}
			return result;
		}

	//public void CreateDatabase()
	//{
	//  string cmd = "CREATE DATABASE mytest";

	//  SqlConnection myConn = new SqlConnection("Server=" + Host + ";Integrated security=SSPI;database=master");
	//  SqlCommand myCommand = new SqlCommand(cmd, myConn);

	//  myConn.Open();
	//  myCommand.ExecuteNonQuery();

	//  myConn.Close();

	//  Connect();
	//  Connection.Open();
	//}

	//public Boolean DatabaseExists
	//{
	//  get
	//  {
	//    string cmd = "SELECT Count(name) FROM sys.databases where name = '" + DataBase +"'";

	//    SqlCommand c = new SqlCommand(cmd, ConnectionWithoutTransaction);
	//    SqlDataReader reader = c.ExecuteReader(CommandBehavior.SequentialAccess);
	//    reader.Read();

	//    return (reader.GetInt16(0) != 0);
	//  }
	//}


}
}

