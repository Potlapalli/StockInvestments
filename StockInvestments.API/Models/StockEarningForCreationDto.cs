using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.Helpers;
using StockInvestments.API.ValidationAttributes;

namespace StockInvestments.API.Models
{
    [EarningsCallTimeShouldBeAMOrPMAtrribute(ErrorMessage = "Invalid Earnings call time provided. The value should be AM or PM")]
    public class StockEarningForCreationDto /*: IValidatableObject*/
    {
        [Required]
        public string Ticker { get; set; }

        public string Company { get; set; }

        [Required(ErrorMessage = "Earnings date is required.")]
        public DateTimeOffset EarningsDate { get; set; }

        [MaxLength(2)]
        public string EarningsCallTime { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (
        //        EarningsCallTime != nameof(EarningsCallTimeEnum.AM) &&
        //        EarningsCallTime != nameof(EarningsCallTimeEnum.PM))
        //    {
        //        yield return new ValidationResult("Invalid Earnings call time provided. The value should be AM or PM.", 
        //            new []{ "StockEarningForCreationDto" });
        //    }
        //}
    }
}
