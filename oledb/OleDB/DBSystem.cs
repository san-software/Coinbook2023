using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
//using System.Data.SQLite;

namespace OleDB
{
	public class DBSystem
	{
		private int datenbank = 0;		// Datenbanknummer, default = 0

		private DBConfig configuration = new DBConfig();
		private List<string> tabelle = new List<string>();
		private List<FieldAttributes> fields = new List<FieldAttributes>();
		private Dictionary<string, string> primaryKey = new Dictionary<string, string>();
		private List<ForeignKey> foreignkeys = new List<ForeignKey>();

		OleDbConnection oledbConnection;
		SqlConnection sqlConnection;
		//SQLiteConnection sqliteConnection;
		Databases datenbankTyp;

		public DBSystem(int datenbank)
		{
			this.datenbank = datenbank;
			Init();
		}

		public void Init()
		{
			string file = "database.config";

			if (OleDBHelper.ConfigFile != "" && OleDBHelper.ConfigFile != null)
				file = OleDBHelper.ConfigFile;

			if (OleDBHelper.FileExists(file))
				configuration.readConnectionFromXML("","");
			else
				configuration.readConnectionFromConfig("");
		}

		public string Tabelle { get; set; }

		public string Schema { get; set; }

		public void AddTable(string tablename)
		{
			tabelle.Add(tablename);
		}

		public void AddField(string tablename, string field, DataTyp dataType, int size, int numericPrecision, int numericScale, bool allowDBNull,
			bool unique, bool key, bool autoincrement)
		{
			FieldAttributes f = new FieldAttributes();

			f.Table = tablename;
			f.Field = field;
			f.DataType = dataType;
			f.Size = size;
			f.NumericPrecision = numericPrecision;
			f.NumericScale = numericScale;
			f.AllowDBNull = allowDBNull;
			f.Unique = unique;
			f.Key = key;
			f.AutoIncrement = autoincrement;

			switch (dataType)
			{
				case DataTyp.Bigint:
					f.DataTypeDotNet = "System.Int64";
					break;

				case DataTyp.Binary:
					break;

				case DataTyp.Bit:
					f.DataTypeDotNet = "System.Boolean";
					break;

				case DataTyp.Bool:
					f.DataTypeDotNet = "System.Boolean";
					break;

				case DataTyp.Char:
					f.DataTypeDotNet = "System.String";
					break;

				case DataTyp.Date:
					f.DataTypeDotNet = "System.DateTime";
					break;

				case DataTyp.DateTime:
					f.DataTypeDotNet = "System.DateTime";
					break;

				case DataTyp.Decimal:
					f.DataTypeDotNet = "System.Decimal";
					break;

				case DataTyp.Float:
					f.DataTypeDotNet = "System.Double";
					break;

				case DataTyp.Image:
					//f.DataTypeDotNet =
					break;

				case DataTyp.Int:
					f.DataTypeDotNet = "System.Int32";
					break;

				case DataTyp.Money:
					f.DataTypeDotNet = "System.Decimal";
					break;

				case DataTyp.Nchar:
					f.DataTypeDotNet = "System.String";
					break;

				case DataTyp.Ntext:
					f.DataTypeDotNet = "System.String";
					break;

				case DataTyp.Numeric:
					f.DataTypeDotNet = "System.Decimal";
					break;

				case DataTyp.Nvarchar:
					f.DataTypeDotNet = "System.String";
					break;

				case DataTyp.Real:
					f.DataTypeDotNet = "System.Double";
					break;

				case DataTyp.Smalldatetime:
					f.DataTypeDotNet = "System.DateTime";
					break;

				case DataTyp.Smallint:
					f.DataTypeDotNet = "System.Int16";
					break;

				case DataTyp.Smallmoney:
					f.DataTypeDotNet = "System.Decimal";
					break;

				case DataTyp.Text:
					f.DataTypeDotNet = "System.String";
					break;
			}



			fields.Add(f);
		}

		public void AddPrimaryKey(string tablename, string field)
		{
			primaryKey.Add(tablename, field);
		}

		public void AddForeignKey(string tablename, string referncetable, string field, string referenzfield)
		{
			ForeignKey f = new ForeignKey();

			f.Table = tablename;
			f.RefernceTable = referncetable;
			f.ReferenceField = referenzfield;
			f.Field = field;

			foreignkeys.Add(f);
		}

