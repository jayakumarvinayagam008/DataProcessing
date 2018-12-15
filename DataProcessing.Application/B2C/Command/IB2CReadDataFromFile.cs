using DataProcessing.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2C.Command
{
    public interface IB2CReadDataFromFile
    {
        (IEnumerable<BusinessToCustomerModel>, int) ReadFileData(string filePath);
    }
}
