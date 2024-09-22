using API.DTO;
using API.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        //Create Account New User ===>Register ==>User
        [HttpPost("Register")]
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = userDto.UserName,
                    Email = userDto.Email,

                };
                IdentityResult result = await userManager.CreateAsync(applicationUser, userDto.Password);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    BadRequest(result.Errors.FirstOrDefault().ToString);
                }
            }
            return BadRequest(ModelState);
        }

        //Check Account Valid User ===>Login   ==>User
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto UserDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(UserDto.UserName);

                if (user != null && await userManager.CheckPasswordAsync(user, UserDto.Password))
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new 
                    {
                        Token = token,
                    });
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SecurityKey securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            SigningCredentials signingCredentials=new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims:claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:DurationInMinutes"])),
                signingCredentials: signingCredentials

            );
            return new JwtSecurityTokenHandler().WriteToken(token); //Create
        }
    }
}
