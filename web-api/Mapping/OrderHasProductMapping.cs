using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class OrderHasProductMapping : ClassMap<OrderHasProduct>
{
    private const string OrdersHasProductsTableName = "ordersHasProducts";
    
    public OrderHasProductMapping()
    {
        Table(OrdersHasProductsTableName);
        Id(x => x.Id);
        References(x => x.Order);
        References(x => x.Product);

    }
}