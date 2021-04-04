using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Virta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;

        public OrdersController(
            IMapper mapper
        )
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Orders");
        }
    }
}
