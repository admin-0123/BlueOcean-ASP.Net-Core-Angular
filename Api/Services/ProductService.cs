using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Virta.Data.Interfaces;
using Virta.Models;
using Virta.Entities;
using Virta.Services.Interfaces;
using System;

namespace Virta.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repo;
        private readonly ICategoriesRepository _categoriesRepo;

        public ProductService(
            IMapper mapper,
            IProductsRepository repo,
            ICategoriesRepository categoriesRepo
        )
        {
            _mapper = mapper;
            _repo = repo;
            _categoriesRepo = categoriesRepo;
        }

        public async Task<bool> UpsertProduct(ProductUpsert product)
        {
            var productToSave = _mapper.Map<Product>(product);

            productToSave = await SetCategorise(productToSave);

            if(product.Id == Guid.Empty) {
                _repo.Add<Product>(productToSave);
            } else {
                _repo.Update<Product>(productToSave);
            }

            if (await _repo.SaveAll())
                return true;

            return false;
        }

        private async Task<Product> SetCategorise(Product product)
        {
            var newCategories = new List<Category>();

            foreach (var category in product.Categories)
            {
                newCategories.Add(await _categoriesRepo.GetCategory(category.Value));
            }

            product.Categories = newCategories;

            return product;
        }
    }
}
