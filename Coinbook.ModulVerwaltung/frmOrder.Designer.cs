using System;
namespace Coinbook.Modulverwaltung
{
  partial class frmOrder
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
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bNav = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.grdUpdate = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWaehrung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAlle = new SAN.Control.ButtonEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optPaypal = new System.Windows.Forms.RadioButton();
            this.optÜberweisung = new System.Windows.Forms.RadioButton();
            this.grdNeu = new System.Windows.Forms.DataGridView();
            this.colCheckNeu = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNummerNeu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModulNeu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVersionNeu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreisNeu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWaehrungNeu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNameNeu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelEx1 = new System.Windows.Forms.Label();
            this.lblUpdateSumme = new System.Windows.Forms.Label();
            this.lblRabatt = new System.Windows.Forms.Label();
            this.lblMwst = new System.Windows.Forms.Label();
            this.lblSummeUpdate = new System.Windows.Forms.Label();
            this.lblVersand = new System.Windows.Forms.Label();
            this.lblGesamtSumme = new System.Windows.Forms.Label();
            this.lblGesamt = new System.Windows.Forms.Label();
            this.lblRabattBetrag = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optVersand = new System.Windows.Forms.RadioButton();
            this.optDownload = new System.Windows.Forms.RadioButton();
            this.lblNeuSumme = new System.Windows.Forms.Label();
            this.lblSummeNeu = new System.Windows.Forms.Label();
            this.btnOrder = new SAN.Control.ButtonEx();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblRabattU = new System.Windows.Forms.Label();
            this.lblRabattBetragU = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bNav)).BeginInit();
            this.bNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUpdate)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeu)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bNav
            // 
            this.bNav.AddNewItem = null;
            this.bNav.BackColor = System.Drawing.SystemColors.Control;
            this.bNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bNav.CountItem = null;
            this.bNav.DeleteItem = null;
            this.bNav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose});
            this.bNav.Location = new System.Drawing.Point(0, 0);
            this.bNav.MoveFirstItem = null;
            this.bNav.MoveLastItem = null;
            this.bNav.MoveNextItem = null;
            this.bNav.MovePreviousItem = null;
            this.bNav.Name = "bNav";
            this.bNav.Padding = new System.Windows.Forms.Padding(0);
            this.bNav.PositionItem = null;
            this.bNav.Size = new System.Drawing.Size(949, 25);
            this.bNav.Stretch = true;
            this.bNav.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "Schliessen";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdate.ForeColor = System.Drawing.Color.Red;
            this.lblUpdate.Location = new System.Drawing.Point(3, 30);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(294, 60);
            this.lblUpdate.TabIndex = 6;
            this.lblUpdate.Text = "Hier können Sie alle für Sie lizenzierten Module und Programme als Update bestell" +
    "en";
            this.lblUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdUpdate
            // 
            this.grdUpdate.AllowUserToAddRows = false;
            this.grdUpdate.AllowUserToDeleteRows = false;
            this.grdUpdate.AllowUserToResizeColumns = false;
            this.grdUpdate.AllowUserToResizeRows = false;
            this.grdUpdate.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdUpdate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUpdate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colNummer,
            this.colModul,
            this.colVersion,
            this.colPreis,
            this.colWaehrung,
            this.colName});
            this.tableLayoutPanel1.SetColumnSpan(this.grdUpdate, 2);
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdUpdate.DefaultCellStyle = dataGridViewCellStyle7;
            this.grdUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUpdate.Location = new System.Drawing.Point(3, 93);
            this.grdUpdate.Name = "grdUpdate";
            this.grdUpdate.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.grdUpdate, 12);
            this.grdUpdate.ShowEditingIcon = false;
            this.grdUpdate.Size = new System.Drawing.Size(457, 562);
            this.grdUpdate.TabIndex = 8;
            this.grdUpdate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdUpdate_CellClick);
            // 
            // colCheck
            // 
            this.colCheck.DataPropertyName = "Bestellen";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colCheck.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCheck.FalseValue = "False";
            this.colCheck.HeaderText = "Bestellen";
            this.colCheck.Name = "colCheck";
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCheck.TrueValue = "True";
            this.colCheck.Width = 55;
            // 
            // colNummer
            // 
            this.colNummer.DataPropertyName = "Nummer";
            this.colNummer.HeaderText = "Best-Nr";
            this.colNummer.Name = "colNummer";
            this.colNummer.ReadOnly = true;
            this.colNummer.Width = 60;
            // 
            // colModul
            // 
            this.colModul.DataPropertyName = "Beschreibung";
            this.colModul.HeaderText = "Modul";
            this.colModul.Name = "colModul";
            this.colModul.ReadOnly = true;
            this.colModul.Width = 180;
            // 
            // colVersion
            // 
            this.colVersion.DataPropertyName = "Version";
            this.colVersion.HeaderText = "Version";
            this.colVersion.Name = "colVersion";
            this.colVersion.ReadOnly = true;
            this.colVersion.Width = 60;
            // 
            // colPreis
            // 
            this.colPreis.DataPropertyName = "Preis";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.colPreis.DefaultCellStyle = dataGridViewCellStyle3;
            this.colPreis.HeaderText = "Preis";
            this.colPreis.Name = "colPreis";
            this.colPreis.ReadOnly = true;
            this.colPreis.Width = 60;
            // 
            // colWaehrung
            // 
            this.colWaehrung.DataPropertyName = "Währung";
            this.colWaehrung.HeaderText = " ";
            this.colWaehrung.Name = "colWaehrung";
            this.colWaehrung.ReadOnly = true;
            this.colWaehrung.Width = 20;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Visible = false;
            // 
            // btnAlle
            // 
            this.btnAlle.BackColor = System.Drawing.SystemColors.Control;
            this.btnAlle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAlle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlle.ForeColor = System.Drawing.Color.Black;
            this.btnAlle.ImageStretch = false;
            this.btnAlle.Location = new System.Drawing.Point(303, 33);
            this.btnAlle.Name = "btnAlle";
            this.btnAlle.Size = new System.Drawing.Size(157, 54);
            this.btnAlle.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnAlle.Stretched = false;
            this.btnAlle.TabIndex = 9;
            this.btnAlle.Text = "Alle Modulupdates bestellen";
            this.btnAlle.UseVisualStyleBackColor = false;
            this.btnAlle.Click += new System.EventHandler(this.btnAlle_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 3, 13);
            this.tableLayoutPanel1.Controls.Add(this.grdNeu, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblUpdate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAlle, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.grdUpdate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelEx1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblUpdateSumme, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblRabatt, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblMwst, 3, 12);
            this.tableLayoutPanel1.Controls.Add(this.lblSummeUpdate, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblVersand, 5, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblGesamtSumme, 3, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblGesamt, 5, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblRabattBetrag, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblNeuSumme, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblSummeNeu, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnOrder, 5, 13);
            this.tableLayoutPanel1.Controls.Add(this.lblVersion, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblRabattU, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblRabattBetragU, 5, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(949, 658);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.optPaypal);
            this.panel2.Controls.Add(this.optÜberweisung);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(486, 611);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 44);
            this.panel2.TabIndex = 36;
            // 
            // optPaypal
            // 
            this.optPaypal.AutoSize = true;
            this.optPaypal.Location = new System.Drawing.Point(0, 19);
            this.optPaypal.Name = "optPaypal";
            this.optPaypal.Size = new System.Drawing.Size(117, 17);
            this.optPaypal.TabIndex = 31;
            this.optPaypal.TabStop = true;
            this.optPaypal.Text = "Zahlung per Paypal";
            this.optPaypal.UseVisualStyleBackColor = true;
            // 
            // optÜberweisung
            // 
            this.optÜberweisung.AutoSize = true;
            this.optÜberweisung.Checked = true;
            this.optÜberweisung.Location = new System.Drawing.Point(0, 0);
            this.optÜberweisung.Name = "optÜberweisung";
            this.optÜberweisung.Size = new System.Drawing.Size(147, 17);
            this.optÜberweisung.TabIndex = 30;
            this.optÜberweisung.TabStop = true;
            this.optÜberweisung.Text = "Zahlung per Überweisung";
            this.optÜberweisung.UseVisualStyleBackColor = false;
            // 
            // grdNeu
            // 
            this.grdNeu.AllowUserToAddRows = false;
            this.grdNeu.AllowUserToDeleteRows = false;
            this.grdNeu.AllowUserToResizeColumns = false;
            this.grdNeu.AllowUserToResizeRows = false;
            this.grdNeu.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNeu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdNeu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNeu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheckNeu,
            this.colNummerNeu,
            this.colModulNeu,
            this.colVersionNeu,
            this.colPreisNeu,
            this.colWaehrungNeu,
            this.ColNameNeu});
            this.tableLayoutPanel1.SetColumnSpan(this.grdNeu, 3);
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdNeu.DefaultCellStyle = dataGridViewCellStyle6;
            this.grdNeu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNeu.Location = new System.Drawing.Point(486, 93);
            this.grdNeu.Name = "grdNeu";
            this.grdNeu.RowHeadersVisible = false;
            this.grdNeu.Size = new System.Drawing.Size(460, 312);
            this.grdNeu.TabIndex = 11;
            this.grdNeu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdNeu_CellClick);
            // 
            // colCheckNeu
            // 
            this.colCheckNeu.DataPropertyName = "Bestellen";
            this.colCheckNeu.FalseValue = "False";
            this.colCheckNeu.HeaderText = "Bestellen";
            this.colCheckNeu.Name = "colCheckNeu";
            this.colCheckNeu.ReadOnly = true;
            this.colCheckNeu.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheckNeu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCheckNeu.TrueValue = "True";
            this.colCheckNeu.Width = 55;
            // 
            // colNummerNeu
            // 
            this.colNummerNeu.DataPropertyName = "Nummer";
            this.colNummerNeu.HeaderText = "Best-Nr";
            this.colNummerNeu.Name = "colNummerNeu";
            this.colNummerNeu.Width = 60;
            // 
            // colModulNeu
            // 
            this.colModulNeu.DataPropertyName = "Beschreibung";
            this.colModulNeu.HeaderText = "Modul";
            this.colModulNeu.Name = "colModulNeu";
            this.colModulNeu.Width = 180;
            // 
            // colVersionNeu
            // 
            this.colVersionNeu.DataPropertyName = "Version";
            this.colVersionNeu.HeaderText = "Version";
            this.colVersionNeu.Name = "colVersionNeu";
            this.colVersionNeu.Width = 60;
            // 
            // colPreisNeu
            // 
            this.colPreisNeu.DataPropertyName = "Preis";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.colPreisNeu.DefaultCellStyle = dataGridViewCellStyle5;
            this.colPreisNeu.HeaderText = "Preis";
            this.colPreisNeu.Name = "colPreisNeu";
            this.colPreisNeu.Width = 60;
            // 
            // colWaehrungNeu
            // 
            this.colWaehrungNeu.DataPropertyName = "Währung";
            this.colWaehrungNeu.HeaderText = "";
            this.colWaehrungNeu.Name = "colWaehrungNeu";
            this.colWaehrungNeu.Width = 20;
            // 
            // ColNameNeu
            // 
            this.ColNameNeu.DataPropertyName = "Name";
            this.ColNameNeu.HeaderText = "Name";
            this.ColNameNeu.Name = "ColNameNeu";
            this.ColNameNeu.ReadOnly = true;
            this.ColNameNeu.Visible = false;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelEx1, 3);
            this.labelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEx1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEx1.ForeColor = System.Drawing.Color.Red;
            this.labelEx1.Location = new System.Drawing.Point(486, 30);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(460, 60);
            this.labelEx1.TabIndex = 10;
            this.labelEx1.Text = "Hier können Sie alle Module und Programme bestellen,  die Sie noch nicht besitzen" +
    "";
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpdateSumme
            // 
            this.lblUpdateSumme.AutoSize = true;
            this.lblUpdateSumme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdateSumme.Location = new System.Drawing.Point(486, 413);
            this.lblUpdateSumme.Name = "lblUpdateSumme";
            this.lblUpdateSumme.Size = new System.Drawing.Size(244, 20);
            this.lblUpdateSumme.TabIndex = 12;
            this.lblUpdateSumme.Text = "Summe der Update-Bestellungen";
            this.lblUpdateSumme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRabatt
            // 
            this.lblRabatt.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRabatt, 2);
            this.lblRabatt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRabatt.Location = new System.Drawing.Point(486, 453);
            this.lblRabatt.Name = "lblRabatt";
            this.lblRabatt.Size = new System.Drawing.Size(344, 25);
            this.lblRabatt.TabIndex = 14;
            this.lblRabatt.Text = "Leider noch kein Rabatt auf neue Module";
            this.lblRabatt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMwst
            // 
            this.lblMwst.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblMwst, 2);
            this.lblMwst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMwst.Location = new System.Drawing.Point(486, 578);
            this.lblMwst.Name = "lblMwst";
            this.lblMwst.Size = new System.Drawing.Size(344, 30);
            this.lblMwst.TabIndex = 15;
            this.lblMwst.Text = "Im Preis {0} Mehrwertsteuer {1}% enthalten";
            this.lblMwst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSummeUpdate
            // 
            this.lblSummeUpdate.AutoSize = true;
            this.lblSummeUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummeUpdate.Location = new System.Drawing.Point(836, 413);
            this.lblSummeUpdate.Name = "lblSummeUpdate";
            this.lblSummeUpdate.Size = new System.Drawing.Size(110, 20);
            this.lblSummeUpdate.TabIndex = 19;
            this.lblSummeUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVersand
            // 
            this.lblVersand.AutoSize = true;
            this.lblVersand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersand.Location = new System.Drawing.Point(836, 528);
            this.lblVersand.Name = "lblVersand";
            this.lblVersand.Size = new System.Drawing.Size(110, 20);
            this.lblVersand.TabIndex = 26;
            this.lblVersand.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGesamtSumme
            // 
            this.lblGesamtSumme.AutoSize = true;
            this.lblGesamtSumme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGesamtSumme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGesamtSumme.Location = new System.Drawing.Point(486, 548);
            this.lblGesamtSumme.Name = "lblGesamtSumme";
            this.lblGesamtSumme.Size = new System.Drawing.Size(244, 30);
            this.lblGesamtSumme.TabIndex = 27;
            this.lblGesamtSumme.Text = "Gesamtbetrag";
            this.lblGesamtSumme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGesamt
            // 
            this.lblGesamt.AutoSize = true;
            this.lblGesamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGesamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGesamt.Location = new System.Drawing.Point(836, 548);
            this.lblGesamt.Name = "lblGesamt";
            this.lblGesamt.Size = new System.Drawing.Size(110, 30);
            this.lblGesamt.TabIndex = 28;
            this.lblGesamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRabattBetrag
            // 
            this.lblRabattBetrag.AutoSize = true;
            this.lblRabattBetrag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRabattBetrag.Location = new System.Drawing.Point(836, 453);
            this.lblRabattBetrag.Name = "lblRabattBetrag";
            this.lblRabattBetrag.Size = new System.Drawing.Size(110, 25);
            this.lblRabattBetrag.TabIndex = 29;
            this.lblRabattBetrag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.optVersand);
            this.panel1.Controls.Add(this.optDownload);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(486, 511);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(344, 34);
            this.panel1.TabIndex = 32;
            // 
            // optVersand
            // 
            this.optVersand.AutoSize = true;
            this.optVersand.Location = new System.Drawing.Point(0, 19);
            this.optVersand.Name = "optVersand";
            this.optVersand.Size = new System.Drawing.Size(133, 17);
            this.optVersand.TabIndex = 31;
            this.optVersand.TabStop = true;
            this.optVersand.Text = "Lieferung per CD/DVD";
            this.optVersand.UseVisualStyleBackColor = true;
            this.optVersand.CheckedChanged += new System.EventHandler(this.optDownload_CheckedChanged);
            // 
            // optDownload
            // 
            this.optDownload.AutoSize = true;
            this.optDownload.Checked = true;
            this.optDownload.Location = new System.Drawing.Point(0, 0);
            this.optDownload.Name = "optDownload";
            this.optDownload.Size = new System.Drawing.Size(188, 17);
            this.optDownload.TabIndex = 30;
            this.optDownload.TabStop = true;
            this.optDownload.Text = "Lieferung per Download (standard)";
            this.optDownload.UseVisualStyleBackColor = false;
            this.optDownload.CheckedChanged += new System.EventHandler(this.optDownload_CheckedChanged);
            // 
            // lblNeuSumme
            // 
            this.lblNeuSumme.AutoSize = true;
            this.lblNeuSumme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNeuSumme.Location = new System.Drawing.Point(486, 433);
            this.lblNeuSumme.Name = "lblNeuSumme";
            this.lblNeuSumme.Size = new System.Drawing.Size(244, 20);
            this.lblNeuSumme.TabIndex = 33;
            this.lblNeuSumme.Text = "Summe Neubestellungen";
            this.lblNeuSumme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSummeNeu
            // 
            this.lblSummeNeu.AutoSize = true;
            this.lblSummeNeu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummeNeu.Location = new System.Drawing.Point(836, 433);
            this.lblSummeNeu.Name = "lblSummeNeu";
            this.lblSummeNeu.Size = new System.Drawing.Size(110, 20);
            this.lblSummeNeu.TabIndex = 34;
            this.lblSummeNeu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOrder
            // 
            this.btnOrder.BackColor = System.Drawing.SystemColors.Control;
            this.btnOrder.ForeColor = System.Drawing.Color.Black;
            this.btnOrder.ImageStretch = false;
            this.btnOrder.Location = new System.Drawing.Point(836, 611);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(110, 24);
            this.btnOrder.Status = SAN.Control.ButtonStatus.Nothing;
            this.btnOrder.Stretched = false;
            this.btnOrder.TabIndex = 35;
            this.btnOrder.Text = "Bestellen";
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblVersion, 6);
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(3, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(943, 30);
            this.lblVersion.TabIndex = 37;
            this.lblVersion.Text = "Version";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRabattU
            // 
            this.lblRabattU.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRabattU, 2);
            this.lblRabattU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRabattU.Location = new System.Drawing.Point(486, 478);
            this.lblRabattU.Name = "lblRabattU";
            this.lblRabattU.Size = new System.Drawing.Size(344, 25);
            this.lblRabattU.TabIndex = 38;
            this.lblRabattU.Text = "Leider noch kein Rabatt auf Modul-Updates";
            this.lblRabattU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRabattBetragU
            // 
            this.lblRabattBetragU.AutoSize = true;
            this.lblRabattBetragU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRabattBetragU.Location = new System.Drawing.Point(836, 478);
            this.lblRabattBetragU.Name = "lblRabattBetragU";
            this.lblRabattBetragU.Size = new System.Drawing.Size(110, 25);
            this.lblRabattBetragU.TabIndex = 39;
            this.lblRabattBetragU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(949, 683);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.bNav);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Module bestellen";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrder_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bNav)).EndInit();
            this.bNav.ResumeLayout(false);
            this.bNav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUpdate)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeu)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.BindingNavigator bNav;
    private System.Windows.Forms.ToolStripButton btnClose;
    private System.Windows.Forms.Label lblUpdate;
    private System.Windows.Forms.DataGridView grdUpdate;
    private SAN.Control.ButtonEx btnAlle;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.DataGridView grdNeu;
    private System.Windows.Forms.Label labelEx1;
    private System.Windows.Forms.Label lblUpdateSumme;
    private System.Windows.Forms.Label lblRabatt;
    private System.Windows.Forms.Label lblMwst;
    private System.Windows.Forms.Label lblSummeUpdate;
    private System.Windows.Forms.Label lblVersand;
    private System.Windows.Forms.Label lblGesamtSumme;
    private System.Windows.Forms.Label lblGesamt;
    private System.Windows.Forms.Label lblRabattBetrag;
    private System.Windows.Forms.RadioButton optDownload;
    private System.Windows.Forms.RadioButton optVersand;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblNeuSumme;
    private System.Windows.Forms.Label lblSummeNeu;
    private SAN.Control.ButtonEx btnOrder;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.RadioButton optPaypal;
    private System.Windows.Forms.RadioButton optÜberweisung;
    private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblRabattU;
		private System.Windows.Forms.Label lblRabattBetragU;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
		private System.Windows.Forms.DataGridViewTextBoxColumn colNummer;
		private System.Windows.Forms.DataGridViewTextBoxColumn colModul;
		private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPreis;
		private System.Windows.Forms.DataGridViewTextBoxColumn colWaehrung;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colCheckNeu;
		private System.Windows.Forms.DataGridViewTextBoxColumn colNummerNeu;
		private System.Windows.Forms.DataGridViewTextBoxColumn colModulNeu;
		private System.Windows.Forms.DataGridViewTextBoxColumn colVersionNeu;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPreisNeu;
		private System.Windows.Forms.DataGridViewTextBoxColumn colWaehrungNeu;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColNameNeu;
	}
}