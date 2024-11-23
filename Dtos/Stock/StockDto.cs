using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        // we wanna identify our stock with a string(AKA word) that is a symbol
        // public string? Symbol { get; set; }
        public string Symbol { get; set; } = string.Empty; // string.Empty is putting "" in the symbol variable
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}