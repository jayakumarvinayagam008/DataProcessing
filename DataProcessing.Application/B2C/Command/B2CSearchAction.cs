using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.B2C.Command
{
    public class B2CSearchAction : IB2CSearchAction
    {
        public readonly IBusinessToCustomerRepository _businessToCustomerRepository;
        public readonly IPrepareBusinessToCustomerSummaryDashBoard _prepareBusinessToCustomerSummaryDashBoard;
        public readonly IB2CDataExport _b2CDataExport;
        
        public B2CSearchAction(IBusinessToCustomerRepository businessToCustomerRepository,
            IPrepareBusinessToCustomerSummaryDashBoard prepareBusinessToCustomerSummaryDashBoard,
            IB2CDataExport b2CDataExport)
        {
            _businessToCustomerRepository = businessToCustomerRepository;
            _prepareBusinessToCustomerSummaryDashBoard = prepareBusinessToCustomerSummaryDashBoard;
            _b2CDataExport = b2CDataExport;
        }
        

        public BusinessToCustomerSearchSumary Filter(B2CSearchFilter requestFilter, string rootPath, int range)
        {
            var tempResult = _businessToCustomerRepository.GetB2CDataSearch(new SearchFilterBlock
            {                
                Contries = (requestFilter.Contries != null) ? requestFilter.Contries : new List<string>(),
                Cities = (requestFilter.Cities != null) ? requestFilter.Cities : new List<string>(),
                Roles = (requestFilter.Roles != null) ? requestFilter.Roles : new List<string>(),
                //Age = (requestFilter.Age != null) ? requestFilter.Age : new List<string>(),
                Area = (requestFilter.Area != null) ? requestFilter.Area : new List<string>(),
                Salary = (requestFilter.Salary != null) ? requestFilter.Salary : new List<string>(),
                States = (requestFilter.States != null) ? requestFilter.States : new List<string>(),
                Experience = (requestFilter.Experience != null) ? requestFilter.Experience : new List<string>()
            });

            var response = tempResult;
            var dashBoard = _prepareBusinessToCustomerSummaryDashBoard.GenerateSummary(response.businessToCustomer);
            string fileId = string.Empty;
            if (tempResult.businessToCustomer.Count() > 0)
            {
                fileId = _b2CDataExport.Export(tempResult.businessToCustomer, rootPath, range);
            }
            dashBoard.SearchId = fileId;
            dashBoard.Total = tempResult.total;
            return dashBoard;
        }
    }
}