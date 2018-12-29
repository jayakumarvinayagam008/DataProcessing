using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IBusinessToCustomerRepository
    {
        Task<IEnumerable<string>> GetPhoneNewAsync();
        Task<bool> CreateManyAsync(IEnumerable<BusinessToCustomer> saveToSource);
        Task<SearchBlock> GetFilterBlocks();
        Task<long> GetTotalDocument();
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

        public Task<SearchBlock> GetFilterBlocks()
        {
            SearchBlock searchBlock = new SearchBlock();
            var country = _context.BusinessToCustomers
                .AsQueryable()
                .Select(x => x.Country).Where(x => x != "")
                .Distinct().ToList();
            var city = _context.BusinessToCustomers
                .AsQueryable()
                .Select(x => x.City).Where(x => x != "")
                .Distinct().ToList();
            var state = _context.BusinessToCustomers
                .AsQueryable()
                .Select(x => x.State).Where(x => x != "")
                .Distinct().ToList();
            var area = _context.BusinessToCustomers
                .AsQueryable()
                .Select(x => x.Area).Where(x => x != "")
                .Distinct().ToList();
            var roles = _context.BusinessToCustomers
                .AsQueryable()
                .Select(x => x.Roles).Where(x => x != "")
                .Distinct().ToList();
            var salary = _context.BusinessToCustomers
                .AsQueryable()
                .Select(x => x.AnnualSalary).Where(x => x != "")
                .Distinct().ToList();
            var ages = _context.BusinessToCustomers
               .AsQueryable()
               .Select(x => x.Dob).Where(x => x.HasValue)
               .Distinct().ToList();
            var experience  = _context.BusinessToCustomers
               .AsQueryable()
               .Select(x => x.Experience).Where(x => x != "")
               .Distinct().ToList();

            searchBlock.Country = country;
            searchBlock.State = state;
            searchBlock.City = city;
            searchBlock.Area = area;
            searchBlock.BusinessVertical = roles;
            searchBlock.Salary = salary;
            //searchBlock.Age = ages;
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
                    .Project(u =>  u.PhoneNew ).ToListAsync();

            return result;
        }

        public async Task<long> GetTotalDocument()
        {
            var totalDocument = _context.BusinessToCustomers.Find(_ => true).CountDocuments();
            return await Task.FromResult(totalDocument);
        }
    }
}
