using System.ComponentModel.DataAnnotations;

namespace USFWebAPI.Validations
{
    public class FirstLetterUpperCaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var strValue = value as string;

                if (string.IsNullOrEmpty(strValue) || char.IsLower(strValue[0]))
                {
                    return new ValidationResult(ErrorMessage ?? "The first letter must be upper");
                }
            }

            return ValidationResult.Success;
        }

    }
}
