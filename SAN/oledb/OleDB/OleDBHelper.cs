using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace OleDB
{
	public class OleDBHelper
	{

		public static bool FileExists(string filename)
		{
			FileInfo file = new FileInfo(filename);

			return file.Exists;
		}

		public static string ConfigFile
		{
			get;
			set;
		}

		public static int CreateAccessDB(string provider, string dataSource)
		{
			int result = -1;

			//ADOX.CatalogClass cat = new ADOX.CatalogClass();
			//try
			//{
			//  cat.Create("Provider=" + provider + ";Data Source=" + dataSource + ";Jet OLEDB:Engine Type=5");
			//  result = 0;
			//}
			//catch
			//{
			//}
			//cat = null;

			return result;
		}


	}
}


//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace Datenbanken_erzeugen
//{
//class Start
//{
//[STAThread]
//static void Main(string[] args)
//{
//// Verbindung zum lokalen SQL Server aufbauen
//SqlConnection connection = null;
//try
//{
//connection = 
//new SqlConnection("Server=(local);Trusted_Connection=Yes");
//connection.Open();

//// Datenbank erzeugen
//string databaseName = "Bookstore";
//string sql = "CREATE Database " + databaseName;
//SqlCommand command = new SqlCommand(sql, connection);
//command.ExecuteNonQuery();

//// Verbindung zur neuen Datenbank aufbauen
//connection.Close();
//connection = 
//new SqlConnection("Server=(local);Database=" + databaseName + ";" +
//"Trusted_Connection=Yes");
//connection.Open();

//// Tabellen erzeugen
//string sql1 = "CREATE TABLE Authors (" +
//"Id int NOT NULL PRIMARY KEY IDENTITY," +
//"FirstName nvarchar(255) NOT NULL," +
//"LastName nvarchar(255) NOT NULL)";

//string sql2 = "CREATE TABLE Books (" +
//"Id int NOT NULL PRIMARY KEY IDENTITY," +
//"Title nvarchar(255) NOT NULL," +
//"ISBN nvarchar(255) NOT NULL," +
//"PublishingDate datetime, " +
//"Price money NOT NULL DEFAULT 0)";

//string sql3 = "CREATE TABLE BookAuthors (" +
//"Id int NOT NULL PRIMARY KEY," +
//"BookId int NOT NULL," +
//"AuthorId int NOT NULL)";

//// Die Beziehungen zwischen den Tabellen definieren
//string sql4 = "ALTER TABLE BookAuthors " +
//"ADD CONSTRAINT FK_Books FOREIGN KEY " +
//"(BookId) REFERENCES Books(Id)";

//string sql5 = "ALTER TABLE BookAuthors " +
//"ADD CONSTRAINT FK_Authors FOREIGN KEY " +
//"(AuthorId) REFERENCES Authors(Id)";

//// Den endgültigen SQL-String zusammensetzen und ausführen
//sql = sql1 + "\r\n" + sql2 + "\r\n" + sql3 + "\r\n" +
//sql4 + "\r\n" + sql5;
//command = new SqlCommand(sql, connection);
//command.ExecuteNonQuery();
//}
//catch (Exception ex)
//{
//Console.WriteLine(ex.Message);
//}
//finally
//{
//try 
//{
//connection.Close();
//}
//catch {}
//}

//Console.WriteLine("Beenden mit Return");
//Console.ReadLine();
