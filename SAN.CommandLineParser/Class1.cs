using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAN.CommandLineParser
{
    public class CommandLineParser
    {
        Dictionary<string, string> parameter = new Dictionary<string, string>();
        public CommandLineParser(string[] args)
        {
            KeyValuePair<string, string> item;
            string key = string.Empty;

            foreach (var arg in args)
            {
                if (arg.Substring(0, 1) == "-")
                    key = arg.Substring(1);
                else
                    parameter.Add(key, arg);
            }
        }

        public string StringValue(string key, bool required=false)
        {
            string result = string.Empty;

            if (parameter.ContainsKey(key))
                result = parameter[key];
            else if (required)
                result = null;

            return result;
        }

        public int IntegerValue(string key, bool required = false)
        {
            int value = 0;
            bool result = true;

            if (parameter.ContainsKey(key))
                result = int.TryParse(parameter[key],out value);
            else if (required)
                value = null;

            return value;
        }

        public bool BooleanValue(string key, bool required = false)
        {
            bool value = 0;
            bool result = true;

            if (parameter.ContainsKey(key))
                result = Boolean.TryParse(parameter[key], out value);
            else if (required)
                value = null;

            return value;
        }

        public List<string> StringArray(string key, bool required = false)
        {
            List<string> list = new List<string>();

            if (parameter.ContainsKey(key))
                list = parameter[key].Split(',');
            else if (required)
                list = null;

            return list;
        }

        public List<int> IntegerArray(string key, bool required = false)
        {
            bool value = 0;
            bool result = true;

            if (parameter.ContainsKey(key))
                result = Boolean.TryParse(parameter[key], out value);
            else if (required)
                value = null;

            return value;
        }

        public List<bool> IntegerBoolean(string key, bool required = false)
        {
            bool value = 0;
            bool result = true;

            if (parameter.ContainsKey(key))
                result = Boolean.TryParse(parameter[key], out value);
            else if (required)
                value = null;

            return value;
        }
    }
}
