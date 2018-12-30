using DataProcessing.Application.NumberLookup.Command;
using DataProcessing.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.NumberLookup.Query
{
    public class GetNumberLoopUpData : IGetNumberLoopUpData
    {
        public readonly INumberLookupRepository _numberLookupRepository;

        public GetNumberLoopUpData(INumberLookupRepository numberLookupRepository)
        {
            _numberLookupRepository = numberLookupRepository;
        }

        public IEnumerable<NumberLookUpDetail> FilterNumberLookUp(IEnumerable<Numbers> numberLookups)
        {
            var lookup = _numberLookupRepository.GetNumberLookup();
            lookup.Wait();
            var sourceLookUpData = lookup.Result;
            var numbersDetail = numberLookups.GroupJoin(sourceLookUpData,
                x => x.Series,
                y => y.Series,
                  (x, y) => new NumberLookUpDetail
                  {
                      Circle = y.Any() ? y.FirstOrDefault().Circle : string.Empty,
                      Operator = y.Any() ? y.FirstOrDefault().Operator : string.Empty,
                      Phone = x.PhoneNumber
                  }).ToList();
            return numbersDetail.AsEnumerable();
        }
    }
}