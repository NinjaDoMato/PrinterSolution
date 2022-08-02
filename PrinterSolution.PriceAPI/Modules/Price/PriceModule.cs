using PrinterSolution.PriceAPI.Modules.Price.Endpoints;
using PrinterSolution.Service.Interfaces;
using PrinterSolution.Service.Services;

namespace PrinterSolution.PriceAPI.Modules.Price
{
    public class PriceModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IPriceService, PriceService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/Price/EstimatePrice", EstimateDetailedPrice.Handler);

            return endpoints;
        }
    }
}
