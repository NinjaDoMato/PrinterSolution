using PrinterSolution.Common.Database;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrinterSolution.Common.Services
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

    public class PriceRuleService : IPriceRuleService
    {
        private readonly DatabaseContext _ctx;

        public PriceRuleService(DatabaseContext context)
        {
            _ctx = context;
        }

        public PriceRule CreateRule(string name, string code, string description, PriceRuleTarget target, PriceRuleOperation type, decimal amount, int priority)
        {
            if (_ctx.PriceRule.Any(p => p.Name == name))
                throw new ArgumentException("This name is already used by another price rule.");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");

            if (string.IsNullOrEmpty(code))
                throw new ArgumentException("Code cannot be empty");

            var priceRule = new PriceRule
            {
                Name = name,
                Description = description,
                Target = target,
                Operation = type,
                Value = amount,
                Code = code,
                Priority = priority,
                Status = true
            };

            _ctx.PriceRule.Add(priceRule);
            _ctx.SaveChanges();

            return priceRule;
        }

        public bool DelceteRule(int id)
        {
            var priceRule = _ctx.PriceRule.Find(id);

            if (priceRule == null)
                throw new KeyNotFoundException("Price Rule not found.");

            _ctx.PriceRule.Remove(priceRule);

            return true;
        }

        public PriceRule GetRuleById(int id)
        {
            return _ctx.PriceRule.Find(id);
        }

        public List<PriceRule> GetRules()
        {
            return _ctx.PriceRule.ToList();
        }

        public List<PriceRule> GetRulesByTarget(PriceRuleTarget target)
        {
            return _ctx.PriceRule.Where(p => p.Target == target).ToList();
        }

        public List<PriceRule> GetRulesByType(PriceRuleOperation type)
        {
            return _ctx.PriceRule.Where(p => p.Operation == type).ToList();
        }

        public PriceRule UpdateRule(PriceRule newRule)
        {
            if (_ctx.PriceRule.Any(p => p.Id != newRule.Id && (p.Name == newRule.Name || p.Code == newRule.Code)))
                throw new Exception("This name or code is already used.");

            _ctx.PriceRule.Update(newRule);

            return newRule;
        }
    }
}
