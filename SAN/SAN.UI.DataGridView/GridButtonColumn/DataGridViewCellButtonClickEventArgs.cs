using System;
using System.Collections.Generic;
using System.Text;

namespace SAN.UI.DataGridView
{
		public delegate void DataGridViewCellButtonClickEventHandler(object sender, DataGridViewCellButtonClickEventArgs e);

	public class DataGridViewCellButtonClickEventArgs : EventArgs
	{
		private int row;
		private int col;
		private string colName;

		public DataGridViewCellButtonClickEventArgs(int row, int col,string columnName)
		{
			this.row = row;
			this.col = col;
			this.colName = columnName;
		}

		public int RowIndex
		{
			get
			{
				return row;
			}
		}
		public int ColIndex
		{
			get
			{
				return col;
			}
		}

		public String ColumnName
		{
			get
			{
				return colName;
			}
		}
	}
}
