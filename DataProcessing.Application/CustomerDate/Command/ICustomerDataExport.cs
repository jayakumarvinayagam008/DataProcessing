using DataProcessing.Persistence;
using System;
using System.Collections.Generic;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerDataExport
    {
        Tuple<string, string> Export(List<CustomerData> businessToBusinesses, string fileRootPath, int range, int zipFileRange = 0);
    }
}