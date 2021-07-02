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
        private readonly ICategoriesRepository _categoriesRepo;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(
            IMapper mapper,
            ICategoriesRepository categoriesRepo,
            ICategoriesService categoriesService
        )
        {
            _categoriesRepo = categoriesRepo;
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Categories");
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
        public async Task<IActionResult> SeedCategoriesFromJson()
        {
            var categoriesRaw = await System.IO.File.ReadAllTextAsync("bsData/categories.json");
            var categories = JsonSerializer.Deserialize<List<CategoryUpsert>>(categoriesRaw);

            if (categories == null)
                return Ok("False");

            foreach (var category in categories)
                await _categoriesService.UpsertCategory(category);

            return Ok("True");
        }
    }
}
