using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace fs_auth.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}