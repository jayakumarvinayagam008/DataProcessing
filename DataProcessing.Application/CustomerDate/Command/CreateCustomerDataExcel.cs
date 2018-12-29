using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using OfficeOpenXml;

namespace DataProcessing.Application.CustomerDate.Command
{
    public class CreateCustomerDataExcel : ICreateCustomerDataExcel
    {
        private string[] columns = new string[] {
                                        "Numbers",
                                        "Operator",
                                        "Circle",
                                        "ClientName",
                                        "Business Vertical",
                                        "Db Quality",
                                        "Date Of Use",
                                        "City",
                                        "State",
                                        "Country"
                                        };
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        public CreateCustomerDataExcel(IDownloadRequestRepository downloadRequestRepository)
        {
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<CustomerDataExportModel> customerData, string filePath, int range, DownloadRequest downloadRequest)
        {
            var sheetContainer = customerData.Batch(range);
            FileInfo fileInfo = new FileInfo(filePath);

            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);

            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                int i = 0;
                foreach (var item in sheetContainer)
                {
                    var workSheet = GetWorkSheet(excelPackage, i++);
                    workSheet = AddHeader(workSheet);
                    workSheet.Cells["A2"].LoadFromCollection(item, false, OfficeOpenXml.Table.TableStyles.Medium1);
                }
                excelPackage.Save();
            }
            downloadRequest.StatusCode = (int)FileCreateStatus.Completed;
            _downloadRequestRepository.UpdateAsync(downloadRequest);

        }

        private ExcelWorksheet AddHeader(ExcelWorksheet excelWorksheet)
        {
            int rowIndex = 1;
            for (int columnIndex = 1; columnIndex <= columns.Length; columnIndex++)
            {
                excelWorksheet.Cells[rowIndex, columnIndex].Value = columns[columnIndex - 1];
            }
            return excelWorksheet;
        }

        private ExcelWorksheet GetWorkSheet(ExcelPackage excelPackage, int count)
        {
            var workSheet = excelPackage.Workbook.Worksheets.Add("Sheet - " + count);
            workSheet = AddHeader(workSheet);
            return workSheet;
        }
    }
}
