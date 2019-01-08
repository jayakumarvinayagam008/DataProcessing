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
                searchSummaryBoard.Name = (response.Select(x => !string.IsNullOrWhiteSpace(x.Name)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Dob = (response.Where(x=>x.Dob != null).Select(x => x.Dob).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Qualification = (response.Select(x => !string.IsNullOrWhiteSpace(x.Qualification)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Experience = (response.Select(x => !string.IsNullOrWhiteSpace(x.Experience)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Employer = (response.Select(x => !string.IsNullOrWhiteSpace(x.Employer)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.KeySkills = (response.Select(x => !string.IsNullOrWhiteSpace(x.KeySkills)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Location = (response.Select(x => !string.IsNullOrWhiteSpace(x.Location)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Roles = (response.Select(x => !string.IsNullOrWhiteSpace(x.Roles)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Industry = (response.Select(x => !string.IsNullOrWhiteSpace(x.Industry)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Address = (response.Select(x => !string.IsNullOrWhiteSpace(x.Address)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Address2 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Address2)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Email = (response.Select(x => !string.IsNullOrWhiteSpace(x.Email)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.PhoneNew = (response.Select(x => !string.IsNullOrWhiteSpace(x.PhoneNew)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.MobileNew = (response.Select(x => !string.IsNullOrWhiteSpace(x.MobileNew)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Mobile2 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Mobile2)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.AnnualSalary = (response.Select(x => !string.IsNullOrWhiteSpace(x.AnnualSalary)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Pincode = (response.Select(x => !string.IsNullOrWhiteSpace(x.Pincode)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Area = (response.Select(x => !string.IsNullOrWhiteSpace(x.Area)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.City = (response.Select(x => !string.IsNullOrWhiteSpace(x.City)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.State = (response.Select(x => !string.IsNullOrWhiteSpace(x.State)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Country = (response.Select(x => !string.IsNullOrWhiteSpace(x.Country)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Network = (response.Select(x => !string.IsNullOrWhiteSpace(x.Network)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Gender = (response.Select(x => !string.IsNullOrWhiteSpace(x.Gender)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Caste = (response.Select(x => !string.IsNullOrWhiteSpace(x.Caste)).Count() / (decimal)searchTotoal) * 100;
            }
            return searchSummaryBoard;
        }
    }
}
