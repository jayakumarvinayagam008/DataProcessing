using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Query
{
    public class GetOperators : IGetOperators
    {
        public readonly INumberLookupRepository _numberLookupRepository;
        public GetOperators(INumberLookupRepository numberLookupRepository)
        {
            _numberLookupRepository = numberLookupRepository;
        }

        public IEnumerable<string> Get()
        {
            var lookup = _numberLookupRepository.GetNumberLookup();
            lookup.Wait();
            var operators = lookup.Result.Select(x => x.Operator).Distinct().OrderBy(x => x);
            return operators;
        }
    }
}
