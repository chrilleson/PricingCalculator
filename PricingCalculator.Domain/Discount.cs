namespace PricingCalculator.Domain;

public record Discount(int CustomerId, Service Service, decimal Percentage, DateOnly ValidFrom, DateOnly? ValidTo);