		private void createTables()
		{
			string cmd = String.Empty;

			for (int i = 0; i < tabelle.Count; i++)
			{
				if (Schema != String.Empty & Schema != null)
					cmd = "CREATE TABLE [" + Schema + "].[" + tabelle[i] + "](" + createFields(tabelle[i]);
				else
					cmd = "CREATE TABLE [" + tabelle[i] + "](" + createFields(tabelle[i]);

				if (createPrimaryKey(tabelle[i]) != String.Empty)
					cmd = cmd + createPrimaryKey(tabelle[i]);

				if (createForeignKey(tabelle[i]) != String.Empty)
					cmd = cmd + createPrimaryKey(tabelle[i]);

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						cmd = cmd.TrimEnd(new char[] { ',' }) + ") ON [PRIMARY]";
						Execute(cmd);
						break;

					case Databases.MSAccess:
						cmd = cmd.TrimEnd(new char[] { ',' }) + ")"; // ON [PRIMARY]";
						Execute(cmd);
						break;

					case Databases.Oracle:
						cmd = cmd.TrimEnd(new char[] { ',' }) + ") ON [PRIMARY]";
						Execute(cmd);
						break;

					case Databases.SQLite:
						cmd = cmd.TrimEnd(new char[] { ',' }) + ")";
						Execute(cmd);

						break;
				}
			}

			if (datenbankTyp == Databases.SQLite)
			{
				cmd = "Create table IF NOT EXISTS sysTableSchema (Tabelle string, ColumnName string, DataType string, ColumnSize Int64, NumericPresicion int, "
				  + "NumericScale int, IsUnique Boolean, IsKey Boolean, AllowDBNull Boolean)";
				Execute(cmd);

				for (int j = 0; j < fields.Count; j++)
				{
					cmd = "Insert into sysTableSchema (Tabelle,ColumnName, DataType, ColumnSize, NumericPresicion, NumericScale) "
							+	"Values('" + fields[j].Table + "','" + fields[j].Field + "','"
							+ fields[j].DataTypeDotNet + "'," + fields[j].Size.ToString() + "," + fields[j].NumericPrecision + "," + fields[j].NumericScale + ")";
					Execute(cmd);
				}
			}
		}

		private string createFields(string table)
		{
			string cmd;

			cmd = "";
			for (int j = 0; j < fields.Count; j++)
			{
				if (fields[j].Table == table)
				{
					cmd = cmd + "[" + fields[j].Field + "] ";

					switch (fields[j].DataType)
					{
						case DataTyp.Bigint:
							cmd = cmd + bigint;
							break;

						case DataTyp.Binary:
							cmd = cmd + fields[j].DataType.ToString() + " (" + fields[j].Size.ToString() + ") ";
							break;

						case DataTyp.Bit:
							cmd = cmd + bit;
							break;

						case DataTyp.Bool:
							cmd = cmd + bit;
							break;

						case DataTyp.Char:
							cmd = cmd + character(fields[j].Size);
							break;

						case DataTyp.Date:
							cmd = cmd + date;
							break;

						case DataTyp.DateTime:
							cmd = cmd + fields[j].DataType.ToString() + " ";
							break;

						case DataTyp.Decimal:
							cmd = cmd + fields[j].DataType.ToString() + " (" + fields[j].NumericPrecision.ToString() + "," + fields[j].NumericScale.ToString() + ") ";
							break;

						case DataTyp.Smalldatetime:
							cmd = cmd + smalldatetime;
							break;

						case DataTyp.Float:
							cmd = cmd + floatType(fields[j].NumericPrecision);
							break;

						case DataTyp.Image:
							cmd = cmd + image(fields[j].Size);
							break;

						case DataTyp.Int:
							cmd = cmd + integer;
							break;

						case DataTyp.Money:
							cmd = cmd + money;
							break;

						case DataTyp.Nchar:
							cmd = cmd + nchar(fields[j].Size);

							break;

						case DataTyp.Ntext:
							cmd = cmd + nvarchar(fields[j].Size);

							break;

						case DataTyp.Numeric:
							cmd = cmd + numeric(fields[j].NumericPrecision, fields[j].NumericScale);
							break;

						case DataTyp.Nvarchar:
							cmd = cmd + nvarchar(fields[j].Size);

							break;

						case DataTyp.Real:
							cmd = cmd + real;
							break;

						case DataTyp.Smallint:
							cmd = cmd + smallint;
							break;

						case DataTyp.Smallmoney:
							cmd = cmd + smallmoney;
							break;

						case DataTyp.Text:
							cmd = cmd + varchar(fields[j].Size);

							break;

						case DataTyp.Time:
							cmd = cmd + time(fields[j].NumericScale);
							break;

						case DataTyp.Timestamp:
							break;

						case DataTyp.Tinyint:
							cmd = cmd + tinyint;
							break;

						case DataTyp.Uniqueidentifier:
							cmd = cmd + guid;
							break;

						case DataTyp.Varbinary:
							cmd = cmd + varbinary(fields[j].Size);
							break;

						case DataTyp.Varchar:
							cmd = cmd + varchar(fields[j].Size);
							break;

						case DataTyp.Xml:
							cmd = cmd + fields[j].DataType.ToString() + " ";
							break;

						default:
							MessageBox.Show("Fehler beim Erzeugen einer Spalte vom Typ " + fields[j].DataType.ToString());
							break;
					}

					if (datenbankTyp == Databases.SQLite)
						if (primaryKey.ContainsKey(table))
							if (primaryKey[table] == fields[j].Field)
								cmd = cmd + "PRIMARY KEY ";

					cmd = cmd + ",";
				}
			}

			return cmd.Substring(0, cmd.Length);
		}

