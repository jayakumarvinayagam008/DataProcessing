using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CreateCsvZip
    {        
        void Create(List<B2CSearchResult> businessToCustomers, string folderPath
            , DownloadRequest downloadRequest, int zipFileRange);

    }

    public class B2CreateCsvZip : IB2CreateCsvZip
    {
        private readonly IB2CCsvExport _b2CCsvExport;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        public B2CreateCsvZip(IB2CCsvExport b2CCsvExport, IDownloadRequestRepository downloadRequestRepository)
        {
            _b2CCsvExport = b2CCsvExport;
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<B2CSearchResult> businessToCustomers, string folderPath, DownloadRequest downloadRequest, int zipFileRange)
        {
            string fileType = "csv";
            var fileContainer = businessToCustomers.Batch(zipFileRange);
            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);
            foreach (var businessToBusinessModels in fileContainer)
            {
                var excelRequest = (DownloadRequest)downloadRequest.Clone();
                var fileName = GetGUID();
                var filePath = $"{folderPath}\\{fileName}.{fileType}";
                excelRequest.SearchId = fileName; // update the search id
                _b2CCsvExport.Create(businessToBusinessModels.ToList(), filePath, excelRequest);
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
