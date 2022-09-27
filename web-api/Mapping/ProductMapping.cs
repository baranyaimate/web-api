using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class ProductMapping : ClassMap<Product>
{
    private const string ProductTableName = "products";
    private const string OrdersHasProductsTableName = "ordersHasProducts";

    public ProductMapping()
    {
        Table(ProductTableName);
        Id(x => x.Id);
        Map(x => x.Name);
        Map(x => x.Price);
        HasManyToMany(x => x.Orders)
            .Inverse().Table(OrdersHasProductsTableName)
            .ParentKeyColumn("[order_id]")
            .ChildKeyColumn("[product_id]")
            .Not.LazyLoad();
    }
}