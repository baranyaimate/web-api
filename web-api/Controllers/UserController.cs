﻿using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using Microsoft.EntityFrameworkCore;
using NHibernate.Linq;
using web_api.Models.DTO;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // GET: api/user
    [HttpGet(Name = "GetAllUser")]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<User>().ToList();
    }
    
    // GET: api/user/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<User>().Single(x => x.Id == id);
    }
    
    // PUT: api/user/{id}
    [HttpPut("{id}")]
    public ActionResult<User> PutUser(int id, UserDto userDto)
    {
        try
        {
            using var session = FluentNHibernateHelper.OpenSession();

            User? oldUser = session.Query<User>().SingleOrDefault(x => x.Id == id);

            if (oldUser == null)
            {
                return NotFound();
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
        catch (DbUpdateConcurrencyException exception)
        {
            if (!IsUserExists(id))
            {
                return NotFound();
            }
            Console.WriteLine(exception.ToString());
            return BadRequest();
        }
    }
    
    // POST: api/user
    [HttpPost]
    public ActionResult<User> PostUser(UserDto userDto)
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
    
    // DELETE: api/user/{id}
    [HttpDelete("{id}")]
    public void DeleteUser(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<User>()
            .Where(x => x.Id == id)
            .Delete();
    }
    
    private bool IsUserExists(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.QueryOver<User>()
            .Where(x => x.Id == id)
            .IsAny();
    }
}