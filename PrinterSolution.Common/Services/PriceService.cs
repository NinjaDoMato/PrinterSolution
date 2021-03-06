using PrinterSolution.Common.Database;
using PrinterSolution.Common.DTOs;
using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Services
{
    public interface IPriceService
    {
        public decimal EstimateFinalPrice(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime);
        public decimal EstimateProductionCost(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime);
        public DetailedPriceEstimation EstimateDetailedCosts(decimal weight, string materialCode, decimal hoursPrinting, decimal preparationTime);
    }


    public class PriceService : IPriceService
    {
        private readonly DatabaseContext ctx;

        public PriceService(DatabaseContext databaseContext)
        {
            ctx = databaseContext;
        }

        public decimal EstimateFinalPrice(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime)
        {
            var productionCost = EstimateProductionCost(weight, materialCode, hoursPrinting, manualWorkTime);
            var finalPrice = productionCost;

            var rules = ctx.PriceRules.Where(r =>
                    r.Target == PriceRuleTarget.FinalPrice);

            foreach (var rule in rules.OrderByDescending(r => r.Priority))
            {
                finalPrice = ApplyPriceRule(finalPrice, rule.Value, rule.Operation);
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


            var rules = ctx.PriceRules.Where(r =>
                r.Target == PriceRuleTarget.Preparation ||
                r.Target == PriceRuleTarget.EnergyCost ||
                r.Target == PriceRuleTarget.MaterialCost).OrderBy(p => p.Priority);

            var material = ctx.Materials.FirstOrDefault(m => m.Code == materialCode);

            if (material == null)
                throw new ArgumentException("Material not found.");

            var energyPrice = Decimal.Parse(ctx.Configurations.FirstOrDefault(c => c.Code == "kWh").Value, CultureInfo.InvariantCulture);
            var averagePowerUse = Decimal.Parse(ctx.Configurations.FirstOrDefault(c => c.Code == "AvgkWh").Value, CultureInfo.InvariantCulture);
            var preparationPrice = Decimal.Parse(ctx.Configurations.FirstOrDefault(c => c.Code == "MWC").Value, CultureInfo.InvariantCulture);

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

            return cost;
        }


        public DetailedPriceEstimation EstimateDetailedCosts(decimal weight, string materialCode, decimal hoursPrinting, decimal preparationTime)
        {
            if (string.IsNullOrEmpty(materialCode))
                throw new ArgumentException("Please provide a valid Material Code.");

            if (weight <= 0 || hoursPrinting <= 0 || preparationTime < 0)
                throw new ArgumentException("This parameters are not valid.");

            var detailedPrice = new DetailedPriceEstimation();


            var rules = ctx.PriceRules.Where(r => r.Status).OrderBy(p => p.Priority);

            var material = ctx.Materials.FirstOrDefault(m => m.Code == materialCode);

            if (material == null)
                throw new ArgumentException("Material not found.");

            var energyPrice = Decimal.Parse(ctx.Configurations.FirstOrDefault(c => c.Code == "kWh").Value, CultureInfo.InvariantCulture);
            var averagePowerUse = Decimal.Parse(ctx.Configurations.FirstOrDefault(c => c.Code == "AvgkWh").Value, CultureInfo.InvariantCulture);
            var preparationPrice = Decimal.Parse(ctx.Configurations.FirstOrDefault(c => c.Code == "MWC").Value, CultureInfo.InvariantCulture);

            detailedPrice.TotalMaterialCost = weight * material.PricePerKilo;
            detailedPrice.TotalEnergyCost = energyPrice * averagePowerUse * hoursPrinting;
            detailedPrice.TotalPreparationCost = preparationPrice * preparationTime;

            // Apply the production rules
            foreach (var rule in rules.Where(r => r.Target != PriceRuleTarget.FinalPrice).OrderByDescending(r => r.Priority))
            {
                var amount = 0m;
                switch (rule.Target)
                {
                    case PriceRuleTarget.MaterialCost:
                        amount = ApplyPriceRule(detailedPrice.TotalMaterialCost, rule.Value, rule.Operation) - detailedPrice.TotalMaterialCost;
                        detailedPrice.TotalMaterialCost = ApplyPriceRule(detailedPrice.TotalMaterialCost, rule.Value, rule.Operation);
                        break;

                    case PriceRuleTarget.EnergyCost:
                        amount = ApplyPriceRule(detailedPrice.TotalEnergyCost, rule.Value, rule.Operation) - detailedPrice.TotalEnergyCost;
                        detailedPrice.TotalEnergyCost = ApplyPriceRule(detailedPrice.TotalEnergyCost, rule.Value, rule.Operation);
                        break;

                    case PriceRuleTarget.Preparation:
                        amount = ApplyPriceRule(detailedPrice.TotalPreparationCost, rule.Value, rule.Operation) - detailedPrice.TotalPreparationCost;
                        detailedPrice.TotalPreparationCost = ApplyPriceRule(detailedPrice.TotalPreparationCost, rule.Value, rule.Operation);
                        break;
                }

                detailedPrice.TotalAditionalCost += amount;
                detailedPrice.Details.Add(new Detail
                {
                    Name = rule.Name,
                    Description = rule.Description,
                    Amount = amount,
                    Target = rule.Target
                });
            }

            detailedPrice.TotalProductionCost = detailedPrice.TotalMaterialCost + detailedPrice.TotalPreparationCost + detailedPrice.TotalEnergyCost;
            detailedPrice.FinalPrice = detailedPrice.TotalProductionCost;


            // Apply the final price rules
            foreach (var rule in rules.Where(r => r.Target == PriceRuleTarget.FinalPrice).OrderBy(r => r.Priority))
            {
                var amount = ApplyPriceRule(detailedPrice.FinalPrice, rule.Value, rule.Operation) - detailedPrice.FinalPrice;

                detailedPrice.FinalPrice = ApplyPriceRule(detailedPrice.FinalPrice, rule.Value, rule.Operation);
                detailedPrice.TotalAditionalCost += amount;
                detailedPrice.Details.Add(new Detail
                {
                    Name = rule.Name,
                    Description = rule.Description,
                    Amount = amount,
                    Target = PriceRuleTarget.FinalPrice
                });
            }

            return detailedPrice;
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

                case PriceRuleOperation.AddPercentage:
                    result = originalAmount + (ruleValue / 100) * originalAmount;
                    break;
            }

            return result;
        }

    }
}