		private string varchar(long size)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					if (size != 0)
						result = "varchar (" + size.ToString() + ") ";
					else
						result = "varchar (max) ";
					break;

				case Databases.MSAccess:
					result = "Text (" + size.ToString() + ") ";
					break;

				case Databases.SQLite:
					result = "String ";

					break;
			}
			return result;
		}

		private string nvarchar(long size)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					if (size != 0)
						result = "nvarchar (" + size.ToString() + ") ";
					else
						result = "nvarchar (max) ";
					break;

				case Databases.MSAccess:
					result = "Text (" + size.ToString() + ") ";
					break;

				case Databases.SQLite:
					result = "String ";
					break;
			}

			return result;
		}

		private string character(long size)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					result = "character (" + size.ToString() + ") ";
					break;

				case Databases.MSAccess:
					result = "character (" + size.ToString() + ") ";
					break;

				case Databases.SQLite:
					result = "String ";
					break;
			}

			return result;
		}

		private string nchar(long size)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					result = "nchar (" + size.ToString() + ") ";
					break;

				case Databases.MSAccess:
					result = "Text (" + size.ToString() + ") ";
					break;

				case Databases.SQLite:
					result = "String ";
					break;
			}
			return result;
		}

		private string varbinary(long size)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:

					if (size != 0)
						result = "varbinary (" + size.ToString() + ") ";
					else
						result = "varbinary (max) ";
					break;

				case Databases.MSAccess:
					result = "OLEOBJECT ";
					break;

				case Databases.SQLite:
					result = "Binary";
					break;
			}

			return result;
		}

		private string bigint
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "bigint ";
						break;

					case Databases.MSAccess:
						result = "float ";
						break;

					case Databases.SQLite:
						result = "Int64";
						break;
				}
				return result;
			}
		}

		private string integer
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "int ";
						break;

					case Databases.MSAccess:
						result = "integer ";
						break;

					case Databases.SQLite:
						result = "Int32 ";
						break;
				}
				return result;
			}
		}
		private string date
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "date ";
						break;

					case Databases.MSAccess:
						result = "DateTime ";
						break;

					case Databases.SQLite:
						result = "DateTime ";
						break;
				}
				return result;
			}
		}

		private string smalldatetime
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "Smalldatetime ";
						break;

					case Databases.MSAccess:
						result = "DateTime ";
						break;

					case Databases.SQLite:
						result = "DateTime ";
						break;
				}
				return result;
			}
		}

		private string image(long size)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					result = varbinary(size);
					break;

				case Databases.MSAccess:
					result = "Image ";
					break;

				case Databases.SQLite:
					result = "Binary ";
					break;
			}
			return result;
		}

		private string numeric(int precision, int scale)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					result = "Numeric(" + precision.ToString() + "," + scale.ToString() + ") ";
					break;

				case Databases.MSAccess:
					result = "Decimal(" + precision.ToString() + "," + scale.ToString() + ") ";
					break;
			}
			return result;
		}

		private string floatType(int precision)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					result = "Float(" + precision.ToString() + ") ";
					break;

				case Databases.MSAccess:
					result = "Float(" + precision.ToString() + ") ";
					break;

				case Databases.SQLite:
					result = "Double ";
					break;
			}
			return result;
		}

		private string smallmoney
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "Smallmoney ";
						break;

					case Databases.MSAccess:
						result = "Money ";
						break;

					case Databases.SQLite:
						result = "Decimal ";
						break;
				}
				return result;
			}
		}

		private string bit
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "Bit ";
						break;

					case Databases.MSAccess:
						result = "Bit ";
						break;

					case Databases.SQLite:
						result = "Boolean ";
						break;

				}
				return result;
			}
		}

		private string guid
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "Uniqueidentifier ";
						break;

					case Databases.MSAccess:
						result = "Uniqueidentifier ";
						break;

					case Databases.SQLite:
						result = "Guit ";
						break;

				}
				return result;
			}
		}
		private string tinyint
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "tinyint ";
						break;

					case Databases.MSAccess:
						result = "tinyint ";
						break;

					case Databases.SQLite:
						result = "Byte ";
						break;

				}
				return result;
			}
		}

		private string money
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "Money ";
						break;

					case Databases.MSAccess:
						result = "Money ";
						break;

					case Databases.SQLite:
						result = "Decimal ";
						break;

				}
				return result;
			}
		}

		private string smallint
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "smallint ";
						break;

					case Databases.MSAccess:
						result = "smallint ";
						break;

					case Databases.SQLite:
						result = "Int16 ";
						break;

				}
				return result;
			}
		}
		private string real
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "Real ";
						break;

					case Databases.MSAccess:
						result = "Real ";
						break;

					case Databases.SQLite:
						result = "Single ";
						break;

				}
				return result;
			}
		}


		private string xml
		{
			get
			{
				string result = "";

				switch (datenbankTyp)
				{
					case Databases.SQLServer:
						result = "xml ";
						break;

					case Databases.MSAccess:
						result = "Text ";
						break;
				}
				return result;
			}
		}

		private string time(int scale)
		{
			string result = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					result = "Time (" + scale.ToString() + ") ";
					break;

				case Databases.MSAccess:
					result = "DateTime ";
					break;

				case Databases.SQLite:
					result = "DateTime ";
					break;
			}
			return result;
		}

		private string createPrimaryKey(string table)
		{
			string cmd = "";

			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					if (primaryKey.ContainsKey(table))
						cmd = " CONSTRAINT [" + table + "_PK] PRIMARY KEY NONCLUSTERED([" + primaryKey[table] + "] ASC) "
							+ "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ";
					break;

				case Databases.MSAccess:
					if (primaryKey.ContainsKey(table))
						cmd = " CONSTRAINT [" + table + "_PK] PRIMARY KEY ([" + primaryKey[table] + "]) ";
					break;

				case Databases.Oracle:
					if (primaryKey.ContainsKey(table))
						cmd = " CONSTRAINT [" + table + "_PK] PRIMARY KEY NONCLUSTERED([" + primaryKey[table] + "] ASC) "
							+ "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ";
					break;

				case Databases.SQLite:
					break;
			}
			return cmd;
		}

		private string createForeignKey(string table)
		{
			string cmd = "";

			for (int i = 0; i < foreignkeys.Count; i++)
			{
				if (foreignkeys[i].Table == table)
					cmd = cmd + " CONSTRAINT [" + table + "_" + foreignkeys[i].RefernceTable + " _FK] FOREIGN KEY ([" + foreignkeys[i].Field + "]) "
						+ "REFERENCES " + foreignkeys[i].RefernceTable + "([" + foreignkeys[i].ReferenceField + "]) ON DELETE CASCADE ON UPDATE CASCADE ";
			}
			return cmd;
		}

		public void CreateDatabase()
		{
			string connectionString;
			string cmd;

			frmAnzeige anzeige = new frmAnzeige();
			anzeige.Show();
			Application.DoEvents();

			datenbankTyp = configuration.DatenbankTyp[datenbank];
			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					cmd = "CREATE DATABASE mytest";

					sqlConnection = new SqlConnection("Server=" + configuration.Host[datenbank] + ";Integrated security=SSPI;database=master");
					SqlCommand sqlCommand = new SqlCommand(cmd, sqlConnection);

					sqlConnection.Open();
					sqlCommand.ExecuteNonQuery();

					sqlConnection.Close();

					connectionString = "Data Source=" + configuration.Provider[datenbank] + ";Initial Catalog=" + configuration.DataBase[datenbank] 
						+ ";Integrated Security=SSPI;Persist Security Info=True";

					sqlConnection = new SqlConnection(connectionString);
					sqlConnection.Open();

					createTables();
					break;

				case Databases.MSAccess:
					ADOX.Catalog cat = new ADOX.Catalog();
					cat.Create(@"Provider=" + configuration.Provider[datenbank] + ";Data Source=" + configuration.DataBase[datenbank] + ";Jet OLEDB:Engine Type=5");

					connectionString = "Provider=" + configuration.Provider[datenbank] + ";Data Source=" + configuration.DataBase[datenbank]
						+ ";User ID=" + configuration.User[datenbank] + ";Password=" + configuration.Password[datenbank] + ";";
					oledbConnection = new OleDbConnection(connectionString);
					oledbConnection.Open();

					createTables();
					break;

				case Databases.Oracle:
					break;

				//case Databases.SQLite:
				//  sqliteConnection = new SQLiteConnection("Data Source=" + configuration.DataBase[datenbank]);
				//  sqliteConnection.Open();

				//  createTables();
				//  break;
			}

			anzeige.Close();
		}

		public void Execute(string cmdstring)
		{
			switch (datenbankTyp)
			{
				case Databases.SQLServer:
					SqlCommand sqlcmd = new SqlCommand(cmdstring, sqlConnection);
					sqlcmd.ExecuteNonQuery();
					sqlcmd.Dispose();
					break;

				case Databases.MSAccess:
					OleDbCommand oledbcmd = new OleDbCommand(cmdstring, oledbConnection);
					oledbcmd.ExecuteNonQuery();
					oledbcmd.Dispose();
					break;

				case Databases.Oracle:
					OleDbCommand oraclecmd = new OleDbCommand(cmdstring, oledbConnection);
					oraclecmd.ExecuteNonQuery();
					oraclecmd.Dispose();
					break;

				//case Databases.SQLite:
				//  SQLiteCommand sqlitecmd = new SQLiteCommand(cmdstring, sqliteConnection);
				//  sqlitecmd.ExecuteNonQuery();
				//  sqlitecmd.Dispose();
				//  break;
			}
		}

		//public Boolean DatabaseExists
		//{
		//  get
		//  {
		//    Boolean result = true;

		//    switch (datenbankTyp)
		//    {
		//      case Databases.SQLServer:
		//        result = ((DBConnectionSQLServer)OleDBConnection.Connections[datenbank]).DatabaseExists;
		//        break;

		//      case Databases.MSAccess:
		//        result = ((DBConnectionAccess)OleDBConnection.Connections[datenbank]).DatabaseExists;
		//        createTables();
		//        break;

		//      case Databases.Oracle:
		//        result = ((DBConnectionOracle)OleDBConnection.Connections[datenbank]).DatabaseExists;
		//        break;

		//      case Databases.SQLite:
		//        result = ((DBConnectionSQLite)OleDBConnection.Connections[datenbank]).DatabaseExists;
		//        createTables();
		//        break;
		//    }

		//    return result;
		//  }
		//}

	}
}


