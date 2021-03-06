﻿using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System.IO;

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
            var status = _downloadRequestRepository.GetDownloadRequestDetail(searchRequistId, fileType);
            status.Wait();
            if (status.Result != null && status.Result.StatusCode == (int)FileCreateStatus.Completed 
                && (File.Exists(filePath) || Directory.Exists(Path.ChangeExtension(filePath, null))))
                return new FileAvailable() { IsAvailable = true, Message = string.Empty };
            return new FileAvailable() { IsAvailable = false, Message = MessageContainer.SearchFile };
        }
    }
}