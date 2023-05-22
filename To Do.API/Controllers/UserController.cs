using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using To_Do.API.Models;
using To_Do.Shared;

namespace To_Do.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public UserController(ILogger<UserController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var newUser = new IdentityUser 
            { 
                UserName = model.UserName, 
                Email = model.EmailAddress,
            };

            var result = await userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }

            return Ok(new RegisterResult { Successful = true });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            
            if (!result.Succeeded)
            {
                return BadRequest(new LoginResult
                {
                    Successful = false,
                    Error = "Username and password are invalid."
                });
            }

            return Ok(new LoginResult 
            { 
                Successful = true, 
                Token = ""
            });
        }

        [HttpPost]
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }
    }
}