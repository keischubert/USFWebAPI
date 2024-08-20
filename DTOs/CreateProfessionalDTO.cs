using System.ComponentModel.DataAnnotations;
using USFWebAPI.Entities;
using USFWebAPI.Validations;

namespace USFWebAPI.DTOs
{
    public class CreateProfessionalDTO
    {

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(50, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [FirstLetterUpperCase]
        [NoWhiteSpace]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(30, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [NoWhiteSpace]
        public string Ci { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "{0} es obligatorio")]
        public int SpecialtyId { get; set; }

        [FutureDate]
        public DateTime? BirthDate { get; set; }

        [StringLength(100, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [FirstLetterUpperCase]
        [NoWhiteSpace]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [NoWhiteSpace]
        public string Tel { get; set; }

        [EmailAddress(ErrorMessage = "{0} no es valido")]
        [StringLength(100, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [NoWhiteSpace]
        public string Email { get; set; }
    }
}
