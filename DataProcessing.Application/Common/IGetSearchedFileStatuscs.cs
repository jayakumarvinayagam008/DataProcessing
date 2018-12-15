using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.Common
{
    public interface IGetSearchedFileStatuscs
    {
        bool FileExist(string searchRequistId, int fileType, string filePath);
    }
}
