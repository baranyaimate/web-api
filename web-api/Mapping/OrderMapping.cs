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
        // TODO: Fix products
        HasMany(x => x.Products).Not.LazyLoad();
        Map(x => x.CreatedAt);
        Map(x => x.UpdatedAt);
    }
}