using System.ComponentModel.DataAnnotations;

namespace paycoreHW02.Attributes;

public class EmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Convert value object to string named email.
        var email = value as string;
        // Checking email is null or not.
        if (email == null) return ValidationResult.Success;

        // Is this a valid email address or not.
        if (ValidateUsingDefaultEmailValidator(email) && ValidateUsingCustomStaffEmailRules(email))
            return ValidationResult.Success;
        // If email control is failed then send ValidationResult errorMessage.
        return new ValidationResult("Invalid email. Email can not contain digits and special characters except '.'.");
    }
    // My custom EmailAddressAttribute
    private static bool ValidateUsingCustomStaffEmailRules(string email)
    {
        // Taking left side of email.(if Email is abc.def@gmail.com, then left side of email is abc.def )
        var leftPart = email.Substring(0, email.IndexOf('@'));
        foreach (var ch in leftPart)
        {
            //Looking every chars in leftPart of email.
            //If it is equal to '.' then it can continue.
            if (ch == '.') continue;
            // If its not letter(has digit or special char except '.') then email format is wrong form.
            if (!char.IsLetter(ch)) return false;
        }
        //Otherwise everything is ok. Email is correct form.
        return true;
    }
    // Default EmailAddressAttribute according to .NET.
    private static bool ValidateUsingDefaultEmailValidator(string emailToValidate)
    {
        var emailAttribute = new EmailAddressAttribute();

        return emailAttribute.IsValid(emailToValidate);
    }
}