using System;
using System.Threading.Tasks;
using fs_auth.Entities;

namespace fs_auth.Interfaces
{
    public interface ITokenService
    {
        Task<String> BuildToken(ApplicationUser user);
    }
}