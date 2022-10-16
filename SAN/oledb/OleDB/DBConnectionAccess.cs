namespace OleDB
{
	using System;
	using System.Text;
	using System.Data;
	using System.Data.OleDb;
	using System.Configuration;
	using System.Xml;
	using System.Collections;
	using System.Windows.Forms;
	using System.Collections.Generic;
	using System.IO;
	using SAN.Converter;
  using System.Reflection;
  using System.Runtime.InteropServices;

	/// <summary>
	/// Summary description for OleDBZugriff.
	/// </summary>
	public class DBConnectionAccess : DBConnection
	{
		private OleDbConnection Connection;
		private OleDbConnection ConnectionWithoutTransaction;
		private OleDbTransaction Transaction;
		private Dictionary<string, OleDbDataAdapter> dataAdapter = new Dictionary<string, OleDbDataAdapter>();

		public DBConnectionAccess()
		{
			DatenbankTyp = Databases.MSAccess;
		}

		#region Connections

		public void Connect()
		{
      ConnectionString = "Provider=" + Provider + ";Data Source=" + DataBase + ";User ID=" + User + ";Jet OLEDB:Database Password=" + Password + ";";


			Connection = new OleDbConnection(ConnectionString);
			ConnectionWithoutTransaction = new OleDbConnection(ConnectionString);
			//Connection.InfoMessage += new OleDbInfoMessageEventHandler(OnInfoMessage);
		}

		//private void OnInfoMessage(object sender,OleDbInfoMessageEventArgs e)
		//{
		//  throw new Exception(e.Message);
		//}

		#endregion

		#region Transaction

		public override void Rollback()
		{
			if (Transaction != null)
			{
						if (Transaction.Connection != null)
							Transaction.Rollback();
			}
		}

		public override void OpenTransaction()
		{
				if (Transaction != null)
				{
					if (Transaction.Connection != null)
					{
						do
							System.Threading.Thread.Sleep(100);

						while (Transaction.Connection != null);
					}

				Transaction = Connection.BeginTransaction();
			}
		}

		public override void Commit()
		{
				if (Transaction != null)
				{
					if (Transaction.Connection != null)
						Transaction.Commit();
				}
		}

		#endregion

		#region Blob
		public long SaveBlob(string tableName, long id, byte[] bytefeld)
		{
			string cmdString;
			OleDbCommand cmd;

			if (id < 1)
			{
				cmdString = "Select Max(id) from " + tableName;
				cmd = new OleDbCommand(addSchema(cmdString), Connection, Transaction);
				id = ConvertEx.ToInt64(cmd.ExecuteScalar()) + 1;

				//cmdString = "Insert Into " + tableName + " (id,Length,Objekt) values(@id,@Length,@Blob)";
				cmdString = "Insert Into " + tableName + " (id,Length) values(@id,@Length)";
			}
			else
			{
				//cmdString = "Update " + tableName + " set Length =@Length, Objekt=@Blob where id = @id";
				cmdString = "Update " + tableName + " set Length = @Length where id = @id";
			}

			cmd = new OleDbCommand(addSchema(cmdString), Connection, Transaction);
			OleDbParameter paramID = cmd.Parameters.Add("@id", OleDbType.Integer);
			OleDbParameter paramLength = cmd.Parameters.Add("@Length", OleDbType.Integer);
			//OleDbParameter paramBlob = cmd.Parameters.Add("@Blob", OleDbType.VarBinary);
			//paramBlob.Value = bytefeld;
			paramID.Value = id;
			paramLength.Value = bytefeld.Length;
			cmd.ExecuteNonQuery();

			cmdString = "Update " + tableName + " set Objekt=? where id = " + id.ToString();
			cmd = new OleDbCommand(addSchema(cmdString), Connection, Transaction);
			OleDbParameter paramBlob = cmd.Parameters.Add("@blob", OleDbType.VarBinary);
			paramBlob.Value = bytefeld;
			cmd.ExecuteNonQuery();

			cmd.Dispose();

			return id;
		}



		internal override byte[] getBlob(string cmdString, long bytes)
		{
			OleDbDataReader reader;
			long count = 0;
			long startIndex = 0;

			OleDbCommand cmd = new OleDbCommand(cmdString, ConnectionWithoutTransaction);
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
			OleDbParameter paramID;
			OleDbParameter paramLength;

			OleDbCommand cmd = new OleDbCommand(addSchema(cmdString), Connection, Transaction);
			paramID = cmd.Parameters.Add("@id", OleDbType.Integer);
			paramLength = cmd.Parameters.Add("@Length", OleDbType.Integer);
			paramID.Value = id;
			paramLength.Value = bytes.Length;
			cmd.ExecuteNonQuery();

			cmd.Dispose();
		}

		public byte[] LoadImage(string cmdString)
		{
			long bytes = 0;
			byte[] bytefeld = null;

			OleDbCommand cmd = new OleDbCommand(cmdString, ConnectionWithoutTransaction);
			OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
			reader.Read();

			if (reader.HasRows)
			{
				bytes = Convert.ToInt64(reader["Length"]);
				if (bytes != 0 && reader["Objekt"] != DBNull.Value)
					bytefeld = (byte[])reader["Objekt"];
				reader.Close();
				reader = null;
				cmd.Dispose();
			}
			return bytefeld;
		}

		internal override void DeleteBlob(string cmdString)
		{
			OleDbCommand cmd = new OleDbCommand(addSchema(cmdString), Connection, Transaction);
			cmd.ExecuteNonQuery();
		}

		#endregion

		internal int Execute(string cmdString)
		{
			int rows = 0;

			try
			{
				OleDbCommand cmd = new OleDbCommand(addSchema(cmdString), Connection, Transaction);
				rows = cmd.ExecuteNonQuery();
				cmd.Dispose();
			}
			catch (Exception e)
			{
				rows = 0;
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmdString) + "\n\n" + e.Message + "\n\n",
					e.InnerException);
			}

			return rows;
		}

		public long Sequence(string cmdString)
		{
			long result;

			OleDbCommand cmd = new OleDbCommand(cmdString, Connection, Transaction);
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
			OleDbDataAdapter da = new OleDbDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
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
			OleDbDataAdapter da = new OleDbDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
			DataTable dt = new DataTable();
			try
			{
				da.Fill(dt);
			}
			catch (Exception e)
			{
				throw new DataException("\nFehler beim Lesen einer Datatable aus der Datenbank\n" + e.Message + "\n" + addSchema(cmdString) 
					+ "\n\n" + e.Message + "\n\n", e.InnerException);
			}

			da.Dispose();

			return dt;
		}

		internal override DataSet GetDataset(string cmdString, string tableName)
		{
			OleDbDataAdapter da = new OleDbDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
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
      try
      {
        OleDbCommand cmdWithoutTransaction = new OleDbCommand(cmdString, ConnectionWithoutTransaction);
        using (OleDbDataReader dr = cmdWithoutTransaction.ExecuteReader(CommandBehavior.SchemaOnly))
        {
          cmdWithoutTransaction.Dispose();
          return dr.GetSchemaTable().Rows;
        }
      }
      catch
      {
        return null;
      }
		}

		internal override void CallProcedure(string cmd, ArrayList parameter)
		{
			try
			{
				OleDbCommand cmdWithoutTransaction = new OleDbCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (OleDbParameter p in parameter)
					cmdWithoutTransaction.Parameters.Add(p);

				cmdWithoutTransaction.CommandText = cmd;
				cmdWithoutTransaction.ExecuteNonQuery();

				cmdWithoutTransaction.Dispose();
			}
			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmd) + "\n\n" + e.Message 
					+ "\n\n", e.InnerException);
			}
		}

		internal override object CallProcedure(string cmd, ArrayList parameter, string returnParameter)
		{
			object result = null;

			try
			{
				OleDbCommand cmdWithoutTransaction = new OleDbCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (OleDbParameter p in parameter)
					cmdWithoutTransaction.Parameters.Add(p);

				cmdWithoutTransaction.CommandText = cmd;
				cmdWithoutTransaction.ExecuteNonQuery();

				result = cmdWithoutTransaction.Parameters[returnParameter].Value;

				cmdWithoutTransaction.Dispose();
			}
			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmd) + "\n\n" + e.Message 
					+ "\n\n", e.InnerException);
			}

			return result;
		}

		internal override object[] CallProcedure(string cmd, ArrayList parameter, string[] returnParameter)
		{
			object[] result;
			result = new object[returnParameter.Length];

			try
			{
				OleDbCommand cmdWithoutTransaction = new OleDbCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (OleDbParameter p in parameter)
					cmdWithoutTransaction.Parameters.Add(p);

				cmdWithoutTransaction.CommandText = cmd;
				cmdWithoutTransaction.ExecuteNonQuery();

				for (int i = 0; i < returnParameter.Length; i++)
					result[i] = cmdWithoutTransaction.Parameters[returnParameter[i]].Value;

				cmdWithoutTransaction.Dispose();
			}
			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmd) + "\n\n" + e.Message 
					+ "\n\n", e.InnerException);
			}

			return result;
		}

		internal override object Find(string cmdString)
		{
			OleDbCommand cmdWithoutTransaction = new OleDbCommand(cmdString, ConnectionWithoutTransaction);
			object result = cmdWithoutTransaction.ExecuteScalar();
			cmdWithoutTransaction.Dispose();

			return result;
		}

		internal override object GetObject(string cmdString)
		{
			object result = null;

			try
			{
				OleDbCommand cmdWithoutTransaction = new OleDbCommand(addSchema(cmdString), ConnectionWithoutTransaction);
				result = cmdWithoutTransaction.ExecuteScalar();
				cmdWithoutTransaction.Dispose();
			}

			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmdString) 
					+ "\n\n" + e.Message + "\n\n", e.InnerException);
			}

			return result;
		}

		internal override void CloseConnection()
		{
			Connection.Close();
			ConnectionWithoutTransaction.Close();
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

    public BindingSource DataBindingInit(string cmdstring, string tabelle)
    {
      BindingSource DataBinding = new BindingSource();

      DataTable table = new DataTable();
      try
      {

        // Create a new data adapter based on the specified query.
        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdstring, Connection);

        // Create a command builder to generate SQL update, insert, and delete commands based on selectCommand. 
        //These are used to update the database.
        OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter);

        // Populate a new data table and bind it to the BindingSource.
        table.Locale = System.Globalization.CultureInfo.InvariantCulture;

        dataAdapter.Fill(table);
        if (this.dataAdapter.ContainsKey(tabelle))
          this.dataAdapter.Remove(tabelle);
        this.dataAdapter.Add(tabelle, dataAdapter);

        DataBinding.DataSource = table;
      }
      catch { }

      return DataBinding;
    }

		public void BindingClose(string tabelle)
		{
			this.dataAdapter.Remove(tabelle);
		}

		public void BindingSave(string tabelle, BindingSource bindingSource)
		{
			int count = dataAdapter[tabelle].Update((DataTable)bindingSource.DataSource);
		}

		public void CreateDatabase()
		{
			ADOX.Catalog cat = new ADOX.Catalog();

			cat.Create(ConnectionString);
			//cat.Create(@"Provider=" + Provider + ";Data Source=" + DataBase + ";Jet OLEDB:Engine Type=5");

			Connect();
			Connection.Open();
		}

		public Boolean DatabaseExists
		{
			get
			{
				return File.Exists(DataBase);
			}
		}

    public void MergeDataTables(string cmd, DataTable dtNew)
    {
      using (OleDbConnection connection = new OleDbConnection(ConnectionString))
      {
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        adapter.SelectCommand = new OleDbCommand(cmd, connection);
        OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);

        connection.Open();

        DataTable temp = new DataTable();
        adapter.Fill(temp);

        temp.Clear();
        temp.Merge(dtNew, false);       // code to modify data in DataTable here

        adapter.Update(temp);

        //return customers;
      }
    }

  /// <summary>
    /// Compacts the access DB.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="filename">The filename.</param>
    public void CompactAccessDB()
    {
      //object dbe = new object();

      var dbe = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));
      //dbe.CompactDatabase(DataBase, Path.Combine(Path.GetDirectoryName(DataBase), "tempdb.mdb"));

      //dbe.


      //object objJRO = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));
      //object[] oParams = new object[] { ConnectionString, Provider + " Source=" + Path.Combine(Path.GetDirectoryName(DataBase), "tempdb.mdb") + ";Jet DAO.DBEngine.120 Type=5" };

      //objJRO.GetType().InvokeMember("CompactDatabase", BindingFlags.InvokeMethod, null, objJRO, oParams);

      //File.Delete(DataBase);
      //File.Move(Path.Combine(Path.GetDirectoryName(DataBase), "tempdb.mdb"), DataBase);

      //Marshal.ReleaseComObject(objJRO);
    }

	}
}