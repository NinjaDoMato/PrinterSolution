using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.DTOs
{
    public class DetailedPriceEstimation
    {
        public decimal FinalPrice { get; set; }
        public decimal TotalProductionCost { get; set; }
        public decimal TotalEnergyCost { get; set; }
        public decimal TotalMaterialCost { get; set; }
        public decimal TotalPreparationCost { get; set; }
        public decimal TotalAditionalCost { get; set; }
        public List<Detail> Details { get; set; }

        public DetailedPriceEstimation()
        {
            Details = new List<Detail>();
        }
    }

    public class Detail
    {
        public PriceRuleTarget Target { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
       
    }
}
