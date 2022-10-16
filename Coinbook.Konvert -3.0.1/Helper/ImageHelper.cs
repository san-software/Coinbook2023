using Coinbook.Helper;
using ExifLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook
{
    public static class ImageHelper
    {
        //Send list JSON objects with full image Exif information
        public static List<JSONDataFormat> GetFullEXIF(string file)
        {
            List<JSONDataFormat> data = new List<JSONDataFormat>();
            try
            {
                using (var reader = new ExifReader(file))
                {
                    var props = Enum.GetValues(typeof(ExifTags)).Cast<ushort>().Select(tagID =>
                    {
                        object val;
                        if (reader.GetTagValue(tagID, out val))
                        {
                            if (val is double)
                            {
                                int[] rational;
                                if (reader.GetTagValue(tagID, out rational))
                                    val = string.Format("{0} ({1}/{2})", val, rational[0], rational[1]);
                            }
                            var inf = new JSONDataFormat();
                            inf.parameter = Enum.GetName(typeof(ExifTags), tagID);
                            inf.data = RenderTag(val);
                            return inf;
                        }

                        return null;

                    }).Where(x => x != null).ToList();
                    data.Add(new JSONDataFormat { parameter = "EXIF FROM IMAGE", data = null }); 
                    data.AddRange(props);
                }
            }
            catch (Exception ex)
            {
                data.Add(new JSONDataFormat { parameter = "EXIF FROM IMAGE", data = ex.Message.ToString() });
            }
            return data;
        }

        private static string RenderTag(object tagValue)
        {
            var array = tagValue as Array;
            if (array != null)
            {
                if (array.Length > 20 && array.GetType().GetElementType() == typeof(byte))
                    return "0x" + string.Join("", array.Cast<byte>().Select(x => x.ToString("X2")).ToArray());

                return string.Join(" ", array.Cast<object>().Select(x => x.ToString()).ToArray());
            }

            return tagValue.ToString();
        }

    }
}
