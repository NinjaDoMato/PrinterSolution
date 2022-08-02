namespace PrinterSolution.PriceAPI.Modules.Core
{
    public class CoreModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.ResolveAutoMapper();
            services.ResolveDbContext();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/Core/Health", () =>
            {
                return "It is on!";
            });

            return endpoints;
        }
    }
}
