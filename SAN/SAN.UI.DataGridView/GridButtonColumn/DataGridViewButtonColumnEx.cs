using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
//using Global;

namespace SAN.UI.DataGridView
{
	public class DataGridViewButtonColumnEx : DataGridViewButtonColumn
	{
		public event DataGridViewCellButtonClickEventHandler ColumnButtonClicked;		
		
		private string columnName = "";
		private System.Windows.Forms.DataGridView parent;

		public DataGridViewButtonColumnEx(string name, System.Windows.Forms.DataGridView parent)
		{
			columnName = name;
			this.parent = parent;
			this.parent.CellPainting += new DataGridViewCellPaintingEventHandler(DataGridView_CellPainting);
			this.parent.CellContentClick +=new DataGridViewCellEventHandler(DataGridView_CellContentClick);
			Enabled = true;
		}

		public Image ButtonImage {get;set;}
		public Image DisabledImage {get;set;}
		public Boolean Enabled { get; set; }

		public DataGridViewEx Parent { get; set; }

		private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (DataGridView.Columns[e.ColumnIndex].Name == columnName && Enabled)
			{
				DataGridViewCellButtonClickEventArgs ce = new DataGridViewCellButtonClickEventArgs(e.RowIndex, e.ColumnIndex, columnName);
				Parent.OnColumnButtonClicked(ce);
			}
		}

		private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (DataGridView.Columns[e.ColumnIndex].Name == columnName)
			{
				if (ButtonImage != null)
					drawButton(e);

				e.Handled = (ButtonImage != null);
			}
		}

		private void drawButton(DataGridViewCellPaintingEventArgs e)
		{
			int x =e.CellBounds.X + e.CellBounds.Width / 2 - (e.CellBounds.Height - 6) / 2;
			int y = e.CellBounds.Y +1;

				Rectangle bounds = new Rectangle(x,y, e.CellBounds.Height - 6, e.CellBounds.Height - 6);

				e.PaintBackground(e.CellBounds, (e.State & DataGridViewElementStates.Selected) > 0);
				if (DisabledImage == null)
					DisabledImage = ImageHelper.CreateGrayscaledBitmap(ButtonImage);

				if (Enabled)
					e.Graphics.DrawImage(ButtonImage, bounds, 0, 0, ButtonImage.Width, ButtonImage.Height, GraphicsUnit.Pixel);
				else
					e.Graphics.DrawImage(DisabledImage, bounds, 0, 0, DisabledImage.Width, DisabledImage.Height, GraphicsUnit.Pixel);

				e.PaintContent(e.CellBounds);

			//Rectangle frame = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);

			//string s = Grid[row, col].ToString();
			//s = "";

			//SizeF sz = g.MeasureString(s, Grid.Font, bounds.Width - 4, StringFormat.GenericTypographic);

			//int x = bounds.Left + Math.Max(0, (bounds.Width - (int)sz.Width)/2);

			//if(sz.Height < bounds.Height)
			//{
			//  int y = bounds.Top + (bounds.Height - (int) sz.Height) / 2;
			//  if(buttonON2 == bm)
			//    x++;

			//  g.DrawString(s, Grid.Font, new SolidBrush(Grid.ForeColor), x, y);
			//}
		}

	}
}
