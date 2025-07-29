using BitacoraReader.Domain.Interfaces;
using BitacoraReader.Infrastructure.Data;
using BitacoraReader.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitacoraReader.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BitacoraDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(BitacoraDbContext).Assembly.FullName)));

            services.AddScoped<IBitacoraRepository, BitacoraRepository>();

            return services;
        }
    }
}