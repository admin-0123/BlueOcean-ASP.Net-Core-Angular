using System.Collections.Generic;
using System.Threading.Tasks;
using Virta.Api.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Virta.Data.Interfaces;
using Virta.Entities;
using Virta.Services.Interfaces;

namespace Virta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrdersRepository _ordersRepo;
        private readonly IOrdersService _orderService;
        private readonly UserManager<User> _userManager;

        public OrdersController(
            IMapper mapper,
            IOrdersRepository ordersRepo,
            IOrdersService orderService,
            UserManager<User> userManager
        )
        {
            _ordersRepo = ordersRepo;
            _orderService = orderService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ordersFromDb = await _ordersRepo.GetOrders();

            if (ordersFromDb == null)
                return Ok("False");

            var response = _mapper.Map<IEnumerable<OrderOutgoing>>(ordersFromDb);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var ordersFromDb = await _ordersRepo.GetOrder(id);

            if (ordersFromDb == null)
                return Ok("False");

            var response = _mapper.Map<OrderOutgoing>(ordersFromDb);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertOrder(OrderIncoming orderIncoming)
        {
            var order = _mapper.Map<OrderUpsert>(orderIncoming);

            if (await _orderService.UpsertOrder(order))
                return Ok();

            return BadRequest();
        }
    }
}
