using System.ComponentModel.DataAnnotations;

namespace paycoreHW02.Models;

public class Staff
{

    public int Id { get; set; }
    public string Name { get; set; } = null!; // null-forgiving operation for = null!;
    public string Lastname { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public decimal Salary { get; set; }

}