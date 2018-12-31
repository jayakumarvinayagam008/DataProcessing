using System;
namespace DataProcessing.Application.Authendication
{
    public interface IValidateUser
    {
        (string userName, bool status, string error) Validate(string userName, string password);
    }
}
