using System.Reflection;
using Mapster;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection service)
    {
        TypeAdapterConfig<Product, ProductDto>.NewConfig()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price);
        
        TypeAdapterConfig<User, UserDto>.NewConfig()
            .Map(d => d.FirstName, s => s.FirstName)
            .Map(d => d.LastName, s => s.LastName)
            .Map(d => d.Email, s => s.Email);
        
        /*
        TODO: Fix this dependency injection
        TypeAdapterConfig<AddressDto, Address>()
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
        */

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}