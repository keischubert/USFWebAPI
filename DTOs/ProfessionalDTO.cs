using System.ComponentModel.DataAnnotations;
using USFWebAPI.Entities;
using USFWebAPI.Validations;

namespace USFWebAPI.DTOs
{
    public class ProfessionalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ci { get; set; }
        public int GenderId { get; set; } //propiedad de navegacion
        public int SpecialtyId { get; set; } //propiedad de navegacion
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
    }
}
