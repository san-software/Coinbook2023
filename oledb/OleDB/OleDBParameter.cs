using System;
using System.Collections;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace OleDB
{
	public class OleDBParameter
	{
		public OleDBParameter()
		{
			Parameter = new ArrayList();
		}

		public ArrayList Parameter
		{
			get;
			set;
		}

		public void AddInput(string name, object value)
		{
			Add(name, value, ParameterDirection.Input);
		}

		public void AddInputOutput(string name, object value)
		{
			Add(name, value, ParameterDirection.InputOutput);
		}

		public void AddOutput(string name, object value)
		{
			Add(name, value, ParameterDirection.Output);
		}

		private void Add(string name, object value, ParameterDirection direction)
		{
			OleDbParameter parameter = new OleDbParameter();

			parameter.ParameterName = name;
			parameter.Value = value;
			parameter.Direction = direction;

			switch (value.GetType().ToString())
			{
				case "System.Int64":
					parameter.OleDbType = OleDbType.BigInt;
					break;

				case "System.String":
					parameter.OleDbType = OleDbType.LongVarChar;
					break;
			}

			Parameter.Add(parameter);
		}
	}
}
