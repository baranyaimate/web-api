using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class UserMapping : ClassMap<User>
{
    public UserMapping()
    {
        Table("users");
        Id(x => x.Id);
        Map(x => x.FirstName);
        Map(x => x.LastName);
        Map(x => x.Email);
        Map(x => x.Password);
        Map(x => x.CreatedAt);
        Map(x => x.UpdatedAt);
    }
}