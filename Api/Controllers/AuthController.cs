using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Virta.Api.DTO;
using Virta.Api.Services.Interfaces;
using Virta.Models;
namespace Virta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public const string INCORRECT_CREDENTIALS = "Email or Password is Incorrect.";

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            ITokenService tokenService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                return GetToken(user);

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
                return GetToken(user);

            return BadRequest(INCORRECT_CREDENTIALS);
        }

        private IActionResult GetToken(User user)
        {
            string token = _tokenService.CreateToken(user);

            Dictionary<string, string> response = new Dictionary<string, string>();

            response.Add("token", token);

            return Ok(response);
        }
    }
}
