using PricingCalculator.Domain;

namespace PricingCalculator.Repositories;

public interface IDiscountRepository
{
    Discount? Get(int customerId, Service service, DateOnly date);
}