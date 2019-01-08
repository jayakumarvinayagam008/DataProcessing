using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CExcelExport
    {
        void Create(List<B2CSearchResult> businessToCustomers, string filePath, 
            int range, DownloadRequest downloadRequest);
    }
    public class B2CExcelExport : IB2CExcelExport
    {
        private string[] columns = new string[] { "Name", "Dob", "Qualification", "Experience", "Employer", "KeySkills", "Location", "Roles", "Industry", "Address", "Address2", "Email", "PhoneNew", "MobileNew", "Mobile2", "AnnualSalary", "Pincode", "Area", "City", "State", "Country", "Network", "Gender", "Caste" };
        private readonly IDownloadRequestRepository _downloadRequestRepository;

        public B2CExcelExport(IDownloadRequestRepository downloadRequestRepository)
        {
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<B2CSearchResult> businessToCustomers, string filePath, int range, DownloadRequest downloadRequest)
        {
            var sheetContainer = businessToCustomers.Batch(range);
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
