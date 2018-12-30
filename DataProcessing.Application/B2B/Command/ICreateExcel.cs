using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace DataProcessing.Application.B2B.Command
{
    public interface ICreateExcel
    {
        void Create(List<BusinessToBusinessModel> businessToBusinesses, string filePath, int range, DownloadRequest downloadRequest);
    }

    public class CreateExcel : ICreateExcel
    {
        private string[] columns = new string[] {
                                        "CompanyName",
                                        "Add1",
                                        "Add2",
                                        "City",
                                        "Area",
                                        "Pincode",
                                        "State",
                                        "PhoneNew",
                                        "MobileNew",
                                        "Phone1",
                                        "Phone2",
                                        "Mobile1",
                                        "Mobile2",
                                        "Fax",
                                        "Email",
                                        "Email1",
                                        "Web",
                                        "ContactPerson",
                                        "Contactperson1",
                                        "Designation",
                                        "Designation1",
                                        "EstYear",
                                        "LandMark",
                                        "NoOfEmp",
                                        "Country",
                                        "CategoryName",
                                        };

        private readonly IDownloadRequestRepository _downloadRequestRepository;

        public CreateExcel(IDownloadRequestRepository downloadRequestRepository)
        {
            _downloadRequestRepository = downloadRequestRepository;
        }

        public void Create(List<BusinessToBusinessModel> businessToBusinesses, string filePath, int range, DownloadRequest downloadRequest)
        {
            var sheetContainer = businessToBusinesses.Batch(range);
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