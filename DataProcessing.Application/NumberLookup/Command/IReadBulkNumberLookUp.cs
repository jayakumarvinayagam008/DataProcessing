using DataProcessing.Persistence;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface IReadBulkNumberLookUp
    {
        bool Process(string filepath);
        bool Remove(string _id);
        bool Update(string _id, Numbers numbers);
    }

    public class ReadBulkNumberLookUp : IReadBulkNumberLookUp
    {
        private readonly INumberLookupRepository _numberLookupRepository;
        public ReadBulkNumberLookUp(INumberLookupRepository numberLookupRepository)
        {
            _numberLookupRepository = numberLookupRepository;
        }

        public bool Process(string filepath)
        {
            FileInfo fileInfo = new FileInfo(filepath);
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

            var lookups = numberLookup.Select(x => new DataProcessing.Persistence.NumberLookup()
            {
                Series = x.Series,
                Operator = x.Operator,
                Circle = x.Circle,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now
            }).ToList();

            var status = _numberLookupRepository.CreateManyAsync(lookups);

            return status.Result;
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
                var Operator = $"{worksheet.Cells[row, 1].Value}";// Operator
                if (CellIsValidCheck(Operator))
                    numberLookups.Add(new Numbers
                    {
                        Operator = Operator,
                        Circle = $"{worksheet.Cells[row, 2].Value}", //Circle
                        Series = $"{worksheet.Cells[row, 3].Value}" //Series
                    });
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

        public bool Remove(string _id)
        {
            return _numberLookupRepository.Remove(_id).Result;
        }

        public bool Update(string _id, Numbers numbers)
        {
            var updateData = new DataProcessing.Persistence.NumberLookup()
            {
                Series = numbers.Series,
                Operator = numbers.Operator,
                Circle = numbers.Circle,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now
            };
            return _numberLookupRepository.UpdateAsync(updateData, _id).Result;
        }
    }

}
