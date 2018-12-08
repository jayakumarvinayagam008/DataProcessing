using System;
using DataProcessing.CommonModels;

namespace DataProcessing.Application.B2C.Command
{
    public interface ISaveB2C
    {
        UploadSummary Save(string filePath);
    }
}
