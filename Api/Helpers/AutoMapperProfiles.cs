using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Virta.Api.DTO;
using Virta.Models;

namespace VirtaApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
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

            CreateMap<ProductPDP, Product>()
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(
                        src => JsonConvert.SerializeObject(src.Images)
                    ));

            CreateMap<UserToRegister, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(
                        src => src.Email
                    )
                );

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<ProductAttributes, ProductAttributesDTO>();
            CreateMap<ProductAttributesDTO, ProductAttributes>();


            CreateMap<Category, Virta.MVC.ViewModels.Category>();
            CreateMap<ProductAttributes, Virta.MVC.ViewModels.ProductAttributes>();
            CreateMap<Product, Virta.MVC.ViewModels.ProductPLP>()
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(
                        src => JsonConvert.DeserializeObject<List<string>>(src.Images).Take(1)
                    )
                );
            CreateMap<Virta.MVC.ViewModels.ProductUpsert, Product>()
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.Ignore()
                );

            CreateMap<Product, Virta.MVC.ViewModels.ProductUpsert>();

            CreateMap<Category, SelectListItem>()
                .ForMember(
                    dest => dest.Text,
                    opt => opt.MapFrom(
                        src => src.Title
                    )
                );
        }
    }
}
