using System;
using System.Threading.Tasks;

namespace DataProcessing.Persistence
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(DataProcessingUser user);
        Task<(string userName, bool status)> Validate(string userName, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDataProcessingContext _context;

        public UserRepository(IDataProcessingContext context)
        {
            _context = context;
        }

        public Task<bool> CreateAsync(DataProcessingUser user)
        {
            throw new NotImplementedException();
        }

        public Task<(string userName, bool status)> Validate(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
