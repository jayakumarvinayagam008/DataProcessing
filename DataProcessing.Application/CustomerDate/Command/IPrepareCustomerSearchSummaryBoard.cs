using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System.Collections.Generic;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface IPrepareCustomerSearchSummaryBoard
    {
        CustomerDataSearchSummary GenerateSummary(List<CustomerData> response);
    }
}