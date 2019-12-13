using MongoDB.Bson;
using MongoDB.Driver;
using System;
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
        Task<bool> Remove(string _id);
        Task<bool> UpdateAsync(NumberLookup numberLookup, string _id);
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

        public async Task<bool> Remove(string _id)
        {
            var builder = Builders<NumberLookup>.Filter;
            var filter = builder.Eq("_id", _id);            
            var deleteResult = _context.NumberLookups.DeleteOne(x=> x.Id == ObjectId.Parse(_id));
            return await Task.FromResult(deleteResult.DeletedCount > 0);
        }

        public async Task<bool> UpdateAsync(NumberLookup numberLookup, string _id)
        {
            FilterDefinition<NumberLookup> filterDefinition = new BsonDocument("_id", ObjectId.Parse(_id));            
            var updateDefinition = Builders<NumberLookup>.Update
               .Set(m => m.Operator, numberLookup.Operator)
               .Set(p => p.Series, numberLookup.Series)
               .Set(p => p.Circle, numberLookup.Circle)
               .Set(p => p.CreatedDate, numberLookup.CreatedDate)
               .Set(p => p.CreatedBy, numberLookup.CreatedBy);            
            var updateResult = await _context.NumberLookups.UpdateOneAsync(filterDefinition, updateDefinition);
            return await Task.FromResult(updateResult.ModifiedCount > 0);
        }
    }
}