using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OleDB
{
	class XmlAttributeEx 
	{
		private XmlAttributeCollection attribute;

		public XmlAttributeEx(XmlAttributeCollection attribute)
		{
			this.attribute = attribute;
		}

		public bool ExistAttribute(string name)
		{
			bool result = false;

			if (attribute != null)
			{
				foreach (XmlAttribute a in attribute)
					if (a.Name == name)
						result = true;
			}

			return result;
		}

		public XmlAttribute Attribute (int index)
		{
			if (index >=0 && index < attribute.Count)
				return attribute[index];
			else
				return null;
		}

		public XmlAttribute Attribute(string name)
		{
			if (ExistAttribute(name))
				return attribute[name];
			else
				return null;
		}
	}
}
