using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using paycoreHW02.Attributes;

namespace paycoreHW02.Models;

public class StaffModel
{
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    // Name's minLength = 2 , maxLength = 120.
    [StringLength(maximumLength:120,MinimumLength = 2, ErrorMessage = "Name area must be range in 2-120")]
    public string? Name { get; set; } 
    
    [Required(ErrorMessage = "Lastname is required.")]
    // Lastname's minLength = 2 , maxLength = 120.
    [StringLength(maximumLength:120,MinimumLength = 2, ErrorMessage = "Lastname area must be range in 20-120")]
    public string? Lastname { get; set; }
    
    [Required(ErrorMessage = "DateOfBirth is required.")]
    //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
    // My Custom Attribute for Staffs DateOfBirth
    [DateOfBirth]
    public string? DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    //[EmailAddress(ErrorMessage = "Email is invalid.")]
    //My Custom Attribute for Staffs Email.
    [Email]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "PhoneNumber is required.")]
    //[RegularExpression(pattern:"^[+]+[90][0-9]{11}")]
    [PhoneNumber]
    public string? PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Salary is required.")]
    
    // Salary (min = 2000 - max = 9000)
    [Range(minimum:2000,maximum:9000,ErrorMessage = "Salary must be in range 2000-9000.")]
    public decimal Salary { get; set; }
}