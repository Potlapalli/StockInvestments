using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    public class CurrentPositionDto
    {
        public string Ticker { get; set; }

        public string Company { get; set; }

        public double PurchasePrice { get; set; }

        public double TotalShares { get; set; }

        public double TotalAmount { get; set; }
    }
}
