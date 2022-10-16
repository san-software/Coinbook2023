using System;
using System.Collections.Generic;
using System.Text;

namespace SAN.UI.DataGridView
{
	/// <summary>
	/// Modes which determine the output generated when an error
	/// in the builded filter criterias occurs.
	/// </summary>
	[Flags]
	public enum FilterErrorModes
	{
		/// <summary>
		/// No error output at all
		/// </summary>
		Off = 0,
		/// <summary>
		/// General error message
		/// </summary>
		General = 1,
		/// <summary>
		/// Message of the exception that occured
		/// </summary>
		ExceptionMessage = 2,
		/// <summary>
		/// StackTrace of the exception that occured
		/// </summary>
		StackTrace = 4,
		/// <summary>
		/// All available output
		/// </summary>
		All = 7
	}
}
