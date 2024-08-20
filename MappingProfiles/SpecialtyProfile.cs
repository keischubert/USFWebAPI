using AutoMapper;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;

namespace USFWebAPI.MappingProfiles
{
    public class SpecialtyProfile : Profile
    {
        public SpecialtyProfile()
        {
            CreateMap<CreateSpecialtyDTO, Specialty>();
            CreateMap<Specialty, SpecialtyDTO>();
        }
    }
}
