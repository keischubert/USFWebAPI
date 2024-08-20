using AutoMapper;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;

namespace USFWebAPI.MappingProfiles
{
    public class GenderProfile : Profile
    {
        public GenderProfile() 
        {
            CreateMap<CreateGenderDTO, Gender>();
            CreateMap<Gender, GenderDTO>();
        }
    }
}
