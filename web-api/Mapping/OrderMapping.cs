using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class OrderMapping : ClassMap<Order>
{
    private const string OrderTableName = "orders";
    public OrderMapping()
    {
        Table(OrderTableName);
        Id(x => x.Id);
        References(x => x.User).Not.LazyLoad();
        // TODO: Products not returned when get all orders
        HasMany(x => x.Products).Not.LazyLoad();
    }
}