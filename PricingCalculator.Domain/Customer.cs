namespace PricingCalculator.Domain;

public record Customer(int Id, string Name, int? FreeDays, DateOnly? FreeDaysValidFrom);