using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Virta.Api.DTO;
using Virta.Entities;
using Virta.Models;
using Virta.MVC.ViewModels;

namespace Virta.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            /* To User Entity */
            CreateMap<UserToRegister, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(
                        src => src.Email
                    )
                );



            /* From Product Entity */
            CreateMap<Product, ProductPDP>()
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(
                        src => JsonConvert.DeserializeObject<List<string>>(src.Images)
                    ));

            CreateMap<Product, ProductPLP>()
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(
                        src => JsonConvert.DeserializeObject<List<string>>(src.Images).Take(1)
                    )
                );

            CreateMap<Product, ProductPLPVM>()
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(
                        src => JsonConvert.DeserializeObject<List<string>>(src.Images).Take(1)
                    )
                );

            CreateMap<Product, ProductUpsertVM>();

            /* To Product Entity*/
            CreateMap<ProductUpsert.ProductAttributes, ProductAttributes>();
            CreateMap<ProductUpsert.Category, Category>();
            CreateMap<ProductUpsert, Product>();

            /* TO Product Upsert */
            //API
            CreateMap<ProductAttributesDTO, ProductUpsert.ProductAttributes>();
            CreateMap<CategoryDTO, ProductUpsert.Category>();
            CreateMap<ProductPDP, ProductUpsert>();
            //MVC
            CreateMap<ProductAttributesVM, ProductUpsert.ProductAttributes>();
            CreateMap<SelectListItem, ProductUpsert.Category>();
            CreateMap<ProductUpsertVM, ProductUpsert>();

            /* From Product Attributes Entity */
            CreateMap<ProductAttributes, ProductAttributesDTO>();
            CreateMap<ProductAttributes, Virta.MVC.ViewModels.ProductAttributesVM>();



            /* From Category Entity */
            CreateMap<Category, CategoryDTO>();
            CreateMap<Category, Virta.MVC.ViewModels.CategoryVM>();
            CreateMap<Category, SelectListItem>()
                .ForMember(
                    dest => dest.Text,
                    opt => opt.MapFrom(
                        src => src.Title
                    )
                );

            /* To Category Entity */
            CreateMap<CategoryDTO, Category>();
            CreateMap<CategoryUpsert, Category>();

            /* To Category Upsert */
            CreateMap<CategoryDTO, CategoryUpsert>();



            /* Self */
            CreateMap<Product, Product>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Category, Category>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
