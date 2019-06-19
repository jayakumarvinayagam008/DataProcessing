using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IBusinessToCustomerRepository
    {
        Task<IEnumerable<string>> GetPhoneNewAsync();

        Task<bool> CreateManyAsync(IEnumerable<BusinessToCustomer> saveToSource);

        Task<SearchBlock> GetFilterBlocks();

        Task<long> GetTotalDocument();
        (List<BusinessToCustomer> businessToCustomer, long total) GetB2CDataSearch(SearchFilterBlock searchFilterBlock);

        Task<IEnumerable<BusinessToCustomer>> Filter(string refId);
    }

    public class BusinessToCustomerRepository : IBusinessToCustomerRepository
    {
        private readonly IDataProcessingContext _context;

        public BusinessToCustomerRepository(IDataProcessingContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateManyAsync(IEnumerable<BusinessToCustomer> saveToSource)
        {
            await _context
                    .BusinessToCustomers.InsertManyAsync(saveToSource);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<BusinessToCustomer>> Filter(string refId)
        {
            var builder = Builders<BusinessToCustomer>.Filter;
            var filter = builder.Eq("RefId", refId);
            var projection = Builders<BusinessToCustomer>
                .Projection.Include("Country")
                .Include("State")
                .Include("City")
                .Include("Area")
                .Include("Roles")
                .Include("AnnualSalary")
                .Include("Experience")
                .Exclude("_id");

            var searchResult = _context.BusinessToCustomers.Find(filter)
                .Project(projection).ToList().Select(x => new BusinessToCustomer
                {
                    Country = x.GetValue("Country").AsString,
                    State = x.GetValue("State").AsString,
                    City = x.GetValue("City").AsString,
                    Area = x.GetValue("Area").AsString,
                    Roles = x.GetValue("Roles").AsString,
                    AnnualSalary = x.GetValue("AnnualSalary").AsString,
                    Experience = x.GetValue("Experience").AsString
                }).ToList<BusinessToCustomer>();


            return await Task.FromResult(searchResult);
        }

        public (List<BusinessToCustomer> businessToCustomer, long total) GetB2CDataSearch(SearchFilterBlock searchFilterBlock)
        {
            var filter = Builders<BusinessToCustomer>.Filter.Empty;

            var totalDocuments = GetTotalDocument().Result;

            if (searchFilterBlock.Contries.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToCustomer>.Filter.In("Country", searchFilterBlock.Contries);
            if (searchFilterBlock.Cities.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToCustomer>.Filter.In("City", searchFilterBlock.Cities);
            if (searchFilterBlock.Roles.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToCustomer>.Filter.In("Roles", searchFilterBlock.Roles);
            //if (searchFilterBlock.Age.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
            //    filter = filter & Builders<BusinessToCustomer>.Filter.In("Dob", searchFilterBlock.Age);
            
            if (searchFilterBlock.States.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToCustomer>.Filter.In("State", searchFilterBlock.States);

            if (searchFilterBlock.Area.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToCustomer>.Filter.In("Area", searchFilterBlock.Area);

            if (searchFilterBlock.Salary.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToCustomer>.Filter.In("AnnualSalary", searchFilterBlock.Salary);

            if (searchFilterBlock.Experience.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToCustomer>.Filter.In("Experience", searchFilterBlock.Experience);

            var searchResult = _context.BusinessToCustomers.Find(filter).ToList();

            return (searchResult, totalDocuments);

        }

        public Task<SearchBlock> GetFilterBlocks()
        {
            var searchItems = _context.B2CSearchItems
                                .Find(_ => true)
                                .ToListAsync()
                                .Result;

            var country = searchItems.Select(x => x.Country).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var city = searchItems.Select(x => x.City).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var state = searchItems.Select(x => x.State).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var area = searchItems.Select(x => x.Area).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var roles = searchItems.Select(x => x.Roles).FirstOrDefault()
                 .Where(x => !string.IsNullOrWhiteSpace(x));
            var salary = searchItems.Select(x => x.Salary).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var ages = searchItems.Select(x => x.Age).FirstOrDefault();
            var experience = searchItems.Select(x => x.Experience).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));

            SearchBlock searchBlock = new SearchBlock();
            //var country = _context.BusinessToCustomers
            //    .AsQueryable()
            //    .Select(x => x.Country).Where(x => x != "")
            //    .Distinct().ToList();
            //var city = _context.BusinessToCustomers
            //    .AsQueryable()
            //    .Select(x => x.City).Where(x => x != "")
            //    .Distinct().ToList();
            //var state = _context.BusinessToCustomers
            //    .AsQueryable()
            //    .Select(x => x.State).Where(x => x != "")
            //    .Distinct().ToList();
            //var area = _context.BusinessToCustomers
            //    .AsQueryable()
            //    .Select(x => x.Area).Where(x => x != "")
            //    .Distinct().ToList();
            //var roles = _context.BusinessToCustomers
            //    .AsQueryable()
            //    .Select(x => x.Roles).Where(x => x != "")
            //    .Distinct().ToList();
            //var salary = _context.BusinessToCustomers
            //    .AsQueryable()
            //    .Select(x => x.AnnualSalary).Where(x => x != "")
            //    .Distinct().ToList();          
            //var ages = _context.BusinessToCustomers
            //   .AsQueryable()
            //   .Select(x => x.Dob).Where(x => x.HasValue)
            //   .Distinct().ToList();
            //var experience = _context.BusinessToCustomers
            //   .AsQueryable()
            //   .Select(x => x.Experience).Where(x => x != "")
            //   .Distinct().ToList();

            searchBlock.Country = country;
            searchBlock.State = state;
            searchBlock.City = city;
            searchBlock.Area = area;
            searchBlock.BusinessVertical = roles;
            searchBlock.Salary = salary;
            searchBlock.Age = ages;
            searchBlock.Expercinse = experience;

            return Task.FromResult(searchBlock);
        }

        public async Task<IEnumerable<string>> GetPhoneNewAsync()
        {
            FilterDefinition<BusinessToCustomer> filter = Builders<BusinessToCustomer>
                .Filter.Ne(m => m.Name, string.Empty);
            var result = await _context
                    .BusinessToCustomers
                    .Find(filter)
                    .Project(u => u.PhoneNew).ToListAsync();

            return result;
        }

        public async Task<long> GetTotalDocument()
        {
            var totalDocument = _context.BusinessToCustomers.Find(_ => true).CountDocuments();
            return await Task.FromResult(totalDocument);
        }
    }
}