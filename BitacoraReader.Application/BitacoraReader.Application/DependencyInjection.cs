using BitacoraReader.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BitacoraReader.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddScoped<IBitacoraService, BitacoraService>();

            return services;
        }
    }
}