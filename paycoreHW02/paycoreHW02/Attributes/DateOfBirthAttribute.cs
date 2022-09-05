using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace paycoreHW02.Attributes;

public class DateOfBirthAttribute : ValidationAttribute
{
    // Max and Min date of births
    private static readonly DateTime MAX_DATE_OF_BIRTH = new(2002, 10, 10);
    private static readonly DateTime MIN_DATE_OF_BIRTH = new(1945, 11, 11);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Converting value object to valueString
        var valueString = value as string;
        // Checking null or not.
        if (valueString == null) return ValidationResult.Success;
        // Using DateTimeTryParseExact method checking the valueString is DateTime format or not.
        var isValid = DateTime.TryParseExact(
            valueString,
            "dd-MM-yyyy", // Turkish DateTime format 
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var parsedDate // Create our DateTime object named parsedDate.
        );
        //If isValid false then send ValidationResult errorMessage.
        if (!isValid) return new ValidationResult("Invalid date format. Date format must be form in 'dd-MM-yyy'");
        //Creating msg variable.
        var msg =
            $"Please enter a value between {MIN_DATE_OF_BIRTH:dd-MM-yyyy} and {MAX_DATE_OF_BIRTH:dd-MM-yyyy}";
        //Compare our parsedDate(Staffs DateOfBirth) is between our min and max values.
        if (parsedDate > MAX_DATE_OF_BIRTH || parsedDate < MIN_DATE_OF_BIRTH)
            // If its not satisfied then send msg as ValidationResult errorMessage.
            return new ValidationResult(msg);
        // Otherwise ValidationResult is succeeded.
        return ValidationResult.Success;
    }
}