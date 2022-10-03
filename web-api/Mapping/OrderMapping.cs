using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class OrderMapping : ClassMap<Order>
{
    public const string OrderTableName = "orders";
    public const string ForeignKeyColumnName = "[order_id]";


    public OrderMapping()
    {
        Table(OrderTableName);
        Id(o => o.Id);
        References(o => o.User)
            .Column(UserMapping.ForeignKeyColumnName)
            .Not.LazyLoad();
        HasManyToMany(o => o.Products)
            .Table(OrderHasProductMapping.OrdersHasProductsTableName)
            .ParentKeyColumn(ForeignKeyColumnName)
            .ChildKeyColumn(ProductMapping.ForeignKeyColumnName)
            .Not.LazyLoad();
    }
}