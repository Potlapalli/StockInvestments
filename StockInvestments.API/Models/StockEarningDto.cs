using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    public class StockEarningDto
    {
        public string Ticker { get; set; }

        public string Company { get; set; }

        public DateTimeOffset EarningsDate { get; set; }

        public string EarningsCallTime { get; set; }
    }
}
