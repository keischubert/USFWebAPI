using System.ComponentModel.DataAnnotations;
using USFWebAPI.Validations;

namespace USFWebAPI.DTOs
{
    public class CreateGenderDTO
    {
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [FirstLetterUpperCase]
        [NoWhiteSpace]
        public string Name { get; set; }
    }
}
