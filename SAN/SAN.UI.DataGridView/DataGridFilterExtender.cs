using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

using SAN.UI.DataGridView.GridFilterFactories;

namespace SAN.UI.DataGridView
{
	/// <summary>
	/// Component which allows <see cref="DataGridView"/>s to be extended with
	/// autometed filter functionality.
	/// </summary>
	public class DataGridFilterExtender : System.ComponentModel.Component, System.ComponentModel.ISupportInitialize
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

		#endregion

		#region Fields

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.DataGridView grid;
		private Control currentParent;
		private GridFiltersControl filters;
		private FilterPosition filterPosition = FilterPosition.Off;
		private bool autoAdjustGrid = false;
		private bool initializing = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance
		/// </summary>
		/// <param name="container"></param>
		public DataGridFilterExtender(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			
			filters = new GridFiltersControl();
			FilterFactory = new DefaultGridFilterFactory();
		}

		/// <summary>
		/// Creates a new instance
		/// </summary>
		public DataGridFilterExtender()
		{
			InitializeComponent();

			filters = new GridFiltersControl();
			FilterFactory = new DefaultGridFilterFactory();
		}

		#endregion

		#region Overriden from Component

		/// <summary> 
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (filters != null)
			{
				RemoveFilterControl();
				filters.Dispose();
				filters = null;
			}
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Vom Komponenten-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode f|r die Designerunterst|tzung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor gedndert werden.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Public interface

		/// <summary>
		/// Gets and sets whether filters are kept while switching between different tables.
		/// </summary>
		[Browsable(true), DefaultValue(false)]
		[Description("Specifies whether filters are kept while switching between different tables.")]
		public bool KeepFilters
		{
			get { return filters.KeepFilters; }
			set { filters.KeepFilters = value; }
		}

		/// <summary>
		/// Gets and sets whether the filter criteria is automatically refreshed when
		/// changes are made to the filter controls. If set to false then a call to
		/// <see cref="RefreshFilters"/> is needed to manually refresh the criteria.
		/// </summary>
        [Browsable(true), DefaultValue(RefreshMode.OnInput)]
		[Description("Specifies if the view automatically refreshes to reflect "
			 + "changes in the grid filter controls.")]
		public RefreshMode AutoRefreshMode
		{
            get { return filters.AutoRefreshMode; }
            set { filters.AutoRefreshMode = value; }
		}

		/// <summary>
		/// Gets and sets the poisiton of the filter GUI elements.
		/// </summary>
		[Browsable(true), DefaultValue(FilterPosition.Top)]
		[Description("Gets and sets the position of the filter GUI elements.")]
		public FilterPosition FilterBoxPosition 
		{
			get { return filterPosition; }
			set 
			{
				if (filterPosition == value)
					return;

				if (autoAdjustGrid)
					AdjustGridPosition(filterPosition, value);

				filterPosition = value;
				AdjustFilterControlToGrid();
			}
		}

		/// <summary>
		/// Sets whether the bounds of the extended DataGridView should be
		/// set automatically depending on where the filters are displayed,
		/// so that the totally covered area by grid and filters is always 
		/// the same.
		/// </summary>
		/// <remarks>
		/// This wont function correctly if the grid is docked
		/// </remarks>
		[Browsable(true), DefaultValue(false)]
		[Description("Sets whether the bounds of the extended DataGridView should be "
			+ "set automatically depending on where the filters are displayed, so "
			+ "that the totally covered area by grid and filters is always the same.")]
		public bool AutoAdjustGridPosition 
		{
			get { return autoAdjustGrid; }
			set 
			{
				if (autoAdjustGrid == value)
					return;

				autoAdjustGrid = value; 

				if (autoAdjustGrid)
					AdjustGridPosition(FilterPosition.Off, filterPosition);
				else
					AdjustGridPosition(filterPosition, FilterPosition.Off);
			}
		}

		/// <summary>
		/// Gets and sets the text for the filter label.
		/// </summary>
		[Browsable(true), DefaultValue("Filter")]
		[Description("Gets and sets the text for the filter label.")]
		public string FilterText 
		{
			get { return filters.FilterText; }
			set { filters.FilterText = value; }
		}

		/// <summary>
		/// Gets and sets whether the filter label should be visible.
		/// </summary>
		[Browsable(true), DefaultValue(true)]
		[Description("Gets and sets whether the filter label should be visible.")]
		public bool FilterTextVisible 
		{
			get { return filters.FilterTextVisible; }
			set { filters.FilterTextVisible = value; }
		}

		/// <summary>
		/// The bounds of the control with the GUI for filtering
		/// </summary>
		[Browsable(false)]
		public Rectangle ControlBounds 
		{
			get { return filters == null ? Rectangle.Empty : filters.Bounds; }
		}

		/// <summary>
		/// The Height of the control which is positioned for filtering
		/// </summary>
		[Browsable(false)]
		public int NeededControlHeight 
		{
			get { return filters == null ? 0 : filters.Height; }
		}

