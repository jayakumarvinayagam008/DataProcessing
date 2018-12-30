using System;
namespace DataProcessing.Application.Authendication
{
    public class ValidateUser: IValidateUser
    {
        public ValidateUser()
        {
        }

        public (string userName, bool status) Validate(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
