using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Virta.Api.DTO;
using Virta.Api.Services.Interfaces;
using Virta.Entities;

namespace Virta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public const string INCORRECT_CREDENTIALS = "Email or Password is Incorrect.";

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IMapper mapper,
            ITokenService tokenService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Success");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegister userToRegister)
        {
            var user = _mapper.Map<User>(userToRegister);

            var result = await _userManager
                .CreateAsync(
                    user,
                    userToRegister.Password
                );

            if (result.Succeeded)
                return await GetTokenAsync(user);

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLogin userToLogin)
        {
            var user = await _userManager.FindByNameAsync(userToLogin.Email);

            if (user == null)
                return BadRequest(INCORRECT_CREDENTIALS);

            var result = await _signInManager.CheckPasswordSignInAsync(user, userToLogin.Password, false);

            if (result.Succeeded)
                return await GetTokenAsync(user);

            return BadRequest(INCORRECT_CREDENTIALS);
        }

        private async Task<IActionResult> GetTokenAsync(User user)
        {
            string token = await _tokenService.CreateAsync(user);

            return Ok(new { Token = token });
        }

        [HttpGet("createAdmin")]
        public async Task<IActionResult> CreateRolesandUsers()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(
                        new Role
                        {
                            Name = "Admin"
                        }
                    );
            }

            var admin = new User
            {
                UserName = "admin@admin.com",
                Firstname = "admin",
                Lastname = "admin",
                Email = "admin@admin.com"
            };

            IdentityResult User = await _userManager.CreateAsync(admin, "password");

            if (User.Succeeded)
            {
                var result = await _userManager.AddToRoleAsync(admin, "Admin");
                return Ok();
            }

            return BadRequest();
        }
    }
}
