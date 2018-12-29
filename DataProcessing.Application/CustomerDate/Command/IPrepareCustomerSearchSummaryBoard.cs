using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.CustomerDate.Command
{
    public interface IPrepareCustomerSearchSummaryBoard
    {
        CustomerDataSearchSummary GenerateSummary(List<CustomerData> response);
    }
}
