using System.Threading.Tasks;
using Virta.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Virta.Api.DTO;
using Virta.Data.Interfaces;
using Virta.Models;
using System.Text.Json;
using System.Collections.Generic;

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoriesRepository.GetCategories();

            if (categories == null)
                return BadRequest();

            var response = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Upset(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryUpsert>(categoryDTO);
            if (await _categoriesService.UpsertCategory(category))
                return Ok();

            return BadRequest();
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            var categoriesRaw = await System.IO.File.ReadAllTextAsync("bsData/categories.json");
            var categories = JsonSerializer.Deserialize<IEnumerable<CategoryUpsert>>(categoriesRaw);

            foreach (var category in categories)
                await _categoriesService.UpsertCategory(category);

            return Ok();
        }
    }
}
