using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FilterableTestApp
{
	public class ExtenderSample : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGridView _grid;
		private SAN.UI.DataGridView.DataGridFilterExtender _extender;
        private System.ComponentModel.IContainer components;
        private BindingSource _source;

		public ExtenderSample()
		{
			InitializeComponent();
            _source = new BindingSource();
            (_extender.FilterFactory as SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory).CreateDistinctGridFilters = true;
            _grid.DataSource = _source;
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //_source.DataSource = DataHelper.SampleData.Tables[1].DefaultView;
            //_source.DataSource = DataHelper.SampleData.Tables[1];
            _source.DataSource = DataHelper.SampleData;
            _source.DataMember = "Orders";
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
            this.components = new System.ComponentModel.Container();
            SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory defaultGridFilterFactory2 = new SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory();
            this._grid = new System.Windows.Forms.DataGridView();
            this._extender = new SAN.UI.DataGridView.DataGridFilterExtender(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._extender)).BeginInit();
            this.SuspendLayout();
            // 
            // _grid
            // 
            this._grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._grid.Location = new System.Drawing.Point(8, 26);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(584, 310);
            this._grid.TabIndex = 0;
            // 
            // _extender
            // 
            this._extender.DataGridView = this._grid;
            defaultGridFilterFactory2.CreateDistinctGridFilters = false;
            defaultGridFilterFactory2.DefaultGridFilterType = typeof(SAN.UI.DataGridView.GridFilters.TextGridFilter);
            defaultGridFilterFactory2.DefaultShowDateInBetweenOperator = false;
            defaultGridFilterFactory2.DefaultShowNumericInBetweenOperator = false;
            defaultGridFilterFactory2.HandleEnumerationTypes = true;
            defaultGridFilterFactory2.MaximumDistinctValues = 20;
            this._extender.FilterFactory = defaultGridFilterFactory2;
            // 
            // ExtenderSample
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(600, 341);
            this.Controls.Add(this._grid);
            this.Name = "ExtenderSample";
            this.Text = "Sample 2 - Filtering an ExtendedDataGrid with the DataGridFilterExtender ";
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._extender)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
