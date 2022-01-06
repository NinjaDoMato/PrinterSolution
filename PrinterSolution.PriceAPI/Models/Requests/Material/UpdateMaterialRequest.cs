using PrinterSolution.Common.Utils.Enum;

namespace PrinterSolution.PriceAPI.Models.Requests
{
    public class UpdateMaterialRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal PricePerKilo { get; set; }
        public MaterialType Type { get; set; }
    }
}
