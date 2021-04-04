using System.Threading.Tasks;
using Virta.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Virta.Api.DTO;
using Virta.Data.Interfaces;
using Virta.Models;

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
            if(await _categoriesService.UpsertCategory(category))
                return Ok("Categories");

            return BadRequest();
        }
    }
}
