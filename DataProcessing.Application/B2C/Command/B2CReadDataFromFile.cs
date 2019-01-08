using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataProcessing.Application.B2C.Command
{
    public class B2CReadDataFromFile : IB2CReadDataFromFile
    {
        private Dictionary<string, int> columnIndex;
        private int totalRows = 0;
        private IDictionary<string, int> columnArray;

        public (IEnumerable<BusinessToCustomerModel>, int) ReadFileData(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            IEnumerable<BusinessToCustomerModel> businessToBusinessModels = null;
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

        private IEnumerable<BusinessToCustomerModel> ReadExcelPackageToString(ExcelPackage package, ExcelWorksheet worksheet)
        {
            var rowCount = worksheet.Dimension?.Rows;
            totalRows = rowCount.Value - 1;
            var colCount = worksheet.Dimension?.Columns;
            columnIndex = new ColumnMapping().GetB2CColumnMapping();
            IDictionary<string, int> columnHeader = new Dictionary<string, int>();
            IList<BusinessToCustomerModel> businessToCustomerModels = new List<BusinessToCustomerModel>();

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
                    columnHeader.Add($"{worksheet.Cells[firstRow, col].Value}", col);
                }

                //Featch all remain rows
                columnArray = columnHeader;
                for (int row = 2; row <= rowCount.Value; row++)
                {
                    DateTime.TryParse($"{worksheet.Cells[row, GetColumnIndex("DOB")].Value}", out DateTime dateOfBirth);
                    int.TryParse($"{worksheet.Cells[row, GetColumnIndex("Experience")].Value}", out int experience);
                    businessToCustomerModels.Add(new BusinessToCustomerModel
                    {
                        Address = $"{worksheet.Cells[row, GetColumnIndex("Address")].Value}",
                        Address2 = $"{worksheet.Cells[row, GetColumnIndex("Address2")].Value}",
                        AnnualSalary = $"{worksheet.Cells[row, GetColumnIndex("AnnualSalary")].Value}",
                        Area = $"{worksheet.Cells[row, GetColumnIndex("Area")].Value}",
                        Caste = $"{worksheet.Cells[row, GetColumnIndex("Caste")].Value}",
                        City = $"{worksheet.Cells[row, GetColumnIndex("City")].Value}",
                        Country = $"{worksheet.Cells[row, GetColumnIndex("Country")].Value}",
                        Dob = dateOfBirth,
                        Email = $"{worksheet.Cells[row, GetColumnIndex("Email")].Value}",
                        Employer = $"{worksheet.Cells[row, GetColumnIndex("Employer")].Value}",
                        Experience = $"{worksheet.Cells[row, GetColumnIndex("Experience")].Value}",
                        Gender = $"{worksheet.Cells[row, GetColumnIndex("Gender")].Value}",
                        Industry = $"{worksheet.Cells[row, GetColumnIndex("Industry")].Value}",
                        KeySkills = $"{worksheet.Cells[row, GetColumnIndex("KeySkills")].Value}",
                        Location = $"{worksheet.Cells[row, GetColumnIndex("Location")].Value}",
                        Mobile2 = $"{worksheet.Cells[row, GetColumnIndex("Mobile2")].Value}",
                        MobileNew = $"{worksheet.Cells[row, GetColumnIndex("MobileNew")].Value}",
                        Name = $"{worksheet.Cells[row, GetColumnIndex("Name")].Value}",
                        Network = $"{worksheet.Cells[row, GetColumnIndex("Network")].Value}",
                        PhoneNew = $"{worksheet.Cells[row, GetColumnIndex("PhoneNew")].Value}",
                        Pincode = $"{worksheet.Cells[row, GetColumnIndex("Pincode")].Value}",
                        Qualification = $"{worksheet.Cells[row, GetColumnIndex("Qualification")].Value}",
                        Roles = $"{worksheet.Cells[row, GetColumnIndex("Roles")].Value}",
                        State = $"{worksheet.Cells[row, GetColumnIndex("State")].Value}",
                    });
                }
            }
            else
            {
            }
            return businessToCustomerModels.AsEnumerable<BusinessToCustomerModel>();
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