namespace OleDB
{
	using System;
	using System.Text;
	using System.Data;
	using System.Data.SQLite;
	using System.Configuration;
	using System.Xml;
	using System.Collections;
	using System.Collections.Generic;
	using System.Windows.Forms;
	using System.IO;
	using SAN.Converter;

	/// <summary>
	/// Summary description for OleDBZugriff.
	/// </summary>
	public class DBConnectionSQLite : DBConnection
	{
		//private SQLiteConnection Connection;
		private SQLiteConnection ConnectionWithoutTransaction;
		private SQLiteTransaction Transaction;
		private Dictionary<string,SQLiteDataAdapter> dataAdapter = new Dictionary<string,SQLiteDataAdapter>();

		public DBConnectionSQLite()
		{
			DatenbankTyp = Databases.SQLite;
		}

		#region Connections

		public void Connect()
		{
			ConnectionString = "Provider=" + Provider + ";Data Source=" + DataBase + ";User ID=" +User + ";Password=" + Password + ";";

			Connection = new SQLiteConnection(ConnectionString);
			ConnectionWithoutTransaction = new SQLiteConnection(ConnectionString);
			//Connection.InfoMessage += new SQLiteInfoMessageEventHandler(OnInfoMessage);
		}

		public SQLiteConnection Connection { get; set; }

		//private void OnInfoMessage(object sender,SQLiteInfoMessageEventArgs e)
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
			SQLiteCommand cmd;

			if (id < 1)
			{
				cmdString = "Select Max(id) from " + tableName;
				cmd = new SQLiteCommand(addSchema(cmdString), Connection, Transaction);
				id = ConvertEx.ToInt64(cmd.ExecuteScalar()) + 1;

				cmdString = "Insert Into " + tableName + " (id,Length,Objekt) values(@id,@Length,@Blob)";
			}
			else
				cmdString = "Update " + tableName + " set Length =@Length, Objekt=@Blob where id = @id";

			cmd = new SQLiteCommand(addSchema(cmdString), Connection, Transaction);
			SQLiteParameter paramID = cmd.Parameters.Add("@id", DbType.Int64);
			SQLiteParameter paramLength = cmd.Parameters.Add("@Length", DbType.Int64);
			SQLiteParameter paramBlob = cmd.Parameters.Add("@Blob", DbType.Object);
			paramBlob.Value = bytefeld;
			paramID.Value = id;
			paramLength.Value = bytefeld.Length;
			cmd.ExecuteNonQuery();
			cmd.Dispose();

			return id;
		}
		internal override byte[] getBlob(string cmdString, long bytes)
		{
			SQLiteDataReader reader;
			long count = 0;
			long startIndex = 0;

			SQLiteCommand cmd = new SQLiteCommand(cmdString, ConnectionWithoutTransaction);
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
			SQLiteParameter paramID;
			SQLiteParameter paramLength;

			SQLiteCommand cmd = new SQLiteCommand(addSchema(cmdString), Connection, Transaction);
			paramID = cmd.Parameters.Add("@id", DbType.Int64);
            paramLength = cmd.Parameters.Add("@Length", DbType.Int64);
			paramID.Value = id;
			paramLength.Value = bytes.Length;
			cmd.ExecuteNonQuery();

			cmd.Dispose();
		}

		public byte[] LoadImage(string cmdString)
		{
			long bytes = 0;
			byte[] bytefeld = null;

			SQLiteCommand cmd = new SQLiteCommand(cmdString, ConnectionWithoutTransaction);
			SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
			reader.Read();

			if (reader.HasRows)
			{
					bytes = Convert.ToInt64(reader["Length"]);
				if (bytes != 0)
					bytefeld = (byte[])reader["Objekt"];
				reader.Close();
				reader = null;
				cmd.Dispose();
			}
			return bytefeld;
		}

		internal override void DeleteBlob(string cmdString)
		{
			SQLiteCommand cmd = new SQLiteCommand(addSchema(cmdString), Connection, Transaction);
			cmd.ExecuteNonQuery();
		}

