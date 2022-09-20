using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NHibernate.Linq;
using web_api.Models;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    // GET: api/order
    [HttpGet(Name = "GetAllOrder")]
    public ActionResult<IEnumerable<Order>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<Order>().ToList();
    }
    
    // GET: api/order/{id}
    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Order>().Single(x => x.Id == id);
    }
    
    // PUT: api/order/{id}
    [HttpPut("{id}")]
    public ActionResult<Order> PutOrder(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        try
        {
            using var session = FluentNHibernateHelper.OpenSession();

            session.Update(order);

            return order;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            if (!IsOrderExists(id))
            {
                return NotFound();
            }
            Console.WriteLine(exception.ToString());
            return BadRequest();
        }
    }
    
    // POST: api/order
    [HttpPost]
    public ActionResult<Order> PostOrder(Order order)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Save(order);
        
        return order;
    }
    
    // DELETE: api/order/{id}
    [HttpDelete("{id}")]
    public void DeleteOrder(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Order>()
            .Where(x => x.Id == id)
            .Delete();
    }
    
    private bool IsOrderExists(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.QueryOver<Order>()
            .Where(x => x.Id == id)
            .IsAny();
    }
}