using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fs_auth.Entities;
using fs_auth.Interfaces;

namespace fs_auth.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            return await _applicationDbContext.Users.FindAsync(id);
        }
    }
}