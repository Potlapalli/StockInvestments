using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    public class ClosedPositionDto
    {
        public string Ticker { get; set; }

        public string Company { get; set; }

        public double FinalValue { get; set; }
    }
}
