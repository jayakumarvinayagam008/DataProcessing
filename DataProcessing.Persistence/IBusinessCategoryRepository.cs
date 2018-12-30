using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IBusinessCategoryRepository
    {
        Task<IEnumerable<BisinessCategory>> Get();
    }

    public class BusinessCategoryRepository : IBusinessCategoryRepository
    {
        private readonly IDataProcessingContext _context;

        public BusinessCategoryRepository(IDataProcessingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BisinessCategory>> Get()
        {
            return await _context
                        .BisinessCategories
                        .Find(_ => true)
                        .ToListAsync();
        }
    }
}