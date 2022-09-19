using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using Microsoft.EntityFrameworkCore;

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
        
        return session.QueryOver<User>()
            .Where(x => x.Id == id)
            .SingleOrDefault();
    }
    
    // PUT: api/user/{id}
    [HttpPut("{id}")]
    public IActionResult PutUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        try
        {
            //TODO
            //_context.Entry(user).State = EntityState.Modified;
            
            //_context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IsUserExists(id))
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        return NoContent();
    }
    
    // POST: api/user
    [HttpPost]
    public ActionResult<User> PostUser(User user)
    {
        //TODO
        //_context.Users.Add(user);
        //await _context.SaveChangesAsync();

        //return CreatedAtAction("GetUser", new { id = user.Id }, user);
        return null;
    }
    
    // DELETE: api/user/{id}
    [HttpDelete("{id}")]
    public ActionResult<User> DeleteUser(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var user = session.QueryOver<User>()
            .Where(x => x.Id == id)
            .SingleOrDefault();
        
        if (user == null)
        {
            return NotFound();
        }

        //TODO
        //_context.Users.Remove(user);
        //await _context.SaveChangesAsync();

        return user;
    }
    
    private bool IsUserExists(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.QueryOver<User>()
            .Where(x => x.Id == id)
            .IsAny();
    }
}