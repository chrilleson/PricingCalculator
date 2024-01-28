namespace PricingCalculator.Domain;

public record Price(Service Service, decimal Value, int? CustomerId);