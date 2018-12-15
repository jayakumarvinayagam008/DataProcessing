using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public interface IPrepareSearchSummaryBoard
    {
        SearchSummaryBoard GenerateSummary(List<BusinessToBusiness> response, long total);
    }
}
