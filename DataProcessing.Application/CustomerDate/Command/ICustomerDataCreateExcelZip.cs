using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerDataCreateExcelZip
    {
        void Create(List<CustomerDataExportModel> customersData, string folderPath,
            int range, DownloadRequest downloadRequest, int zipFileRange);
    }
    public class CustomerDataCreateExcelZip : ICustomerDataCreateExcelZip
    {
        private readonly ICreateCustomerDataExcel _createCustomerDataExcel;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        public CustomerDataCreateExcelZip(ICreateCustomerDataExcel createCustomerDataExcel, IDownloadRequestRepository downloadRequestRepository)
        {
            _createCustomerDataExcel = createCustomerDataExcel;
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<CustomerDataExportModel> customersData, string folderPath, int range, DownloadRequest downloadRequest, int zipFileRange)
        {
            string fileType = "xlsx";
            var fileContainer = customersData.Batch(zipFileRange);
            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);
            foreach (var customerDataModels in fileContainer)
            {
                var excelRequest = (DownloadRequest)downloadRequest.Clone();
                var fileName = GetGUID();
                var filePath = $"{folderPath}\\{fileName}.{fileType}";
                excelRequest.SearchId = fileName; // update the search id
                _createCustomerDataExcel.Create(customerDataModels.ToList(), filePath, range, excelRequest);
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