//USE [master]
//GO

///****** Object:  Database [Garantiemanager]    Script Date: 10.09.2014 21:57:49 ******/
//CREATE DATABASE [Garantiemanager] ON  PRIMARY 
//( NAME = N'Garantiemanager_dat', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\Garantiemanager.mdf' , SIZE = 2240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
// LOG ON 
//( NAME = N'Garantiemanager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\Garantiemanager.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
//GO

//ALTER DATABASE [Garantiemanager] SET COMPATIBILITY_LEVEL = 100
//GO

//IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
//begin
//EXEC [Garantiemanager].[dbo].[sp_fulltext_database] @action = 'enable'
//end
//GO

//ALTER DATABASE [Garantiemanager] SET ANSI_NULL_DEFAULT OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET ANSI_NULLS OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET ANSI_PADDING OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET ANSI_WARNINGS OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET ARITHABORT OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET AUTO_CLOSE ON 
//GO

//ALTER DATABASE [Garantiemanager] SET AUTO_CREATE_STATISTICS ON 
//GO

//ALTER DATABASE [Garantiemanager] SET AUTO_SHRINK OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET AUTO_UPDATE_STATISTICS ON 
//GO

//ALTER DATABASE [Garantiemanager] SET CURSOR_CLOSE_ON_COMMIT OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET CURSOR_DEFAULT  GLOBAL 
//GO

