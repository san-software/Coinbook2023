using System.Windows.Forms;
using System;

namespace Coinbook
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		public void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuImportCoinbook3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuPrint = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuPrintSammlung = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintDoubletten = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrintReporting2Sammlung = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintReporting2Doubletten = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrintFehllisten = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint1 = new System.Windows.Forms.ToolStripButton();
            this.btnWert = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNationZurueck = new System.Windows.Forms.ToolStripButton();
            this.lblNation2 = new System.Windows.Forms.ToolStripLabel();
            this.btnNationVor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.btnÄraZurueck = new System.Windows.Forms.ToolStripButton();
            this.lblÄra2 = new System.Windows.Forms.ToolStripLabel();
            this.btnÄraVor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGebietZurueck = new System.Windows.Forms.ToolStripButton();
            this.lblGebiet2 = new System.Windows.Forms.ToolStripLabel();
            this.btnGebietVor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.btnWaehrungZurueck = new System.Windows.Forms.ToolStripButton();
            this.lblWährung2 = new System.Windows.Forms.ToolStripLabel();
            this.btnWaehrungVor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMuenzwertZurueck = new System.Windows.Forms.ToolStripButton();
            this.lblNominale2 = new System.Windows.Forms.ToolStripLabel();
            this.btnMuenzwertVor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.btnJahrZurueck = new System.Windows.Forms.ToolStripButton();
            this.lblJahr2 = new System.Windows.Forms.ToolStripLabel();
            this.btnJahrVor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDown = new System.Windows.Forms.ToolStripButton();
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.btnKurse = new System.Windows.Forms.ToolStripButton();
            this.btnPrägeanstalt = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBackup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMagnifier = new System.Windows.Forms.ToolStripButton();
            this.btnTeamviewer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuNationVor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuNationZurück = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuÄraVor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuÄraZurück = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuGebietVor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuGebietZurück = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuMünzdetails = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuMünzeAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuDeleteCoin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuEigeneKatalognummern = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuPreise = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuAuktionen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuPrägeanstalten = new System.Windows.Forms.ToolStripMenuItem();
            this.mainmen = new System.Windows.Forms.MenuStrip();
            this.mnuDatei = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuWerteSammlung = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWerteDoublette = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuReportingSammlung = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportingDoubletten = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuReporting2Sammlung = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReporting2Doubletten = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuReportingFehllisten = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuKostenSammlung = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKostenDoubletten = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEnde = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDBStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrägeanstalten = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMünzdetails = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMünzeAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteCoin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickEditSammlung = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEigeneKatalognummern = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPreise = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAuktionen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuKurse = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickEditDubletten = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNavigation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNationVor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNationZurueck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuÄraVor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuÄraZurueck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGebietVor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGebietZurueck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReorg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatabaseTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuImportCB2006 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImport2x = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImport2XML = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAktivieren = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLupe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLupeSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuColumnWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWeb = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNews = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFAQ = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWebOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSupport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGästebuch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModulUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateVonCD = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbonnements = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloudBackupBestellen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExtras = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHandbuchPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTeamviewer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreateDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSwitchDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13_5 = new System.Windows.Forms.ToolStripSeparator();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.mnuWert = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuWerteSammlung = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuWerteDoubletten = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dlgBackup = new System.Windows.Forms.FolderBrowserDialog();
            this.bgwUpdate = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splashPanel1 = new Syncfusion.Windows.Forms.Tools.SplashPanel();
            this.labelEx1 = new System.Windows.Forms.Label();
            this.lblStatusleiste = new System.Windows.Forms.Label();
            this.lblAnzeige = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.pgbModul = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.pgbDetails = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.ctlMain = new Coinbook.usrMain();
            this.toolStrip1.SuspendLayout();
            this.mnuPrint.SuspendLayout();
            this.cmnuStrip.SuspendLayout();
            this.mainmen.SuspendLayout();
            this.mnuWert.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.splashPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgbModul)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgbDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuImportCoinbook3
            // 
            this.mnuImportCoinbook3.Name = "mnuImportCoinbook3";
            this.mnuImportCoinbook3.Size = new System.Drawing.Size(303, 26);
            this.mnuImportCoinbook3.Text = "Import von Coinbook 3.0.0.x";
            this.mnuImportCoinbook3.Click += new System.EventHandler(this.mnuInportCoinbook3_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStrip1.CanOverflow = false;
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 6);
            this.toolStrip1.ContextMenuStrip = this.mnuPrint;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.toolStripSeparator4,
            this.btnPrint1,
            this.btnWert,
            this.toolStripSeparator9,
            this.btnNationZurueck,
            this.lblNation2,
            this.btnNationVor,
            this.toolStripSeparator16,
            this.btnÄraZurueck,
            this.lblÄra2,
            this.btnÄraVor,
            this.toolStripSeparator17,
            this.btnGebietZurueck,
            this.lblGebiet2,
            this.btnGebietVor,
            this.toolStripSeparator15,
            this.btnWaehrungZurueck,
            this.lblWährung2,
            this.btnWaehrungVor,
            this.toolStripSeparator18,
            this.btnMuenzwertZurueck,
            this.lblNominale2,
            this.btnMuenzwertVor,
            this.toolStripSeparator19,
            this.btnJahrZurueck,
            this.lblJahr2,
            this.btnJahrVor,
            this.toolStripSeparator5,
            this.btnDown,
            this.btnUp,
            this.toolStripSeparator21,
            this.btnSettings,
            this.btnKurse,
            this.btnPrägeanstalt,
            this.toolStripSeparator14,
            this.btnBackup,
            this.toolStripSeparator20,
            this.btnUpdate,
            this.toolStripSeparator22,
            this.btnMagnifier,
            this.btnTeamviewer,
            this.toolStripSeparator23});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1313, 30);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuPrint
            // 
            this.mnuPrint.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuPrint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPrintSammlung,
            this.mnuPrintDoubletten,
            this.toolStripMenuItem14,
            this.mnuPrintReporting2Sammlung,
            this.mnuPrintReporting2Doubletten,
            this.toolStripMenuItem15,
            this.mnuPrintFehllisten});
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(305, 146);
            // 
            // mnuPrintSammlung
            // 
            this.mnuPrintSammlung.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrintSammlung.Image")));
            this.mnuPrintSammlung.Name = "mnuPrintSammlung";
            this.mnuPrintSammlung.Size = new System.Drawing.Size(304, 26);
            this.mnuPrintSammlung.Text = "Reporting Sammlung";
            this.mnuPrintSammlung.Click += new System.EventHandler(this.mnuReportingSammlung_Click);
            // 
            // mnuPrintDoubletten
            // 
            this.mnuPrintDoubletten.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrintDoubletten.Image")));
            this.mnuPrintDoubletten.Name = "mnuPrintDoubletten";
            this.mnuPrintDoubletten.Size = new System.Drawing.Size(304, 26);
            this.mnuPrintDoubletten.Text = "Reporting Doubletten";
            this.mnuPrintDoubletten.Click += new System.EventHandler(this.mnuReportingDoubletten_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(301, 6);
            // 
            // mnuPrintReporting2Sammlung
            // 
            this.mnuPrintReporting2Sammlung.Name = "mnuPrintReporting2Sammlung";
            this.mnuPrintReporting2Sammlung.Size = new System.Drawing.Size(304, 26);
            this.mnuPrintReporting2Sammlung.Text = "Reporting 2 Sammlung (nur Katalogpreise)";
            this.mnuPrintReporting2Sammlung.Click += new System.EventHandler(this.mnuReporting2Sammlung_Click);
            // 
            // mnuPrintReporting2Doubletten
            // 
            this.mnuPrintReporting2Doubletten.Name = "mnuPrintReporting2Doubletten";
            this.mnuPrintReporting2Doubletten.Size = new System.Drawing.Size(304, 26);
            this.mnuPrintReporting2Doubletten.Text = "Reporting 2 Doubletten (nur Katalogpreise)";
            this.mnuPrintReporting2Doubletten.Click += new System.EventHandler(this.mnuReporting2Doubletten_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(301, 6);
            // 
            // mnuPrintFehllisten
            // 
            this.mnuPrintFehllisten.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrintFehllisten.Image")));
            this.mnuPrintFehllisten.Name = "mnuPrintFehllisten";
            this.mnuPrintFehllisten.Size = new System.Drawing.Size(304, 26);
            this.mnuPrintFehllisten.Text = "Reporting Fehllisten";
            this.mnuPrintFehllisten.Click += new System.EventHandler(this.mnuReportingFehllisten_Click);
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.Text = "Programm beenden";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.toolStripSeparator4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
            // 
            // btnPrint1
            // 
            this.btnPrint1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint1.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint1.Image")));
            this.btnPrint1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint1.Name = "btnPrint1";
            this.btnPrint1.Size = new System.Drawing.Size(24, 24);
            this.btnPrint1.Text = "Reporting";
            this.btnPrint1.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnWert
            // 
            this.btnWert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWert.Image = ((System.Drawing.Image)(resources.GetObject("btnWert.Image")));
            this.btnWert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWert.Name = "btnWert";
            this.btnWert.Size = new System.Drawing.Size(24, 24);
            this.btnWert.Text = "Werte berechnen";
            this.btnWert.Click += new System.EventHandler(this.mnuWert_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 23);
            // 
            // btnNationZurueck
            // 
            this.btnNationZurueck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNationZurueck.Image = ((System.Drawing.Image)(resources.GetObject("btnNationZurueck.Image")));
            this.btnNationZurueck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNationZurueck.Name = "btnNationZurueck";
            this.btnNationZurueck.Size = new System.Drawing.Size(24, 24);
            this.btnNationZurueck.Text = "Nation zurück";
            this.btnNationZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // lblNation2
            // 
            this.lblNation2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblNation2.Name = "lblNation2";
            this.lblNation2.Size = new System.Drawing.Size(43, 15);
            this.lblNation2.Text = "Nation";
            this.lblNation2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnNationVor
            // 
            this.btnNationVor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNationVor.Image = ((System.Drawing.Image)(resources.GetObject("btnNationVor.Image")));
            this.btnNationVor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNationVor.Name = "btnNationVor";
            this.btnNationVor.Size = new System.Drawing.Size(24, 24);
            this.btnNationVor.Text = "Nation vorwärts";
            this.btnNationVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 23);
            // 
            // btnÄraZurueck
            // 
            this.btnÄraZurueck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnÄraZurueck.Image = ((System.Drawing.Image)(resources.GetObject("btnÄraZurueck.Image")));
            this.btnÄraZurueck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnÄraZurueck.Name = "btnÄraZurueck";
            this.btnÄraZurueck.Size = new System.Drawing.Size(24, 24);
            this.btnÄraZurueck.Text = "Ära zurück";
            this.btnÄraZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // lblÄra2
            // 
            this.lblÄra2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 2);
            this.lblÄra2.Name = "lblÄra2";
            this.lblÄra2.Size = new System.Drawing.Size(25, 15);
            this.lblÄra2.Text = "Ära";
            // 
            // btnÄraVor
            // 
            this.btnÄraVor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnÄraVor.Image = ((System.Drawing.Image)(resources.GetObject("btnÄraVor.Image")));
            this.btnÄraVor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnÄraVor.Name = "btnÄraVor";
            this.btnÄraVor.Size = new System.Drawing.Size(24, 24);
            this.btnÄraVor.Text = "Ära vorwärts";
            this.btnÄraVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 23);
            // 
            // btnGebietZurueck
            // 
            this.btnGebietZurueck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGebietZurueck.Image = ((System.Drawing.Image)(resources.GetObject("btnGebietZurueck.Image")));
            this.btnGebietZurueck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGebietZurueck.Name = "btnGebietZurueck";
            this.btnGebietZurueck.Size = new System.Drawing.Size(24, 24);
            this.btnGebietZurueck.Text = "Gebiet zurück";
            this.btnGebietZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // lblGebiet2
            // 
            this.lblGebiet2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 2);
            this.lblGebiet2.Name = "lblGebiet2";
            this.lblGebiet2.Size = new System.Drawing.Size(41, 15);
            this.lblGebiet2.Text = "Gebiet";
            // 
            // btnGebietVor
            // 
            this.btnGebietVor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGebietVor.Image = ((System.Drawing.Image)(resources.GetObject("btnGebietVor.Image")));
            this.btnGebietVor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGebietVor.Name = "btnGebietVor";
            this.btnGebietVor.Size = new System.Drawing.Size(24, 24);
            this.btnGebietVor.Text = "Gebiet vorwärts";
            this.btnGebietVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.toolStripSeparator15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 23);
            // 
            // btnWaehrungZurueck
            // 
            this.btnWaehrungZurueck.AutoSize = false;
            this.btnWaehrungZurueck.BackColor = System.Drawing.Color.Gainsboro;
            this.btnWaehrungZurueck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnWaehrungZurueck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWaehrungZurueck.Image = ((System.Drawing.Image)(resources.GetObject("btnWaehrungZurueck.Image")));
            this.btnWaehrungZurueck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWaehrungZurueck.Name = "btnWaehrungZurueck";
            this.btnWaehrungZurueck.Size = new System.Drawing.Size(18, 18);
            this.btnWaehrungZurueck.Text = "Währung zurück";
            this.btnWaehrungZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // lblWährung2
            // 
            this.lblWährung2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblWährung2.Name = "lblWährung2";
            this.lblWährung2.Size = new System.Drawing.Size(56, 15);
            this.lblWährung2.Text = "Währung";
            // 
            // btnWaehrungVor
            // 
            this.btnWaehrungVor.AutoSize = false;
            this.btnWaehrungVor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWaehrungVor.Image = ((System.Drawing.Image)(resources.GetObject("btnWaehrungVor.Image")));
            this.btnWaehrungVor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWaehrungVor.Name = "btnWaehrungVor";
            this.btnWaehrungVor.Size = new System.Drawing.Size(18, 18);
            this.btnWaehrungVor.Text = "Währung vorwärts";
            this.btnWaehrungVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 23);
            // 
            // btnMuenzwertZurueck
            // 
            this.btnMuenzwertZurueck.AutoSize = false;
            this.btnMuenzwertZurueck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMuenzwertZurueck.Image = ((System.Drawing.Image)(resources.GetObject("btnMuenzwertZurueck.Image")));
            this.btnMuenzwertZurueck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMuenzwertZurueck.Name = "btnMuenzwertZurueck";
            this.btnMuenzwertZurueck.Size = new System.Drawing.Size(18, 18);
            this.btnMuenzwertZurueck.Text = "Münzwert zurück";
            this.btnMuenzwertZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // lblNominale2
            // 
            this.lblNominale2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblNominale2.Name = "lblNominale2";
            this.lblNominale2.Size = new System.Drawing.Size(60, 15);
            this.lblNominale2.Text = "Münzwert";
            // 
            // btnMuenzwertVor
            // 
            this.btnMuenzwertVor.AutoSize = false;
            this.btnMuenzwertVor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMuenzwertVor.Image = ((System.Drawing.Image)(resources.GetObject("btnMuenzwertVor.Image")));
            this.btnMuenzwertVor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMuenzwertVor.Name = "btnMuenzwertVor";
            this.btnMuenzwertVor.Size = new System.Drawing.Size(18, 18);
            this.btnMuenzwertVor.Text = "Münzwert vorwärts";
            this.btnMuenzwertVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 23);
            // 
            // btnJahrZurueck
            // 
            this.btnJahrZurueck.AutoSize = false;
            this.btnJahrZurueck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnJahrZurueck.Image = ((System.Drawing.Image)(resources.GetObject("btnJahrZurueck.Image")));
            this.btnJahrZurueck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnJahrZurueck.Name = "btnJahrZurueck";
            this.btnJahrZurueck.Size = new System.Drawing.Size(18, 18);
            this.btnJahrZurueck.Text = "Jahr zurück";
            this.btnJahrZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // lblJahr2
            // 
            this.lblJahr2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblJahr2.Name = "lblJahr2";
            this.lblJahr2.Size = new System.Drawing.Size(28, 15);
            this.lblJahr2.Text = "Jahr";
            // 
            // btnJahrVor
            // 
            this.btnJahrVor.AutoSize = false;
            this.btnJahrVor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnJahrVor.Image = ((System.Drawing.Image)(resources.GetObject("btnJahrVor.Image")));
            this.btnJahrVor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnJahrVor.Name = "btnJahrVor";
            this.btnJahrVor.Size = new System.Drawing.Size(18, 18);
            this.btnJahrVor.Text = "Jahr vorwärts";
            this.btnJahrVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.toolStripSeparator5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 23);
            // 
            // btnDown
            // 
            this.btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(24, 24);
            this.btnDown.Text = "Nächste Sammlungsmünze";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(24, 24);
            this.btnUp.Text = "Vorherhehende Sammlungsmünze";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(6, 23);
            // 
            // btnSettings
            // 
            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(24, 24);
            this.btnSettings.Text = "Einstellungen";
            this.btnSettings.Click += new System.EventHandler(this.tsbutSettings_Click);
            // 
            // btnKurse
            // 
            this.btnKurse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnKurse.Image = ((System.Drawing.Image)(resources.GetObject("btnKurse.Image")));
            this.btnKurse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnKurse.Name = "btnKurse";
            this.btnKurse.Size = new System.Drawing.Size(24, 24);
            this.btnKurse.Text = "Kurse anpassen";
            this.btnKurse.Click += new System.EventHandler(this.btnKurse_Click);
            // 
            // btnPrägeanstalt
            // 
            this.btnPrägeanstalt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrägeanstalt.Image = ((System.Drawing.Image)(resources.GetObject("btnPrägeanstalt.Image")));
            this.btnPrägeanstalt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrägeanstalt.Name = "btnPrägeanstalt";
            this.btnPrägeanstalt.Size = new System.Drawing.Size(24, 24);
            this.btnPrägeanstalt.Text = "Prägeanstalten";
            this.btnPrägeanstalt.Click += new System.EventHandler(this.mnuPrägeanstalten_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.toolStripSeparator14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 23);
            // 
            // btnBackup
            // 
            this.btnBackup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBackup.Image = ((System.Drawing.Image)(resources.GetObject("btnBackup.Image")));
            this.btnBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(24, 24);
            this.btnBackup.Text = "Datensicherung";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(6, 23);
            // 
            // btnUpdate
            // 
            this.btnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(24, 24);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(6, 23);
            // 
            // btnMagnifier
            // 
            this.btnMagnifier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMagnifier.DoubleClickEnabled = true;
            this.btnMagnifier.Image = ((System.Drawing.Image)(resources.GetObject("btnMagnifier.Image")));
            this.btnMagnifier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMagnifier.Name = "btnMagnifier";
            this.btnMagnifier.Size = new System.Drawing.Size(24, 24);
            this.btnMagnifier.Text = "Bildschirmlupe";
            this.btnMagnifier.ToolTipText = "Bildschirmlupe";
            this.btnMagnifier.Click += new System.EventHandler(this.mnuLupe_Click);
            // 
            // btnTeamviewer
            // 
            this.btnTeamviewer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTeamviewer.Image = ((System.Drawing.Image)(resources.GetObject("btnTeamviewer.Image")));
            this.btnTeamviewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTeamviewer.Name = "btnTeamviewer";
            this.btnTeamviewer.Size = new System.Drawing.Size(24, 24);
            this.btnTeamviewer.Text = "Teamviewer starten";
            this.btnTeamviewer.Click += new System.EventHandler(this.mnuTeamviewer_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 23);
            // 
            // cmnuStrip
            // 
            this.cmnuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuNationVor,
            this.cmnuNationZurück,
            this.toolStripMenuItem6,
            this.cmnuÄraVor,
            this.cmnuÄraZurück,
            this.toolStripMenuItem7,
            this.cmnuGebietVor,
            this.cmnuGebietZurück,
            this.toolStripSeparator7,
            this.cmnuUp,
            this.cmnuDown,
            this.toolStripMenuItem9,
            this.cmnuMünzdetails,
            this.cmnuMünzeAdd,
            this.cmenuDeleteCoin,
            this.toolStripMenuItem1,
            this.cmnuEigeneKatalognummern,
            this.cmnuPicture,
            this.cmnuPreise,
            this.cmnuAuktionen,
            this.toolStripMenuItem2,
            this.cmnuPrägeanstalten});
            this.cmnuStrip.Name = "cmStrip";
            this.cmnuStrip.Size = new System.Drawing.Size(311, 456);
            // 
            // cmnuNationVor
            // 
            this.cmnuNationVor.Image = ((System.Drawing.Image)(resources.GetObject("cmnuNationVor.Image")));
            this.cmnuNationVor.Name = "cmnuNationVor";
            this.cmnuNationVor.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.cmnuNationVor.Size = new System.Drawing.Size(310, 26);
            this.cmnuNationVor.Text = "Nation vor";
            this.cmnuNationVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // cmnuNationZurück
            // 
            this.cmnuNationZurück.Image = ((System.Drawing.Image)(resources.GetObject("cmnuNationZurück.Image")));
            this.cmnuNationZurück.Name = "cmnuNationZurück";
            this.cmnuNationZurück.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
            this.cmnuNationZurück.Size = new System.Drawing.Size(310, 26);
            this.cmnuNationZurück.Text = "Nation zurück";
            this.cmnuNationZurück.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(307, 6);
            // 
            // cmnuÄraVor
            // 
            this.cmnuÄraVor.Image = ((System.Drawing.Image)(resources.GetObject("cmnuÄraVor.Image")));
            this.cmnuÄraVor.Name = "cmnuÄraVor";
            this.cmnuÄraVor.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.cmnuÄraVor.Size = new System.Drawing.Size(310, 26);
            this.cmnuÄraVor.Text = "Aera vor";
            this.cmnuÄraVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // cmnuÄraZurück
            // 
            this.cmnuÄraZurück.Image = ((System.Drawing.Image)(resources.GetObject("cmnuÄraZurück.Image")));
            this.cmnuÄraZurück.Name = "cmnuÄraZurück";
            this.cmnuÄraZurück.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.cmnuÄraZurück.Size = new System.Drawing.Size(310, 26);
            this.cmnuÄraZurück.Text = "Aera zurück";
            this.cmnuÄraZurück.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(307, 6);
            // 
            // cmnuGebietVor
            // 
            this.cmnuGebietVor.Image = ((System.Drawing.Image)(resources.GetObject("cmnuGebietVor.Image")));
            this.cmnuGebietVor.Name = "cmnuGebietVor";
            this.cmnuGebietVor.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.cmnuGebietVor.Size = new System.Drawing.Size(310, 26);
            this.cmnuGebietVor.Text = "Gebiet vor";
            this.cmnuGebietVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // cmnuGebietZurück
            // 
            this.cmnuGebietZurück.Image = ((System.Drawing.Image)(resources.GetObject("cmnuGebietZurück.Image")));
            this.cmnuGebietZurück.Name = "cmnuGebietZurück";
            this.cmnuGebietZurück.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.cmnuGebietZurück.Size = new System.Drawing.Size(310, 26);
            this.cmnuGebietZurück.Text = "Gebiet zurück";
            this.cmnuGebietZurück.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(307, 6);
            // 
            // cmnuUp
            // 
            this.cmnuUp.Image = ((System.Drawing.Image)(resources.GetObject("cmnuUp.Image")));
            this.cmnuUp.Name = "cmnuUp";
            this.cmnuUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6)));
            this.cmnuUp.Size = new System.Drawing.Size(310, 26);
            this.cmnuUp.Text = "Vorhergehende Sammlungsmünze";
            this.cmnuUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // cmnuDown
            // 
            this.cmnuDown.Image = ((System.Drawing.Image)(resources.GetObject("cmnuDown.Image")));
            this.cmnuDown.Name = "cmnuDown";
            this.cmnuDown.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.cmnuDown.Size = new System.Drawing.Size(310, 26);
            this.cmnuDown.Text = "Nächste Sammlungsmünze";
            this.cmnuDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(307, 6);
            // 
            // cmnuMünzdetails
            // 
            this.cmnuMünzdetails.Image = ((System.Drawing.Image)(resources.GetObject("cmnuMünzdetails.Image")));
            this.cmnuMünzdetails.Name = "cmnuMünzdetails";
            this.cmnuMünzdetails.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.cmnuMünzdetails.Size = new System.Drawing.Size(310, 26);
            this.cmnuMünzdetails.Text = "Münzdetails";
            this.cmnuMünzdetails.Click += new System.EventHandler(this.mnuMünzdetails_Click);
            // 
            // cmnuMünzeAdd
            // 
            this.cmnuMünzeAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmnuMünzeAdd.Image")));
            this.cmnuMünzeAdd.Name = "cmnuMünzeAdd";
            this.cmnuMünzeAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.cmnuMünzeAdd.Size = new System.Drawing.Size(310, 26);
            this.cmnuMünzeAdd.Text = "Münze hinzufügen";
            this.cmnuMünzeAdd.Click += new System.EventHandler(this.mnuMünzeAdd_Click);
            // 
            // cmenuDeleteCoin
            // 
            this.cmenuDeleteCoin.Image = ((System.Drawing.Image)(resources.GetObject("cmenuDeleteCoin.Image")));
            this.cmenuDeleteCoin.Name = "cmenuDeleteCoin";
            this.cmenuDeleteCoin.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.cmenuDeleteCoin.Size = new System.Drawing.Size(310, 26);
            this.cmenuDeleteCoin.Text = "Münze löschen";
            this.cmenuDeleteCoin.Click += new System.EventHandler(this.mnuDeleteCoin_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(307, 6);
            // 
            // cmnuEigeneKatalognummern
            // 
            this.cmnuEigeneKatalognummern.Image = ((System.Drawing.Image)(resources.GetObject("cmnuEigeneKatalognummern.Image")));
            this.cmnuEigeneKatalognummern.Name = "cmnuEigeneKatalognummern";
            this.cmnuEigeneKatalognummern.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.cmnuEigeneKatalognummern.Size = new System.Drawing.Size(310, 26);
            this.cmnuEigeneKatalognummern.Text = "Eigene Katalognummern bearbeiten";
            this.cmnuEigeneKatalognummern.Click += new System.EventHandler(this.mnuEigeneKatalognummern_Click);
            // 
            // cmnuPicture
            // 
            this.cmnuPicture.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPicture.Image")));
            this.cmnuPicture.Name = "cmnuPicture";
            this.cmnuPicture.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.cmnuPicture.Size = new System.Drawing.Size(310, 26);
            this.cmnuPicture.Text = "Eigenes Bild importieren";
            this.cmnuPicture.Click += new System.EventHandler(this.mnuPicture_Click);
            // 
            // cmnuPreise
            // 
            this.cmnuPreise.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPreise.Image")));
            this.cmnuPreise.Name = "cmnuPreise";
            this.cmnuPreise.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.cmnuPreise.Size = new System.Drawing.Size(310, 26);
            this.cmnuPreise.Text = "Eigene Preise bearbeiten";
            this.cmnuPreise.Click += new System.EventHandler(this.mnuPreise_Click);
            // 
            // cmnuAuktionen
            // 
            this.cmnuAuktionen.Image = ((System.Drawing.Image)(resources.GetObject("cmnuAuktionen.Image")));
            this.cmnuAuktionen.Name = "cmnuAuktionen";
            this.cmnuAuktionen.Size = new System.Drawing.Size(310, 26);
            this.cmnuAuktionen.Text = "Münz-Auktionen verwalten";
            this.cmnuAuktionen.Click += new System.EventHandler(this.mnuAuktionen_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(307, 6);
            // 
            // cmnuPrägeanstalten
            // 
            this.cmnuPrägeanstalten.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPrägeanstalten.Image")));
            this.cmnuPrägeanstalten.Name = "cmnuPrägeanstalten";
            this.cmnuPrägeanstalten.Size = new System.Drawing.Size(310, 26);
            this.cmnuPrägeanstalten.Text = "Münzprägeanstalten";
            this.cmnuPrägeanstalten.Click += new System.EventHandler(this.mnuPrägeanstalten_Click);
            // 
            // mainmen
            // 
            this.mainmen.BackColor = System.Drawing.SystemColors.Control;
            this.mainmen.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainmen.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainmen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDatei,
            this.mnuModule,
            this.mnuEdit,
            this.mnuNavigation,
            this.mnuOptionen,
            this.mnuWeb,
            this.mnuExtras,
            this.mnuBackup,
            this.hilfeToolStripMenuItem,
            this.mnuLanguage,
            this.mnuCreateDatabase,
            this.mnuSwitchDatabase});
            this.mainmen.Location = new System.Drawing.Point(0, 0);
            this.mainmen.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.mainmen.Name = "mainmen";
            this.mainmen.Padding = new System.Windows.Forms.Padding(7, 1, 0, 1);
            this.mainmen.Size = new System.Drawing.Size(1313, 24);
            this.mainmen.TabIndex = 14;
            this.mainmen.Text = "menuStrip1";
            // 
            // mnuDatei
            // 
            this.mnuDatei.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.mnuWerteSammlung,
            this.mnuWerteDoublette,
            this.toolStripMenuItem5,
            this.mnuReportingSammlung,
            this.mnuReportingDoubletten,
            this.toolStripMenuItem17,
            this.mnuReporting2Sammlung,
            this.mnuReporting2Doubletten,
            this.toolStripMenuItem16,
            this.mnuReportingFehllisten,
            this.toolStripSeparator3,
            this.mnuKostenSammlung,
            this.mnuKostenDoubletten,
            this.toolStripMenuItem10,
            this.mnuEnde});
            this.mnuDatei.Name = "mnuDatei";
            this.mnuDatei.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.mnuDatei.Size = new System.Drawing.Size(48, 22);
            this.mnuDatei.Text = "Datei";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuWerteSammlung
            // 
            this.mnuWerteSammlung.Image = ((System.Drawing.Image)(resources.GetObject("mnuWerteSammlung.Image")));
            this.mnuWerteSammlung.Name = "mnuWerteSammlung";
            this.mnuWerteSammlung.Size = new System.Drawing.Size(323, 26);
            this.mnuWerteSammlung.Text = "Werte berechnen Sammlung";
            this.mnuWerteSammlung.Click += new System.EventHandler(this.mnuWerteSammlung_Click);
            // 
            // mnuWerteDoublette
            // 
            this.mnuWerteDoublette.Image = ((System.Drawing.Image)(resources.GetObject("mnuWerteDoublette.Image")));
            this.mnuWerteDoublette.Name = "mnuWerteDoublette";
            this.mnuWerteDoublette.Size = new System.Drawing.Size(323, 26);
            this.mnuWerteDoublette.Text = "Werte berechnen Doublette";
            this.mnuWerteDoublette.Click += new System.EventHandler(this.mnuWerteDoublette_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuReportingSammlung
            // 
            this.mnuReportingSammlung.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportingSammlung.Image")));
            this.mnuReportingSammlung.Name = "mnuReportingSammlung";
            this.mnuReportingSammlung.Size = new System.Drawing.Size(323, 26);
            this.mnuReportingSammlung.Text = "Reporting Sammlung";
            this.mnuReportingSammlung.Click += new System.EventHandler(this.mnuReportingSammlung_Click);
            // 
            // mnuReportingDoubletten
            // 
            this.mnuReportingDoubletten.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportingDoubletten.Image")));
            this.mnuReportingDoubletten.Name = "mnuReportingDoubletten";
            this.mnuReportingDoubletten.Size = new System.Drawing.Size(323, 26);
            this.mnuReportingDoubletten.Text = "Reporting Doubletten";
            this.mnuReportingDoubletten.Click += new System.EventHandler(this.mnuReportingDoubletten_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuReporting2Sammlung
            // 
            this.mnuReporting2Sammlung.Image = ((System.Drawing.Image)(resources.GetObject("mnuReporting2Sammlung.Image")));
            this.mnuReporting2Sammlung.Name = "mnuReporting2Sammlung";
            this.mnuReporting2Sammlung.Size = new System.Drawing.Size(323, 26);
            this.mnuReporting2Sammlung.Text = "Reporting 2 Sammlung (nur Katalogpreise)";
            this.mnuReporting2Sammlung.Click += new System.EventHandler(this.mnuReporting2Sammlung_Click);
            // 
            // mnuReporting2Doubletten
            // 
            this.mnuReporting2Doubletten.Image = ((System.Drawing.Image)(resources.GetObject("mnuReporting2Doubletten.Image")));
            this.mnuReporting2Doubletten.Name = "mnuReporting2Doubletten";
            this.mnuReporting2Doubletten.Size = new System.Drawing.Size(323, 26);
            this.mnuReporting2Doubletten.Text = "Reporting 2 Doubletten (nur Katalogpreise)";
            this.mnuReporting2Doubletten.Click += new System.EventHandler(this.mnuReporting2Doubletten_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuReportingFehllisten
            // 
            this.mnuReportingFehllisten.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportingFehllisten.Image")));
            this.mnuReportingFehllisten.Name = "mnuReportingFehllisten";
            this.mnuReportingFehllisten.Size = new System.Drawing.Size(323, 26);
            this.mnuReportingFehllisten.Text = "Reporting Fehllisten";
            this.mnuReportingFehllisten.Click += new System.EventHandler(this.mnuReportingFehllisten_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuKostenSammlung
            // 
            this.mnuKostenSammlung.Image = ((System.Drawing.Image)(resources.GetObject("mnuKostenSammlung.Image")));
            this.mnuKostenSammlung.Name = "mnuKostenSammlung";
            this.mnuKostenSammlung.Size = new System.Drawing.Size(323, 26);
            this.mnuKostenSammlung.Text = "Kosten berechnen Sammlung";
            this.mnuKostenSammlung.Click += new System.EventHandler(this.mnuKostenSammlung_Click);
            // 
            // mnuKostenDoubletten
            // 
            this.mnuKostenDoubletten.Image = ((System.Drawing.Image)(resources.GetObject("mnuKostenDoubletten.Image")));
            this.mnuKostenDoubletten.Name = "mnuKostenDoubletten";
            this.mnuKostenDoubletten.Size = new System.Drawing.Size(323, 26);
            this.mnuKostenDoubletten.Text = "Kosten berechnen Doubletten";
            this.mnuKostenDoubletten.Click += new System.EventHandler(this.mnuKostenDoubletten_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(320, 6);
            // 
            // mnuEnde
            // 
            this.mnuEnde.Image = ((System.Drawing.Image)(resources.GetObject("mnuEnde.Image")));
            this.mnuEnde.Name = "mnuEnde";
            this.mnuEnde.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuEnde.Size = new System.Drawing.Size(323, 26);
            this.mnuEnde.Text = "Anwendung beenden";
            this.mnuEnde.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // mnuModule
            // 
            this.mnuModule.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDBStatus,
            this.toolStripSeparator1,
            this.mnuPrägeanstalten});
            this.mnuModule.Name = "mnuModule";
            this.mnuModule.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.mnuModule.Size = new System.Drawing.Size(60, 22);
            this.mnuModule.Text = "Module";
            // 
            // mnuDBStatus
            // 
            this.mnuDBStatus.Image = ((System.Drawing.Image)(resources.GetObject("mnuDBStatus.Image")));
            this.mnuDBStatus.Name = "mnuDBStatus";
            this.mnuDBStatus.Size = new System.Drawing.Size(201, 26);
            this.mnuDBStatus.Text = "Status der Datenbank";
            this.mnuDBStatus.Click += new System.EventHandler(this.mnuDBStatus_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // mnuPrägeanstalten
            // 
            this.mnuPrägeanstalten.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrägeanstalten.Image")));
            this.mnuPrägeanstalten.Name = "mnuPrägeanstalten";
            this.mnuPrägeanstalten.Size = new System.Drawing.Size(201, 26);
            this.mnuPrägeanstalten.Text = "Münzprägeanstalten";
            this.mnuPrägeanstalten.Click += new System.EventHandler(this.mnuPrägeanstalten_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMünzdetails,
            this.mnuMünzeAdd,
            this.mnuDeleteCoin,
            this.mnuQuickEditSammlung,
            this.toolStripSeparator6,
            this.mnuEigeneKatalognummern,
            this.mnuPicture,
            this.mnuPreise,
            this.mnuAuktionen,
            this.toolStripMenuItem3,
            this.mnuKurse,
            this.mnuQuickEditDubletten});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.mnuEdit.Size = new System.Drawing.Size(80, 22);
            this.mnuEdit.Text = "Bearbeiten";
            // 
            // mnuMünzdetails
            // 
            this.mnuMünzdetails.Enabled = false;
            this.mnuMünzdetails.Image = ((System.Drawing.Image)(resources.GetObject("mnuMünzdetails.Image")));
            this.mnuMünzdetails.Name = "mnuMünzdetails";
            this.mnuMünzdetails.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuMünzdetails.Size = new System.Drawing.Size(328, 26);
            this.mnuMünzdetails.Text = "Münzdetails ";
            this.mnuMünzdetails.Click += new System.EventHandler(this.mnuMünzdetails_Click);
            // 
            // mnuMünzeAdd
            // 
            this.mnuMünzeAdd.Image = ((System.Drawing.Image)(resources.GetObject("mnuMünzeAdd.Image")));
            this.mnuMünzeAdd.Name = "mnuMünzeAdd";
            this.mnuMünzeAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuMünzeAdd.Size = new System.Drawing.Size(328, 26);
            this.mnuMünzeAdd.Text = "Münze hinzufügen";
            this.mnuMünzeAdd.Click += new System.EventHandler(this.mnuMünzeAdd_Click);
            // 
            // mnuDeleteCoin
            // 
            this.mnuDeleteCoin.Image = ((System.Drawing.Image)(resources.GetObject("mnuDeleteCoin.Image")));
            this.mnuDeleteCoin.Name = "mnuDeleteCoin";
            this.mnuDeleteCoin.Size = new System.Drawing.Size(328, 26);
            this.mnuDeleteCoin.Text = "Münze löschen";
            this.mnuDeleteCoin.Click += new System.EventHandler(this.mnuDeleteCoin_Click);
            // 
            // mnuQuickEditSammlung
            // 
            this.mnuQuickEditSammlung.Name = "mnuQuickEditSammlung";
            this.mnuQuickEditSammlung.Size = new System.Drawing.Size(328, 26);
            this.mnuQuickEditSammlung.Text = "QuickEdit Sammlung";
            this.mnuQuickEditSammlung.Click += new System.EventHandler(this.mnuQuickEditSammlung_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(325, 6);
            // 
            // mnuEigeneKatalognummern
            // 
            this.mnuEigeneKatalognummern.Image = ((System.Drawing.Image)(resources.GetObject("mnuEigeneKatalognummern.Image")));
            this.mnuEigeneKatalognummern.Name = "mnuEigeneKatalognummern";
            this.mnuEigeneKatalognummern.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuEigeneKatalognummern.Size = new System.Drawing.Size(328, 26);
            this.mnuEigeneKatalognummern.Text = "Eigene Katalognummern bearbeiten";
            this.mnuEigeneKatalognummern.Click += new System.EventHandler(this.mnuEigeneKatalognummern_Click);
            // 
            // mnuPicture
            // 
            this.mnuPicture.Image = ((System.Drawing.Image)(resources.GetObject("mnuPicture.Image")));
            this.mnuPicture.Name = "mnuPicture";
            this.mnuPicture.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.mnuPicture.Size = new System.Drawing.Size(328, 26);
            this.mnuPicture.Text = "Eigenes Bild bearbeiten";
            this.mnuPicture.Click += new System.EventHandler(this.mnuPicture_Click);
            // 
            // mnuPreise
            // 
            this.mnuPreise.Image = ((System.Drawing.Image)(resources.GetObject("mnuPreise.Image")));
            this.mnuPreise.Name = "mnuPreise";
            this.mnuPreise.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuPreise.Size = new System.Drawing.Size(328, 26);
            this.mnuPreise.Text = "Eigene Preise bearbeiten";
            this.mnuPreise.Click += new System.EventHandler(this.mnuPreise_Click);
            // 
            // mnuAuktionen
            // 
            this.mnuAuktionen.Image = ((System.Drawing.Image)(resources.GetObject("mnuAuktionen.Image")));
            this.mnuAuktionen.Name = "mnuAuktionen";
            this.mnuAuktionen.Size = new System.Drawing.Size(328, 26);
            this.mnuAuktionen.Text = "Münz-Auktionen verwalten";
            this.mnuAuktionen.Click += new System.EventHandler(this.mnuAuktionen_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(325, 6);
            // 
            // mnuKurse
            // 
            this.mnuKurse.Image = ((System.Drawing.Image)(resources.GetObject("mnuKurse.Image")));
            this.mnuKurse.Name = "mnuKurse";
            this.mnuKurse.Size = new System.Drawing.Size(328, 26);
            this.mnuKurse.Text = "Kurse anpassen";
            this.mnuKurse.Click += new System.EventHandler(this.mnuKurse_Click);
            // 
            // mnuQuickEditDubletten
            // 
            this.mnuQuickEditDubletten.Name = "mnuQuickEditDubletten";
            this.mnuQuickEditDubletten.Size = new System.Drawing.Size(328, 26);
            this.mnuQuickEditDubletten.Text = "QuickEdit Dubletten";
            this.mnuQuickEditDubletten.Click += new System.EventHandler(this.mnuQuickEditDubletten_Click);
            // 
            // mnuNavigation
            // 
            this.mnuNavigation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNationVor,
            this.mnuNationZurueck,
            this.toolStripSeparator10,
            this.mnuÄraVor,
            this.mnuÄraZurueck,
            this.toolStripSeparator11,
            this.mnuGebietVor,
            this.mnuGebietZurueck,
            this.toolStripMenuItem4,
            this.mnuDown,
            this.mnuUp});
            this.mnuNavigation.Name = "mnuNavigation";
            this.mnuNavigation.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.mnuNavigation.Size = new System.Drawing.Size(78, 22);
            this.mnuNavigation.Text = "Navigation";
            // 
            // mnuNationVor
            // 
            this.mnuNationVor.Image = ((System.Drawing.Image)(resources.GetObject("mnuNationVor.Image")));
            this.mnuNationVor.Name = "mnuNationVor";
            this.mnuNationVor.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuNationVor.Size = new System.Drawing.Size(328, 26);
            this.mnuNationVor.Text = "Nation vor";
            this.mnuNationVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // mnuNationZurueck
            // 
            this.mnuNationZurueck.Image = ((System.Drawing.Image)(resources.GetObject("mnuNationZurueck.Image")));
            this.mnuNationZurueck.Name = "mnuNationZurueck";
            this.mnuNationZurueck.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
            this.mnuNationZurueck.Size = new System.Drawing.Size(328, 26);
            this.mnuNationZurueck.Text = "Nation zurück";
            this.mnuNationZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(325, 6);
            // 
            // mnuÄraVor
            // 
            this.mnuÄraVor.Image = ((System.Drawing.Image)(resources.GetObject("mnuÄraVor.Image")));
            this.mnuÄraVor.Name = "mnuÄraVor";
            this.mnuÄraVor.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnuÄraVor.Size = new System.Drawing.Size(328, 26);
            this.mnuÄraVor.Text = "Ära vor";
            this.mnuÄraVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // mnuÄraZurueck
            // 
            this.mnuÄraZurueck.Image = ((System.Drawing.Image)(resources.GetObject("mnuÄraZurueck.Image")));
            this.mnuÄraZurueck.Name = "mnuÄraZurueck";
            this.mnuÄraZurueck.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.mnuÄraZurueck.Size = new System.Drawing.Size(328, 26);
            this.mnuÄraZurueck.Text = "Ära zurück";
            this.mnuÄraZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(325, 6);
            // 
            // mnuGebietVor
            // 
            this.mnuGebietVor.Image = ((System.Drawing.Image)(resources.GetObject("mnuGebietVor.Image")));
            this.mnuGebietVor.Name = "mnuGebietVor";
            this.mnuGebietVor.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuGebietVor.Size = new System.Drawing.Size(328, 26);
            this.mnuGebietVor.Text = "Gebiet vor";
            this.mnuGebietVor.Click += new System.EventHandler(this.menuNavigation);
            // 
            // mnuGebietZurueck
            // 
            this.mnuGebietZurueck.Image = ((System.Drawing.Image)(resources.GetObject("mnuGebietZurueck.Image")));
            this.mnuGebietZurueck.Name = "mnuGebietZurueck";
            this.mnuGebietZurueck.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.mnuGebietZurueck.Size = new System.Drawing.Size(328, 26);
            this.mnuGebietZurueck.Text = "Gebiet zurück";
            this.mnuGebietZurueck.Click += new System.EventHandler(this.menuNavigation);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(325, 6);
            // 
            // mnuDown
            // 
            this.mnuDown.Image = ((System.Drawing.Image)(resources.GetObject("mnuDown.Image")));
            this.mnuDown.Name = "mnuDown";
            this.mnuDown.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.mnuDown.Size = new System.Drawing.Size(328, 26);
            this.mnuDown.Text = "Nächste Sammlungsmünze";
            this.mnuDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // mnuUp
            // 
            this.mnuUp.Image = ((System.Drawing.Image)(resources.GetObject("mnuUp.Image")));
            this.mnuUp.Name = "mnuUp";
            this.mnuUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6)));
            this.mnuUp.Size = new System.Drawing.Size(328, 26);
            this.mnuUp.Text = "Vorhergehende Sammlungsmünze";
            this.mnuUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // mnuOptionen
            // 
            this.mnuOptionen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReorg,
            this.mnuDatabaseTransfer,
            this.toolStripMenuItem8,
            this.mnuSettings,
            this.toolStripSeparator13,
            this.mnuImportCB2006,
            this.mnuImport2x,
            this.mnuImport2XML,
            this.mnuImportCoinbook3,
            this.toolStripSeparator8,
            this.mnuAktivieren,
            this.toolStripMenuItem12,
            this.mnuLupe,
            this.mnuLupeSettings,
            this.toolStripMenuItem13,
            this.mnuColumnWidth});
            this.mnuOptionen.Name = "mnuOptionen";
            this.mnuOptionen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.mnuOptionen.Size = new System.Drawing.Size(70, 22);
            this.mnuOptionen.Text = "Optionen";
            // 
            // mnuReorg
            // 
            this.mnuReorg.Image = ((System.Drawing.Image)(resources.GetObject("mnuReorg.Image")));
            this.mnuReorg.Name = "mnuReorg";
            this.mnuReorg.Size = new System.Drawing.Size(303, 26);
            this.mnuReorg.Text = "Datenbank reparieren";
            this.mnuReorg.Click += new System.EventHandler(this.mnuReorg_Click);
            // 
            // mnuDatabaseTransfer
            // 
            this.mnuDatabaseTransfer.Name = "mnuDatabaseTransfer";
            this.mnuDatabaseTransfer.Size = new System.Drawing.Size(303, 26);
            this.mnuDatabaseTransfer.Text = "Datenbank zur Überprüfung schicken";
            this.mnuDatabaseTransfer.Click += new System.EventHandler(this.mnuDatabaseTransfer_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(300, 6);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Image = ((System.Drawing.Image)(resources.GetObject("mnuSettings.Image")));
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(303, 26);
            this.mnuSettings.Text = "Einstellungen";
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(300, 6);
            // 
            // mnuImportCB2006
            // 
            this.mnuImportCB2006.Image = ((System.Drawing.Image)(resources.GetObject("mnuImportCB2006.Image")));
            this.mnuImportCB2006.Name = "mnuImportCB2006";
            this.mnuImportCB2006.Size = new System.Drawing.Size(303, 26);
            this.mnuImportCB2006.Text = "Import von CB2006";
            this.mnuImportCB2006.Click += new System.EventHandler(this.mnuImport_Click);
            // 
            // mnuImport2x
            // 
            this.mnuImport2x.Image = ((System.Drawing.Image)(resources.GetObject("mnuImport2x.Image")));
            this.mnuImport2x.Name = "mnuImport2x";
            this.mnuImport2x.Size = new System.Drawing.Size(303, 26);
            this.mnuImport2x.Text = "Import von Coinbook 2.x";
            this.mnuImport2x.Click += new System.EventHandler(this.mnuImport2x_Click);
            // 
            // mnuImport2XML
            // 
            this.mnuImport2XML.Image = ((System.Drawing.Image)(resources.GetObject("mnuImport2XML.Image")));
            this.mnuImport2XML.Name = "mnuImport2XML";
            this.mnuImport2XML.Size = new System.Drawing.Size(303, 26);
            this.mnuImport2XML.Text = "Import von Coinbook 2.x aus XML-Datei";
            this.mnuImport2XML.Click += new System.EventHandler(this.mnuImport2XML_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(300, 6);
            this.toolStripSeparator8.Visible = false;
            // 
            // mnuAktivieren
            // 
            this.mnuAktivieren.Image = ((System.Drawing.Image)(resources.GetObject("mnuAktivieren.Image")));
            this.mnuAktivieren.Name = "mnuAktivieren";
            this.mnuAktivieren.Size = new System.Drawing.Size(303, 26);
            this.mnuAktivieren.Text = "Coinbook aktivieren";
            this.mnuAktivieren.Click += new System.EventHandler(this.mnuAktivieren_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(300, 6);
            // 
            // mnuLupe
            // 
            this.mnuLupe.Name = "mnuLupe";
            this.mnuLupe.Size = new System.Drawing.Size(303, 26);
            this.mnuLupe.Text = "Bildschirmlupe";
            this.mnuLupe.Click += new System.EventHandler(this.mnuLupe_Click);
            // 
            // mnuLupeSettings
            // 
            this.mnuLupeSettings.Image = ((System.Drawing.Image)(resources.GetObject("mnuLupeSettings.Image")));
            this.mnuLupeSettings.Name = "mnuLupeSettings";
            this.mnuLupeSettings.Size = new System.Drawing.Size(303, 26);
            this.mnuLupeSettings.Text = "Bildschirmlupe Einstellungen";
            this.mnuLupeSettings.Click += new System.EventHandler(this.mnuLupeSettings_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(300, 6);
            // 
            // mnuColumnWidth
            // 
            this.mnuColumnWidth.Image = ((System.Drawing.Image)(resources.GetObject("mnuColumnWidth.Image")));
            this.mnuColumnWidth.Name = "mnuColumnWidth";
            this.mnuColumnWidth.Size = new System.Drawing.Size(303, 26);
            this.mnuColumnWidth.Text = "Spaltenbreiten auf Voreinstellung";
            this.mnuColumnWidth.Click += new System.EventHandler(this.mnuColumnWidth_Click);
            // 
            // mnuWeb
            // 
            this.mnuWeb.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNews,
            this.mnuFAQ,
            this.mnuWebOrder,
            this.mnuSupport,
            this.mnuGästebuch,
            this.toolStripSeparator12,
            this.mnuOrder,
            this.mnuModulUpdates,
            this.mnuUpdate,
            this.mnuUpdateVonCD,
            this.mnuAbonnements});
            this.mnuWeb.Name = "mnuWeb";
            this.mnuWeb.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
            this.mnuWeb.Size = new System.Drawing.Size(45, 22);
            this.mnuWeb.Text = "Web";
            // 
            // mnuNews
            // 
            this.mnuNews.Image = ((System.Drawing.Image)(resources.GetObject("mnuNews.Image")));
            this.mnuNews.Name = "mnuNews";
            this.mnuNews.Size = new System.Drawing.Size(289, 26);
            this.mnuNews.Text = "Aktuelle News laden";
            this.mnuNews.Click += new System.EventHandler(this.mnuNews_Click);
            // 
            // mnuFAQ
            // 
            this.mnuFAQ.Image = ((System.Drawing.Image)(resources.GetObject("mnuFAQ.Image")));
            this.mnuFAQ.Name = "mnuFAQ";
            this.mnuFAQ.Size = new System.Drawing.Size(289, 26);
            this.mnuFAQ.Text = "FAQ";
            this.mnuFAQ.Click += new System.EventHandler(this.mnuFAQ_Click);
            // 
            // mnuWebOrder
            // 
            this.mnuWebOrder.Image = ((System.Drawing.Image)(resources.GetObject("mnuWebOrder.Image")));
            this.mnuWebOrder.Name = "mnuWebOrder";
            this.mnuWebOrder.Size = new System.Drawing.Size(289, 26);
            this.mnuWebOrder.Text = "Bestellung im Internet";
            this.mnuWebOrder.Click += new System.EventHandler(this.mnuWebOrder_Click);
            // 
            // mnuSupport
            // 
            this.mnuSupport.Image = ((System.Drawing.Image)(resources.GetObject("mnuSupport.Image")));
            this.mnuSupport.Name = "mnuSupport";
            this.mnuSupport.Size = new System.Drawing.Size(289, 26);
            this.mnuSupport.Text = "Support";
            this.mnuSupport.Click += new System.EventHandler(this.mnuSupport_Click);
            // 
            // mnuGästebuch
            // 
            this.mnuGästebuch.Image = ((System.Drawing.Image)(resources.GetObject("mnuGästebuch.Image")));
            this.mnuGästebuch.Name = "mnuGästebuch";
            this.mnuGästebuch.Size = new System.Drawing.Size(289, 26);
            this.mnuGästebuch.Text = "Gästebuch";
            this.mnuGästebuch.Click += new System.EventHandler(this.mnuGästebuch_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(286, 6);
            // 
            // mnuOrder
            // 
            this.mnuOrder.Image = ((System.Drawing.Image)(resources.GetObject("mnuOrder.Image")));
            this.mnuOrder.Name = "mnuOrder";
            this.mnuOrder.Size = new System.Drawing.Size(289, 26);
            this.mnuOrder.Text = "neue Module bestellen";
            this.mnuOrder.Click += new System.EventHandler(this.mnuOrder_Click);
            // 
            // mnuModulUpdates
            // 
            this.mnuModulUpdates.Image = ((System.Drawing.Image)(resources.GetObject("mnuModulUpdates.Image")));
            this.mnuModulUpdates.Name = "mnuModulUpdates";
            this.mnuModulUpdates.Size = new System.Drawing.Size(289, 26);
            this.mnuModulUpdates.Text = "Modul Updates einlesen";
            this.mnuModulUpdates.Click += new System.EventHandler(this.mnuModulUpdates_Click);
            // 
            // mnuUpdate
            // 
            this.mnuUpdate.Image = ((System.Drawing.Image)(resources.GetObject("mnuUpdate.Image")));
            this.mnuUpdate.Name = "mnuUpdate";
            this.mnuUpdate.Size = new System.Drawing.Size(289, 26);
            this.mnuUpdate.Text = "Programmupdate vom Internet laden";
            this.mnuUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // mnuUpdateVonCD
            // 
            this.mnuUpdateVonCD.Image = ((System.Drawing.Image)(resources.GetObject("mnuUpdateVonCD.Image")));
            this.mnuUpdateVonCD.Name = "mnuUpdateVonCD";
            this.mnuUpdateVonCD.Size = new System.Drawing.Size(289, 26);
            this.mnuUpdateVonCD.Text = "Programmupdate von CD laden";
            this.mnuUpdateVonCD.Click += new System.EventHandler(this.mnuUpdateVonCD_Click);
            // 
            // mnuAbonnements
            // 
            this.mnuAbonnements.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCloudBackupBestellen});
            this.mnuAbonnements.Name = "mnuAbonnements";
            this.mnuAbonnements.Size = new System.Drawing.Size(289, 26);
            this.mnuAbonnements.Text = "Abonnements bestellen";
            // 
            // mnuCloudBackupBestellen
            // 
            this.mnuCloudBackupBestellen.Name = "mnuCloudBackupBestellen";
            this.mnuCloudBackupBestellen.Size = new System.Drawing.Size(201, 22);
            this.mnuCloudBackupBestellen.Text = "CloudBackup bestellen";
            this.mnuCloudBackupBestellen.Click += new System.EventHandler(this.mnuCloudBackupBestellen_Click);
            // 
            // mnuExtras
            // 
            this.mnuExtras.Name = "mnuExtras";
            this.mnuExtras.Size = new System.Drawing.Size(54, 22);
            this.mnuExtras.Text = "Extras";
            // 
            // mnuBackup
            // 
            this.mnuBackup.Name = "mnuBackup";
            this.mnuBackup.Size = new System.Drawing.Size(107, 22);
            this.mnuBackup.Text = "Datensicherung";
            this.mnuBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.toolStripMenuItem11,
            this.mnuHandbuchPDF,
            this.mnuTeamviewer,
            this.mnuLogSettings});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuAbout.Image")));
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(272, 26);
            this.mnuAbout.Text = "Über das Programm";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(269, 6);
            // 
            // mnuHandbuchPDF
            // 
            this.mnuHandbuchPDF.Image = ((System.Drawing.Image)(resources.GetObject("mnuHandbuchPDF.Image")));
            this.mnuHandbuchPDF.Name = "mnuHandbuchPDF";
            this.mnuHandbuchPDF.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mnuHandbuchPDF.Size = new System.Drawing.Size(272, 26);
            this.mnuHandbuchPDF.Text = "Handbuch (PDF) anzeigen";
            this.mnuHandbuchPDF.Click += new System.EventHandler(this.mnuHandbuchPDF_Click);
            // 
            // mnuTeamviewer
            // 
            this.mnuTeamviewer.Image = ((System.Drawing.Image)(resources.GetObject("mnuTeamviewer.Image")));
            this.mnuTeamviewer.Name = "mnuTeamviewer";
            this.mnuTeamviewer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mnuTeamviewer.Size = new System.Drawing.Size(272, 26);
            this.mnuTeamviewer.Text = "Teamviewer aufrufen";
            this.mnuTeamviewer.Click += new System.EventHandler(this.mnuTeamviewer_Click);
            // 
            // mnuLogSettings
            // 
            this.mnuLogSettings.Name = "mnuLogSettings";
            this.mnuLogSettings.Size = new System.Drawing.Size(272, 26);
            this.mnuLogSettings.Text = "Log Settings";
            this.mnuLogSettings.Click += new System.EventHandler(this.mnuLogSettings_Click);
            // 
            // mnuLanguage
            // 
            this.mnuLanguage.Name = "mnuLanguage";
            this.mnuLanguage.Size = new System.Drawing.Size(66, 22);
            this.mnuLanguage.Text = "Sprache";
            // 
            // mnuCreateDatabase
            // 
            this.mnuCreateDatabase.Name = "mnuCreateDatabase";
            this.mnuCreateDatabase.Size = new System.Drawing.Size(76, 22);
            this.mnuCreateDatabase.Text = "Create DB";
            this.mnuCreateDatabase.Visible = false;
            this.mnuCreateDatabase.Click += new System.EventHandler(this.mnuCreateDatabase_Click);
            // 
            // mnuSwitchDatabase
            // 
            this.mnuSwitchDatabase.Name = "mnuSwitchDatabase";
            this.mnuSwitchDatabase.Size = new System.Drawing.Size(113, 22);
            this.mnuSwitchDatabase.Text = "Switch database";
            this.mnuSwitchDatabase.Visible = false;
            this.mnuSwitchDatabase.Click += new System.EventHandler(this.mnuSwitchDatabase_Click);
            // 
            // toolStripSeparator13_5
            // 
            this.toolStripSeparator13_5.Name = "toolStripSeparator13_5";
            this.toolStripSeparator13_5.Size = new System.Drawing.Size(391, 6);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = ".zip|*.zip";
            this.dlgOpenFile.Title = "Coinbook-Backup suchen";
            // 
            // mnuWert
            // 
            this.mnuWert.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuWert.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuWerteSammlung,
            this.cmnuWerteDoubletten});
            this.mnuWert.Name = "mnuWert";
            this.mnuWert.Size = new System.Drawing.Size(234, 56);
            this.mnuWert.Click += new System.EventHandler(this.mnuWert_Click);
            // 
            // cmnuWerteSammlung
            // 
            this.cmnuWerteSammlung.Image = ((System.Drawing.Image)(resources.GetObject("cmnuWerteSammlung.Image")));
            this.cmnuWerteSammlung.Name = "cmnuWerteSammlung";
            this.cmnuWerteSammlung.Size = new System.Drawing.Size(233, 26);
            this.cmnuWerteSammlung.Text = "Werte berechnen Sammlung";
            this.cmnuWerteSammlung.Click += new System.EventHandler(this.mnuWerteSammlung_Click);
            // 
            // cmnuWerteDoubletten
            // 
            this.cmnuWerteDoubletten.Image = ((System.Drawing.Image)(resources.GetObject("cmnuWerteDoubletten.Image")));
            this.cmnuWerteDoubletten.Name = "cmnuWerteDoubletten";
            this.cmnuWerteDoubletten.Size = new System.Drawing.Size(233, 26);
            this.cmnuWerteDoubletten.Text = "Werte berechnen  Doubletten";
            this.cmnuWerteDoubletten.Click += new System.EventHandler(this.mnuWerteDoublette_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Controls.Add(this.splashPanel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblStatusleiste, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblAnzeige, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblRecords, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.pgbModul, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pgbDetails, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.ctlMain, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1313, 572);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // splashPanel1
            // 
            this.splashPanel1.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Cyan);
            this.splashPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splashPanel1.BeforeTouchSize = new System.Drawing.Size(68, 14);
            this.splashPanel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.splashPanel1.CloseOnClick = true;
            this.splashPanel1.Controls.Add(this.labelEx1);
            this.splashPanel1.DesktopAlignment = Syncfusion.Windows.Forms.Tools.SplashAlignment.SystemTray;
            this.splashPanel1.DiscreetLocation = new System.Drawing.Point(0, 0);
            this.splashPanel1.Location = new System.Drawing.Point(3, 555);
            this.splashPanel1.Name = "splashPanel1";
            this.splashPanel1.ShowAnimation = false;
            this.splashPanel1.Size = new System.Drawing.Size(68, 14);
            this.splashPanel1.SlideStyle = Syncfusion.Windows.Forms.Tools.SlideStyle.BottomToTop;
            this.splashPanel1.SuspendAutoCloseWhenMouseOver = true;
            this.splashPanel1.TabIndex = 24;
            this.splashPanel1.Text = "splashPanel1";
            this.splashPanel1.TimerInterval = 10000;
            this.splashPanel1.Visible = false;
            // 
            // labelEx1
            // 
            this.labelEx1.BackColor = System.Drawing.Color.DarkRed;
            this.labelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx1.Location = new System.Drawing.Point(0, 0);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(36, 16);
            this.labelEx1.TabIndex = 0;
            // 
            // lblStatusleiste
            // 
            this.lblStatusleiste.BackColor = System.Drawing.SystemColors.Info;
            this.lblStatusleiste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusleiste.Location = new System.Drawing.Point(3, 532);
            this.lblStatusleiste.Name = "lblStatusleiste";
            this.lblStatusleiste.Size = new System.Drawing.Size(647, 20);
            this.lblStatusleiste.TabIndex = 16;
            this.lblStatusleiste.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAnzeige
            // 
            this.lblAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnzeige.Location = new System.Drawing.Point(1138, 532);
            this.lblAnzeige.Name = "lblAnzeige";
            this.lblAnzeige.Size = new System.Drawing.Size(130, 20);
            this.lblAnzeige.TabIndex = 17;
            this.lblAnzeige.Text = "Anzeige";
            this.lblAnzeige.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRecords
            // 
            this.lblRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(1274, 532);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(36, 20);
            this.lblRecords.TabIndex = 18;
            this.lblRecords.Text = "0/0";
            this.lblRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbModul
            // 
            this.pgbModul.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pgbModul.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.pgbModul.BackSegments = false;
            this.pgbModul.CustomText = "";
            this.pgbModul.CustomWaitingRender = false;
            this.pgbModul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgbModul.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgbModul.FontColor = System.Drawing.Color.Black;
            this.pgbModul.ForeColor = System.Drawing.Color.SpringGreen;
            this.pgbModul.ForegroundImage = null;
            this.pgbModul.ForeSegments = false;
            this.pgbModul.GradientEndColor = System.Drawing.Color.Green;
            this.pgbModul.GradientStartColor = System.Drawing.Color.GreenYellow;
            this.pgbModul.Location = new System.Drawing.Point(653, 532);
            this.pgbModul.Margin = new System.Windows.Forms.Padding(0);
            this.pgbModul.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.pgbModul.Name = "pgbModul";
            this.pgbModul.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.Gradient;
            this.pgbModul.SegmentWidth = 1;
            this.pgbModul.ShowProgressImage = true;
            this.pgbModul.Size = new System.Drawing.Size(330, 20);
            this.pgbModul.Step = 1;
            this.pgbModul.TabIndex = 19;
            this.pgbModul.TabStop = false;
            this.pgbModul.TextShadow = false;
            this.pgbModul.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
            this.pgbModul.ThemeName = "Gradient";
            this.pgbModul.Visible = false;
            this.pgbModul.WaitingGradientWidth = 400;
            // 
            // pgbDetails
            // 
            this.pgbDetails.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.pgbDetails.BackSegments = false;
            this.tableLayoutPanel1.SetColumnSpan(this.pgbDetails, 2);
            this.pgbDetails.CustomText = null;
            this.pgbDetails.CustomWaitingRender = false;
            this.pgbDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgbDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgbDetails.FontColor = System.Drawing.Color.Black;
            this.pgbDetails.ForeColor = System.Drawing.Color.Cyan;
            this.pgbDetails.ForegroundImage = null;
            this.pgbDetails.ForeSegments = false;
            this.pgbDetails.GradientEndColor = System.Drawing.Color.RoyalBlue;
            this.pgbDetails.GradientStartColor = System.Drawing.Color.Cyan;
            this.pgbDetails.Location = new System.Drawing.Point(983, 532);
            this.pgbDetails.Margin = new System.Windows.Forms.Padding(0);
            this.pgbDetails.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.pgbDetails.Name = "pgbDetails";
            this.pgbDetails.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.Gradient;
            this.pgbDetails.SegmentWidth = 1;
            this.pgbDetails.ShowProgressImage = true;
            this.pgbDetails.Size = new System.Drawing.Size(152, 20);
            this.pgbDetails.Step = 1;
            this.pgbDetails.TabIndex = 20;
            this.pgbDetails.TabStop = false;
            this.pgbDetails.TextShadow = false;
            this.pgbDetails.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
            this.pgbDetails.ThemeName = "Gradient";
            this.pgbDetails.Visible = false;
            this.pgbDetails.WaitingGradientWidth = 400;
            // 
            // ctlMain
            // 
            this.ctlMain.AeraID = 0;
            this.tableLayoutPanel1.SetColumnSpan(this.ctlMain, 6);
            this.ctlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlMain.Location = new System.Drawing.Point(3, 33);
            this.ctlMain.Name = "ctlMain";
            this.ctlMain.NationID = 0;
            this.ctlMain.RegionID = 0;
            this.ctlMain.Size = new System.Drawing.Size(1307, 496);
            this.ctlMain.TabIndex = 25;
            this.ctlMain.OverviewLoaded += new System.EventHandler(this.ctlMain_OverviewLoaded);
            this.ctlMain.EnableEditMenues += new System.EventHandler(this.EnableEditMenues);
            this.ctlMain.AddMünze += new System.EventHandler(this.mnuMünzeAdd_Click);
            this.ctlMain.MünzeLöschen += new System.EventHandler(this.mnuDeleteCoin_Click);
            this.ctlMain.OwnCatalog += new System.EventHandler(this.mnuEigeneKatalognummern_Click);
            this.ctlMain.OwnPicture += new System.EventHandler(this.mnuPicture_Click);
            this.ctlMain.EigenePreise += new System.EventHandler(this.mnuPreise_Click);
            this.ctlMain.Auktionen += new System.EventHandler(this.mnuAuktionen_Click);
            this.ctlMain.Prägeanstalten += new System.EventHandler(this.mnuPrägeanstalten_Click);
            this.ctlMain.CoinNext += new System.EventHandler(this.btnUp_Click);
            this.ctlMain.CoinPrevious += new System.EventHandler(this.btnDown_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1313, 596);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mainmen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(957, 477);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Münzdetails";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mnuPrint.ResumeLayout(false);
            this.cmnuStrip.ResumeLayout(false);
            this.mainmen.ResumeLayout(false);
            this.mainmen.PerformLayout();
            this.mnuWert.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splashPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pgbModul)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgbDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private MenuStrip mainmen;
		private ToolStripMenuItem mnuDatei;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem mnuReportingSammlung;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem mnuEnde;
		private ToolStripMenuItem mnuEdit;
		private ToolStripMenuItem mnuMünzdetails;
		private ToolStripMenuItem mnuMünzeAdd;
		private DataGridViewImageColumn dataGridViewImageColumn1;
		private ToolStripMenuItem mnuOptionen;
		private ToolStripMenuItem mnuSettings;
		private ToolStripMenuItem mnuImportCB2006;
		private ToolStripMenuItem mnuNavigation;
		private ToolStripMenuItem mnuWerteDoublette;
		private ToolStripMenuItem mnuNationVor;
		private ToolStripMenuItem mnuNationZurueck;
		private ToolStripMenuItem mnuÄraVor;
		private ToolStripMenuItem mnuÄraZurueck;
		private ToolStripMenuItem mnuGebietVor;
		private ToolStripMenuItem mnuGebietZurueck;
		private ToolStrip toolStrip1;
		private ToolStripButton btnNationZurueck;
		private ToolStripButton btnNationVor;
		private ToolStripButton btnÄraZurueck;
		private ToolStripButton btnÄraVor;
		private ToolStripButton btnGebietZurueck;
		private ToolStripButton btnGebietVor;
		private ToolStripButton btnWaehrungVor;
		private ToolStripButton btnWaehrungZurueck;
		private ToolStripButton btnMuenzwertZurueck;
		private ToolStripButton btnMuenzwertVor;
		private ToolStripButton btnJahrZurueck;
		private ToolStripButton btnJahrVor;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripButton btnWert;
		private ToolStripButton btnPrint1;
		private ToolStripSeparator toolStripSeparator5;
		private OpenFileDialog dlgOpenFile;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripMenuItem mnuPrägeanstalten;
		private ToolStripButton btnSettings;
		private ToolStripMenuItem mnuEigeneKatalognummern;
		private ContextMenuStrip cmnuStrip;
		private ToolStripMenuItem cmnuNationVor;
		private ToolStripMenuItem cmnuNationZurück;
		private ToolStripMenuItem cmnuÄraVor;
		private ToolStripMenuItem cmnuÄraZurück;
		private ToolStripMenuItem cmnuGebietZurück;
		private ToolStripSeparator toolStripSeparator7;
		private ToolStripMenuItem cmnuMünzdetails;
		private ToolStripMenuItem cmnuMünzeAdd;
		private ToolStripMenuItem cmnuPrägeanstalten;
		private ToolStripMenuItem cmnuEigeneKatalognummern;
		private ToolStripMenuItem cmnuGebietVor;
		private ToolStripMenuItem mnuWeb;
		private ToolStripMenuItem mnuNews;
		private ToolStripMenuItem mnuOrder;
    private ToolStripMenuItem mnuFAQ;
		private ToolStripSeparator toolStripSeparator8;
		private ToolStripSeparator toolStripSeparator10;
		private ToolStripSeparator toolStripSeparator11;
		private ToolStripSeparator toolStripSeparator12;
		private ToolStripSeparator toolStripSeparator13;
		private ToolStripSeparator toolStripSeparator13_5;
		
		private ToolStripMenuItem mnuAktivieren;
		private ToolStripSeparator toolStripSeparator14;
		private ToolStripLabel lblNation2;
		private ToolStripLabel lblÄra2;
		private ToolStripLabel lblGebiet2;
		private ToolStripLabel lblWährung2;
		private ToolStripLabel lblNominale2;
		private ToolStripLabel lblJahr2;
		private ToolStripSeparator toolStripSeparator15;

		#endregion

    private ToolStripMenuItem mnuModule;
		private ToolStripMenuItem mnuDBStatus;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton btnClose;
		private ToolStripSeparator toolStripSeparator9;
		private ToolStripMenuItem mnuReportingDoubletten;
		private ToolStripMenuItem mnuReportingFehllisten;
		private ToolStripSeparator toolStripMenuItem5;
		private ToolStripMenuItem mnuWerteSammlung;
		private ContextMenuStrip mnuPrint;
		private ToolStripMenuItem mnuPrintSammlung;
		private ToolStripMenuItem mnuPrintDoubletten;
		private ToolStripMenuItem mnuPrintFehllisten;
		private ContextMenuStrip mnuWert;
		private ToolStripMenuItem cmnuWerteSammlung;
		private ToolStripMenuItem cmnuWerteDoubletten;
		private FolderBrowserDialog dlgBackup;
		private ToolStripButton btnBackup;
		private ToolStripSeparator toolStripMenuItem8;
		private ToolStripButton btnUpdate;
		private ToolStripMenuItem mnuPicture;
		private ToolStripMenuItem mnuPreise;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem cmnuPicture;
		private ToolStripMenuItem cmnuPreise;
		private ToolStripSeparator toolStripMenuItem2;
		private ToolStripButton btnKurse;
		private ToolStripSeparator toolStripMenuItem3;
		private ToolStripMenuItem mnuKurse;
		private ToolStripSeparator toolStripSeparator16;
		private ToolStripSeparator toolStripSeparator17;
		private ToolStripSeparator toolStripSeparator18;
		private ToolStripSeparator toolStripSeparator19;
		private ToolStripButton btnDown;
		private ToolStripButton btnUp;
		private ToolStripSeparator toolStripSeparator21;
		private ToolStripSeparator toolStripSeparator20;
		private ToolStripButton btnPrägeanstalt;
		private ToolStripSeparator toolStripMenuItem6;
		private ToolStripSeparator toolStripMenuItem7;
		private ToolStripMenuItem cmnuUp;
		private ToolStripMenuItem cmnuDown;
		private ToolStripSeparator toolStripMenuItem9;
		private ToolStripSeparator toolStripMenuItem4;
		private ToolStripMenuItem mnuDown;
		private ToolStripMenuItem mnuUp;
		private ToolStripMenuItem mnuReorg;
		private ToolStripMenuItem mnuKostenSammlung;
    private ToolStripMenuItem mnuKostenDoubletten;
    private System.ComponentModel.BackgroundWorker bgwUpdate;
    private ToolStripMenuItem mnuModulUpdates;
    private ToolStripSeparator toolStripMenuItem10;
    private ToolStripMenuItem mnuUpdate;
    private ToolStripMenuItem mnuImport2x;
    private ToolStripMenuItem mnuImport2XML;
		private ToolStripSeparator toolStripSeparator22;
    private ToolStripMenuItem hilfeToolStripMenuItem;
    private ToolStripMenuItem mnuAbout;
    private ToolStripSeparator toolStripMenuItem11;
    private ToolStripMenuItem mnuHandbuchPDF;
    private ToolStripMenuItem mnuUpdateVonCD;
		private ToolStripSeparator toolStripMenuItem12;
		private ToolStripMenuItem mnuLupe;
		private ToolStripMenuItem mnuLupeSettings;
		private ToolStripButton btnMagnifier;
		private ToolStripMenuItem mnuTeamviewer;
		private ToolStripButton btnTeamviewer;
		private ToolStripMenuItem mnuWebOrder;
		private ToolStripMenuItem mnuSupport;
		private ToolStripMenuItem mnuGästebuch;
        private TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblStatusleiste;
		private ToolStripMenuItem mnuCreateDatabase;
        private ToolStripMenuItem mnuSwitchDatabase;
        private ToolStripMenuItem mnuReporting2Sammlung;
        private ToolStripMenuItem mnuReporting2Doubletten;
        private ToolStripMenuItem mnuExtras;
        private ToolStripSeparator toolStripSeparator23;
        private ToolStripSeparator toolStripMenuItem13;
        private ToolStripMenuItem mnuColumnWidth;
        private ToolStripMenuItem mnuDeleteCoin;
        private ToolStripMenuItem cmenuDeleteCoin;
        private ToolStripMenuItem mnuAuktionen;
        private ToolStripMenuItem cmnuAuktionen;
        private ToolStripSeparator toolStripMenuItem14;
        private ToolStripMenuItem mnuPrintReporting2Sammlung;
        private ToolStripMenuItem mnuPrintReporting2Doubletten;
        private ToolStripSeparator toolStripMenuItem15;
        private ToolStripSeparator toolStripMenuItem16;
        private ToolStripSeparator toolStripMenuItem17;
        private ToolStripMenuItem mnuLanguage;
        private System.Windows.Forms.Label lblAnzeige;
        private System.Windows.Forms.Label lblRecords;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv pgbModul;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv pgbDetails;
        private Syncfusion.Windows.Forms.Tools.SplashPanel splashPanel1;
        private System.Windows.Forms.Label labelEx1;
        private usrMain ctlMain;
        private ToolStripMenuItem mnuLogSettings;
        private ToolStripMenuItem mnuAbonnements;
        private ToolStripMenuItem mnuCloudBackupBestellen;
        private ToolStripMenuItem mnuImportCoinbook3;
        private ToolStripMenuItem mnuDatabaseTransfer;
        private ToolStripMenuItem mnuQuickEditSammlung;
        private ToolStripMenuItem mnuBackup;
        private ToolStripMenuItem mnuQuickEditDubletten;
    }
}