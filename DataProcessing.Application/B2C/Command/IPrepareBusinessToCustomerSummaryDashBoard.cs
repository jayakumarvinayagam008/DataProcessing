using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2C.Command
{
    public interface IPrepareBusinessToCustomerSummaryDashBoard
    {
        BusinessToCustomerSearchSumary GenerateSummary(List<BusinessToCustomer> response);
    }
}
