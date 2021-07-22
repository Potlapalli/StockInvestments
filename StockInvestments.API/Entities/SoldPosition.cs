using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Entities
{
    public class SoldPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Number { get; set; }

        [Required(ErrorMessage = "SellingPrice is required.")]
        public double SellingPrice { get; set; }

        [Required(ErrorMessage = "TotalShares is required.")]
        public double TotalShares { get; set; }

        [Required(ErrorMessage = "TotalAmount is missing.")]
        public double TotalAmount { get; set; }

        [Required(ErrorMessage = "Ticker is missing.")]
        public string Ticker { get; set; }

        [ForeignKey("Ticker")]
        public CurrentPosition CurrentPosition { get; set; }
    }
}
