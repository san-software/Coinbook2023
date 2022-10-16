using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Specialized;

namespace SAN.UI.DataGridView
{
	/// <summary>
	/// Control which embeds an <see cref="DataGridView"/> and a
	/// <see cref="DataGridFilterExtender"/> for providing automatic 
	/// filtering on all visible columns.
	/// </summary>
	public class DataGridViewEx : System.Windows.Forms.UserControl
	{
		#region Events

		/// <summary>
		/// Event, which gets fired whenever the filter criteria has been changed.
		/// </summary>
		public event EventHandler AfterFiltersChanged;

		/// <summary>
		/// Event, which gets fired whenever the filter criteria are going to be changed.
		/// </summary>
		public event EventHandler BeforeFiltersChanging;

		/// <summary>
		/// Event, which gets fired whenever an <see cref="IGridFilter"/> has been bound
		/// and thus added to this instance.
		/// </summary>
		public event GridFilterEventHandler GridFilterBound;

		/// <summary>
		/// Event, which gets fired whenever an <see cref="IGridFilter"/> has been unbound
		/// and thus removed to this instance.
		/// </summary>
		public event GridFilterEventHandler GridFilterUnbound;

		public event DataGridViewCellEventHandler RowEnter;
		public event DataGridViewCellEventHandler RowLeave;
		public event DataGridViewCellEventHandler ButtonClick;
		public event DataGridViewCellButtonClickEventHandler ColumnButtonClicked;
		public event EventHandler ButtonNewClicked;
		public event DataGridViewCellEventHandler CellEndEdit;
		public event DataGridViewRowEventHandler UserDeletedRow;
		public event DataGridViewRowCancelEventHandler UserDeletingRow;
		public event DataGridViewRowsRemovedEventHandler RowsRemoved;
    public event DataGridViewCellEventHandler CellDoubleClick;

    public event DataGridViewCellMouseEventHandler CellMouseClick;
    public event DataGridViewCellPaintingEventHandler CellPainting;
    public event DataGridViewCellToolTipTextNeededEventHandler CellToolTipTextNeeded;

    #endregion

		#region Fields

		private System.Windows.Forms.DataGridView grid;
		private SAN.UI.DataGridView.DataGridFilterExtender extender;
		private System.ComponentModel.IContainer components;
		private ToolStrip toolStrip1;
		private ToolStripButton btnNew;
		private ToolStripButton btnFilter;
    private PictureBox picBild;
		private Boolean toolStrip1Visible = true;
    private Boolean bufferShow = false;
		#endregion


		#region Constructors

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public DataGridViewEx()
		{
			InitializeComponent();
			grid.RowTemplate.Height = 18;

			RepositionGrid();

      grid.CellMouseClick += new DataGridViewCellMouseEventHandler(grid_CellMouseClick);
      grid.CellPainting += new DataGridViewCellPaintingEventHandler(grid_CellPainting);
      grid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(grid_CellToolTipTextNeeded);

      picBild.Dock = DockStyle.Fill;
      picBild.Visible = false;
      picBild.SendToBack();
		}

    void grid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
    {
      if (CellToolTipTextNeeded != null)
        CellToolTipTextNeeded(sender, e);
    }

    void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      if (CellPainting != null)
        CellPainting(sender, e);
    }

