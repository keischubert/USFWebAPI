using AutoMapper;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;

namespace USFWebAPI.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile() 
        {
            CreateMap<CreateAppointmentDTO, Appointment>();
            CreateMap<Appointment, AppointmentDTO>();
        }
    }
}
