using System.ComponentModel.DataAnnotations;

namespace USFWebAPI.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int QueueNumber { get; set; }
    }
}
