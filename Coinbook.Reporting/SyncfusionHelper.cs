using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;

namespace Coinbook
{
    public static class SyncfusionHelper
    {
        /// <summary>
        /// Applying WeightedSummary for Given Columns
        /// </summary>
        public static GridSummaryColumnDescriptor SummaryColumnDescriptor(string sourceCol, System.TypeCode type)
        {
            GridSummaryColumnDescriptor wgtSumCol = new GridSummaryColumnDescriptor();
            wgtSumCol.Name = sourceCol.ToString(); //special name following the convention above
            wgtSumCol.DataMember = sourceCol; //the column this summary is applied to
            wgtSumCol.DisplayColumn = sourceCol; //where thissummary is displayed

            switch (type)
            {
                case TypeCode.Int32:
                    wgtSumCol.SummaryType = SummaryType.Int32Aggregate; //marks this as a CustomSummary
                    wgtSumCol.Format = "{Sum:###,###}"; //what is displayed in the summary
                    wgtSumCol.MaxLength = 6;
                    break;

                case TypeCode.Double:
                    wgtSumCol.SummaryType = SummaryType.DoubleAggregate; //marks this as a CustomSummary
                    wgtSumCol.Format = "{Sum:###,##0.00}"; //what is displayed in the summary
                    wgtSumCol.MaxLength = 8;
                    break;
            }

            wgtSumCol.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;

            return wgtSumCol;
        }
    }
}
