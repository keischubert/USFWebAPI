using System.ComponentModel.DataAnnotations;
using USFWebAPI.Validations;

namespace USFWebAPI.Entities
{
    public class Gender
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20)]
        [FirstLetterUpperCaseAttribute(ErrorMessage = "El nombre del género debe comenzar con mayúscula")]
        [NoWhiteSpace(ErrorMessage = "El nombre del genero no puede comenzar con espacio en blanco")]
        public string Name { get; set; }
    }
}
