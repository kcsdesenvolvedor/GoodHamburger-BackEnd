using GoodHamburger.Application.Services.OrderSevice;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Application
{
    public static class ServiceExtension
    {
        public static void ConfigureServicesApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
