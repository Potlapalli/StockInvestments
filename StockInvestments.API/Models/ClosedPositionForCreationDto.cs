using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    public class ClosedPositionForCreationDto
    {
        [Required(ErrorMessage = "Ticker is required.")]
        public string Ticker { get; set; }

        public string Company { get; set; }

        [Required]
        public double FinalValue { get; set; }
    }
}
