using Microsoft.Extensions.DependencyInjection;

namespace PricingCalculator.Services;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPricingService, PricingService>();
    }
}