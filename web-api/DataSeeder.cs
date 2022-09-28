using web_api.Models.DTO;
using web_api.Services;

namespace web_api;

public class DataSeeder
{
    private readonly IAddressService _addressService;
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public DataSeeder(
        IAddressService addressService,
        IOrderService orderService,
        IProductService productService,
        IUserService userService)
    {
        _addressService = addressService;
        _orderService = orderService;
        _productService = productService;
        _userService = userService;
    }

    public void Seed()
    {
        SeedUsers();
        SeedAddresses();
        //TODO: Error when run with empty tables 
        SeedProducts();
        SeedOrders();
    }

    private void SeedAddresses()
    {
        if (!_addressService.IsEmpty()) return;

        using var session = FluentNHibernateHelper.OpenSession();

        var query = session.CreateSQLQuery(@"
            alter table
                drop constraint FK_users_addresses;

            alter table addresses
                add constraint FK_users_addresses
                foreign key ([user_id])
                references users
		        on delete cascade
                on update cascade;
        ");
        query.ExecuteUpdate();

        var address1 = new AddressDto
        {
            City = "Rose Hill",
            Country = "United States",
            Postcode = "67133",
            State = "Kansas",
            StreetName = "Henery Street",
            StreetNumber = "990",
            UserId = 1
        };

        var address2 = new AddressDto
        {
            City = "Louisville",
            Country = "United States",
            Postcode = "40203",
            State = "Kentucky",
            StreetName = "Gregory Lane",
            StreetNumber = "4542",
            UserId = 2
        };

        var address3 = new AddressDto
        {
            City = "Finchville",
            Country = "United States",
            Postcode = "40022",
            State = "Kentucky",
            StreetName = "Karen Lane",
            StreetNumber = "3083",
            UserId = 3
        };

        _addressService.SaveAddress(address1);
        _addressService.SaveAddress(address2);
        _addressService.SaveAddress(address3);
    }

    private void SeedOrders()
    {
        if (!_orderService.IsEmpty()) return;

        using var session = FluentNHibernateHelper.OpenSession();

        var query = session.CreateSQLQuery(@"
            alter table
                drop constraint FK_orders_ordersHasProducts;

            alter table ordersHasProducts
                add constraint FK_orders_ordersHasProducts
                foreign key ([order_id])
                references orders
                on delete cascade
                on update cascade;

            alter table
                drop constraint FK_products_ordersHasProducts;

            alter table ordersHasProducts
                add constraint FK_products_ordersHasProducts
                foreign key ([product_id])
                references products
                on delete cascade
                on update cascade;

            alter table
                drop constraint FK_users_orders;

            alter table orders
                add constraint FK_users_orders
                foreign key ([user_id])
                references users
                on delete cascade
                on update cascade;
        ");
        query.ExecuteUpdate();

        var order1 = new OrderDto
        {
            UserId = 1,
            ProductIds = new[] { 1 }
        };
        var order2 = new OrderDto
        {
            UserId = 2,
            ProductIds = new[] { 1, 2 }
        };
        var order3 = new OrderDto
        {
            UserId = 3,
            ProductIds = new[] { 1, 2, 3 }
        };

        _orderService.SaveOrder(order1);
        _orderService.SaveOrder(order2);
        _orderService.SaveOrder(order3);
    }

    private void SeedProducts()
    {
        if (!_productService.IsEmpty()) return;

        var product1 = new ProductDto
        {
            Name = "Apple",
            Price = 1000
        };
        var product2 = new ProductDto
        {
            Name = "Orange",
            Price = 1500
        };
        var product3 = new ProductDto
        {
            Name = "Banana",
            Price = 2000
        };

        _productService.SaveProduct(product1);
        _productService.SaveProduct(product2);
        _productService.SaveProduct(product3);
    }

    private void SeedUsers()
    {
        if (!_userService.IsEmpty()) return;

        var user1 = new UserDto
        {
            FirstName = "Johanna",
            LastName = "Watts",
            Email = "johanna.watts@gmail.com"
        };
        var user2 = new UserDto
        {
            FirstName = "Bryce",
            LastName = "Cooper",
            Email = "bryce.cooper@gmail.com"
        };
        var user3 = new UserDto
        {
            FirstName = "Lilah",
            LastName = "Davis",
            Email = "lilah.davis@gmail.com"
        };

        _userService.SaveUser(user1);
        _userService.SaveUser(user2);
        _userService.SaveUser(user3);
    }
}