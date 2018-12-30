using OfficeOpenXml;
using System.Collections.Generic;

namespace DataProcessing.Application.Common
{
    public static class ExcelWorksheetExtension
    {
        public static string[] GetHeaderColumns(this ExcelWorksheet sheet)
        {
            List<string> columnNames = new List<string>();
            foreach (var firstRowCell in sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1, sheet.Dimension.End.Column])
                columnNames.Add(firstRowCell.Text.Trim());
            return columnNames.ToArray();
        }
    }
}