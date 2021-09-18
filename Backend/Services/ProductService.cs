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
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesRepository _categoriesRepo;
        private readonly IAttributesRepository _attributesRepository;

        public ProductService(
            IMapper mapper,
            IProductsRepository productsRepository,
            ICategoriesRepository categoriesRepo,
            IAttributesRepository attributesRepository
        )
        {
            _mapper = mapper;
            _productsRepository = productsRepository;
            _categoriesRepo = categoriesRepo;
            _attributesRepository = attributesRepository;
        }

        public async Task<bool> Upsert(ProductUpsert product)
        {
            var productToSave = _mapper.Map<Product>(product);

            if (product.Categories.Length > 0)
                productToSave.Categories = await SetCategories(product.Categories);

            if (product.ProductAttributes?.Count > 0)
                productToSave.ProductAttributes = await SetProductAttributes(product.ProductAttributes);

            if (product.AssociatedProducts?.Count > 0)
                productToSave.AssociatedProducts = await SetAssociatedProducts(product.AssociatedProducts);

            if (productToSave.Id == Guid.Empty)
            {
                _productsRepository.Add(productToSave);
            }
            else
            {
                var productFromDb = await _productsRepository.GetProduct(productToSave.Id);
                _mapper.Map<Product, Product>(productToSave, productFromDb);
                _productsRepository.Update(productFromDb);
            }

            if (await _productsRepository.SaveAll())
                return true;

            return false;
        }

        private async Task<List<Category>> SetCategories(string[] categories)
        {
            var result = new List<Category>();

            foreach (var category in categories)
                result.Add(await _categoriesRepo.GetCategory(category));

            return result;
        }

        private async Task<List<ProductAttribute>> SetProductAttributes(List<ProductAttributeUpsert> attributes)
        {
            var result = new List<ProductAttribute>();

            foreach (var attribute in attributes)
            {
                var entity = _mapper.Map<ProductAttribute>(attribute);
                entity.Attribute = await _attributesRepository.GetAttribute(attribute.Name);
                result.Add(entity);
            }

            return result;
        }

        private async Task<List<Product>> SetAssociatedProducts(List<Guid> associatedProducts)
        {
            var result = new List<Product>();

            foreach (var Id in associatedProducts)
                result.Add(await _productsRepository.GetProduct(Id));

            return result;
        }
    }
}
