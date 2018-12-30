using DataProcessing.Persistence;
using System.Collections.Generic;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerDataExport
    {
        string Export(List<CustomerData> businessToBusinesses, string fileRootPath, int range);
    }
}