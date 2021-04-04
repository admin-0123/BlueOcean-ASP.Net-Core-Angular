using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Virta.Api.DTO;
using Virta.Data.Interfaces;

namespace Virta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoCompleteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoriesRepository;

        public AutoCompleteController(
            IMapper mapper,
            ICategoriesRepository categoriesRepository
        )
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("True");
        }

        [HttpGet("categories/{category}")]
        public async Task<IActionResult> Get(string category)
        {
            var categories = await _categoriesRepository.GetCategoriesAC(category);

            var response = _mapper.Map<List<CategoryDTO>>(categories);

            return Ok(response);
        }
    }
}
