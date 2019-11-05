using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerDataCreateCsvZip
    {
        void Create(List<CustomerDataExportModel> customerData, string folderPath
            , DownloadRequest downloadRequest, int zipFileRange);
    }

    public class CustomerDataCreateCsvZip : ICustomerDataCreateCsvZip
    {

        private readonly ICreateCustomerDataCsv _createCustomerDataCsv;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        public CustomerDataCreateCsvZip(ICreateCustomerDataCsv createCustomerDataCsv, IDownloadRequestRepository downloadRequestRepository)
        {
            _createCustomerDataCsv = createCustomerDataCsv;
            _downloadRequestRepository = downloadRequestRepository;
        }

        public void Create(List<CustomerDataExportModel> customerData, string folderPath, DownloadRequest downloadRequest, int zipFileRange)
        {
            string fileType = "csv";
            var fileContainer = customerData.Batch(zipFileRange);
            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);
            foreach (var customerDataModels in fileContainer)
            {
                var excelRequest = (DownloadRequest)downloadRequest.Clone();
                var fileName = GetGUID();
                var filePath = $"{folderPath}\\{fileName}.{fileType}";
                excelRequest.SearchId = fileName; // update the search id
                _createCustomerDataCsv.Create(customerDataModels.ToList(), filePath, excelRequest);
            }
            downloadRequest.StatusCode = (int)FileCreateStatus.Completed;
            _downloadRequestRepository.UpdateAsync(downloadRequest);
        }
        private string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return $"{guid.ToString()}{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        }
    }


}
