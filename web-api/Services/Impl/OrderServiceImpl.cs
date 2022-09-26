using FluentNHibernate.Conventions;
using MapsterMapper;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class OrderServiceImpl : IOrderService
{
    private readonly IMapper _mapper;

    public OrderServiceImpl(IMapper mapper)
    {
        _mapper = mapper;
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

        var order = GetOrderById(id);

        if (order == null) throw new Exception("Order not found");
        
        session.Query<Order>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public Order UpdateOrder(int id, OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var order = _mapper.Map<Order>(orderDto);
        
        using var transaction = session.BeginTransaction();
        
        session.Merge(order);
        transaction.Commit();

        return order;
    }

    public Order SaveOrder(OrderDto orderDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var order = _mapper.Map<Order>(orderDto);

        session.Save(order);

        return order;
    }
    
    public bool IsEmpty()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Order>().IsEmpty();
    }
}