using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WildOasis.API.Controllers.Identity.Dto;
using WildOasis.Infrastructure.Persistence;

namespace WildOasis.API.Controllers.Identity
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("makeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddClaimAsync(user, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("removeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.RemoveClaimAsync(user, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create(
            [FromBody] UserCredentials userCredentials)
        {
            var user = new ApplicationUser { UserName = userCredentials.Email, Email = userCredentials.Email };
            var result = await _userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponse>> Login(
            [FromBody] UserCredentials userCredentials)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredentials.Email,
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return BadRequest("Incorrect Login");
            }
        }

        private async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentials)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userCredentials.Email);

                var claims = new List<Claim>();

                claims.Add(new Claim("user_telephone", user.PhoneNumber ?? string.Empty));
                claims.Add(new Claim("user_email", user.Email ?? string.Empty));
                claims.Add(new Claim("user_locale", user.Locale ?? string.Empty));
                claims.Add(new Claim("user_fullname", user.FullName ?? string.Empty));
                claims.Add(new Claim("user_organization", user.Organization ?? string.Empty));
                claims.Add(new Claim("user_map", user.UserCode ?? string.Empty));
                //claims.Add(new Claim("user_avatar", user.ImageUrl ?? string.Empty));

                var claimsDb = await _userManager.GetClaimsAsync(user);

                claims.AddRange(claimsDb);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0FD5B805172C464597D2FFC0025677210381FF67654C4888B3B131445968D173"));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(1);

                var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                    expires: expiration, signingCredentials: credentials);

                return new AuthenticationResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Success = true,
                    Expiration = expiration
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResponse()
                {
                    Token = null,
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}