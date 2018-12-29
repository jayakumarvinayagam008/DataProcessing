using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerDataExport
    {
        string Export(List<CustomerData> businessToBusinesses, string fileRootPath, int range);
    }
}
