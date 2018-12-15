using DataProcessing.Application.B2B.Command;
using DataProcessing.CommonModels;
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
        public FileAvailable FileExist(string searchRequistId, int fileType, string filePath)
        {
            var fileExist = new FileAvailable() {

            };
            var status = _downloadRequestRepository.GetDownloadRequestDetail(searchRequistId, fileType);
            status.Wait();
            if (status.Result != null && status.Result.StatusCode == (int)FileCreateStatus.Completed && File.Exists(filePath))
                return new FileAvailable() { IsAvailable = true, Message = string.Empty };
            return new FileAvailable() { IsAvailable = false, Message = MessageContainer.SearchFile };

        }
    }
}
