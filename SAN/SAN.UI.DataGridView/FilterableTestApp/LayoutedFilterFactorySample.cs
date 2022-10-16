using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

using SAN.UI.DataGridView;
using SAN.UI.DataGridView.GridFilters;

namespace FilterableTestApp
{
	public class LayoutedFilterFactorySample : System.Windows.Forms.Form
	{
		private SAN.UI.DataGridView.GridFilterFactories.LayoutedGridFilterFactoryControl _filterFactory;
		private SAN.UI.DataGridView.FilterableDataGrid _grid;
		private System.Windows.Forms.Splitter _splitter;
		private System.Windows.Forms.GroupBox _gbFactory;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton _rbAlignmentLeft;
		private System.Windows.Forms.RadioButton _rbAlignmentRight;
		private System.Windows.Forms.TextBox _tbMinControlWidth;
		private System.Windows.Forms.CheckBox _cbUseDefaultPlacement;
		private System.Windows.Forms.ListView _lvColumns;
		private System.ComponentModel.Container components = null;
		private StringCollection _checkedItems = new StringCollection();
		private bool _addingItem = false;

		public LayoutedFilterFactorySample()
		{
			InitializeComponent();
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _grid.EmbeddedDataGridView.DataSourceChanged += new EventHandler(OnDataSourceChanged);
            _grid.DataSource = DataHelper.SampleData.Tables[1].DefaultView;
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
			this._filterFactory = new SAN.UI.DataGridView.GridFilterFactories.LayoutedGridFilterFactoryControl();
			this._grid = new SAN.UI.DataGridView.FilterableDataGrid();
			this._splitter = new System.Windows.Forms.Splitter();
			this._gbFactory = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._rbAlignmentLeft = new System.Windows.Forms.RadioButton();
			this._rbAlignmentRight = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this._tbMinControlWidth = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this._cbUseDefaultPlacement = new System.Windows.Forms.CheckBox();
			this._lvColumns = new System.Windows.Forms.ListView();
			this._gbFactory.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _filterFactory
			// 
			this._filterFactory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._filterFactory.ControlsMinimumWidth = 120;
			this._filterFactory.Location = new System.Drawing.Point(8, 16);
			this._filterFactory.Name = "_filterFactory";
			this._filterFactory.Size = new System.Drawing.Size(240, 320);
			this._filterFactory.TabIndex = 0;
			this._filterFactory.GridFilterCreated += new SAN.UI.DataGridView.GridFilterEventHandler(this.OnGridFilterCreated);
			// 
			// _grid
			// 
			this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this._grid.FilterBoxPosition = SAN.UI.DataGridView.FilterPosition.Off;
			this._grid.FilterFactory = this._filterFactory;
			this._grid.KeepFilters = true;
			this._grid.Location = new System.Drawing.Point(268, 140);
			this._grid.Name = "_grid";
			this._grid.Size = new System.Drawing.Size(428, 345);
			this._grid.TabIndex = 1;
			// 
			// _splitter
			// 
			this._splitter.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this._splitter.Location = new System.Drawing.Point(264, 140);
			this._splitter.Name = "_splitter";
			this._splitter.Size = new System.Drawing.Size(4, 345);
			this._splitter.TabIndex = 2;
			this._splitter.TabStop = false;
			// 
			// _gbFactory
			// 
			this._gbFactory.Controls.Add(this._filterFactory);
			this._gbFactory.Dock = System.Windows.Forms.DockStyle.Left;
			this._gbFactory.Location = new System.Drawing.Point(8, 140);
			this._gbFactory.Name = "_gbFactory";
			this._gbFactory.Size = new System.Drawing.Size(256, 345);
			this._gbFactory.TabIndex = 3;
			this._gbFactory.TabStop = false;
			this._gbFactory.Text = "Filter";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._rbAlignmentLeft);
			this.groupBox1.Controls.Add(this._rbAlignmentRight);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(104, 80);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Alignment";
			// 
			// _rbAlignmentLeft
			// 
			this._rbAlignmentLeft.Checked = true;
			this._rbAlignmentLeft.Location = new System.Drawing.Point(24, 24);
			this._rbAlignmentLeft.Name = "_rbAlignmentLeft";
			this._rbAlignmentLeft.Size = new System.Drawing.Size(56, 16);
			this._rbAlignmentLeft.TabIndex = 0;
			this._rbAlignmentLeft.TabStop = true;
			this._rbAlignmentLeft.Text = "Left";
			this._rbAlignmentLeft.CheckedChanged += new System.EventHandler(this.OnAlignmentCheckedChanged);
			// 
			// _rbAlignmentRight
			// 
			this._rbAlignmentRight.Location = new System.Drawing.Point(24, 48);
			this._rbAlignmentRight.Name = "_rbAlignmentRight";
			this._rbAlignmentRight.Size = new System.Drawing.Size(56, 16);
			this._rbAlignmentRight.TabIndex = 0;
			this._rbAlignmentRight.Text = "Right";
			this._rbAlignmentRight.CheckedChanged += new System.EventHandler(this.OnAlignmentCheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(120, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Minimum control width:";
			// 
			// _tbMinControlWidth
			// 
			this._tbMinControlWidth.Location = new System.Drawing.Point(120, 24);
			this._tbMinControlWidth.Name = "_tbMinControlWidth";
			this._tbMinControlWidth.Size = new System.Drawing.Size(112, 20);
			this._tbMinControlWidth.TabIndex = 6;
			this._tbMinControlWidth.Text = "120";
			this._tbMinControlWidth.TextChanged += new System.EventHandler(this.OnMinControlWidthChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(216, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "<< Move splitter >>";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(256, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 32);
			this.label3.TabIndex = 5;
			this.label3.Text = "Do not show in LayoutedFilterFactory:";
			// 
			// _cbUseDefaultPlacement
			// 
			this._cbUseDefaultPlacement.Location = new System.Drawing.Point(280, 40);
			this._cbUseDefaultPlacement.Name = "_cbUseDefaultPlacement";
			this._cbUseDefaultPlacement.Size = new System.Drawing.Size(88, 32);
			this._cbUseDefaultPlacement.TabIndex = 0;
			this._cbUseDefaultPlacement.Text = "Use default placement";
			this._cbUseDefaultPlacement.CheckedChanged += new System.EventHandler(this.OnUseDefaultPlacementCheckedChanged);
			// 
			// _lvColumns
			// 
			this._lvColumns.CheckBoxes = true;
			this._lvColumns.FullRowSelect = true;
			this._lvColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this._lvColumns.Location = new System.Drawing.Point(368, 8);
			this._lvColumns.MultiSelect = false;
			this._lvColumns.Name = "_lvColumns";
			this._lvColumns.Size = new System.Drawing.Size(320, 120);
			this._lvColumns.TabIndex = 9;
			this._lvColumns.View = System.Windows.Forms.View.List;
			this._lvColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OnItemCheck);
			// 
			// LayoutedFilterFactorySample
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(704, 493);
			this.Controls.Add(this._lvColumns);
			this.Controls.Add(this._cbUseDefaultPlacement);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._tbMinControlWidth);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._grid);
			this.Controls.Add(this._splitter);
			this.Controls.Add(this._gbFactory);
			this.Controls.Add(this.label3);
			this.DockPadding.Bottom = 8;
			this.DockPadding.Left = 8;
			this.DockPadding.Right = 8;
			this.DockPadding.Top = 140;
			this.Name = "LayoutedFilterFactorySample";
			this.Text = "Sample 4 - LayoutedFilterFactory";
			this._gbFactory.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnAlignmentCheckedChanged(object sender, System.EventArgs e)
		{
			if (sender == _rbAlignmentLeft && _rbAlignmentLeft.Checked)
				_filterFactory.RightAlignLabels = false;
			else if (sender == _rbAlignmentRight && _rbAlignmentRight.Checked)
				_filterFactory.RightAlignLabels = true;
		}

		private void OnMinControlWidthChanged(object sender, System.EventArgs e)
		{
			int value = 0;
			try
			{
				value = Convert.ToInt32(_tbMinControlWidth.Text);
			} 
			catch {}
			if (value > 0)
				_filterFactory.ControlsMinimumWidth = value;
		}

		private void OnUseDefaultPlacementCheckedChanged(object sender, System.EventArgs e)
		{
			_filterFactory.HasChanged();
			_grid.FilterBoxPosition = _cbUseDefaultPlacement.Checked ?  FilterPosition.Top : FilterPosition.Off;
		}

		private void OnDataSourceChanged(object sender, EventArgs e)
		{
			_lvColumns.Items.Clear();
		}

		private void OnGridFilterCreated(object sender, SAN.UI.DataGridView.GridFilterEventArgs args)
		{
			try 
			{
				_addingItem = true;
				if (!Contains(args.HeaderText))
				{
					ListViewItem newItem = new ListViewItem(args.HeaderText);
					newItem.Checked = true;
					_lvColumns.Items.Add(newItem);
					if (!_checkedItems.Contains(args.HeaderText))
						_checkedItems.Add(args.HeaderText);
				}
				else if (!_checkedItems.Contains(args.HeaderText))
				{
					if (_cbUseDefaultPlacement.Checked)
						args.GridFilter.UseCustomFilterPlacement = false;
					else
						args.GridFilter = new EmptyGridFilter();
				}
			} 
			finally
			{
				_addingItem = false;
			}
		}

		private bool Contains(string headerText)
		{
			for (int i = 0; i < _lvColumns.Items.Count; i++)
				if (_lvColumns.Items[i].Text == headerText) 
					return true;

			return false;
		}

		private void OnItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if (_addingItem)
				return;

			ListViewItem item = _lvColumns.Items[e.Index];
			if (e.NewValue == CheckState.Checked)
			{
				if (!_checkedItems.Contains(item.Text))
					_checkedItems.Add(item.Text);
			} 
			else 
			{
				if (_checkedItems.Contains(item.Text))
					_checkedItems.Remove(item.Text);
			}

			_filterFactory.HasChanged();
		}
	}
}
