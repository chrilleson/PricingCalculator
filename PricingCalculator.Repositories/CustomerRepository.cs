using PricingCalculator.Domain;

namespace PricingCalculator.Repositories;

internal sealed  class CustomerRepository : ICustomerRepository
{
    public Customer Get(int id) =>
        Customers.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Could not find customer with id: {id}");

    private static IReadOnlyCollection<Customer> Customers =>
    [
        new Customer(Id: 1, Name: "CustomerA", FreeDays: null, FreeDaysValidFrom: null),
        new Customer(Id: 2, Name: "CustomerB", FreeDays: 24, FreeDaysValidFrom: new DateOnly(2023, 01, 24)),
        new Customer(Id: 3, Name: "CustomerC", FreeDays: null, FreeDaysValidFrom: null)
    ];
}