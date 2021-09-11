using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Virta.Api.DTO;
using Virta.Data.Interfaces;
using Virta.Models;
using Virta.Services.Interfaces;

namespace Virta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(
            IMapper mapper,
            ICategoriesRepository categoriesRepository,
            ICategoriesService categoriesService
        )
        {
            _categoriesRepository = categoriesRepository;
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        [HttpGet("{amount:int?}")]// TODO: Make swagger optional
        public async Task<IActionResult> Get(int amount = 10)
        {
            var categories = await _categoriesRepository.GetCategories(amount);

            if (categories == null)
                return BadRequest();

            var response = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryUpsert>(categoryDTO);
            if (await _categoriesService.Upsert(category))
                return Ok();

            return BadRequest();
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            var rawData = await System.IO.File.ReadAllTextAsync("bsData/categories.json");
            var categories = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(rawData);

            foreach (var category in categories)
                await Upsert(category);

            return Ok();
        }
    }
}
