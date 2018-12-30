using DataProcessing.Persistence;
using System.Collections.Generic;

namespace DataProcessing.Application.B2B.Command
{
    public interface IBusinessToBusinessExport
    {
        string Export(List<BusinessToBusiness> businessToBusinesses, string fileRootPath, int range);
    }
}