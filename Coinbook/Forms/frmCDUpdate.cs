using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Coinbook.Lokalisierung;
using NetOffice.OfficeApi.Tools.Dialogs;

namespace Coinbook
{
  public partial class frmCDUpdate : Form
  {
    public frmCDUpdate()
    {
      InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

			//Get all Drives
			DriveInfo[] ListAllDrives = DriveInfo.GetDrives();

      foreach (DriveInfo drive in ListAllDrives)
      {
        if (drive.DriveType == DriveType.Removable  || drive.DriveType == DriveType.CDRom)
        {
          ListViewItem NewItem = new ListViewItem();   //Create ListViewItem, give name etc.

          if (drive.DriveType == DriveType.CDRom)
            NewItem.Text = drive.Name + " " + LanguageHelper.Localization.GetTranslation(Name,"itemCD");

          if (drive.DriveType == DriveType.Removable)
            NewItem.Text = drive.Name + " " + LanguageHelper.Localization.GetTranslation(Name, "itemFloppy"); 

          lstDrives.Items.Add(NewItem);
        }

      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
