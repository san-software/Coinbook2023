using System;
using System.Collections.Generic;
using System.Text;

namespace SAN.UI.DataGridView
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
		public delegate void DataGridViewCellButtonClickEventHandler(object sender, DataGridViewCellButtonClickEventArgs e);

	/// <summary>
	/// 
	/// </summary>
	public class DataGridViewCellButtonClickEventArgs : EventArgs
	{
		private int row;
		private int col;
		private string colName;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <param name="columnName"></param>
		public DataGridViewCellButtonClickEventArgs(int row, int col,string columnName)
		{
			this.row = row;
			this.col = col;
			this.colName = columnName;
		}

		/// <summary>
		/// 
		/// </summary>
		public int RowIndex
		{
			get
			{
				return row;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int ColIndex
		{
			get
			{
				return col;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public String ColumnName
		{
			get
			{
				return colName;
			}
		}
	}
}
