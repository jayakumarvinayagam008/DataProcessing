using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.B2B.Command
{
    public class SearchAction : ISearchAction
    {
        private readonly IBusinessToBusinessRepository _businessToBusinessRepository;
        private readonly IPrepareSearchSummaryBoard _prepareSearchSummaryBoard;
        private readonly IBusinessToBusinessExport _businessToBusinessExport;

        public SearchAction(IBusinessToBusinessRepository businessToBusinessRepository,
            IPrepareSearchSummaryBoard prepareSearchSummaryBoard, IBusinessToBusinessExport businessToBusinessExport)
        {
            _businessToBusinessRepository = businessToBusinessRepository;
            _prepareSearchSummaryBoard = prepareSearchSummaryBoard;
            _businessToBusinessExport = businessToBusinessExport;
        }

        public SearchSummaryBoard Filter(SearchFilter searchFilter, string rootPath, int range, int zipFileRange =0)
        {
            var tempResult = _businessToBusinessRepository.GetB2BSearch(new B2BFilter
            {
                Area = (searchFilter.Area != null) ? searchFilter.Area : new List<string>(),
                BusinessCategoryId = (searchFilter.BusinessCategoryId != null) ? searchFilter.BusinessCategoryId : new List<int?>(),
                Cities = (searchFilter.Cities != null) ? searchFilter.Cities : new List<string>(),
                Contries = (searchFilter.Contries != null) ? searchFilter.Contries : new List<string>(),
                Designation = (searchFilter.Designation != null) ? searchFilter.Designation : new List<string>(),
                States = (searchFilter.States != null) ? searchFilter.States : new List<string>()
            });
            var response = tempResult;
            var dashBoard = _prepareSearchSummaryBoard.GenerateSummary(response.BusinessToBusinesses, response.Total);
            Tuple<string, string> fileId = null;
            if (tempResult.BusinessToBusinesses.Count() > 0)
            {
                fileId = _businessToBusinessExport.Export(tempResult.BusinessToBusinesses, rootPath, range, zipFileRange);
            }
            dashBoard.SearchId = fileId.Item1;
            dashBoard.Total = tempResult.Total;
            dashBoard.SearchCount = tempResult.BusinessToBusinesses.Count();
            dashBoard.SearchCsvId = fileId.Item2;
            return dashBoard;
        }
    }
}