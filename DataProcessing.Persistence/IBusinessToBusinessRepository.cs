using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IBusinessToBusinessRepository
    {
        Task<IEnumerable<BusinessToBusiness>> GetAllB2BAsync();

        //Task<BusinessToBusiness> GetGame(string name);
        Task<bool> CreateAsync(BusinessToBusiness game);

        //Task<bool> Update(BusinessToBusiness game);
        //Task<bool> Delete(string name);
        Task<bool> CreateManyAsync(List<BusinessToBusiness> games);

        Task<IEnumerable<BusinessToBusiness>> Filter(string refId);

        Task<SearchBlock> GetFilterBlocks();

        (List<BusinessToBusiness> BusinessToBusinesses, long Total) GetB2BSearch(B2BFilter b2BFilter);

        Task<long> GetTotalDocument();
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

        public async Task<bool> CreateAsync(BusinessToBusiness game)
        {
            _context
               .BusinessToBusiness.InsertOne(game);
            return await Task.FromResult(true);
        }

        public async Task<bool> CreateManyAsync(List<BusinessToBusiness> games)
        {
            await _context
                         .BusinessToBusiness.InsertManyAsync(games);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<BusinessToBusiness>> Filter(string refId)
        {
            var builder = Builders<BusinessToBusiness>.Filter;
            var filter = builder.Eq("RefId", refId);
            var projection = Builders<BusinessToBusiness>
                .Projection.Include("Country")
                .Include("State")
                .Include("City")
                .Include("Area")
                .Include("Designation")
                .Include("CategoryId")
                .Exclude("_id");

            var searchResult = _context.BusinessToBusiness.Find(filter)
                .Project(projection).ToList().Select(x => new BusinessToBusiness
                {
                    Country = x.GetValue("Country").AsString,
                    State = x.GetValue("State").AsString,
                    City = x.GetValue("City").AsString,
                    Area = x.GetValue("Area").AsString,
                    Designation = x.GetValue("Designation").AsString,
                    CategoryId = x.GetValue("CategoryId").AsNullableInt64
                }).ToList<BusinessToBusiness>();

            
            return await Task.FromResult(searchResult);
                
        }

        public async Task<IEnumerable<BusinessToBusiness>> GetAllB2BAsync()
        {
            return await _context
                        .BusinessToBusiness
                        .Find(_ => true)
                        .ToListAsync();
        }

        public (List<BusinessToBusiness> BusinessToBusinesses, long Total) GetB2BSearch(B2BFilter b2BFilter)
        {
            var filter = Builders<BusinessToBusiness>.Filter.Empty;

            var totalDocuments = _context.BusinessToBusiness.Find(_ => true).CountDocuments();

            if (b2BFilter.Contries.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToBusiness>.Filter.In("Country", b2BFilter.Contries);

            if (b2BFilter.States.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToBusiness>.Filter.In("State", b2BFilter.States);

            if (b2BFilter.Cities.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToBusiness>.Filter.In("City", b2BFilter.Cities);

            if (b2BFilter.Area.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToBusiness>.Filter.In("Area", b2BFilter.Area);

            if (b2BFilter.Designation.Where(x => !string.IsNullOrWhiteSpace(x)).Any())
                filter = filter & Builders<BusinessToBusiness>.Filter.In("Designation", b2BFilter.Designation);

            if (b2BFilter.BusinessCategoryId.Where(x => x.HasValue).Any())
                filter = filter & Builders<BusinessToBusiness>.Filter.In("CategoryId", b2BFilter.BusinessCategoryId);

            var searchResult = _context.BusinessToBusiness.Find(filter).ToList();

            return (searchResult, totalDocuments);
        }

        public Task<SearchBlock> GetFilterBlocks()
        {

            var searchItems = _context.B2BSearchItems
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
            var destination = searchItems.Select(x => x.Designation).FirstOrDefault()
                .Where(x => !string.IsNullOrWhiteSpace(x));
            var categories = searchItems.Select(x => x.BusinessCategory).FirstOrDefault();


            SearchBlock searchBlock = new SearchBlock();
            //var country = _context.BusinessToBusiness
            //    .AsQueryable()
            //    .Select(x => x.Country).Where(x => x != "")
            //    .Distinct().ToList();
            //var city = _context.BusinessToBusiness
            //    .AsQueryable()
            //    .Select(x => x.City).Where(x => x != "")
            //    .Distinct().ToList();
            //var state = _context.BusinessToBusiness
            //    .AsQueryable()
            //    .Select(x => x.State).Where(x => x != "")
            //    .Distinct().ToList();
            //var area = _context.BusinessToBusiness
            //    .AsQueryable()
            //    .Select(x => x.Area).Where(x => x != "")
            //    .Distinct().ToList();
            //var destination = _context.BusinessToBusiness
            //    .AsQueryable()
            //    .Select(x => x.Designation).Where(x => x != "")
            //    .Distinct().ToList();
            //var categories = _context.BusinessToBusiness
            //    .AsQueryable()
            //    .Where(x => x.CategoryId != null)
            //    .Select(x => x.CategoryId.Value)
            //    .Distinct().ToList();
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
            IEnumerable<string> empty = new List<string>();
            IEnumerable<BusinessCategoryItem> categoryEmpty = new List<BusinessCategoryItem>();
            searchBlock.Country = country.Any() ? country : empty;
            searchBlock.State = state.Any() ? state : empty;
            searchBlock.City = city.Any() ? city : empty;
            searchBlock.Area = area.Any() ? area : empty;
            searchBlock.Desigination = destination.Any() ? destination : empty;
            searchBlock.BusinessCategory = searchCategory.Any() ? searchCategory : categoryEmpty;

            return Task.FromResult(searchBlock);
        }

        public async Task<long> GetTotalDocument()
        {
            var totalDocument = _context.BusinessToBusiness.Find(_ => true).CountDocuments();
            return await Task.FromResult(totalDocument);
        }
    }
}