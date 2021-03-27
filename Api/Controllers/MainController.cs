using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtaApi.Models;

namespace VirtaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public MainController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<MainController> logger
        )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("True");
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            var result = await _userManager
                .CreateAsync(
                    new User()
                    {
                        UserName = "bob",
                        Email = "bob@bob.com"
                    },
                    "Test123!"
                );

            if (result.Succeeded)
            {
                return Ok("True");
            }

            return Ok("False");
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var user = await _userManager.FindByNameAsync("bob");
            var result = await _signInManager.CheckPasswordSignInAsync(user, "Test123!", false);

            if (result.Succeeded)
            {
                return Ok("True");
            }

            return Ok("False");
        }
    }
}
