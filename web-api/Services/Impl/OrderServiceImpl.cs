using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
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
    
    public ActionResult<IEnumerable<Order>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Order>().ToList();
    }

    public ActionResult<Order> GetOrderById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Order>().Single(x => x.Id == id);
    }

    public void DeleteOrder(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Order>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public ActionResult<Order> UpdateOrder(int id, OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var oldOrder = GetOrderById(id).Value;
        var user = _userService.GetUserById(orderDto.UserId).Value;
        var products = _productService.GetProductsByIds(orderDto.ProductIds);

        if (user == null || oldOrder == null || products.IsEmpty()) throw new Exception("Not found");

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

    public ActionResult<Order> SaveOrder(OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var user = _userService.GetUserById(orderDto.UserId).Value;
        var products = _productService.GetProductsByIds(orderDto.ProductIds);

        if (user == null || products.IsEmpty()) throw new Exception("Not found");

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