using DataProcessing.Persistence;
using System;
namespace DataProcessing.Application.Authendication
{
    public class ValidateUser: IValidateUser
    {
        private readonly IUserRepository _userRepository;
        public ValidateUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public (string userName, bool status, string error) Validate(string userName, string password)
        {
            var userDetail = _userRepository.ValidateAsync(userName, password).Result;
            if (userDetail)
                return (userName, userDetail, string.Empty);
            else
                return (userName, userDetail, "Login Failed.Please enter correct credentials");
        }
    }
}
