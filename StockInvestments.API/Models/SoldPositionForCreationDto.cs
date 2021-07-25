using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    public class SoldPositionForCreationDto
    {
        [Required(ErrorMessage = "SellingPrice is required.")]
        public double SellingPrice { get; set; }

        [Required(ErrorMessage = "TotalShares is required.")]
        public double TotalShares { get; set; }

        [Required(ErrorMessage = "TotalAmount is missing.")]
        public double TotalAmount { get; set; }
    }
}
