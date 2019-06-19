using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IB2BSearchRepository
    {
        Task<IEnumerable<BToBSearch>> Get();
        Task<bool> Update(
            IEnumerable<string> country,
            IEnumerable<string> state,
            IEnumerable<string> city,
            IEnumerable<string> area,
            IEnumerable<string> designation,
            IEnumerable<int> categoryId);
    }

    public class B2BSearchRepository : IB2BSearchRepository
    {
        private readonly IDataProcessingContext _context;
        public B2BSearchRepository(IDataProcessingContext context )
        {
            _context = context;
        }
        public async Task<IEnumerable<BToBSearch>> Get()
        {
            return await _context.B2BSearchItems
                                 .Find(_ => true)
                                 .ToListAsync();
        }
       
        public Task<bool> Update(
                IEnumerable<string> country,
                IEnumerable<string> state,
                IEnumerable<string> city,
                IEnumerable<string> area,
                IEnumerable<string> designation,
                IEnumerable<int> categoryId)
        {            
            var builder = Builders<BToBSearch>.Filter;
            var filter = builder.Eq("Label", "Search");
            var update = Builders<BToBSearch>.Update.Set("Country", country)
                .Set("State", state)
                .Set("City", city)
                .Set("Area", area)
                .Set("Designation", designation)
                .Set("BusinessCategory", categoryId); 
            _context.B2BSearchItems.UpdateOne(filter, update);
            return Task.FromResult(true);
        }
    }
}
