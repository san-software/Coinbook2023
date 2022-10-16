using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using System.Runtime.Versioning;
using Coinbook.Lokalisierung;
using Coinbook.Helper;

namespace Coinbook
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	public class frmAbout : Form
	{
		private System.Windows.Forms.Label lblProgramm;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Label lblVertrieb;
		private System.Windows.Forms.Label lblAutor;
		private System.Windows.Forms.Label lblTelefon;
		private System.Windows.Forms.Label lblTelefonText;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.Label lblEmailText;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblProgrammverzeichnisText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblProgrammverzeichnis;
		private Label lblDatenbank;
		private Label lblDatenbankText;
		private Panel panel1;
		private Label lblProvider;
		private Label lblHost;
		private Label lblDataSource;
		private Label lblSchema;
		private Label lblProviderText;
		private Label lblSchemaText;
		private Label lblDataSourceText;
		private Label lblEntwicklerEmailText;
		private Label lblEntwicklerEmail;
		private Label lblEntwicklerTelefonText;
		private Label lblEntwicklerTelefon;
		private Label lblEntwicklerText;
		private Label lblEntwickler;
    private DataGridView grdAssemblies;
    private DataGridViewTextBoxColumn colAssemblyName;
    private DataGridViewTextBoxColumn colVersion;
    private TabControl TabControl1;
    private TabPage tabDatenbank;
    private TabPage tabModule;
    private TabPage tabCopyrights;
    private RichTextBox txtCopyright;
		private Label lblHostText;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		public frmAbout()
		{
			InitializeComponent();
			LanguageHelper.Localization.UpdateModul(this);

			lblProgramm.Text = Application.ProductName;
			lblVersion.Text = "Version: " + Application.ProductVersion + " - .net-Framework 4.7.2";
			lblCopyright.Text = "Copyright(c) 2010 - 2020 " + Application.CompanyName;
			lblAutor.Text = "Bernd Wiegand";
			lblTelefonText.Text = "05651/76211";
			lblEmailText.Text = "vertrieb@coinbook.de";
			lblEntwicklerText.Text = "Bernhard Höhn";
			lblEntwicklerTelefonText.Text = "0173/5169745";
			lblEntwicklerEmailText.Text = "Support@Coinbook.de";

			lblProgrammverzeichnisText.Text = Application.ExecutablePath;
			//lblDatenbankText.Text = OleDB.OleDBConnection.File;
			//lblHostText.Text = OleDB.OleDBConnection.Host;
			//lblSchemaText.Text = OleDB.OleDBConnection.Schema;
			//lblDataSourceText.Text = OleDB.OleDBConnection.DataSource;
			//lblProviderText.Text = OleDB.OleDBConnection.Provider;

      if (CoinbookHelper.Settings.Culture == "de-DE")
        txtCopyright.LoadFile("Copyrights Bilder-DE.rtf");
      else
        txtCopyright.LoadFile("Copyrights Bilder-EN.rtf");

			grdAssemblies.DataSource = ReferencedAssemblies(Assembly.GetExecutingAssembly());
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.lblProgramm = new System.Windows.Forms.Label();
      this.lblVersion = new System.Windows.Forms.Label();
      this.lblCopyright = new System.Windows.Forms.Label();
      this.lblVertrieb = new System.Windows.Forms.Label();
      this.lblAutor = new System.Windows.Forms.Label();
      this.lblTelefon = new System.Windows.Forms.Label();
      this.lblTelefonText = new System.Windows.Forms.Label();
      this.lblEmail = new System.Windows.Forms.Label();
      this.lblEmailText = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.lblProgrammverzeichnisText = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lblProgrammverzeichnis = new System.Windows.Forms.Label();
      this.lblDatenbank = new System.Windows.Forms.Label();
      this.lblDatenbankText = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblProviderText = new System.Windows.Forms.Label();
      this.lblSchemaText = new System.Windows.Forms.Label();
      this.lblDataSourceText = new System.Windows.Forms.Label();
      this.lblHostText = new System.Windows.Forms.Label();
      this.lblProvider = new System.Windows.Forms.Label();
      this.lblHost = new System.Windows.Forms.Label();
      this.lblDataSource = new System.Windows.Forms.Label();
      this.lblSchema = new System.Windows.Forms.Label();
      this.lblEntwicklerEmailText = new System.Windows.Forms.Label();
      this.lblEntwicklerEmail = new System.Windows.Forms.Label();
      this.lblEntwicklerTelefonText = new System.Windows.Forms.Label();
      this.lblEntwicklerTelefon = new System.Windows.Forms.Label();
      this.lblEntwicklerText = new System.Windows.Forms.Label();
      this.lblEntwickler = new System.Windows.Forms.Label();
      this.grdAssemblies = new System.Windows.Forms.DataGridView();
      this.colAssemblyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.TabControl1 = new System.Windows.Forms.TabControl();
      this.tabDatenbank = new System.Windows.Forms.TabPage();
      this.tabModule = new System.Windows.Forms.TabPage();
      this.tabCopyrights = new System.Windows.Forms.TabPage();
      this.txtCopyright = new System.Windows.Forms.RichTextBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdAssemblies)).BeginInit();
      this.TabControl1.SuspendLayout();
      this.tabDatenbank.SuspendLayout();
      this.tabModule.SuspendLayout();
      this.tabCopyrights.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgramm
      // 
      this.lblProgramm.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProgramm.Location = new System.Drawing.Point(8, 8);
      this.lblProgramm.Name = "lblProgramm";
      this.lblProgramm.Size = new System.Drawing.Size(648, 32);
      this.lblProgramm.TabIndex = 0;
      this.lblProgramm.Text = "WWU-Datenbank";
      this.lblProgramm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblVersion
      // 
      this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblVersion.Location = new System.Drawing.Point(8, 48);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(648, 24);
      this.lblVersion.TabIndex = 2;
      this.lblVersion.Text = "Version";
      this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblCopyright
      // 
      this.lblCopyright.Location = new System.Drawing.Point(8, 80);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(648, 16);
      this.lblCopyright.TabIndex = 4;
      this.lblCopyright.Text = "Copyright";
      this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblVertrieb
      // 
      this.lblVertrieb.AutoSize = true;
      this.lblVertrieb.Location = new System.Drawing.Point(16, 120);
      this.lblVertrieb.Name = "lblVertrieb";
      this.lblVertrieb.Size = new System.Drawing.Size(43, 13);
      this.lblVertrieb.TabIndex = 9;
      this.lblVertrieb.Text = "Vertrieb";
      // 
      // lblAutor
      // 
      this.lblAutor.Location = new System.Drawing.Point(80, 120);
      this.lblAutor.Name = "lblAutor";
      this.lblAutor.Size = new System.Drawing.Size(208, 16);
      this.lblAutor.TabIndex = 10;
      this.lblAutor.Text = "label6";
      // 
      // lblTelefon
      // 
      this.lblTelefon.Location = new System.Drawing.Point(16, 144);
      this.lblTelefon.Name = "lblTelefon";
      this.lblTelefon.Size = new System.Drawing.Size(56, 16);
      this.lblTelefon.TabIndex = 11;
      this.lblTelefon.Text = "Telefon";
      // 
      // lblTelefonText
      // 
      this.lblTelefonText.Location = new System.Drawing.Point(80, 144);
      this.lblTelefonText.Name = "lblTelefonText";
      this.lblTelefonText.Size = new System.Drawing.Size(208, 16);
      this.lblTelefonText.TabIndex = 12;
      this.lblTelefonText.Text = "label7";
      // 
      // lblEmail
      // 
      this.lblEmail.Location = new System.Drawing.Point(16, 168);
      this.lblEmail.Name = "lblEmail";
      this.lblEmail.Size = new System.Drawing.Size(56, 16);
      this.lblEmail.TabIndex = 13;
      this.lblEmail.Text = "E-Mail";
      // 
      // lblEmailText
      // 
      this.lblEmailText.Location = new System.Drawing.Point(80, 168);
      this.lblEmailText.Name = "lblEmailText";
      this.lblEmailText.Size = new System.Drawing.Size(208, 16);
      this.lblEmailText.TabIndex = 14;
      this.lblEmailText.Text = "label8";
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(582, 583);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(64, 24);
      this.btnOK.TabIndex = 15;
      this.btnOK.Text = "OK";
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // lblProgrammverzeichnisText
      // 
      this.lblProgrammverzeichnisText.Location = new System.Drawing.Point(152, 208);
      this.lblProgrammverzeichnisText.Name = "lblProgrammverzeichnisText";
      this.lblProgrammverzeichnisText.Size = new System.Drawing.Size(416, 16);
      this.lblProgrammverzeichnisText.TabIndex = 16;
      this.lblProgrammverzeichnisText.Text = "Programmverzeichnis";
      // 
      // label1
      // 
      this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label1.Location = new System.Drawing.Point(8, 104);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(648, 3);
      this.label1.TabIndex = 17;
      this.label1.Text = "label1";
      // 
      // label2
      // 
      this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label2.Location = new System.Drawing.Point(8, 192);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(648, 3);
      this.label2.TabIndex = 18;
      this.label2.Text = "label2";
      // 
      // lblProgrammverzeichnis
      // 
      this.lblProgrammverzeichnis.Location = new System.Drawing.Point(16, 208);
      this.lblProgrammverzeichnis.Name = "lblProgrammverzeichnis";
      this.lblProgrammverzeichnis.Size = new System.Drawing.Size(128, 16);
      this.lblProgrammverzeichnis.TabIndex = 19;
      this.lblProgrammverzeichnis.Text = "lblProgrammverzeichnis";
      // 
      // lblDatenbank
      // 
      this.lblDatenbank.Location = new System.Drawing.Point(7, 9);
      this.lblDatenbank.Name = "lblDatenbank";
      this.lblDatenbank.Size = new System.Drawing.Size(128, 16);
      this.lblDatenbank.TabIndex = 20;
      this.lblDatenbank.Text = "Datenbank:";
      // 
      // lblDatenbankText
      // 
      this.lblDatenbankText.Location = new System.Drawing.Point(140, 9);
      this.lblDatenbankText.Name = "lblDatenbankText";
      this.lblDatenbankText.Size = new System.Drawing.Size(489, 16);
      this.lblDatenbankText.TabIndex = 21;
      this.lblDatenbankText.Text = "Programmverzeichnis";
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.lblProviderText);
      this.panel1.Controls.Add(this.lblSchemaText);
      this.panel1.Controls.Add(this.lblDataSourceText);
      this.panel1.Controls.Add(this.lblHostText);
      this.panel1.Controls.Add(this.lblProvider);
      this.panel1.Controls.Add(this.lblHost);
      this.panel1.Controls.Add(this.lblDataSource);
      this.panel1.Controls.Add(this.lblSchema);
      this.panel1.Controls.Add(this.lblDatenbankText);
      this.panel1.Controls.Add(this.lblDatenbank);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(634, 318);
      this.panel1.TabIndex = 22;
      // 
      // lblProviderText
      // 
      this.lblProviderText.Location = new System.Drawing.Point(140, 33);
      this.lblProviderText.Name = "lblProviderText";
      this.lblProviderText.Size = new System.Drawing.Size(489, 16);
      this.lblProviderText.TabIndex = 29;
      this.lblProviderText.Text = "Programmverzeichnis";
      // 
      // lblSchemaText
      // 
      this.lblSchemaText.Location = new System.Drawing.Point(140, 109);
      this.lblSchemaText.Name = "lblSchemaText";
      this.lblSchemaText.Size = new System.Drawing.Size(487, 16);
      this.lblSchemaText.TabIndex = 28;
      this.lblSchemaText.Text = "Programmverzeichnis";
      // 
      // lblDataSourceText
      // 
      this.lblDataSourceText.Location = new System.Drawing.Point(140, 87);
      this.lblDataSourceText.Name = "lblDataSourceText";
      this.lblDataSourceText.Size = new System.Drawing.Size(489, 16);
      this.lblDataSourceText.TabIndex = 27;
      this.lblDataSourceText.Text = "Programmverzeichnis";
      // 
      // lblHostText
      // 
      this.lblHostText.Location = new System.Drawing.Point(143, 60);
      this.lblHostText.Name = "lblHostText";
      this.lblHostText.Size = new System.Drawing.Size(486, 16);
      this.lblHostText.TabIndex = 26;
      this.lblHostText.Text = "Programmverzeichnis";
      // 
      // lblProvider
      // 
      this.lblProvider.Location = new System.Drawing.Point(7, 33);
      this.lblProvider.Name = "lblProvider";
      this.lblProvider.Size = new System.Drawing.Size(128, 16);
      this.lblProvider.TabIndex = 25;
      this.lblProvider.Text = "Provider";
      // 
      // lblHost
      // 
      this.lblHost.Location = new System.Drawing.Point(7, 60);
      this.lblHost.Name = "lblHost";
      this.lblHost.Size = new System.Drawing.Size(128, 16);
      this.lblHost.TabIndex = 24;
      this.lblHost.Text = "Host";
      // 
      // lblDataSource
      // 
      this.lblDataSource.Location = new System.Drawing.Point(7, 87);
      this.lblDataSource.Name = "lblDataSource";
      this.lblDataSource.Size = new System.Drawing.Size(128, 16);
      this.lblDataSource.TabIndex = 23;
      this.lblDataSource.Text = "Quelle";
      // 
      // lblSchema
      // 
      this.lblSchema.Location = new System.Drawing.Point(7, 109);
      this.lblSchema.Name = "lblSchema";
      this.lblSchema.Size = new System.Drawing.Size(81, 16);
      this.lblSchema.TabIndex = 22;
      this.lblSchema.Text = "Schema";
      // 
      // lblEntwicklerEmailText
      // 
      this.lblEntwicklerEmailText.Location = new System.Drawing.Point(444, 168);
      this.lblEntwicklerEmailText.Name = "lblEntwicklerEmailText";
      this.lblEntwicklerEmailText.Size = new System.Drawing.Size(208, 16);
      this.lblEntwicklerEmailText.TabIndex = 28;
      this.lblEntwicklerEmailText.Text = "label8";
      // 
      // lblEntwicklerEmail
      // 
      this.lblEntwicklerEmail.Location = new System.Drawing.Point(365, 168);
      this.lblEntwicklerEmail.Name = "lblEntwicklerEmail";
      this.lblEntwicklerEmail.Size = new System.Drawing.Size(56, 16);
      this.lblEntwicklerEmail.TabIndex = 27;
      this.lblEntwicklerEmail.Text = "E-Mail";
      // 
      // lblEntwicklerTelefonText
      // 
      this.lblEntwicklerTelefonText.Location = new System.Drawing.Point(444, 144);
      this.lblEntwicklerTelefonText.Name = "lblEntwicklerTelefonText";
      this.lblEntwicklerTelefonText.Size = new System.Drawing.Size(208, 16);
      this.lblEntwicklerTelefonText.TabIndex = 26;
      this.lblEntwicklerTelefonText.Text = "label7";
      // 
      // lblEntwicklerTelefon
      // 
      this.lblEntwicklerTelefon.Location = new System.Drawing.Point(365, 144);
      this.lblEntwicklerTelefon.Name = "lblEntwicklerTelefon";
      this.lblEntwicklerTelefon.Size = new System.Drawing.Size(56, 16);
      this.lblEntwicklerTelefon.TabIndex = 25;
      this.lblEntwicklerTelefon.Text = "Telefon";
      // 
      // lblEntwicklerText
      // 
      this.lblEntwicklerText.Location = new System.Drawing.Point(444, 120);
      this.lblEntwicklerText.Name = "lblEntwicklerText";
      this.lblEntwicklerText.Size = new System.Drawing.Size(208, 16);
      this.lblEntwicklerText.TabIndex = 24;
      this.lblEntwicklerText.Text = "label6";
      // 
      // lblEntwickler
      // 
      this.lblEntwickler.Location = new System.Drawing.Point(365, 120);
      this.lblEntwickler.Name = "lblEntwickler";
      this.lblEntwickler.Size = new System.Drawing.Size(73, 16);
      this.lblEntwickler.TabIndex = 23;
      this.lblEntwickler.Text = "Entwicklung";
      // 
      // grdAssemblies
      // 
      this.grdAssemblies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdAssemblies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAssemblyName,
            this.colVersion});
      this.grdAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grdAssemblies.Location = new System.Drawing.Point(3, 3);
      this.grdAssemblies.Name = "grdAssemblies";
      this.grdAssemblies.ReadOnly = true;
      this.grdAssemblies.RowHeadersVisible = false;
      this.grdAssemblies.Size = new System.Drawing.Size(634, 318);
      this.grdAssemblies.TabIndex = 29;
      // 
      // colAssemblyName
      // 
      this.colAssemblyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colAssemblyName.DataPropertyName = "Name";
      this.colAssemblyName.HeaderText = "Name";
      this.colAssemblyName.Name = "colAssemblyName";
      this.colAssemblyName.ReadOnly = true;
      // 
      // colVersion
      // 
      this.colVersion.DataPropertyName = "Version";
      this.colVersion.HeaderText = "Version";
      this.colVersion.Name = "colVersion";
      this.colVersion.ReadOnly = true;
      this.colVersion.Width = 98;
      // 
      // TabControl1
      // 
      this.TabControl1.Controls.Add(this.tabDatenbank);
      this.TabControl1.Controls.Add(this.tabModule);
      this.TabControl1.Controls.Add(this.tabCopyrights);
      this.TabControl1.Location = new System.Drawing.Point(8, 227);
      this.TabControl1.Name = "TabControl1";
      this.TabControl1.SelectedIndex = 0;
      this.TabControl1.Size = new System.Drawing.Size(648, 350);
      this.TabControl1.TabIndex = 30;
      // 
      // tabDatenbank
      // 
      this.tabDatenbank.Controls.Add(this.panel1);
      this.tabDatenbank.Location = new System.Drawing.Point(4, 22);
      this.tabDatenbank.Name = "tabDatenbank";
      this.tabDatenbank.Padding = new System.Windows.Forms.Padding(3);
      this.tabDatenbank.Size = new System.Drawing.Size(640, 324);
      this.tabDatenbank.TabIndex = 0;
      this.tabDatenbank.Text = "Datenbank";
      this.tabDatenbank.UseVisualStyleBackColor = true;
      // 
      // tabModule
      // 
      this.tabModule.Controls.Add(this.grdAssemblies);
      this.tabModule.Location = new System.Drawing.Point(4, 22);
      this.tabModule.Name = "tabModule";
      this.tabModule.Padding = new System.Windows.Forms.Padding(3);
      this.tabModule.Size = new System.Drawing.Size(640, 324);
      this.tabModule.TabIndex = 1;
      this.tabModule.Text = "Referenzierte Module";
      this.tabModule.UseVisualStyleBackColor = true;
      // 
      // tabCopyrights
      // 
      this.tabCopyrights.Controls.Add(this.txtCopyright);
      this.tabCopyrights.Location = new System.Drawing.Point(4, 22);
      this.tabCopyrights.Name = "tabCopyrights";
      this.tabCopyrights.Padding = new System.Windows.Forms.Padding(3);
      this.tabCopyrights.Size = new System.Drawing.Size(640, 324);
      this.tabCopyrights.TabIndex = 2;
      this.tabCopyrights.Text = "Copyrights";
      this.tabCopyrights.UseVisualStyleBackColor = true;
      // 
      // txtCopyright
      // 
      this.txtCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCopyright.Location = new System.Drawing.Point(3, 3);
      this.txtCopyright.Name = "txtCopyright";
      this.txtCopyright.Size = new System.Drawing.Size(634, 318);
      this.txtCopyright.TabIndex = 0;
      this.txtCopyright.Text = String.Empty;
      // 
      // frmAbout
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.BackColor = System.Drawing.Color.LightGray;
      this.ClientSize = new System.Drawing.Size(669, 619);
      this.ControlBox = false;
      this.Controls.Add(this.TabControl1);
      this.Controls.Add(this.lblEntwicklerEmailText);
      this.Controls.Add(this.lblEntwicklerEmail);
      this.Controls.Add(this.lblEntwicklerTelefonText);
      this.Controls.Add(this.lblEntwicklerTelefon);
      this.Controls.Add(this.lblEntwicklerText);
      this.Controls.Add(this.lblEntwickler);
      this.Controls.Add(this.lblProgrammverzeichnis);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblProgrammverzeichnisText);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.lblEmailText);
      this.Controls.Add(this.lblEmail);
      this.Controls.Add(this.lblTelefonText);
      this.Controls.Add(this.lblTelefon);
      this.Controls.Add(this.lblAutor);
      this.Controls.Add(this.lblVertrieb);
      this.Controls.Add(this.lblCopyright);
      this.Controls.Add(this.lblVersion);
      this.Controls.Add(this.lblProgramm);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAbout";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Über das Programm";
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmAbout_Paint);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdAssemblies)).EndInit();
      this.TabControl1.ResumeLayout(false);
      this.tabDatenbank.ResumeLayout(false);
      this.tabModule.ResumeLayout(false);
      this.tabCopyrights.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmAbout_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			//Helper.DrawFrame3D(e.Graphics, 0, 0, this.Width, this.Height);
		}

		private Version GetNetFXVersion(Assembly assembly)
		{
			if (assembly == null)
				throw new ArgumentNullException("assembly");

			Version version = new Version(1, 0);
			foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
			{
				Assembly refAssembly = null;
				try
				{
					refAssembly = Assembly.Load(assemblyName);
				}
				catch
				{
					continue;
				}

				if (refAssembly != null)
				{
					AssemblyProductAttribute[] productAttr = (AssemblyProductAttribute[])refAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
					if (productAttr.Length == 1)
						if (productAttr[0].Product == "Microsoft® .NET Framework" && assemblyName.Version > version)
							version = assemblyName.Version;
				}
			}

			return version;
		}

		private DataTable ReferencedAssemblies(Assembly assembly)
		{
			DataTable liste = new DataTable();

			liste.Columns.Add("Name");
			liste.Columns.Add("Version");

			foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
			{
				if (assemblyName.Name.Length <= 6 || assemblyName.Name.Substring(0, 6) != "System")
				{
					DataRow row = liste.NewRow();
					row["Name"] = assemblyName.Name;
					row["Version"] = assemblyName.Version.ToString();

					liste.Rows.Add(row);
				}
			}

			return liste;
		}

	}
}

