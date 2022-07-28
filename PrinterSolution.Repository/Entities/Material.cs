namespace PrinterSolution.Repository.Entities
{
    public class Material : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal PricePerKilo { get; set; }
        public decimal Weight { get; set; }
        public decimal WeightLeft { get; set; }
        public MaterialType Type { get; set; }
    }
}
