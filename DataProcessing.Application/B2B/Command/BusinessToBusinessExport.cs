using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataProcessing.Persistence;

namespace DataProcessing.Application.B2B.Command
{
    public enum FileCreateStatus
    {
        Started = 0,
        InProgress = 1,
        Completed = 2
    }
    public class BusinessToBusinessExport : IBusinessToBusinessExport
    {
        private readonly ICreateExcel _createExcel;
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        private readonly ICreateCsv _createCsv; 
        public BusinessToBusinessExport(ICreateExcel createExcel, IDownloadRequestRepository downloadRequestRepository
            , ICreateCsv createCsv)
        {
            _createExcel = createExcel;
            _downloadRequestRepository = downloadRequestRepository;
            _createCsv = createCsv;
        }
        public string Export(List<BusinessToBusiness> businessToBusinesses, string fileRootPath, int range)
        {
            var fileName = GetGUID();
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
            _downloadRequestRepository.CreateAsync(searchRequest).Wait();
            
            //_createExcel.Create(businessToBusinesses, filePath, range);
            Task.Run(() => _createExcel.Create(businessToBusinesses, filePath, range, searchRequest[0]));
            Task.Run(() => _createCsv.Create(businessToBusinesses, fileCsvPath, searchRequest[1]));

            return fileName;
        }

        private string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return $"{guid.ToString()}{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        }
    }


}
