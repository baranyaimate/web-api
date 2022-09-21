using web_api.Services;
using web_api.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IAddressService), typeof(AddressServiceImpl));
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderServiceImpl));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductServiceImpl));
builder.Services.AddScoped(typeof(IUserService), typeof(UserServiceImpl));

// TODO
//var typeAdapterConfig = new TypeAdapterConfig();
//typeAdapterConfig.Apply(new MapperConfig());
//typeAdapterConfig.Compile();

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