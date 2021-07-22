using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Entities
{
    public class CurrentPosition
    {
        [Key]
        public string Ticker { get; set; }

        public string Company { get; set; }

        [Required(ErrorMessage = "PurchasePrice is required.")]
        public double PurchasePrice { get; set; }

        [Required(ErrorMessage = "TotalShares is required.")]
        public double TotalShares { get; set; }

        [Required(ErrorMessage = "TotalAmount is missing.")]
        public double TotalAmount { get; set; }

        public ICollection<SoldPosition> SoldPositions { get; set; } = new List<SoldPosition>();
    }
}
