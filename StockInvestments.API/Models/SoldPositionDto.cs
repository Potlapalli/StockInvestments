using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    public class SoldPositionDto
    {
        public long Number { get; set; }

        public double SellingPrice { get; set; }

        public double TotalShares { get; set; }

        public string Ticker { get; set; }
    }
}