    void grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (CellMouseClick != null)
        CellMouseClick(sender, e);
    }

		#endregion

		#region Designer generated code

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory defaultGridFilterFactory1 = new SAN.UI.DataGridView.GridFilterFactories.DefaultGridFilterFactory();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridViewEx));
      this.grid = new System.Windows.Forms.DataGridView();
      this.extender = new SAN.UI.DataGridView.DataGridFilterExtender(this.components);
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnNew = new System.Windows.Forms.ToolStripButton();
      this.btnFilter = new System.Windows.Forms.ToolStripButton();
      this.picBild = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.extender)).BeginInit();
      this.toolStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picBild)).BeginInit();
      this.SuspendLayout();
      // 
      // grid
      // 
      this.grid.Location = new System.Drawing.Point(0, 24);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(496, 352);
      this.grid.TabIndex = 0;
      this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellDoubleClick);
      this.grid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellEndEdit);
      this.grid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grid_DataError);
      this.grid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_RowEnter);
      this.grid.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_RowLeave);
      this.grid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.OnRowsRemoved);
      this.grid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.OnUserDeletedRow);
      this.grid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.OnUserDeletingRow);
      this.grid.DoubleClick += new System.EventHandler(this.OnDoubleClick);
      this.grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
      this.grid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
      this.grid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
      this.grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
      this.grid.MouseEnter += new System.EventHandler(this.OnMouseEnter);
      this.grid.MouseLeave += new System.EventHandler(this.OnMouseLeave);
      this.grid.MouseHover += new System.EventHandler(this.OnMouseHover);
      this.grid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
      this.grid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
      this.grid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
      // 
      // extender
      // 
      this.extender.DataGridView = this.grid;
      this.extender.FilterBoxPosition = SAN.UI.DataGridView.FilterPosition.Off;
      defaultGridFilterFactory1.CreateDistinctGridFilters = false;
      defaultGridFilterFactory1.DefaultGridFilterType = typeof(SAN.UI.DataGridView.GridFilters.TextGridFilter);
      defaultGridFilterFactory1.DefaultShowDateInBetweenOperator = false;
      defaultGridFilterFactory1.DefaultShowNumericInBetweenOperator = false;
      defaultGridFilterFactory1.HandleEnumerationTypes = true;
      defaultGridFilterFactory1.MaximumDistinctValues = 20;
      this.extender.FilterFactory = defaultGridFilterFactory1;
      this.extender.FilterTextVisible = false;
      this.extender.AfterFiltersChanged += new System.EventHandler(this.OnAfterFiltersChanged);
      this.extender.BeforeFiltersChanging += new System.EventHandler(this.OnBeforeFiltersChanging);
      this.extender.GridFilterBound += new SAN.UI.DataGridView.GridFilterEventHandler(this.OnGridFilterBound);
      this.extender.GridFilterUnbound += new SAN.UI.DataGridView.GridFilterEventHandler(this.OnGridFilterUnbound);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnFilter});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(496, 25);
      this.toolStrip1.TabIndex = 4;
      this.toolStrip1.Text = "toolStrip1";
      this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
      // 
      // btnNew
      // 
      this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
      this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(23, 22);
      this.btnNew.ToolTipText = "Neue Zeile anlegen";
      // 
      // btnFilter
      // 
      this.btnFilter.Checked = true;
      this.btnFilter.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
      this.btnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFilter.Name = "btnFilter";
      this.btnFilter.Size = new System.Drawing.Size(23, 22);
      this.btnFilter.ToolTipText = "Filter ein/ausblenden";
      // 
      // picBild
      // 
      this.picBild.BackColor = System.Drawing.Color.Red;
      this.picBild.Location = new System.Drawing.Point(276, 54);
      this.picBild.Name = "picBild";
      this.picBild.Size = new System.Drawing.Size(130, 73);
      this.picBild.TabIndex = 6;
      this.picBild.TabStop = false;
      // 
      // DataGridViewEx
      // 
      this.Controls.Add(this.picBild);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.grid);
      this.Name = "DataGridViewEx";
      this.Size = new System.Drawing.Size(496, 376);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.extender)).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picBild)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		#region Public interface

		/// <summary>
		/// Gets and sets whether the filter criteria is automatically refreshed when
		/// changes are made to the filter controls. If set to false then a call to
		/// <see cref="RefreshFilters"/> is needed to manually refresh the criteria.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(RefreshMode.OnInput)]
		[Description("Specifies if the view automatically refreshes to reflect changes in the grid filter controls.")]
		public RefreshMode AutoRefreshMode
		{
			get { return extender.AutoRefreshMode; }
			set { extender.AutoRefreshMode = value; }
		}

		/// <summary>
		/// Gets and sets whether filters are kept while switching between different tables.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(false)]
		[Description("Specifies whether filters are kept while switching between different tables.")]
		public bool KeepFilters
		{
			get { return extender.KeepFilters; }
			set { extender.KeepFilters = value; }
		}

		/// <summary>
		/// Publishes the embedded <see cref="DataGridView"/> to allow
		/// full control over its settings.
		/// </summary>
		[Browsable(false)]
		public System.Windows.Forms.DataGridView EmbeddedDataGridView
		{
			get { return grid; }
		}

		/// <summary>
		/// Gets and sets the poisiton of the filter GUI elements.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(FilterPosition.Top)]
		[Description("Gets and sets the position of the filter GUI elements.")]
		public FilterPosition FilterBoxPosition
		{
			get { return extender.FilterBoxPosition; }
			set
			{
				extender.FilterBoxPosition = value;
				RepositionGrid();
			}
		}

		///// <summary>
		///// Gets and sets the text for the filter label.
		///// </summary>
		//[Browsable(true), DefaultValue("Filter")]
		//[Description("Gets and sets the text for the filter label.")]
		//public string FilterText 
		//{
		//  get { return extender.FilterText; }
		//  set { extender.FilterText = value; }
		//}

		/// <summary>
		/// Gets and sets the <see cref="IGridFilterFactory"/> used to generate the filter GUI.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(null)]
		[Description("Gets and sets factory instance which should be used to create grid filters.")]
		public IGridFilterFactory FilterFactory
		{
			get { return extender.FilterFactory; }
			set { extender.FilterFactory = value; }
		}

		///// <summary>
		///// Gets and sets whether the filter label should be visible.
		///// </summary>
		//[Browsable(true), DefaultValue(true)]
		//[Description("Gets and sets whether the filter label should be visible.")]
		//public bool FilterTextVisible 
		//{
		//  get { return extender.FilterTextVisible; }
		//  set { extender.FilterTextVisible = value; }
		//}

		/// <summary>
		/// The selected operator to combine the filter criterias.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(LogicalOperators.And)]
		[Description("The selected operator to combine the filter criterias.")]
		public LogicalOperators Operator
		{
			get { return extender.Operator; }
			set { extender.Operator = value; }
		}

		///// <summary>
		///// Gets and sets the <see cref="IBindingListView"/> which should be displayed in the grid.
		///// This is needed because only <see cref="IBindingListView"/>s provide in built mechanisms
		///// to filter their content.
		///// </summary>
		//[Browsable(true), DefaultValue(null)]
		//[Description("The IBindingListView which should be initially displayed.")]
		//public IBindingListView DataSource
		//{
		//  get { return grid.DataSource as IBindingListView; }
		//  set
		//  {
		//    extender.BeginInit();
		//    grid.DataSource = value;
		//    extender.EndInit();
		//  }
		//}

		/// <summary>
		/// Gets and sets the <see cref="IBindingListView"/> which should be displayed in the grid.
		/// This is needed because only <see cref="IBindingListView"/>s provide in built mechanisms
		/// to filter their content.
		/// </summary>
		[Browsable(true), DefaultValue(null)]
		[Description("The IBindingListView which should be initially displayed.")]
		public BindingSource DataSource
		{
			get { return grid.DataSource as BindingSource; }
			set
			{
				extender.BeginInit();
				grid.DataSource = value;
				extender.EndInit();
			}
		}

		/// <summary>
		/// Gets and sets what information is shown to the user
		/// if an error in the builded filter criterias occurs.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(FilterErrorModes.General)]
		[Description("Specifies what information is shown to the user if an error in the builded filter criterias occurs.")]
		public FilterErrorModes MessageErrorMode
		{
			get { return extender.MessageErrorMode; }
			set { extender.MessageErrorMode = value; }
		}

		/// <summary>
		/// Gets and sets what information is printed to the console
		/// if an error in the builded filter criterias occurs.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(FilterErrorModes.Off)]
		[Description("Specifies what information is printed to the console if an error in the builded filter criterias occurs.")]
		public FilterErrorModes ConsoleErrorMode
		{
			get { return extender.ConsoleErrorMode; }
			set { extender.ConsoleErrorMode = value; }
		}

		/// <summary>
		/// Gets a modifyable collection which maps <see cref="DataTable.TableName"/>s
		/// to base filter strings which are applied in front of the automatically
		/// created filter.
		/// </summary>
		/// <remarks>
		/// The grid contents is not automatically refreshed when modifying this 
		/// collection. A call to <see cref="RefreshFilters"/> is needed for this.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public StringDictionary BaseFilters
		{
			get { return extender.BaseFilters; }
		}

		/// <summary>
		/// Gets or sets which operator should be used to combine the base filter
		/// with the automatically created filters.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(LogicalOperators.And)]
		[Description("Operator which should be used to combine the base filter with the automatically created filters.")]
		public LogicalOperators BaseFilterOperator
		{
			get { return extender.BaseFilterOperator; }
			set { extender.BaseFilterOperator = value; }
		}

		/// <summary>
		/// Gets or sets whether base filters should be used when refreshing
		/// the filter criteria. Setting it to false will disable the functionality
		/// while still keeping the base filter strings in the <see cref="BaseFilters"/>
		/// collection intact.
		/// </summary>
		[Category("Filter")]
		[Browsable(true), DefaultValue(true)]
		[Description("Gets or sets whether base filters should be used when refreshing the filter criteria.")]
		public bool BaseFilterEnabled
		{
			get { return extender.BaseFilterEnabled; }
			set { extender.BaseFilterEnabled = value; }
		}

		/// <summary>
		/// Gets or sets the currently used base filter. Internally it adjusts the
		/// <see cref="BaseFilters"/> collection with the given value and the current
		/// <see cref="DataTable.TableName"/> and also initiates a refresh.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string CurrentTableBaseFilter
		{
			get { return extender.CurrentTableBaseFilter; }
			set { extender.CurrentTableBaseFilter = value; }
		}

		/// <summary>
		/// Gets all currently set <see cref="IGridFilter"/>s.
		/// </summary>
		/// <returns>Collection of <see cref="IGridFilter"/>s.</returns>
		public GridFilterCollection GetGridFilters()
		{
			return extender.GetGridFilters();
		}

		/// <summary>
		/// Clears all filters to initial state.
		/// </summary>
		public void ClearFilters()
		{
			extender.ClearFilters();
		}

		/// <summary>
		/// Gets all filters currently set
		/// </summary>
		/// <returns></returns>
		public string[] GetFilters()
		{
			return extender.GetFilters();
		}

		/// <summary>
		/// Sets all filters to the specified values.
		/// The values must be in order of the column styles in the current view.
		/// This function should normally be used with data previously coming
		/// from the <see cref="GetFilters"/> function.
		/// </summary>
		/// <param name="filters">filters to set</param>
		public void SetFilters(string[] filters)
		{
			extender.SetFilters(filters);
		}

		/// <summary>
		/// Refreshes the filter criteria to match the current contents of the associated
		/// filter controls.
		/// </summary>
		public void RefreshFilters()
		{
			extender.RefreshFilters();
		}
		#endregion

		#region Privates

		private void RepositionGrid()
		{
			int top = grid.Top;
			int height = grid.Height;
			int newLeft = 0;
			int newWidth = this.Width;

			switch (extender.FilterBoxPosition)
			{
				case FilterPosition.Off:

					if (!toolStrip1Visible)
					{
						top = 0;
						height = this.Height;
					}
					else
					{
						top = toolStrip1.Height + 1;
						height = this.Height - top - 1;
					}
					break;

				case FilterPosition.Top:
					if (!toolStrip1Visible)
					{
						top = toolStrip1.Height + 1;
						//top = extender.NeededControlHeight + 1;
						height = this.Height - top - 1;
					}
					else
					{
						top = toolStrip1.Height + 1;
						//top = extender.NeededControlHeight + toolStrip1.Height + 1;
						height = this.Height - top - 1;
					}
					break;

				case FilterPosition.Bottom:
					if (!toolStrip1Visible)
					{
						top = 0;
						height = this.Height - extender.NeededControlHeight - 1;
					}
					else
					{
						top = toolStrip1.Height + 1;
						height = this.Height - extender.NeededControlHeight - toolStrip1.Height - 2;
					}
					break;
			}

			grid.SetBounds(newLeft, top, newWidth, height, BoundsSpecified.All);
		}

		private void OnAfterFiltersChanged(object sender, EventArgs e)
		{
			OnAfterFiltersChanged(e);
		}

		private void OnBeforeFiltersChanging(object sender, EventArgs e)
		{
			OnBeforeFiltersChanging(e);
		}

		private void OnGridFilterBound(object sender, GridFilterEventArgs e)
		{
			OnGridFilterBound(e);
		}

		private void OnGridFilterUnbound(object sender, GridFilterEventArgs e)
		{
			OnGridFilterUnbound(e);
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		private void OnMouseEnter(object sender, EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		private void OnMouseHover(object sender, EventArgs e)
		{
			base.OnMouseHover(e);
		}

		private void OnMouseLeave(object sender, EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			base.OnMouseMove(e);
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			base.OnMouseUp(e);
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
		}

		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			base.OnKeyUp(e);
		}

		private void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			base.OnPreviewKeyDown(e);
		}

		private void OnDoubleClick(object sender, EventArgs e)
		{
			base.OnDoubleClick(e);
		}

		#endregion

		#region Protected interface

		/// <summary>
		/// Raises the <see cref="BeforeFiltersChanging"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected virtual void OnBeforeFiltersChanging(EventArgs e)
		{
			if (BeforeFiltersChanging != null)
				BeforeFiltersChanging(this, e);
		}

		/// <summary>
		/// Raises the <see cref="AfterFiltersChanged"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected virtual void OnAfterFiltersChanged(EventArgs e)
		{
			if (AfterFiltersChanged != null)
				AfterFiltersChanged(this, e);
		}

		/// <summary>
		/// Raises the <see cref="GridFilterBound"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected virtual void OnGridFilterBound(GridFilterEventArgs e)
		{
			if (GridFilterBound != null)
				GridFilterBound(this, e);
		}

		/// <summary>
		/// Raises the <see cref="GridFilterUnbound"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected virtual void OnGridFilterUnbound(GridFilterEventArgs e)
		{
			if (GridFilterUnbound != null)
				GridFilterUnbound(this, e);
		}

		#endregion

		#region Overriden from UserControl

		/// <summary> 
		/// Cleans up.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}

			try
			{
				base.Dispose(disposing);
			}
			catch { }

		}

		/// <summary>
		/// Repositions the grid to match the new size
		/// </summary>
		/// <param name="e">event arguments</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			RepositionGrid();
		}

		#endregion

		[Category("Action")]
		private void grid_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (RowEnter != null)
				RowEnter(sender, e);
		}

		[Category("Action")]
		private void grid_RowLeave(object sender, DataGridViewCellEventArgs e)
		{
			if (RowLeave != null)
				RowLeave(sender, e);
		}

		#region ButtonColumn
		/// <summary>
		/// Erzeugt eine ButtonColumn mit Icons für das Grid
		/// </summary>
		public void AddButtonColumn(string columnName, Image image, Image disabled)
		{
			DataGridViewButtonColumnEx temp = new DataGridViewButtonColumnEx(columnName, grid);

			temp.Name = columnName;
			temp.MinimumWidth = 30;
			temp.ReadOnly = true;
			temp.Resizable = DataGridViewTriState.False;
			temp.SortMode = DataGridViewColumnSortMode.NotSortable;
			temp.UseColumnTextForButtonValue = true;
			temp.Text = "";
			temp.ButtonImage = image;
			temp.DisabledImage = disabled;
			temp.Parent = this;

			grid.Columns.Add(temp);
		}

		/// <summary>
		/// Erzeugt eine ButtonColumn mit Icons für das Grid
		/// </summary>
		public void AddButtonColumn(string columnName, Image image)
		{
			AddButtonColumn(columnName, image, null);
		}

		/// <summary>
		/// Erzeugt eine ButtonColumn mit Textbeschreibung für das Grid
		/// </summary>
		public void AddButtonColumn(string columnName)
		{
			AddButtonColumn(columnName, null, null);
		}

		public void OnColumnButtonClicked(DataGridViewCellButtonClickEventArgs e)
		{
			if (ColumnButtonClicked != null)
				ColumnButtonClicked(this, e);
		}
		#endregion

		#region BomboboxColumn
		/// <summary>
		/// Hängt eine <cref ="DataGridViewComboBoxColumn"/> an die Column-Collection
		/// </summary>
		public void AddComboboxColumn(string name, object dataSource, string valueMember, string displayMember)
		{
			grid.Columns.Add(createComboBox(name, dataSource, valueMember, displayMember));
		}

		/// <summary>
		/// Ersetzt eine Spalte durch eine <cref ="DataGridViewComboBoxColumn"/> 
		/// </summary>
		public void SetComboboxColumn(string name, object dataSource, string valueMember, string displayMember)
		{
			int id = grid.Columns[name].Index;
			grid.Columns.Remove(name);
			grid.Columns.Insert(id, createComboBox(name, dataSource, valueMember, displayMember));
		}

		/// <summary>
		/// Erzeugt eine <cref ="DataGridViewComboBoxColumn"/> 
		/// </summary>
		private DataGridViewComboBoxColumn createComboBox(string name, object dataSource, string valueMember, string displayMember)
		{
			DataGridViewComboBoxColumn comboboxColumn = new DataGridViewComboBoxColumn();

			comboboxColumn.DataPropertyName = name;
			comboboxColumn.HeaderText = name;
			comboboxColumn.DropDownWidth = 160;
			comboboxColumn.Width = 90;
			comboboxColumn.MaxDropDownItems = 3;
			comboboxColumn.FlatStyle = FlatStyle.Flat;
			comboboxColumn.Name = name;

			comboboxColumn.DataSource = dataSource;
			comboboxColumn.ValueMember = valueMember;
			comboboxColumn.DisplayMember = displayMember;

			return comboboxColumn;
		}
		#endregion

		public Boolean ToolBarVisible
		{
			get
			{
				return toolStrip1Visible;
			}
			set
			{
				toolStrip1.Visible = value;
				toolStrip1Visible = value;
				RepositionGrid();
			}
		}

		public Boolean ButtonVisible()
		{
			return true;
		}

		public DataGridViewRow RowTemplate
		{
			get
			{
				return grid.RowTemplate;
			}
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			switch (e.ClickedItem.Name)
			{
				case "btnNew":
					DataSource.AddNew();
					DataSource.ResetBindings(false);
					DataSource.MoveLast();
					break;

				case "btnFilter":
					if (FilterBoxPosition == FilterPosition.Off)
						FilterBoxPosition = FilterPosition.Top;
					else
						FilterBoxPosition = FilterPosition.Off;

					break;
			}
		}

		private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		public int Find(string column, string id)
		{
			int result = -1;

			for (int i = 0; i < grid.RowCount; i++)
				if (grid[column, i].Value.ToString() == id)
				{
					result = i;
					break;
				}

			return result;
		}

		public DataGridViewAutoSizeRowsMode AutoSizeRowsMode
		{
			get
			{
				return grid.AutoSizeRowsMode;
			}
			set
			{
				grid.AutoSizeRowsMode = value;

			}
		}

		public DataGridViewColumnCollection Columns
		{
			get
			{
				return grid.Columns;
			}
		}

		[Browsable(false)]
		public int RowCount
		{
			get
			{
				return grid.RowCount;
			}
		}

		public DataGridViewRowCollection Rows
		{
			get
			{
				return grid.Rows;
			}
		}

		public Boolean MultiSelect
		{
			get
			{
				return grid.MultiSelect;
			}
			set
			{
				grid.MultiSelect = value;
			}
		}

		public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
		{
			get
			{
				return grid.ColumnHeadersHeightSizeMode;
			}
			set
			{
				grid.ColumnHeadersHeightSizeMode = value;
			}
		}

		public DataGridViewSelectionMode SelectionMode
		{
			get
			{
				return grid.SelectionMode;
			}
			set
			{
				grid.SelectionMode = value;
			}
		}

		public object Value(int row, int column)
		{
			return grid.Rows[row].Cells[column].Value;
		}


		public bool AllowUserToAddRows
		{
			get
			{
				return grid.AllowUserToAddRows;
			}
			set
			{
				grid.AllowUserToAddRows = value;
			}
		}

		[Browsable(false)]
		public int CurrentRow
		{
			get
			{
				int result = -1;

				if (grid.CurrentCell != null)
					result = grid.CurrentCell.RowIndex;

				return result;
			}

			set
			{
				if (value >= 0 && value < grid.RowCount)
				{
					grid.CurrentCell = grid.Rows[value].Cells[1];
					grid.Rows[value].Selected = true;
				}
			}
		}

		[Category("Verhalten")]
		public bool Readonly
		{
			get
			{
				return grid.ReadOnly;
			}

			set
			{
				grid.ReadOnly = value;
			}
		}

		private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (CellEndEdit != null)
				CellEndEdit(sender, e);
		}

		private void OnUserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			if (UserDeletedRow != null)
				UserDeletedRow(sender,e);
		}

		private void OnUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if (UserDeletingRow != null)
				UserDeletingRow(sender, e);
		}

		private void OnRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (RowsRemoved != null)
				RowsRemoved(sender, e);
		}

		private void OnCellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (CellDoubleClick != null)
				CellDoubleClick(sender, e);
		}

    public DataGridViewCell CurrentCell
    {
      get 
      {
        return grid.CurrentCell;
      }
      set
      {
        grid.CurrentCell = value;
      }
    }

    public Boolean AllowUserToDeleteRows
    {
      get
      {
        return grid.AllowUserToDeleteRows;
      }
      set
      {
        grid.AllowUserToDeleteRows = value;
      }
    }

    public Boolean AllowUserToResizeRows
    {
      get
      {
        return grid.AllowUserToResizeRows;
      }
      set
      {
        grid.AllowUserToResizeRows = value;
      }
    }

    public Boolean ReadOnly
    {
      get
      {
        return grid.ReadOnly;
      }
      set
      {
        grid.ReadOnly = value;
      }
    }

    public Boolean ShowEditingIcon
    {
      get
      {
        return grid.ShowEditingIcon;
      }
      set
      {
        grid.ShowEditingIcon = value;
      }
    }

    public Boolean RowHeadersVisible
    {
      get
      {
        return grid.RowHeadersVisible;
      }
      set
      {
        grid.RowHeadersVisible = value;
      }
    }

    public Color BackgroundColor
    {
      get
      {
        return grid.BackgroundColor;
      }
      set
      {
        grid.BackgroundColor = value;
      }
    }

    public Color GridColor
    {
      get
      {
        return grid.GridColor;
      }
      set
      {
        grid.GridColor = value;
      }
    }

    public DataGridViewSelectedRowCollection SelectedRows
    {
      get
      {
        return grid.SelectedRows;
      }
    }

    public DataGridViewCellStyle ColumnHeadersDefaultCellStyle
    {
      get
      {
        return grid.ColumnHeadersDefaultCellStyle;
      }
    }

    public int FirstDisplayedScrollingRowIndex
    {
      get
      {
        return grid.FirstDisplayedScrollingRowIndex;
      }
      set
      {
        if (value != -1)
          grid.FirstDisplayedScrollingRowIndex = value;
      }
    }

    public DataGridViewCell FirstDisplayedCell
    {
      get
      {
        return grid.FirstDisplayedCell;
      }
      set
      {
        grid.FirstDisplayedCell = value;
      }
    }

    public DataGridViewCell Cell(string col, int row)
    {
      return grid[col, row];
    }

    public DataGridViewCell Cell(int col, int row)
    {
      return grid[col, row];
    }

    public Boolean DoubleBufferedOn
    {
      get
      {
        return DoubleBuffered;
      }
      set
      {
        DoubleBuffered = value;
      }
    }

    public void SetRowHeight(int height)
    {
      DataGridViewRow row = grid.RowTemplate;
      //row.DefaultCellStyle.BackColor = Color.Bisque;
      row.Height = height;
      //row.MinimumHeight = 20;
    }

    public Boolean BufferShow
    {
      set
      {
        bufferShow = value;
        if (value)
        {
          picBild.Visible = true;
          picBild.BringToFront();
        }
        else
        {
          picBild.Visible = false;
          picBild.SendToBack();
        }
      }
      get
      {
        return bufferShow;
      }
    }
	}
}