using System;
using System.Collections.Generic;
using System.Data;

namespace FilterableTestApp
{
	public enum SampleEnum 
	{
		Low = 0,
		Medium = 1,
		High = 2
	}

    public class Order
    {
        private int _orderId;
        private string _customerId;
        private int _employeeId;
        private DateTime _orderDate;
        private DateTime _requiredDate;
        private DateTime _shippedDate;
        private int _shipVia;
        private decimal _freight;
        private string _shipName;
        private string _shipAddress;
        private string _shipCity;
        private string _shipRegion;
        private string _shipPostalCode;
        private string _shipCountry;
        private SampleEnum _freightQuantity;

        public Order(DataRow data)
        {
            _orderId = (int)data[0];
            _customerId = (string)data[1];
            _employeeId = (int)data[2];
            _orderDate = (DateTime)data[3];
            _requiredDate = (DateTime)data[4];
            _shippedDate = data[5] == DBNull.Value ? DateTime.MinValue : (DateTime)data[5];
            _shipVia = (int)data[6];
            _freight = (decimal)data[7];
            _shipName = (string)data[8];
            _shipAddress = (string)data[9];
            _shipCity = (string)data[10];
            _shipRegion = data[11] == DBNull.Value ? "" : (string)data[11];
            _shipPostalCode = data[12] == DBNull.Value ? "" : (string)data[12];
            _shipCountry = data[13] == DBNull.Value ? "" : (string)data[13];
            _freightQuantity = (SampleEnum)data[14];
        }

        public int OrderId
        {
            get { return _orderId; }
        }

        public string CustomerId
        {
            get { return _customerId; }
        }

        public int EmployeeId
        {
            get { return _employeeId; }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
        }

        public DateTime RequiredDate
        {
            get { return _requiredDate; }
        }

        public DateTime ShippedDate
        {
            get { return _shippedDate; }
        }

        public int ShipVia
        {
            get { return _shipVia; }
        }

        public decimal Freight
        {
            get { return _freight; }
        }

        public string ShipName
        {
            get { return _shipName; }
        }

        public string ShipAddress
        {
            get { return _shipAddress; }
        }

        public string ShipCity
        {
            get { return _shipCity; }
        }

        public string ShipRegion
        {
            get { return _shipRegion; }
        }

        public string ShipPostalCode
        {
            get { return _shipPostalCode; }
        }

        public string ShipCountry
        {
            get { return _shipCountry; }
        }

        public SampleEnum FreightQuantity
        {
            get { return _freightQuantity; }
        }
    }

	public sealed class DataHelper
	{
		private static DataSet _dataSet;
        private static List<Order> _sampleList;

		private DataHelper() {}

		public static DataSet SampleData
		{
			get 
			{
				if (_dataSet == null)
					_dataSet = CreateDataSet();
				return _dataSet;
			}
		}

        public static List<Order> SampleList
        {
            get
            {
                if (_sampleList == null)
                    _sampleList = CreateSampleList();
                return _sampleList;
            }
        }

        private static List<Order> CreateSampleList()
        {
            DataTable ordersTable = SampleData.Tables["Orders"];
            List<Order> result = new List<Order>();
            foreach (DataRow row in ordersTable.Rows)
                result.Add(new Order(row));
            return result;
        }

		private static DataSet CreateDataSet() 
		{
			DataSet ds = new DataSet();

			IDbConnection connection = new System.Data.OleDb.OleDbConnection();
			connection.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:Database Password=;Data Source=""c:\Verein\muteba.mdb"";Password=;Jet OLEDB:Engine Type=3;Jet OLEDB:Global Bulk Transactions=1;Provider=Microsoft.ACE.OLEDB.12.0;Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";

			IDbDataAdapter adapterSender = new System.Data.OleDb.OleDbDataAdapter();
			IDbCommand oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			oleDbSelectCommand1.Connection = connection;
			adapterSender.SelectCommand = oleDbSelectCommand1;

			oleDbSelectCommand1.CommandText = "SELECT * FROM tblStammKunden";
			adapterSender.Fill(ds);
			ds.Tables[0].TableName = "tblKunden";

			oleDbSelectCommand1.CommandText = "SELECT * FROM T_Stamm_Anrede";
			adapterSender.Fill(ds);
			ds.Tables[1].TableName = "tblStammAnrede";
			ds.Tables[1].Columns.Add("FreightQuantity", typeof(SampleEnum));
			foreach (DataRow row in ds.Tables[1].Rows)
			{
				double value = Convert.ToDouble(row["ID"]);
				if (value < 30)
					row["FreightQuantity"] = SampleEnum.Low;
				else if (value < 60)
					row["FreightQuantity"] = SampleEnum.Medium;
				else
				{
					row["ShipRegion"] = "";
					row["FreightQuantity"] = SampleEnum.High;
				}
			}

			//oleDbSelectCommand1.CommandText = "SELECT * FROM Products";
			//adapterSender.Fill(ds);
			//ds.Tables[2].TableName = "Products";

			//oleDbSelectCommand1.CommandText = "SELECT * FROM Shippers";
			//adapterSender.Fill(ds);
			//ds.Tables[3].TableName = "Shippers";

			//oleDbSelectCommand1.CommandText = "SELECT * FROM Suppliers";
			//adapterSender.Fill(ds);
			//ds.Tables[4].TableName = "Suppliers";

			//oleDbSelectCommand1.CommandText = "SELECT * FROM [Order Details]";
			//adapterSender.Fill(ds);
			//ds.Tables[5].TableName = "OrderDetails";

			ds.Relations.Add("Suppliers2Products", ds.Tables[1].Columns["ID"], ds.Tables[0].Columns["Anrede"]);
			//ds.Relations.Add("Products2OrderDetails", ds.Tables[2].Columns["ProductID"], ds.Tables[5].Columns["ProductID"]);
			//ds.Relations.Add("OrderDetails2Order", ds.Tables[1].Columns["OrderID"], ds.Tables[5].Columns["OrderID"]);

			return ds;
		}
	}
}
