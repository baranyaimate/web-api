using Mapster;
using web_api.Models;
using web_api.Models.DTO;
using web_api.Services;

namespace web_api;

public class MapperConfig
{
    
    private static void RegisterAddressMapping(TypeAdapterConfig config)
    {
        config.NewConfig<AddressDto, Address>()
            .Map(d => d.Country, s => s.Country)
            .Map(d => d.City, s => s.City)
            .Map(d => d.Postcode, s => s.Postcode)
            .Map(d => d.State, s => s.State)
            .Map(d => d.StreetName, s => s.StreetName)
            .Map(d => d.StreetNumber, s => s.StreetNumber)
            .Map(d => d.User, s => MapContext.Current.GetService<IUserService>().GetUserById(s.UserId));
    }
    
    private static void RegisterOrderMapping(TypeAdapterConfig config)
    {
        config.NewConfig<OrderDto, Order>()
            .Map(d => d.User, s => MapContext.Current.GetService<IUserService>().GetUserById(s.UserId))
            .Map(d => d.Products, s => MapContext.Current.GetService<IProductService>().GetProductsByIds(s.ProductIds));
    }
    
    private static void RegisterProductMapping(TypeAdapterConfig config)
    {
        config.NewConfig<ProductDto, Product>()
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Price, s => s.Price);
    }
    
    private static void RegisterUserMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UserDto, User>()
            .Map(d => d.FirstName, s => s.FirstName)
            .Map(d => d.LastName, s => s.LastName)
            .Map(d => d.Email, s => s.Email);
    }
}