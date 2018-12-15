using DataProcessing.Application.B2B.Command;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataProcessing.Application.Common
{
    public class GetSearchedFileStatuscs : IGetSearchedFileStatuscs
    {
        private readonly IDownloadRequestRepository _downloadRequestRepository;
        public GetSearchedFileStatuscs(IDownloadRequestRepository downloadRequestRepository)
        {
            _downloadRequestRepository = downloadRequestRepository;
        }
        public bool FileExist(string searchRequistId, int fileType, string filePath)
        {
            var status = _downloadRequestRepository.GetDownloadRequestDetail(searchRequistId, fileType);
            status.Wait();
            if (status.Result != null && status.Result.StatusCode == (int)FileCreateStatus.Completed && File.Exists(filePath))
                return true;
            return false;

        }
    }
}
