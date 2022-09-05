using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using paycoreHW1.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace paycoreHW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompoundInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<CompoundInterestModel> CalculateInterestIncome([FromQuery] CompoundInterest compoundInterest)
        {
            // If Due,Principle or InterestRate are not valid like Due < 0 or InterestRate > 1.Then this have to be checked.
            if (compoundInterest.Due < 0 || (compoundInterest.Principle < 0) || (compoundInterest.InterestRate < 0 && compoundInterest.InterestRate > 1))
                return Ok(new { message = "Please enter valid parameters." });
            // Creating CompoundInterest object.
            var element = new CompoundInterest 
            {
                Principle = compoundInterest.Principle,
                InterestRate = compoundInterest.InterestRate,
                Due = compoundInterest.Due
            };

            var totalBalance = element.Principle * Math.Pow(1 + element.InterestRate,element.Due);// A = a+(1+r)^t
            var interestAmount = totalBalance-element.Principle; // A-a = interestAmount

            // Round the values
            totalBalance = Math.Round(totalBalance); 
            interestAmount = Math.Round(interestAmount);
            // Creating CompoundInterestModel 
            var result = new CompoundInterestModel 
            {
                // Type casting operation with Convert.ToInt (double to int)
                InterestAmount = Convert.ToInt32(interestAmount),
                TotalBalance = Convert.ToInt32(totalBalance)
            };
            // Return the result
            return Ok(result); 
        }


    }
}

