using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IB2CSearchRepository
    {
        Task<IEnumerable<BToCSearch>> Get();
        Task<bool> Update(
            IEnumerable<string> country,
            IEnumerable<string> state,
            IEnumerable<string> city,
            IEnumerable<string> area,
            IEnumerable<string> roles,
            IEnumerable<string> salary,
            IEnumerable<string> experience);
    }

    public class B2CSearchRepository : IB2CSearchRepository
    {
        private readonly IDataProcessingContext _context;
        public B2CSearchRepository(IDataProcessingContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BToCSearch>> Get()
        {
            return await _context.B2CSearchItems
                                  .Find(_ => true)
                                  .ToListAsync();
        }

        public Task<bool> Update(IEnumerable<string> country, IEnumerable<string> state, IEnumerable<string> city, IEnumerable<string> area, IEnumerable<string> roles, IEnumerable<string> salary, IEnumerable<string> experience)
        {
            var builder = Builders<BToCSearch>.Filter;
            var filter = builder.Eq("Label", "Search");
            var update = Builders<BToCSearch>.Update.Set("Country", country)
                .Set("State", state)
                .Set("City", city)
                .Set("Area", area)
                .Set("Roles", roles)
                .Set("Experience", experience)
                .Set("Salary", salary);            
            _context.B2CSearchItems.UpdateOne(filter, update);
            return Task.FromResult(true);
        }
    }
}