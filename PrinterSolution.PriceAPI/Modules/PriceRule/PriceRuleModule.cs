using PrinterSolution.PriceAPI.Modules.Price.Endpoints;
using PrinterSolution.Repository.Interfaces;
using PrinterSolution.Repository.Repositories;
using PrinterSolution.Service.Interfaces;
using PrinterSolution.Service.Services;

namespace PrinterSolution.PriceAPI.Modules.PriceRule
{
    public class PriceRuleModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IRepository<Repository.Entities.PriceRule>, Repository<Repository.Entities.PriceRule>>();
            services.AddScoped<IPriceRuleService, PriceRuleService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/PriceRule/{id}", GetPriceRule.Handler);
            endpoints.MapPost("/PriceRule", CreatePriceRule.Handler);
            endpoints.MapPut("/PriceRule", UpdatePriceRule.Handler);
            endpoints.MapDelete("/PriceRule/{id}", UpdatePriceRule.Handler);

            return endpoints;
        }
    }
}
