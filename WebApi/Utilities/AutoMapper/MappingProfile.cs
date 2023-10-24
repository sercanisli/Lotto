﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SuperLoto, SuperLotoDto>().ReverseMap();
            CreateMap<SuperLotoDtoForUpdate, SuperLoto>().ReverseMap();
        }
    }
}
