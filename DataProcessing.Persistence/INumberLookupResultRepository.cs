using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface INumberLookupResultRepository
    {
        Task<bool> CreateManyAsync(List<NumberLookupResult> numberLookups);
        Task<IEnumerable<NumberLookupResult>> Filter(string filterExpression);
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

        public async Task<IEnumerable<NumberLookupResult>> Filter(string filterExpression)
        {
            var builder = Builders<NumberLookupResult>.Filter;
            var filter = builder.Eq("SearchId", filterExpression);
            var searchResult = _context.NumberLookupResult.Find(filter).ToList<NumberLookupResult>();
            return await Task.FromResult(searchResult);
        }
    }
}
