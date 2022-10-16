using System;
using System.Data;
using System.Windows.Forms;

namespace SAN.UI.DataGridView
{
	/// <summary>
	/// Delegate for use with <see cref="GridFilterEventArgs"/>.
	/// </summary>
  public delegate void GridFilterEventHandler(object sender, GridFilterEventArgs args);

	/// <summary>
	/// Argumentsclass for events needing extended informations about <see cref="IGridFilter"/>s.
	/// </summary>
	public class GridFilterEventArgs : EventArgs
	{
		#region Fields

    private DataGridViewColumn column;
		private IGridFilter gridFilter;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance
		/// </summary>
		/// <param name="column">Column the <see cref="IGridFilter"/> is created for.</param>
		/// <param name="gridFilter">Default <see cref="IGridFilter"/> instance.</param>
        public GridFilterEventArgs(DataGridViewColumn column, IGridFilter gridFilter)
		{
			this.column = column;
			this.gridFilter = gridFilter;
		}

		#endregion

		#region Public Interface

		/// <summary>
		/// Type of the column the <see cref="IGridFilter"/> is created for.
		/// </summary>
		public Type DataType 
		{
			get { return column.ValueType; }
		}

		/// <summary>
		/// Name of the column the <see cref="IGridFilter"/> is created for.
		/// </summary>
		public string ColumnName 
		{
			get { return column.DataPropertyName; }
		}

		/// <summary>
		/// The column the <see cref="IGridFilter"/> is created for.
		/// </summary>
        public DataGridViewColumn Column 
		{
			get { return column; }
		}

		/// <summary>
		/// Text of the header of the column the <see cref="IGridFilter"/> is created for.
		/// </summary>
		public string HeaderText 
		{
			get { return column.HeaderText; }
		}

		/// <summary>
		/// Gets/sets the <see cref="IGridFilter"/> which should be used.
		/// </summary>
		public IGridFilter GridFilter 
		{
			get { return gridFilter; }
			set { gridFilter = value; }
		}

		#endregion
	}
}
