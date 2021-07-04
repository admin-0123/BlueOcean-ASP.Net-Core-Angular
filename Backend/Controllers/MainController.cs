using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Virta.Extensions;

namespace Virta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;

        public MainController(
            ILogger<MainController> logger
        )
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("auth")]
        public IActionResult Auth()
        {
            return Ok(User.GetUserId());
        }
    }
}
