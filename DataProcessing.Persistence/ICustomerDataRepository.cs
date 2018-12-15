using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface ICustomerDataRepository
    {
        Task<IEnumerable<string>> GetPhoneNewAsync();
        Task<bool> CreateManyAsync(IEnumerable<CustomerData> saveToSource);
        Task<SearchBlock> GetFilterBlocks();
    }

    public class CustomerDataRepository : ICustomerDataRepository
    {
        private readonly IDataProcessingContext _context;
        public CustomerDataRepository(IDataProcessingContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateManyAsync(IEnumerable<CustomerData> saveToSource)
        {
            await _context
                    .CustomerDatas.InsertManyAsync(saveToSource);
            return await Task.FromResult(true);
        }

        public Task<SearchBlock> GetFilterBlocks()
        {
            SearchBlock searchBlock = new SearchBlock();
            var country = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.Country).Where(x => x != "")
                .Distinct().ToList();
            var city = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.ClientCity).Where(x => x != "")
                .Distinct().ToList();
            var state = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.State).Where(x => x != "")
                .Distinct().ToList();
            var network = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.Operator).Where(x => x != "")
                .Distinct().ToList();
            var businessVertical = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.ClientBusinessVertical).Where(x => x != "")
                .Distinct().ToList();
            var clientName = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.ClientName).Where(x => x != "")
                .Distinct().ToList();
            var dbQuality = _context.CustomerDatas
               .AsQueryable()
               .Select(x => x.Dbquality).Where(x => x != "")
               .Distinct().ToList();

            searchBlock.Country = country;
            searchBlock.State = state;
            searchBlock.City = city;
            searchBlock.NetWork = network;
            searchBlock.BusinessVertical = businessVertical;
            searchBlock.ClientName = clientName;
            searchBlock.Dbquality = dbQuality;

            return Task.FromResult(searchBlock);
        }

        public async Task<IEnumerable<string>> GetPhoneNewAsync()
        {
            FilterDefinition<CustomerData> filter = Builders<CustomerData>
                .Filter.Ne(m => m.Numbers, string.Empty);
            var result = await _context
                    .CustomerDatas
                    .Find(filter)
                    .Project(u => u.Numbers).ToListAsync();

            return result;
        }
    }
}
