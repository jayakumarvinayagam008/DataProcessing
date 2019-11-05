using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Application.CustomerDate.Command
{
    public class CustomerDataExport : ICustomerDataExport
    {
        private readonly ICreateCustomerDataExcel _createExcel;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        private readonly ICreateCustomerDataCsv _createCsv;
        private readonly ICustomerDataCreateExcelZip _customerDataCreateExcelZip;
        private readonly ICustomerDataCreateCsvZip _customerDataCreateCsvZip;
        public CustomerDataExport(ICreateCustomerDataExcel createExcel, IDownloadRequestRepository downloadRequestRepository
            , ICreateCustomerDataCsv createCsv, ICustomerDataCreateExcelZip customerDataCreateExcelZip
            , ICustomerDataCreateCsvZip customerDataCreateCsvZip)
        {
            _createExcel = createExcel;
            _downloadRequestRepository = downloadRequestRepository;
            _createCsv = createCsv;
            _customerDataCreateExcelZip = customerDataCreateExcelZip;
            _customerDataCreateCsvZip = customerDataCreateCsvZip;
        }

        public Tuple<string, string> Export(List<CustomerData> customerData, string fileRootPath, int range, int zipFileRange = 0)
        {
            var fileName = GetGuid.Get();
            string fileType = "xlsx";
            string fileCsvType = "csv";
            var filePath = $"{fileRootPath}{fileName}.{fileType}";
            var fileCsvPath = $"{fileRootPath}{fileName}.{fileCsvType}";
            // db insert
            var createdOn = DateTime.Now;
            var searchRequest = new List<DownloadRequest>() {
                new DownloadRequest
                {
                    CreatedBy = "Admin",
                    CreatedOn = createdOn,
                    SearchId = fileName,
                    StatusCode = (int)FileCreateStatus.Started,
                    FileType = 0 // excel
                },
                new DownloadRequest
                {
                    CreatedBy = "Admin",
                    CreatedOn = createdOn,
                    SearchId = fileName,
                    StatusCode = (int)FileCreateStatus.Started,
                    FileType = 1 // csv
                }
            };
            var downLoad = customerData.Select(x => new CustomerDataExportModel()
            {
                Circle = x.Circle,
                ClientBusinessVertical = x.ClientBusinessVertical,
                ClientCity = x.ClientCity,
                ClientName = x.ClientName,
                Country = x.Country,
                DateOfUse = x.DateOfUse,
                Dbquality = x.Dbquality,
                Numbers = x.Numbers,
                Operator = x.Operator,
                State = x.State
            }).ToList();

            if(downLoad.Count() > zipFileRange)
            {
                var folderPath = $"{fileRootPath}{fileName}";
                var excelFolder = $"{folderPath}{"Excel"}";
                var csvFolder = $"{folderPath}{"Csv"}";

                searchRequest[0].SearchId = $"{searchRequest[0].SearchId}{"Excel"}";
                searchRequest[1].SearchId = $"{searchRequest[1].SearchId}{"Csv"}";
                _downloadRequestRepository.CreateAsync(searchRequest).Wait();
                try
                {
                    // If the directory doesn't exist, create it.

                    if (!Directory.Exists(excelFolder))
                    {
                        Directory.CreateDirectory(excelFolder);
                    }

                    if (!Directory.Exists(csvFolder))
                    {
                        Directory.CreateDirectory(csvFolder);
                    }
                }
                catch (Exception)
                {
                }
                _customerDataCreateExcelZip.Create(downLoad, excelFolder, range, searchRequest[0], zipFileRange);
                _customerDataCreateCsvZip.Create(downLoad, csvFolder, searchRequest[1], zipFileRange);
            }
            else
            {
                _downloadRequestRepository.CreateAsync(searchRequest).Wait();
                Task.Run(() => _createExcel.Create(downLoad, filePath, range, searchRequest[0]));
                Task.Run(() => _createCsv.Create(downLoad, fileCsvPath, searchRequest[1]));
            }
            
            return new Tuple<string, string>(searchRequest[0].SearchId, searchRequest[1].SearchId);
        }
    }
}