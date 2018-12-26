﻿using DataProcessing.CommonModels;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public SearchSummaryBoard Filter(SearchFilter searchFilter, string rootPath, int range)
        {            
            var tempResult = _businessToBusinessRepository.GetB2BSearch(new B2BFilter
            {
                Area = searchFilter.Area,
                BusinessCategoryId = searchFilter.BusinessCategoryId,
                Cities = searchFilter.Cities,
                Contries = searchFilter.Contries,
                Designation = searchFilter.Designation,
                States = searchFilter.States
            });
            var response = tempResult;
            var dashBoard = _prepareSearchSummaryBoard.GenerateSummary(response.BusinessToBusinesses, response.Total);
            string fileId = string.Empty;
            if (tempResult.BusinessToBusinesses.Count()>0)
            {
                fileId = _businessToBusinessExport.Export(tempResult.BusinessToBusinesses, rootPath, range);
            }           
            dashBoard.SearchId = fileId;
            dashBoard.Total = tempResult.Total;
            dashBoard.SearchCount = tempResult.BusinessToBusinesses.Count();
            return dashBoard;
        }
    }
}