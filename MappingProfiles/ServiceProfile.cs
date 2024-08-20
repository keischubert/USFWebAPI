using AutoMapper;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;

namespace USFWebAPI.MappingProfiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDTO>()
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Specialty.Name));
            CreateMap<CreateServiceDTO, Service>();
        }
    }
}
