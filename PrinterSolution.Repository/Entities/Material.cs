namespace PrinterSolution.Repository.Entities
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
