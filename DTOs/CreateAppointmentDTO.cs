using System.ComponentModel.DataAnnotations;
using USFWebAPI.Entities;

namespace USFWebAPI.DTOs
{
    public class CreateAppointmentDTO
    {
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int SpecialtyId { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int ProfessionalId { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
