using DataProcessing.Application.Common;
using DataProcessing.CommonModels;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface ICustomerDataSearchAction
    {
        CustomerDataSearchSummary Filter(RequestFilter requestFilter, string rootPath, int range, int zipFileRange = 0);
    }
}