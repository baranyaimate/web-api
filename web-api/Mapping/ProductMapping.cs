using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class ProductMapping : ClassMap<Product>
{
    public const string ProductTableName = "products";
    public const string ForeignKeyColumnName = "[product_id]";


    public ProductMapping()
    {
        Table(ProductTableName);
        Id(p => p.Id);
        Map(p => p.Name);
        Map(p => p.Price);
        HasManyToMany(p => p.Orders)
            .Inverse().Table(OrderHasProductMapping.OrdersHasProductsTableName)
            .ParentKeyColumn(OrderMapping.ForeignKeyColumnName)
            .ChildKeyColumn(ForeignKeyColumnName)
            .Not.LazyLoad();
    }
}