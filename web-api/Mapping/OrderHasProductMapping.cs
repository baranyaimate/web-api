using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class OrderHasProductMapping : ClassMap<OrderHasProduct>
{
    public const string OrdersHasProductsTableName = "ordersHasProducts";

    public OrderHasProductMapping()
    {
        Table(OrdersHasProductsTableName);
        Id(x => x.Id);
        References(x => x.Order).Column("[order_id]");
        References(x => x.Product).Column("[product_id]");
    }
}