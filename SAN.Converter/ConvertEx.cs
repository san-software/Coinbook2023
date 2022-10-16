using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace SAN.Converter
{
	/// <summary>
	/// Summary description for ConvertEx.
	/// </summary>
	public class ConvertEx 
	{
		public ConvertEx()
		{
			
		}

		#region Image
		public static byte[] ImageToByteArray(Image imageIn)
		{
			MemoryStream ms = new MemoryStream();
			imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
			return ms.ToArray();
		}

		public static Image ByteArrayToImage(byte[] byteArrayIn)
		{
			MemoryStream ms = new MemoryStream(byteArrayIn);
			Image returnImage = Image.FromStream(ms);
			return returnImage;
		}
		#endregion

		#region Convert to Int64
		public static Int64 ToInt64 (string value , Int32 fromBase )
		{
			if (value.Length == 0)
				return 0;

			return Convert.ToInt64(value,fromBase);
		}

		public static Int64 ToInt64 (string value)
		{
			long result = 0;
			Int64.TryParse(value, out result);
			return result;
		}

		public static Int64 ToInt64 (object value)
		{
			if (value == null)
				return 0;

			if (value == DBNull.Value)
				return 0;

			if (value.ToString() == "")
				return 0;

			else
				return Convert.ToInt64(value);
		}
		#endregion

		#region Convert to Int32
		public static Int32 ToInt32 (string value , Int32 fromBase )
		{
			if (value.Length == 0)
				return 0;
			else
				return Convert.ToInt32(value,fromBase);
		}

		public static Int32 ToInt32 (string value)
		{
			int result = 0;
			Int32.TryParse(value, out result);
			return result;
		}

		public static Int32 ToInt32 (object value)
		{
			Int32 number = 0;

			if (value != null && value != DBNull.Value)
			{
				switch (value.GetType().ToString())
				{
					case "System.String":
						Int32.TryParse(value.ToString(), out number);
						break;
					default:
						number = Convert.ToInt32(value);
						break;
				}
			}
			return number;
		}
		#endregion

		#region Convert to Int16
		public static Int16 ToInt16 (string value , Int32 fromBase )
		{
			Int16 number = 0;

			if (Int16.TryParse(value, out number))
				return Convert.ToInt16(value,fromBase);
			else
				return 0;
		}

		public static Int16 ToInt16 (string value)
		{
			Int16 number = 0;

			if (Int16.TryParse(value, out number))
				return number;
			else
				return 0;
		}

		public static Int16 ToInt16 (object value)
		{
			if (value == null || value.ToString().Length == 0)
				return 0;
			else
				return Convert.ToInt16(value);
		}
		#endregion

		#region Convert to Double
		//Rückgabe NAN wenn Null
		public static double ToDouble (string value)
		{
			double result = double.NaN;
			if (value.Length != 0)
				if (value == "-" || value == "+" || value == "." || value.ToString().Trim() == "")
					result = 0;
				else
					result= Convert.ToDouble(value);

			return result;
		}

		public static double ToDouble (object value)
		{
			try
			{
				if (value == null)
					return double.NaN;
				else if (value == DBNull.Value)
					return double.NaN;
				else if (value.ToString().Trim() == "" )
					return double.NaN;
				else
					return Convert.ToDouble(value);
			}
			catch
			{
				return double.NaN;
			}
		}
		
		//Rückgabe 0 wenn Null
		public static double ToDouble0 (string value)
		{
			value = value.Trim();
			if (value.Length == 0)
				return 0;
			else
				return Convert.ToDouble(value);
		}

		public static double ToDouble0 (object value)
		{
			try
			{
				if (value == null)
					return 0;
				else if (value == DBNull.Value)
					return 0;
				else if (value.ToString().Trim() == "" )
					return 0;
				else
					return Convert.ToDouble(value);
			}
			catch
			{
				return double.NaN;
			}
		}
		#endregion

		#region Convert to Decimal
		public static decimal ToDecimal(string value)
		{
			if (string.IsNullOrEmpty(value))
				return 0;
			else if (value.Length == 0)
				return 0;
			else
				return Convert.ToDecimal(value);
		}

		public static decimal ToDecimal(object value)
		{
			try
			{
				if (value == null)
					return 0;
				else if (value == DBNull.Value)
					return 0;
				else if (value.ToString() == "")
					return 0;
				else
					return Convert.ToDecimal(value);
			}
			catch
			{
				return 0;
			}
		}
		#endregion

		#region DB-Konvertierungen
		public static string ToDBNumber(object wert)
		{
			string result = "";

			if (wert == DBNull.Value)
				result = "Null";
			else
			{
				switch (wert.GetType().ToString())
				{
					case "System.Double":
						if(Double.IsNaN(Convert.ToDouble(wert)))
							result = "Null";
						else
						  result = wert.ToString().Replace(",",".");
						break;

					default:
							//					else if(Convert.ToDouble(wert) != double.NaN)
							//						result = "Null";
							result = wert.ToString().Replace(",",".");
						break;
				}
			}

			return result;
		}

		public static string ToDBString(object wert)
		{
			if (wert != DBNull.Value)
				return "'" + wert.ToString().Replace("'","''") + "'";
			else
				return "Null";
		}

		public static string ToDBbool(object value, string databaseTyp)
		{
			bool result = false;
			string returnValue = "";

			if (value == null)
				result = false;
			else if (value == DBNull.Value)
				result = false;
			else if (value.ToString() == "")
				result = false;
			else
				result = Convert.ToBoolean(value);

			switch (databaseTyp)
			{
				case "SQLServer":
					if (result == false)
						returnValue = "0";
					else
						returnValue = "1";

					break;

				case "MSAccess":
					returnValue = result.ToString();
					break;
			}

			return returnValue;
		}

		public static object ConvertToDBNull(object wert)
		{
			if (wert == null || wert == DBNull.Value)
				return DBNull.Value;
			else
				return wert;
		}

		public static Int16 boolToInt16(bool flag)
		{
			if (flag)
				return -1;
			else
				return 0;
		}

		public static string ToDBDate(string dbTyp, object wert)
		{
			string result = "Null";

			if (wert != DBNull.Value)
			{
				try
				{
					DateTime datum = Convert.ToDateTime(wert);
					string Day = datum.Day.ToString();
					string Month = datum.Month.ToString();
					string Year = datum.Year.ToString();

					switch (dbTyp)
					{
						case "MSAccess":
							result = "#" + Month.PadLeft(2, '0') + "/" + Day.PadLeft(2, '0') + "/" + Year.PadLeft(4, '0') + "#";
							break;

						case "SQLServer":
							//result = "'" + Day.PadLeft(2, '0') + "." + Month.PadLeft(2, '0') + "." + Year.PadLeft(4, '0') + "'";
							result = "'" + Year.PadLeft(4, '0') + Month.PadLeft(2, '0') + Day.PadLeft(2, '0') + "'";
							break;
					}
				}
				catch
				{
				}
			}

			return result;
		}
		#endregion

		#region Convert To Single
		public static Single ToSingle(string value)
		{
			if (value.Length == 0)
				return Single.NaN;
			else
				return Convert.ToSingle(value);
		}

		public static Single ToSingle(object value)
		{
			Single wert = 0;

			if (value == null || value == DBNull.Value)
				return Single.NaN;
			else
			{
				if (Single.TryParse(value.ToString(), out wert))
					return wert;
				else
					return Single.NaN;
			}
		}
		#endregion

		public static bool IsNumber(object value)
		{
			bool result = true;
			double wert;

			if (value == null || value == DBNull.Value)
				result = false;
			else
				result=(Double.TryParse(value.ToString(), out wert));
			
			return result;
		}

		public static string ToString(object value)
		{
			string result = "";

			if (!Convert.IsDBNull(value))
				result = Convert.ToString(value);

			return result;
		}

		public static double NaNTo0(double value)
		{
			if (Double.IsNaN(value))
				return 0;
			else
				return value;
		}

		public static bool ToBoolean(object value)
		{
			if (value == null)
				return false;
			else if (value == DBNull.Value)
				return false;
			else if (value.ToString() == "")
				return false;
            else if (value.ToString() == "0")
                return false;
            else if (value.ToString() == "1")
                return true;
            else
                return Convert.ToBoolean(value);
		}

		public static DateTime? ToDateTimeNull(object value)
		{
		  if (value == DBNull.Value)
				value = null;
			
			if (value != null)
				value= Convert.ToDateTime(value);

			return (DateTime?)value;
		}

		//wenn text datum war ist erfolg==true und date hat das konvertierte datum drin
		public static bool IsDate(object value)
		{
			DateTime date;

			if (value == null || value == DBNull.Value)
				return false;
			else
				return DateTime.TryParse(value.ToString(), out date);
		}

		// Function to Check for AlphaNumeric.
		public static bool IsAlphaNumeric(string strToCheck)
		{
			Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");

			return !objAlphaNumericPattern.IsMatch(strToCheck);
		}

		// Function To test for Alphabets.
		public static bool IsAlpha(string strToCheck)
		{
			Regex objAlphaPattern = new Regex("[^a-zA-Z]");

			return !objAlphaPattern.IsMatch(strToCheck);
		}

		// Function to test for Positive Integers.
		public static bool IsNaturalNumber(String strNumber)
		{
			Regex objNotNaturalPattern = new Regex("[^0-9]");
			Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");

			return !objNotNaturalPattern.IsMatch(strNumber) && objNaturalPattern.IsMatch(strNumber);
		}

		// Function to test for Positive Integers with zero inclusive
		public static bool IsWholeNumber(string strNumber)
		{
			Regex objNotWholePattern = new Regex("[^0-9]");

			return !objNotWholePattern.IsMatch(strNumber);
		}

		// Function to Test for Integers both Positive & Negative
		public static bool IsInteger(string strNumber)
		{
			Regex objNotIntPattern = new Regex("[^0-9-]");
			Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");

			return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
		}

		// Function to Test for Positive Number both Integer & Real
		public static bool IsPositiveNumber(string strNumber)
		{
			Regex objNotPositivePattern = new Regex("[^0-9.]");
			Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
			Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");

			return !objNotPositivePattern.IsMatch(strNumber) &&	objPositivePattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber);
		}

		// Function to test whether the string is valid number or not
		public static bool IsNumber(string strNumber)
		{
			Regex objNotNumberPattern = new Regex("[^0-9.-]");
			Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
			Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
			String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
			String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
			Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

			return !objNotNumberPattern.IsMatch(strNumber) &&	!objTwoDotPattern.IsMatch(strNumber) &&	!objTwoMinusPattern.IsMatch(strNumber) 
				&&
			objNumberPattern.IsMatch(strNumber);
		}

		//public static double Eval(string expr)
		//{
		//	CalcQuick calculator = CalcQuick();
		//	CalcEngine engine = new CalcEngine(calculator);

		//	string parsedFormula = engine.ParseFormula(expr);

		//	return double.Parse(parsedFormula);
		//}

	}
}
