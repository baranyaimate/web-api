﻿using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class UserServiceImpl : IUserService 
{
    public ActionResult<IEnumerable<User>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<User>().ToList();
    }
    
    public ActionResult<User> GetUserById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<User>().Single(x => x.Id == id);
    }

    public void DeleteUser(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<User>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public ActionResult<User> UpdateUser(int id, UserDto userDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        User? oldUser = session.Query<User>().SingleOrDefault(x => x.Id == id);

        if (oldUser == null)
        {
            throw new Exception("User not found");
        }

        User user = new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            CreatedAt = oldUser.CreatedAt,
            UpdatedAt = DateTime.Now
        };
        
        session.Update(user);

        return user;
    }

    public ActionResult<User> SaveUser(UserDto userDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        User user = new User
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