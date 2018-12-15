using DataProcessing.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerReadDataFromFile
    {
        (IEnumerable<CustomerDataModel>, int) ReadFileData(string filePath);
    }
}
