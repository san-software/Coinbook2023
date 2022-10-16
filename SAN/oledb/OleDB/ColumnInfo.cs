namespace OleDB
{
	using System;
	using System.Text;
	using System.Data;
	using System.Data.OleDb;
	using System.Collections;
	using System.Configuration;
	using System.Windows.Forms;
	using SAN.Converter;

	/// <summary>
	/// Summary description for OleDBZugriff.
	/// </summary>

	public class Columninfo
	{
		private int colNum;
		private DataRowCollection tableSchema;

		public Columninfo(int col, DataRowCollection tableSchema)
		{
			this.colNum = col;
			this.tableSchema = tableSchema;
		}

		public string ColumnName
		{
			get
			{
				return tableSchema[colNum]["ColumnName"].ToString();
			}
		}

		public string DataType
		{
			get
			{
				return tableSchema[colNum]["DataType"].ToString();
			}
		}

		public int ColumnSize
		{
			get
			{
				return (int)tableSchema[colNum]["ColumnSize"];
			}
		}
	}
}
