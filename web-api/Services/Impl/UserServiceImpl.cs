using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class UserServiceImpl : IUserService
{
    public IEnumerable<User> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<User>().ToList();
    }

    public User GetUserById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var user = session.Query<User>().SingleOrDefault(x => x.Id == id);

        if (user == null)
        {
            throw new BadHttpRequestException("User not found");
        }
        
        return user;
    }

    public void DeleteUser(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<User>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public User UpdateUser(int id, UserDto userDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var oldUser = GetUserById(id);

        if (oldUser == null) throw new Exception("User not found");

        var user = new User
        {
            Id = id,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            CreatedAt = oldUser.CreatedAt,
            UpdatedAt = DateTime.Now
        };

        using var transaction = session.BeginTransaction();
        
        session.Merge(user);
        transaction.Commit();

        return user;
    }

    public User SaveUser(UserDto userDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var user = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        session.Save(user);

        return user;
    }
}