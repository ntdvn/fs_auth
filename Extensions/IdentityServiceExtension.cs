using fs_auth.Data;
using fs_auth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fs_auth.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            }).AddRoles<ApplicationRole>().AddRoleManager<RoleManager<ApplicationRole>>().AddSignInManager<SignInManager<ApplicationUser>>().AddRoleValidator<RoleValidator<ApplicationRole>>().AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}