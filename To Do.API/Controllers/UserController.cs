using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using To_Do.API.Entities;
using To_Do.API.Models;
using To_Do.Helpers;
using To_Do.Shared;

namespace To_Do.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UserController(
            ILogger<UserController> logger,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var newUser = new User 
            { 
                CreateTime = DateTime.Now,
                UserName = model.EmailAddress,
                Email = model.EmailAddress
            };

            // Add new user
            var result = await userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }

            // Add new role to user
            var role = new Role() { Name = RoleHelper.NORMAL_ROLE };
            var normalRole = await roleManager.CreateAsync(role);
            result = await userManager.AddToRoleAsync(newUser, RoleHelper.NORMAL_ROLE);
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
            string email = login.Email;
            string password = login.Password;

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Ok(new LoginResult
                {
                    Successful = false,
                    Token = "",
                    Error = "Wrong email or password."
                });
            }

            if (await userManager.IsLockedOutAsync(user))
            {
                return Ok(new LoginResult
                {
                    Successful = false,
                    Token = "",
                    Error = "Account is locked."
                });
            }

            if (await userManager.CheckPasswordAsync(user, password))
            {
                return Ok(new LoginResult
                {
                    Successful = true,
                });
            }
            else
            {
                await userManager.AccessFailedAsync(user);
                return Ok(new LoginResult
                {
                    Successful = false,
                    Error = "Wrong email or password."
                });
            }
            // TODO: Token
        }

        [HttpPost]
        public async Task Logout()
        {
        }
    }
}