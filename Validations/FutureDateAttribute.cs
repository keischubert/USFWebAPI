using System.ComponentModel.DataAnnotations;

namespace USFWebAPI.Validations
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }

            if(value is not DateTime)
            {
                return new ValidationResult("El formato no corresponde a \"mes-dia-año\"");
            }

            var today = DateTime.Now;
            var birthDate = (DateTime)value;

            if (birthDate > today) 
            {
                return new ValidationResult($"{validationContext.DisplayName} debe ser una fecha antes del dia de hoy");
            }

            return ValidationResult.Success;
        }
    }
}
