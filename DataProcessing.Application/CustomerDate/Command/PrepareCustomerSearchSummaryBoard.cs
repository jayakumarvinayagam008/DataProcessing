using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.CustomerDate.Command
{
    public class PrepareCustomerSearchSummaryBoard : IPrepareCustomerSearchSummaryBoard
    {
        public CustomerDataSearchSummary GenerateSummary(List<CustomerData> response)
        {
            var searchTotoal = response.Count();
            CustomerDataSearchSummary searchSummaryBoard = new CustomerDataSearchSummary();
            searchSummaryBoard.SearchCount = searchTotoal;
            //if (searchTotoal > 0)
            //{
            //    searchSummaryBoard.SearchCount = searchTotoal;
            //    searchSummaryBoard.Circle = (response.Select(x => !string.IsNullOrWhiteSpace(x.Circle)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.ClientBusinessVertical = (response.Select(x => !string.IsNullOrWhiteSpace(x.ClientBusinessVertical)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.ClientCity = (response.Select(x => !string.IsNullOrWhiteSpace(x.ClientCity)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.ClientName = (response.Select(x => !string.IsNullOrWhiteSpace(x.ClientName)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.Country = (response.Select(x => !string.IsNullOrWhiteSpace(x.Country)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.DateOfUse = (response.Select(x => !string.IsNullOrWhiteSpace(x.DateOfUse)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.Dbquality = (response.Select(x => !string.IsNullOrWhiteSpace(x.Dbquality)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.Numbers = (response.Select(x => !string.IsNullOrWhiteSpace(x.Numbers)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.Operator = (response.Select(x => !string.IsNullOrWhiteSpace(x.Operator)).Count() / (decimal)searchTotoal) * 100;
            //    searchSummaryBoard.State = (response.Select(x => !string.IsNullOrWhiteSpace(x.State)).Count() / (decimal)searchTotoal) * 100;
            //}
            return searchSummaryBoard;
        }
    }
}