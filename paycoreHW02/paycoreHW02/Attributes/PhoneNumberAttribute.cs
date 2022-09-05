using System.ComponentModel.DataAnnotations;

namespace paycoreHW02.Attributes;

public class PhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Taking phoneNumberString
        var phoneNumberString = value as string;
        //Control is it null or not.(Those controls for compile bc [Required] also used.)
        if (phoneNumberString == null) return ValidationResult.Success;
        // Taking our country code which is +90
        var code = phoneNumberString.Substring(0, 3);
        // Checking country code
        bool isValidCode = code.Equals("+90");
        // Checking length of the phone is valid.
        bool isValidLength = phoneNumberString.Length == 13;
        // Checking after '+90', the whole chars are digit or not
        bool isDigit = phoneNumberString.Substring(3).Any(x => char.IsDigit(x));

        // Control, using if statement
        if (isValidCode && isValidLength && isDigit)
        {
            // If all checking operations true then Success.
            return ValidationResult.Success;
        }
        // Otherwise return invalid.
        return new ValidationResult(
            errorMessage: "Invalid phone number. Phone number must be starts with +90 and remains are digit form.");
    }
}