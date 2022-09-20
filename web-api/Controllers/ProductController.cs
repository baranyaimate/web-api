using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    // GET: api/product
    [HttpGet(Name = "GetAllProduct")]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<Product>().ToList();
    }
    
    // GET: api/product/{id}
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Product>().Single(x => x.Id == id);
    }
    
    // PUT: api/product/{id}
    [HttpPut("{id}")]
    public ActionResult<Product> PutProduct(int id, ProductDto productDto)
    {
        try
        {
            using var session = FluentNHibernateHelper.OpenSession();

            Product? oldProduct = session.Query<Product>().SingleOrDefault(x => x.Id == id);

            if (oldProduct == null)
            {
                return NotFound();
            }
            
            Product product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CreatedAt = oldProduct.CreatedAt,
                UpdatedAt = DateTime.Now
            };
            
            session.Update(product);

            return product;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            if (!IsProductExists(id))
            {
                return NotFound();
            }
            Console.WriteLine(exception.ToString());
            return BadRequest();
        }
    }
    
    // POST: api/product
    [HttpPost]
    public ActionResult<Product> PostProduct(ProductDto productDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        Product product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        session.Save(product);
        
        return product;
    }
    
    // DELETE: api/product/{id}
    [HttpDelete("{id}")]
    public void DeleteProduct(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Product>()
            .Where(x => x.Id == id)
            .Delete();
    }
    
    private bool IsProductExists(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.QueryOver<Product>()
            .Where(x => x.Id == id)
            .IsAny();
    }
}