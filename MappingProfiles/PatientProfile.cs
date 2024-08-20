using AutoMapper;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;

namespace USFWebAPI.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientDTO, Patient>();
            CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name));
        }
    }
}
