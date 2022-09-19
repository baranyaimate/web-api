using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class ProductMapping : ClassMap<Product>
{
    public ProductMapping()
    {
        Table("products");
        Id(x => x.Id);
        Map(x => x.Name);
        Map(x => x.Price);
        Map(x => x.CreatedAt);
        Map(x => x.UpdatedAt);
    }
}