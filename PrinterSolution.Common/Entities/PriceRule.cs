using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Entities
{
    public class PriceRule
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Priority { get; set; }
        public bool Status { get; set; }
        public PriceRuleTarget Target { get; set; }
        public PriceRuleOperation Operation { get; set; }
    }
}
