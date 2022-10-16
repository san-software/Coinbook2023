using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Drawing;
using Syncfusion.WinForms.DataGrid;
using Ookii.Dialogs.WinForms;
using Syncfusion.Windows.Forms;

namespace Coinbook.Lokalisierung
{
	public class Localization
	{
		public event EventHandler LanguageChange;

		private ProgressDialog progressDialog;

		private XmlDocument language = new XmlDocument();
		private XmlNode root;
		private XmlNode land;
		private XmlNode lokalisierung;
		private XmlNode global;
		private XmlNode modul;
		//public string ResourcePath = Path.Combine(Application.StartupPath, "Lokalisation");
		private string resourcePath;

		private List<Type> excludedControlTypes = new List<Type>();

		public Localization(string resourcepath)
		{
			this.resourcePath = resourcepath;

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

		public void UpdateLanguage(string langID)
		{
			progressDialog = new ProgressDialog();

			Language = langID;

			string file = Path.Combine(resourcePath, "Resources." + langID + ".resx");
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

		public string Language { get; set; }
		public string Sprache { get; set; }
		public string Kennung { get; set; }
		public string Land { get; set; }
		public Image Image { get; set; }

		public void UpdateModul(Form form)
		{
			if (lokalisierung != null)
			{
				modul = lokalisierung.SelectSingleNode(form.Name);

				if (modul != null)
					SetLanguage(form);
			}
		}

		public void UpdateModul(UserControl form)
		{
			if (lokalisierung != null)
			{
				modul = lokalisierung.SelectSingleNode(form.Name);

				if (modul != null)
					SetLanguage(form);
			}
		}

		public void UpdateModul(string formName)
		{
			if (lokalisierung != null)
			{
				modul = lokalisierung.SelectSingleNode(formName);

				//if (modul != null)
				//	SetLanguage(form);
			}
		}

		public void UpdateModul(SfDataGrid grid, string name)
		{
			if (lokalisierung != null)
			{
				modul = lokalisierung.SelectSingleNode(name);

				if (modul != null)
					SetLanguage(grid);
			}
		}

		public void UpdateModul(UserControl form, string name)
		{
			if (lokalisierung != null)
			{
				modul = lokalisierung.SelectSingleNode(name);

				if (modul != null)
					SetLanguage(form);
			}
		}

		public void SetLanguage(Control control)
		{
			string temp;

			if (!excludedControlTypes.Contains(control.GetType()))
			{
				if (control.GetType() == typeof(DataGridView))
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
                else if (control.GetType() == typeof(SfDataGrid))
                {
					SetLanguage((SfDataGrid)control);
                }
                else
				{
					if (control.Name != String.Empty)
					{
						XmlNode key = global.SelectSingleNode(control.Name.Replace(" ", ""));
						if (key == null)
							if (modul != null)
								key = modul.SelectSingleNode(control.Name.Replace(" ", ""));

						if (key != null)
						{
							temp = key.Attributes["value"].Value;
							if (temp != String.Empty)
							{
								control.Text = temp;
							}
						}

						control.Text = control.Text.Replace("{##}", Environment.NewLine);
					}
				}
			}

			if (control.GetType() == typeof(MenuStrip))
			{
				var temp1 = ((MenuStrip)control).Items;
				foreach (var item in temp1)
					SetLanguageMenu((ToolStripMenuItem)item);
			}
			else if (control.GetType() == typeof(ContextMenuStrip))
			{
				var temp1 = ((MenuStrip)control).Items;
				foreach (var item in temp1)
					SetLanguageMenu((ToolStripMenuItem)item);
			}
			else if (control.GetType() == typeof(ToolStrip))
			{
				var temp2 = ((ToolStrip)control).Items;
				foreach (var item in temp2)
				{
					if (item.GetType() == typeof(ToolStripButton))
						SetLanguageToolStripButton((ToolStripButton)item);

					if (item.GetType() == typeof(ToolStripLabel))
						SetLanguageToolStripLabel((ToolStripLabel)item);
				}

				//if (control.ContextMenuStrip is not null)
				//	foreach (var item in control.ContextMenuStrip.Items)
				//		if (item.GetType() == typeof(ToolStripMenuItem))
				//			SetLanguageMenu((ToolStripMenuItem)item);

			}
			else if (control.GetType() == typeof(BindingNavigator))
			{
				//for (int i = 0; i < ((BindingNavigator)control).Items.Count; i++)
				//	SetLanguageMenu((ToolStripItem)((BindingNavigator)control).Items[i]);
			}
			else if (control.GetType() == typeof(SplitContainer))
			{
				SetLanguage(((SplitContainer)control).Panel1);
				SetLanguage(((SplitContainer)control).Panel2);
			}
			else
				foreach (Control c in control.Controls)
					SetLanguage(c);
		}

		public void SetLanguage(SfDataGrid grid)
		{
			string temp;

			var x = ((SfDataGrid)grid).Columns;
			foreach (var c in x)
			{
				XmlNode key = modul.SelectSingleNode(c.MappingName);
				if (key == null)
					key = global.SelectSingleNode(c.MappingName);

				if (key != null)
				{
					temp = key.Attributes["value"].Value;
					if (temp != String.Empty)
						c.HeaderText = temp;
				}

				c.HeaderText = c.HeaderText.Replace("{##}", Environment.NewLine);
			}
		}

		public List<string> ExistingLanguages
		{
			get
			{
				List<string> result = new List<string>();

				string[] files = Directory.GetFiles(resourcePath, "*.xml");
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

		public void ChangeLanguage(string langID)
		{
			UpdateLanguage(langID);

			foreach (Form form in Application.OpenForms)
				SetLanguage(form);
		}

		public string GetTranslation(string name, string keyName, string defaultText = "")
		{
			string temp = String.Empty;

			if (lokalisierung != null)
			{
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

				if (temp == string.Empty)
					temp = defaultText;
			}
			return temp.Replace("{##}", Environment.NewLine);
		}

		public void SetLanguageMenu(ToolStripMenuItem control)
		{
			string temp;

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

			if (control.HasDropDownItems)
				for (int i = 0; i < control.DropDownItems.Count; i++)
					if (control.DropDownItems[i].GetType() == typeof(ToolStripMenuItem))
						SetLanguageSubMenu(control.DropDownItems[i]);
		}

		public void SetLanguageToolStripLabel(ToolStripLabel control)
		{
			string temp;

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
		}

		public void SetLanguageToolStripButton(ToolStripButton control)
		{
			string temp;

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
		}

		public void SetLanguageSubMenu(ToolStripItem control)
		{
			string temp;

			if (control.Name != string.Empty)
			{
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
		}

		public List<ComboStruktur> Languages
		{
			get
			{
				List<ComboStruktur> result = new List<ComboStruktur>();

				string[] files = Directory.GetFiles(resourcePath, "*.resx");
				foreach (string file in files)
				{
					XmlDocument doc = new XmlDocument();
					doc.Load(file);
					XmlNode root = doc.SelectSingleNode("root");
					XmlNode land = root.SelectSingleNode("land");

					ComboStruktur s = new ComboStruktur();
					s.Key = land.Attributes["kennung"].Value;
					s.Sprache = GetTranslation("Keys","lng" + land.Attributes["sprache"].Value); // land.Attributes["sprache"].Value;

					byte[] bytes = Convert.FromBase64String(land.Attributes["flagge"].Value);
					MemoryStream mem = new MemoryStream(bytes);
					s.Flagge = new Bitmap(mem);

					result.Add(s);
				}

				return result;
			}
		}

		public void TranslateContextMenu(string name, ContextMenuStrip menu)
		{
			XmlNode node = lokalisierung.SelectSingleNode(name);
			if (node != null)
			{
				for (int i = 0; i < menu.Items.Count; i++)
				{
					if (menu.Items[i].GetType() == typeof(ToolStripMenuItem))
					{
						ToolStripMenuItem control = (ToolStripMenuItem)menu.Items[i];
						string temp;

						XmlNode key = node.SelectSingleNode(control.Name);
						if (key == null)
							key = modul.SelectSingleNode(control.Name);
						else
						{
							temp = key.Attributes["value"].Value;
							if (temp != String.Empty)
								control.Text = temp;
						}

						control.Text = control.Text.Replace("{##}", Environment.NewLine);

						//if (control.HasDropDownItems)
						//	for (int i = 0; i < control.DropDownItems.Count; i++)
						//		if (control.DropDownItems[i].GetType() == typeof(ToolStripMenuItem))
						//			SetLanguageMenu(control.DropDownItems[i]);
					}
				}
			}
		}
	}

	public class ComboStruktur
	{
		public Image Flagge { get; set; }
		public string Sprache { get; set; }
		public string Key { get; set; }
	}
}
