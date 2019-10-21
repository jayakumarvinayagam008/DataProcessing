using DataProcessing.Persistence;
using System;
using System.Collections.Generic;

namespace DataProcessing.Application.B2B.Command
{
    public interface IBusinessToBusinessExport
    {
        Tuple<string, string> Export(List<BusinessToBusiness> businessToBusinesses, string fileRootPath, int range, int zipFileRange=0);
    }
}