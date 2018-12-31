using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Application.Authendication
{
    public interface ICreateUser
    {
        Task<bool> Create(string username, string password, string email);
    }
}
