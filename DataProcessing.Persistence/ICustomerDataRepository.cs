using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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
        Task<IEnumerable<CustomerData>> Filter(string refId);
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

        public async Task<IEnumerable<CustomerData>> Filter(string refId)
        {
            var builder = Builders<CustomerData>.Filter;
            var filter = builder.Eq("RefId", refId);
            var projection = Builders<CustomerData>
                .Projection.Include("Country")
                .Include("State")
                .Include("ClientCity")
                .Include("ClientBusinessVertical")
                .Include("Operator")
                .Include("Dbquality")
                .Include("ClientName")
                .Exclude("_id");

            var searchResult = _context.CustomerDatas.Find(filter)
                .Project(projection).ToList().Select(x => new CustomerData
                {
                    Country = x.GetValue("Country").AsString,
                    State = x.GetValue("State").AsString,
                    ClientCity = x.GetValue("ClientCity").AsString,
                    ClientBusinessVertical = x.GetValue("ClientBusinessVertical").AsString,
                    Operator = x.GetValue("Operator").AsString,
                    Dbquality = x.GetValue("Dbquality").AsString,
                    ClientName = x.GetValue("ClientName").AsString
                }).ToList<CustomerData>();
            return await Task.FromResult(searchResult);
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
            var searchItems = _context.UserDataSearchItems
                               .Find(_ => true)
                               .ToListAsync()
                               .Result;
            var country = searchItems.Select(x => x.Country).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var city = searchItems.Select(x => x.City).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var state = searchItems.Select(x => x.State).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var network = searchItems.Select(x => x.Network).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var businessVertical = searchItems.Select(x => x.BusinessVertical).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var clientName = searchItems.Select(x => x.Customer).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            
            SearchBlock searchBlock = new SearchBlock();
            //var country = _context.CustomerDatas
            //    .AsQueryable()
            //    .Select(x => x.Country).Where(x => x != "" && x != null)
            //    .Distinct().ToList();
            //var city = _context.CustomerDatas
            //    .AsQueryable()
            //    .Select(x => x.ClientCity).Where(x => x != "" && x != null)
            //    .Distinct().ToList();
            //var state = _context.CustomerDatas
            //    .AsQueryable()
            //    .Select(x => x.State).Where(x => x != "" && x != null)
            //    .Distinct().ToList();
            //var network = _context.CustomerDatas
            //    .AsQueryable()
            //    .Select(x => x.Operator).Where(x => x != "" && x != null)
            //    .Distinct().ToList();
            //var businessVertical = _context.CustomerDatas
            //    .AsQueryable()
            //    .Select(x => x.ClientBusinessVertical).Where(x => x != "" && x != null)
            //    .Distinct().ToList();
            //var clientName = _context.CustomerDatas
            //    .AsQueryable()
            //    .Select(x => x.ClientName).Where(x => x != "" && x != null)
            //    .Distinct().ToList();
            //var dbQuality = _context.CustomerDatas
            //   .AsQueryable()
            //   .Select(x => x.Dbquality).Where(x => x != "" && x != null)
            //   .Distinct().ToList();
            IEnumerable<string> empty = new List<string>();
            searchBlock.Country = country.Any() ? country : empty;
            searchBlock.State = state.Any() ? state : empty;
            searchBlock.City = city.Any() ? city : empty;
            searchBlock.NetWork = network.Any() ? network : empty;
            searchBlock.BusinessVertical = businessVertical.Any() ? businessVertical : empty;
            searchBlock.ClientName = clientName.Any() ? clientName : empty;

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