using DataProcessing.Application.B2B.Common;
using DataProcessing.Application.Common;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataProcessing.Application.B2B.Command
{
    public class ReadDataFromFile : IReadDataFromFile
    {
        private Dictionary<string, int> columnIndex;
        private int totalRows = 0;

        public (IEnumerable<BusinessToBusinesModel>, int) ReadFileData(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            IEnumerable<BusinessToBusinesModel> businessToBusinessModels = null;
            if (fileInfo != null)
            {
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    businessToBusinessModels = ReadExcelPackageToString(package, worksheet);
                    package.Dispose();
                }
            }
            return (businessToBusinessModels, totalRows);
        }

        private IDictionary<string, int> columnArray;

        private IEnumerable<BusinessToBusinesModel> ReadExcelPackageToString(ExcelPackage package, ExcelWorksheet worksheet)
        {
            var rowCount = worksheet.Dimension?.Rows;
            totalRows = rowCount.Value - 1;
            var colCount = worksheet.Dimension?.Columns;
            columnIndex = new BusinessToBusinessColumnMapping().GetCustomerColumnMapping();
            IDictionary<string, int> columnHeader = new Dictionary<string, int>();
            IList<BusinessToBusinesModel> businessToBusinesModels = new List<BusinessToBusinesModel>();

            var requiredColumns = columnIndex.Select(x => x.Key.Trim()).ToArray();

            var uploadedColumns = worksheet.GetHeaderColumns();
            var tt = requiredColumns.Except(uploadedColumns);
            var allColumnExist = requiredColumns.Except(uploadedColumns).Count() == 0;
            // check column count
            if (colCount.HasValue && allColumnExist)
            {
                // fetch first row for column header
                int firstRow = 1;
                for (int col = 1; col <= colCount.Value; col++)
                {
                    columnHeader.Add($"{worksheet.Cells[firstRow, col].Value}", col);
                }

                //Featch all remain rows
                columnArray = columnHeader;
                for (int row = 2; row <= rowCount.Value; row++)
                {
                    int.TryParse($"{worksheet.Cells[row, GetColumnIndex("CategoryId")].Value}", out int categoryId);
                    int.TryParse($"{worksheet.Cells[row, GetColumnIndex("EstYear")].Value}", out int estYear);
                    int.TryParse($"{worksheet.Cells[row, GetColumnIndex("NoOfEmp")].Value}", out int noOfEmp);
                    businessToBusinesModels.Add(new BusinessToBusinesModel
                    {
                        Add1 = $"{worksheet.Cells[row, GetColumnIndex("Add1")].Value}".Trim(),
                        Add2 = $"{worksheet.Cells[row, GetColumnIndex("Add2")].Value}".Trim(),
                        Area = $"{worksheet.Cells[row, GetColumnIndex("Area")].Value}".Trim(),
                        CategoryId = categoryId,
                        City = $"{worksheet.Cells[row, GetColumnIndex("City")].Value}".Trim(),
                        CompanyName = $"{worksheet.Cells[row, GetColumnIndex("CompanyName")].Value}".Trim(),
                        ContactPerson = $"{worksheet.Cells[row, GetColumnIndex("ContactPerson")].Value}".Trim(),
                        Contactperson1 = $"{worksheet.Cells[row, GetColumnIndex("Contactperson1")].Value}".Trim(),
                        Country = $"{worksheet.Cells[row, GetColumnIndex("Country")].Value}".Trim(),
                        Designation = $"{worksheet.Cells[row, GetColumnIndex("Designation")].Value}".Trim(),
                        Designation1 = $"{worksheet.Cells[row, GetColumnIndex("Designation1")].Value}".Trim(),
                        Email = $"{worksheet.Cells[row, GetColumnIndex("Email")].Value}".Trim(),
                        Email1 = $"{worksheet.Cells[row, GetColumnIndex("Email1")].Value}".Trim(),
                        EstYear = estYear,
                        Fax = $"{worksheet.Cells[row, GetColumnIndex("Fax")].Value}".Trim(),
                        LandMark = $"{worksheet.Cells[row, GetColumnIndex("LandMark")].Value}".Trim(),
                        Mobile1 = $"{worksheet.Cells[row, GetColumnIndex("Mobile1")].Value}".Trim(),
                        Mobile2 = $"{worksheet.Cells[row, GetColumnIndex("Mobile2")].Value}".Trim(),
                        MobileNew = $"{worksheet.Cells[row, GetColumnIndex("Mobile_New")].Value}".Trim(),
                        NoOfEmp = noOfEmp,
                        Phone1 = $"{worksheet.Cells[row, GetColumnIndex("Phone1")].Value}".Trim(),
                        Phone2 = $"{worksheet.Cells[row, GetColumnIndex("Phone2")].Value}".Trim(),
                        PhoneNew = $"{worksheet.Cells[row, GetColumnIndex("Phone_New")].Value}".Trim(),
                        Pincode = $"{worksheet.Cells[row, GetColumnIndex("Pincode")].Value}".Trim(),
                        State = $"{worksheet.Cells[row, GetColumnIndex("State")].Value}".Trim(),
                        Web = $"{worksheet.Cells[row, GetColumnIndex("Web")].Value}".Trim()
                    });
                }
            }
            else
            {
            }
            return businessToBusinesModels.AsEnumerable<BusinessToBusinesModel>();
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