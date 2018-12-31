using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Application.Authendication
{
    public class CreateUser : ICreateUser
    {
        private readonly IUserRepository _userRepository;
        public CreateUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> Create(string username, string password, string email)
        {
            return _userRepository.CreateAsync(new DataProcessingUser() {
                Password = password,
                UserName = username,
                Email = email
            });
        }
    }
}
