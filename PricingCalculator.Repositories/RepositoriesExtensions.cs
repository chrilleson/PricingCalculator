using Microsoft.Extensions.DependencyInjection;

namespace PricingCalculator.Repositories;

public static class RepositoriesExtensions
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<IDiscountRepository, DiscountRepository>();
        serviceCollection.AddScoped<IPricingRepository, PricingRepository>();
    }
}