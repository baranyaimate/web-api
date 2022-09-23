using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using web_api.Models;

namespace web_api.Mapping;

public class ProductMapping : ClassMap<Product>
{
    private const string ProductTableName = "products";
    private const string OrdersHasProductsTableName = "OrdersHasProducts";
    public ProductMapping()
    {
        Table(ProductTableName);
        Id(x => x.Id);
        Map(x => x.Name);
        Map(x => x.Price);
        HasManyToMany(x => x.Orders)
            .Cascade.All().Inverse().Table(OrdersHasProductsTableName).Not.LazyLoad();
    }
}