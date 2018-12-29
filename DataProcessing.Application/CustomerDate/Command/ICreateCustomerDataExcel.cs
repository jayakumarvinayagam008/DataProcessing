using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System.Collections.Generic;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICreateCustomerDataExcel
    {
        void Create(List<CustomerDataExportModel> customerData, string filePath, int range, DownloadRequest downloadRequest);
    }
}