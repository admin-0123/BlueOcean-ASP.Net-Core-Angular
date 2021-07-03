using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            /* User */
            CreateMap<UserToRegister, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(
                        src => src.Email
                    )
                );



            /* Attribute */
            CreateMap<Attribute, AttributeDTO>();
            CreateMap<AttributeDTO, AttributeUpsert>();
            CreateMap<AttributeUpsert, Attribute>();
            CreateMap<AttributeDTO, Attribute>();



            /* Category */
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryUpsert>();
            CreateMap<CategoryUpsert, Category>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Category, Virta.MVC.ViewModels.CategoryVM>();
            CreateMap<Category, SelectListItem>()
                .ForMember(
                    dest => dest.Text,
                    opt => opt.MapFrom(
                        src => src.Title
                    )
                );


            /* Product */
            CreateMap<ProductDTO, ProductUpsert>();
            CreateMap<ProductUpsert, Product>();
            // CreateMap<Product, ProductDTO>();

            CreateMap<string, Category>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src)
                );

            CreateMap<System.Guid, Product>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src)
                );


            /* Product Attributes */
            CreateMap<ProductAttributesDTO, ProductAttributeUpsert>();
            CreateMap<ProductAttributeUpsert, ProductAttribute>();

            /* Product Images*/
            CreateMap<ProductImageDTO, ProductImageUpsert>();
            CreateMap<ProductImageUpsert, ProductImage>();





            /* From Product Entity */
            // CreateMap<Product, ProductPDP>()
            //     .ForMember(
            //         dest => dest.Images,
            //         opt => opt.MapFrom(
            //             src => JsonConvert.DeserializeObject<List<string>>(src.Images)
            //         ));

            // CreateMap<Product, ProductPLP>()
            //     .ForMember(
            //         dest => dest.Images,
            //         opt => opt.MapFrom(
            //             src => JsonConvert.DeserializeObject<List<string>>(src.Images).Take(1)
            //         )
            //     );

            // CreateMap<Product, ProductPLPVM>()
            //     .ForMember(
            //         dest => dest.Images,
            //         opt => opt.MapFrom(
            //             src => JsonConvert.DeserializeObject<List<string>>(src.Images).Take(1)
            //         )
            //     );

            // CreateMap<Product, ProductUpsertVM>();

            /* To Product Entity*/
            // CreateMap<ProductUpsert.ProductAttributes, ProductAttributes>();
            // CreateMap<ProductUpsert.Category, Category>();
            // /* TO Product Upsert */
            // //API
            // CreateMap<CategoryDTO, ProductUpsert.Category>();
            // CreateMap<ProductPDP, ProductUpsert>();
            // //MVC
            // CreateMap<ProductAttributesVM, ProductUpsert.ProductAttributes>();
            // CreateMap<SelectListItem, ProductUpsert.Category>();
            // CreateMap<ProductUpsertVM, ProductUpsert>();

            /* From Product Attributes Entity */
            // CreateMap<ProductAttributes, ProductAttributesDTO>();
            // CreateMap<ProductAttributes, Virta.MVC.ViewModels.ProductAttributesVM>();







            /* From Order Entity */
            CreateMap<Order, OrderOutgoing>()
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(
                        src => src.User.Id
                    )
                )
                .ForMember(
                    dest => dest.Products,
                    opt => opt.MapFrom(
                        src => src.OrderProduct
                    )
                );

            CreateMap<OrderProduct, OrderOutgoing.OrderProduct>()
                .ForMember(
                    dest => dest.OrderPrice,
                    opt => opt.MapFrom(
                        src => src.Price
                    )
                );

            /* TO Product Upsert */
            CreateMap<OrderIncoming, OrderUpsert>();
            CreateMap<OrderIncoming.OrderProduct, OrderUpsert.OrderProduct>();










            /* Self */
            CreateMap<Product, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Attribute, Attribute>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Category, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Order, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
