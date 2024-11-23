using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        public int Id { get; set; }
        // we wanna identify our stock with a string(AKA word) that is a symbol
        // public string? Symbol { get; set; }
        public string Symbol { get; set; } = string.Empty; // string.Empty is putting "" in the symbol variable
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")] // here the column can store value of 18 digit long with 2 precision
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}