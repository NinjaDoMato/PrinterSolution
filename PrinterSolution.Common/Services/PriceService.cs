using PrinterSolution.Common.Database;
using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Services
{
    public interface IPriceService
    {
        public decimal EstimateFinalPrice(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime);
        public decimal EstimateProductionCost(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime);
    }


    public class PriceService : IPriceService
    {
        public decimal EstimateFinalPrice(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime)
        {
            var productionCost = EstimateProductionCost(weight, materialCode, hoursPrinting, manualWorkTime);
            var finalPrice = productionCost;
            using (var ctx = new DatabaseContext())
            {
                var rules = ctx.PriceRule.Where(r =>
                    r.Target == PriceRuleTarget.FinalPrice);

                foreach (var rule in rules.OrderByDescending(r => r.Priority))
                {
                    finalPrice = ApplyPriceRule(finalPrice, rule.Value, rule.Operation);
                }
            }

            return finalPrice;
        }

        public decimal EstimateProductionCost(decimal weight, string materialCode, decimal hoursPrinting, decimal preparationTime)
        {
            if (string.IsNullOrEmpty(materialCode))
                throw new ArgumentException("Please provide a valid Material Code.");

            if (weight <= 0 || hoursPrinting <= 0 || preparationTime < 0)
                throw new ArgumentException("This parameters are not valid.");

            decimal cost = 0m;

            using (var ctx = new DatabaseContext())
            {
                var rules = ctx.PriceRule.Where(r =>
                    r.Target == PriceRuleTarget.Preparation ||
                    r.Target == PriceRuleTarget.EnergyCost ||
                    r.Target == PriceRuleTarget.MaterialCost);

                var material = ctx.Material.FirstOrDefault(m => m.Code == materialCode);

                if (material == null)
                    throw new ArgumentException("Material not found.");

                var energyPrice = Convert.ToDecimal(ctx.Configuration.FirstOrDefault(c => c.Code == "kWh"));
                var averagePowerUse = Convert.ToDecimal(ctx.Configuration.FirstOrDefault(c => c.Code == "AvgkWh"));
                var preparationPrice = Convert.ToDecimal(ctx.Configuration.FirstOrDefault(c => c.Code == "MWC"));

                var materialCost = weight * material.PricePerKilo;
                var energyCost = energyPrice * averagePowerUse * hoursPrinting;
                var preparationCost = preparationPrice * preparationTime;

                foreach (var rule in rules.OrderByDescending(r => r.Priority))
                {
                    switch (rule.Target)
                    {
                        case PriceRuleTarget.EnergyCost:
                            energyCost = ApplyPriceRule(energyCost, rule.Value, rule.Operation);
                            break;

                        case PriceRuleTarget.Preparation:
                            preparationCost = ApplyPriceRule(preparationCost, rule.Value, rule.Operation);
                            break;

                        case PriceRuleTarget.MaterialCost:
                            materialCost = ApplyPriceRule(materialCost, rule.Value, rule.Operation);
                            break;
                    }
                }

                cost = materialCost + energyCost + preparationCost;
            }

            return cost;
        }

        private decimal ApplyPriceRule(decimal originalAmount, decimal ruleValue, PriceRuleOperation type)
        {
            var result = originalAmount;
            switch (type)
            {
                case PriceRuleOperation.Add:
                    result += ruleValue;
                    break;

                case PriceRuleOperation.Divide:
                    result = originalAmount / ruleValue;
                    break;

                case PriceRuleOperation.Multiply:
                    result = originalAmount * ruleValue;
                    break;

                case PriceRuleOperation.Subtract:
                    result -= ruleValue;
                    break;
            }

            return result;
        }
    }
}
