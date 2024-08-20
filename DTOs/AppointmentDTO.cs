using System.ComponentModel.DataAnnotations;
using USFWebAPI.Entities;

namespace USFWebAPI.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int SpecialtyId { get; set; }
        public int ProfessionalId { get; set; }
        public DateOnly Date { get; set; }
        public int QueueNumber { get; set; }
    }
}
