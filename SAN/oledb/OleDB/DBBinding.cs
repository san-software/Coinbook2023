using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace OleDB
{
	public class DBBinding
	{
		private int datenbank = 0;		// Datenbanknummer, default = 0
		private int adapterIndex = 0;
		private BindingSource dataBinding;
		private DataRowCollection tableSchema;

		public DBBinding(string tabelle)
		{
			Tabelle = tabelle;
			DataBindingInit();
		}

		public DBBinding(int datenbank, string tabelle)
		{
			this.datenbank = datenbank;
			Tabelle = tabelle;
			DataBindingInit();
		}

		public DBBinding()
		{
		}

		public void Close()
		{
			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).BindingClose(Key);
					break;

				case Databases.MSAccess:
					((DBConnectionAccess)OleDBConnection.Connections[datenbank]).BindingClose(Key);
					break;

				case Databases.Oracle:
					((DBConnectionOracle)OleDBConnection.Connections[datenbank]).BindingClose(Key);
					break;

				//case Databases.SQLite:
				//  ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).BindingClose(Key);
				//  break;
			}
		}

		public string Tabelle { get; set; }
		public string Key { get; set; }

		public void DataBindingInit()
		{
			DataBindingInit("SELECT * FROM " + Tabelle);
		}

		public void DataBindingInit(int id)
		{
			DataBindingInit("SELECT * FROM " + Tabelle + " where id =" + id.ToString());
		}

		public void DataBindingInitFilter(string filter)
		{
			DataBindingInit("SELECT * FROM " + Tabelle + " where " + filter);
		}

		public void DataBindingInitOrderBy(string order)
		{
			DataBindingInit("SELECT * FROM " + Tabelle + " Order By " + order);
		}

		public void DataBindingInitOrderBy(string filter, string order)
		{
			DataBindingInit("SELECT * FROM " + Tabelle + " where " + filter + " Order By " + order);
		}

		public void DataBindingInit(string cmdstring)
		{
			DataBinding = new BindingSource();

			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					DataBinding = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).DataBindingInit(cmdstring, Key);
					tableSchema = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdstring);
					Datatable = (DataTable)DataBinding.DataSource;
					break;

				case Databases.MSAccess:
					DataBinding = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).DataBindingInit(cmdstring, Key);
					tableSchema = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdstring);
					Datatable = (DataTable)DataBinding.DataSource;
					break;

				case Databases.Oracle:
					DataBinding = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).DataBindingInit(cmdstring, Key);
					tableSchema = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdstring);
					Datatable = (DataTable)DataBinding.DataSource;
					break;

				//case Databases.SQLite:
				//  DataBinding = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).DataBindingInit(cmdstring, Key);
				//  tableSchema = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).GetTableSchema(cmdstring);
				//  Datatable = (DataTable)DataBinding.DataSource;
				//  break;
			}
		}

		public BindingSource DataBinding {get;set;}

		public void Save()
		{
			switch (((DBConnection)OleDBConnection.Connections[datenbank]).DatenbankTyp)
			{
				case Databases.SQLServer:
					((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).BindingSave(Key, DataBinding);
					break;

				case Databases.MSAccess:
					((DBConnectionAccess)OleDBConnection.Connections[datenbank]).BindingSave(Key, DataBinding);
					break;

				case Databases.Oracle:
					((DBConnectionOracle)OleDBConnection.Connections[datenbank]).BindingSave(Key, DataBinding);
					break;

				//case Databases.SQLite:
				//  ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).BindingSave(Key, DataBinding);
				//  break;
			}
		}

		public void Undo()
		{
			DataBinding.CancelEdit();
		}

		public DataRow NewRow(string id)
		{
			DataRow newRow = Datatable.NewRow();
			newRow[0] = id;
			for (int i = 1; i < Datatable.Columns.Count;i++)
				switch (Datatable.Columns[i].DataType.ToString())
				{
					case "System.String":
						newRow[i] = " ";
						break;

					case "System.Int32":
						newRow[i] = 0;
						break;

					case "System.Boolean":
						newRow[i] = false;
						break;

					default:
						break;
				}

			Datatable.Rows.Add(newRow);
			return newRow;
		}

		public void DeleteRow(int index)
		{
			DataBinding.RemoveAt(index);
		}

		public void ResetBindings()
		{
			DataBinding.ResetBindings(false);
		}

		public DataTable Datatable { get; set; }

		public long Sequence(string id)
		{
			long result = -1;
			string cmdString = "Select Max(" + id + ") from " + Tabelle;

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

		public int Length(int col)
		{
			return Length(Datatable.Columns[col].ColumnName);
		}

		public int Length(string name)
		{
			int result = 0;

			switch (Datatable.Columns[name].DataType.ToString())
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

				case "System.String":
										for (int row = 0; row < tableSchema.Count; row++)
						if (name.ToLower() == tableSchema[row]["ColumnName"].ToString().ToLower())
							result = Convert.ToInt32(tableSchema[row]["ColumnSize"]);
					break;

				default:
					result = 0;
					for (int row = 0; row < tableSchema.Count; row++)
						if (name.ToLower() == tableSchema[row]["ColumnName"].ToString().ToLower())
							result = Convert.ToInt32(tableSchema[row]["ColumnSize"]);
					break;
			}

			return result;
		}


	}
}