		#endregion

		public int Execute(string cmdString)
		{
			int rows = 0;

			try
			{
				SQLiteCommand cmd = new SQLiteCommand(addSchema(cmdString), Connection, Transaction);
				rows = cmd.ExecuteNonQuery();
				cmd.Dispose();
			}
			catch (Exception e)
			{
				rows = 0;
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmdString) + "\n\n" + e.Message 
					+ "\n\n", e.InnerException);
			}

			return rows;
		}

		public long Sequence(string cmdString)
		{
			long result;

			SQLiteCommand cmd = new SQLiteCommand(cmdString, Connection, Transaction);
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
			SQLiteDataAdapter da = new SQLiteDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
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

			if (dt.Rows.Count != 0)
				return dt.Rows[0];
			else
				return null;
		}

		internal override DataTable GetDataTable(string cmdString)
		{
			SQLiteDataAdapter da = new SQLiteDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
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
			SQLiteDataAdapter da = new SQLiteDataAdapter(addSchema(cmdString), ConnectionWithoutTransaction);
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
			SQLiteCommand cmdWithoutTransaction = new SQLiteCommand(cmdString, ConnectionWithoutTransaction);
			using (SQLiteDataReader dr = cmdWithoutTransaction.ExecuteReader(CommandBehavior.SchemaOnly))
			{
				cmdWithoutTransaction.Dispose();
				return dr.GetSchemaTable().Rows;
			}
		}

		internal override void CallProcedure(string cmd, ArrayList parameter)
		{
			try
			{
				SQLiteCommand cmdWithoutTransaction = new SQLiteCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (SQLiteParameter p in parameter)
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
				SQLiteCommand cmdWithoutTransaction = new SQLiteCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (SQLiteParameter p in parameter)
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
				SQLiteCommand cmdWithoutTransaction = new SQLiteCommand(addSchema(cmd), ConnectionWithoutTransaction);

				foreach (SQLiteParameter p in parameter)
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
			SQLiteCommand cmdWithoutTransaction = new SQLiteCommand(cmdString, ConnectionWithoutTransaction);
			object result = cmdWithoutTransaction.ExecuteScalar();
			cmdWithoutTransaction.Dispose();

			return result;
		}

		internal override object GetObject(string cmdString)
		{
			object result = null;

			try
			{
				SQLiteCommand cmdWithoutTransaction = new SQLiteCommand(addSchema(cmdString), ConnectionWithoutTransaction);
				result = cmdWithoutTransaction.ExecuteScalar();
				cmdWithoutTransaction.Dispose();
			}

			catch (Exception e)
			{
				throw new DataException("Fehler beim Ansprechen der Datenbank \n" + addSchema(cmdString) + "\n\n"
					+ e.Message + "\n\n", e.InnerException);
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

				if (!File.Exists(DataBase))
					result = false;
				else

					Connection.Open();
				ConnectionWithoutTransaction.Open();

				return result;
			}
		}

		public BindingSource DataBindingInit(string cmdstring, string tabelle)
		{
			BindingSource DataBinding = new BindingSource();

			DataTable table = new DataTable();

			// Create a new data adapter based on the specified query.
			SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmdstring, Connection);			

			// Create a command builder to generate SQL update, insert, and delete commands based on selectCommand.
			//These are used to update the database.
			SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);

			table.Locale = System.Globalization.CultureInfo.InvariantCulture;// Populate a new data table and bind it to the BindingSource.

			dataAdapter.Fill(table);
			this.dataAdapter.Add(tabelle,dataAdapter);

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

		public void CreateDatabase()
		{
			Connection = new SQLiteConnection("Data Source=" + DataBase);
			Connection.Open();
			string cmd = "Create table IF NOT EXISTS sysTableSchema (Tabelle string, feld string, datatyp string, Length Int64)";
			Execute(cmd);
			//connection.Close();
		}

		public Boolean DatabaseExists
		{
			get
			{
				return File.Exists(DataBase);
			}
		}
	}
}