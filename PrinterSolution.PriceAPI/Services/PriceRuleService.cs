using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.PriceAPI.Services
{
    public interface IPriceRuleService
    {
        public PriceRule GetRuleById(int id);
        public List<PriceRule> GetRules();
        public List<PriceRule> GetRulesByType(PriceRuleOperation type);
        public List<PriceRule> GetRulesByTarget(PriceRuleTarget target);
        public PriceRule CreateRule(string name, string description, PriceRuleTarget target, PriceRuleOperation type, decimal amount);
        public PriceRule UpdateRule(PriceRule newRule);
        public PriceRule DelceteRule(int id);
    }

    public class PriceRuleService : IPriceRuleService
    {
        public PriceRule CreateRule(string name, string description, PriceRuleTarget target, PriceRuleOperation type, decimal amount)
        {
            throw new NotImplementedException();
        }

        public PriceRule DelceteRule(int id)
        {
            throw new NotImplementedException();
        }

        public PriceRule GetRuleById(int id)
        {
            throw new NotImplementedException();
        }

        public List<PriceRule> GetRules()
        {
            throw new NotImplementedException();
        }

        public List<PriceRule> GetRulesByTarget(PriceRuleTarget target)
        {
            throw new NotImplementedException();
        }

        public List<PriceRule> GetRulesByType(PriceRuleOperation type)
        {
            throw new NotImplementedException();
        }

        public PriceRule UpdateRule(PriceRule newRule)
        {
            throw new NotImplementedException();
        }
    }
}
