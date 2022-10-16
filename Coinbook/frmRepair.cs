using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OleDB;
using System.IO;

namespace Coinbook
{
	public partial class frmRepair : Form
	{
		OleDBZugriff database;

		public frmRepair()
		{
			InitializeComponent();
			Localization.UpdateModul(this);
		}

		public new void ShowDialog()
		{
			database = new OleDBZugriff();
			database.Tabelle = "stbLBestand";

			base.ShowDialog();
		}

		private void btnRepair_Click(object sender, EventArgs e)
		{
      btnClose.Enabled = false;
      Helper.RepairBestand(database);
      //compactAccessDB();

      MessageBox.Show(Localization.GetTranslation(Name,"msgOK"));

      btnClose.Enabled = true;
		}

		/// <summary>
		/// MBD compact method (c) 2004 Alexander Youmashev
		/// !!IMPORTANT!!
		/// !make sure there's no open connections
		///    to your db before calling this method!
		/// !!IMPORTANT!!
		/// </summary>
		/// <param name="connectionString">connection string to your db</param>
		/// <param name="mdwfilename">FULL name
		///     of an MDB file you want to compress.</param>
		private void compactAccessDB()
		{
			OleDBConnection.CloseAllConnections();
			string mdwfilename;
			string connectionString = ((DBConnectionAccess)OleDBConnection.Connections[0]).ConnectionString;

			object[] oParams;

			object objJRO =	Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));			//create an inctance of a Jet Replication Object

			//filling Parameters array - change "Jet OLEDB:Engine Type=5" to an appropriate value or leave it as is if you db is JET4X format (access 2000,2002) (yes, jetengine5 is for JET4X, no misprint here)
			oParams = new object[] { connectionString, "Provider=" + OleDBConnection.Provider + ";Data " + OleDBConnection.Datapath + @"\tempdb.mdb;Jet OLEDB:Engine Type=5" };

			//invoke a CompactDatabase method of a JRO object pass Parameters array
			objJRO.GetType().InvokeMember("CompactDatabase", System.Reflection.BindingFlags.InvokeMethod, null, objJRO, oParams);

			//database is compacted now to a new file tempdb.mdw let's copy it over an old one and delete it

			//File.Delete(mdwfilename);
			//File.Move(OleDBConnection.Datapath + "tempdb.mdb", mdwfilename);

			//clean up (just in case)
			System.Runtime.InteropServices.Marshal.ReleaseComObject(objJRO);
			objJRO = null;

			DBConnect result = OleDBConnection.Init;
		}

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
	
	}
}
		
			

			

			

			
