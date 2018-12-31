using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(DataProcessingUser user);
        Task<bool> ValidateAsync(string userName, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDataProcessingContext _context;

        public UserRepository(IDataProcessingContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(DataProcessingUser user)
        {
            user.IsActive = true;
            user.Role = "Administrator";
            user.CreatedBy = "Admin";
            user.CreatedDate = DateTime.Now;
            _context
              .DataProcessingUsers.InsertOne(user);
            return await Task.FromResult(true);
        }

        public async Task<bool> ValidateAsync(string userName, string password)
        {
            FilterDefinition<DataProcessingUser> filter = Builders<DataProcessingUser>
                .Filter.Eq(m => m.IsActive, true)
                & Builders<DataProcessingUser>.Filter.Eq(m => m.UserName, userName)
                & Builders<DataProcessingUser>.Filter.Eq(m => m.Password, password);
           
            var result = await _context
                    .DataProcessingUsers
                    .Find(filter)
                    .FirstOrDefaultAsync();
            return result!= null && !string.IsNullOrWhiteSpace(result.UserName);
        }
    }
}
