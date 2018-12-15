using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public interface IBusinessToBusinessExport
    {
        string Export(List<BusinessToBusiness> businessToBusinesses, string fileRootPath, int range);
    }
}
