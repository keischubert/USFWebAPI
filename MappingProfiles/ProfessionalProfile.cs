using AutoMapper;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;

namespace USFWebAPI.MappingProfiles
{
    public class ProfessionalProfile : Profile
    {
        public ProfessionalProfile()
        {
            CreateMap<CreateProfessionalDTO, Professional>();
            CreateMap<Professional, ProfessionalDTO>();
        }
    }
}
