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
        (List<CustomerData> customerDatas, long Total) GetCustomerDataSearch(SearchFilterBlock searchFilterBlock);
        Task<long> GetTotalDocument();
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

        public (List<CustomerData> customerDatas, long Total) GetCustomerDataSearch(SearchFilterBlock searchFilterBlock)
        {
            var filter = Builders<CustomerData>.Filter.Empty;

            var totalDocuments = _context.CustomerDatas.Find(_ => true).CountDocuments();

            if (searchFilterBlock.Contries.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<CustomerData>.Filter.In("Country", searchFilterBlock.Contries);

            if (searchFilterBlock.States.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<CustomerData>.Filter.In("State", searchFilterBlock.States);

            if (searchFilterBlock.Cities.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<CustomerData>.Filter.In("ClientCity", searchFilterBlock.Cities);

            if (searchFilterBlock.BusinessVertical.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<CustomerData>.Filter.In("ClientBusinessVertical", searchFilterBlock.BusinessVertical);

            if (searchFilterBlock.DataQuality.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<CustomerData>.Filter.In("Dbquality", searchFilterBlock.DataQuality);

            if (searchFilterBlock.Network.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<CustomerData>.Filter.In("Operator", searchFilterBlock.Network);

            if (searchFilterBlock.ClientName.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<CustomerData>.Filter.In("ClientName", searchFilterBlock.ClientName);

            var searchResult = _context.CustomerDatas.Find(filter).ToList();

            return (searchResult, totalDocuments);
        }

        public Task<SearchBlock> GetFilterBlocks()
        {
            SearchBlock searchBlock = new SearchBlock();
            var country = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.Country).Where(x => x != "" && x != null)
                .Distinct().ToList();
            var city = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.ClientCity).Where(x => x != "" && x != null)
                .Distinct().ToList();
            var state = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.State).Where(x => x != "" && x != null)
                .Distinct().ToList();
            var network = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.Operator).Where(x => x != "" && x != null)
                .Distinct().ToList();
            var businessVertical = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.ClientBusinessVertical).Where(x => x != "" && x != null)
                .Distinct().ToList();
            var clientName = _context.CustomerDatas
                .AsQueryable()
                .Select(x => x.ClientName).Where(x => x != "" && x != null)
                .Distinct().ToList();
            var dbQuality = _context.CustomerDatas
               .AsQueryable()
               .Select(x => x.Dbquality).Where(x => x != "" && x != null)
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

        public async Task<long> GetTotalDocument()
        {
            var totalDocument = _context.CustomerDatas.Find(_ => true).CountDocuments();
            return await Task.FromResult(totalDocument);
        }
    }
}
