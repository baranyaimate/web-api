using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class ProductMapping : ClassMap<Product>
{
    private const string ProductTableName = "products";
    public ProductMapping()
    {
        Table(ProductTableName);
        Id(x => x.Id);
        Map(x => x.Name);
        Map(x => x.Price);
        Map(x => x.CreatedAt).Default("getdate()");
        Map(x => x.UpdatedAt);
    }
}