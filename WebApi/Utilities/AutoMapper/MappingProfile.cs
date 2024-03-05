using AutoMapper;
using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.LogModels;
using Entities.Models;
using Entities.RequestFeatures;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDtoForRegistration, User>().ReverseMap();

            CreateMap<SuperLoto, SuperLotoDto>().ReverseMap();
            CreateMap<SuperLotoDtoForUpdate, SuperLoto>().ReverseMap();
            CreateMap<SuperLotoDtoForInsertion, SuperLoto>().ReverseMap();
            CreateMap<SuperLotoDto, SuperLotoDtoForLastItem>().ReverseMap();

            CreateMap<SayisalLoto, SayisalLotoDto>().ReverseMap();
            CreateMap<SayisalLotoDtoForUpdate, SayisalLoto>().ReverseMap();
            CreateMap<SayisalLotoDtoForInsertion, SayisalLoto>().ReverseMap();
            
            CreateMap<OnNumara, OnNumaraDto>().ReverseMap();
            CreateMap<OnNumaraDtoForUpdate, OnNumara>().ReverseMap();
            CreateMap<OnNumaraDtoForInsertion, OnNumara>().ReverseMap();

            CreateMap<SansTopu, SansTopuDto>().ReverseMap();
            CreateMap<SansTopuDtoForUpdate, SansTopu>().ReverseMap();
            CreateMap<SansTopuDtoForInsertion, SansTopu>().ReverseMap();
            CreateMap<SansTopuLogs, SansTopuDtoForRandom>().ReverseMap();
        }
    }
}