//ALTER DATABASE [Garantiemanager] SET CONCAT_NULL_YIELDS_NULL OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET NUMERIC_ROUNDABORT OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET QUOTED_IDENTIFIER OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET RECURSIVE_TRIGGERS OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET  ENABLE_BROKER 
//GO

//ALTER DATABASE [Garantiemanager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET DATE_CORRELATION_OPTIMIZATION OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET TRUSTWORTHY OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET PARAMETERIZATION SIMPLE 
//GO

//ALTER DATABASE [Garantiemanager] SET READ_COMMITTED_SNAPSHOT OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET HONOR_BROKER_PRIORITY OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET RECOVERY SIMPLE 
//GO

//ALTER DATABASE [Garantiemanager] SET  MULTI_USER 
//GO

//ALTER DATABASE [Garantiemanager] SET PAGE_VERIFY CHECKSUM  
//GO

//ALTER DATABASE [Garantiemanager] SET DB_CHAINING OFF 
//GO

//ALTER DATABASE [Garantiemanager] SET  READ_WRITE 
//GO


//----------------------

//CREATE TABLE [dbo].[tblBeleg](
//  [id] [int] NOT NULL,
//  [Bild] [image] NULL,
// CONSTRAINT [aaaaatblBeleg_PK] PRIMARY KEY NONCLUSTERED 
//(
//  [id] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


