using PrinterSolution.Common.DTOs;
using PrinterSolution.PriceAPI.Models.Requests;
using PrinterSolution.Repository.Interfaces;
using System.Globalization;

namespace PrinterSolution.Service.Services
{
    public class PriceService : IPriceService
    {
        private readonly IRepository<PriceRule> priceRuleRepository;
        private readonly IRepository<Material> materialRepository;
        private readonly IRepository<Configuration> configurationRepository;

        public PriceService(
            IRepository<PriceRule> priceRuleRepository,
            IRepository<Material> materialRepository,
            IRepository<Configuration> configurationRepository)
        {
            this.priceRuleRepository = priceRuleRepository;
            this.materialRepository = materialRepository;
            this.configurationRepository = configurationRepository;
        }

        public decimal EstimateFinalPrice(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime)
        {
            var productionCost = EstimateProductionCost(weight, materialCode, hoursPrinting, manualWorkTime);
            var finalPrice = productionCost;

            var rules = priceRuleRepository.Where(r =>
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


            IOrderedEnumerable<PriceRule> rules = priceRuleRepository.Where(r =>
                r.Target == PriceRuleTarget.Preparation ||
                r.Target == PriceRuleTarget.EnergyCost ||
                r.Target == PriceRuleTarget.MaterialCost).OrderBy(p => p.Priority);

            var material = materialRepository.FirstOrDefault(m => m.Code == materialCode);

            if (material == null)
                throw new ArgumentException("Material not found.");

            decimal energyPrice = decimal.Parse(configurationRepository.Single(c => c.Code == "kWh").Value, CultureInfo.InvariantCulture);
            decimal averagePowerUse = decimal.Parse(configurationRepository.Single(c => c.Code == "AvgkWh").Value, CultureInfo.InvariantCulture);
            decimal preparationPrice = decimal.Parse(configurationRepository.Single(c => c.Code == "MWC").Value, CultureInfo.InvariantCulture);

            decimal materialCost = weight * material.PricePerKilo;
            decimal energyCost = energyPrice * averagePowerUse * hoursPrinting;
            decimal preparationCost = preparationPrice * preparationTime;

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


            var rules = priceRuleRepository.Where(r => r.Status).OrderBy(p => p.Priority);

            var material = materialRepository.Single(m => m.Code == materialCode);


            var energyPrice = Decimal.Parse(configurationRepository.Single(c => c.Code == "kWh").Value, CultureInfo.InvariantCulture);
            var averagePowerUse = Decimal.Parse(configurationRepository.Single(c => c.Code == "AvgkWh").Value, CultureInfo.InvariantCulture);
            var preparationPrice = Decimal.Parse(configurationRepository.Single(c => c.Code == "MWC").Value, CultureInfo.InvariantCulture);

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

        public DetailedPriceEstimation EstimateDetailedCosts(EstimatePriceRequest request)
        {
            return EstimateDetailedCosts(request.Weight, request.MaterialCode, request.HoursPrinting, request.PreparationTime);
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
