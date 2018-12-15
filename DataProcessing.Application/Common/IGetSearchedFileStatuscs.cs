using DataProcessing.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.Common
{
    public interface IGetSearchedFileStatuscs
    {
        FileAvailable FileExist(string searchRequistId, int fileType, string filePath);
    }
}
