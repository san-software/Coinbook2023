using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using SAN.UI.DataGridView;
using SAN.UI.DataGridView.GridFilterFactories;

namespace FilterableTestApp
{
	public class FullTextSearchSample : System.Windows.Forms.Form
	{
        private SAN.UI.DataGridView.GridFilterFactories.FullTextSearchGridFilterFactoryTextBox _tbFilterFactory;
        private System.Windows.Forms.Label _lblFilterText;
        private SAN.UI.DataGridView.FilterableDataGrid _grid3;
		private System.Windows.Forms.CheckBox _cbGrid3;
        private TableLayoutPanel _tableLayoutPanel;
        private Panel _panel3;
        private Panel _panel1;
        private FilterableDataGrid _grid1;
        private CheckBox _cbGrid1;
        private Panel _panel2;
        private FilterableDataGrid _grid2;
        private CheckBox _cbGrid2;
        private Panel _panel4;
        private FilterableDataGrid _grid4;
        private CheckBox _cbGrid4;
		private System.ComponentModel.Container components = null;

		public FullTextSearchSample()
		{
			InitializeComponent();
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
			_cbGrid1.Tag = _grid1;
			_cbGrid2.Tag = _grid2;
			_cbGrid3.Tag = _grid3;
			_cbGrid4.Tag = _grid4;

			_grid1.EmbeddedDataGridView.ReadOnly = true;
			_grid2.EmbeddedDataGridView.ReadOnly = true;
			_grid3.EmbeddedDataGridView.ReadOnly = true;
			_grid4.EmbeddedDataGridView.ReadOnly = true;

			_grid1.DataSource = DataHelper.SampleData.Tables[1].DefaultView;
			_grid2.DataSource = DataHelper.SampleData.Tables[2].DefaultView;
			_grid3.DataSource = DataHelper.SampleData.Tables[3].DefaultView;
			_grid4.DataSource = DataHelper.SampleData.Tables[4].DefaultView;

			OnEnabledFullTextSearchCheckedChanged(_cbGrid1, EventArgs.Empty);
			OnEnabledFullTextSearchCheckedChanged(_cbGrid2, EventArgs.Empty);
			OnEnabledFullTextSearchCheckedChanged(_cbGrid3, EventArgs.Empty);
			OnEnabledFullTextSearchCheckedChanged(_cbGrid4, EventArgs.Empty);
        }

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory defaultGridFilterFactory5 = new SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory();
            SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory defaultGridFilterFactory6 = new SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory();
            SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory defaultGridFilterFactory7 = new SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory();
            SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory defaultGridFilterFactory8 = new SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory();
            this._lblFilterText = new System.Windows.Forms.Label();
            this._cbGrid3 = new System.Windows.Forms.CheckBox();
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._panel3 = new System.Windows.Forms.Panel();
            this._panel1 = new System.Windows.Forms.Panel();
            this._cbGrid1 = new System.Windows.Forms.CheckBox();
            this._panel2 = new System.Windows.Forms.Panel();
            this._cbGrid2 = new System.Windows.Forms.CheckBox();
            this._panel4 = new System.Windows.Forms.Panel();
            this._cbGrid4 = new System.Windows.Forms.CheckBox();
            this._grid3 = new SAN.UI.DataGridView.FilterableDataGrid();
            this._grid1 = new SAN.UI.DataGridView.FilterableDataGrid();
            this._grid2 = new SAN.UI.DataGridView.FilterableDataGrid();
            this._grid4 = new SAN.UI.DataGridView.FilterableDataGrid();
            this._tbFilterFactory = new SAN.UI.DataGridView.GridFilterFactories.FullTextSearchGridFilterFactoryTextBox();
            this._tableLayoutPanel.SuspendLayout();
            this._panel3.SuspendLayout();
            this._panel1.SuspendLayout();
            this._panel2.SuspendLayout();
            this._panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblFilterText
            // 
            this._lblFilterText.Location = new System.Drawing.Point(8, 8);
            this._lblFilterText.Name = "_lblFilterText";
            this._lblFilterText.Size = new System.Drawing.Size(56, 24);
            this._lblFilterText.TabIndex = 2;
            this._lblFilterText.Text = "Filter text:";
            this._lblFilterText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cbGrid3
            // 
            this._cbGrid3.BackColor = System.Drawing.SystemColors.Control;
            this._cbGrid3.Checked = true;
            this._cbGrid3.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cbGrid3.Dock = System.Windows.Forms.DockStyle.Top;
            this._cbGrid3.ForeColor = System.Drawing.SystemColors.ControlText;
            this._cbGrid3.Location = new System.Drawing.Point(0, 0);
            this._cbGrid3.Name = "_cbGrid3";
            this._cbGrid3.Size = new System.Drawing.Size(383, 16);
            this._cbGrid3.TabIndex = 1;
            this._cbGrid3.Text = "Enable full text search";
            this._cbGrid3.UseVisualStyleBackColor = false;
            this._cbGrid3.CheckedChanged += new System.EventHandler(this.OnEnabledFullTextSearchCheckedChanged);
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tableLayoutPanel.ColumnCount = 2;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Controls.Add(this._panel3, 0, 1);
            this._tableLayoutPanel.Controls.Add(this._panel1, 0, 0);
            this._tableLayoutPanel.Controls.Add(this._panel2, 1, 0);
            this._tableLayoutPanel.Controls.Add(this._panel4, 1, 1);
            this._tableLayoutPanel.Location = new System.Drawing.Point(11, 35);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 2;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(778, 635);
            this._tableLayoutPanel.TabIndex = 4;
            // 
            // _panel3
            // 
            this._panel3.Controls.Add(this._grid3);
            this._panel3.Controls.Add(this._cbGrid3);
            this._panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel3.Location = new System.Drawing.Point(3, 320);
            this._panel3.Name = "_panel3";
            this._panel3.Size = new System.Drawing.Size(383, 312);
            this._panel3.TabIndex = 5;
            // 
            // _panel1
            // 
            this._panel1.Controls.Add(this._grid1);
            this._panel1.Controls.Add(this._cbGrid1);
            this._panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel1.Location = new System.Drawing.Point(3, 3);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(383, 311);
            this._panel1.TabIndex = 5;
            // 
            // _cbGrid1
            // 
            this._cbGrid1.BackColor = System.Drawing.SystemColors.Control;
            this._cbGrid1.Checked = true;
            this._cbGrid1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cbGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this._cbGrid1.ForeColor = System.Drawing.SystemColors.ControlText;
            this._cbGrid1.Location = new System.Drawing.Point(0, 0);
            this._cbGrid1.Name = "_cbGrid1";
            this._cbGrid1.Size = new System.Drawing.Size(383, 16);
            this._cbGrid1.TabIndex = 1;
            this._cbGrid1.Text = "Enable full text search";
            this._cbGrid1.UseVisualStyleBackColor = false;
            this._cbGrid1.CheckedChanged += new System.EventHandler(this.OnEnabledFullTextSearchCheckedChanged);
            // 
            // _panel2
            // 
            this._panel2.Controls.Add(this._grid2);
            this._panel2.Controls.Add(this._cbGrid2);
            this._panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel2.Location = new System.Drawing.Point(392, 3);
            this._panel2.Name = "_panel2";
            this._panel2.Size = new System.Drawing.Size(383, 311);
            this._panel2.TabIndex = 5;
            // 
            // _cbGrid2
            // 
            this._cbGrid2.BackColor = System.Drawing.SystemColors.Control;
            this._cbGrid2.Checked = true;
            this._cbGrid2.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cbGrid2.Dock = System.Windows.Forms.DockStyle.Top;
            this._cbGrid2.ForeColor = System.Drawing.SystemColors.ControlText;
            this._cbGrid2.Location = new System.Drawing.Point(0, 0);
            this._cbGrid2.Name = "_cbGrid2";
            this._cbGrid2.Size = new System.Drawing.Size(383, 16);
            this._cbGrid2.TabIndex = 1;
            this._cbGrid2.Text = "Enable full text search";
            this._cbGrid2.UseVisualStyleBackColor = false;
            this._cbGrid2.CheckedChanged += new System.EventHandler(this.OnEnabledFullTextSearchCheckedChanged);
            // 
            // _panel4
            // 
            this._panel4.Controls.Add(this._grid4);
            this._panel4.Controls.Add(this._cbGrid4);
            this._panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel4.Location = new System.Drawing.Point(392, 320);
            this._panel4.Name = "_panel4";
            this._panel4.Size = new System.Drawing.Size(383, 312);
            this._panel4.TabIndex = 5;
            // 
            // _cbGrid4
            // 
            this._cbGrid4.BackColor = System.Drawing.SystemColors.Control;
            this._cbGrid4.Checked = true;
            this._cbGrid4.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cbGrid4.Dock = System.Windows.Forms.DockStyle.Top;
            this._cbGrid4.ForeColor = System.Drawing.SystemColors.ControlText;
            this._cbGrid4.Location = new System.Drawing.Point(0, 0);
            this._cbGrid4.Name = "_cbGrid4";
            this._cbGrid4.Size = new System.Drawing.Size(383, 16);
            this._cbGrid4.TabIndex = 1;
            this._cbGrid4.Text = "Enable full text search";
            this._cbGrid4.UseVisualStyleBackColor = false;
            this._cbGrid4.CheckedChanged += new System.EventHandler(this.OnEnabledFullTextSearchCheckedChanged);
            // 
            // _grid3
            // 
            this._grid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid3.FilterBoxPosition = SAN.UI.DataGridView.FilterPosition.Off;
            defaultGridFilterFactory5.CreateDistinctGridFilters = false;
            defaultGridFilterFactory5.DefaultGridFilterType = typeof(SAN.UI.DataGridView.GridFilters.TextGridFilter);
            defaultGridFilterFactory5.HandleEnumerationTypes = true;
            defaultGridFilterFactory5.MaximumDistinctValues = 20;
            this._grid3.FilterFactory = defaultGridFilterFactory5;
            this._grid3.Location = new System.Drawing.Point(0, 16);
            this._grid3.Name = "_grid3";
            this._grid3.Operator = SAN.UI.DataGridView.LogicalOperators.Or;
            this._grid3.Size = new System.Drawing.Size(383, 296);
            this._grid3.TabIndex = 0;
            // 
            // _grid1
            // 
            this._grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid1.FilterBoxPosition = SAN.UI.DataGridView.FilterPosition.Off;
            defaultGridFilterFactory6.CreateDistinctGridFilters = false;
            defaultGridFilterFactory6.DefaultGridFilterType = typeof(SAN.UI.DataGridView.GridFilters.TextGridFilter);
            defaultGridFilterFactory6.HandleEnumerationTypes = true;
            defaultGridFilterFactory6.MaximumDistinctValues = 20;
            this._grid1.FilterFactory = defaultGridFilterFactory6;
            this._grid1.Location = new System.Drawing.Point(0, 16);
            this._grid1.Name = "_grid1";
            this._grid1.Operator = SAN.UI.DataGridView.LogicalOperators.Or;
            this._grid1.Size = new System.Drawing.Size(383, 295);
            this._grid1.TabIndex = 0;
            // 
            // _grid2
            // 
            this._grid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid2.FilterBoxPosition = SAN.UI.DataGridView.FilterPosition.Off;
            defaultGridFilterFactory7.CreateDistinctGridFilters = false;
            defaultGridFilterFactory7.DefaultGridFilterType = typeof(SAN.UI.DataGridView.GridFilters.TextGridFilter);
            defaultGridFilterFactory7.HandleEnumerationTypes = true;
            defaultGridFilterFactory7.MaximumDistinctValues = 20;
            this._grid2.FilterFactory = defaultGridFilterFactory7;
            this._grid2.Location = new System.Drawing.Point(0, 16);
            this._grid2.Name = "_grid2";
            this._grid2.Operator = SAN.UI.DataGridView.LogicalOperators.Or;
            this._grid2.Size = new System.Drawing.Size(383, 295);
            this._grid2.TabIndex = 0;
            // 
            // _grid4
            // 
            this._grid4.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid4.FilterBoxPosition = SAN.UI.DataGridView.FilterPosition.Off;
            defaultGridFilterFactory8.CreateDistinctGridFilters = false;
            defaultGridFilterFactory8.DefaultGridFilterType = typeof(SAN.UI.DataGridView.GridFilters.TextGridFilter);
            defaultGridFilterFactory8.HandleEnumerationTypes = true;
            defaultGridFilterFactory8.MaximumDistinctValues = 20;
            this._grid4.FilterFactory = defaultGridFilterFactory8;
            this._grid4.Location = new System.Drawing.Point(0, 16);
            this._grid4.Name = "_grid4";
            this._grid4.Operator = SAN.UI.DataGridView.LogicalOperators.Or;
            this._grid4.Size = new System.Drawing.Size(383, 296);
            this._grid4.TabIndex = 0;
            // 
            // _tbFilterFactory
            // 
            this._tbFilterFactory.Location = new System.Drawing.Point(72, 8);
            this._tbFilterFactory.Name = "_tbFilterFactory";
            this._tbFilterFactory.Size = new System.Drawing.Size(184, 20);
            this._tbFilterFactory.TabIndex = 1;
            this._tbFilterFactory.Text = "*";
            // 
            // FullTextSearchSample
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(801, 682);
            this.Controls.Add(this._tableLayoutPanel);
            this.Controls.Add(this._lblFilterText);
            this.Controls.Add(this._tbFilterFactory);
            this.Name = "FullTextSearchSample";
            this.Text = "Sample 3 - Full text search in one or many grids";
            this._tableLayoutPanel.ResumeLayout(false);
            this._panel3.ResumeLayout(false);
            this._panel1.ResumeLayout(false);
            this._panel2.ResumeLayout(false);
            this._panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void OnEnabledFullTextSearchCheckedChanged(object sender, System.EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (checkBox == null)
				return;
			FilterableDataGrid grid = checkBox.Tag as FilterableDataGrid;
			if (grid == null)
				return;
			if (checkBox.Checked)
			{
				grid.FilterFactory = _tbFilterFactory;
				grid.FilterBoxPosition = FilterPosition.Off;
			} 
			else 
			{
				grid.FilterFactory = new DefaultGridFilterFactory();
				grid.FilterBoxPosition = FilterPosition.Top;
			}
		}
	}
}
