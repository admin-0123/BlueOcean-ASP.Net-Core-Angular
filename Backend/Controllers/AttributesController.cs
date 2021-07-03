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
    public class AttributesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttributesRepository _attributesRepository;
        private readonly IAttributesService _attributesService;

        public AttributesController(
            IMapper mapper,
            IAttributesRepository attributesRepository,
            IAttributesService attributesService
        )
        {
            _mapper = mapper;
            _attributesRepository = attributesRepository;
            _attributesService = attributesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var attributes = await _attributesRepository.GetAttributes();

            if (attributes == null)
                return BadRequest();

            var response = _mapper.Map<IEnumerable<AttributeDTO>>(attributes);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(AttributeDTO attributeDTO)
        {
            var attribute = _mapper.Map<AttributeUpsert>(attributeDTO);

            if (await _attributesService.Upsert(attribute))
                return Ok();

            return BadRequest();
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            var rawData = await System.IO.File.ReadAllTextAsync("bsData/attributes.json");
            var attributes = JsonSerializer.Deserialize<IEnumerable<AttributeDTO>>(rawData);

            foreach (var attribute in attributes)
                await Upsert(attribute);

            return Ok();
        }
    }
}
