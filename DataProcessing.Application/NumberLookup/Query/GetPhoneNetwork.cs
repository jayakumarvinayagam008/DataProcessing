using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Query
{
    public class GetPhoneNetwork : IGetPhoneNetwork
    {
        public readonly INumberLookupResultRepository _numberLookupResultRepository;
        public GetPhoneNetwork(INumberLookupResultRepository numberLookupResultRepository)
        {
            _numberLookupResultRepository = numberLookupResultRepository;
        }

        public IList<NumberLookUpDetail> Get(string searchId, string[] operators)
        {
            var numberResult = _numberLookupResultRepository.Filter(searchId).Result;
            return numberResult.Select(x => new NumberLookUpDetail
            {
                Circle = x.Circle,
                Operator = x.Operator,
                Phone = x.Phone
            }).Where(x => operators.Contains(x.Operator)).ToList();
        }
    }
}
