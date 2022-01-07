using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Entities
{
    public class Material : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal PricePerKilo { get; set; }
        public decimal Weight { get; set; }
        public decimal WeightLeft { get; set; }
        public MaterialType Type { get; set; }
    }
}
