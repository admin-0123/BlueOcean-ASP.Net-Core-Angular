using System.Threading.Tasks;
using AutoMapper;
using Virta.Data.Interfaces;
using Virta.Entities;
using Virta.Models;
using Virta.Services.Interfaces;

namespace Virta.Services
{
    public class AttributesService : IAttributesService
    {
        private readonly IMapper _mapper;
        private readonly IAttributesRepository _attributesRepository;

        public AttributesService(
            IMapper mapper,
            IAttributesRepository attributesRepository
        )
        {
            _mapper = mapper;
            _attributesRepository = attributesRepository;
        }

        public async Task<bool> Upsert(AttributeUpsert attribute)
        {
            var attributeToSave = _mapper.Map<Attribute>(attribute);

            if (attributeToSave.Id != 0)
            {
                var attributeFromDb = await _attributesRepository.GetAttribute(attributeToSave.Id);
                _mapper.Map<Attribute, Attribute>(attributeToSave, attributeFromDb);
                _attributesRepository.Update<Attribute>(attributeFromDb);
            }
            else
            {
                _attributesRepository.Add<Attribute>(attributeToSave);
            }

            if (await _attributesRepository.SaveAll())
                return true;

            return false;
        }
    }
}
