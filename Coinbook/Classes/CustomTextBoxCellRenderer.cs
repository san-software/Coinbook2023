using System;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.DataGrid;
using System.Drawing;

using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Enums;
using System.IO;
using System.Windows.Forms;

namespace Coinbook
{
	public class CustomTextBoxCellRenderer : Syncfusion.WinForms.DataGrid.Renderers.GridTextBoxCellRenderer
	{
		private Icon coin = new Icon(Path.Combine(Path.Combine(Application.StartupPath, "Images"), "Coin.ico"));
		private Icon hinweis = new Icon(Path.Combine(Path.Combine(Application.StartupPath, "Images"), "Hinweis.ico"));

		protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style,
			DataColumnBase column, Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex rowColumnIndex)
		{
			try
			{
				base.OnRender(paint, cellRect, cellValue, style, column, rowColumnIndex);
			}
			catch { }
		}

		private void showIcon(Icon icon, Rectangle rec, Graphics graphics)
		{
			int x = (rec.Width - icon.Width) / 2;
			if (x > 0)
			{
				rec.X = rec.X + x;
				rec.Width = icon.Width;
			}

			graphics.DrawIcon(icon, rec);
		}
	}
}
