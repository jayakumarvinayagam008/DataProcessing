using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Application.CustomerDate.Command
{
    public class CustomerDataExport : ICustomerDataExport
    {
        private readonly ICreateCustomerDataExcel _createExcel;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        private readonly ICreateCustomerDataCsv _createCsv;

        public CustomerDataExport(ICreateCustomerDataExcel createExcel, IDownloadRequestRepository downloadRequestRepository
            , ICreateCustomerDataCsv createCsv)
        {
            _createExcel = createExcel;
            _downloadRequestRepository = downloadRequestRepository;
            _createCsv = createCsv;
        }

        public string Export(List<CustomerData> customerData, string fileRootPath, int range)
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

            _downloadRequestRepository.CreateAsync(searchRequest).Wait();
            Task.Run(() => _createExcel.Create(downLoad, filePath, range, searchRequest[0]));
            Task.Run(() => _createCsv.Create(downLoad, fileCsvPath, searchRequest[1]));
            return fileName;
        }
    }
}