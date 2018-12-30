using DataProcessing.CommonModels;
using System.Collections.Generic;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerReadDataFromFile
    {
        (IEnumerable<CustomerDataModel>, int) ReadFileData(string filePath);
    }
}