using Ookii.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Lokalisierung
{
    sealed class Localization
  {
    public static event EventHandler ChangeLangauge;

    private static ProgressDialog progressDialog;

    private static XmlDocument language = new XmlDocument();
    private static XmlNode root;
    private static XmlNode land;
    private static XmlNode lokalisierung;
    private static XmlNode global;
    private static XmlNode modul;
    public static string ResourcePath = Path.Combine(Application.StartupPath,"Lokalisation");

    private static List<Type> excludedControlTypes = new List<Type>();

    static Localization()
    {
      excludedControlTypes.Add(typeof(ListBox));
      excludedControlTypes.Add(typeof(ComboBox));
      excludedControlTypes.Add(typeof(HScrollBar));
      excludedControlTypes.Add(typeof(VScrollBar));
      excludedControlTypes.Add(typeof(ComboBox));
      excludedControlTypes.Add(typeof(WebBrowser));
      //excludedControlTypes.Add(typeof(MenuStrip));
      excludedControlTypes.Add(typeof(StatusStrip));
      //excludedControlTypes.Add(typeof(SplitterPanel));
    }

    public static void UpdateLanguage(string langID)
    {
      progressDialog  = new ProgressDialog();

      Language = langID;

      string file = Path.Combine(ResourcePath, "Resources." + langID + ".resx");
      if (File.Exists(file))
      {
        language.Load(file);

        root = language.SelectSingleNode("root");
        land = root.SelectSingleNode("land");
        lokalisierung = root.SelectSingleNode("lokalisierung");
        global = lokalisierung.SelectSingleNode("global");

        Sprache = land.Attributes["sprache"].Value;
        Kennung = land.Attributes["kennung"].Value;
        Land = land.Attributes["land"].Value;

        if (land.Attributes["flagge"].Value != String.Empty)
        {
          string flagge = land.Attributes["flagge"].Value;
          byte[] bytes = Convert.FromBase64String(flagge);
          MemoryStream mem = new MemoryStream(bytes);
          Image = new Bitmap(mem);
        }
      }
    }

    public static string Language { get; set; }
    public static string Sprache { get; set; }
    public static string Kennung { get; set; }
    public static string Land { get; set; }
    public static Image Image { get; set; }

    public static void UpdateModul(Form form)
    {
      modul = lokalisierung.SelectSingleNode(form.Name);

			SetLanguage(form);
    }

		public static void UpdateModul(UserControl form)
		{
			modul = lokalisierung.SelectSingleNode(form.Name);

			SetLanguage(form);
		}

    public static void SetLanguage(Control control)
    {
      string temp;

      Console.WriteLine(control.Name);

      if (!excludedControlTypes.Contains(control.GetType()))
        if (control.GetType() == typeof(DataGridView) )
        {
          var x = ((DataGridView)control).Columns;
          foreach (DataGridViewColumn c in ((DataGridView)control).Columns)
          {
            XmlNode key = modul.SelectSingleNode(c.Name);
            if (key == null)
              key = global.SelectSingleNode(c.Name);

            if (key != null)
            {
              temp = key.Attributes["value"].Value;
              if (temp != String.Empty)
                c.HeaderText = temp;
            }

            c.HeaderText = c.HeaderText.Replace("{##}", Environment.NewLine);
          }
        }
        else
        {
          if (control.Name != String.Empty)
          {
            XmlNode key = global.SelectSingleNode(control.Name);
            if (key == null)
              if (modul != null)
                key = modul.SelectSingleNode(control.Name);

            if (key != null)
            {
              temp = key.Attributes["value"].Value;
              if (temp != String.Empty)
                control.Text = temp;
            }

            control.Text = control.Text.Replace("{##}", Environment.NewLine);
          }
        }

      if (control.GetType() == typeof(MenuStrip))
        for (int i = 0; i < ((MenuStrip)control).Items.Count; i++)
          SetLanguageMenu((ToolStripMenuItem)((MenuStrip)control).Items[i]);
      else if (control.GetType() == typeof(ToolStrip))
        for (int i = 0; i < ((ToolStrip)control).Items.Count; i++)
          SetLanguageMenu((ToolStripItem)((ToolStrip)control).Items[i]);
      else if (control.GetType() == typeof(BindingNavigator))
        for (int i = 0; i < ((BindingNavigator)control).Items.Count; i++)
          SetLanguageMenu((ToolStripItem)((BindingNavigator)control).Items[i]);
      else if (control.GetType() == typeof(SplitContainer))
      {
        SetLanguage(((SplitContainer)control).Panel1);
        SetLanguage(((SplitContainer)control).Panel2);
      }
      else
        foreach (Control c in control.Controls)
          SetLanguage(c);
    }

    public static List<string> ExistingLanguages
    {
      get
      {
        List<string> result = new List<string>();

        string[] files = Directory.GetFiles(Localization.ResourcePath, "*.xml");
        foreach (string file in files)
        {
          XmlDocument doc = new XmlDocument();
          doc.Load(file);
          XmlNode root = doc.SelectSingleNode("root");
          XmlNode land = root.SelectSingleNode("land");

          if (land.Attributes["kennung"].Value != "de")
            result.Add(land.Attributes["kennung"].Value);
        }

        return result;
      }
    }

    public static void ChangeLanguage(string langID)
    {
      UpdateLanguage(langID);

      foreach (Form form in Application.OpenForms)
        SetLanguage(form);
    }

    public static string GetTranslation(string name, string keyName)
    {
      string temp = "";

      XmlNode node = lokalisierung.SelectSingleNode(name);
      if (node != null)
      {
        node = node.SelectSingleNode(keyName);
        if (node != null)
          temp = node.Attributes["value"].Value;
      }

      if (temp == string.Empty)
      {
        node = lokalisierung.SelectSingleNode("global");
        if (node != null)
        {
          node = node.SelectSingleNode(keyName);
          if (node != null)
            temp = node.Attributes["value"].Value;
        }
      }
      return temp.Replace("{##}", Environment.NewLine);
    }

    public static void SetLanguageMenu(ToolStripMenuItem control)
    {
      string temp;

      Console.WriteLine(control.Name);

      XmlNode key = global.SelectSingleNode(control.Name);
      if (key == null)
        key = modul.SelectSingleNode(control.Name);

      if (key != null)
      {
        temp = key.Attributes["value"].Value;
        if (temp != String.Empty)
          control.Text = temp;
      }

      control.Text = control.Text.Replace("{##}", Environment.NewLine);

     if(control.HasDropDownItems)
       for (int i = 0; i < control.DropDownItems.Count; i++)
         if (control.DropDownItems[i].GetType() == typeof(ToolStripMenuItem))
           SetLanguageMenu(control.DropDownItems[i]);
    }

    public static void SetLanguageMenu(ToolStripItem control)
    {
      string temp;

      Console.WriteLine(control.Name);

      XmlNode key = global.SelectSingleNode(control.Name);
      if (key == null)
        key = modul.SelectSingleNode(control.Name);

      if (key != null)
      {
        temp = key.Attributes["value"].Value;
        if (temp != String.Empty)
          control.Text = temp;
      }
    }

		public static List<ComboStruktur> Languages
		{
			get
			{
				List<ComboStruktur> result = new List<ComboStruktur>();

				string[] files = Directory.GetFiles(Localization.ResourcePath, "*.resx");
				foreach (string file in files)
				{
					XmlDocument doc = new XmlDocument();
					doc.Load(file);
					XmlNode root = doc.SelectSingleNode("root");
					XmlNode land = root.SelectSingleNode("land");

					ComboStruktur s = new ComboStruktur();
					s.Key=land.Attributes["kennung"].Value;
					s.Sprache=land.Attributes["sprache"].Value;

					byte[] bytes = Convert.FromBase64String(land.Attributes["flagge"].Value);
					MemoryStream mem = new MemoryStream(bytes);
					s.Flagge = new Bitmap(mem);

					result.Add(s);
				}

				return result;
			}
		}

        //public static void SetTexte()
        //{
        //  progressDialog.DoWork += new System.ComponentModel.DoWorkEventHandler(progressDialog_DoWork);
        //  progressDialog.RunWorkerCompleted += new RunWorkerCompletedEventHandler(progressDialog_RunWorkerCompleted);

        //  progressDialog.Description = "Processing...";
        //  progressDialog.ShowTimeRemaining = true;
        //  progressDialog.Text ="Texte werden auf die gewählte Sprache eingestellt";
        //  progressDialog.WindowTitle = "Sprachumstellung"; 
        //  progressDialog.ShowCancelButton=false;

        //  progressDialog.Show();
        //}

        //static void progressDialog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //  progressDialog.DoWork -= new System.ComponentModel.DoWorkEventHandler(progressDialog_DoWork);
        //  progressDialog.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(progressDialog_RunWorkerCompleted);

        //  if (ChangeLangauge != null)
        //    ChangeLangauge(null, null);
        //}

        //private static void progressDialog_DoWork(object sender, DoWorkEventArgs e)
        //{
        //  int pStep = 6;
        //  int p = 0;
        //  string text = "Texte werden auf die gewählte Sprache eingestellt";
        //  OleDBZugriff database = new OleDBZugriff();
        //  database.Tabelle = "Übersicht";

        //  Language = Language.ToUpper();

        //  database.OpenTransaction();

        //  Thread.Sleep(500);
        //  progressDialog.ReportProgress(p, text, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Aversbeschreibung: {0}%", p));

        //  Thread.Sleep(500);
        //  string cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Aversbeschreibung=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Aversbeschreibung'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, text, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Reversbeschreibung: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Reversbeschreibung=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Reversbeschreibung'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Besonderheit: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Besonderheit=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Besonderheit'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Kommentar: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Kommentar=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Kommentar'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Motiv: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Motiv=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Motiv'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Rand: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Rand=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Rand'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Ausgabeanlass: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Ausgabeanlass=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Ausgabeanlass'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "ÄhnlicheMotive: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET ÄhnlicheMotive=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='ÄhnlicheMotive'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Material: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Material=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Material'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Legierung: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET Legierung=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='Legierung'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "AversEntwurf: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET AversEntwurf=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='AversEntwurf'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "ReversEntwurf: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblCB_DB INNER JOIN tblTexte ON tblCB_DB.RepID = tblTexte.GUID SET ReversEntwurf=Text+Text2 WHERE tblTexte.[Sprache]='" + Language + "' AND tblTexte.typ='ReversEntwurf'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Nation: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblNation INNER JOIN tblModule ON tblNation.ID = tblModule.ID SET Bezeichnung=Text WHERE tblModule.[Sprache]='" + Language + "' AND tblModule.typ='Nation'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Ära: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblAera INNER JOIN tblModule ON tblAera.ID = tblModule.ID SET Bezeichnung=Text WHERE tblModule.[Sprache]='" + Language + "' AND tblModule.typ='Ära'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Gebiet: {0}%", p));

        //  Thread.Sleep(500);
        //  cmd = "UPDATE tblGebiet INNER JOIN tblModule ON tblGebiet.ID = tblModule.ID SET Bezeichnung=Text WHERE tblModule.[Sprache]='" + Language + "' AND tblModule.typ='Gebiet'";
        //  database.Execute(cmd);
        //  p = p + pStep;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Gebiet: {0}%", p));
        //  database.Commit();

        //  database.Execute("Update stblSettings set Wert = 'true' where id='Flag'");

        //  do
        //  {
        //    Thread.Sleep(500);
        //    text = database.Text("Select wert from stblSettings where id ='Flag'");
        //  } while (text != "true");

        //  database.Execute("Update stblSettings set Wert = 'false' where id='Flag'");

        //  p = 100;
        //  progressDialog.ReportProgress(p, null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Fertig: {0}%", p));
        //}
    }

    public class ComboStruktur
	{
		public Image Flagge { get; set; }
		public string Sprache { get; set; }
		public string Key { get; set; }
	}
}
