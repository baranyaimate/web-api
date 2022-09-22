using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class OrderServiceImpl : IOrderService
{
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public OrderServiceImpl(IProductService productService, IUserService userService)
    {
        _productService = productService;
        _userService = userService;
    }
    
    public IEnumerable<Order> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Order>().ToList();
    }

    public Order GetOrderById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var order = session.Query<Order>().SingleOrDefault(x => x.Id == id);

        if (order == null) throw new BadHttpRequestException("Order not found");

        return order;
    }

    public void DeleteOrder(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Order>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public Order UpdateOrder(int id, OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var oldOrder = GetOrderById(id);
        var user = _userService.GetUserById(orderDto.UserId);
        var products = _productService.GetProductsByIds(orderDto.ProductIds);
        
        var order = new Order
        {
            Id = id,
            User = user,
            Products = products,
            CreatedAt = oldOrder.CreatedAt,
            UpdatedAt = DateTime.Now
        };

        using var transaction = session.BeginTransaction();
        
        session.Merge(order);
        transaction.Commit();

        return order;
    }

    public Order SaveOrder(OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var user = _userService.GetUserById(orderDto.UserId);
        var products = _productService.GetProductsByIds(orderDto.ProductIds);
        
        var order = new Order
        {
            User = user,
            Products = products,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        session.Save(order);

        return order;
    }
}