using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Orla.Core.Common.Extensions;

public  class ValidDateAttribute: ValidationAttribute
{
    private readonly string _dateFormat;

    public ValidDateAttribute(string dateFormat = "dd/MM/yyyy")
    {
        _dateFormat = dateFormat;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is string dateString)
        {
            if (DateTime.TryParseExact(dateString, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue))
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult(ErrorMessage ?? $"Data inválida!");
    }
}
