using DataProcessing.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface ISaveNumberLookupResult
    {
        void SaveResult(IList<NumberLookupResult> numberLookupResults);
    }

    public class SaveNumberLookupResult : ISaveNumberLookupResult
    {
        private readonly INumberLookupResultRepository _numberLookupResultRepository = null;
        public SaveNumberLookupResult(INumberLookupResultRepository numberLookupResultRepository)
        {
            _numberLookupResultRepository = numberLookupResultRepository;
        }
        public async void SaveResult(IList<NumberLookupResult> numberLookupResults)
        {
            await _numberLookupResultRepository.CreateManyAsync(numberLookupResults.ToList());
        }
    }
}
