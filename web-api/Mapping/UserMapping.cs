using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class UserMapping : ClassMap<User>
{
    private const string UserTableName = "users";
    public UserMapping()
    {
        Table(UserTableName);
        Id(x => x.Id);
        Map(x => x.FirstName);
        Map(x => x.LastName);
        Map(x => x.Email);
        Map(x => x.CreatedAt).Default("getdate()");
        Map(x => x.UpdatedAt);
    }
}