using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface ICustomerDataSearchRepository
    {
        Task<IEnumerable<CustomerDataSearch>> Get();
        Task<bool> Update(
            IEnumerable<string> country,
            IEnumerable<string> state,
            IEnumerable<string> city,
            IEnumerable<string> businessVertical,
            IEnumerable<string> network,
            IEnumerable<string> customer,
            IEnumerable<string> DataQuality);
    }

    public class CustomerDataSearchRepository : ICustomerDataSearchRepository
    {
        private readonly IDataProcessingContext _context;
        public CustomerDataSearchRepository(IDataProcessingContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomerDataSearch>> Get()
        {
            return await _context.UserDataSearchItems
                                 .Find(_ => true)
                                 .ToListAsync();
        }

        public Task<bool> Update(IEnumerable<string> country, IEnumerable<string> state,
            IEnumerable<string> city, IEnumerable<string> businessVertical,
            IEnumerable<string> network, IEnumerable<string> customer,
            IEnumerable<string> DataQuality)
        {
            var builder = Builders<CustomerDataSearch>.Filter;
            var filter = builder.Eq("Label", "Search");
            var update = Builders<CustomerDataSearch>.Update.Set("Country", country)
                .Set("State", state)
                .Set("City", city)
                .Set("BusinessVertical", businessVertical)
                .Set("Network", network)
                .Set("Customer", customer)
                .Set("DataQuality", DataQuality);
            _context.UserDataSearchItems.UpdateOne(filter, update);
            return Task.FromResult(true);
        }
    }


}
