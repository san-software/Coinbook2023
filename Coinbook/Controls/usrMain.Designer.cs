using System;
using System.Windows.Forms;

namespace Coinbook
{
    partial class usrMain
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.splashPanel1 = new Syncfusion.Windows.Forms.Tools.SplashPanel();
            this.labelEx1 = new System.Windows.Forms.Label();
            this.lblNation = new System.Windows.Forms.Label();
            this.picEigen = new SAN.Control.PictureBoxEx();
            this.lblWährung = new System.Windows.Forms.Label();
            this.lblGebiet = new System.Windows.Forms.Label();
            this.cboJahr = new SAN.Control.ComboBoxEx();
            this.cboGebiete = new System.Windows.Forms.ComboBox();
            this.lblJahr = new System.Windows.Forms.Label();
            this.cboNominale = new SAN.Control.ComboBoxEx();
            this.cboWährung = new SAN.Control.ComboBoxEx();
            this.lblÄra = new System.Windows.Forms.Label();
            this.cboÄra = new System.Windows.Forms.ComboBox();
            this.lblNominale = new System.Windows.Forms.Label();
            this.btnNavigate = new System.Windows.Forms.Button();
            this.picMünze = new SAN.Control.PictureBoxEx();
            this.cboNationen = new System.Windows.Forms.ComboBox();
            this.lblBestandAnzeige = new System.Windows.Forms.Label();
            this.lblAnzeigeTyp = new System.Windows.Forms.Label();
            this.grdMain1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.cmnuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuNationVor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNationZurück = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuÄraVor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuÄraZurück = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGebietVor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGebietZurück = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panCoins = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.splashPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEigen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMünze)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain1)).BeginInit();
            this.cmnuStrip.SuspendLayout();
            this.panCoins.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdMain1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.09977F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.90023F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1326, 558);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 6);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1306, 90);
            this.panel1.TabIndex = 13;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.Controls.Add(this.splashPanel1, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblNation, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.picEigen, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblWährung, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblGebiet, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboJahr, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.cboGebiete, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblJahr, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.cboNominale, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.cboWährung, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblÄra, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboÄra, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblNominale, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnNavigate, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.picMünze, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboNationen, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblBestandAnzeige, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblAnzeigeTyp, 3, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1306, 90);
            this.tableLayoutPanel2.TabIndex = 16;
            // 
            // splashPanel1
            // 
            this.splashPanel1.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.Color.DodgerBlue, System.Drawing.Color.PowderBlue);
            this.splashPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splashPanel1.BeforeTouchSize = new System.Drawing.Size(68, 16);
            this.splashPanel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.splashPanel1.CloseOnClick = true;
            this.splashPanel1.Controls.Add(this.labelEx1);
            this.splashPanel1.DesktopAlignment = Syncfusion.Windows.Forms.Tools.SplashAlignment.SystemTray;
            this.splashPanel1.DiscreetLocation = new System.Drawing.Point(0, 0);
            this.splashPanel1.Location = new System.Drawing.Point(1221, 3);
            this.splashPanel1.Name = "splashPanel1";
            this.splashPanel1.ShowAnimation = false;
            this.splashPanel1.Size = new System.Drawing.Size(68, 16);
            this.splashPanel1.SlideStyle = Syncfusion.Windows.Forms.Tools.SlideStyle.BottomToTop;
            this.splashPanel1.SuspendAutoCloseWhenMouseOver = true;
            this.splashPanel1.TabIndex = 23;
            this.splashPanel1.Text = "splashPanel1";
            this.splashPanel1.TimerInterval = 10000;
            this.splashPanel1.Visible = false;
            // 
            // labelEx1
            // 
            this.labelEx1.BackColor = System.Drawing.Color.Transparent;
            this.labelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEx1.Location = new System.Drawing.Point(0, 0);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(68, 16);
            this.labelEx1.TabIndex = 0;
            // 
            // lblNation
            // 
            this.lblNation.AutoSize = true;
            this.lblNation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNation.Location = new System.Drawing.Point(3, 0);
            this.lblNation.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblNation.Name = "lblNation";
            this.lblNation.Size = new System.Drawing.Size(154, 19);
            this.lblNation.TabIndex = 1;
            this.lblNation.Text = "Nation";
            this.lblNation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // picEigen
            // 
            this.picEigen.BackColor = System.Drawing.Color.Transparent;
            this.picEigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picEigen.Location = new System.Drawing.Point(1018, 3);
            this.picEigen.Name = "picEigen";
            this.picEigen.PictureName = null;
            this.tableLayoutPanel2.SetRowSpan(this.picEigen, 4);
            this.picEigen.Size = new System.Drawing.Size(197, 84);
            this.picEigen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEigen.TabIndex = 15;
            this.picEigen.TabStop = false;
            this.picEigen.Click += new System.EventHandler(this.picMünze_Click);
            // 
            // lblWährung
            // 
            this.lblWährung.AutoSize = true;
            this.lblWährung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWährung.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWährung.Location = new System.Drawing.Point(3, 44);
            this.lblWährung.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblWährung.Name = "lblWährung";
            this.lblWährung.Size = new System.Drawing.Size(154, 19);
            this.lblWährung.TabIndex = 9;
            this.lblWährung.Text = "Währung";
            this.lblWährung.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblGebiet
            // 
            this.lblGebiet.AutoSize = true;
            this.lblGebiet.BackColor = System.Drawing.Color.Transparent;
            this.lblGebiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGebiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGebiet.Location = new System.Drawing.Point(363, 0);
            this.lblGebiet.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblGebiet.Name = "lblGebiet";
            this.lblGebiet.Size = new System.Drawing.Size(84, 19);
            this.lblGebiet.TabIndex = 5;
            this.lblGebiet.Text = "Gebiet";
            this.lblGebiet.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboJahr
            // 
            this.cboJahr.BackColor = System.Drawing.Color.White;
            this.cboJahr.Column = 0;
            this.cboJahr.ColumnsToDisplay = "";
            this.cboJahr.ColumnType = SAN.Control.ComboBoxEx.ColType.SingleColumn;
            this.cboJahr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboJahr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJahr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboJahr.ForeColor = System.Drawing.Color.Black;
            this.cboJahr.FormattingEnabled = true;
            this.cboJahr.GridLinesMultiColumn = false;
            this.cboJahr.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboJahr.ID = ((long)(-1));
            this.cboJahr.IDObject = -1;
            this.cboJahr.IDString = "";
            this.cboJahr.IsPflichtfeld = false;
            this.cboJahr.Location = new System.Drawing.Point(363, 66);
            this.cboJahr.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.cboJahr.MaxDropDownItems = 10;
            this.cboJahr.Name = "cboJahr";
            this.cboJahr.ReadOnly = false;
            this.cboJahr.Row = 0;
            this.cboJahr.ShowClipBoard = true;
            this.cboJahr.Size = new System.Drawing.Size(87, 21);
            this.cboJahr.TabIndex = 10;
            this.cboJahr.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboJahr.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // cboGebiete
            // 
            this.cboGebiete.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.SetColumnSpan(this.cboGebiete, 2);
            this.cboGebiete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGebiete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGebiete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboGebiete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboGebiete.FormattingEnabled = true;
            this.cboGebiete.Location = new System.Drawing.Point(363, 22);
            this.cboGebiete.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboGebiete.MaxDropDownItems = 10;
            this.cboGebiete.Name = "cboGebiete";
            this.cboGebiete.Size = new System.Drawing.Size(317, 21);
            this.cboGebiete.TabIndex = 4;
            this.cboGebiete.SelectedValueChanged += new System.EventHandler(this.cboGebiete_SelectedValueChanged);
            // 
            // lblJahr
            // 
            this.lblJahr.AutoSize = true;
            this.lblJahr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJahr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJahr.Location = new System.Drawing.Point(363, 44);
            this.lblJahr.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblJahr.Name = "lblJahr";
            this.lblJahr.Size = new System.Drawing.Size(84, 19);
            this.lblJahr.TabIndex = 11;
            this.lblJahr.Text = "Jahr";
            this.lblJahr.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboNominale
            // 
            this.cboNominale.BackColor = System.Drawing.Color.White;
            this.cboNominale.Column = 0;
            this.cboNominale.ColumnsToDisplay = "";
            this.cboNominale.ColumnType = SAN.Control.ComboBoxEx.ColType.SingleColumn;
            this.cboNominale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNominale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNominale.ForeColor = System.Drawing.Color.Black;
            this.cboNominale.FormattingEnabled = true;
            this.cboNominale.GridLinesMultiColumn = false;
            this.cboNominale.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboNominale.ID = ((long)(-1));
            this.cboNominale.IDObject = -1;
            this.cboNominale.IDString = "";
            this.cboNominale.IsPflichtfeld = false;
            this.cboNominale.Location = new System.Drawing.Point(163, 66);
            this.cboNominale.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboNominale.MaxDropDownItems = 10;
            this.cboNominale.Name = "cboNominale";
            this.cboNominale.ReadOnly = false;
            this.cboNominale.Row = 0;
            this.cboNominale.ShowClipBoard = true;
            this.cboNominale.Size = new System.Drawing.Size(194, 21);
            this.cboNominale.TabIndex = 6;
            this.cboNominale.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboNominale.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // cboWährung
            // 
            this.cboWährung.BackColor = System.Drawing.Color.White;
            this.cboWährung.Column = 0;
            this.cboWährung.ColumnsToDisplay = "";
            this.cboWährung.ColumnType = SAN.Control.ComboBoxEx.ColType.SingleColumn;
            this.cboWährung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWährung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboWährung.ForeColor = System.Drawing.Color.Black;
            this.cboWährung.FormattingEnabled = true;
            this.cboWährung.GridLinesMultiColumn = false;
            this.cboWährung.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cboWährung.ID = ((long)(-1));
            this.cboWährung.IDObject = -1;
            this.cboWährung.IDString = "";
            this.cboWährung.IsPflichtfeld = false;
            this.cboWährung.Location = new System.Drawing.Point(3, 66);
            this.cboWährung.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboWährung.MaxDropDownItems = 10;
            this.cboWährung.Name = "cboWährung";
            this.cboWährung.ReadOnly = false;
            this.cboWährung.Row = 0;
            this.cboWährung.ShowClipBoard = true;
            this.cboWährung.Size = new System.Drawing.Size(154, 21);
            this.cboWährung.TabIndex = 8;
            this.cboWährung.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboWährung.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // lblÄra
            // 
            this.lblÄra.AutoSize = true;
            this.lblÄra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblÄra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblÄra.Location = new System.Drawing.Point(163, 0);
            this.lblÄra.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblÄra.Name = "lblÄra";
            this.lblÄra.Size = new System.Drawing.Size(194, 19);
            this.lblÄra.TabIndex = 3;
            this.lblÄra.Text = "Ära";
            this.lblÄra.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboÄra
            // 
            this.cboÄra.BackColor = System.Drawing.Color.White;
            this.cboÄra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboÄra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboÄra.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboÄra.FormattingEnabled = true;
            this.cboÄra.Location = new System.Drawing.Point(163, 22);
            this.cboÄra.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboÄra.MaxDropDownItems = 10;
            this.cboÄra.Name = "cboÄra";
            this.cboÄra.Size = new System.Drawing.Size(194, 21);
            this.cboÄra.TabIndex = 2;
            this.cboÄra.SelectedValueChanged += new System.EventHandler(this.cboÄra_SelectedValueChanged);
            // 
            // lblNominale
            // 
            this.lblNominale.AutoSize = true;
            this.lblNominale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNominale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNominale.Location = new System.Drawing.Point(163, 44);
            this.lblNominale.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblNominale.Name = "lblNominale";
            this.lblNominale.Size = new System.Drawing.Size(194, 19);
            this.lblNominale.TabIndex = 7;
            this.lblNominale.Text = "Nominale";
            this.lblNominale.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnNavigate
            // 
            this.btnNavigate.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNavigate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNavigate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavigate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNavigate.Image = ((System.Drawing.Image)(resources.GetObject("btnNavigate.Image")));
            this.btnNavigate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavigate.Location = new System.Drawing.Point(686, 3);
            this.btnNavigate.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnNavigate.Name = "btnNavigate";
            this.tableLayoutPanel2.SetRowSpan(this.btnNavigate, 2);
            this.btnNavigate.Size = new System.Drawing.Size(123, 41);
            this.btnNavigate.TabIndex = 13;
            this.btnNavigate.Text = "Navigieren ";
            this.btnNavigate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNavigate.UseVisualStyleBackColor = false;
            this.btnNavigate.Click += new System.EventHandler(this.btnNavigate_Click);
            // 
            // picMünze
            // 
            this.picMünze.BackColor = System.Drawing.Color.Transparent;
            this.picMünze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMünze.Location = new System.Drawing.Point(815, 3);
            this.picMünze.Name = "picMünze";
            this.picMünze.PictureName = null;
            this.tableLayoutPanel2.SetRowSpan(this.picMünze, 4);
            this.picMünze.Size = new System.Drawing.Size(197, 84);
            this.picMünze.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMünze.TabIndex = 0;
            this.picMünze.TabStop = false;
            this.picMünze.Click += new System.EventHandler(this.picMünze_Click);
            this.picMünze.MouseEnter += new System.EventHandler(this.picMünze_MouseEnter);
            this.picMünze.MouseLeave += new System.EventHandler(this.picMünze_MouseLeave);
            // 
            // cboNationen
            // 
            this.cboNationen.BackColor = System.Drawing.Color.White;
            this.cboNationen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNationen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNationen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboNationen.FormattingEnabled = true;
            this.cboNationen.Location = new System.Drawing.Point(3, 22);
            this.cboNationen.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboNationen.MaxDropDownItems = 10;
            this.cboNationen.Name = "cboNationen";
            this.cboNationen.Size = new System.Drawing.Size(154, 21);
            this.cboNationen.TabIndex = 16;
            this.cboNationen.SelectedValueChanged += new System.EventHandler(this.cboNationen_SelectedValueChanged);
            // 
            // lblBestandAnzeige
            // 
            this.lblBestandAnzeige.AutoSize = true;
            this.lblBestandAnzeige.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBestandAnzeige.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBestandAnzeige.ForeColor = System.Drawing.Color.Red;
            this.lblBestandAnzeige.Location = new System.Drawing.Point(453, 66);
            this.lblBestandAnzeige.Name = "lblBestandAnzeige";
            this.lblBestandAnzeige.Size = new System.Drawing.Size(227, 24);
            this.lblBestandAnzeige.TabIndex = 17;
            this.lblBestandAnzeige.Text = "label1";
            this.lblBestandAnzeige.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblAnzeigeTyp
            // 
            this.lblAnzeigeTyp.AutoSize = true;
            this.lblAnzeigeTyp.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblAnzeigeTyp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnzeigeTyp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnzeigeTyp.Location = new System.Drawing.Point(453, 44);
            this.lblAnzeigeTyp.Name = "lblAnzeigeTyp";
            this.lblAnzeigeTyp.Size = new System.Drawing.Size(227, 22);
            this.lblAnzeigeTyp.TabIndex = 18;
            this.lblAnzeigeTyp.Text = "Bestandsanzeige-Typ:";
            this.lblAnzeigeTyp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // grdMain1
            // 
            this.grdMain1.AccessibleName = "Table";
            this.grdMain1.AllowEditing = false;
            this.grdMain1.AllowSorting = false;
            this.tableLayoutPanel1.SetColumnSpan(this.grdMain1, 6);
            this.grdMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain1.EditorSelectionBehavior = Syncfusion.WinForms.DataGrid.Enums.EditorSelectionBehavior.SelectAll;
            this.grdMain1.Location = new System.Drawing.Point(3, 93);
            this.grdMain1.Name = "grdMain1";
            this.grdMain1.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.tableLayoutPanel1.SetRowSpan(this.grdMain1, 2);
            this.grdMain1.ShowBusyIndicator = true;
            this.grdMain1.Size = new System.Drawing.Size(1300, 462);
            this.grdMain1.TabIndex = 21;
            this.grdMain1.Text = "sfDataGrid1";
            this.grdMain1.ToolTipOpening += new Syncfusion.WinForms.DataGrid.Events.ToolTipOpeningEventHandler(this.grdMain1_ToolTipOpening);
            this.grdMain1.QueryCellStyle += new Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventHandler(this.grdMain1_QueryCellStyle);
            this.grdMain1.DrawCell += new Syncfusion.WinForms.DataGrid.Events.DrawCellEventHandler(this.grdMain1_DrawCell);
            this.grdMain1.SelectionChanged += new Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventHandler(this.grdMain1_SelectionChanged);
            this.grdMain1.QueryRowHeight += new Syncfusion.WinForms.DataGrid.Events.QueryRowHeightEventHandler(this.grdMain1_QueryRowHeight);
            this.grdMain1.CellClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.grdMain1_CellClick);
            this.grdMain1.CurrentCellActivating += new Syncfusion.WinForms.DataGrid.Events.CurrentCellActivatingEventHandler(this.grdMain1_CurrentCellActivating);
            this.grdMain1.ColumnResizing += new Syncfusion.WinForms.DataGrid.Events.ColumnResizingEventHandler(this.grdMain1_ColumnResizing);
            // 
            // cmnuStrip
            // 
            this.cmnuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNationVor,
            this.mnuNationZurück,
            this.toolStripMenuItem6,
            this.mnuÄraVor,
            this.mnuÄraZurück,
            this.toolStripMenuItem7,
            this.mnuGebietVor,
            this.mnuGebietZurück,
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
            // mnuNationVor
            // 
            this.mnuNationVor.Image = ((System.Drawing.Image)(resources.GetObject("mnuNationVor.Image")));
            this.mnuNationVor.Name = "mnuNationVor";
            this.mnuNationVor.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuNationVor.Size = new System.Drawing.Size(310, 26);
            this.mnuNationVor.Text = "Nation vor";
            this.mnuNationVor.Click += new System.EventHandler(this.MenuNavigation);
            // 
            // mnuNationZurück
            // 
            this.mnuNationZurück.Image = ((System.Drawing.Image)(resources.GetObject("mnuNationZurück.Image")));
            this.mnuNationZurück.Name = "mnuNationZurück";
            this.mnuNationZurück.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
            this.mnuNationZurück.Size = new System.Drawing.Size(310, 26);
            this.mnuNationZurück.Text = "Nation zurück";
            this.mnuNationZurück.Click += new System.EventHandler(this.MenuNavigation);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(307, 6);
            // 
            // mnuÄraVor
            // 
            this.mnuÄraVor.Image = ((System.Drawing.Image)(resources.GetObject("mnuÄraVor.Image")));
            this.mnuÄraVor.Name = "mnuÄraVor";
            this.mnuÄraVor.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnuÄraVor.Size = new System.Drawing.Size(310, 26);
            this.mnuÄraVor.Text = "Aera vor";
            this.mnuÄraVor.Click += new System.EventHandler(this.MenuNavigation);
            // 
            // mnuÄraZurück
            // 
            this.mnuÄraZurück.Image = ((System.Drawing.Image)(resources.GetObject("mnuÄraZurück.Image")));
            this.mnuÄraZurück.Name = "mnuÄraZurück";
            this.mnuÄraZurück.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.mnuÄraZurück.Size = new System.Drawing.Size(310, 26);
            this.mnuÄraZurück.Text = "Aera zurück";
            this.mnuÄraZurück.Click += new System.EventHandler(this.MenuNavigation);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(307, 6);
            // 
            // mnuGebietVor
            // 
            this.mnuGebietVor.Image = ((System.Drawing.Image)(resources.GetObject("mnuGebietVor.Image")));
            this.mnuGebietVor.Name = "mnuGebietVor";
            this.mnuGebietVor.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuGebietVor.Size = new System.Drawing.Size(310, 26);
            this.mnuGebietVor.Text = "Gebiet vor";
            this.mnuGebietVor.Click += new System.EventHandler(this.MenuNavigation);
            // 
            // mnuGebietZurück
            // 
            this.mnuGebietZurück.Image = ((System.Drawing.Image)(resources.GetObject("mnuGebietZurück.Image")));
            this.mnuGebietZurück.Name = "mnuGebietZurück";
            this.mnuGebietZurück.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.mnuGebietZurück.Size = new System.Drawing.Size(310, 26);
            this.mnuGebietZurück.Text = "Gebiet zurück";
            this.mnuGebietZurück.Click += new System.EventHandler(this.MenuNavigation);
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
            this.cmnuUp.Click += new System.EventHandler(this.cmnuUp_Click);
            // 
            // cmnuDown
            // 
            this.cmnuDown.Image = ((System.Drawing.Image)(resources.GetObject("cmnuDown.Image")));
            this.cmnuDown.Name = "cmnuDown";
            this.cmnuDown.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.cmnuDown.Size = new System.Drawing.Size(310, 26);
            this.cmnuDown.Text = "Nächste Sammlungsmünze";
            this.cmnuDown.Click += new System.EventHandler(this.cmnuDown_Click);
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
            this.cmnuMünzdetails.Click += new System.EventHandler(this.cmnuMünzdetails_Click);
            // 
            // cmnuMünzeAdd
            // 
            this.cmnuMünzeAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmnuMünzeAdd.Image")));
            this.cmnuMünzeAdd.Name = "cmnuMünzeAdd";
            this.cmnuMünzeAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.cmnuMünzeAdd.Size = new System.Drawing.Size(310, 26);
            this.cmnuMünzeAdd.Text = "Münze hinzufügen";
            this.cmnuMünzeAdd.Click += new System.EventHandler(this.cmnuMünzeAdd_Click);
            // 
            // cmenuDeleteCoin
            // 
            this.cmenuDeleteCoin.Image = ((System.Drawing.Image)(resources.GetObject("cmenuDeleteCoin.Image")));
            this.cmenuDeleteCoin.Name = "cmenuDeleteCoin";
            this.cmenuDeleteCoin.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.cmenuDeleteCoin.Size = new System.Drawing.Size(310, 26);
            this.cmenuDeleteCoin.Text = "Münze löschen";
            this.cmenuDeleteCoin.Click += new System.EventHandler(this.cmenuDeleteCoin_Click);
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
            this.cmnuEigeneKatalognummern.Click += new System.EventHandler(this.cmnuEigeneKatalognummern_Click);
            // 
            // cmnuPicture
            // 
            this.cmnuPicture.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPicture.Image")));
            this.cmnuPicture.Name = "cmnuPicture";
            this.cmnuPicture.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.cmnuPicture.Size = new System.Drawing.Size(310, 26);
            this.cmnuPicture.Text = "Eigenes Bild importieren";
            this.cmnuPicture.Click += new System.EventHandler(this.cmnuPicture_Click);
            // 
            // cmnuPreise
            // 
            this.cmnuPreise.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPreise.Image")));
            this.cmnuPreise.Name = "cmnuPreise";
            this.cmnuPreise.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.cmnuPreise.Size = new System.Drawing.Size(310, 26);
            this.cmnuPreise.Text = "Eigene Preise bearbeiten";
            this.cmnuPreise.Click += new System.EventHandler(this.cmnuPreise_Click);
            // 
            // cmnuAuktionen
            // 
            this.cmnuAuktionen.Image = ((System.Drawing.Image)(resources.GetObject("cmnuAuktionen.Image")));
            this.cmnuAuktionen.Name = "cmnuAuktionen";
            this.cmnuAuktionen.Size = new System.Drawing.Size(310, 26);
            this.cmnuAuktionen.Text = "Münz-Auktionen verwalten";
            this.cmnuAuktionen.Click += new System.EventHandler(this.cmnuAuktionen_Click);
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
            this.cmnuPrägeanstalten.Click += new System.EventHandler(this.cmnuPrägeanstalten_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 1000;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipTitle = "Copyright";
            // 
            // panCoins
            // 
            this.panCoins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCoins.Controls.Add(this.tableLayoutPanel1);
            this.panCoins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panCoins.Location = new System.Drawing.Point(0, 0);
            this.panCoins.Name = "panCoins";
            this.panCoins.Size = new System.Drawing.Size(1328, 560);
            this.panCoins.TabIndex = 18;
            // 
            // usrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panCoins);
            this.Name = "usrMain";
            this.Size = new System.Drawing.Size(1328, 560);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splashPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEigen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMünze)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain1)).EndInit();
            this.cmnuStrip.ResumeLayout(false);
            this.panCoins.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Syncfusion.Windows.Forms.Tools.SplashPanel splashPanel1;
        private System.Windows.Forms.Label labelEx1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grdMain1;
        private System.Windows.Forms.ContextMenuStrip cmnuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuNationVor;
        private System.Windows.Forms.ToolStripMenuItem mnuNationZurück;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuÄraVor;
        private System.Windows.Forms.ToolStripMenuItem mnuÄraZurück;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem mnuGebietVor;
        private System.Windows.Forms.ToolStripMenuItem mnuGebietZurück;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem cmnuUp;
        private System.Windows.Forms.ToolStripMenuItem cmnuDown;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem cmnuMünzdetails;
        private System.Windows.Forms.ToolStripMenuItem cmnuMünzeAdd;
        private System.Windows.Forms.ToolStripMenuItem cmenuDeleteCoin;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmnuEigeneKatalognummern;
        private System.Windows.Forms.ToolStripMenuItem cmnuPicture;
        private System.Windows.Forms.ToolStripMenuItem cmnuPreise;
        private System.Windows.Forms.ToolStripMenuItem cmnuAuktionen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cmnuPrägeanstalten;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblNation;
        private SAN.Control.PictureBoxEx picEigen;
        private System.Windows.Forms.Label lblWährung;
        private System.Windows.Forms.Label lblGebiet;
        private SAN.Control.ComboBoxEx cboJahr;
        private System.Windows.Forms.ComboBox cboGebiete;
        private System.Windows.Forms.Label lblJahr;
        private SAN.Control.ComboBoxEx cboNominale;
        private SAN.Control.ComboBoxEx cboWährung;
        private System.Windows.Forms.Label lblÄra;
        private System.Windows.Forms.ComboBox cboÄra;
        private System.Windows.Forms.Label lblNominale;
        private System.Windows.Forms.Button btnNavigate;
        private SAN.Control.PictureBoxEx picMünze;
        private System.Windows.Forms.ComboBox cboNationen;
        private System.Windows.Forms.Label lblBestandAnzeige;
        private System.Windows.Forms.Label lblAnzeigeTyp;
        private Panel panCoins;
    }
}
