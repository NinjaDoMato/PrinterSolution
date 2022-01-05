using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.PriceAPI.Models.Requests
{
    public class EstimatePriceRequest
    {
        public string MaterialCode { get; set; }
        public decimal Weight { get; set; }
        public decimal HoursPrinting { get; set; }
        public decimal PreparationTime { get; set; }
    }
}
