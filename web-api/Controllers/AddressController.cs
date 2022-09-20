using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    // GET: api/address
    [HttpGet(Name = "GetAllAddress")]
    public ActionResult<IEnumerable<Address>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<Address>().ToList();
    }
    
    // GET: api/address/{id}
    [HttpGet("{id}")]
    public ActionResult<Address> GetAddress(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Address>().Single(x => x.Id == id);
    }
    
    // PUT: api/address/{id}
    [HttpPut("{id}")]
    public ActionResult<Address> PutAddress(int id, AddressDto addressDto)
    {
        try
        {
            using var session = FluentNHibernateHelper.OpenSession();

            User? user = session.Query<User>().SingleOrDefault(x => x.Id == addressDto.UserId);
            Address? oldAddress = session.Query<Address>().SingleOrDefault(x => x.Id == id);
            
            if (user == null || oldAddress == null)
            {
                return NotFound();
            }
            
            Address address = new Address
            {
                Country = addressDto.Country,
                City = addressDto.City,
                Postcode = addressDto.Postcode,
                State = addressDto.State,
                StreetName = addressDto.StreetName,
                StreetNumber = addressDto.StreetNumber,
                User = user,
                CreatedAt = oldAddress.CreatedAt,
                UpdatedAt = DateTime.Now
            };
            
            session.Update(address);

            return address;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            if (!IsAddressExists(id))
            {
                return NotFound();
            }
            Console.WriteLine(exception.ToString());
            return BadRequest();
        }
    }
    
    // POST: api/address
    [HttpPost]
    public ActionResult<Address> PostAddress(AddressDto addressDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        User? user = session.Query<User>().SingleOrDefault(x => x.Id == addressDto.UserId);

        if (user == null)
        {
            return NotFound();
        }
        
        Address address = new Address
        {
            Country = addressDto.Country,
            City = addressDto.City,
            Postcode = addressDto.Postcode,
            State = addressDto.State,
            StreetName = addressDto.StreetName,
            StreetNumber = addressDto.StreetNumber,
            User = user,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        session.Save(address);
        
        return address;
    }
    
    // DELETE: api/address/{id}
    [HttpDelete("{id}")]
    public void DeleteAddress(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Address>()
            .Where(x => x.Id == id)
            .Delete();
    }
    
    private bool IsAddressExists(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.QueryOver<Address>()
            .Where(x => x.Id == id)
            .IsAny();
    }
}