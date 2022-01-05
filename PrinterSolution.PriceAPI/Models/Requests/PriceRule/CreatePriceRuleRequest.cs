using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.PriceAPI.Models.Requests.PriceRule
{
    public class CreatePriceRuleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Priority { get; set; }
        public string Code { get; set; }
        public PriceRuleTarget Target { get; set; }
        public PriceRuleOperation Type { get; set; }
    }
}
