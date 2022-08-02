using PrinterSolution.Common.DTOs;
using PrinterSolution.PriceAPI.Models.Requests;
using PrinterSolution.Service.Interfaces;

namespace PrinterSolution.PriceAPI.Modules.Price.Endpoints;

public class EstimateDetailedPrice
{
    public static async Task<DetailedPriceEstimation> Handler(IPriceService service, EstimatePriceRequest request)
    {
        return await Task.FromResult(service.EstimateDetailedCosts(request));
    }
}

