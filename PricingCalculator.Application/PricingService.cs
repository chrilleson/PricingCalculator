using PricingCalculator.Domain;
using PricingCalculator.Repositories;

namespace PricingCalculator.Services;

public class PricingService : IPricingService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IPricingRepository _pricingRepository;

    public PricingService(ICustomerRepository customerRepository, IDiscountRepository discountRepository, IPricingRepository pricingRepository)
    {
        _customerRepository = customerRepository;
        _discountRepository = discountRepository;
        _pricingRepository = pricingRepository;
    }

    public decimal GetServicePrice(int customerId, Service service, DateOnly start, DateOnly end)
    {
        var dates = GetDateRange(start, end);
        var customer = _customerRepository.Get(customerId);
        var customerFreeDays = customer.FreeDays ?? 0;
        var totalPrice = 0m;

        foreach (var date in dates)
        {
            if (service is Service.A or Service.B && date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                continue;
            }

            if (customerFreeDays is not 0)
            {
                totalPrice += 0m;
                customerFreeDays--;
                continue;
            }

            var price = _pricingRepository.GetByCustomer(service, customerId) ?? _pricingRepository.Get(service);
            var discount = _discountRepository.Get(customerId, service, date);
            if (discount is not null && date >= discount.ValidFrom && ((discount.ValidTo is not null && date <= discount.ValidTo) || discount.ValidTo is null))
            {
                totalPrice += price * discount.Percentage;
                continue;
            }

            totalPrice += price;
        }

        return totalPrice;
    }


    private static IEnumerable<DateOnly> GetDateRange(DateOnly startDate, DateOnly endDate)
    {
        while (startDate <= endDate)
        {
            yield return startDate;
            startDate = startDate.AddDays(1);
        }
    }
}