namespace PrinterSolution.Repository.Entities
{
    public class PriceRule : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public int Priority { get; set; }
        public bool Status { get; set; }
        public PriceRuleTarget Target { get; set; }
        public PriceRuleOperation Operation { get; set; }
    }
}
