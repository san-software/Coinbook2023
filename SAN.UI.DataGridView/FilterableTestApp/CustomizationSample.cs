using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using SAN.UI.DataGridView.GridFilters;
using SAN.UI.DataGridView.GridFilters.EnumerationSources;

namespace FilterableTestApp
{
	public class CustomizationSample : System.Windows.Forms.Form
	{
		private class MappingColumnStyle : DataGridTextBoxColumn
		{
			protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
			{
				g.FillRectangle(Brushes.White, bounds);
				int value = (int)base.GetColumnValueAtRow(source, rowNum);
				string text = Mappings[value];
				base.PaintText(g, bounds, text, backBrush, foreBrush, alignToRight);
			}
		}

		internal static string[] Mappings = new string[] { "Opteron", "Pentium", "Athlon", "Itanium", "Celeron" };

		private System.Windows.Forms.DataGridView _grid;
		private SAN.UI.DataGridView.DataGridFilterExtender _extender;
		private SAN.UI.DataGridView.GridFilters.NumericGridFilterControl _ngfDoubleColumn;
		private SAN.UI.DataGridView.GridFilters.DateGridFilterControl _dgfDateColumn;
		private System.Windows.Forms.TextBox _tbTextColumn;
		private System.Windows.Forms.CheckBox _cbBoolColumn;
		private System.Windows.Forms.ComboBox _cmbEnumColumn;
		private System.Windows.Forms.ComboBox _cmbMappingColumn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label _lblInfo;
		private System.ComponentModel.IContainer components;

