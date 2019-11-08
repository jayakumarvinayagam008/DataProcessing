using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface INumberLookupResultRepository
    {
        Task<bool> CreateManyAsync(List<NumberLookupResult> numberLookups);
    }

    public class NumberLookupResultRepository : INumberLookupResultRepository
    {
        private readonly IDataProcessingContext _context;

        public NumberLookupResultRepository(IDataProcessingContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateManyAsync(List<NumberLookupResult> numberLookups)
        {
            await _context
                .NumberLookupResult.InsertManyAsync(numberLookups);
            return await Task.FromResult(true);
        }
    }
}
