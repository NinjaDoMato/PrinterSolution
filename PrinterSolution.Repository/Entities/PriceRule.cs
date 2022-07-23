namespace PrinterSolution.Repository.Entities
{
    public class PriceRule : BaseEntity
    {
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
