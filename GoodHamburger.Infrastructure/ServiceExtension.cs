using GoodHamburger.Domain.Repositories;
using GoodHamburger.Infrastructure.Data;
using GoodHamburger.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Infrastructure
{
    public static class ServiceExtension
    {
        public static void ConfigureServicesInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<OrderDbContext>(options => options.UseSqlite(connectionString));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
