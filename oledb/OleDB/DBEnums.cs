using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OleDB
{
	public enum DataTyp
	{
		Bigint,
		Int,
		Smallint,
		Tinyint,
		Decimal,
		Numeric,
		Bit,
		Smallmoney,
		Money,
		Float,
		Real,
		Char,
		Varchar,
		Text,
		Nvarchar,
		Nchar,
		Ntext,
		Date,
		Time,
		Smalldatetime,
		Binary,
		Varbinary,
		Image,
		Timestamp,
		Uniqueidentifier,
		Xml,
		DateTime,
		Bool,
	}

	public enum DBConnect
	{
		OK,
		Create,
		Cancel
	}
}
