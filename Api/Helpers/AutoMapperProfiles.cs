using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using Newtonsoft.Json;
using VirtaApi.DTO;
using VirtaApi.Models;

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
        }
    }
}
