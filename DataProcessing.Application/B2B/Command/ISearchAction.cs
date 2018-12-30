using DataProcessing.CommonModels;

namespace DataProcessing.Application.B2B.Command
{
    public interface ISearchAction
    {
        SearchSummaryBoard Filter(SearchFilter searchFilter, string rootPath, int range);
    }
}