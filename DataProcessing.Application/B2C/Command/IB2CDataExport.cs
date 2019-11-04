using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CDataExport
    {
        Tuple<string, string> Export(List<BusinessToCustomer> businessToBusinesses, string fileRootPath, int range, int zipFileRange = 0);
    }
}
