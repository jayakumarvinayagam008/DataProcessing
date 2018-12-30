using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System.Collections.Generic;

namespace DataProcessing.Application.B2B.Command
{
    public interface IPrepareSearchSummaryBoard
    {
        SearchSummaryBoard GenerateSummary(List<BusinessToBusiness> response, long total);
    }
}