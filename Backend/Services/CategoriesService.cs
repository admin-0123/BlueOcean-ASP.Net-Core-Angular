using System.Threading.Tasks;
using Virta.Services.Interfaces;
using AutoMapper;
using Virta.Data.Interfaces;
using Virta.Entities;
using Virta.Models;

namespace Virta.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(
            IMapper mapper,
            ICategoriesRepository categoriesRepository
        )
        {
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<bool> Upsert(CategoryUpsert category)
        {
            var categoryToSave = _mapper.Map<Category>(category);

            if(categoryToSave.Id != 0) {
                var categoryFromDb = await _categoriesRepository.GetCategory(categoryToSave.Id);
                _mapper.Map<Category, Category>(categoryToSave, categoryFromDb);
                _categoriesRepository.Update<Category>(categoryFromDb);
            } else {
                _categoriesRepository.Add<Category>(categoryToSave);
            }

            if (await _categoriesRepository.SaveAll())
                return true;

            return false;
        }
    }
}
