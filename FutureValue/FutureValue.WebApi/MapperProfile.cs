using AutoMapper;
using FutureValue.Domain.Entities;
using FutureValue.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutureValue.WebApi
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<ProjectionFormDto, ProjectionForm>().ForMember(destination=>destination.IsActive, o => o.MapFrom(s => true)).ReverseMap();
            CreateMap<AspUserDto, AspUser>().ForMember(destination => destination.IsActive, o => o.MapFrom(s => true)).ReverseMap();

            CreateMap<IEnumerable<ProjectionForm>, List<ProjectionFormDto>>();
            CreateMap<IEnumerable<AspUser>, List<AspUserDto>>();
        }
    }
}
