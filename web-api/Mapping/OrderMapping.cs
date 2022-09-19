using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class OrderMapping : ClassMap<Order>
{
    public OrderMapping()
    {
        Table("orders");
        Id(x => x.Id);
        References(x => x.User);
        HasMany(x => x.Products);
        Map(x => x.CreatedAt);
        Map(x => x.UpdatedAt);
    }
}