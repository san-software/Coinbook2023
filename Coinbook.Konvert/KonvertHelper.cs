using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Coinbook.Konvert
{
	public static class Helper
	{
		public static string CommonAppDataPath
		{
			get
			{
				string result = Application.CommonAppDataPath;
				int pos = result.LastIndexOf(@"\");
				result = result.Substring(0, pos);
				return result;
			}
		}
	}
}



