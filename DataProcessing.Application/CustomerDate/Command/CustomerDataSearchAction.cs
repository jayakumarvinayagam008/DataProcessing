using DataProcessing.Application.Common;
using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.CustomerDate.Command
{
    public class CustomerDataSearchAction : ICustomerDataSearchAction
    {
        private readonly ICustomerDataRepository _customerDataRepository;
        private readonly IPrepareCustomerSearchSummaryBoard _prepareSearchSummaryBoard;
        private readonly ICustomerDataExport _customerDataExport;

        public CustomerDataSearchAction(ICustomerDataRepository customerDataRepository,
            IPrepareCustomerSearchSummaryBoard prepareSearchSummaryBoard,
            ICustomerDataExport customerDataExport)
        {
            _customerDataRepository = customerDataRepository;
            _prepareSearchSummaryBoard = prepareSearchSummaryBoard;
            _customerDataExport = customerDataExport;
        }

        public CustomerDataSearchSummary Filter(RequestFilter requestFilter, string rootPath, int range, int zipFileRange = 0)
        {
            //SearchFilterBlock

            var tempResult = _customerDataRepository.GetCustomerDataSearch(new SearchFilterBlock
            {
                BusinessVertical = (requestFilter.BusinessVertical != null) ? requestFilter.BusinessVertical : new List<string>(),
                Cities = (requestFilter.Cities != null) ? requestFilter.Cities : new List<string>(),
                ClientName = (requestFilter.CustomerName != null) ? requestFilter.CustomerName : new List<string>(),
                Contries = (requestFilter.Contries != null) ? requestFilter.Contries : new List<string>(),
                DataQuality = (requestFilter.DataQuality != null) ? requestFilter.DataQuality : new List<string>(),
                Network = (requestFilter.Network != null) ? requestFilter.Network : new List<string>(),
                States = (requestFilter.States != null) ? requestFilter.States : new List<string>()
            });
            var response = tempResult;

            var dashBoard = _prepareSearchSummaryBoard.GenerateSummary(response.customerDatas);
            Tuple<string, string> fileId = null;
            if (tempResult.customerDatas.Count() > 0)
            {
                fileId = _customerDataExport.Export(tempResult.customerDatas, rootPath, range, zipFileRange);
                dashBoard.SearchId = fileId.Item1;
                dashBoard.SearchCsvId = fileId.Item2;
            }
            dashBoard.Total = tempResult.Total;
            dashBoard.SearchCount = tempResult.customerDatas.Count();
            return dashBoard;
        }
    }
}