using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface INumberLookupRepository
    {
        Task<bool> CreateAsync(NumberLookup numberLookup);

        Task<bool> CreateManyAsync(List<NumberLookup> numberLookups);

        Task<IEnumerable<NumberLookup>> GetNumberLookup();

        Task<long> GetTotalDocument();
    }

    public class NumberLookupRepository : INumberLookupRepository
    {
        private readonly IDataProcessingContext _context;

        public NumberLookupRepository(IDataProcessingContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(NumberLookup numberLookup)
        {
            _context
                .NumberLookups.InsertOne(numberLookup);
            return await Task.FromResult(true);
        }

        public async Task<bool> CreateManyAsync(List<NumberLookup> numberLookups)
        {
            await _context
                         .NumberLookups.InsertManyAsync(numberLookups);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<NumberLookup>> GetNumberLookup()
        {
            return await _context
                        .NumberLookups
                        .Find(_ => true)
                        .ToListAsync();
        }

        public async Task<long> GetTotalDocument()
        {
            var totalDocument = _context.NumberLookups.Find(_ => true).CountDocuments();
            return await Task.FromResult(totalDocument);
        }
    }
}