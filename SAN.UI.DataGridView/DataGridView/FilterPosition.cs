using System;
using System.Collections.Generic;
using System.Text;

namespace SAN.UI.DataGridView
{
	/// <summary>
	/// Enumeration representing the regions where the filter GUI elements
	/// are shown.
	/// </summary>
	public enum FilterPosition
	{
		/// <summary>
		/// Filter GUI above the grid.
		/// </summary>
		Top,
		/// <summary>
		/// Filter GUI beyond the grid.
		/// </summary>
		Bottom,
		/// <summary>
		/// Turns off the filter
		/// </summary>
		Off
	}
}
