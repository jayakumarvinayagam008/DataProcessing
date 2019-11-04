using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CreateExcelZip
    {
        void Create(List<B2CSearchResult> businessToCustomers, string folderPath,
            int range, DownloadRequest downloadRequest, int zipFileRange);
    }

    public class B2CreateExcelZip : IB2CreateExcelZip
    {
        private readonly IB2CExcelExport _b2CExcelExport;
        private readonly IDownloadRequestRepository _downloadRequestRepository;

        public B2CreateExcelZip(IB2CExcelExport createExcel, IDownloadRequestRepository downloadRequestRepository)
        {
            _b2CExcelExport = createExcel;
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<B2CSearchResult> businessToCustomers, string folderPath, int range, DownloadRequest downloadRequest, int zipFileRange)
        {
            string fileType = "xlsx";
            var fileContainer = businessToCustomers.Batch(zipFileRange);
            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);
            foreach (var businessToCustomerModels in fileContainer)
            {
                var excelRequest = (DownloadRequest)downloadRequest.Clone();
                var fileName = GetGUID();
                var filePath = $"{folderPath}\\{fileName}.{fileType}";
                excelRequest.SearchId = fileName; // update the search id
                _b2CExcelExport.Create(businessToCustomerModels.ToList(), filePath, range, excelRequest);
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
