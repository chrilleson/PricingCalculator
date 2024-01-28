using PricingCalculator.Domain;

namespace PricingCalculator.Repositories;

internal sealed  class DiscountRepository : IDiscountRepository
{
    public Discount? Get(int customerId, Service service, DateOnly date) =>
        Discounts.FirstOrDefault(x => x.CustomerId == customerId && x.Service == service && date >= x.ValidFrom && date <= x.ValidTo);

    private static IReadOnlyCollection<Discount?> Discounts =>
        [
            new Discount(CustomerId: 1, Service: Service.A, Percentage: 0.2m, ValidFrom: new DateOnly(2024, 01, 01), ValidTo: new DateOnly(2024, 01, 31)),
            new Discount(CustomerId: 3, Service: Service.A, Percentage: 0.25m, ValidFrom: new DateOnly(2023, 10, 01), ValidTo: new DateOnly(2023, 12, 31)),
            new Discount(CustomerId: 2, Service: Service.C, Percentage: 0.5m, ValidFrom: new DateOnly(2024, 01, 24), ValidTo: new DateOnly(2024, 01, 31)),
            new Discount(CustomerId: 1, Service: Service.B, Percentage: 0.3m, ValidFrom: new DateOnly(2024, 01, 01), ValidTo: null)
        ];
}