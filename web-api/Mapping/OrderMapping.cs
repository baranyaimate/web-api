using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class OrderMapping : ClassMap<Order>
{
    public const string OrderTableName = "orders";

    public OrderMapping()
    {
        Table(OrderTableName);
        Id(x => x.Id);
        References(x => x.User)
            .Column("[user_id]")
            .Not.LazyLoad();
        HasManyToMany(x => x.Products)
            .Table(OrderHasProductMapping.OrdersHasProductsTableName)
            .ParentKeyColumn("[order_id]")
            .ChildKeyColumn("[product_id]")
            .Not.LazyLoad();
    }
}