using PrinterSolution.Common.Utils.Enum;
using System.ComponentModel.DataAnnotations;

namespace PrinterSolution.Common.DTOs.Requests
{
    public class CreatePriceRuleModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [Range(0.01f, int.MaxValue, ErrorMessage = "Price rule value must be greater than zero.")]
        public decimal Value { get; set; }
        public int Priority { get; set; }
       
        [Required]
        public string Code { get; set; }
        public PriceRuleTarget Target { get; set; }
        public PriceRuleOperation Type { get; set; }
    }
}
