using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;

namespace DataProcessing.Application.B2C.Command
{
    public class PrepareBusinessToCustomerSummaryDashBoard : IPrepareBusinessToCustomerSummaryDashBoard
    {
        public BusinessToCustomerSearchSumary GenerateSummary(List<BusinessToCustomer> response)
        {
            var searchTotoal = response.Count();
            BusinessToCustomerSearchSumary searchSummaryBoard = new BusinessToCustomerSearchSumary();
            if (searchTotoal > 0)
            {
                searchSummaryBoard.SearchCount = searchTotoal;
                searchSummaryBoard.Country = (response.Select(x => !string.IsNullOrWhiteSpace(x.Country)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.City = (response.Select(x => !string.IsNullOrWhiteSpace(x.City)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Roles = (response.Select(x => !string.IsNullOrWhiteSpace(x.Roles)).Count() / (decimal)searchTotoal) * 100;
                //searchSummaryBoard.Age = (response.Select(x => !string.IsNullOrWhiteSpace(x.Dob)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.State = (response.Select(x => !string.IsNullOrWhiteSpace(x.State)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Area = (response.Select(x => !string.IsNullOrWhiteSpace(x.Area)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Salary = (response.Select(x => !string.IsNullOrWhiteSpace(x.AnnualSalary)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Experience = (response.Select(x => !string.IsNullOrWhiteSpace(x.Experience)).Count() / (decimal)searchTotoal) * 100;
            }
            return searchSummaryBoard;
        }
    }
}
