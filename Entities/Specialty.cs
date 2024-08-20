using System.ComponentModel.DataAnnotations;
using USFWebAPI.Validations;

namespace USFWebAPI.Entities
{
    public class Specialty
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(maximumLength: 20, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [FirstLetterUpperCase]
        [NoWhiteSpace]
        public string Name { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Professional> Professionals { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
