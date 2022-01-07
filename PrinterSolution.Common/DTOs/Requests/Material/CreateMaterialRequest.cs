using PrinterSolution.Common.Utils.Enum;

namespace PrinterSolution.Common.DTOs.Requests
{
    public class CreateMaterialRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal PricePerKilo { get; set; }
        public MaterialType Type { get; set; }
    }
}
