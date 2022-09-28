using Mapster;
using MapsterMapper;
using Newtonsoft.Json;
using web_api;
using web_api.Models;
using web_api.Models.DTO;
using web_api.Services;
using web_api.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
    options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddScoped(typeof(IAddressService), typeof(AddressServiceImpl));
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderServiceImpl));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductServiceImpl));
builder.Services.AddScoped(typeof(IUserService), typeof(UserServiceImpl));

builder.Services.AddTransient<DataSeeder>();

var config = new TypeAdapterConfig();

config.NewConfig<ProductDto, Product>()
    .Map(d => d.Name, s => s.Name)
    .Map(d => d.Price, s => s.Price)
    .MaxDepth(1);

config.NewConfig<UserDto, User>()
    .Map(d => d.FirstName, s => s.FirstName)
    .Map(d => d.LastName, s => s.LastName)
    .Map(d => d.Email, s => s.Email)
    .MaxDepth(1);

config.NewConfig<AddressDto, Address>()
    .Map(d => d.Country, s => s.Country)
    .Map(d => d.City, s => s.City)
    .Map(d => d.Postcode, s => s.Postcode)
    .Map(d => d.State, s => s.State)
    .Map(d => d.StreetName, s => s.StreetName)
    .Map(d => d.StreetNumber, s => s.StreetNumber)
    .Map(d => d.User, s => MapContext.Current.GetService<IUserService>().GetUserById(s.UserId))
    .MaxDepth(1);

config.NewConfig<OrderDto, Order>()
    .Map(d => d.User, s => MapContext.Current.GetService<IUserService>().GetUserById(s.UserId))
    .Map(d => d.Products, s => MapContext.Current.GetService<IProductService>().GetProductsByIds(s.ProductIds))
    .MaxDepth(1);


builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

using var scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
scope?.ServiceProvider.GetService<DataSeeder>()?.Seed();

app.Run();