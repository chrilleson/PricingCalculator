using PricingCalculator.Domain;

namespace PricingCalculator.Repositories;

public interface IPricingRepository
{
    decimal Get(Service service);

    decimal? GetByCustomer(Service service, int customerId);
}