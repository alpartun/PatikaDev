using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using paycoreHW02.Models;

namespace paycoreHW02.Controllers;

[Route("api/[controller]")]
[ApiController]

public class StaffController : ControllerBase
{
    //StaffList
    private static  List<Staff> StaffList = new List<Staff>();
    // Constructor
    public StaffController()
    {

    }
    // Get all Staffs in StaffList
    [HttpGet]
    public IActionResult Get()
    {
        // Checking StaffList is empty or not. If its not empty then StaffList items will send.
        return StaffList.Count == 0 ? Ok(new { message = "StaffList is empty" }) : Ok(StaffList);
    }
    // Getting Specific Staff via id.
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        //Finding our Staff in StaffList.
        var result = StaffList.FirstOrDefault(x => x.Id == id);
        // If its not found. Then Staff not exists message send. If its found then Staff will send.
        return result == null ? Ok(new { message = "Staff not exists." }) : Ok(result);
    }
    // Post(Create) operation.
    [HttpPost("Post")]
    public IActionResult Post([FromBody]StaffModel? staffModel)
    {
        // Checking staffModel is null or not. If its null then send message.
        if (staffModel == null) return Ok(new { message = "Invalid model." });
        
        var item = new Staff
        {
            Id =  staffModel.Id,
            Name = staffModel.Name!,
            Lastname = staffModel.Lastname!,
            DateOfBirth =  DateTime.ParseExact(staffModel.DateOfBirth!,"dd-MM-yyyy",CultureInfo.InvariantCulture), //Its validated in an Attribute of DateofBirth 
            Email = staffModel.Email!,
            PhoneNumber = staffModel.PhoneNumber!,
            Salary = staffModel.Salary
        };
        // Add our new Staff to list.
        StaffList.Add(item);
        return Ok(new{message = "Staff is added successfully."});
    }
    // Update(edit) operation.
    [HttpPut("Put")]
    public IActionResult Put(int id,[FromBody] StaffModel staffModel)
    {
        // we are going to check if the given id match or not.
        var removeStaff  = StaffList.FirstOrDefault(x => x.Id == id);
        // if its not match then removeStaff is null. We are going to send message.
        if (removeStaff == null) return Ok(new{message = "Staff can not be founded."});
        // If its not null, create Staff object.
        Staff staff = new Staff
        {
            Id = staffModel.Id,
            Name = staffModel.Name!,
            Lastname = staffModel.Lastname!,
            DateOfBirth =  DateTime.Parse(staffModel.DateOfBirth!), //Its validated in an Attribute of DateofBirth 
            Email = staffModel.Email!,
            PhoneNumber = staffModel.PhoneNumber!,
            Salary = staffModel.Salary
        };
        // remove our old staff
        StaffList.Remove(removeStaff);
        //add our updated staff
        StaffList.Add(staff);
        return Ok(new{message = $"Staff is changed."});
    }
    // Delete operation.
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        // Looking in list if the given id parameter match or not.
        var deleteStaff = StaffList.FirstOrDefault(x => x.Id == id);
        // If deleteStaff is not found in list then message send.
        if (deleteStaff == null) return Ok(new { message = "Staff not found." });
        // Deletion of staff in StaffList.
        StaffList.Remove(deleteStaff);
        return Ok(new{message="Deletion is successfully."});
    }
}