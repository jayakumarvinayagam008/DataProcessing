using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public class SearchAction : ISearchAction
    {
        private readonly IBusinessToBusinessRepository _businessToBusinessRepository;
        public SearchAction(IBusinessToBusinessRepository businessToBusinessRepository )
        {
            _businessToBusinessRepository = businessToBusinessRepository;
        }
        public void Filter(SearchFilter searchFilter)
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
            tempResult.Wait();
            var response = tempResult.Result;


        }
    }
}
