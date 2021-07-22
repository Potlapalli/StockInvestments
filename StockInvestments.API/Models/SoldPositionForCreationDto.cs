using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    public class SoldPositionForCreationDto
    {
        public double SellingPrice { get; set; }

        public double TotalShares { get; set; }

        public double TotalAmount { get; set; }
    }
}