//------------------------------

//CREATE TABLE [dbo].[tblBelegKategorie](
//  [id] [int] NOT NULL,
//  [Bezeichnung] [nvarchar](50) NULL,
//  [Extention] [nvarchar](10) NULL,
// CONSTRAINT [aaaaatblBelegKategorie_PK] PRIMARY KEY NONCLUSTERED 
//(
//  [id] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY]

//---------------------------------

//CREATE TABLE [dbo].[tblHaendler](
//  [ID] [int] NOT NULL,
//  [Haendler] [nvarchar](50) NULL,
//  [Strasse] [nvarchar](50) NULL,
//  [PLZ] [nvarchar](10) NULL,
//  [Ort] [nvarchar](50) NULL,
// CONSTRAINT [aaaaatblHaendler_PK] PRIMARY KEY NONCLUSTERED 
//(
//  [ID] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY]

//----------------------------------

//CREATE TABLE [dbo].[tblHersteller](
//  [ID] [int] NOT NULL,
//  [Name] [nvarchar](50) NULL,
// CONSTRAINT [aaaaatblHersteller_PK] PRIMARY KEY NONCLUSTERED 
//(
//  [ID] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY]

//-----------------------

//CREATE TABLE [dbo].[tblKategorie](
//  [ID] [int] NOT NULL,
//  [Bezeichnung] [nvarchar](30) NULL,
// CONSTRAINT [aaaaatblKategorie_PK] PRIMARY KEY NONCLUSTERED 
//(
//  [ID] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY]

//-------------------------

//CREATE TABLE [dbo].[tblProdukt](
//  [ID] [int] NOT NULL,
//  [HaendlerID] [int] NULL,
//  [HerstellerID] [int] NULL,
//  [Haendler] [nvarchar](50) NULL,
//  [Strasse] [nvarchar](50) NULL,
//  [PLZ] [nvarchar](10) NULL,
//  [Ort] [nvarchar](50) NULL,
//  [Verkaeufer] [nvarchar](50) NULL,
//  [Hersteller] [nvarchar](50) NULL,
//  [Produkt] [nvarchar](50) NULL,
//  [Seriennummer] [nvarchar](50) NULL,
//  [Model] [nvarchar](50) NULL,
//  [Kaufdatum] [datetime] NULL,
//  [Garantielaufzeit] [int] NULL,
//  [Garantieerinnerung1] [int] NULL,
//  [Garantieerinnerung2] [int] NULL,
// CONSTRAINT [aaaaatblProdukt_PK] PRIMARY KEY NONCLUSTERED 
//(
//  [ID] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY]
