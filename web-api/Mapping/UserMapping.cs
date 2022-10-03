using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class UserMapping : ClassMap<User>
{
    public const string UserTableName = "users";

    public UserMapping()
    {
        Table(UserTableName);
        Id(x => x.Id);
        Map(x => x.FirstName);
        Map(x => x.LastName);
        Map(x => x.Email);
        HasMany(x => x.Addresses)
            .KeyColumn("[user_id]").Inverse().Not.LazyLoad();
    }
}