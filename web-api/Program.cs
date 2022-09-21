using Mapster;
using web_api.Models;
using web_api.Models.DTO;
using web_api.Services;
using web_api.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IAddressService), typeof(AddressServiceImpl));
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderServiceImpl));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductServiceImpl));
builder.Services.AddScoped(typeof(IUserService), typeof(UserServiceImpl));

// TODO: Fix this after CreatedAt and UpdatedAt fields
// Add Mapster configs
TypeAdapterConfig<AddressDto, Address>.NewConfig()
    .Map(d => d.Country, s => s.Country)
    .Map(d => d.City, s => s.City)
    .Map(d => d.Postcode, s => s.Postcode)
    .Map(d => d.State, s => s.State)
    .Map(d => d.StreetName, s => s.StreetName)
    .Map(d => d.StreetNumber, s => s.StreetNumber)
    .Map(d => d.User, s => MapContext.Current.GetService<IUserService>().GetUserById(s.UserId));

TypeAdapterConfig<OrderDto, Order>.NewConfig()
    .Map(d => d.User, s => MapContext.Current.GetService<IUserService>().GetUserById(s.UserId))
    .Map(d => d.Products, s => MapContext.Current.GetService<IProductService>().GetProductsByIds(s.ProductIds));

TypeAdapterConfig<ProductDto, Product>.NewConfig()
    .Map(d => d.Name, s => s.Name)
    .Map(d => d.Price, s => s.Price);

TypeAdapterConfig<UserDto, User>.NewConfig()
    .Map(d => d.FirstName, s => s.FirstName)
    .Map(d => d.LastName, s => s.LastName)
    .Map(d => d.Email, s => s.Email);

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

app.UseAuthorization();

app.MapControllers();

app.Run();