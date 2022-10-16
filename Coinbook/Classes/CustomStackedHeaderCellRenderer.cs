using System;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.DataGrid;
using System.Drawing;
using Coinbook.Helper;

namespace Coinbook
{
	public class CustomStackedHeaderCellRenderer : Syncfusion.WinForms.DataGrid.Renderers.GridStackedHeaderCellRenderer
	{
		protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, 
			DataColumnBase column, Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex rowColumnIndex)
		{
			switch (column.ColumnIndex)
			{
				case 4:
					style.BackColor = CoinbookHelper.ColorHeader1;
					style.Borders.Bottom = new GridBorder(CoinbookHelper.ColorHeader1, GridBorderWeight.ExtraThin);
					break;

				case 28:
					style.BackColor = CoinbookHelper.ColorHeader2;
					style.Borders.Bottom = new GridBorder(CoinbookHelper.ColorHeader2, GridBorderWeight.ExtraThin);
					break;

				case 37:
					style.BackColor = CoinbookHelper.ColorHeader2;
					style.Borders.Bottom = new GridBorder(CoinbookHelper.ColorHeader2, GridBorderWeight.ExtraThin);
					break;

				case 50:
					style.BackColor = CoinbookHelper.ColorHeader3;
					style.Borders.Bottom = new GridBorder(CoinbookHelper.ColorHeader3, GridBorderWeight.ExtraThin);
					break;
			}

			base.OnRender(paint, cellRect, cellValue, style, column, rowColumnIndex);
		}
	}
}
