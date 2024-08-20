using System.ComponentModel.DataAnnotations;
using USFWebAPI.Validations;

namespace USFWebAPI.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(maximumLength: 100, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [FirstLetterUpperCase]
        [NoWhiteSpace]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}
