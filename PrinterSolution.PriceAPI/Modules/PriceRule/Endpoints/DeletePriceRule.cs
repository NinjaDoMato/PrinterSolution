using PrinterSolution.Common.DTOs;
using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.PriceAPI.Models.Requests;
using PrinterSolution.Service.Interfaces;

namespace PrinterSolution.PriceAPI.Modules.Price.Endpoints;

public class DeltePriceRule
{
    public static async Task<bool> Handler(IPriceRuleService service, int id)
    {
        return await Task.FromResult(service.DeleteRule(id));
    }
}

