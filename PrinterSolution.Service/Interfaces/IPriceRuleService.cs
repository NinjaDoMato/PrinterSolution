namespace PrinterSolution.Service.Interfaces
{
    public interface IPriceRuleService
    {
        public PriceRule GetRuleById(int id);
        public List<PriceRule> GetRules();
        public List<PriceRule> GetRulesByType(PriceRuleOperation type);
        public List<PriceRule> GetRulesByTarget(PriceRuleTarget target);
        public PriceRule CreateRule(string name, string code, string description, PriceRuleTarget target, PriceRuleOperation type, decimal amount, int priority = 0);
        public PriceRule UpdateRule(PriceRule newRule);
        public bool DelceteRule(int id);
    }
}
