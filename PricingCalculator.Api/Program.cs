using PricingCalculator.Domain;
using PricingCalculator.Repositories;
using PricingCalculator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/service-a/{customerId:int}/{startDate:datetime}/{endDate:datetime}", (int customerId, DateTime startDate, DateTime endDate, IPricingService pricingService) => pricingService.GetServicePrice(customerId, Service.A, DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate)))
    .WithName("GetServiceA")
    .WithOpenApi();

app.MapGet("/service-b/{customerId:int}/{startDate:datetime}/{endDate:datetime}", (int customerId, DateTime startDate, DateTime endDate, IPricingService pricingService) => pricingService.GetServicePrice(customerId, Service.B, DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate)))
    .WithName("GetServiceB")
    .WithOpenApi();

app.MapGet("/service-c/{customerId:int}/{startDate:datetime}/{endDate:datetime}", (int customerId, DateTime startDate, DateTime endDate, IPricingService pricingService) => pricingService.GetServicePrice(customerId, Service.C, DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate)))
    .WithName("GetServiceC")
    .WithOpenApi();

app.Run();