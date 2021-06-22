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

        public string Ticker { get; set; }

        public CurrentPosition CurrentPosition { get; set; }
    }
}
