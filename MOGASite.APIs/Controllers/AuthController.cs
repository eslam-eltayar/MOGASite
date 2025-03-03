using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class AuthController : ApiBaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid Login. This Email doesn't Exist" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new { Message = "Invalid Login." });

            var roles = await _userManager.GetRolesAsync(user);

            var role = roles.FirstOrDefault();

            return Ok(new UserDto()
            {
                Email = user?.Email ?? string.Empty,
                Token = await _tokenService.CreateTokenAsync(user, _userManager),
                Role = role
            });
        }
    }
}
