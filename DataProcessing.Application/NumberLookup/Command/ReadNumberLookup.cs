using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace DataProcessing.Application.NumberLookup.Command
{
    public class ReadNumberLookup : IReadNumberLookup
    {
        public IEnumerable<Numbers> Read(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            IEnumerable<Numbers> numberLookup = null;
            if (fileInfo != null)
            {
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    numberLookup = ReadExcelPackageToString(package, worksheet);
                    package.Dispose();
                }
            }
            return numberLookup;
        }

        private IEnumerable<Numbers> ReadExcelPackageToString(ExcelPackage package, ExcelWorksheet worksheet)
        {
            var rowCount = worksheet.Dimension?.Rows;
            // check the excel has header or not, if header exist it start reading from row 2, ot start 1
            var startRow = CheckTheHeaderHasColumnOrNot($"{worksheet.Cells[1, 1].Value}");
            IList<Numbers> numberLookups = new List<Numbers>();

            var rowCountValue = (rowCount != null && rowCount.Value != null) ? rowCount.Value : 0;

            for (int row = startRow; row <= rowCountValue; row++)
            {
                var cellValue = $"{worksheet.Cells[row, 1].Value}";
                if (CellIsValidCheck(cellValue))
                    numberLookups.Add(new Numbers { PhoneNumber = cellValue, Series = cellValue.Substring(0, 4) });
            }
            return numberLookups;
        }

        private bool CellIsValidCheck(string cellValue)
        {
            return !string.IsNullOrWhiteSpace(cellValue);
        }

        private int CheckTheHeaderHasColumnOrNot(string header)
        {
            if (!string.IsNullOrWhiteSpace(header) && long.TryParse(header, out long val))
                return 1;
            return 2;
        }
    }
}