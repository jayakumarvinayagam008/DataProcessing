using DataProcessing.CommonModels;
using System.Collections.Generic;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CReadDataFromFile
    {
        (IEnumerable<BusinessToCustomerModel>, int) ReadFileData(string filePath);
    }
}