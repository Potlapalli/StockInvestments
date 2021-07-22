using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Entities
{
    public class StockEarning
    {
        [Key]
        public string Ticker { get; set; }

        public string Company { get; set; }

        [Required(ErrorMessage = "Earnings date is required.")]
        public DateTimeOffset EarningsDate { get; set; }

        public string EarningsCallTime { get; set; }

    }
}
