using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class AddressMapping : ClassMap<Address>
{
    public const string AddressTableName = "addresses";

    public AddressMapping()
    {
        Table(AddressTableName);
        Id(x => x.Id);
        Map(x => x.City);
        Map(x => x.Country);
        Map(x => x.Postcode);
        Map(x => x.State);
        Map(x => x.StreetName);
        Map(x => x.StreetNumber);
        References(x => x.User)
            .Column("[user_id]").Not.LazyLoad();
    }
}