using System.Threading.Tasks;
using fs_auth.Entities;

namespace fs_auth.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(int id);
    }
}