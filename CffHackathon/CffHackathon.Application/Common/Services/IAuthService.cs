using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(string email, string password);
        Task<string> LoginAsync(string email, string password);
        Task<string> AssignedRole(string userId, string roleName);
    }
}