		public CustomizationSample()
		{
			InitializeComponent();
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _extender.FilterFactory.GridFilterCreated += new SAN.UI.DataGridView.GridFilterEventHandler(OnGridFilterCreated);
            _grid.DataSource = this.CreateSampleData().DefaultView;
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
            SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory defaultGridFilterFactory1 = new SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory();
            this._grid = new System.Windows.Forms.DataGridView();
            this._extender = new SAN.UI.DataGridView.DataGridFilterExtender(this.components);
            this._ngfDoubleColumn = new SAN.UI.DataGridView.GridFilters.NumericGridFilterControl();
            this._dgfDateColumn = new SAN.UI.DataGridView.GridFilters.DateGridFilterControl();
            this._tbTextColumn = new System.Windows.Forms.TextBox();
            this._cbBoolColumn = new System.Windows.Forms.CheckBox();
            this._cmbEnumColumn = new System.Windows.Forms.ComboBox();
            this._cmbMappingColumn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._extender)).BeginInit();
            this.SuspendLayout();
            // 
            // _grid
            // 
            this._grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._grid.Location = new System.Drawing.Point(8, 75);
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.Size = new System.Drawing.Size(576, 301);
            this._grid.TabIndex = 0;
            // 
            // _extender
            // 
            this._extender.DataGridView = this._grid;
            defaultGridFilterFactory1.CreateDistinctGridFilters = false;
            defaultGridFilterFactory1.DefaultGridFilterType = typeof(SAN.UI.DataGridView.GridFilters.TextGridFilter);
            defaultGridFilterFactory1.HandleEnumerationTypes = true;
            defaultGridFilterFactory1.MaximumDistinctValues = 20;
            this._extender.FilterFactory = defaultGridFilterFactory1;
            // 
            // _ngfDoubleColumn
            // 
            this._ngfDoubleColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ngfDoubleColumn.Location = new System.Drawing.Point(568, 408);
            this._ngfDoubleColumn.Name = "_ngfDoubleColumn";
            this._ngfDoubleColumn.Size = new System.Drawing.Size(152, 24);
            this._ngfDoubleColumn.TabIndex = 1;
            // 
            // _dgfDateColumn
            // 
            this._dgfDateColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._dgfDateColumn.Location = new System.Drawing.Point(8, 400);
            this._dgfDateColumn.Name = "_dgfDateColumn";
            this._dgfDateColumn.Size = new System.Drawing.Size(152, 24);
            this._dgfDateColumn.TabIndex = 2;
            // 
            // _tbTextColumn
            // 
            this._tbTextColumn.Location = new System.Drawing.Point(8, 32);
            this._tbTextColumn.Name = "_tbTextColumn";
            this._tbTextColumn.Size = new System.Drawing.Size(136, 20);
            this._tbTextColumn.TabIndex = 3;
            // 
            // _cbBoolColumn
            // 
            this._cbBoolColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cbBoolColumn.Location = new System.Drawing.Point(608, 8);
            this._cbBoolColumn.Name = "_cbBoolColumn";
            this._cbBoolColumn.Size = new System.Drawing.Size(112, 16);
            this._cbBoolColumn.TabIndex = 4;
            this._cbBoolColumn.Text = "BoolColumn filter";
            // 
            // _cmbEnumColumn
            // 
            this._cmbEnumColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmbEnumColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbEnumColumn.Location = new System.Drawing.Point(600, 128);
            this._cmbEnumColumn.Name = "_cmbEnumColumn";
            this._cmbEnumColumn.Size = new System.Drawing.Size(121, 21);
            this._cmbEnumColumn.TabIndex = 5;
            // 
            // _cmbMappingColumn
            // 
            this._cmbMappingColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmbMappingColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbMappingColumn.Location = new System.Drawing.Point(600, 216);
            this._cmbMappingColumn.Name = "_cmbMappingColumn";
            this._cmbMappingColumn.Size = new System.Drawing.Size(121, 21);
            this._cmbMappingColumn.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "TextColumn filter:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(600, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "EnumColumn filter:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(600, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "MappingColumn filter:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(8, 384);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "DateColumn filter:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(568, 392);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "DoubleColumn filter:";
            // 
            // _lblInfo
            // 
            this._lblInfo.Location = new System.Drawing.Point(168, 8);
            this._lblInfo.Name = "_lblInfo";
            this._lblInfo.Size = new System.Drawing.Size(384, 32);
            this._lblInfo.TabIndex = 9;
            this._lblInfo.Text = "Noone would actually arrange filters that way but this sample should just demonst" +
                "rate that you are really totally free in arranging them.";
            // 
            // CustomizationSample
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(728, 437);
            this.Controls.Add(this._lblInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cmbMappingColumn);
            this.Controls.Add(this._cmbEnumColumn);
            this.Controls.Add(this._cbBoolColumn);
            this.Controls.Add(this._tbTextColumn);
            this.Controls.Add(this._dgfDateColumn);
            this.Controls.Add(this._ngfDoubleColumn);
            this.Controls.Add(this._grid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Name = "CustomizationSample";
            this.Text = "Sample 5 - Extreme customizationing";
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._extender)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private DataTable CreateSampleData()
		{
			DataTable result = new DataTable("Sample");

			result.Columns.Add("TextColumn", typeof(string));
			result.Columns.Add("DoubleColumn", typeof(double));
			result.Columns.Add("DateColumn", typeof(DateTime));
			result.Columns.Add("BoolColumn", typeof(bool));
			result.Columns.Add("EnumColumn", typeof(SampleEnum));
			result.Columns.Add("MappingColumn", typeof(int));
			result.Columns.Add("TextColumn2", typeof(string));
			result.Columns.Add("TextColumn3", typeof(string));

			string[] texts = new string[] { "CodeProject", "is", "one", "of", "the", 
											  "best", "sites", "you", "will", "find", "on", "the", "internet" };

			for (int i = 0; i < 100; i++)
			{
				result.Rows.Add(new object[] { texts[i % texts.Length], i * 3.543, 
												DateTime.Now + new TimeSpan(i * 7, 0, 0), 
												i % 2 == 0, (SampleEnum)(i % 3), i % 5,
												texts[(i * 2 + 2) % (texts.Length - 1)], texts[(i * 3 + 5) % (texts.Length - 2)]});
			}

			return result;
		}

		private void OnGridFilterCreated(object sender, SAN.UI.DataGridView.GridFilterEventArgs args)
		{
			switch (args.ColumnName)
			{
				case "TextColumn":
					args.GridFilter = new TextGridFilter(_tbTextColumn);
					break;
				case "DoubleColumn":
					args.GridFilter = new NumericGridFilter(_ngfDoubleColumn);
					break;
				case "DateColumn":
					args.GridFilter = new DateGridFilter(_dgfDateColumn);
					break;
				case "BoolColumn":
					args.GridFilter = new BoolGridFilter(_cbBoolColumn);
					break;
				case "EnumColumn":
					args.GridFilter = new EnumerationGridFilter(new TypeEnumerationSource(typeof(SampleEnum)), _cmbEnumColumn);
					break;
				case "MappingColumn":
					args.GridFilter = new EnumerationGridFilter(new IntStringMapEnumerationSource(new int[] { 0, 1, 2, 3, 4}, Mappings), _cmbMappingColumn);
					break;
			}
		}
	}
}
