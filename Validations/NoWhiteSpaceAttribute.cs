using System.ComponentModel.DataAnnotations;

namespace USFWebAPI.Validations
{
    public class NoWhiteSpaceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is null)
            {
                return ValidationResult.Success;
            }

            if(value is string)
            {
                var strValue = value as string;

                if (string.IsNullOrEmpty(strValue) || char.IsWhiteSpace(strValue[0]))
                {
                    return new ValidationResult($"{validationContext.DisplayName} no puede comenzar con espacio en blanco");
                }
            }

            return ValidationResult.Success;
        }
    }
}
