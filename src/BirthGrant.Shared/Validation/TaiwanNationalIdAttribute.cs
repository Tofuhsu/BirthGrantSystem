using System.ComponentModel.DataAnnotations;
using BirthGrant.Shared.Helpers;

namespace BirthGrant.Shared.Validation;

[AttributeUsage(
    AttributeTargets.Property |
    AttributeTargets.Field,
    AllowMultiple = false)]
public sealed class TaiwanNationalIdAttribute
    : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        var text = value as string;

        if (string.IsNullOrWhiteSpace(text))
        {
            return ValidationResult.Success;
        }

        if (TaiwanNationalIdValidator.IsValid(text))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(
            ErrorMessage ??
            $"{validationContext.DisplayName}格式錯誤，請確認身分證字號是否正確。");
    }
}