		/// <summary>
		/// Gets and sets the <see cref="IGridFilterFactory"/> used to generate the filter GUI.
		/// </summary>
		[Browsable(true), DefaultValue(null)]
		public IGridFilterFactory FilterFactory 
		{
			get { return filters.FilterFactory; }
			set { filters.FilterFactory = value; }
		}

		/// <summary>
		/// The selected operator to combine the filter criterias.
		/// </summary>
		[Browsable(true), DefaultValue(LogicalOperators.And)]
		public LogicalOperators Operator 
		{
			get { return filters.Operator; }
			set { filters.Operator = value; }
		}

		/// <summary>
		/// Gets and sets what information is showed to the user
		/// if an error in the builded filter criterias occurs.
		/// </summary>
		[Browsable(true), DefaultValue(FilterErrorModes.General)]
		[Description("Specifies what information is shown to the user "
			 + "if an error in the builded filter criterias occurs.")]
		public FilterErrorModes MessageErrorMode 
		{
			get { return filters.MessageErrorMode; }
			set { filters.MessageErrorMode = value; }
		}

		/// <summary>
		/// Gets and sets what information is showed to the user
		/// if an error in the builded filter criterias occurs.
		/// </summary>
		[Browsable(true), DefaultValue(FilterErrorModes.Off)]
		[Description("Specifies what information is printed to the console "
			 + "if an error in the builded filter criterias occurs.")]
		public FilterErrorModes ConsoleErrorMode 
		{
			get { return filters.ConsoleErrorMode; }
			set { filters.ConsoleErrorMode = value; }
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
            get { return filters.BaseFilters; }
        }

        /// <summary>
        /// Gets or sets which operator should be used to combine the base filter
        /// with the automatically created filters.
        /// </summary>
        [Browsable(true), DefaultValue(LogicalOperators.And)]
        [Description("Operator which should be used to combine the base filter "
            + "with the automatically created filters.")]
        public LogicalOperators BaseFilterOperator
        {
            get { return filters.BaseFilterOperator; }
            set { filters.BaseFilterOperator = value; }
        }

        /// <summary>
        /// Gets or sets whether base filters should be used when refreshing
        /// the filter criteria. Setting it to false will disable the functionality
        /// while still keeping the base filter strings in the <see cref="BaseFilters"/>
        /// collection intact.
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        [Description("Gets or sets whether base filters should be used when "
            + "refreshing the filter criteria.")]
        public bool BaseFilterEnabled
        {
            get { return filters.BaseFilterEnabled; }
            set { filters.BaseFilterEnabled = value; }
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
            get { return filters.CurrentTableBaseFilter; }
            set { filters.CurrentTableBaseFilter = value; }
        }

		/// <summary>
		/// Gets and sets the grid which should be extended.
		/// </summary>
		[Browsable(true)]
		[Description("Gets and sets the grid which should be extended.")]
				public System.Windows.Forms.DataGridView DataGridView 
		{
			get { return grid; }
			set 
			{	
				if (grid == value)
					return;

				RemoveFilterControl();
				if (grid != null) 
				{
					grid.LocationChanged -= new EventHandler(OnGridLocationChanged);
					grid.Resize -= new EventHandler(OnGridResize);
					grid.ParentChanged -= new EventHandler(OnGridParentChanged);
				}

				grid = value;

				filters.DataGridView = grid;		

				AdjustFilterControlToGrid();
				AddFilterControl();

				if (autoAdjustGrid)
					AdjustGridPosition(FilterPosition.Off, filterPosition);

                if (grid != null)
                {
                    grid.LocationChanged += new EventHandler(OnGridLocationChanged);
                    grid.Resize += new EventHandler(OnGridResize);
                    grid.ParentChanged += new EventHandler(OnGridParentChanged);
                }
			}
        }

        /// <summary>
        /// Gets all currently set <see cref="IGridFilter"/>s.
        /// </summary>
        /// <returns>Collection of <see cref="IGridFilter"/>s.</returns>
        public GridFilterCollection GetGridFilters()
        {
            return filters.GetGridFilters();
        }
	
		/// <summary>
		/// Clears all filters to initial state.
		/// </summary>
		public void ClearFilters() 
		{
			filters.ClearFilters();
		}
	
		/// <summary>
		/// Gets all filters currently set
		/// </summary>
		/// <returns></returns>
		public string[] GetFilters() 
		{
			return filters.GetFilters();
		}
	
		/// <summary>
		/// Sets all filters to the specified values.
		/// The values must be in order of the column styles in the current view.
		/// This function should normally be used with data previously coming
		/// from the <see cref="GetFilters"/> function.
		/// </summary>
		/// <param name="filter">filters to set</param>
		public void SetFilters(string[] filter) 
		{
			if (filters != null)
				filters.SetFilters(filter);
		}

