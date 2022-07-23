using PrinterSolution.Common.DTOs;

namespace PrinterSolution.Service.Interfaces
{
    public interface IPriceService
    {
        public decimal EstimateFinalPrice(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime);
        public decimal EstimateProductionCost(decimal weight, string materialCode, decimal hoursPrinting, decimal manualWorkTime);
        public DetailedPriceEstimation EstimateDetailedCosts(decimal weight, string materialCode, decimal hoursPrinting, decimal preparationTime);
    }
}
