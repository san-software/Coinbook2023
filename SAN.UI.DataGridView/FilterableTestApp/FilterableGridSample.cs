using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using SAN.UI.DataGridView;
using SAN.UI.DataGridView.GridFilters;
using SAN.UI.DataGridView.GridFilterFactories;

namespace FilterableTestApp
{
	public class FilterableGridSample : System.Windows.Forms.Form
	{
		private SAN.UI.DataGridView.FilterableDataGrid _grid;
		private System.ComponentModel.Container components = null;
		private bool _listMode;

		private string[] _savedFilters;

		public FilterableGridSample(bool listMode)
		{
			InitializeComponent();
            _listMode = listMode;
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_listMode)
                _grid.DataSource = new SAN.UI.DataGridView.BindingListView<Order>(DataHelper.SampleList);
            else
							_grid.DataSource = DataHelper.SampleData.Tables["tblKunden"].DefaultView;

            _grid.EmbeddedDataGridView.ReadOnly = true;
            _grid.EmbeddedDataGridView.AllowUserToOrderColumns = true;
						_grid.FilterBoxPosition = FilterPosition.Top;
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
			SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory defaultGridFilterFactory1 = new SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory();
			this._grid = new SAN.UI.DataGridView.FilterableDataGrid();
			this.SuspendLayout();
			// 
			// _grid
			// 
			this._grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._grid.ConsoleErrorMode = ((SAN.UI.DataGridView.FilterErrorModes)(((SAN.UI.DataGridView.FilterErrorModes.General | SAN.UI.DataGridView.FilterErrorModes.ExceptionMessage) 
            | SAN.UI.DataGridView.FilterErrorModes.StackTrace)));
			defaultGridFilterFactory1.CreateDistinctGridFilters = false;
			defaultGridFilterFactory1.DefaultGridFilterType = typeof(SAN.UI.DataGridView.GridFilters.TextGridFilter);
			defaultGridFilterFactory1.DefaultShowDateInBetweenOperator = false;
			defaultGridFilterFactory1.DefaultShowNumericInBetweenOperator = false;
			defaultGridFilterFactory1.HandleEnumerationTypes = true;
			defaultGridFilterFactory1.MaximumDistinctValues = 20;
			this._grid.FilterFactory = defaultGridFilterFactory1;
			this._grid.Location = new System.Drawing.Point(8, 2);
			this._grid.MessageErrorMode = ((SAN.UI.DataGridView.FilterErrorModes)(((SAN.UI.DataGridView.FilterErrorModes.General | SAN.UI.DataGridView.FilterErrorModes.ExceptionMessage) 
            | SAN.UI.DataGridView.FilterErrorModes.StackTrace)));
			this._grid.Name = "_grid";
			this._grid.Size = new System.Drawing.Size(784, 559);
			this._grid.TabIndex = 1000;
			// 
			// FilterableGridSample
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(800, 573);
			this.Controls.Add(this._grid);
			this.Name = "FilterableGridSample";
			this.Text = "Sample 1 - FilterableGrid functionalities";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
