using PricingCalculator.Domain;

namespace PricingCalculator.Services;

public interface IPricingService
{
    decimal GetServicePrice(int customerId, Service service, DateOnly start, DateOnly end);
}