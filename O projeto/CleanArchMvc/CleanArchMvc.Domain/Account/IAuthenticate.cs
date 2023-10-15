using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Account
{
    public interface IAuthenticate
    {
        Task<bool> Login(string email, string password);
        Task<bool> Register(string email, string password);
        Task Logout();
    }
}
