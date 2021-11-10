using System.Threading.Tasks;
using AutoMapper;
using fs_auth.Entities;
using fs_auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fs_auth.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet("register")]
        public ActionResult<object> register()
        {
            return BadRequest("Username is taken");
        }
    }
}