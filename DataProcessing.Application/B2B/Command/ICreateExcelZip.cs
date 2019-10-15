using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Application.B2B.Command
{
    public interface ICreateExcelZip
    {
        void Create(List<BusinessToBusinessModel> businessToBusinesses, string folderPath, int range, DownloadRequest downloadRequest, int zipFileRange);

    }
    public class CreateExcelZip : ICreateExcelZip
    {
        private readonly ICreateExcel _createExcel;
        private readonly IDownloadRequestRepository _downloadRequestRepository;

        public CreateExcelZip(ICreateExcel createExcel, IDownloadRequestRepository downloadRequestRepository)
        {
            _createExcel = createExcel;
            _downloadRequestRepository = downloadRequestRepository;
        }
        public void Create(List<BusinessToBusinessModel> businessToBusinesses, string folderPath
            , int range, DownloadRequest downloadRequest, int zipFileRange)
        {
            string fileType = "xlsx";
            var fileContainer = businessToBusinesses.Batch(range);
            //FileInfo fileInfo = new FileInfo(folderPath);

            downloadRequest.StatusCode = (int)FileCreateStatus.InProgress;
            _downloadRequestRepository.UpdateAsync(downloadRequest);

            foreach (var businessToBusinessModels in fileContainer)
            {
                var excelRequest = (DownloadRequest)downloadRequest.Clone();
                var fileName = GetGUID();
                var filePath = $"{folderPath}\\{fileName}.{fileType}";
                excelRequest.SearchId = fileName; // update the search id
                _createExcel.Create(businessToBusinessModels.ToList(), filePath, range, excelRequest);
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
