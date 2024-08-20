using USFWebAPI.Entities;

namespace USFWebAPI.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ci { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; } //estableceremos para que AutoMapper le asigne el valor del nombre
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
    }
}
