using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logTesting.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace logTesting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Dtos.UserRegistrationRequestDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                //Check if email exists
                if (existingUser != null)
                {
                    return BadRequest(new Models.AuthResult
                    {
                        Errors = new List<string>()
                        {
                            "Email already in use"
                        },
                        Result = false
                    });
                }

                //Create new user if email does not exist
                var newUser = new IdentityUser()
                {
                    Email = user.Email,
                    UserName = user.Name
                };

                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);
                    return Ok(new Models.AuthResult
                    {
                        Errors = null,
                        Result = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new Models.AuthResult
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Result = false
                    });
                }
            }
            else
            {
                return BadRequest(new Models.AuthResult
                {
                    Errors = new List<string>()
                    {
                        "Invalid payload"
                    },
                    Result = false
                });
            }
        }
 
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            //Create token descriptor. All config to put into the token
            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new []{
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}