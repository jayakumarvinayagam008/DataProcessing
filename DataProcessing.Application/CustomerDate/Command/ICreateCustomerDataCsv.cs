using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System.Collections.Generic;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICreateCustomerDataCsv
    {
        void Create(List<CustomerDataExportModel> customerData, string filePath, DownloadRequest downloadRequest);
    }
}