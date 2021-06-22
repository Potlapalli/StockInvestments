using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Entities
{
    public class ClosedPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Number { get; set; }

        [Required(ErrorMessage = "Ticker is required.")]
        public string Ticker { get; set; }

        public string Company { get; set; }

        [Required]
        public double FinalValue { get; set; }
    }
}
