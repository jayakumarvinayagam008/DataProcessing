using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataProcessing.Application.NumberLookup.Query;
using OfficeOpenXml;

namespace DataProcessing.Application.NumberLookup.Command
{
    public class SaveNumberLookUp : ISaveNumberLookUp
    {
        public string CreateAndSave(IEnumerable<NumberLookUpDetail> numberLookUpDetails, string rootPath)
        {
            var headerRow = new List<string[]>()
                          {
                            new string[] { "Phone", "Circle", "Operator" }
                          };
            string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
            string fileName = string.Empty;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("NumberLookup");
                // Target a worksheet
                var worksheet = excel.Workbook.Worksheets["NumberLookup"];
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);
                int rowIndex = 2;
                foreach (var item in numberLookUpDetails)
                {
                    worksheet.Cells[rowIndex, 1].Value = (item.Phone);
                    worksheet.Cells[rowIndex, 2].Value = (item.Circle);
                    worksheet.Cells[rowIndex, 3].Value = (item.Operator);
                    rowIndex++;
                }
                fileName = $"{GetGUID()}";
                FileInfo excelFile = new FileInfo($"{rootPath}{fileName}.xlsx");
                excel.SaveAs(excelFile);
            }
            return fileName;
        }
        private string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return $"{guid.ToString()}{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        }
    }
}
