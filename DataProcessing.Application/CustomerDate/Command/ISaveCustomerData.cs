using DataProcessing.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ISaveCustomerData
    {
        UploadSummary Save(string filePath);
    }
}
