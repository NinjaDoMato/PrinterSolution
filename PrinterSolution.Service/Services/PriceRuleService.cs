using AutoMapper;
using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.Common.Utils.Validators;
using PrinterSolution.Repository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PrinterSolution.Service.Services
{
    public class PriceRuleService : IPriceRuleService
    {
        private readonly IRepository<PriceRule> repository;
        private readonly IMapper mapper;

        public PriceRuleService(IRepository<PriceRule> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public PriceRule CreateRule(CreatePriceRuleModel request)
        {
            PriceRule priceRule = mapper.Map<PriceRule>(request);

            priceRule.Status = true;

            if (repository.Where(p => p.Name == priceRule.Name).Any())
                throw new ArgumentException("This name is already used by another price rule.");

            if (repository.Where(p => p.Code == priceRule.Code).Any())
                throw new ArgumentException("This code is already used by another price rule.");

            repository.Insert(priceRule);

            return priceRule;
        }

        public bool DeleteRule(int id)
        {
            var priceRule = repository.FirstOrDefault(p => p.Id == id);

            if (priceRule == null)
                throw new KeyNotFoundException("Price Rule not found.");

            repository.Delete(priceRule);

            return true;
        }

        public PriceRule GetRuleById(int id)
        {
            return repository.Single(p => p.Id == id);
        }

        public List<PriceRule> GetRules()
        {
            return repository.Where(p => true).ToList();
        }

        public List<PriceRule> GetRulesByTarget(PriceRuleTarget target)
        {
            return repository.Where(p => p.Target == target).ToList();
        }

        public List<PriceRule> GetRulesByType(PriceRuleOperation type)
        {
            return repository.Where(p => p.Operation == type).ToList();
        }

        public PriceRule UpdateRule(PriceRule newRule)
        {
            if (repository.Where(p => p.Id != newRule.Id && (p.Name == newRule.Name || p.Code == newRule.Code)).Any())
                throw new Exception("This name or code is already used.");

            repository.Update(newRule);

            return newRule;
        }
    }
}