		/// <summary>
		/// Refreshes the filter criteria to match the current contents of the associated
		/// filter controls.
		/// </summary>
		public void RefreshFilters()
		{
			filters.RefreshFilters();
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

        #region Privates

        private void AdjustGridPosition(FilterPosition fromPosition, FilterPosition toPosition) 
		{
			if (grid == null || filters == null || fromPosition == toPosition)
				return;

			if (initializing)
				return;

			int newTop = grid.Top;
			int newHeight = grid.Height;

			switch (fromPosition)
			{
				case FilterPosition.Bottom:
					newHeight += filters.Height;
					break;
				case FilterPosition.Top:
					newTop -= filters.Height;
					newHeight += filters.Height;
					break;
			}

			switch (toPosition) 
			{
				case FilterPosition.Bottom:
					newHeight -= filters.Height;
					break;
				case FilterPosition.Top:
					newTop += filters.Height;
					newHeight -= filters.Height;
					break;
			}

			AnchorStyles oldStyle = grid.Anchor;
			grid.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			grid.SetBounds(0, newTop, 0, newHeight, BoundsSpecified.Y | BoundsSpecified.Height);
			grid.Anchor = oldStyle;
		}
		
		private void RemoveFilterControl()
		{
			if (currentParent != null) 
			{
                filters.AfterFiltersChanged -= new EventHandler(OnAfterFiltersChanged);
                filters.BeforeFiltersChanging -= new EventHandler(OnBeforeFiltersChanging);
                filters.GridFilterBound -= new GridFilterEventHandler(OnGridFilterBound);
                filters.GridFilterUnbound -= new GridFilterEventHandler(OnGridFilterUnbound);
				currentParent.Controls.Remove(filters);
				currentParent.BackColorChanged -= new EventHandler(OnColorsChanged);
				currentParent.ForeColorChanged -= new EventHandler(OnColorsChanged);
				currentParent = null;
			}
		}

		private void AddFilterControl()
		{
			RemoveFilterControl();

            if (grid != null)
            {
                if (grid.Parent != null)
                {
                    if (currentParent != null)
                    {
                        currentParent.BackColorChanged -= new EventHandler(OnColorsChanged);
                        currentParent.ForeColorChanged -= new EventHandler(OnColorsChanged);
                    }

                    currentParent = grid.Parent;
                    currentParent.BackColorChanged += new EventHandler(OnColorsChanged);
                    currentParent.ForeColorChanged += new EventHandler(OnColorsChanged);
                    grid.Parent.Controls.Add(filters);
                    filters.BringToFront();
                    filters.AfterFiltersChanged += new EventHandler(OnAfterFiltersChanged);
                    filters.BeforeFiltersChanging += new EventHandler(OnBeforeFiltersChanging);
                    filters.GridFilterBound += new GridFilterEventHandler(OnGridFilterBound);
                    filters.GridFilterUnbound += new GridFilterEventHandler(OnGridFilterUnbound);
                }

                AdjustFilterControlToGrid();
            }
		}

		private void AdjustFilterControlToGrid() 
		{
			if (grid == null || filters == null || grid.Parent == null)
				return;

			switch (filterPosition) 
			{
				case FilterPosition.Top:
					filters.Top = grid.Top - filters.Height;
					filters.Left = grid.Left;
					filters.Width = grid.Width;
					filters.BackColor = grid.Parent.BackColor;
					filters.ForeColor = grid.Parent.ForeColor;
					filters.Visible = true;
					break;
				case FilterPosition.Bottom:
					filters.Top = grid.Bottom + 1;
					filters.Left = grid.Left;
					filters.Width = grid.Width;
					filters.BackColor = grid.Parent.BackColor;
					filters.ForeColor = grid.Parent.ForeColor;
					filters.Visible = true;
					break;
				default:
					filters.Visible = false;
					break;
			}
		}

		#region Eventhandlers

		private void OnGridLocationChanged(object sender, EventArgs e)
		{
			AdjustFilterControlToGrid();
		}

		private void OnGridResize(object sender, EventArgs e)
		{
			AdjustFilterControlToGrid();
		}

		private void OnGridParentChanged(object sender, EventArgs e)
		{
			AddFilterControl();
		}

		private void OnColorsChanged(object sender, EventArgs e)
		{
			AdjustFilterControlToGrid();
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

		#endregion

		#endregion

		#region ISupportInitialize Member

		/// <summary>
		/// Sets a flag to true representing that the component is now initializing.
		/// </summary>
		/// <remarks>
		/// This is important as the component must know if the properties are set within
		/// the designer generated code so that no abnormal moving of the contained grid occurs
		/// when AutoAdjustGridPosition is set to true
		/// </remarks>
		public void BeginInit()
		{
			initializing = true;
            if (filters != null)
                filters.BeginInit();
		}

		/// <summary>
		/// Sets a flag to false representing that the initialization of the
		/// component has completed
		/// </summary>
		public void EndInit()
		{
            initializing = false;
            if (filters != null)
                filters.EndInit();
		}

		#endregion
	}
}
