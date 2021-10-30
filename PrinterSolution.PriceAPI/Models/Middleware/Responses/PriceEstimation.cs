using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.PriceAPI.Models.Middleware.Responses
{
    public class PriceEstimation
    {
        public decimal ProductionCost { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
