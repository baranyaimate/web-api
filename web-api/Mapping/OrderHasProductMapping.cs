using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class OrderHasProductMapping : ClassMap<OrderHasProduct>
{
    public const string OrdersHasProductsTableName = "ordersHasProducts";

    public OrderHasProductMapping()
    {
        Table(OrdersHasProductsTableName);
        Id(ohp => ohp.Id);
        References(ohp => ohp.Order).Column(OrderMapping.ForeignKeyColumnName);
        References(ohp => ohp.Product).Column(ProductMapping.ForeignKeyColumnName);
    }
}