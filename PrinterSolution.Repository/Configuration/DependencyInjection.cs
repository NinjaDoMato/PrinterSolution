using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PrinterSolution.Repository.AutoMapper;

namespace PrinterSolution.Repository.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mappingConfig = new(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(Program));
            return services;
        }
    }
}
