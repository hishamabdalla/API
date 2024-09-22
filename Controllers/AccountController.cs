using API.DTO;
using API.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        //Create Account New User ===>Register ==>User
        [HttpPost("Register")]
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = userDto.UserName,
                    Email = userDto.Email,

                };
               IdentityResult result= await userManager.CreateAsync(applicationUser, userDto.Password);
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


    }
}
