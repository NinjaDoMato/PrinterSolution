using PrinterSolution.Common.DTOs.Requests;

namespace PrinterSolution.Service.Interfaces
{
    public interface IPriceRuleService
    {
        public PriceRule GetRuleById(int id);
        public List<PriceRule> GetRules();
        public List<PriceRule> GetRulesByType(PriceRuleOperation type);
        public List<PriceRule> GetRulesByTarget(PriceRuleTarget target);
        public PriceRule CreateRule(CreatePriceRuleModel request);
        public PriceRule UpdateRule(PriceRule newRule);
        public bool DeleteRule(int id);
    }
}
