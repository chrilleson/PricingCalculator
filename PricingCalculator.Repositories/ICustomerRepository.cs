using PricingCalculator.Domain;

namespace PricingCalculator.Repositories;

public interface ICustomerRepository
{
    Customer Get(int id);
}