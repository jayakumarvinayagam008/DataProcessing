using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public interface ICreateCsvZip
    {
       void Create(List<BusinessToBusinessModel> businessToBusinesses, string folderPath, DownloadRequest downloadRequest, int zipFileRange);
    }

    public class CreateCsvZip : ICreateCsvZip
    {
        private readonly ICreateCsv _createCsv;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        public CreateCsvZip(ICreateCsv createCsv, IDownloadRequestRepository downloadRequestRepository)
        {
            _createCsv = createCsv;
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<BusinessToBusinessModel> businessToBusinesses, string folderPath, DownloadRequest downloadRequest, int zipFileRange)
        {
            string fileType = "csv";
            var fileContainer = businessToBusinesses.Batch(zipFileRange);
            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);

            foreach (var businessToBusinessModels in fileContainer)
            {
                var excelRequest = (DownloadRequest)downloadRequest.Clone();
                var fileName = GetGUID();
                var filePath = $"{folderPath}\\{fileName}.{fileType}";
                excelRequest.SearchId = fileName; // update the search id
                _createCsv.Create(businessToBusinessModels.ToList(), filePath, excelRequest);
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
