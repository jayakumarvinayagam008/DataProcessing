using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using OfficeOpenXml;

namespace DataProcessing.Application.CustomerDate.Command
{
    public class CustomerReadDataFromFile : ICustomerReadDataFromFile
    {
        private Dictionary<string, int> columnIndex;
        private int totalRows = 0;
        private IDictionary<string, int> columnArray;
        public (IEnumerable<CustomerDataModel>, int) ReadFileData(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            IEnumerable<CustomerDataModel> customerDataModels = null;
            if (fileInfo != null)
            {
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    customerDataModels = ReadExcelPackageToString(package, worksheet);
                    package.Dispose();
                }
            }
            return (customerDataModels, totalRows);
        }
        private IEnumerable<CustomerDataModel> ReadExcelPackageToString(ExcelPackage package, ExcelWorksheet worksheet)
        {
            var rowCount = worksheet.Dimension?.Rows;
            totalRows = rowCount.Value - 1;
            var colCount = worksheet.Dimension?.Columns;
            columnIndex = new ColumnMapping().GetCustomerColumnMapping();
            IDictionary<string, int> columnHeader = new Dictionary<string, int>();
            IList<CustomerDataModel> customerDataModel = new List<CustomerDataModel>();

            var requiredColumns = columnIndex.Select(x => x.Key.Trim()).ToArray();
            var uploadedColumns = worksheet.GetHeaderColumns();
            var allColumnExist = requiredColumns.Except(uploadedColumns).Count() == 0;
            // check column count
            if (colCount.HasValue && allColumnExist)
            {
                // fetch first row for column header
                int firstRow = 1;
                for (int col = 1; col <= colCount.Value; col++)
                {
                    if (!string.IsNullOrWhiteSpace($"{worksheet.Cells[firstRow, col].Value}"))
                        columnHeader.Add($"{worksheet.Cells[firstRow, col].Value}".Trim(), col);
                }

                //Featch all remain rows
                columnArray = columnHeader;
                for (int row = 2; row <= rowCount.Value; row++)
                {
                    DateTime.TryParse($"{worksheet.Cells[row, GetColumnIndex("DateOfUse")].Value}", out DateTime dateOfUse);

                    customerDataModel.Add(new CustomerDataModel
                    {
                        Circle = $"{worksheet.Cells[row, GetColumnIndex("Circle")].Value}",
                        ClientBusinessVertical = $"{worksheet.Cells[row, GetColumnIndex("ClientBusinessVertical")].Value}",
                        ClientCity = $"{worksheet.Cells[row, GetColumnIndex("ClientCity")].Value}",
                        ClientName = $"{worksheet.Cells[row, GetColumnIndex("ClientName")].Value}",
                        DateOfUse = dateOfUse,
                        Dbquality = $"{worksheet.Cells[row, GetColumnIndex("DbQuality")].Value}",
                        Numbers = $"{worksheet.Cells[row, GetColumnIndex("Numbers")].Value}",
                        Operator = $"{worksheet.Cells[row, GetColumnIndex("Operator")].Value}",
                        State = $"{worksheet.Cells[row, GetColumnIndex("State")].Value}",
                        Country = $"{worksheet.Cells[row, GetColumnIndex("Country")].Value}"
                    });
                }
            }         
            return customerDataModel.AsEnumerable<CustomerDataModel>();
        }

        private int GetColumnIndex(string keyName)
        {
            if (columnArray.Keys.Any(x => x.Trim() == keyName.Trim()))
            {
                return columnArray[keyName];
            }
            return 0;
        }
    }
}
