using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fs_auth.DTOs;
using fs_auth.Entities;
using fs_auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fs_auth.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            this._mapper = mapper;
            this._tokenService = tokenService;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }



        [HttpPost("register")]
        public async Task<ActionResult<BaseDto>> register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                if (await UserExist(registerDto.Username))
                {
                    return BadRequest(new BaseDto
                    {
                        Status = false,
                        Message = "User is exist"
                    });
                }
                var user = _mapper.Map<ApplicationUser>(registerDto);

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded) return BadRequest(new BaseDto
                {
                    Status = false,
                    Message = result.Errors
                });

                return Ok(new BaseDto
                {
                    Status = true,
                    Data = new UserDto
                    {
                        FirstName = user.UserName,
                        LastName = user.LastName,
                        Token = await _tokenService.BuildToken(user),
                        Gender = user.Gender
                    },
                    Message = ""
                });
            }
            else
            {
                var error = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return BadRequest(new BaseDto
                {
                    Status = false,
                    Message = error
                });
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<BaseDto>> login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());
                if (user == null)
                {
                    return BadRequest(new BaseDto
                    {
                        Status = false,
                        Message = "User isn't exist!"
                    });
                }
                else
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                    if (!result.Succeeded) return Ok(new BaseDto
                    {
                        Status = false,
                        Message = "Login failed"
                    });
                    return Ok(new BaseDto
                    {
                        Status = true,
                        Data = new UserDto
                        {
                            FirstName = user.UserName,
                            LastName = user.LastName,
                            Token = await _tokenService.BuildToken(user),
                            Gender = user.Gender
                        },
                        Message = ""
                    });
                }
            }
            else
            {
                var error = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return BadRequest(new BaseDto
                {
                    Status = false,
                    Message = error
                });
            }
        }


        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
        }

    }
}