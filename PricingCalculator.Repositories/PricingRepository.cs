using PricingCalculator.Domain;

namespace PricingCalculator.Repositories;

internal sealed class PricingRepository : IPricingRepository
{
    public decimal Get(Service service) =>
        Prices.First(x => x.Service == service && x.CustomerId is null).Value;

    public decimal? GetByCustomer(Service service, int customerId) =>
        Prices.FirstOrDefault(x => x.Service == service && x.CustomerId == customerId)?.Value;

    private static IReadOnlyCollection<Price> Prices =>
    [
        new Price(Service: Service.A, Value: 0.20m, CustomerId: null),
        new Price(Service: Service.B, Value: 0.24m, CustomerId: null),
        new Price(Service: Service.C, Value: 0.40m, CustomerId: null),
        new Price(Service: Service.B, Value: 0.25m, CustomerId: 1),
        new Price(Service: Service.C, Value: 1.00m, CustomerId: 3)
    ];
}