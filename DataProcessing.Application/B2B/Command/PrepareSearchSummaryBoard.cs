using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;

namespace DataProcessing.Application.B2B.Command
{
    public class PrepareSearchSummaryBoard : IPrepareSearchSummaryBoard
    {
        public SearchSummaryBoard GenerateSummary(List<BusinessToBusiness> response, long total)
        {
            var searchTotoal = response.Count();
            SearchSummaryBoard searchSummaryBoard = new SearchSummaryBoard();
            if (searchTotoal > 0)
            {
                searchSummaryBoard.Add1 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Add1)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Add2 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Add2)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Area = (response.Select(x => !string.IsNullOrWhiteSpace(x.Area)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.CategoryId = (response.Select(x => x.CategoryId > 0).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.City = (response.Select(x => !string.IsNullOrWhiteSpace(x.City)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.CompanyName = (response.Select(x => !string.IsNullOrWhiteSpace(x.CompanyName)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.ContactPerson = (response.Select(x => !string.IsNullOrWhiteSpace(x.ContactPerson)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Contactperson1 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Contactperson1)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Country = (response.Select(x => !string.IsNullOrWhiteSpace(x.Country)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Designation = (response.Select(x => !string.IsNullOrWhiteSpace(x.Designation)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Designation1 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Designation1)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Email = (response.Select(x => !string.IsNullOrWhiteSpace(x.Email)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Email1 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Email1)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.EstYear = (response.Select(x => x.EstYear > 0).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.LandMark = (response.Select(x => !string.IsNullOrWhiteSpace(x.LandMark)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Mobile1 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Mobile1)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Mobile2 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Mobile2)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.MobileNew = (response.Select(x => !string.IsNullOrWhiteSpace(x.Mobile_New)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.NoOfEmp = (response.Select(x => x.NoOfEmp > 0).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Phone1 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Phone1)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Phone2 = (response.Select(x => !string.IsNullOrWhiteSpace(x.Phone2)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.PhoneNew = (response.Select(x => !string.IsNullOrWhiteSpace(x.Phone_New)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Pincode = (response.Select(x => !string.IsNullOrWhiteSpace(x.Pincode)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.State = (response.Select(x => !string.IsNullOrWhiteSpace(x.State)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Web = (response.Select(x => !string.IsNullOrWhiteSpace(x.Web)).Count() / (decimal)searchTotoal) * 100;
                searchSummaryBoard.Total = searchTotoal;
            }
            return searchSummaryBoard;
        }
    }
}
