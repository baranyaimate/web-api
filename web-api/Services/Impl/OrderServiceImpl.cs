using FluentNHibernate.Conventions;
using MapsterMapper;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class OrderServiceImpl : IOrderService
{
    private readonly IMapper _mapper;

    private readonly IUserService _userService;
    
    public OrderServiceImpl(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
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

        if (order is null) throw new Exception($"Order({id}) not found");

        return order;
    }

    public void DeleteOrder(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var order = GetOrderById(id);

        if (order is null) throw new Exception($"Order({id}) not found");

        session.Query<Order>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public Order UpdateOrder(int id, OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var order = _mapper.Map<Order>(orderDto);
        order.Id = id;

        using var transaction = session.BeginTransaction();

        session.Update(order);
        transaction.Commit();

        return order;
    }

    public Order SaveOrder(OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var order = _mapper.Map<Order>(orderDto);

        session.Save(order);

        foreach (var product in order.Products)
        {
            var orderHasProduct = new OrderHasProduct
            {
                Order = order,
                Product = product
            };

            session.Save(orderHasProduct);
        }

        return order;
    }

    public IEnumerable<Order> GetOrdersByUserId(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        if (_userService.GetUserById(id).Equals(null)) throw new Exception($"User({id}) not found");
        
        return session.Query<Order>().Where(order => order.User.Id == id).ToList();
    }
    
    public IEnumerable<Order> GetOrdersByProductId(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<Order>().Where(order => order.Products.Any(p => p.Id == id)).ToList();
    }

    public bool IsEmpty()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Order>().IsEmpty();
    }
}