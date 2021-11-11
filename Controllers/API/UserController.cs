using System;
using System.Threading.Tasks;
using fs_auth.DTOs;
using fs_auth.Entities;
using fs_auth.Extensions;
using fs_auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fs_auth.Controllers.API
{
    [Authorize]
    public class UserController : BaseApiController
    {
        public UserManager<ApplicationUser> _userManager { get; set; }
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            if (userManager is null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }
        }

        [HttpGet("profile")]
        public async Task<ActionResult<BaseDto>> profile()
        {

            var userId = User.GetUserId();
            // var user = await _userRepository.GetUserByIdAsync(userId);
            return Ok(new BaseDto
            {
                Status = true,
                Data = userId,
                Message = "wtf"
            });
        }
    }
}