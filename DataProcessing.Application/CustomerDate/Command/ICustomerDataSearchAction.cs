using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerDataSearchAction
    {
        CustomerDataSearchSummary Filter(RequestFilter requestFilter, string rootPath, int range);
    }
}
