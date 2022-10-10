using FluentNHibernate.Mapping;
using web_api.Models;

namespace web_api.Mapping;

public class UserMapping : ClassMap<User>
{
    public const string UserTableName = "users";
    public const string ForeignKeyColumnName = "[user_id]";
    
    public UserMapping()
    {
        Table(UserTableName);
        Id(u => u.Id);
        Map(u => u.FirstName);
        Map(u => u.LastName);
        Map(u => u.Email);
        HasMany(u => u.Addresses)
            .KeyColumn(ForeignKeyColumnName)
            .Inverse()
            .NotFound.Ignore()
            .Not.LazyLoad();
    }
}