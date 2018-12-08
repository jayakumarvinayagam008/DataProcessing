using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IBusinessToBusinessRepository
    {
        Task<IEnumerable<BusinessToBusiness>> GetAllB2BAsync();
        //Task<BusinessToBusiness> GetGame(string name);
        Task CreateAsync(BusinessToBusiness game);
        //Task<bool> Update(BusinessToBusiness game);
        //Task<bool> Delete(string name);
        Task<bool> CreateManyAsync(List<BusinessToBusiness> games);

        Task<IEnumerable<BusinessToBusiness>> Filter();

        Task<SearchBlock> GetFilterBlocks();

        Task<List<BusinessToBusiness>> GetB2BSearch(B2BFilter b2BFilter);
    }

    public class BusinessToBusinessRepository : IBusinessToBusinessRepository
    {
        private readonly IDataProcessingContext _context;
        private readonly IBusinessCategoryRepository _businessCategoryRepository;
        public BusinessToBusinessRepository(IDataProcessingContext context, IBusinessCategoryRepository businessCategoryRepository)
        {
            _context = context;
            _businessCategoryRepository = businessCategoryRepository;
        }

        public async Task CreateAsync(BusinessToBusiness game)
        {
            _context
                .BusinessToBusiness.InsertOne(game);
        }

        public async Task<bool> CreateManyAsync(List<BusinessToBusiness> games)
        {
            await _context
                         .BusinessToBusiness.InsertManyAsync(games);
            return await Task.FromResult(true);
        }

        public Task<IEnumerable<BusinessToBusiness>> Filter()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BusinessToBusiness>> GetAllB2BAsync()
        {
            return await _context
                        .BusinessToBusiness
                        .Find(_ => true)
                        .ToListAsync();
        }

        public async Task<List<BusinessToBusiness>> GetB2BSearch(B2BFilter b2BFilter)
        {

            IQueryable<BusinessToBusiness> filter1 = null;

            filter1 = _context
                   .BusinessToBusiness.AsQueryable();
            if(b2BFilter.Contries.Where(x=> !string.IsNullOrWhiteSpace(x)).Any())
                filter1.Where(x => b2BFilter.Contries.Contains(x.Country));

            if (b2BFilter.States.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter1.Where(x => b2BFilter.States.Contains(x.State));

            if (b2BFilter.Cities.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter1.Where(x => b2BFilter.Cities.Contains(x.City));

            if (b2BFilter.Area.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter1.Where(x => b2BFilter.Area.Contains(x.Area));

            if (b2BFilter.Designation.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter1.Where(x => b2BFilter.Designation.Contains(x.Designation));

            if (b2BFilter.BusinessCategoryId.Where(x => x.HasValue).Any())
                filter1.Where(x => b2BFilter.BusinessCategoryId.Contains(x.CategoryId));


            var filter = filter1.ToList();


            //var filter = _context
            //        .BusinessToBusiness                    
            //        .Find(x => b2BFilter.Contries.Contains(x.Country) 
            //        && b2BFilter.Cities.Contains(x.City)
            //        && b2BFilter.Area.Contains(x.Area)
            //        && b2BFilter.States.Contains(x.State)
            //        && b2BFilter.Designation.Contains(x.Designation)
            //        && b2BFilter.BusinessCategoryId.Contains(x.CategoryId))
            //        .ToList();
            return await Task.FromResult(filter);
            
        }

        public Task<SearchBlock> GetFilterBlocks()
        {
            SearchBlock searchBlock = new SearchBlock();
            var country = _context.BusinessToBusiness
                .AsQueryable()
                .Select(x => x.Country).Where(x => x != "")
                .Distinct().ToList();
            var city = _context.BusinessToBusiness
                .AsQueryable()
                .Select(x => x.City).Where(x => x != "")
                .Distinct().ToList();
            var state = _context.BusinessToBusiness
                .AsQueryable()
                .Select(x => x.State).Where(x => x != "")
                .Distinct().ToList();
            var area = _context.BusinessToBusiness
                .AsQueryable()
                .Select(x => x.Area).Where(x => x != "")
                .Distinct().ToList();
            var destination = _context.BusinessToBusiness
                .AsQueryable()
                .Select(x => x.Designation).Where(x => x != "")
                .Distinct().ToList();
            var categories = _context.BusinessToBusiness
                .AsQueryable()
                .Where(x=>x.CategoryId != null)
                .Select(x => x.CategoryId.Value)
                .Distinct().ToList();
            IEnumerable<BusinessCategoryItem> searchCategory = new List<BusinessCategoryItem>();

            if (categories.Any())
            {
                var category = _businessCategoryRepository.Get().Result.ToList();
                searchCategory = category.Join(categories,
                    x => x.CategoryId,
                    y => y,
                    (x, y) => new BusinessCategoryItem
                    {
                        Id = y,
                        Name = x.Name
                    });
            }           

            searchBlock.Country = country;
            searchBlock.State = state;
            searchBlock.City = city;
            searchBlock.Area = area;
            searchBlock.Desigination = destination;
            searchBlock.BusinessCategory = searchCategory;


            return Task.FromResult(searchBlock);
        }
    }
}
