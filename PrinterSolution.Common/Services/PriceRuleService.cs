using PrinterSolution.Common.Database;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.Common.Validators;
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

            var validator = new PriceRuleValidator();
            validator.ValidateAndHandle(priceRule);

            if (_ctx.PriceRules.Any(p => p.Name == name))
                throw new ArgumentException("This name is already used by another price rule.");

            if (_ctx.PriceRules.Any(p => p.Code == name))
                throw new ArgumentException("This code is already used by another price rule.");

            _ctx.PriceRules.Add(priceRule);
            _ctx.SaveChanges();

            return priceRule;
        }

        public bool DelceteRule(int id)
        {
            var priceRule = _ctx.PriceRules.Find(id);

            if (priceRule == null)
                throw new KeyNotFoundException("Price Rule not found.");

            _ctx.PriceRules.Remove(priceRule);

            return true;
        }

        public PriceRule GetRuleById(int id)
        {
            return _ctx.PriceRules.Find(id);
        }

        public List<PriceRule> GetRules()
        {
            return _ctx.PriceRules.ToList();
        }

        public List<PriceRule> GetRulesByTarget(PriceRuleTarget target)
        {
            return _ctx.PriceRules.Where(p => p.Target == target).ToList();
        }

        public List<PriceRule> GetRulesByType(PriceRuleOperation type)
        {
            return _ctx.PriceRules.Where(p => p.Operation == type).ToList();
        }

        public PriceRule UpdateRule(PriceRule newRule)
        {
            var validator = new PriceRuleValidator();
            validator.ValidateAndHandle(newRule);

            if (_ctx.PriceRules.Any(p => p.Id != newRule.Id && (p.Name == newRule.Name || p.Code == newRule.Code)))
                throw new Exception("This name or code is already used.");

            _ctx.PriceRules.Update(newRule);

            return newRule;
        }
    }
}
