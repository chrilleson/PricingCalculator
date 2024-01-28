using FluentAssertions;
using NSubstitute;
using PricingCalculator.Domain;
using PricingCalculator.Repositories;
using PricingCalculator.Services;

namespace PricingCalculator.Tests;

public class PricingServiceTest
{
    [Fact]
    public void PricingService_CustomerHasDiscount_AccumulatedPriceWithDiscount()
    {
        var startDate = new DateOnly(2019, 09, 20);
        var endDate = new DateOnly(2019, 10, 01);
        var discount = new Discount(1, Service.C, 0.20m, new DateOnly(2019, 09, 22), new DateOnly(2019, 09, 24));
        var (sut, customerRepository, discountRepository, pricingRepository) = CreateSut();
        customerRepository.Get(1).Returns(new Customer(1, "CustomerX", null, null));
        discountRepository.Get(1, Service.C, new DateOnly(2019, 09, 22)).Returns(discount);
        discountRepository.Get(1, Service.C, new DateOnly(2019, 09, 23)).Returns(discount);
        discountRepository.Get(1, Service.C, new DateOnly(2019, 09, 24)).Returns(discount);
        pricingRepository.Get(Service.A).Returns(0.20m);
        pricingRepository.Get(Service.C).Returns(0.40m);

        var actualPriceServiceA = sut.GetServicePrice(1, Service.A, startDate, endDate);
        var actualPriceServiceC = sut.GetServicePrice(1, Service.C, startDate, endDate);
        var actualTotalPrice = actualPriceServiceA + actualPriceServiceC;

        actualPriceServiceA.Should().Be(1.6m);
        actualPriceServiceC.Should().Be(3.84m);
        actualTotalPrice.Should().Be(5.44m);
    }

    [Fact]
    public void PricingService_CustomerHasFreeDaysAndDiscount_AccumulatedPriceWithFreeDaysAndDiscount()
    {
        var startDate = new DateOnly(2018, 01, 01);
        var endDate = new DateOnly(2019, 10, 01);
        var discountServiceB = new Discount(CustomerId: 1, Service: Service.B, Percentage: 0.30m, ValidFrom: new DateOnly(2018, 01, 01), ValidTo: null);
        var discountServiceC = new Discount(CustomerId: 1, Service: Service.C, Percentage: 0.30m, ValidFrom: new DateOnly(2018, 01, 01), ValidTo: null);
        var (sut, customerRepository, discountRepository, pricingRepository) = CreateSut();
        customerRepository.Get(default).Returns(new Customer(Id: default, Name: "CustomerY", FreeDays: 200, FreeDaysValidFrom: startDate));
        discountRepository.Get(default, Service.B,default).ReturnsForAnyArgs(discountServiceB);
        discountRepository.Get(default, Service.C, default).ReturnsForAnyArgs(discountServiceC);
        pricingRepository.Get(Service.B).Returns(0.24m);
        pricingRepository.Get(Service.C).Returns(0.40m);

        var actualPriceServiceB = sut.GetServicePrice(default, Service.B, startDate, endDate);
        var actualPriceServiceC = sut.GetServicePrice(default, Service.C, startDate, endDate);
        var actualTotalPrice = actualPriceServiceB + actualPriceServiceC;

        actualPriceServiceB.Should().Be(60.504m);
        actualPriceServiceC.Should().Be(172.8m);
        actualTotalPrice.Should().Be(233.3040m);
    }

    private (PricingService, ICustomerRepository, IDiscountRepository, IPricingRepository) CreateSut()
    {
        var customerRepository = Substitute.For<ICustomerRepository>();
        var discountRepository = Substitute.For<IDiscountRepository>();
        var pricingRepository = Substitute.For<IPricingRepository>();

        var sut = new PricingService(customerRepository, discountRepository, pricingRepository);
        return (sut, customerRepository, discountRepository, pricingRepository);
    }
}