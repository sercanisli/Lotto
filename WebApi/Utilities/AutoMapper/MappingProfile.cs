using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

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

            CreateMap<SayisalLoto, SayisalLotoDto>().ReverseMap();
            CreateMap<SayisalLotoDtoForUpdate, SayisalLoto>().ReverseMap();
            CreateMap<SayisalLotoDtoForInsertion, SayisalLoto>().ReverseMap();
            
            CreateMap<OnNumara, OnNumaraDto>().ReverseMap();
            CreateMap<OnNumaraDtoForUpdate, OnNumara>().ReverseMap();
        }
    }
}
