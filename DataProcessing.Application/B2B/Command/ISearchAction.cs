using DataProcessing.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public interface ISearchAction
    {
        SearchSummaryBoard Filter(SearchFilter searchFilter, string rootPath, int range);
    }
}
