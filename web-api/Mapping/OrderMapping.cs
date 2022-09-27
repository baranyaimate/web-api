using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class OrderMapping : ClassMap<Order>
{
    private const string OrderTableName = "orders";
    private const string OrdersHasProductsTableName = "ordersHasProducts";

    public OrderMapping()
    {
        Table(OrderTableName);
        Id(x => x.Id);
        References(x => x.User)
            .Column("[user_id]")
            .Not.LazyLoad();
        HasManyToMany(x => x.Products)
            .Table(OrdersHasProductsTableName)
            .ParentKeyColumn("[order_id]")
            .ChildKeyColumn("[product_id]")
            .Not.LazyLoad();
    }
}