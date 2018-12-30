using System;
namespace DataProcessing.Application.Authendication
{
    public interface IValidateUser
    {
        (string userName, bool status) Validate(string userName, string password);
    }
}
