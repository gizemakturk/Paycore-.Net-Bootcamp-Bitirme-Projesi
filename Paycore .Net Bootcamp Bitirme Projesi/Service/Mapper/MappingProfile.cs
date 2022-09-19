using AutoMapper;
using Data.Model;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<ProductDto, Product>().ReverseMap();

        }

    }
}