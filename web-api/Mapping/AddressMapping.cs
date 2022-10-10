using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class AddressMapping : ClassMap<Address>
{
    public const string AddressTableName = "addresses";

    public AddressMapping()
    {
        Table(AddressTableName);
        Id(a => a.Id);
        Map(a => a.City);
        Map(a => a.Country);
        Map(a => a.Postcode);
        Map(a => a.State);
        Map(a => a.StreetName);
        Map(a => a.StreetNumber);
        References(a => a.User)
            .Column(UserMapping.ForeignKeyColumnName)
            .NotFound.Ignore()
            .Not.LazyLoad();
    }
}